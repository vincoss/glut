using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using Http = System.Net.Http;

namespace HttpParseSample
{
    public class RawHttpParserReader
    {
        public static Http.HttpRequestMessage Run(string requestString)
        {
            if (string.IsNullOrWhiteSpace(requestString))
            {
                throw new ArgumentNullException(nameof(requestString));
            }

            var message = new Http.HttpRequestMessage();
            byte[] requestRaw = Encoding.UTF8.GetBytes(requestString);
            ReadOnlySequence<byte> buffer = new ReadOnlySequence<byte>(requestRaw);
            HttpParser<RawHttpParser> parser = new HttpParser<RawHttpParser>();
            RawHttpParser app = new RawHttpParser(message);
            Console.WriteLine("Start line:");
            parser.ParseRequestLine(app, buffer, out var consumed, out var examined);
            buffer = buffer.Slice(consumed);
            Console.WriteLine("Headers:");
            parser.ParseHeaders(app, buffer, out consumed, out examined, out var b);
            buffer = buffer.Slice(consumed);
            string body = Encoding.UTF8.GetString(buffer.ToArray());

            if (string.IsNullOrWhiteSpace(body) == false)
            {
                message.Content = new Http.StringContent(body, Encoding.UTF8);
                Console.WriteLine(body);
            }

            return message;
        }
    }


    public class RawHttpParser : IHttpRequestLineHandler, IHttpHeadersHandler
    {
        private readonly Http.HttpRequestMessage _message;

        public RawHttpParser(Http.HttpRequestMessage message)
        {
            _message = message;
        }

        public void OnHeader(Span<byte> name, Span<byte> value)
        {
            var strName = Encoding.UTF8.GetString(name);
            var strValue = Encoding.UTF8.GetString(value);
            _message.Headers.Add(strName, strValue);
        }

        public void OnStartLine(HttpMethod method, HttpVersion version, Span<byte> target, Span<byte> path, Span<byte> query, Span<byte> customMethod, bool pathEncoded)
        {
            _message.Method = new Http.HttpMethod(method.ToString());
            _message.Version = GetHttpVersion(version);
            _message.RequestUri = new Uri(Encoding.UTF8.GetString(target));
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


    public class LocalHttpParser : IHttpRequestLineHandler, IHttpHeadersHandler
    {
        private readonly Http.HttpRequestMessage _message = new Http.HttpRequestMessage();

        public Http.HttpRequestMessage Run(string requestString)
        {
            if (string.IsNullOrWhiteSpace(requestString))
            {
                throw new ArgumentNullException(nameof(requestString));
            }

            byte[] requestRaw = Encoding.UTF8.GetBytes(requestString);
            ReadOnlySequence<byte> buffer = new ReadOnlySequence<byte>(requestRaw);
            HttpParser<LocalHttpParser> parser = new HttpParser<LocalHttpParser>();
            LocalHttpParser app = new LocalHttpParser();
            Console.WriteLine("Start line:");
            parser.ParseRequestLine(app, buffer, out var consumed, out var examined);
            buffer = buffer.Slice(consumed);
            Console.WriteLine("Headers:");
            parser.ParseHeaders(app, buffer, out consumed, out examined, out var b);
            buffer = buffer.Slice(consumed);
            string body = Encoding.UTF8.GetString(buffer.ToArray());

            //using (var stringContent = new Http.StringContent(body, Encoding.UTF8))
            //{
            //    _message.Content = stringContent;
            //}
            Console.WriteLine(body);

            return _message;
        }

        public void OnHeader(Span<byte> name, Span<byte> value)
        {
            var strName = Encoding.UTF8.GetString(name);
            var strValue = Encoding.UTF8.GetString(value);
            _message.Headers.Add(strName, strValue);
        }

        public void OnStartLine(HttpMethod method, HttpVersion version, Span<byte> target, Span<byte> path, Span<byte> query, Span<byte> customMethod, bool pathEncoded)
        {
            _message.Method = new Http.HttpMethod(method.ToString());
            _message.Version = GetHttpVersion(version);
            _message.RequestUri = new Uri(Encoding.UTF8.GetString(target));
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

        /*
          public void Parse()
        {
            string requestString = @"POST /resource/?query_id=0 HTTP/1.1
Host: example.com
User-Agent: custom
Accept: *//*
Connection: close
Content-Length: 20
Content-Type: application/json

{""key1"":1, ""key2"":2}";

            byte[] requestRaw = Encoding.UTF8.GetBytes(requestString);
    ReadOnlySequence<byte> buffer = new ReadOnlySequence<byte>(requestRaw);
    HttpParser<LocalHttpParser> parser = new HttpParser<LocalHttpParser>();
    LocalHttpParser app = new LocalHttpParser();
    Console.WriteLine("Start line:");
            parser.ParseRequestLine(app, buffer, out var consumed, out var examined);
            buffer = buffer.Slice(consumed);
            Console.WriteLine("Headers:");
            parser.ParseHeaders(app, buffer, out consumed, out examined, out var b);
            buffer = buffer.Slice(consumed);
            string body = Encoding.UTF8.GetString(buffer.ToArray());
    Dictionary<string, int> bodyObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, int>>(body);
    Console.WriteLine("Body:");
            foreach (var item in bodyObject)
            {
                Console.WriteLine($"key: {item.Key}, value: {item.Value}");
            }
Console.ReadKey();
        }

        public void OnHeader(Span<byte> name, Span<byte> value)
{
    _message.Headers.Add(Encoding.UTF8.GetString(name), Encoding.UTF8.GetString(value));

    Console.WriteLine($"{Encoding.UTF8.GetString(name)}: {Encoding.UTF8.GetString(value)}");
}

public void OnStartLine(HttpMethod method, HttpVersion version, Span<byte> target, Span<byte> path, Span<byte> query, Span<byte> customMethod, bool pathEncoded)
{
    _message.Method = new Http.HttpMethod(method.ToString());
    _message.Version = GetHttpVersion(version);
    _message.RequestUri = new Uri(Encoding.UTF8.GetString(target));

    Console.WriteLine($"method: {method}");
    Console.WriteLine($"version: {version}");
    Console.WriteLine($"target: {Encoding.UTF8.GetString(target)}");
    Console.WriteLine($"path: {Encoding.UTF8.GetString(path)}");
    Console.WriteLine($"query: {Encoding.UTF8.GetString(query)}");
    Console.WriteLine($"customMethod: {Encoding.UTF8.GetString(customMethod)}");
    Console.WriteLine($"pathEncoded: {pathEncoded}");
}
        */
    }
}
