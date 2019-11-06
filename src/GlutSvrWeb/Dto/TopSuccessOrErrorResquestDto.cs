using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GlutSvrWeb.Dto
{
    public class TopSuccessOrErrorResquestDto
    {
        public string Url { get; set; }
        public int Count { get; set; }
        public decimal Frequency { get; set; }
        public int TotalItems { get; set; }
    }

}
