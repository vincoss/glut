using Glut.Interface;
using Glut.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace Glut.Services
{
    public class LoadBackgroundService : BackgroundService
    {
        private IEnumerable<HttpRequestMessage> _messages;
        private readonly AppConfig _appConfig;
        private readonly CompositeRequestMessageProvider _compositeMessageProvider;
        private readonly ILogger<LoadBackgroundService> _logger;
        private readonly IApplicationLifetime _applicationLifetime;
        private readonly Runner _runner;
        private readonly ThreadResult _threadResult;
        private readonly IResultStore _resultStore;

        public LoadBackgroundService(ThreadResult result, Runner runner, IApplicationLifetime applicationLifetime, IOptions<AppConfig> appConfig, ILogger<LoadBackgroundService> logger, CompositeRequestMessageProvider compositeMessageProvider, IResultStore resultStore)
        {
            _runner = runner;
            _applicationLifetime = applicationLifetime;
            _appConfig = appConfig.Value;
            _logger = logger;
            _compositeMessageProvider = compositeMessageProvider;
            _threadResult = result;
            _resultStore = resultStore;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Begin { nameof(StartAsync)}");

            _messages = _compositeMessageProvider.Get().ToArray();

            _logger.LogDebug($"End {nameof(StartAsync)}, messages {_messages.Count()}.");

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Begin {nameof(ExecuteAsync)}");

            _runner.CreateWorkerThreads(_appConfig.Threads, TimeSpan.FromMilliseconds(_appConfig.DurationMilliseconds), _appConfig.Count, _appConfig.IntervalMilliseconds, _messages, cancellationToken);

            DisplayResultInformation();

            if (_appConfig.PersistResults)
            {
                _resultStore.Add(_appConfig.ProjectName, _appConfig.ProjectRunId, GetConfigToDictionary(_appConfig), _threadResult);
            }
            
            Environment.ExitCode = (int)ExitCode.Success;

            _applicationLifetime.StopApplication();

            _logger.LogDebug($"End {nameof(ExecuteAsync)}");

            return Task.CompletedTask;
        }

        public IDictionary<string, string> GetConfigToDictionary(AppConfig config)
        {
            var dict = new Dictionary<string, string>();
            dict.Add(nameof(config.BaseAddress), config.BaseAddress);
            dict.Add(nameof(config.ContentRootPath), config.ContentRootPath);
            dict.Add(nameof(config.ListSubpath), config.ListSubpath);
            dict.Add(nameof(config.SingleSubpath), config.SingleSubpath);
            dict.Add(nameof(config.Threads), config.Threads.ToString());
            dict.Add(nameof(config.DurationMilliseconds), config.DurationMilliseconds.ToString());
            dict.Add(nameof(config.IntervalMilliseconds), config.IntervalMilliseconds.ToString());
            dict.Add(nameof(config.ProjectName), config.ProjectName);
            dict.Add(nameof(config.ProjectRunId), config.ProjectRunId.ToString());
            dict.Add(nameof(config.PersistResults), config.PersistResults.ToString());

            return dict;
        }

        private void DisplayResultInformation()
        {
            var str = _threadResult.ToString();
            _logger.LogDebug(str);
            Console.WriteLine(str);
        }
    }
}
