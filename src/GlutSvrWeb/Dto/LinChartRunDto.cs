using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlutSvrWeb.Dto
{
    public class LinChartRunDto
    {
        public IEnumerable<string> Labels { get; set; }
        public IEnumerable<DataInfo> DataSets { get; set; }

        public class DataInfo
        {
            public string Label { get; set; }
            public string BorderColor { get; set; }
            public int?[] Data { get; set; }
        }

    }
}
