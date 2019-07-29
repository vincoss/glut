using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Glut.Data
{
    public class GlutRunAttribute
    {
        public string GlutProjectName { get; set; }
        public int GlutProjectRunId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
        public string CreatedByUserName { get; set; }
    }
}
