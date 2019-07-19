using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Glut.Data
{
    public class ResultTest
    {
        [Fact]
        public void Test()
        {
            var result = new GlutResultItem
            {
                GlutProjectName = "Test",
                RequestUri = "/",
                StatusCode = 200
            };

            Assert.Equal("Test, Url: /, StatusCode: 200", result.ToString());
        }
    }
}
