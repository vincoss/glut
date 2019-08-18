using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;


namespace Glut
{
    public class ThreadResult
    {
        private readonly ConcurrentDictionary<string, ThreadResultItem> _cache = new ConcurrentDictionary<string, ThreadResultItem>(StringComparer.InvariantCultureIgnoreCase);

        public void Add(string requestUri, DateTime startDateTimeUtc, DateTime endDateTimeUtc, bool isSuccessStatusCode, int statusCode, long headerLength, long responseLength, long requestSentTicks, long responseTicks, string responseHeaders, Exception exception)
        {
            if(string.IsNullOrWhiteSpace(requestUri))
            {
                throw new ArgumentNullException(nameof(requestUri));
            }
            if(statusCode <= 0)
            {
                throw new ArgumentException(nameof(statusCode));
            }

            Action<ThreadResultItem> action = (info) =>
            {
                info.Add(startDateTimeUtc, endDateTimeUtc, isSuccessStatusCode, statusCode, headerLength, responseLength, requestSentTicks, responseTicks, responseHeaders, exception);
            };

            _cache.AddOrUpdate(requestUri, (key) =>
            {
                var insert = new ThreadResultItem();

                action(insert);
                return insert;
            }, (key, update) =>
            {
                action(update);
                return update;
            });
        }

        public override string ToString()
        {
            return $"Total - results: {TotalResults}, time: {new TimeSpan(TotalTicks)}, length: {TotalLength}";
        }

        public IDictionary<string, ThreadResultItem> Results
        {
            get { return _cache; }
        }

        public int TotalResults
        {
            get { return _cache.Select(s => s.Value).Sum(s => s.TotalResults); }
        }

        public long TotalRequestTicks
        {
            get { return _cache.Select(s => s.Value).Sum(s => s.TotalRequestTicks); }
        }

        public long TotalResponseTicks
        {
            get { return _cache.Select(s => s.Value).Sum(s => s.TotalResponseTicks); }
        }

        public long TotalTicks
        {
            get { return TotalRequestTicks + TotalResponseTicks; }
        }

        public long TotalHeaderLength
        {
            get { return _cache.Select(s => s.Value).Sum(s => s.TotalHeaderLength); }
        }

        public long TotalResponseLength
        {
            get { return _cache.Select(s => s.Value).Sum(s => s.TotalResponseLength); }
        }

        public long TotalLength
        {
            get { return TotalHeaderLength + TotalResponseLength; }
        }

    }
}