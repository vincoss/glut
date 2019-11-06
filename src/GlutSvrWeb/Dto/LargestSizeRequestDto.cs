using System;


namespace GlutSvrWeb.Dto
{
    public class LargestSizeRequestDto
    {
        public string Url { get; set; }
        public decimal Length { get; set; }
        public decimal Percent { get; set; }
        public decimal TotalLength { get; set; }

    }
}
