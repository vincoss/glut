using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;


namespace Glut.Providers
{
    public class SingleRequestMessageProvider : BaseRequestMessageProvider
    {
        public SingleRequestMessageProvider(string subpath, IFileProvider fileProvider, ILogger<BaseRequestMessageProvider> logger) : base(subpath, fileProvider, logger)
        {
        }

        /*
         TODO: complete this
          first row
          headers
          empty line
          content
          add tests
          */
        private static HttpRequestMessage ParseFirstLine(string line)
        {
            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if(parts.Length <= 2)
            {
                throw new InvalidOperationException("Request did not contain valid HTTP headers.");
            }
            var method = parts[0];
            var requestUrl = parts[1];
            var message = new HttpRequestMessage(new HttpMethod(method), requestUrl);

            if (parts.Length == 2) // Http version
            {
                var versionString = parts[2];
                message.Version = HttpUtilities.GetHttpVersion(versionString);
            }

            return message;
        }

        private static void ParseHeaders(string line, HttpRequestMessage message)
        {
            var parts = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            
            if(parts.Length <= 0)
            {
                return;
            }

            if(parts.Length > 2)
            {
                throw new InvalidOperationException("Request did not contain valid HTTP headers.");
            }

            var key = parts[0].Trim();
            var value = string.Empty;

            if(parts.Length > 1)
            {
                value = parts[1];
            }

            message.Headers.Add(key, value);
        }

        protected override IEnumerable<HttpRequestMessage> ParseFile(IFileInfo info)
        {
            var lines = ReadLines(info);
            var count = lines.Count();

            HttpRequestMessage message = null;

            for (int i = 0; i < count; i++)
            {
                var line = lines.ElementAt(i);

                if (i == 0)
                {
                    message = ParseFirstLine(line);
                    break;
                }
            }
            yield return message;
        }
    }

}
