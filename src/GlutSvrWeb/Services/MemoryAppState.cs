using GlutSvrWeb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlutSvrWeb.Services
{
    // TODO: remove (part of Blazor)
    public class MemoryAppState : IAppState
    {
        public MemoryAppState()
        {
            SelectedProject = "Test";
            SelectedRunId = 1;
        }

        public string SelectedProject { get; set; }
        public int SelectedRunId { get; set; }
    }
}
