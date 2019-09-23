using Glut.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Glut
{
    public class GlutSeedData
    {
        public void Initialize(EfDbContext context)
        {
            WithProjects(context);
            WithResults(context);
            WithRunAttributes(context);
        }

        public GlutSeedData WithProjects(EfDbContext context)
        {
            // Look for any movies.
            if (context.Results.Any())
            {
                return this;   // DB has been seeded
            }

            var createdDate = DateTime.UtcNow;

            context.Projects.AddRange(new GlutProject
            {
                GlutProjectName = "Glut",
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

        public GlutSeedData WithResults(EfDbContext context)
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
                     CreatedDateTimeUtc = DateTime.UtcNow,
                     CreatedByUserName = Environment.UserName
                 },
                new GlutResultItem
                {
                    GlutProjectName = "Test",
                    GlutProjectRunId = 1,
                    StartDateTimeUtc = DateTime.UtcNow,
                    EndDateTimeUtc = DateTime.UtcNow.AddSeconds(1),
                    Url = "/successful",
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
                     StartDateTimeUtc = DateTime.UtcNow,
                     EndDateTimeUtc = DateTime.UtcNow.AddSeconds(1),
                     Url = "/successful0001",
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
                    StartDateTimeUtc = DateTime.UtcNow,
                    EndDateTimeUtc = DateTime.UtcNow.AddSeconds(1),
                    Url = "/redirection",
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
                    Url = "/clientError",
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
                    Url = "/serverError",
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

        public GlutSeedData WithRunAttributes(EfDbContext context)
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

    }
}
