using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Glut.Data
{
    public class GlutResultItem
    {
        [Key]
        public int GlutResultId { get; set; }
        public string GlutProjectName { get; set; }
        public int GlutProjectRunId { get; set; }

        public DateTime StartDateTimeUtc { get; set; }
        public DateTime EndDateTimeUtc { get; set; }

        public string Url { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public byte StatusCodeGroup { get; set; }
        public int StatusCode { get; set; }

        public long HeaderLength { get; set; }
        public long ResponseLength { get; set; }
        public long TotalLegth { get; set; }

        public long RequestSentTicks { get; set; }
        public long ResponseTicks { get; set; }
        public long TotalTicks { get; set; }

        public string ResponseHeaders { get; set; }
        public string Exception { get; set; }

        public DateTime CreatedDateTimeUtc { get; set; }
        public string CreatedByUserName { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(GlutProjectName))
            {
                return base.ToString();
            }
            return string.Format($"{GlutProjectName}, Url: {Url}, StatusCode: {StatusCode}");
        }
    }
}
