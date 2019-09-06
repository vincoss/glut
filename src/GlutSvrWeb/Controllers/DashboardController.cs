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
    public class DashboardController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IDataStoreSvr _dataStoreSvr;

        public DashboardController(ILogger<ProjectController> logger, IDataStoreSvr dataStoreSvr)
        {
            _logger = logger;
            _dataStoreSvr = dataStoreSvr;
        }

        /// <summary>
        /// dashboard/statusCodePie
        /// </summary>
        [HttpPost("statusCodePie/{id}/{run?}")]
        [ValidateAntiForgeryToken]
        public async Task<dynamic> GetPieChartData(string id, int run)
        {
            if (string.IsNullOrWhiteSpace(id) || run <= 0)
            {
                return NotFound();
            }

           var result = await _dataStoreSvr.GetStatusCodePieData(id, run);

            var data = new List<decimal>();
            var colour = new List<string>();
            var labels = new List<string>();

            foreach (var item in result)
            {
                data.Add(item.StatusCodePercent);
                colour.Add(StatusCodeColour.GetColour(item.StatusCode));
                labels.Add(item.StatusCode.ToString());
            }

            var json = new
            {
                datasets = new[]
                {
                   new
                   {
                       data = data.ToArray(),
                       backgroundColor = colour.ToArray()
                   }
                },

                labels = labels.ToArray()
            };

            return json;
        }

        /// <summary>
        /// dashboard/statusCodePie
        /// </summary>
        [HttpPost("headerStatusCodes/{id}/{run?}")]
        [ValidateAntiForgeryToken]
        public async Task<dynamic> GetHeaderStatusCodes(string id, int run)
        {
            if (string.IsNullOrWhiteSpace(id) || run <= 0)
            {
                return NotFound();
            }

            var result = await _dataStoreSvr.GetResponseDetails(id, run);
            return result;
        }

        /// <summary>
        /// dashboard/statusCodePie
        /// </summary>
        [HttpGet("statusCodeLines/{id}/{run?}")]
        //[ValidateAntiForgeryToken]
        public async Task<dynamic> GetStatusCodeLines(string id, int run)
        {
            if (string.IsNullOrWhiteSpace(id) || run <= 0)
            {
                return NotFound();
            }

            var result = await _dataStoreSvr.GetLineChartRequests(id, run);
            return result;
        }

    }
}
