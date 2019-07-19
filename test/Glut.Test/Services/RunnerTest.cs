using Glut.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace Glut.Services
{
    public class RunnerTest
    {
        [Fact]
        public void CountWork_ShallTimeOutTest()
        {
            var service = new TestWorker
            {
                SleepDurationMilliseconds = 800
            };

            var logger = new LoggerFactory().CreateLogger<Runner>();
            var threadResult = new ThreadResult();
            var worker = new Runner(service, threadResult, logger);
            var messageA = new HttpRequestMessage(HttpMethod.Get, "/a");
            var messageB = new HttpRequestMessage(HttpMethod.Get, "/b");
            var resetEvent = new ManualResetEventSlim(false);

            var cts = new CancellationTokenSource(100);
            var cancellationToken = cts.Token;

            Assert.ThrowsAsync<TaskCanceledException>(async () =>
            {
                await worker.CountWork(new[] { messageA, messageB, }, 1, resetEvent, cancellationToken);
            });
        }

        [Fact]
        public async Task CountWork_ShallRunAllMessagesTest()
        {
            var service = new TestWorker
            {
                SleepDurationMilliseconds = 100
            };

            var logger = new LoggerFactory().CreateLogger<Runner>();
            var threadResult = new ThreadResult();
            var worker = new Runner(service, threadResult, logger);
            var messageA = new HttpRequestMessage(HttpMethod.Get, "/a");
            var messageB = new HttpRequestMessage(HttpMethod.Get, "/b");
            var resetEvent = new ManualResetEventSlim(false);

            var cts1 = new CancellationTokenSource(1000);
            var cancellationToken = cts1.Token;

            await worker.CountWork(new[] { messageA, messageB }, 1, resetEvent, cancellationToken);

            var results = threadResult.Results;

            Assert.Equal(2, results.Count);
        }

        [Fact]
        public async Task CountWork_ShallRunOneMessagesTest()
        {
            var service = new TestWorker
            {
                SleepDurationMilliseconds = 100
            };

            var logger = new LoggerFactory().CreateLogger<Runner>();
            var threadResult = new ThreadResult();
            var worker = new Runner(service, threadResult, logger);
            var messageA = new HttpRequestMessage(HttpMethod.Get, "/a");
            var messageB = new HttpRequestMessage(HttpMethod.Get, "/b");
            var resetEvent = new ManualResetEventSlim(false);

            var cts1 = new CancellationTokenSource(100);
            var cancellationToken = cts1.Token;

            await worker.CountWork(new[] { messageA, messageB }, 1, resetEvent, cancellationToken);

            var results = threadResult.Results;

            Assert.Equal(1, results.Count);
        }

        [Fact]
        public void DurationWork_ShallTimeOutTest()
        {
            var duration = TimeSpan.FromSeconds(2);
            var stopWatch = new Stopwatch();

            var service = new TestWorker
            {
                SleepDurationMilliseconds = 800
            };

            var logger = new LoggerFactory().CreateLogger<Runner>();
            var threadResult = new ThreadResult();
            var worker = new Runner(service, threadResult, logger);
            var messageA = new HttpRequestMessage(HttpMethod.Get, "/a");
            var messageB = new HttpRequestMessage(HttpMethod.Get, "/b");
            var resetEvent = new ManualResetEventSlim(false);

            var cts = new CancellationTokenSource(100);
            var cancellationToken = cts.Token;

            Assert.ThrowsAsync<TaskCanceledException>(async () =>
            {
                await worker.DurationWork(new[] { messageA, messageB }, duration, stopWatch, resetEvent, cancellationToken);
            });
        }

        [Fact]
        public async Task DurationWork_ShallRunAllMessagesTest()
        {
            var duration = TimeSpan.FromSeconds(1);
            var stopWatch = Stopwatch.StartNew();

            var service = new TestWorker
            {
                SleepDurationMilliseconds = 100
            };

            var logger = new LoggerFactory().CreateLogger<Runner>();
            var threadResult = new ThreadResult();
            var worker = new Runner(service, threadResult, logger);
            var messageA = new HttpRequestMessage(HttpMethod.Get, "/a");
            var messageB = new HttpRequestMessage(HttpMethod.Get, "/b");
            var resetEvent = new ManualResetEventSlim(false);

            var cts = new CancellationTokenSource(2000);
            var cancellationToken = cts.Token;

            await worker.DurationWork(new[] { messageA, messageB }, duration, stopWatch, resetEvent, cancellationToken);

            var results = threadResult.Results;

            Assert.Equal(2, results.Count);
        }

        [Fact]
        public void CreateWorkerThreads_CountWorkTest()
        {
            var service = new TestWorker
            {
                SleepDurationMilliseconds = 100
            };

            var logger = new LoggerFactory().CreateLogger<Runner>();
            var threadResult = new ThreadResult();
            var worker = new Runner(service, threadResult, logger);
            var messageA = new HttpRequestMessage(HttpMethod.Get, "/a");
            var messageB = new HttpRequestMessage(HttpMethod.Get, "/b");

            var cts = new CancellationTokenSource(1000);
            var cancellationToken = cts.Token;

            worker.CreateWorkerThreads(1, TimeSpan.FromSeconds(0), 1, 0, new[] { messageA, messageB }, cancellationToken);

            var results = threadResult.Results;

            Assert.Equal(2, results.Count);
        }

        class TestWorker : IWorker
        {
            public int SleepDurationMilliseconds { get; set; } = 10000;

            public async ValueTask Run(HttpRequestMessage request, ThreadResult result, CancellationToken cancellationToken)
            {
                await Task.Factory.StartNew
                      (() =>
                      {
                          Task.Delay(SleepDurationMilliseconds, cancellationToken).Wait();
                          result.Add(request.RequestUri.ToString(), DateTime.UtcNow, DateTime.UtcNow.AddSeconds(1), true, 200, 0, 0, 0, 0, null, null);
                      }, cancellationToken);
            }
        }
    }
}
