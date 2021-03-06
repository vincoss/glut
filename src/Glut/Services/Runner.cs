﻿using Glut.Interface;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Glut
{
    // TODO: Refactor this
    public class Runner
    {
        private readonly IWorker _worker;
        private readonly ThreadResult _threadResult;
        private readonly ILogger<Runner> _logger;

        public Runner(IWorker worker, ThreadResult threadResult, ILogger<Runner> logger)
        {
            if(worker == null)
            {
                throw new ArgumentNullException(nameof(worker));
            }
            if(threadResult == null)
            {
                throw new ArgumentNullException(nameof(threadResult));
            }
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            _worker = worker;
            _threadResult = threadResult;
            _logger = logger;
        }

        public void CreateWorkerThreads(int threads, TimeSpan duration, int count, long interval, IEnumerable<HttpRequestMessage> messages, CancellationToken cancellationToken)
        {
            _logger.LogDebug(nameof(CreateWorkerThreads));

            var stopWatch = new Stopwatch();
            var events = new List<ManualResetEventSlim>();
            try
            {
                var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                var token = cts.Token;

                _logger.LogDebug($"Begin build threads.");

                for (var i = 0; i < threads; i++)
                {
                    var resetEvent = new ManualResetEventSlim(false);

                    Thread thread;

                    if (count > 0)
                    {
                        thread = new Thread(async (index) => await CountWork(messages, count, resetEvent, token));
                    }
                    else if (interval > 0)
                    {
                        thread = new Thread((index) => IntervalWork(messages, duration, interval, stopWatch, resetEvent, token));
                    }
                    else
                    {
                        thread = new Thread(async (index) => await DurationWork(messages, duration, stopWatch, resetEvent, token));
                    }

                    thread.IsBackground = true;
                    thread.Start(i);
                    events.Add(resetEvent);
                }

                _logger.LogDebug("Completed build threads");

                stopWatch.Start();

                _logger.LogDebug($"Waiting: {stopWatch.Elapsed}");

                const int maxWaitHandles = 64;
                for (int i = 0; i < events.Count; i += maxWaitHandles)
                {
                    var group = events.Skip(i).Take(maxWaitHandles).Select(x => x.WaitHandle).ToArray();
                    WaitHandle.WaitAll(group);
                }

                _logger.LogDebug($"Completed: {stopWatch.Elapsed}");
            }
            finally
            {
                // Dispose those events
                for (int i = 0; i < events.Count; i++)
                {
                    events[i].Dispose();
                }
            }
            _logger.LogDebug($"Disposed: {stopWatch.Elapsed}");
        }

        public async ValueTask CountWork(IEnumerable<HttpRequestMessage> messages, int count, ManualResetEventSlim resetEvent, CancellationToken cancellationToken)
        {
            if (messages == null)
            {
                throw new ArgumentNullException(nameof(messages));
            }
            if(count <= 0)
            {
                throw new ArgumentException(nameof(count));
            }
            if(resetEvent == null)
            {
                throw new ArgumentNullException(nameof(resetEvent));
            }
            _logger.LogDebug($"{nameof(CountWork)} - Start");
            for (int i = 0; i < count; i++)
            {
                if(cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                foreach (var message in messages)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }
                    var clone = CloneMessage(message);
                    await _worker.Run(clone, _threadResult, cancellationToken);
                }
            }
            _logger.LogDebug($"{nameof(CountWork)} - End");
            resetEvent.Set();
        }

        public async ValueTask DurationWork(IEnumerable<HttpRequestMessage> messages, TimeSpan duration, Stopwatch stopWatch, ManualResetEventSlim resetEvent, CancellationToken cancellationToken)
        {
            if (messages == null)
            {
                throw new ArgumentNullException(nameof(messages));
            }
            if (duration.TotalMilliseconds <= 0)
            {
                throw new ArgumentException(nameof(duration));
            }
            if (stopWatch == null)
            {
                throw new ArgumentNullException(nameof(stopWatch));
            }
            if (resetEvent == null)
            {
                throw new ArgumentNullException(nameof(resetEvent));
            }

            _logger.LogDebug($"{nameof(DurationWork)} - Start");
            while (cancellationToken.IsCancellationRequested == false && duration.TotalMilliseconds > stopWatch.Elapsed.TotalMilliseconds)
            {
                foreach (var message in messages)
                {
                    if(cancellationToken.IsCancellationRequested ||
                       duration.TotalMilliseconds <= stopWatch.Elapsed.TotalMilliseconds)
                    {
                        break;
                    }
                    var clone = CloneMessage(message);
                    await _worker.Run(clone, _threadResult, cancellationToken);
                }
            }
            _logger.LogDebug($"{nameof(DurationWork)} - End");
            resetEvent.Set();
        }

        public void IntervalWork(IEnumerable<HttpRequestMessage> messages, TimeSpan duration, long interval, Stopwatch stopWatch, ManualResetEventSlim resetEvent, CancellationToken cancellationToken)
        {
            if (messages == null)
            {
                throw new ArgumentNullException(nameof(messages));
            }
            if (duration.TotalMilliseconds <= 0)
            {
                throw new ArgumentException(nameof(duration));
            }
            if (interval <= 0)
            {
                throw new ArgumentException(nameof(interval));
            }
            if (stopWatch == null)
            {
                throw new ArgumentNullException(nameof(stopWatch));
            }
            if (resetEvent == null)
            {
                throw new ArgumentNullException(nameof(resetEvent));
            }

            _logger.LogDebug($"{nameof(IntervalWork)} - Start");
            var heartbeat = new System.Timers.Timer(interval);
            heartbeat.Start();
            heartbeat.Elapsed += async (s, e) =>
            {
                foreach (var message in messages)
                {
                    if (cancellationToken.IsCancellationRequested || duration.TotalMilliseconds <= stopWatch.Elapsed.TotalMilliseconds)
                    {
                        break;
                    }
                    var clone = CloneMessage(message);
                    await _worker.Run(clone, _threadResult, cancellationToken);
                }
            };

            while (cancellationToken.IsCancellationRequested == false && duration.TotalMilliseconds > stopWatch.Elapsed.TotalMilliseconds)
            {
            }
            _logger.LogDebug($"{nameof(IntervalWork)} - End");
            resetEvent.Set();
        }

        public static HttpRequestMessage CloneMessage(HttpRequestMessage message) // TODO: async
        {
            HttpRequestMessage clone = new HttpRequestMessage(message.Method, message.RequestUri);

            var ms = new MemoryStream();
            if (message.Content != null)
            {
                message.Content.CopyToAsync(ms).Wait();// TODO: async
                ms.Position = 0;
                clone.Content = new StreamContent(ms);

                // Copy the content headers
                if (message.Content.Headers != null)
                    foreach (var h in message.Content.Headers)
                        clone.Content.Headers.Add(h.Key, h.Value);
            }


            clone.Version = message.Version;

            foreach (KeyValuePair<string, object> prop in message.Properties)
                clone.Properties.Add(prop);

            foreach (KeyValuePair<string, IEnumerable<string>> header in message.Headers)
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

            return clone;
        }
    }
}
