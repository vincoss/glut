using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;


namespace Glut.Providers
{
    public class ListHttpRequestMessageProvider : BaseRequestMessageProvider
    {
        public ListHttpRequestMessageProvider(string subpath, IFileProvider fileProvider, ILogger<BaseRequestMessageProvider> logger) : base(subpath, fileProvider, logger)
        {
        }

        protected override IEnumerable<HttpRequestMessage> ParseFile(IFileInfo info)
        {
            foreach (var line in ReadLines(info))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                yield return new HttpRequestMessage(HttpMethod.Get, line.Trim());
            }
        }
    }
}
