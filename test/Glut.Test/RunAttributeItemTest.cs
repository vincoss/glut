using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Glut
{
    public class RunAttributeItemTest
    {
        [Fact]
        public void Test()
        {
            var attribute = new RunAttributeItem
            {
                AttributeName = "Test",
                AttributeValue = "Value"
            };

            Assert.Equal("Name: Test, Value: Value", attribute.ToString());
        }
    }
}
