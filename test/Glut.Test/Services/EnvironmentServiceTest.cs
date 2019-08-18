using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Glut.Services
{
    public class EnvironmentServiceTest
    {
        [Fact]
        public void Test()
        {
            var service = new EnvironmentService();

            Assert.NotEmpty(service.SystemDateTimeUtc.ToString());
            Assert.NotNull(service.UserName);
        }
    }
}
