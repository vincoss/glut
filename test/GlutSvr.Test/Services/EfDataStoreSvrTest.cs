using Glut.Data;
using GlutSvrWeb.Dto;
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
        #region Forms

        [Fact]
        public void GetLastProject()
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

                    var r = service.GetLastProject();

                    Assert.Equal("Test", r.ProjectName);
                    Assert.Equal(2, r.RunId);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void GetLastProject_ShallReturnEmptyInstanceIfNotExistsYet()
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
                    var service = new EfDataStoreSvr(context);

                    var result = service.GetLastProject();

                    Assert.NotNull(result);
                }
            }
            finally
            {
                connection.Close();
            }
        }

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
                    new SeedData().WithProjects(context);
                    var service = new EfDataStoreSvr(context);

                    var r = await service.GetProjectString();

                    Assert.Equal(2, r.Count());
                    Assert.Equal("Gabo", r.ElementAt(0));
                    Assert.Equal("Test", r.ElementAt(1));
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
        public void GetProjects()
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
                    new SeedData().WithProjects(context).WithRunAttributes(context);
                    var service = new EfDataStoreSvr(context);

                    var results = service.GetProjects(new DataTableParameter());

                    Assert.Equal(2, results.Data.Count());
                    Assert.Equal("Gabo", results.Data.ElementAt(0).ProjectName);
                    Assert.Equal(0, results.Data.ElementAt(0).Runs);
                    Assert.NotNull(results.Data.ElementAt(0).LastChangeDateTime);

                    Assert.Equal("Test", results.Data.ElementAt(1).ProjectName);
                    Assert.Equal(2, results.Data.ElementAt(1).Runs);
                    Assert.NotNull(results.Data.ElementAt(1).LastChangeDateTime);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void GetProjects_CanSortByEveryColumn()
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
                    new SeedData().WithProjects(context).WithRunAttributes(context);
                    var service = new EfDataStoreSvr(context);

                    var columnNames = from x in typeof(ProjectDto).GetProperties()
                                      select x.Name;

                    foreach (var c in columnNames)
                    {
                        var args = new DataTableParameter
                        {
                            SortColumn = c.ToLower()
                        };

                        var results = service.GetProjects(args);

                        Assert.Equal(2, results.Data.Count());
                    }
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
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

                    var args = new DataTableParameter()
                    {
                        Search = null
                    };
                    var results = service.GetResultItems("Test", 1, args);

                    Assert.Equal(7, results.Data.Count());
                    Assert.Equal("/information", results.Data.ElementAt(0).Url);
                    Assert.True(results.Data.ElementAt(0).IsSuccessStatusCode);
                    Assert.Equal(100, results.Data.ElementAt(0).StatusCode);
                    Assert.Equal(0.9765625M, results.Data.ElementAt(0).HeaderLength);
                    Assert.Equal(1.953125M, results.Data.ElementAt(0).ResponseLength);
                    Assert.Equal(2.9296875M, results.Data.ElementAt(0).TotalLength);
                    Assert.Equal(1, results.Data.ElementAt(0).RequestTicks);
                    Assert.Equal(2, results.Data.ElementAt(0).ResponseTicks);
                    Assert.Equal(3, results.Data.ElementAt(0).TotalTicks);
                    Assert.Equal("StatusCode: 200", results.Data.ElementAt(0).ResponseHeaders);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void GetResultItems_CanSortByEveryColumn()
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

                    var columnNames = from x in typeof(ResultItemDto).GetProperties()
                                      select x.Name;

                    foreach (var c in columnNames)
                    {
                        var args = new DataTableParameter
                        {
                            SortColumn = c
                        };

                        var results = service.GetResultItems("Test", 1, args);

                        Assert.Equal(7, results.Data.Count());
                    }
                }
            }
            finally
            {
                connection.Close();
            }
        } 

        #endregion

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

                    Assert.NotNull(r);
                    Assert.Equal(7, r.TotalRequests);
                    Assert.Equal(14.29M, Math.Round(r.Information, 2));
                    Assert.Equal(42.86M, Math.Round(r.Successful, 2));
                    Assert.Equal(14.29M, Math.Round(r.Redirection, 2));
                    Assert.Equal(14.29M, Math.Round(r.ClientError, 2));
                    Assert.Equal(14.29M, Math.Round(r.ServerError, 2));
                }
            }
            finally
            {
                connection.Close();
            }

        }

        [Fact]
        public async Task GetRunAttributes()
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

                    var results = await service.GetRunAttributes("Test", 2);

                    Assert.Equal(2, results.Count());
                    Assert.Equal("Threads", results.ElementAt(0).Key);
                    Assert.Equal("5", results.ElementAt(0).Value);
                    Assert.Equal("Duration", results.ElementAt(1).Key);
                    Assert.Equal("10000", results.ElementAt(1).Value);
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
                    // Information
                    Assert.Equal(1, results.ElementAt(0).StatusCode);
                    Assert.Equal(1, results.ElementAt(0).StatusCodeItems);
                    Assert.Equal(14.29M, Math.Round(results.ElementAt(0).StatusCodePercent, 2));
                    Assert.Equal(7, results.ElementAt(0).TotalItems);
                    // Successful
                    Assert.Equal(2, results.ElementAt(1).StatusCode);
                    Assert.Equal(3, results.ElementAt(1).StatusCodeItems);
                    Assert.Equal(42.86M, Math.Round(results.ElementAt(1).StatusCodePercent, 2));
                    Assert.Equal(7, results.ElementAt(1).TotalItems);
                    // Redirection
                    Assert.Equal(3, results.ElementAt(2).StatusCode);
                    Assert.Equal(1, results.ElementAt(2).StatusCodeItems);
                    Assert.Equal(14.29M, Math.Round(results.ElementAt(2).StatusCodePercent, 2));
                    Assert.Equal(7, results.ElementAt(2).TotalItems);
                    // ClientError
                    Assert.Equal(4, results.ElementAt(3).StatusCode);
                    Assert.Equal(1, results.ElementAt(3).StatusCodeItems);
                    Assert.Equal(14.29M, Math.Round(results.ElementAt(3).StatusCodePercent, 2));
                    Assert.Equal(7, results.ElementAt(3).TotalItems);
                    // ServerError
                    Assert.Equal(5, results.ElementAt(4).StatusCode);
                    Assert.Equal(1, results.ElementAt(4).StatusCodeItems);
                    Assert.Equal(14.29M, Math.Round(results.ElementAt(4).StatusCodePercent, 2));
                    Assert.Equal(7, results.ElementAt(4).TotalItems);
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
                    // 0
                    Assert.Equal("/successful", results.ElementAt(0).Url);
                    Assert.Equal(2, results.ElementAt(0).Count);
                    Assert.Equal(66.67M, Math.Round(results.ElementAt(0).Frequency, 2));
                    Assert.Equal(3, results.ElementAt(0).TotalItems);
                    // 1
                    Assert.Equal("/successful0001", results.ElementAt(1).Url);
                    Assert.Equal(1, results.ElementAt(1).Count);
                    Assert.Equal(33.33M, Math.Round(results.ElementAt(1).Frequency, 2));
                    Assert.Equal(3, results.ElementAt(1).TotalItems);
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
                    // 0
                    Assert.Equal("/clientError", results.ElementAt(0).Url);
                    Assert.Equal(1, results.ElementAt(0).Count);
                    Assert.Equal(50, results.ElementAt(0).Frequency);
                    Assert.Equal(2, results.ElementAt(0).TotalItems);
                    // 1
                    Assert.Equal("/serverError", results.ElementAt(1).Url);
                    Assert.Equal(1, results.ElementAt(1).Count);
                    Assert.Equal(50, results.ElementAt(1).Frequency);
                    Assert.Equal(2, results.ElementAt(1).TotalItems);
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
                    // 0
                    Assert.Equal("/successful", results.ElementAt(0).Url);
                    Assert.Equal(1, results.ElementAt(0).Min);
                    Assert.Equal(3, results.ElementAt(0).Max);
                    Assert.Equal(2, results.ElementAt(0).Avg);
                    // 1
                    Assert.Equal("/successful0001", results.ElementAt(1).Url);
                    Assert.Equal(2, results.ElementAt(1).Min);
                    Assert.Equal(2, results.ElementAt(1).Max);
                    Assert.Equal(2, results.ElementAt(1).Avg);
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
                    // 0
                    Assert.Equal("/successful", results.ElementAt(0).Url);
                    Assert.Equal(3, results.ElementAt(0).Max);
                    Assert.Equal(1, results.ElementAt(0).Min);
                    Assert.Equal(2, results.ElementAt(0).Avg);
                    // 1
                    Assert.Equal("/successful0001", results.ElementAt(1).Url);
                    Assert.Equal(2, results.ElementAt(1).Max);
                    Assert.Equal(2, results.ElementAt(1).Min);
                    Assert.Equal(2, results.ElementAt(1).Avg);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task GetAvgSuccessRequests()
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

                    var results = await service.GetAvgSuccessRequests("Test", 1);

                    Assert.Equal(2, results.Count());
                    // 0
                    Assert.Equal("/successful", results.ElementAt(0).Url);
                    Assert.Equal(1, results.ElementAt(0).Min);
                    Assert.Equal(3, results.ElementAt(0).Max);
                    Assert.Equal(2, results.ElementAt(0).Avg);
                    // 1
                    Assert.Equal("/successful0001", results.ElementAt(1).Url);
                    Assert.Equal(2, results.ElementAt(1).Min);
                    Assert.Equal(2, results.ElementAt(1).Max);
                    Assert.Equal(2, results.ElementAt(1).Avg);
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
                    // 0
                    Assert.Equal("/successful0001", results.ElementAt(0).Url);
                    Assert.Equal(2, Math.Round(results.ElementAt(0).Length));
                    Assert.Equal(50M, Math.Round(results.ElementAt(0).Percent, 2));
                    Assert.Equal(3, results.ElementAt(0).TotalLength);
                    // 1
                    Assert.Equal("/successful", results.ElementAt(1).Url);
                    Assert.Equal(1, Math.Round(results.ElementAt(1).Length));
                    Assert.Equal(25, Math.Round(results.ElementAt(1).Percent, 2));
                    Assert.Equal(3, results.ElementAt(1).TotalLength);
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

                    Assert.Equal(6, results.Count());
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

    }
}
