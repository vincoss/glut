using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpParseSample
{
    public class Program
    {
        static void Main(string[] args)
        {
            var a = new DateTime(2019, 9, 30, 23, 1, 1);
            var b = new DateTime(2015, 9, 29, 23, 1, 1);

            var ta = TimeSpan.FromTicks(a.Ticks / TimeSpan.FromSeconds(1).Ticks);
            var tb = TimeSpan.FromTicks(b.Ticks / TimeSpan.FromSeconds(1).Ticks);

            Console.WriteLine(ta);
            Console.WriteLine(tb);

        }

        public void Sample()
        {
            var request = @"GET https://raw.githubusercontent.com/AlonAm/NLoad/master/.gitattributes HTTP/1.1
Host: raw.githubusercontent.com
Connection: keep-alive
Cache-Control: max-age=0
Upgrade-Insecure-Requests: 1
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36
Sec-Fetch-Mode: navigate
Sec-Fetch-User: ?1
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3
Sec-Fetch-Site: none
Referer: https://github.com/AlonAm/NLoad/blob/master/.gitattributes
Accept-Encoding: gzip, deflate, br
Accept-Language: en-US,en;q=0.9,fr;q=0.8


";

            var message = RawHttpParserReader.Run(request);

            Run(message).Wait();

            Console.WriteLine("Done...");
            Console.ReadKey();
        }

        public async static Task Run(HttpRequestMessage message)
        {
            var client = new HttpClient();

            using (var response = await client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                var filePath = @"C:\Temp\DataImport\Request.txt";
                var str = await response.Content.ReadAsStringAsync();
                File.WriteAllText(filePath, str);
            }
        }

        public  async Task SampleTwo()
        {
            var client = new HttpClient() { BaseAddress = new Uri("https://auth0.com/blog/exploring-dotnet-core-3-whats-new/") };

            using (var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, "/") { Version = new Version(2, 0) })
            using (var response = await client.SendAsync(request))
            {
                Console.WriteLine(response.Content);
            }
        }
    }

}
