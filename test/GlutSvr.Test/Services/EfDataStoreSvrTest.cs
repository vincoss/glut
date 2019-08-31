using Glut.Data;
using GlutSvrWeb.Properties;
using GlutSvrWeb.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace GlutSvr.Services
{
    public class EfDataStoreSvrTest
    {
        [Fact]
        public async Task GetProjectString()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<EfDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new EfDbContext(options))
                {
                    context.Database.EnsureCreated();
                    new SeedData().Initialize(context);
                    var service = new EfDataStoreSvr(context);

                    var r = await service.GetProjectString();

                    Assert.Equal(2, r.Count());
                    Assert.Equal("Gabo", r.ElementAt(0));
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task GetProjectRuns()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<EfDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new EfDbContext(options))
                {
                    context.Database.EnsureCreated();
                    new SeedData().WithRunAttributes(context);
                    var service = new EfDataStoreSvr(context);

                    var r = await service.GetProjectRuns("Test");

                    Assert.Equal(2, r.Count());
                    Assert.Equal(2, r.ElementAt(0));
                    Assert.Equal(1, r.ElementAt(1));
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task GetResponseDetails()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<EfDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new EfDbContext(options))
                {
                    context.Database.EnsureCreated();
                    new SeedData().Initialize(context);
                    var service = new EfDataStoreSvr(context);

                    var r = await service.GetResponseDetails("Test", 1);

                    Assert.Equal(6, r.Count);
                    Assert.Equal(6, Math.Round(r[AppResources.TotalRequests]));
                    Assert.Equal(17.0M, Math.Round(r[AppResources.Information]));
                    Assert.Equal(33.0M, Math.Round(r[AppResources.Successful]));
                    Assert.Equal(17.0M, Math.Round(r[AppResources.Redirection]));
                    Assert.Equal(17.0M, Math.Round(r[AppResources.ClientError]));
                    Assert.Equal(17.0M, Math.Round(r[AppResources.ServerError]));
                }
            }
            finally
            {
                connection.Close();
            }

        }

        [Fact]
        public async Task GetStatusCodePieData()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<EfDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new EfDbContext(options))
                {
                    context.Database.EnsureCreated();
                    new SeedData().Initialize(context);
                    var service = new EfDataStoreSvr(context);

                    var results = await service.GetStatusCodePieData("Test", 1);

                    Assert.Equal(5, results.Count());
                    Assert.Equal(100, results.ElementAt(0).StatusCode);
                    Assert.Equal(1, results.ElementAt(0).StatusCodeItems);
                    Assert.Equal(16.0M, results.ElementAt(0).StatusCodePercent);
                    Assert.Equal(6, results.ElementAt(0).TotalItems);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task GetTopSuccessRequests()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<EfDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new EfDbContext(options))
                {
                    context.Database.EnsureCreated();
                    new SeedData().Initialize(context);
                    var service = new EfDataStoreSvr(context);

                    var results = await service.GetTopSuccessRequests("Test", 1);

                    Assert.Equal(2, results.Count());
                    Assert.Equal("/successful", results.ElementAt(0).Url);
                    Assert.Equal(1, results.ElementAt(0).Count);
                    Assert.Equal(50, results.ElementAt(0).Frequency);
                    Assert.Equal(2, results.ElementAt(0).TotalItems);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task GetTopErrorRequests()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<EfDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new EfDbContext(options))
                {
                    context.Database.EnsureCreated();
                    new SeedData().Initialize(context);
                    var service = new EfDataStoreSvr(context);

                    var results = await service.GetTopErrorRequests("Test", 1);

                    Assert.Equal(2, results.Count());
                    Assert.Equal("/clientError", results.ElementAt(0).Url);
                    Assert.Equal(1, results.ElementAt(0).Count);
                    Assert.Equal(50, results.ElementAt(0).Frequency);
                    Assert.Equal(2, results.ElementAt(0).TotalItems);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task GetFastestSuccessRequests()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<EfDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new EfDbContext(options))
                {
                    context.Database.EnsureCreated();
                    new SeedData().Initialize(context);
                    var service = new EfDataStoreSvr(context);

                    var results = await service.GetFastestSuccessRequests("Test", 1);

                    Assert.Equal(2, results.Count());
                    Assert.Equal("/successful", results.ElementAt(0).Key);
                    Assert.Equal(10000, results.ElementAt(0).Value);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task GetSlowestSuccessRequests()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<EfDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new EfDbContext(options))
                {
                    context.Database.EnsureCreated();
                    new SeedData().Initialize(context);
                    var service = new EfDataStoreSvr(context);

                    var results = await service.GetSlowestSuccessRequests("Test", 1);

                    Assert.Equal(2, results.Count());
                    Assert.Equal("/successful0001", results.ElementAt(0).Key);
                    Assert.Equal(20000, results.ElementAt(0).Value);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task GetLargestSuccessRequests()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<EfDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new EfDbContext(options))
                {
                    context.Database.EnsureCreated();
                    new SeedData().Initialize(context);
                    var service = new EfDataStoreSvr(context);

                    var results = await service.GetLargestSuccessRequests("Test", 1);

                    Assert.Equal(2, results.Count());
                    Assert.Equal("/successful0001", results.ElementAt(0).Key);
                    Assert.Equal(2000, results.ElementAt(0).Value);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task GetLineChartRequests()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<EfDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new EfDbContext(options))
                {
                    context.Database.EnsureCreated();
                    new SeedData().WithLineChartData(context);
                    var service = new EfDataStoreSvr(context);

                    var results = await service.GetLineChartRequests("Test", 1);

                    Assert.Equal(4, results.Count());
                    Assert.Equal("Error", results.ElementAt(0).SeriesString);
                    Assert.Equal("Success", results.ElementAt(1).SeriesString);
                    Assert.Equal("Error", results.ElementAt(2).SeriesString);
                    Assert.Equal("Success", results.ElementAt(3).SeriesString);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact] // TODO: async?
        public void GetResultItems()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<EfDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new EfDbContext(options))
                {
                    context.Database.EnsureCreated();
                    new SeedData().WithResults(context);
                    var service = new EfDataStoreSvr(context);

                    var results = service.GetResultItems("Test", 1);

                    Assert.Equal(6, results.Count());
                    Assert.Equal("/information", results.ElementAt(0).Url);
                    Assert.True(results.ElementAt(0).IsSuccessStatusCode);
                    Assert.Equal(100, results.ElementAt(0).StatusCode);
                    Assert.Equal(0.9765625M, results.ElementAt(0).HeaderLength);
                    Assert.Equal(1.953125M, results.ElementAt(0).ResponseLength);
                    Assert.Equal(2.9296875M, results.ElementAt(0).TotalLength);
                    Assert.Equal(1, results.ElementAt(0).RequestTicks);
                    Assert.Equal(2, results.ElementAt(0).ResponseTicks);
                    Assert.Equal(3, results.ElementAt(0).TotalTicks);
                    Assert.Equal("StatusCode: 200", results.ElementAt(0).ResponseHeaders);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        //[Fact] TODO:
        //public async Task GetProjects()
        //{
        //    // In-memory database only exists while the connection is open
        //    var connection = new SqliteConnection("DataSource=:memory:");
        //    connection.Open();

        //    try
        //    {
        //        var options = new DbContextOptionsBuilder<EfDbContext>()
        //            .UseSqlite(connection)
        //            .Options;

        //        // Create the schema in the database
        //        using (var context = new EfDbContext(options))
        //        {
        //            context.Database.EnsureCreated();
        //            new SeedData().WithProjects(context).WithRunAttributes(context);
        //            var service = new EfDataStoreSvr(context);

        //            var results = await service.GetProjects();

        //            Assert.Equal(2, results.Count());
        //            Assert.Equal("Gabo", results.ElementAt(0).ProjectName);
        //            Assert.Equal(0, results.ElementAt(0).Runs);
        //            Assert.NotNull(results.ElementAt(0).LastChangeDateTime);
        //        }
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //}

        [Fact]
        public async Task GetRunInfo()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<EfDbContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new EfDbContext(options))
                {
                    context.Database.EnsureCreated();
                    new SeedData().WithRunAttributes(context);
                    var service = new EfDataStoreSvr(context);

                    var results = await service.GetRunInfo("Test", 2);

                    Assert.Equal(2, results.Count());
                    Assert.Equal("Duration", results.ElementAt(0).Key);
                    Assert.Equal("10000", results.ElementAt(0).Value);
                    Assert.Equal("Threads", results.ElementAt(1).Key);
                    Assert.Equal("5", results.ElementAt(1).Value);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
