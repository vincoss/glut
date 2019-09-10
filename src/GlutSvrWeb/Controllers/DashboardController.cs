using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                Labels = results.Select(x => x.TimeSeries.ToString("hh.mm.ss.fff")).ToArray(),
                TotalRequests = results.Where(x => x.SeriesString == AppResources.TotalRequests).Select(x => x.Value).ToArray(),
                Successful = results.Where(x => x.SeriesString == AppResources.Successful).Select(x => x.Value).ToArray(),
                ClientError = results.Where(x => x.SeriesString == AppResources.ClientError).Select(x => x.Value).ToArray(),
                ServerError = results.Where(x => x.SeriesString == AppResources.ServerError).Select(x => x.Value).ToArray(),
            };
            return json;

            //var dataSets = new List<dynamic>();
            //var labels = results.Select(x => x.TimeSeries).ToArray();
            //var groups = (from x in results
            //             group x by new { x.TimeSeries, x.SeriesString } into g
            //             select new
            //             {
            //                 g.Key,
            //                 Count = g.Sum(x => x.Value)
            //             }).ToArray();

            //var total = results.Where(x => x.SeriesString == AppResources.TotalRequests).OrderBy(x => x.TimeSeries).Select(x => x.Value).ToArray();
            //var totalColour = results.Where(x => x.SeriesString == AppResources.TotalRequests).OrderBy(x => x.TimeSeries).Select(x => "Red").ToArray();

            //var success = results.Where(x => x.SeriesString == AppResources.Successful).OrderBy(x => x.TimeSeries).Select(x => x.Value).ToArray();
            //var successColour = results.Where(x => x.SeriesString == AppResources.Successful).OrderBy(x => x.TimeSeries).Select(x => "Green").ToArray();

            ////  "fill":false,"borderColor":"rgb(75, 192, 192)","lineTension":0.1}

            //dataSets.Add(new
            //{
            //    data = total,
            //    borderColor = totalColour.ToArray(),
            //    fill = false,
            //    lineTension = 0.1
            //});

            //dataSets.Add(new
            //{
            //    data = success,
            //    borderColor = successColour.ToArray(),
            //    fill = false,
            //    lineTension = 0.1
            //}); 

            //foreach (var item in groups)
            //{
            //    var data = new List<decimal>();
            //    var colour = new List<string>();

            //    data.Add(item.Count);
            //    colour.Add("Red");

            //    dataSets.Add(new
            //    {
            //        data = data.ToArray(),
            //        backgroundColor = colour.ToArray()
            //    });
            //}

            //var json = new
            //{
            //    datasets = dataSets.ToArray(),
            //    labels = labels.ToArray()
            //};

            //return json;
        }

        /*
          var dataPoints =
            {
                labels: ['00:00:01', '00:00:02', '00:00:03', '00:00:04', '00:00:05', '00:00:06', '00:00:07', '00:00:08', '00:00:09', '00:00:10'],
                datasets: [{
                    data: [86, 1500, 106, 300, 500, 111, 900, 221, 1300, 2478],
                    label: "Success",
                    borderColor: "#33FF33",
                    fill: false,
                    type: 'line',
                    pointRadius: 0,
                    fill: false,
                    lineTension: 0,
                    borderWidth: 2
                },
                {
                    data: [0, 50, 1, 300, 33, 0, 10, 100, 0, 4],
                    label: "Error",
                    borderColor: "#FF5733",
                    fill: false,
                    type: 'line',
                    pointRadius: 0,
                    fill: false,
                    lineTension: 0,
                    borderWidth: 2
                }]
            }; 
        */

    }
}
