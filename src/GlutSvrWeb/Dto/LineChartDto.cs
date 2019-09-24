using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GlutSvrWeb.Dto
{
    public class LineChartDto
    {
        public string SeriesString { get; set; }
        public TimeSpan TimeSeries { get; set; }
        public decimal Value { get; set; }

        public override string ToString()
        {
            return $"{SeriesString}-{TimeSeries}-{Value}";
        }
    }
}
