using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Glut
{
    public class GlutConstantsTest
    {
        [Fact]
        public void Test()
        {
            Assert.Equal("Glut", GlutConstants.ApplicationName);
            Assert.Equal("Glut - 1.0.0.0", GlutConstants.FullApplicationName);
            Assert.Equal("1.0.0.0", GlutConstants.Version.ToString());
        }
    }
}
