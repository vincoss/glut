using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Glut
{
    public class JobResultTest
    {
        [Fact]
        public void Test()
        {
            var result = new JobResult
            {
                StatusCode = 200,
                RequestUri = "/"
            };

            Assert.Equal("200 /", result.ToString());
        }
    }
}
