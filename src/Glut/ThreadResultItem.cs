using System;
using System.Collections.Generic;
using System.Linq;


namespace Glut
{
    public class ThreadResultItem
    {
        private static object _lock = new object();

        public ThreadResultItem()
        {
            StartDateTimes = new List<DateTime>();
            EndDateTimes = new List<DateTime>();
            IsSuccessStatusCodes = new List<bool>();
            StatusCodes = new List<int>();
            HeaderLengths = new List<long>();
            ResponseLengths = new List<long>();
            RequestSentTicks = new List<long>();
            ResponseTicks = new List<long>();
            Exceptions = new List<Exception>();
            ResponseHeaders = new List<string>();
        }

        public void Add(DateTime startDateTimeUtc, DateTime endDateTimeUtc, bool isSuccessStatusCode, int statusCode, long headerLength, long responseLength, long requestSentTicks, long responseTicks, string responseHeaders, Exception exception)
        {
            lock (_lock)
            {
                this.StartDateTimes.Add(startDateTimeUtc);
                this.EndDateTimes.Add(endDateTimeUtc);
                this.IsSuccessStatusCodes.Add(isSuccessStatusCode);
                this.StatusCodes.Add(statusCode);
                this.HeaderLengths.Add(headerLength);
                this.ResponseLengths.Add(responseLength);
                this.RequestSentTicks.Add(requestSentTicks);
                this.ResponseTicks.Add(responseTicks);
                this.ResponseHeaders.Add(responseHeaders);
                this.Exceptions.Add(exception);
            }
        }

        public override string ToString()
        {
            return $"Total - results: {TotalResults}, time: {new TimeSpan(TotalTicks)}, length: {TotalLength}";
        }

        public int TotalResults
        {
            get { return IsSuccessStatusCodes.Count; }
        }

        public long TotalRequestTicks
        {
            get { return RequestSentTicks.Sum(s => s); }
        }

        public long TotalResponseTicks
        {
            get { return ResponseTicks.Sum(s => s); }
        }

        public long TotalTicks
        {
            get { return TotalRequestTicks + TotalResponseTicks; }
        }

        public long TotalHeaderLength
        {
            get { return HeaderLengths.Sum(s => s); }
        }

        public long TotalResponseLength
        {
            get { return ResponseLengths.Sum(s => s); }
        }

        public long TotalLength
        {
            get { return TotalHeaderLength + TotalResponseLength; }
        }

        public IList<DateTime> StartDateTimes { get; private set; }
        public IList<DateTime> EndDateTimes { get; private set; }
        public IList<bool> IsSuccessStatusCodes { get; private set; }
        public IList<int> StatusCodes { get; private set; }
        public IList<long> HeaderLengths { get; private set; }
        public IList<long> ResponseLengths { get; private set; }
        public IList<long> RequestSentTicks { get; private set; }
        public IList<long> ResponseTicks { get; private set; }
        public IList<Exception> Exceptions { get; private set; }
        public IList<string> ResponseHeaders { get; private set; }

    }
}
