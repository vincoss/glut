using Glut.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace GlutSvr
{
    public static class SeedData
    {
        public static void Initialize(EfDbContext context)
        {
            // Look for any movies.
            if (context.Results.Any())
            {
                return;   // DB has been seeded
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
                     HeaderLength = 1,
                     ResponseLength = 2,
                     TotalLegth = 3,
                     RequestSentTicks = 1,
                     ResponseTicks = 2,
                     TotalTicks = 3,
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
        }
    }
}
