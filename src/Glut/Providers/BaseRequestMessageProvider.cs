using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Glut.Interface;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;


namespace Glut.Providers
{
    public abstract class BaseRequestMessageProvider : IRequestMessageProvider
    {
        private readonly string _subpath;
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<BaseRequestMessageProvider> _logger;

        public BaseRequestMessageProvider(string subpath, IFileProvider fileProvider, ILogger<BaseRequestMessageProvider> logger)
        {
            if (string.IsNullOrWhiteSpace(subpath))
            {
                throw new ArgumentNullException(nameof(subpath));
            }
            if (fileProvider == null)
            {
                throw new ArgumentNullException(nameof(fileProvider));
            }
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            _subpath = subpath;
            _fileProvider = fileProvider;
            _logger = logger;
        }

        public IEnumerable<HttpRequestMessage> Get()
        {
            _logger.LogInformation($"Begin load messages for subpath: {_subpath}");

            var messages = new HashSet<HttpRequestMessage>();
            var infos = _fileProvider.GetDirectoryContents(_subpath).OrderBy(o => o.Name);

            foreach (var info in infos)
            {
                if (info.IsDirectory)
                {
                    continue;
                }
                try
                {
                    messages.AddRange(ParseFile(info));
                }
                catch (Exception ex)
                {
                   _logger.LogError(ex, "Parse message.");
                }
            }

            _logger.LogInformation($"Found {messages.Count} messages.");

            return messages;
        }

        protected abstract IEnumerable<HttpRequestMessage> ParseFile(IFileInfo info);

        protected static IEnumerable<string> ReadLines(IFileInfo fileInfo)
        {
            using (var reader = new StreamReader(fileInfo.CreateReadStream()))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line.Trim();
                }
            }
        }
    }
}
