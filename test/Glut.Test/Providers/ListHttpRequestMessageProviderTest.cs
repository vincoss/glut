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
    public class ListHttpRequestMessageProviderTest
    {
        [Fact]
        public void RequestListParseTest()
        {
            var rootPath = Path.Combine(AppContext.BaseDirectory, "TestData");
            var subpath = "list";
            var fileProvider = new PhysicalFileProvider(rootPath);
            var lLogger = new LoggerFactory().CreateLogger<ListHttpRequestMessageProvider>();

            var provider = new ListHttpRequestMessageProvider(subpath, fileProvider, lLogger);

            var messages = provider.Get();

            Assert.Equal(9, messages.Count());
            Assert.True(messages.All(x => x.Method == HttpMethod.Get));

            Assert.Equal("/", messages.ElementAt(0).RequestUri.ToString());
            Assert.Equal("/home", messages.ElementAt(1).RequestUri.ToString());
            Assert.Equal("/home/index", messages.ElementAt(2).RequestUri.ToString());
            Assert.Equal("/home/index/5", messages.ElementAt(3).RequestUri.ToString());
            Assert.Equal("/0002", messages.ElementAt(4).RequestUri.ToString());
        }
    }
}
