using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlutSvrWeb.Dto;
using GlutSvrWeb.Interfaces;
using GlutSvrWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Default_WebApplication_API_V3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IDataStoreSvr _dataStoreSvr;

        public ProjectController(ILogger<ProjectController> logger, IDataStoreSvr dataStoreSvr)
        {
            _logger = logger;
            _dataStoreSvr = dataStoreSvr;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public DataTableDto<ProjectDto> Get()
        {
            var args = Request.Form.GetDataTableArgs();

            var response = _dataStoreSvr.GetProjects(args);

            return response;
        }

        [HttpPost("projectNames")]
        public async Task<IEnumerable<string>> GetProjects()
        {
            return await _dataStoreSvr.GetProjectString();
        }

        [HttpPost("runIds/{id}")]
        public async Task<IEnumerable<int>> GetRuns(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _dataStoreSvr.GetProjectRuns(id);
        }
    }
}
