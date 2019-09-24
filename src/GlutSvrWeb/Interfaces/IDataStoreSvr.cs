using GlutSvrWeb.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GlutSvrWeb.Interfaces
{
    public interface IDataStoreSvr
    {
        LastRunDto GetLastProject();

        Task<IEnumerable<string>> GetProjectString();

        Task<IEnumerable<int>> GetProjectRuns(string projectName);

        DataTableDto<ProjectDto> GetProjects(DataTableParameter args);

        DataTableDto<ResultItemDto> GetResultItems(string projectName, int runId, DataTableParameter args);

        Task<StatusCodeHeaderDto> GetResponseDetails(string projectName, int runId);

        Task<IEnumerable<StatusCodePieDto>> GetStatusCodePieData(string projectName, int runId);

        Task<IEnumerable<KeyValueData<string>>> GetRunAttributes(string projectName, int runId);

        Task<IEnumerable<TopSuccessOrErrorResquestDto>> GetTopSuccessRequests(string projectName, int runId);

        Task<IEnumerable<TopSuccessOrErrorResquestDto>> GetTopErrorRequests(string projectName, int runId);

        Task<IEnumerable<TopMinMaxAvgResquestDto>> GetFastestSuccessRequests(string projectName, int runId);

        Task<IEnumerable<TopMinMaxAvgResquestDto>> GetSlowestSuccessRequests(string projectName, int runId);

        Task<IEnumerable<TopMinMaxAvgResquestDto>> GetAvgSuccessRequests(string projectName, int runId);

        Task<IEnumerable<LargestSizeRequestDto>> GetLargestSuccessRequests(string projectName, int runId);

        Task<LineChartDto> GetLineChartRequests(string projectName, int runId);
        Task<IEnumerable<LineChartDto>> GetLineChartRuns(string projectName);
    }
}
