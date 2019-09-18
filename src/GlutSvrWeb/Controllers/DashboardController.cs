using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glut.Services;
using GlutSvrWeb.Dto;
using GlutSvrWeb.Interfaces;
using GlutSvrWeb.Properties;
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
                colour.Add(StatusCodeHelper.GetColour(item.StatusCode));
                labels.Add(StatusCodeHelper.GetStatusCodeString(item.StatusCode));
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
        
        [HttpPost("topSuccessfulRequestBox/{id}/{run?}")]
        [ValidateAntiForgeryToken]
        public async Task<dynamic> GetTopSuccessfulRequestBox(string id, int run)
        {
            if (string.IsNullOrWhiteSpace(id) || run <= 0)
            {
                return NotFound();
            }

            var results = await _dataStoreSvr.GetTopSuccessRequests(id, run);
            return results;
        }

        [HttpPost("topErrorRequestBox/{id}/{run?}")]
        [ValidateAntiForgeryToken]
        public async Task<dynamic> GetTopErrorRequestBox(string id, int run)
        {
            if (string.IsNullOrWhiteSpace(id) || run <= 0)
            {
                return NotFound();
            }

            var results = await _dataStoreSvr.GetTopErrorRequests(id, run);
            return results;
        }

        [HttpPost("topMinMaxAvgSuccessfulRequestBox/{id}/{run?}")]
        [ValidateAntiForgeryToken]
        public async Task<dynamic> GetTopMinMaxAvgSuccessfulRequestBox(string id, int run)
        {
            if (string.IsNullOrWhiteSpace(id) || run <= 0)
            {
                return NotFound();
            }

            var results = await _dataStoreSvr.GetFastestSuccessRequests(id, run);
            return results;
        }
        
        [HttpPost("topSlowestMinMaxAvgSuccessfulRequestBox/{id}/{run?}")]
        [ValidateAntiForgeryToken]
        public async Task<dynamic> GetTopSlowestMinMaxAvgSuccessfulRequestBox(string id, int run)
        {
            if (string.IsNullOrWhiteSpace(id) || run <= 0)
            {
                return NotFound();
            }

            var results = await _dataStoreSvr.GetSlowestSuccessRequests(id, run);
            return results;
        }

        [HttpPost("topAverageMinMaxAvgSuccessRequestBox/{id}/{run?}")]
        [ValidateAntiForgeryToken]
        public async Task<dynamic> GetTopAverageMinMaxAvgSuccessRequestBox(string id, int run)
        {
            if (string.IsNullOrWhiteSpace(id) || run <= 0)
            {
                return NotFound();
            }

            var results = await _dataStoreSvr.GetAvgSuccessRequests(id, run);
            return results;
        }

        [HttpPost("topLargestSuccessfulRequestBox/{id}/{run?}")]
        [ValidateAntiForgeryToken]
        public async Task<dynamic> GetTopLargestSuccessfulRequestBox(string id, int run)
        {
            if (string.IsNullOrWhiteSpace(id) || run <= 0)
            {
                return NotFound();
            }

            var results = await _dataStoreSvr.GetLargestSuccessRequests(id, run);
            return results;
        }

        /// <summary>
        /// dashboard/statusCodePie
        /// </summary>
        [HttpPost("statusCodeLines/{id}/{run?}")]
        [ValidateAntiForgeryToken]
        public async Task<dynamic> GetStatusCodeLines(string id, int run)
        {
            if (string.IsNullOrWhiteSpace(id) || run <= 0)
            {
                return NotFound();
            }

            var results = await _dataStoreSvr.GetLineChartRequests(id, run);

            var json = new
            {
                Labels = results.Select(x => x.TimeSeries.ToString("hh.mm.ss.fff")).Distinct().ToArray(),
                TotalRequests = results.Where(x => x.SeriesString == AppResources.TotalRequests).Select(x => x.Value).ToArray(),
                Successful = results.Where(x => x.SeriesString == AppResources.Successful).Select(x => x.Value).ToArray(),
                ClientError = results.Where(x => x.SeriesString == AppResources.ClientError).Select(x => x.Value).ToArray(),
                ServerError = results.Where(x => x.SeriesString == AppResources.ServerError).Select(x => x.Value).ToArray(),
            };
            return json;
        }


        [HttpPost("runAttributeBox/{id}/{run?}")]
        [ValidateAntiForgeryToken]
        public async Task<dynamic> GetRunAttributeBox(string id, int run)
        {
            if (string.IsNullOrWhiteSpace(id) || run <= 0)
            {
                return NotFound();
            }

            var results = await _dataStoreSvr.GetRunAttributes(id, run);
            return results;
        }

        /// <summary>
        /// dashboard/statusCodePie
        /// </summary>
        [HttpPost("totalRequestsPerRunLineBox/{id}/{run?}")]
        [ValidateAntiForgeryToken]
        public async Task<dynamic> GetTotalRequestsPerRunLineBox(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var results = await _dataStoreSvr.GetLineChartRuns(id);

            var r = from x in results
                    group x by x.SeriesString into g
                    select new
                    {
                        g.Key,
                        Data = results.Where(x => x.SeriesString == g.Key).OrderBy(x => x.TimeSeries)
                    };

            var json = new
            {
                Labels = results.Select(x => x.TimeSeries.ToString("hh.mm.ss.fff")).ToArray(),
                Run1 = new { Label = "Hi", Data = results.Select(x => x.Value).ToArray() },
            };
            return json;
        }
    }
}
