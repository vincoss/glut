using Glut;
using Glut.Data;
using Glut.Services;
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
        private const long MillisecondTicks = 10000;
        private const int KbBits = 1024;

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
            var project = (from x in _context.RunAttributes.AsNoTracking()
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
            if (string.IsNullOrWhiteSpace(projectName))
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
                        select new
                        {
                            ProjectName = x.GlutProjectName,
                            Runs = count,
                            LastChangeDateTime = x.ModifiedDateTimeUtc
                        };

            int recordsTotal = 0;
            int recordsFilteredTotal = 0;

            recordsTotal = query.Count();

            // Search
            if (!string.IsNullOrWhiteSpace(args.Search))
            {
                query = query.Where(x => x.ProjectName != null && EF.Functions.Like(x.ProjectName, $"%{args.Search}%"));
            }

            recordsFilteredTotal = query.Count();

            // Sort
            if (string.IsNullOrWhiteSpace(args.SortColumn) == false)
            {
                var sortColumn = LinqExtensions.GetPropertyNameIgnoreCase(typeof(ProjectDto), args.SortColumn);

                if (string.IsNullOrWhiteSpace(sortColumn))
                {
                    throw new InvalidOperationException($"Could not find column: {sortColumn}");
                }

                query = query.OrderBy(sortColumn, string.Equals(GlutWebConstants.SortDirectionAsc, args.SortDirection, StringComparison.CurrentCultureIgnoreCase));
            }

            // Paging
            var model = (from x in query.Skip(args.Skip).Take(args.Take)
                         select new ProjectDto
                         {
                             ProjectName = x.ProjectName,
                             Runs = x.Runs,
                             LastChangeDateTime = x.LastChangeDateTime.ToLocalTime()
                         }).ToArray();

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
                        where x.GlutProjectName == projectName && x.GlutProjectRunId == runId
                        select new
                        {
                            StartDateTime = x.StartDateTimeUtc,
                            EndDateTime = x.EndDateTimeUtc,
                            Url = x.Url,
                            IsSuccessStatusCode = x.IsSuccessStatusCode,
                            StatusCode = x.StatusCode,
                            HeaderLength = x.HeaderLength,
                            ResponseLength = x.ResponseLength,
                            TotalLength = x.TotalLength,
                            RequestTicks = x.RequestSentTicks,
                            ResponseTicks = x.ResponseTicks,
                            TotalTicks = x.TotalTicks,
                            ResponseHeaders = x.ResponseHeaders,
                            Exception = x.Exception,
                            CreatedDateTime = x.CreatedDateTimeUtc,
                            CreatedByUser = x.CreatedByUserName
                        };

            int recordsTotal = 0;
            int recordsFilteredTotal = 0;

            recordsTotal = query.Count();

            // Search
            if (!string.IsNullOrWhiteSpace(args.Search))
            {
                query = query.Where(x => 
                (x.Url != null && EF.Functions.Like(x.Url, $"%{args.Search}%")) ||
                //(EF.Functions.Like(Convert.ToString(x.StatusCode), $"%{args.Search}%")) || // TODO:
                (x.ResponseHeaders != null && EF.Functions.Like(x.ResponseHeaders, $"%{args.Search}%")));
            }

            recordsFilteredTotal = query.Count();

            // Sort
            if (string.IsNullOrWhiteSpace(args.SortColumn) == false)
            {
                var sortColumn = LinqExtensions.GetPropertyNameIgnoreCase(typeof(ResultItemDto), args.SortColumn);

                if (string.IsNullOrWhiteSpace(sortColumn))
                {
                    throw new InvalidOperationException($"Could not find column :{sortColumn}");
                }

                query = query.OrderBy(sortColumn, string.Equals(GlutWebConstants.SortDirectionAsc, args.SortDirection, StringComparison.CurrentCultureIgnoreCase));
            }

            var model = (from x in query.Skip(args.Skip).Take(args.Take)
                         select new ResultItemDto
                         {
                             StartDateTime = x.StartDateTime.ToLocalTime(),
                             EndDateTime = x.EndDateTime.ToLocalTime(),
                             Url = x.Url,
                             IsSuccessStatusCode = x.IsSuccessStatusCode,
                             StatusCode = x.StatusCode,
                             HeaderLength = ConvertToKb(x.HeaderLength),
                             ResponseLength = ConvertToKb(x.ResponseLength),
                             TotalLength = ConvertToKb(x.TotalLength),
                             RequestTicks = ConvertToMillisecond(x.RequestTicks),
                             ResponseTicks = ConvertToMillisecond(x.ResponseTicks),
                             TotalTicks = ConvertToMillisecond(x.TotalTicks),
                             ResponseHeaders = x.ResponseHeaders,
                             Exception = x.Exception,
                             CreatedDateTime = x.CreatedDateTime.ToLocalTime(),
                             CreatedByUser = x.CreatedByUser
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

        #region Dashboard

        public Task<StatusCodeHeaderDto> GetResponseDetails(string projectName, int runId)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }
            if (runId <= 0)
            {
                throw new ArgumentException(nameof(runId));
            }

            var query = _context.Results.AsNoTracking().Where(x => x.GlutProjectName == projectName && x.GlutProjectRunId == runId);

            var total = query.Count();
            var information = query.Count(x => x.StatusCode >= 100 && x.StatusCode < 200);
            var success = query.Count(x => x.StatusCode >= 200 && x.StatusCode < 300);
            var redirection = query.Count(x => x.StatusCode >= 300 && x.StatusCode < 400);
            var clientError = query.Count(x => x.StatusCode >= 400 && x.StatusCode < 500);
            var serverError = query.Count(x => x.StatusCode >= 500);

            var result = new StatusCodeHeaderDto
            {
                TotalRequests = total,
                Information = GlutSvrExtensions.GetPercent(information, total),
                Successful = GlutSvrExtensions.GetPercent(success, total),
                Redirection = GlutSvrExtensions.GetPercent(redirection, total),
                ClientError = GlutSvrExtensions.GetPercent(clientError, total),
                ServerError = GlutSvrExtensions.GetPercent(serverError, total)
            };

            return Task.FromResult(result);
        }

        public async Task<IEnumerable<KeyValueData<string>>> GetRunAttributes(string projectName, int runId)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }
            if (runId <= 0)
            {
                throw new ArgumentException(nameof(runId));
            }

            var query = _context.RunAttributes.AsNoTracking()
                                              .Where(x => x.GlutProjectName == projectName && x.GlutProjectRunId == runId);

            return await (from x in query
                          select new KeyValueData<string>
                          {
                              Key = x.AttributeName,
                              Value = GetValue(x.AttributeName, x.AttributeValue)
                          }).ToListAsync();
        }

        private static string GetValue(string key, string value)
        {
            if(string.Equals(GlutConstants.StartDateTime, key, StringComparison.OrdinalIgnoreCase) ||
               string.Equals(GlutConstants.EndDateTime, key, StringComparison.OrdinalIgnoreCase))
            {
                return DateTime.Parse(value).ToLocalTime().ToString();
            }
            return value;
        }

        public async Task<IEnumerable<StatusCodePieDto>> GetStatusCodePieData(string projectName, int runId)
        {
            if (string.IsNullOrWhiteSpace(projectName))
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
                                 group x by (x.StatusCode / 100) into g
                                 select new StatusCodePieDto
                                 {
                                     StatusCode = g.Key,
                                     StatusCodeItems = g.Count(),
                                     StatusCodePercent = ((decimal)g.Count() * 100) / total,
                                     TotalItems = total

                                 }).ToListAsync();

            return results;
        }

        public async Task<IEnumerable<TopSuccessOrErrorResquestDto>> GetTopSuccessRequests(string projectName, int runId)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }
            if (runId <= 0)
            {
                throw new ArgumentException(nameof(runId));
            }

            var query = from x in _context.Results.AsNoTracking()
                        where x.GlutProjectName == projectName && x.GlutProjectRunId == runId &&
                              x.StatusCode >= 200 && x.StatusCode <= 299
                        select x;

            var total = await query.CountAsync();

            var data = (from x in _context.Results.AsNoTracking()
                        where x.GlutProjectName == projectName && x.GlutProjectRunId == runId &&
                              x.StatusCode >= 200 && x.StatusCode <= 299
                        group x by x.Url into g
                        select new
                        {
                            Url = g.Key,
                            Count = g.Count(),
                        }).ToArray(); ;

            var results = (from x in data
                                 orderby x.Url
                                 select new TopSuccessOrErrorResquestDto
                                 {
                                     Url = x.Url,
                                     Count = x.Count,
                                     Frequency = ((decimal)x.Count * 100) / total,
                                     TotalItems = total

                                 }).OrderByDescending(o => o.Count).Take(10).ToList();

            return results;
        }

        public async Task<IEnumerable<TopSuccessOrErrorResquestDto>> GetTopErrorRequests(string projectName, int runId)
        {
            if (string.IsNullOrWhiteSpace(projectName))
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

            var data = (from x in _context.Results.AsNoTracking()
                     where x.GlutProjectName == projectName && x.GlutProjectRunId == runId &&
                           x.StatusCode >= 400
                     group x by x.Url into g
                     select new
                     {
                         Url = g.Key,
                         Count = g.Count(),
                     }).ToArray(); ;

            var results = (from x in data
                                 select new TopSuccessOrErrorResquestDto
                                 {
                                     Url = x.Url,
                                     Count = x.Count,
                                     Frequency = ((decimal)x.Count * 100) / total,
                                     TotalItems = total

                                 }).OrderByDescending(o => o.Count).ToList();

            return results;
        }

        public async Task<IEnumerable<TopMinMaxAvgResquestDto>> GetFastestSuccessRequests(string projectName, int runId)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }
            if (runId <= 0)
            {
                throw new ArgumentException(nameof(runId));
            }

            var query = from x in _context.Results.AsNoTracking()
                        where x.GlutProjectName == projectName && x.GlutProjectRunId == runId &&
                              x.StatusCode >= 200 && x.StatusCode <= 299
                        select x;

            var results = await (from x in query
                                 group x by x.Url into g
                                 select new TopMinMaxAvgResquestDto
                                 {
                                     Url = g.Key,
                                     Min = g.Min(x => x.TotalTicks) / MillisecondTicks,     // TO ms
                                     Max = g.Max(x => x.TotalTicks) / MillisecondTicks,     // TO ms
                                     Avg = g.Average(x => x.TotalTicks) / MillisecondTicks, // TO ms

                                 }).OrderBy(x => x.Min).Take(10).ToListAsync();

            return results;
        }

        public async Task<IEnumerable<TopMinMaxAvgResquestDto>> GetSlowestSuccessRequests(string projectName, int runId)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }
            if (runId <= 0)
            {
                throw new ArgumentException(nameof(runId));
            }

            var query = from x in _context.Results.AsNoTracking()
                        where x.GlutProjectName == projectName && x.GlutProjectRunId == runId &&
                              x.StatusCode >= 200 && x.StatusCode <= 299
                        select x;

            var results = await (from x in query
                                 group x by x.Url into g
                                 select new TopMinMaxAvgResquestDto
                                 {
                                     Url = g.Key,
                                     Min = g.Min(x => x.TotalTicks) / MillisecondTicks,     // TO ms
                                     Max = g.Max(x => x.TotalTicks) / MillisecondTicks,     // TO ms
                                     Avg = g.Average(x => x.TotalTicks) / MillisecondTicks, // TO ms

                                 }).OrderByDescending(x => x.Max).Take(10).ToListAsync();

            return results;
        }

        public async Task<IEnumerable<TopMinMaxAvgResquestDto>> GetAvgSuccessRequests(string projectName, int runId)
        {
            if (string.IsNullOrWhiteSpace(projectName))
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
                                 group x by x.Url into g
                                 select new TopMinMaxAvgResquestDto
                                 {
                                     Url = g.Key,
                                     Min = g.Min(x => x.TotalTicks) / MillisecondTicks,     // TO ms
                                     Max = g.Max(x => x.TotalTicks) / MillisecondTicks,     // TO ms
                                     Avg = g.Average(x => x.TotalTicks) / MillisecondTicks, // TO ms

                                 }).OrderBy(x => x.Avg).Take(10).ToListAsync();

            return results;
        }

        public async Task<IEnumerable<LargestSizeRequestDto>> GetLargestSuccessRequests(string projectName, int runId)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }
            if (runId <= 0)
            {
                throw new ArgumentException(nameof(runId));
            }

            var query = from x in _context.Results.AsNoTracking()
                        where x.GlutProjectName == projectName && x.GlutProjectRunId == runId &&
                              x.StatusCode >= 200 && x.StatusCode <= 299
                        select x;

            var totalSize = query.Sum(x => x.TotalLength);

            var res = await (from x in query
                             group x by x.Url into g
                             select new
                             {
                                 Url = g.Key,
                                 Length = g.Max(x => x.TotalLength)
                             }).OrderByDescending(o => o.Length).Take(10).ToListAsync();

            var results = (from x in res
                           select new LargestSizeRequestDto
                           {
                               Url = x.Url,
                               Length = (decimal)x.Length / KbBits,    // To Kb
                               Percent = ((decimal)x.Length * 100) / totalSize,
                               TotalLength = totalSize / KbBits

                           }).ToArray();

            return results;
        }

        public async Task<LineChartDto> GetLineChartRequests(string projectName, int runId)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }
            if (runId <= 0)
            {
                throw new ArgumentException(nameof(runId));
            }

            var query =  _context.Results.AsNoTracking().Where(x => x.GlutProjectName == projectName && x.GlutProjectRunId == runId);

            var min = query.Min(x => x.EndDateTimeUtc);
            var max = query.Max(x => x.EndDateTimeUtc);
            var diff = TimeSpan.FromTicks(max.Ticks - min.Ticks);

            var groups = await (from x in query
                                let res = (x.EndDateTimeUtc.Ticks - min.Ticks)
                                orderby x.EndDateTimeUtc
                                group x by new { Ticks = res, StatusCode = (x.StatusCode / 100) } into g
                                select new
                                {
                                    g.Key.Ticks,
                                    g.Key.StatusCode,
                                    Count = g.Count()
                                }).ToListAsync();

            var groupg = (from x in groups
                         select new
                         {
                             Seconds = TimeSpan.FromSeconds(Math.Round(TimeSpan.FromTicks(x.Ticks).TotalSeconds)),
                             x.StatusCode,
                             x.Count
                         }).ToArray();

            var model = new LineChartDto();
            model.Labels = Enumerable.Range(0, (int)Math.Round(diff.TotalSeconds) + 1).Select(x => new DateTime(TimeSpan.FromSeconds(x).Ticks).ToString("mm:ss")).ToArray();

            // Total
             model.TotalRequests = (from x in groupg
                                  group x by x.Seconds into g
                                  select g.Sum(c => c.Count)).ToArray();

            var codeGroups = (from x in groupg
                                   group x by new { x.Seconds, x.StatusCode  } into g
                                   select new
                                   {
                                       g.Key.Seconds,
                                       g.Key.StatusCode,
                                       Count = g.Sum(c => c.Count)
                                   }).ToArray();

            model.Information = Enumerable.Repeat<int?>(null, model.Labels.Count()).ToArray();
            model.Successful = Enumerable.Repeat<int?>(null, model.Labels.Count()).ToArray();
            model.Redirection = Enumerable.Repeat<int?>(null, model.Labels.Count()).ToArray();
            model.ClientError = Enumerable.Repeat<int?>(null, model.Labels.Count()).ToArray();
            model.ServerError = Enumerable.Repeat<int?>(null, model.Labels.Count()).ToArray();

            foreach (var item in codeGroups)
            {
                if(item.StatusCode == 1)
                {
                    model.Information[(int)item.Seconds.TotalSeconds] = item.Count;
                }
                if (item.StatusCode == 2)
                {
                    model.Successful[(int)item.Seconds.TotalSeconds] = item.Count;
                }
                if (item.StatusCode == 3)
                {
                    model.Redirection[(int)item.Seconds.TotalSeconds] = item.Count;
                }
                if (item.StatusCode == 4)
                {
                    model.ClientError[(int)item.Seconds.TotalSeconds] = item.Count;
                }
                if (item.StatusCode == 5)
                {
                    model.ServerError[(int)item.Seconds.TotalSeconds] = item.Count;
                }
            }

            return model;
        }

        public async Task<LinChartRunDto> GetLineChartRuns(string projectName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }

            var query = from x in _context.Results.AsNoTracking()
                        where x.GlutProjectName == projectName && x.StatusCode >= 200 && x.StatusCode <= 299
                        select x;

            var lastRuns = (from x in query
                            group x by x.GlutProjectRunId into g
                            select new
                            {
                                RunId = g.Key,
                                MinDateTicks = g.Min(x => x.EndDateTimeUtc).Ticks,
                                MaxDateTicks = g.Max(x => x.EndDateTimeUtc).Ticks,
                                Diff = g.Max(x => x.EndDateTimeUtc).Ticks - g.Min(x => x.EndDateTimeUtc).Ticks
                            }).Distinct().OrderByDescending(x=> x.RunId).Take(5).ToArray();

            var diff = TimeSpan.FromTicks(lastRuns.OrderByDescending(x => x.Diff).First().Diff);

            var groups = await (from x in query
                                where lastRuns.Select(s => s.RunId).Contains(x.GlutProjectRunId)
                                orderby x.EndDateTimeUtc
                                group x by new { Ticks = x.EndDateTimeUtc.Ticks, x.GlutProjectRunId } into g
                                select new
                                {
                                    g.Key.Ticks,
                                    g.Key.GlutProjectRunId,
                                    Count = g.Count()
                                }).ToListAsync();

            var groupg = (from x in groups
                          from y in lastRuns
                          where x.GlutProjectRunId == y.RunId
                          select new
                          {
                              Seconds = TimeSpan.FromSeconds(Math.Round(TimeSpan.FromTicks(x.Ticks - y.MinDateTicks).TotalSeconds)),
                              x.GlutProjectRunId,
                              x.Count
                          }).ToArray();

            var runGroups = (from x in groupg
                             orderby x.Seconds
                             orderby x.GlutProjectRunId
                             group x by new { x.Seconds, x.GlutProjectRunId } into g
                             select new
                             {
                                 g.Key.Seconds,
                                 g.Key.GlutProjectRunId,
                                 Count = g.Sum(c => c.Count)
                             }).ToArray();

            var model = new LinChartRunDto();
            model.Labels = Enumerable.Range(0, (int)Math.Round(diff.TotalSeconds) + 1).Select(x => new DateTime(TimeSpan.FromSeconds(x).Ticks).ToString("mm:ss")).ToArray();

            var colours = new[]
            {
                StatusCodeHelper.Information,
                StatusCodeHelper.Successful,
                StatusCodeHelper.Redirection,
                StatusCodeHelper.ClientError,
                StatusCodeHelper.ServerError,
            };

            for(int i = 0; i < lastRuns.Count(); i++)
            {
                var run = lastRuns[i];
                var data = new LinChartRunDto.DataInfo();
                data.Label = $"Run-{run.RunId}";
                data.BorderColor = colours[i];
                data.Fill = false;
                data.LineTension = 0;
                data.Data = Enumerable.Repeat<int?>(null, model.Labels.Count()).ToArray();
                model.DataSets.Add(data);

                foreach (var item in runGroups.Where(x => x.GlutProjectRunId == run.RunId))
                {
                    data.Data[(int)item.Seconds.TotalSeconds] = item.Count;
                }
            }

            return model;
        }

        #endregion

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
