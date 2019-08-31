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
       // [ValidateAntiForgeryToken]
        public DataTableDto<ResultItemDto> Get(string id, int run)
        {
            if (string.IsNullOrWhiteSpace(id) || run <= 0)
            {
                return new DataTableDto<ResultItemDto>();
            }

            var draw = Request.Form["draw"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][data]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();

            //Paging Size (10,20,50,100)    
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            int recordsFilteredTotal = 0;

            var query = from x in _dataStoreSvr.GetResultItems(id, run) select x;
            recordsTotal = query.Count();

            // Sort
            if (string.IsNullOrWhiteSpace(sortColumn) == false && string.IsNullOrWhiteSpace(sortColumnDir) == false)
            {
                sortColumn = LinqExtensions.GetPropertyName(typeof(ResultItemDto), sortColumn);
                query = query.OrderBy(sortColumn, string.Equals("asc", sortColumnDir, StringComparison.CurrentCultureIgnoreCase));
            }

            // Search
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(m => 
                (m.Url != null && m.Url.Contains(searchValue, StringComparison.CurrentCultureIgnoreCase)) ||
                (m.StatusCode.ToString().Contains(searchValue)) ||
                (m.ResponseHeaders != null && m.ResponseHeaders.Contains(searchValue, StringComparison.CurrentCultureIgnoreCase)));
            }

            recordsFilteredTotal = query.Count();
            var model = query.Skip(skip).Take(pageSize).ToList();

            var response = new DataTableDto<ResultItemDto>
            {
                Draw = draw,
                RecordsFiltered = recordsFilteredTotal,
                RecordsTotal = recordsTotal,
                Data = model
            };

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
