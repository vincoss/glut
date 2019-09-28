using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;


namespace Glut.Providers
{
    public class SingleRequestMessageProvider : BaseRequestMessageProvider
    {
        public SingleRequestMessageProvider(string subpath, IFileProvider fileProvider, ILogger<BaseRequestMessageProvider> logger) : base(subpath, fileProvider, logger)
        {
        }

        protected override IEnumerable<HttpRequestMessage> ParseFile(IFileInfo info)
        {
            using (var reader = new StreamReader(info.CreateReadStream()))
            {
                var request = reader.ReadToEnd();
                yield return Run(request);
            }
        }

        public static HttpRequestMessage Run(string requestString)
        {
            if (string.IsNullOrWhiteSpace(requestString))
            {
                throw new ArgumentNullException(nameof(requestString));
            }

            byte[] requestRaw = Encoding.UTF8.GetBytes(requestString);
            ReadOnlySequence<byte> buffer = new ReadOnlySequence<byte>(requestRaw);
            HttpParser<RawHttpParser> parser = new HttpParser<RawHttpParser>();
            RawHttpParser app = new RawHttpParser();
            parser.ParseRequestLine(app, buffer, out var consumed, out var examined);
            buffer = buffer.Slice(consumed);
            parser.ParseHeaders(app, buffer, out consumed, out examined, out var b);
            buffer = buffer.Slice(consumed);
            string body = Encoding.UTF8.GetString(buffer.ToArray());
            var message = app.Get();

            if (string.IsNullOrWhiteSpace(body) == false)
            {
                message.Content = new StringContent(body, Encoding.UTF8);
            }

            return message;
        }
    }

}
