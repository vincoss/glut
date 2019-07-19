using GlutSvr.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlutSvr.Interfaces
{
    public interface IDataStoreSvr
    {
        Task<IEnumerable<string>> GetProjectString();

        Task<IEnumerable<ProjectDto>> GetProjects();

        Task<IDictionary<string, decimal>> GetResponseDetails(string projectName, int runId);

        Task<IEnumerable<PieDto>> GetStatusCodePieData(string projectName, int runId);

        Task<IEnumerable<ResultItemDto>> GetResultItems(string projectName, int runId);
    }
}
