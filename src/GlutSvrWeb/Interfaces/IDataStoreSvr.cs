using GlutSvrWeb.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlutSvrWeb.Interfaces
{
    public interface IDataStoreSvr
    {
        Task<IEnumerable<string>> GetProjectString();

        Task<IEnumerable<int>> GetProjectRuns(string projectName);

        IQueryable<ProjectDto> GetProjects();

        Task<IEnumerable<ResultItemDto>> GetResultItems(string projectName, int runId);

        Task<IDictionary<string, decimal>> GetResponseDetails(string projectName, int runId);

        Task<IEnumerable<StatusCodePieDto>> GetStatusCodePieData(string projectName, int runId);

        Task<IEnumerable<KeyValueData<string>>> GetRunInfo(string projectName, int runId);

        Task<IEnumerable<TopSuccessOrErrorResquestDto>> GetTopSuccessRequests(string projectName, int runId);

        Task<IEnumerable<TopSuccessOrErrorResquestDto>> GetTopErrorRequests(string projectName, int runId);

        Task<IEnumerable<KeyValueData<decimal>>> GetFastestSuccessRequests(string projectName, int runId);

        Task<IEnumerable<KeyValueData<decimal>>> GetSlowestSuccessRequests(string projectName, int runId);

        Task<IEnumerable<KeyValueData<decimal>>> GetLargestSuccessRequests(string projectName, int runId);

        Task<IEnumerable<LineChartDto>> GetLineChartRequests(string projectName, int runId);
    }
}
