using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlutSvrWeb.Dto
{
    public class TopMinMaxAvgResquestDto
    {
        public string Url { get; set; }
        public long Min { get; set; }
        public long Max { get; set; }
        public double Avg { get; set; }
    }
}
