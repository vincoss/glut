using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlutSvr.Interfaces
{
    public interface IAppState
    {
        string SelectedProject { get; set; }
        int SelectedRunId { get; set; }

    }
}
