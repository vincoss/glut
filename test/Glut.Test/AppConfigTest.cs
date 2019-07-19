using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Glut
{
    public class AppConfigTest
    {
        [Fact]
        public void DefeaultsTest()
        {
            var config = new AppConfig();

            Assert.Equal("http://localhost/", config.BaseAddress);
            Assert.Null(config.ContentRootPath);
            Assert.Equal("List", config.ListSubpath);
            Assert.Equal("Single", config.SingleSubpath);
            Assert.Equal(5, config.Threads);
            Assert.Equal(0, config.Count);
            Assert.Equal(1000, config.DurationMilliseconds);
            Assert.Equal(0, config.IntervalMilliseconds);
        }
    }
}
