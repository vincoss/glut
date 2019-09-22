using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlutSvrWeb.Dto;
using GlutSvrWeb.Interfaces;
using GlutSvrWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Default_WebApplication_API_V3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResultController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IDataStoreSvr _dataStoreSvr;

        public ResultController(ILogger<ProjectController> logger, IDataStoreSvr dataStoreSvr)
        {
            _logger = logger;
            _dataStoreSvr = dataStoreSvr;
        }

        [HttpPost]
        [Route("{id}/{run?}")]
        [ValidateAntiForgeryToken]
        public DataTableDto<ResultItemDto> Get(string id, int? run)
        {
            if (string.IsNullOrWhiteSpace(id) || run == null || run <= 0)
            {
                return new DataTableDto<ResultItemDto>();
            }

            var args = Request.Form.GetDataTableArgs();

            var response = _dataStoreSvr.GetResultItems(id, run.Value , args);

            return response;
        }
    }
}
