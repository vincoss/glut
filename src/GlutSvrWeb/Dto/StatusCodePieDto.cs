using System;


namespace GlutSvrWeb.Dto
{
    public class StatusCodePieDto
    {
        public int StatusCode { get; set; }
        public int StatusCodeItems { get; set; }
        public decimal StatusCodePercent { get; set; }
        public int TotalItems { get; set; }
    }
}
