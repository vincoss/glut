using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Http = System.Net.Http;


namespace Glut.Providers
{
    public class RawHttpParser : IHttpRequestLineHandler, IHttpHeadersHandler
    {
        private Http.HttpRequestMessage _message;
        public readonly IDictionary<string, string> ContentHeaders = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private static readonly string[] ContentReaders = new[]
        {
            "Content-Length",
            "Content-Type"
        };


        public void OnHeader(Span<byte> name, Span<byte> value)
        {
            var strName = Encoding.UTF8.GetString(name);
            var strValue = Encoding.UTF8.GetString(value);

            // NOTE: Content headers

            if(ContentReaders.Any(x => string.Equals(x, strName, StringComparison.OrdinalIgnoreCase)))
            {
                if(ContentHeaders.ContainsKey(strName) == false)
                {
                    ContentHeaders.Add(strName, null);
                }
                ContentHeaders[strName] = strValue;
                return;
            }

            _message.Headers.Add(strName, strValue);
           
        }

        public void OnStartLine(HttpMethod method, HttpVersion version, Span<byte> target, Span<byte> path, Span<byte> query, Span<byte> customMethod, bool pathEncoded)
        {
            _message = new Http.HttpRequestMessage(new Http.HttpMethod(method.ToString()), Encoding.UTF8.GetString(target));
            _message.Version = GetHttpVersion(version);
        }

        public Http.HttpRequestMessage Get()
        {
            return _message;
        }

        // TODO: refactor
        public static Version GetHttpVersion(HttpVersion value)
        {
            if (value == HttpVersion.Http10)
            {
                return new Version(1, 0);
            }
            else if (value == HttpVersion.Http11)
            {
                return new Version(1, 1);
            }
            else if (value == HttpVersion.Http2)
            {
                return new Version(2, 0);
            }
            else
            {
                return new Version(0, 0);
            }
        }
    }
}
