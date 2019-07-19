using Glut.Data;
using GlutSvr.Dto;
using GlutSvr.Interfaces;
using GlutSvr.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GlutSvr.Services
{
    public class EfDataStoreSvr : IDataStoreSvr
    {
        private readonly EfDbContext _context;

        public EfDataStoreSvr(EfDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public async Task<IEnumerable<ProjectDto>> GetProjects()
        {
            return await (from x in _context.Projects.AsNoTracking()
                   select new ProjectDto
                   {
                       Name = x.GlutProjectName
                   }).ToListAsync();
        }

        public Task<IDictionary<string, decimal>> GetResponseDetails(string projectName, int runId)
        {
            if(string.IsNullOrEmpty(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }
            if(runId <= 0)
            {
                throw new ArgumentException(nameof(runId));
            }

            var results = new Dictionary<string, decimal>(StringComparer.InvariantCultureIgnoreCase);
            var query = _context.Results.AsNoTracking().Where(x => x.GlutProjectName == projectName && x.GlutProjectRunId == runId);

            var total = query.Count();
            var information = query.Count(x => x.StatusCode >= 100 && x.StatusCode < 200);
            var success = query.Count(x => x.StatusCode >= 200 && x.StatusCode < 300);
            var redirection = query.Count(x => x.StatusCode >= 300 && x.StatusCode < 400);
            var clientError = query.Count(x => x.StatusCode >= 400 && x.StatusCode < 500);
            var serverError = query.Count(x => x.StatusCode >= 500);

            results.Add(AppResources.TotalRequests, total);
            results.Add(AppResources.Information, GlutSvrExtensions.GetPercent(information, total));
            results.Add(AppResources.Successful, GlutSvrExtensions.GetPercent(success, total));
            results.Add(AppResources.Redirection, GlutSvrExtensions.GetPercent(redirection, total));
            results.Add(AppResources.ClientError, GlutSvrExtensions.GetPercent(clientError, total));
            results.Add(AppResources.ServerError, GlutSvrExtensions.GetPercent(serverError, total));

            return Task.FromResult<IDictionary<string, decimal>>(results);
        }

        public async Task<IEnumerable<PieDto>> GetStatusCodePieData(string projectName, int runId)
        {
            if (string.IsNullOrEmpty(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }
            if (runId <= 0)
            {
                throw new ArgumentException(nameof(runId));
            }

            var query = from x in _context.Results.AsNoTracking()
                        where x.GlutProjectName == projectName && x.GlutProjectRunId == runId
                        select x;

            var total = query.Count();

            var results = await (from x in query
                                 orderby x.StatusCode
                                 group x by x.StatusCode into g
                                 select new PieDto
                                 {
                                     StatusCode = g.Key,
                                     StatusCodeItems = g.Count(),
                                     StatusCodePercent = (g.Count() * 100) / total,
                                     TotalItems = total

                                 }).ToListAsync();

            return results;
        }

        public async Task<IEnumerable<ResultItemDto>> GetResultItems(string projectName, int runId)
        {
            if (string.IsNullOrEmpty(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }
            if (runId <= 0)
            {
                throw new ArgumentException(nameof(runId));
            }

            var query = from x in _context.Results.AsNoTracking()
                        where x.GlutProjectName == projectName && x.GlutProjectRunId == runId
                        select x;

            return await (from x in query
                          select new ResultItemDto
                          {
                              StartDateTime = x.StartDateTimeUtc.ToLocalTime(),
                              EndDateTime = x.EndDateTimeUtc.ToLocalTime(),
                              Url = x.RequestUri,
                              IsSuccessStatusCode = x.IsSuccessStatusCode,
                              StatusCode = x.StatusCode,
                              HeaderLength = ConvertToKb(x.HeaderLength),
                              ResponseLength = ConvertToKb(x.ResponseLength),
                              TotalLength = ConvertToKb(x.TotalLegth),
                              RequestTicks = ConvertToMillisecond(x.RequestSentTicks),
                              ResponseTicks = ConvertToMillisecond(x.ResponseTicks),
                              TotalTicks = ConvertToMillisecond(x.TotalTicks),
                              ResponseHeaders = x.ResponseHeaders,
                              Exception = x.Exception,
                              CreatedDateTime = x.CreatedDateTimeUtc.ToLocalTime(),
                              CreatedByUser = x.CreatedByUserName
                          }).ToListAsync();
        }

        public static long ConvertToMillisecond(long ticks)
        {
            return ticks / 10000;
        }

        public static decimal ConvertToKb(long length)
        {
            return ((decimal)length) / 1024M;
        }

        public async Task<IEnumerable<string>> GetProjectString()
        {
            return await (from x in _context.Projects.AsNoTracking()
                         select x.GlutProjectName).ToListAsync();
        }

        // Top Requests total
        // 
    }
}
