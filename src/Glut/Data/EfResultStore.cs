using Glut.Data;
using Glut.Interface;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Glut.Services
{
    public class EfResultStore : IResultStore
    {
        private readonly EfDbContext _context;
        private readonly IEnvironment _environment;

        public EfResultStore(EfDbContext context, IEnvironment environment)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
            _environment = environment;
        }

        public void Add(string projectName, int runId, IDictionary<string, string> attributes, ThreadResult result)
        {
            if(string.IsNullOrWhiteSpace(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }
            if (attributes == null)
            {
                throw new ArgumentNullException(nameof(attributes));
            }
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            if(runId <= 0)
            {
                runId = GetProjectLastRunId(projectName) + 1;
            }

            var items = new List<GlutResultItem>();

            SaveProject(projectName);
            SaveRunAttributes(projectName, runId, attributes);

            foreach (var pair in result.Results)
            {
                for (int i = 0; i < pair.Value.IsSuccessStatusCodes.Count; i++)
                {
                    var item = new GlutResultItem
                    {
                        GlutProjectName = projectName,
                        GlutProjectRunId = runId,
                        StartDateTimeUtc = pair.Value.StartDateTimes[i],
                        EndDateTimeUtc = pair.Value.EndDateTimes[i],
                        RequestUri = pair.Key,
                        IsSuccessStatusCode = pair.Value.IsSuccessStatusCodes[i],
                        StatusCode = pair.Value.StatusCodes[i],
                        HeaderLength = pair.Value.HeaderLengths[i],
                        ResponseLength = pair.Value.ResponseLengths[i],
                        TotalLegth = pair.Value.HeaderLengths[i] + pair.Value.ResponseLengths[i],
                        RequestSentTicks = pair.Value.RequestSentTicks[i],
                        ResponseTicks = pair.Value.ResponseTicks[i],
                        TotalTicks = pair.Value.RequestSentTicks[i] + pair.Value.ResponseTicks[i],
                        ResponseHeaders = (pair.Value.ResponseHeaders.Count > i) ? pair.Value.ResponseHeaders[i] : null,
                        Exception = (pair.Value.Exceptions.Count > i) ? pair.Value.Exceptions[i].ToString() : null,
                        CreatedDateTimeUtc = _environment.SystemDateTimeUtc,
                        CreatedByUserName = _environment.UserName
                    };
                    items.Add(item);
                }
            }

            _context.AddRange(items);
            _context.SaveChanges();
        }

        public int GetProjectLastRunId(string projectName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                throw new ArgumentNullException(nameof(projectName));
            }
            return _context.Results.Where(x => x.GlutProjectName == projectName).DefaultIfEmpty().Max(x => x.GlutProjectRunId);
        }

        private void SaveRunAttributes(string projectName, int runId, IDictionary<string, string> attributes)
        {
            var items = from x in attributes
                        select new GlutRunAttribute
                        {
                            GlutProjectName = projectName,
                            GlutProjectRunId = runId,
                            AttributeName = x.Key,
                            AttributeValue = x.Value,
                            CreatedDateTimeUtc = _environment.SystemDateTimeUtc,
                            CreatedByUserName = _environment.UserName
                        };

            _context.AddRange(items);
        }

        private void SaveProject(string projectName)
        {
            if(_context.Projects.Any(x => x.GlutProjectName == projectName) == false)
            {
                var project = new GlutProject
                {
                    GlutProjectName = projectName,
                    CreatedDateTimeUtc = _environment.SystemDateTimeUtc,
                    ModifiedDateTimeUtc = _environment.SystemDateTimeUtc,
                    CreatedByUserName = _environment.UserName
                };
                _context.Add(project);
            }
        }
    }
}