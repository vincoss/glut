using System;


namespace GlutSvr.Dto
{
    public class ProjectDto
    {
        public string ProjectName { get; set; }
        public int Runs { get; set; }
        public DateTime? LastChangeDateTime { get; set; }
    }
}
