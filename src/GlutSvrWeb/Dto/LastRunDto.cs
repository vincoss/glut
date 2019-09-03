using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlutSvrWeb.Dto
{
    public class LastRunDto
    {
        public string ProjectName { get; set; }
        public int RunId { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(ProjectName))
            {
                return base.ToString();
            }
            return $"{ProjectName}, {RunId}";
        }
    }
}
