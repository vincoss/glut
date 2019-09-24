using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GlutSvrWeb.Dto
{
    public class LineChartDto
    {
        public IEnumerable<TimeSpan> Labels { get; set; }

        public IEnumerable<int> TotalRequests { get; set; }
        public IEnumerable<decimal> Information { get; set; }
        public IEnumerable<decimal> Successful { get; set; }
        public IEnumerable<decimal> Redirection { get; set; }
        public IEnumerable<decimal> ClientError { get; set; }
        public IEnumerable<decimal> ServerError { get; set; }
    }
}
