using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Glut.Providers
{
    public class SingleRequestMessageProviderTest
    {
        [Fact]
        public void Test()
        {
            var rootPath = Path.Combine(AppContext.BaseDirectory, "TestData", "Sample");
            var subpath = "single";
            var fileProvider = new PhysicalFileProvider(rootPath);
            var sLogger = new LoggerFactory().CreateLogger<SingleRequestMessageProvider>();

            var provider = new SingleRequestMessageProvider(subpath, fileProvider, sLogger);

            var messages = provider.Get();

            Assert.True(messages.Count() == 2, "The messages count is not right");

            var message = messages.ElementAt(0);

            Assert.Equal(message.Method, HttpMethod.Get);
            Assert.Equal("/", message.RequestUri.ToString());

            message = messages.ElementAt(1);

            Assert.Equal(message.Method, HttpMethod.Get);
            Assert.Equal("/home", message.RequestUri.ToString());
        }
    }
}
