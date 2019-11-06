using Microsoft.Extensions.Options;
using RichardSzalay.MockHttp;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace Glut.Services
{
    public class HttpClientServiceTest
    {
        [Fact]
        public async Task Test200()
        {
            var mockHandler = new MockHttpMessageHandler();

            mockHandler
                .When("/")
                .Respond("application/json", "{'status' : 'OK'}");

            var client = mockHandler.ToHttpClient();
            var config = new AppConfig()
            {
                BaseAddress = "http://localhost/"
            };
            var options = Options.Create(config);
            var service = new HttpClientService(client, options);
            var message = new HttpRequestMessage(HttpMethod.Get, "/");
            var cts = new CancellationTokenSource(100000);
            var result = new ThreadResult();

            await service.Run(message, result, cts.Token);

            var item = result.Results.Single();

            Assert.Equal("/", item.Key);
            Assert.True(item.Value.StartDateTimes.Count == 1);
            Assert.True(item.Value.EndDateTimes.Count == 1);
            Assert.True(item.Value.IsSuccessStatusCodes[0]);
            Assert.Equal(200, item.Value.StatusCodes[0]);
            Assert.True(item.Value.HeaderLengths[0] > 0);
            Assert.True(item.Value.ResponseLengths[0] > 0);
            Assert.True(item.Value.RequestSentTicks[0] > 0);
            Assert.True(item.Value.ResponseTicks[0] > 0);
            Assert.True(item.Value.ResponseTicks.Count == 1);
            Assert.Equal(0, item.Value.Exceptions.Count);
        }
    }
}
