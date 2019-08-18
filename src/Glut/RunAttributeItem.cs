using System;
using System.Collections.Generic;
using System.Text;

namespace Glut
{
    public  class RunAttributeItem
    {
        public string GlutProjectName { get; set; }
        public int GlutProjectRunId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }

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
