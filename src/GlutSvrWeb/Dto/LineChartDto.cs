using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GlutSvrWeb.Dto
{
    public class LineChartDto
    {
        public IEnumerable<TimeSpan> Labels { get; set; }

        public int[] TotalRequests { get; set; }
        public int[] Information { get; set; }
        public int[] Successful { get; set; }
        public int[] Redirection { get; set; }
        public int[] ClientError { get; set; }
        public int[] ServerError { get; set; }
    }
}
