using System;


namespace GlutSvrWeb.Dto
{
    public class ResultItemDto
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public string Url { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public int StatusCode { get; set; }

        public decimal HeaderLength { get; set; }
        public decimal ResponseLength { get; set; }
        public decimal TotalLength { get; set; }

        public long RequestTicks { get; set; }
        public long ResponseTicks { get; set; }
        public long TotalTicks { get; set; }

        public string ResponseHeaders { get; set; }
        public string Exception { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public string CreatedByUser { get; set; }

    }
}
