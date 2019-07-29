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

            context.Projects.AddRange(new GlutProject
            {
                GlutProjectName = "Test",
                CreatedDateTimeUtc = DateTime.UtcNow,
                CreatedByUserName = Environment.UserName
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

            context.Results.AddRange(
                 new GlutResultItem
                 {
                     GlutProjectName = "Test",
                     GlutProjectRunId = 1,
                     StartDateTimeUtc = DateTime.UtcNow,
                     EndDateTimeUtc = DateTime.UtcNow.AddSeconds(1),
                     RequestUri = "/information",
                     IsSuccessStatusCode = true,
                     StatusCode = 100,
                     HeaderLength = 1000,
                     ResponseLength = 2000,
                     TotalLegth = 3000,
                     RequestSentTicks = 10000,
                     ResponseTicks = 20000,
                     TotalTicks = 30000,
                     ResponseHeaders = "StatusCode: 200",
                     CreatedDateTimeUtc = DateTime.UtcNow,
                     CreatedByUserName = Environment.UserName
                 },
                new GlutResultItem
                {
                    GlutProjectName = "Test",
                    GlutProjectRunId = 1,
                    StartDateTimeUtc = DateTime.UtcNow,
                    EndDateTimeUtc = DateTime.UtcNow.AddSeconds(1),
                    RequestUri = "/successful",
                    IsSuccessStatusCode = true,
                    StatusCode = 200,
                    TotalTicks = 10000,
                    TotalLegth = 1000,
                    CreatedDateTimeUtc = DateTime.UtcNow,
                    CreatedByUserName = Environment.UserName
                },
                 new GlutResultItem
                 {
                     GlutProjectName = "Test",
                     GlutProjectRunId = 1,
                     StartDateTimeUtc = DateTime.UtcNow,
                     EndDateTimeUtc = DateTime.UtcNow.AddSeconds(1),
                     RequestUri = "/successful0001",
                     IsSuccessStatusCode = true,
                     StatusCode = 200,
                     TotalTicks = 20000,
                     TotalLegth = 2000,
                     CreatedDateTimeUtc = DateTime.UtcNow,
                     CreatedByUserName = Environment.UserName
                 },
                new GlutResultItem
                {
                    GlutProjectName = "Test",
                    GlutProjectRunId = 1,
                    StartDateTimeUtc = DateTime.UtcNow,
                    EndDateTimeUtc = DateTime.UtcNow.AddSeconds(1),
                    RequestUri = "/redirection",
                    IsSuccessStatusCode = true,
                    StatusCode = 300,
                    CreatedDateTimeUtc = DateTime.UtcNow,
                    CreatedByUserName = Environment.UserName
                },
                new GlutResultItem
                {
                    GlutProjectName = "Test",
                    GlutProjectRunId = 1,
                    StartDateTimeUtc = DateTime.UtcNow,
                    EndDateTimeUtc = DateTime.UtcNow.AddSeconds(1),
                    RequestUri = "/clientError",
                    IsSuccessStatusCode = true,
                    StatusCode = 400,
                    CreatedDateTimeUtc = DateTime.UtcNow,
                    CreatedByUserName = Environment.UserName
                },
                new GlutResultItem
                {
                    GlutProjectName = "Test",
                    GlutProjectRunId = 1,
                    StartDateTimeUtc = DateTime.UtcNow,
                    EndDateTimeUtc = DateTime.UtcNow.AddSeconds(1),
                    RequestUri = "/serverError",
                    IsSuccessStatusCode = true,
                    StatusCode = 500,
                    Exception = new Exception("some error").ToString(),
                    CreatedDateTimeUtc = DateTime.UtcNow,
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
            
            context.RunAttributes.AddRange(
                 new GlutRunAttribute
                 {
                     GlutProjectName = "Test",
                     GlutProjectRunId = 1,
                     AttributeName = "Threads",
                     AttributeValue = "5",
                     CreatedDateTimeUtc = DateTime.UtcNow,
                     CreatedByUserName = Environment.UserName
                 },
                 new GlutRunAttribute
                 {
                     GlutProjectName = "Test",
                     GlutProjectRunId = 1,
                     AttributeName = "Duration",
                     AttributeValue = "1000",
                     CreatedDateTimeUtc = DateTime.UtcNow,
                     CreatedByUserName = Environment.UserName
                 },
                new GlutRunAttribute
                {
                    GlutProjectName = "Test",
                    GlutProjectRunId = 2,
                    AttributeName = "Threads",
                    AttributeValue = "5",
                    CreatedDateTimeUtc = DateTime.UtcNow,
                    CreatedByUserName = Environment.UserName
                },
                 new GlutRunAttribute
                 {
                     GlutProjectName = "Test",
                     GlutProjectRunId = 2,
                     AttributeName = "Duration",
                     AttributeValue = "10000",
                     CreatedDateTimeUtc = DateTime.UtcNow,
                     CreatedByUserName = Environment.UserName
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

            var start = new DateTime(2000, 01, 01, 1, 0, 0);

            context.Results.AddRange(
                new GlutResultItem
                {
                    GlutProjectName = "Test",
                    GlutProjectRunId = 1,
                    StartDateTimeUtc = start,
                    EndDateTimeUtc = start.AddMilliseconds(500),
                    RequestUri = "/successful01",
                    IsSuccessStatusCode = true,
                    StatusCode = 200,
                    TotalTicks = 10000,
                    TotalLegth = 1000,
                    CreatedDateTimeUtc = DateTime.UtcNow,
                    CreatedByUserName = Environment.UserName
                },
                 new GlutResultItem
                 {
                     GlutProjectName = "Test",
                     GlutProjectRunId = 1,
                     StartDateTimeUtc = start,
                     EndDateTimeUtc = start.AddMilliseconds(600),
                     RequestUri = "/successful02",
                     IsSuccessStatusCode = true,
                     StatusCode = 200,
                     TotalTicks = 10000,
                     TotalLegth = 1000,
                     CreatedDateTimeUtc = DateTime.UtcNow,
                     CreatedByUserName = Environment.UserName
                 },
                 new GlutResultItem
                 {
                     GlutProjectName = "Test",
                     GlutProjectRunId = 1,
                     StartDateTimeUtc = start,
                     EndDateTimeUtc = start.AddMilliseconds(1000),
                     RequestUri = "/successful03",
                     IsSuccessStatusCode = true,
                     StatusCode = 200,
                     TotalTicks = 20000,
                     TotalLegth = 2000,
                     CreatedDateTimeUtc = DateTime.UtcNow,
                     CreatedByUserName = Environment.UserName
                 },
                new GlutResultItem
                {
                    GlutProjectName = "Test",
                    GlutProjectRunId = 1,
                    StartDateTimeUtc = start,
                    EndDateTimeUtc = start.AddMilliseconds(200),
                    RequestUri = "/clientError01",
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
                    RequestUri = "/serverError01",
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
