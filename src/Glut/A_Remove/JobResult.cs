using System;


namespace Glut
{
    public class JobResult
    {
        public string RequestUri { get; set; }
        public DateTime StartDateTimeUtc { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public int StatusCode { get; set; }
        public long HeaderLength { get; set; }
        public long ResponseLength { get; set; }
        public long RequestSentTicks { get; set; }
        public long ResponseTicks { get; set; }
        public Exception Exception { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(RequestUri))
            {
                return base.ToString();
            }
            return $"{StatusCode} {RequestUri}";
        }
    }
}
