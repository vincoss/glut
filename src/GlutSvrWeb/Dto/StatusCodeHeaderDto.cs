using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlutSvrWeb.Dto
{
    public class StatusCodeHeaderDto
    {
        public int TotalRequests { get; set; }
        public decimal Information { get; set; }
        public decimal Successful { get; set; }
        public decimal Redirection { get; set; }
        public decimal ClientError { get; set; }
        public decimal ServerError { get; set; }
    }
}
