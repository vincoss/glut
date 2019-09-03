using Glut.Data;
using GlutSvrWeb.Dto;
using GlutSvrWeb.Interfaces;
using GlutSvrWeb.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GlutSvrWeb.Services
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

        public LastRunDto GetLastProject()
        {
            var project = (from x in _context.Results.AsNoTracking()
                           orderby x.CreatedDateTimeUtc descending
                           select x).FirstOrDefault();

            var result = new LastRunDto();

            if(project != null)
            {
                result.ProjectName = project.GlutProjectName;
                result.RunId = project.GlutProjectRunId;
            }

            return result;
        }

        public async Task<IEnumerable<string>> GetProjectString()
        {
            return await (from x in _context.Projects.AsNoTracking()
                          orderby x.ModifiedDateTimeUtc descending
                          orderby x.GlutProjectName ascending
                          select x.GlutProjectName).ToListAsync();
        }

        public async Task<IEnumerable<int>> GetProjectRuns(string projectName)
        {
            if (string.IsNullOrEmpty(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }

            var query = _context.RunAttributes.AsNoTracking()
                                              .Where(x => x.GlutProjectName == projectName);

            return await (from x in query
                          group x by x.GlutProjectRunId into g
                          orderby g.Key descending
                          select g.Key).ToListAsync();

        }

        public DataTableDto<ProjectDto> GetProjects(DataTableParameter args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            var query = from x in _context.Projects.AsNoTracking()
                                let count = _context.RunAttributes.AsNoTracking()
                                                                  .Where(a => a.GlutProjectName == x.GlutProjectName)
                                                                  .GroupBy(g => g.GlutProjectRunId)
                                                                  .Count()
                                orderby x.ModifiedDateTimeUtc descending
                                select new ProjectDto
                                {
                                    ProjectName = x.GlutProjectName,
                                    Runs = count,
                                    LastChangeDateTime = x.ModifiedDateTimeUtc
                                };


            int recordsTotal = 0;
            int recordsFilteredTotal = 0;

            recordsTotal = query.Count();

            // Sort
            if (string.IsNullOrWhiteSpace(args.SortColumn) == false)
            {
                var sortColumn = LinqExtensions.GetPropertyName(typeof(ProjectDto), args.SortColumn);
                query = query.OrderBy(sortColumn, string.Equals(ViewConstants.SortDirectionAsc, args.SortDirection, StringComparison.CurrentCultureIgnoreCase));
            }

            // Search
            if (!string.IsNullOrWhiteSpace(args.Search))
            {
                query = query.Where(m => m.ProjectName != null && m.ProjectName.StartsWith(args.Search, StringComparison.CurrentCultureIgnoreCase));
            }

            recordsFilteredTotal = query.Count();
            var model = query.Skip(args.Skip).Take(args.Take).ToList();

            var response = new DataTableDto<ProjectDto>
            {
                Draw = args.Draw,
                RecordsFiltered = recordsFilteredTotal,
                RecordsTotal = recordsTotal,
                Data = model
            };

            return response;
        }

        public DataTableDto<ResultItemDto> GetResultItems(string projectName, int runId, DataTableParameter args)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }
            if (runId <= 0)
            {
                throw new ArgumentException(nameof(runId));
            }
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            var query = from x in _context.Results.AsNoTracking()
                        where x.GlutProjectName.Equals(projectName, StringComparison.CurrentCultureIgnoreCase) &&
                              x.GlutProjectRunId == runId
                        select x;

            int recordsTotal = 0;
            int recordsFilteredTotal = 0;

            recordsTotal = query.Count();

            // Sort
            if (string.IsNullOrWhiteSpace(args.SortColumn) == false)
            {
                var sortColumn = LinqExtensions.GetPropertyName(typeof(ProjectDto), args.SortColumn);
                query = query.OrderBy(sortColumn, string.Equals(ViewConstants.SortDirectionAsc, args.SortDirection, StringComparison.CurrentCultureIgnoreCase));
            }

            // Search
            if (!string.IsNullOrWhiteSpace(args.Search))
            {
                query = query.Where(m =>
                (m.RequestUri != null && m.RequestUri.Contains(args.Search, StringComparison.CurrentCultureIgnoreCase)) ||
                (m.StatusCode.ToString().Contains(args.Search)) ||
                (m.ResponseHeaders != null && m.ResponseHeaders.Contains(args.Search, StringComparison.CurrentCultureIgnoreCase)));
            }

            recordsFilteredTotal = query.Count();
            var model = (from x in query.Skip(args.Skip).Take(args.Take)
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
                         }).ToArray();

            var response = new DataTableDto<ResultItemDto>
            {
                Draw = args.Draw,
                RecordsFiltered = recordsFilteredTotal,
                RecordsTotal = recordsTotal,
                Data = model
            };

            return response;
        }

        public Task<IDictionary<string, decimal>> GetResponseDetails(string projectName, int runId)
        {
            if (string.IsNullOrEmpty(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }
            if (runId <= 0)
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

        public async Task<IEnumerable<StatusCodePieDto>> GetStatusCodePieData(string projectName, int runId)
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

            var total = await query.CountAsync();

            var results = await (from x in query
                                 orderby x.StatusCode
                                 group x by x.StatusCode into g
                                 select new StatusCodePieDto
                                 {
                                     StatusCode = g.Key,
                                     StatusCodeItems = g.Count(),
                                     StatusCodePercent = (g.Count() * 100) / total,
                                     TotalItems = total

                                 }).ToListAsync();

            return results;
        }

        public async Task<IEnumerable<TopSuccessOrErrorResquestDto>> GetTopSuccessRequests(string projectName, int runId)
        {
            if (string.IsNullOrEmpty(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }
            if (runId <= 0)
            {
                throw new ArgumentException(nameof(runId));
            }

            if (string.IsNullOrEmpty(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }
            if (runId <= 0)
            {
                throw new ArgumentException(nameof(runId));
            }

            var query = from x in _context.Results.AsNoTracking()
                        where x.GlutProjectName == projectName && x.GlutProjectRunId == runId &&
                              x.StatusCode >= 200 && x.StatusCode < 300
                        select x;

            var total = await query.CountAsync();

            var results = await (from x in query
                                 orderby x.RequestUri
                                 group x by x.RequestUri into g
                                 select new TopSuccessOrErrorResquestDto
                                 {
                                     Url = g.Key,
                                     Count = g.Count(),
                                     Frequency = (g.Count() * 100) / total,
                                     TotalItems = total

                                 }).OrderByDescending(o => o.Count).ToListAsync();

            return results;
        }

        public async Task<IEnumerable<TopSuccessOrErrorResquestDto>> GetTopErrorRequests(string projectName, int runId)
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
                        where x.GlutProjectName == projectName && x.GlutProjectRunId == runId &&
                              x.StatusCode >= 400
                        select x;

            var total = await query.CountAsync();

            var results = await (from x in query
                                 orderby x.StatusCode
                                 group x by x.RequestUri into g
                                 select new TopSuccessOrErrorResquestDto
                                 {
                                     Url = g.Key,
                                     Count = g.Count(),
                                     Frequency = (g.Count() * 100) / total,
                                     TotalItems = total

                                 }).OrderByDescending(o => o.Count).ToListAsync();

            return results;
        }

        public async Task<IEnumerable<KeyValueData<decimal>>> GetFastestSuccessRequests(string projectName, int runId)
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
                        where x.GlutProjectName == projectName && x.GlutProjectRunId == runId &&
                              x.StatusCode >= 200 && x.StatusCode < 300
                        select x;

            var results = await (from x in query
                                 group x by x.RequestUri into g
                                 select new KeyValueData<decimal>
                                 {
                                     Key = g.Key,
                                     Value = g.Min(x => x.TotalTicks)

                                 }).OrderBy(x => x.Value).Take(10).ToListAsync();

            return results;
        }

        public async Task<IEnumerable<KeyValueData<decimal>>> GetSlowestSuccessRequests(string projectName, int runId)
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
                        where x.GlutProjectName == projectName && x.GlutProjectRunId == runId &&
                              x.StatusCode >= 200 && x.StatusCode < 300
                        select x;

            var results = await (from x in query
                                 group x by x.RequestUri into g
                                 select new KeyValueData<decimal>
                                 {
                                     Key = g.Key,
                                     Value = g.Max(x => x.TotalTicks)

                                 }).OrderByDescending(x => x.Value).Take(10).ToListAsync();

            return results;
        }

        public async Task<IEnumerable<KeyValueData<decimal>>> GetLargestSuccessRequests(string projectName, int runId)
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
                        where x.GlutProjectName == projectName && x.GlutProjectRunId == runId &&
                              x.StatusCode >= 200 && x.StatusCode < 300
                        select x;

            var results = await (from x in query
                                 group x by x.RequestUri into g
                                 select new KeyValueData<decimal>
                                 {
                                     Key = g.Key,
                                     Value = g.Max(x => x.TotalLegth)

                                 }).OrderByDescending(x => x.Value).Take(10).ToListAsync();

            return results;
        }

        public async Task<IEnumerable<LineChartDto>> GetLineChartRequests(string projectName, int runId)
        {
            if (string.IsNullOrEmpty(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }
            if (runId <= 0)
            {
                throw new ArgumentException(nameof(runId));
            }

            var query = _context.Results.AsNoTracking().Where(x => x.GlutProjectName == projectName && x.GlutProjectRunId == runId);

            var groups = await (from x in query
                                where x.StatusCode >= 200 && x.StatusCode < 300 || x.StatusCode >= 400
                                orderby x.EndDateTimeUtc
                                group x by new { x.IsSuccessStatusCode, Ticks = (x.EndDateTimeUtc.Ticks / TimeSpan.FromSeconds(1).Ticks) } into g
                                select g).ToListAsync();

            var results = (from x in groups
                           select new LineChartDto
                           {
                               SeriesString = x.Key.IsSuccessStatusCode ? "Success" : "Error",
                               TimeSeries = new DateTime(x.Key.Ticks * TimeSpan.FromSeconds(1).Ticks),
                               Value = x.Count()
                           }).ToList();

            return results.OrderBy(x => x.TimeSeries);
        }

        public async Task<IEnumerable<KeyValueData<string>>> GetRunInfo(string projectName, int runId)
        {
            if (string.IsNullOrEmpty(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }
            if (runId <= 0)
            {
                throw new ArgumentException(nameof(runId));
            }

            var query = _context.RunAttributes.AsNoTracking().Where(x => x.GlutProjectName == projectName && x.GlutProjectRunId == runId);

            return await (from x in query
                          orderby x.AttributeName
                          select new KeyValueData<string>
                          {
                              Key = x.AttributeName,
                              Value = x.AttributeValue
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

    }
}
