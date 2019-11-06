using System;
using Xunit;


namespace Glut
{
    public class ThreadResultItemTest
    {
        [Fact]
        public void Test()
        {
            var item = new ThreadResultItem();

            var start = DateTime.UtcNow;
            item.Add(start, start.AddSeconds(1), true, 200, 100, 200, 2000, 3000, "Headers", new Exception("Error"));

            Assert.Equal(1, item.StartDateTimes.Count);
            Assert.Equal(1, item.StartDateTimes.Count);
            Assert.Equal(1, item.IsSuccessStatusCodes.Count);
            Assert.Equal(1, item.StatusCodes.Count);
            Assert.Equal(1, item.HeaderLengths.Count);
            Assert.Equal(1, item.ResponseLengths.Count);
            Assert.Equal(1, item.RequestSentTicks.Count);
            Assert.Equal(1, item.ResponseTicks.Count);
            Assert.Equal(1, item.ResponseHeaders.Count);
            Assert.Equal(1, item.Exceptions.Count);

            Assert.Equal(1, item.TotalResults);
            Assert.Equal(2000, item.TotalRequestTicks);
            Assert.Equal(3000, item.TotalResponseTicks);
            Assert.Equal(5000, item.TotalTicks);
            Assert.Equal(100, item.TotalHeaderLength);
            Assert.Equal(200, item.TotalResponseLength);
            Assert.Equal(300, item.TotalLength);

            Assert.Equal("Total - results: 1, time: 00:00:00.0005000, length: 300", item.ToString());
        }
    }
}
