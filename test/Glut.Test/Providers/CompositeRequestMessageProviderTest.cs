using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using Xunit;


namespace Glut.Providers
{
    public class CompositeRequestMessageProviderTest
    {
        [Fact]
        public void Test()
        {
            var rootPath = Path.Combine(AppContext.BaseDirectory, "TestData");
            var listSubpath = "list";
            var singleSubpath = "single";
            var fileProvider = new PhysicalFileProvider(rootPath);
            var lLogger = new LoggerFactory().CreateLogger<ListHttpRequestMessageProvider>();
            var sLogger = new LoggerFactory().CreateLogger<SingleRequestMessageProvider>();

            var listProvider = new ListHttpRequestMessageProvider(listSubpath, fileProvider, lLogger);
            var singleProvider = new SingleRequestMessageProvider(singleSubpath, fileProvider, sLogger);
            var compositeProvider = new CompositeRequestMessageProvider(listProvider, singleProvider);

            var results = compositeProvider.Get();

            Assert.Equal(10, results.Count());
        }
    }
}
