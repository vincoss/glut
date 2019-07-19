using System;
using Xunit;


namespace Glut
{
    public class ThreadResultTest
    {
        [Fact]
        public void Test()
        {
            var result = new ThreadResult();
            result.Add("/",  DateTime.UtcNow, DateTime.UtcNow.AddSeconds(1), true, 200, 100, 200, 2000, 3000, "Some headers", new Exception());

            Assert.Equal(1, result.Results.Count);
            Assert.Equal(1, result.TotalResults);
            Assert.Equal(2000, result.TotalRequestTicks);
            Assert.Equal(3000, result.TotalResponseTicks);
            Assert.Equal(5000, result.TotalTicks);
            Assert.Equal(100, result.TotalHeaderLength);
            Assert.Equal(200, result.TotalResponseLength);
            Assert.Equal(300, result.TotalLength);

            Assert.Equal("Total - results: 1, time: 00:00:00.0005000, length: 300", result.ToString());

        }
    }
}
