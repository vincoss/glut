using System;
using System.ComponentModel.DataAnnotations;

namespace Glut.Data
{
    public class GlutRunAttribute
    {
        [Key]
        public int GlutRunAttributeId { get; set; }
        public string GlutProjectName { get; set; }
        public int GlutProjectRunId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
        public string CreatedByUserName { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(AttributeName))
            {
                return base.ToString();
            }
            return $"Name: {AttributeName}, Value: {AttributeValue}";
        }
    }
}
