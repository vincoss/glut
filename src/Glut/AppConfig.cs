using System;


namespace Glut
{
    public class AppConfig
    {
        public string BaseAddress { get; set; } = "http://localhost/";

        public string ContentRootPath { get; set; }
        public string ListSubpath { get; set; } = "List";
        public string SingleSubpath { get; set; } = "Single";

        public int Threads { get; set; } = 5;
        public int Count { get; set; }
        public long DurationMilliseconds { get; set; } = 1000; // Milliseconds
        public long IntervalMilliseconds { get; set; } // Milliseconds (between requests) used with duration property.

        public string ProjectName { get; set; } = GlutConstants.ApplicationName;
        public int ProjectRunId { get; set; } = 0;

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(BaseAddress))
            {
                return base.ToString();
            }
            return BaseAddress;
        }
    }
}
