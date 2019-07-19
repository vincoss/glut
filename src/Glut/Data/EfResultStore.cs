using Glut.Data;
using Glut.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glut.Services
{
    public class EfResultStore : IResultStore
    {
        private readonly EfDbContext _context;

        public EfResultStore(EfDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public void Add(ThreadResult result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            var items = new List<GlutResultItem>();

            var p = "Test";
            var r = 1;

            foreach (var pair in result.Results)
            {
                for (int i = 0; i < pair.Value.IsSuccessStatusCodes.Count; i++)
                {
                    var item = new GlutResultItem
                    {
                        GlutProjectName = p,
                        GlutProjectRunId = r,
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
                        ResponseHeaders = pair.Value.ResponseHeaders[i],
                        Exception = pair.Value.Exceptions[i] != null ? pair.Value.Exceptions[i].ToString() : null,
                        CreatedDateTimeUtc = DateTime.UtcNow,
                        CreatedByUserName = Environment.UserName // TODO:
                    };
                    items.Add(item);
                }
            }

            _context.AddRange(items);
            _context.SaveChanges();


        }
    }
}