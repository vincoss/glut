using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Xunit;
using System.Linq;


namespace Glut
{
    public class ExtensionsTest
    {
        [Fact]
        public void PrintConfigurationTest()
        {
            var buildder = new ConfigurationBuilder();
            buildder.AddInMemoryCollection();

            var config = buildder.Build();
            config["SomeKey"] = "Hello";

            Extensions.PrintConfiguration(config, (str) =>
            {
                Assert.Equal("SomeKey: Hello", str);
            });
        }

        [Fact]
        public void AddRangeTest()
        {
            var set = new HashSet<int>();

            set.AddRange(new[] { 1 });

            Assert.True(set.Count == 1);
        }
    }
}
