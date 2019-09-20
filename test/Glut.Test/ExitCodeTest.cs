using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Glut
{
    public class ExitCodeTest
    {
        [Fact]
        public void Test()
        {
            Assert.Equal(0, (int)ExitCode.Success);
            Assert.Equal(-1, (int)ExitCode.UnknownError);
        }
    }
}
