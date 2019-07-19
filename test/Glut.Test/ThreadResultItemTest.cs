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
            item.StartDateTimes.Add(DateTime.UtcNow);
            item.IsSuccessStatusCodes.Add(true);
            item.StatusCodes.Add(200);
            item.RequestSentTicks.Add(2000);
            item.ResponseTicks.Add(3000);
            item.HeaderLengths.Add(100);
            item.ResponseLengths.Add(200);
            item.Exceptions.Add(new Exception());

            Assert.Equal(1, item.StartDateTimes.Count);
            Assert.Equal(1, item.IsSuccessStatusCodes.Count);
            Assert.Equal(1, item.StatusCodes.Count);
            Assert.Equal(1, item.HeaderLengths.Count);
            Assert.Equal(1, item.ResponseLengths.Count);
            Assert.Equal(1, item.RequestSentTicks.Count);
            Assert.Equal(1, item.ResponseTicks.Count);
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
