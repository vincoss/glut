using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Glut.Interface;


namespace Glut.Services
{
    public class HttpClientService : IWorker
    {
        private readonly HttpClient _client;
        private readonly AppConfig _appConfig;

        public HttpClientService(HttpClient client, IOptions<AppConfig> appConfig)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (appConfig == null)
            {
                throw new ArgumentNullException(nameof(appConfig));
            }

            _client = client;
            _appConfig = appConfig.Value;

            if (string.IsNullOrWhiteSpace(_appConfig.BaseAddress) == false) // TODO: possible move that where context is added into the services
            {
                client.BaseAddress = new Uri(_appConfig.BaseAddress);
            }
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json"); // TODO:
            client.DefaultRequestHeaders.Add("User-Agent", GlutConstants.FullApplicationName);
        }

        public async ValueTask Run(HttpRequestMessage request, ThreadResult result, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            var requestUri = request.RequestUri.ToString();
            var startDateTimeUtc = DateTime.UtcNow;
            var isSuccessStatusCode = false;
            var statusCode = 500;
            var headerLength = 0L;
            var responseLength = 0L;
            var requestSentTicks = 0L;
            var responseTicks = 0L;
            Exception exception = null;
            string responseHeaders = null;

            try
            {
                var sw = Stopwatch.StartNew();
                using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                {
                    requestSentTicks = sw.ElapsedTicks;
                    statusCode = (int)response.StatusCode;
                    isSuccessStatusCode = response.IsSuccessStatusCode; // 200-299
                    responseHeaders = response.ToString();
                    headerLength = responseHeaders.Length;
                    sw.Restart();

                    using (var sourceStream = await response.Content.ReadAsStreamAsync())
                    {
                        string fileToWriteTo = Path.GetTempFileName();
                        using (var destinationStream = new FileStream(fileToWriteTo, FileMode.Create, FileAccess.Write, FileShare.None, 4096, FileOptions.DeleteOnClose))
                        {
                            await sourceStream.CopyToAsync(destinationStream);
                            responseLength = destinationStream.Length;
                        }
                    }
                    sw.Stop();
                    responseTicks = sw.ElapsedTicks;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                result.Add(requestUri, startDateTimeUtc, DateTime.UtcNow, isSuccessStatusCode, statusCode, headerLength, responseLength, requestSentTicks, responseTicks, responseHeaders, exception);
            }
        }
    }
}