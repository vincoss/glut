using Glut.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace GlutSvr
{
    public class SeedData
    {
        public void Initialize(EfDbContext context)
        {
            WithProjects(context);
            WithResults(context);
            WithRunAttributes(context);
        }

        public SeedData WithProjects(EfDbContext context)
        {
            // Look for any movies.
            if (context.Results.Any())
            {
                return this;   // DB has been seeded
            }

            var createdDate = DateTime.UtcNow;

            context.Projects.AddRange(new GlutProject
            {
                GlutProjectName = "Test",
                CreatedDateTimeUtc = createdDate,
                CreatedByUserName = Environment.UserName,
                ModifiedDateTimeUtc = createdDate.AddDays(1)
            });

            context.Projects.AddRange(new GlutProject
            {
                GlutProjectName = "Gabo",
                CreatedDateTimeUtc = createdDate,
                CreatedByUserName = Environment.UserName,
                ModifiedDateTimeUtc = createdDate.AddDays(2)
            });

            context.SaveChanges();

            return this;
        }

        public SeedData WithResults(EfDbContext context)
        {
            // Look for any movies.
            if (context.Results.Any())
            {
                return this;   // DB has been seeded
            }

            var now = DateTime.UtcNow;

            context.Results.AddRange(
                new GlutResultItem
                 {
                     GlutProjectName = "Test",
                     GlutProjectRunId = 1,
                     StartDateTimeUtc = now,
                     EndDateTimeUtc = now.AddSeconds(1),
                     Url = "/information",
                     IsSuccessStatusCode = true,
                     StatusCode = 100,
                     HeaderLength = 1000,
                     ResponseLength = 2000,
                     TotalLength = 3000,
                     RequestSentTicks = 10000,
                     ResponseTicks = 20000,
                     TotalTicks = 30000,
                    ResponseHeaders = "StatusCode: 200",
                     CreatedDateTimeUtc = now.AddSeconds(1),
                     CreatedByUserName = Environment.UserName
                 },
                new GlutResultItem
                {
                    GlutProjectName = "Test",
                    GlutProjectRunId = 1,
                    StartDateTimeUtc = now,
                    EndDateTimeUtc = now.AddSeconds(1),
                    Url = "/successful",
                    IsSuccessStatusCode = true,
                    StatusCode = 200,
                    TotalTicks = 10000,
                    TotalLength = 1000,
                    ResponseHeaders = "Headers",
                    CreatedDateTimeUtc = now.AddSeconds(2),
                    CreatedByUserName = Environment.UserName
                }, 
                new GlutResultItem
                {
                    GlutProjectName = "Test",
                    GlutProjectRunId = 1,
                    StartDateTimeUtc = now,
                    EndDateTimeUtc = now.AddSeconds(1.1),
                    Url = "/successful",
                    IsSuccessStatusCode = true,
                    StatusCode = 200,
                    TotalTicks = 30000,
                    TotalLength = 1000,
                    ResponseHeaders = "Headers",
                    CreatedDateTimeUtc = now.AddSeconds(2.2),
                    CreatedByUserName = Environment.UserName
                },
                new GlutResultItem
                 {
                     GlutProjectName = "Test",
                     GlutProjectRunId = 1,
                     StartDateTimeUtc = now,
                     EndDateTimeUtc = now.AddSeconds(1),
                     Url = "/successful0001",
                     IsSuccessStatusCode = true,
                     StatusCode = 200,
                     TotalTicks = 20000,
                     TotalLength = 2000,
                    ResponseHeaders = "Headers",
                    CreatedDateTimeUtc = now.AddSeconds(3),
                     CreatedByUserName = Environment.UserName
                 },
                new GlutResultItem
                {
                    GlutProjectName = "Test",
                    GlutProjectRunId = 1,
                    StartDateTimeUtc = now,
                    EndDateTimeUtc = now.AddSeconds(1),
                    Url = "/redirection",
                    IsSuccessStatusCode = true,
                    StatusCode = 300,
                    ResponseHeaders = "Headers",
                    CreatedDateTimeUtc = now.AddSeconds(4),
                    CreatedByUserName = Environment.UserName
                },
                new GlutResultItem
                {
                    GlutProjectName = "Test",
                    GlutProjectRunId = 1,
                    StartDateTimeUtc = now,
                    EndDateTimeUtc = now.AddSeconds(1),
                    Url = "/clientError",
                    IsSuccessStatusCode = true,
                    StatusCode = 400,
                    ResponseHeaders = "Headers",
                    CreatedDateTimeUtc = now.AddSeconds(5),
                    CreatedByUserName = Environment.UserName
                },
                new GlutResultItem
                {
                    GlutProjectName = "Test",
                    GlutProjectRunId = 1,
                    StartDateTimeUtc = now,
                    EndDateTimeUtc = now.AddSeconds(1),
                    Url = "/serverError",
                    IsSuccessStatusCode = true,
                    StatusCode = 500,
                    ResponseHeaders = null,
                    Exception = new Exception("some error").ToString(),
                    CreatedDateTimeUtc = now.AddSeconds(6),
                    CreatedByUserName = Environment.UserName
                }
            );
            context.SaveChanges();

            return this;
        }

        public SeedData WithRunAttributes(EfDbContext context)
        {
            if (context.RunAttributes.Any())
            {
                return this;
            }

            var createdDate = DateTime.UtcNow;
            var createdBy = Environment.UserName;

            context.RunAttributes.AddRange(
                 new GlutRunAttribute
                 {
                     GlutProjectName = "Test",
                     GlutProjectRunId = 1,
                     AttributeName = "Threads",
                     AttributeValue = "5",
                     CreatedDateTimeUtc = createdDate,
                     CreatedByUserName = createdBy
                 },
                 new GlutRunAttribute
                 {
                     GlutProjectName = "Test",
                     GlutProjectRunId = 1,
                     AttributeName = "Duration",
                     AttributeValue = "1000",
                     CreatedDateTimeUtc = createdDate,
                     CreatedByUserName = createdBy
                 },
                new GlutRunAttribute
                {
                    GlutProjectName = "Test",
                    GlutProjectRunId = 2,
                    AttributeName = "Threads",
                    AttributeValue = "5",
                    CreatedDateTimeUtc = createdDate.AddMinutes(1),
                    CreatedByUserName = createdBy
                },
                 new GlutRunAttribute
                 {
                     GlutProjectName = "Test",
                     GlutProjectRunId = 2,
                     AttributeName = "Duration",
                     AttributeValue = "10000",
                     CreatedDateTimeUtc = createdDate.AddMinutes(1),
                     CreatedByUserName = createdBy
                 });

            context.SaveChanges();

            return this;
        }

        public SeedData WithLineChartData(EfDbContext context)
        {
            // Look for any movies.
            if (context.Results.Any())
            {
                return this;   // DB has been seeded
            }

            var start = new DateTime(2019, 09, 24, 1, 0, 0);

            context.Results.AddRange(
                new GlutResultItem
                {
                    GlutProjectName = "Test",
                    GlutProjectRunId = 1,
                    StartDateTimeUtc = start,
                    EndDateTimeUtc = start.AddMilliseconds(500),
                    Url = "/information01",
                    IsSuccessStatusCode = true,
                    StatusCode = 100,
                    TotalTicks = 10000,
                    TotalLength = 1000,
                    CreatedDateTimeUtc = DateTime.UtcNow,
                    CreatedByUserName = Environment.UserName
                },
                new GlutResultItem
                {
                    GlutProjectName = "Test",
                    GlutProjectRunId = 1,
                    StartDateTimeUtc = start,
                    EndDateTimeUtc = start.AddMilliseconds(500),
                    Url = "/successful01",
                    IsSuccessStatusCode = true,
                    StatusCode = 200,
                    TotalTicks = 10000,
                    TotalLength = 1000,
                    CreatedDateTimeUtc = DateTime.UtcNow,
                    CreatedByUserName = Environment.UserName
                },
                 new GlutResultItem
                 {
                     GlutProjectName = "Test",
                     GlutProjectRunId = 1,
                     StartDateTimeUtc = start,
                     EndDateTimeUtc = start.AddMilliseconds(600),
                     Url = "/successful02",
                     IsSuccessStatusCode = true,
                     StatusCode = 200,
                     TotalTicks = 10000,
                     TotalLength = 1000,
                     CreatedDateTimeUtc = DateTime.UtcNow,
                     CreatedByUserName = Environment.UserName
                 },
                 new GlutResultItem
                 {
                     GlutProjectName = "Test",
                     GlutProjectRunId = 1,
                     StartDateTimeUtc = start,
                     EndDateTimeUtc = start.AddMilliseconds(1000),
                     Url = "/successful03",
                     IsSuccessStatusCode = true,
                     StatusCode = 200,
                     TotalTicks = 20000,
                     TotalLength = 2000,
                     CreatedDateTimeUtc = DateTime.UtcNow,
                     CreatedByUserName = Environment.UserName
                 },
                  new GlutResultItem
                  {
                      GlutProjectName = "Test",
                      GlutProjectRunId = 1,
                      StartDateTimeUtc = start,
                      EndDateTimeUtc = start.AddMilliseconds(1500),
                      Url = "/redirection01",
                      IsSuccessStatusCode = true,
                      StatusCode = 300,
                      TotalTicks = 10000,
                      TotalLength = 1000,
                      CreatedDateTimeUtc = DateTime.UtcNow,
                      CreatedByUserName = Environment.UserName
                  },
                new GlutResultItem
                {
                    GlutProjectName = "Test",
                    GlutProjectRunId = 1,
                    StartDateTimeUtc = start,
                    EndDateTimeUtc = start.AddMilliseconds(200),
                    Url = "/clientError01",
                    IsSuccessStatusCode = false,
                    StatusCode = 400,
                    CreatedDateTimeUtc = DateTime.UtcNow,
                    CreatedByUserName = Environment.UserName
                },
                new GlutResultItem
                {
                    GlutProjectName = "Test",
                    GlutProjectRunId = 1,
                    StartDateTimeUtc = start,
                    EndDateTimeUtc = start.AddMilliseconds(1500),
                    Url = "/serverError01",
                    IsSuccessStatusCode = false,
                    StatusCode = 500,
                    Exception = new Exception("some error").ToString(),
                    CreatedDateTimeUtc = DateTime.UtcNow,
                    CreatedByUserName = Environment.UserName
                }
            );
            context.SaveChanges();

            return this;
        }
    }
}
