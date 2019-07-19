using Glut.Data;
using GlutSvr.Properties;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GlutSvr.Services
{
    public class EfDataStoreSvrTest
    {
        [Fact]
        public void GetProjectsTest()
        {
            //var store = new EfDataStoreSvr
        }

        [Fact]
        public async Task GetResponseDetailTest()
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
                    SeedData.Initialize(context);
                    var service = new EfDataStoreSvr(context);

                    var r = await service.GetResponseDetails("Test", 1);

                    Assert.Equal(6, r.Count);
                    Assert.Equal(5, r[AppResources.TotalRequests]);
                    Assert.Equal(20.0M, r[AppResources.Information]);
                    Assert.Equal(20.0M, r[AppResources.Successful]);
                    Assert.Equal(20.0M, r[AppResources.Redirection]);
                    Assert.Equal(20.0M, r[AppResources.ClientError]);
                    Assert.Equal(20.0M, r[AppResources.ServerError]);
                }
            }
            finally
            {
                connection.Close();
            }

        }

        [Fact]
        public async Task GetStatusCodePieDataTest()
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
                    SeedData.Initialize(context);
                    var service = new EfDataStoreSvr(context);

                    var results = await service.GetStatusCodePieData("Test", 1);

                    Assert.Equal(5, results.Count());
                    Assert.Equal(100, results.ElementAt(0).StatusCode);
                    Assert.Equal(1, results.ElementAt(0).StatusCodeItems);
                    Assert.Equal(20.0M, results.ElementAt(0).StatusCodePercent);
                    Assert.Equal(5, results.ElementAt(0).TotalItems);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task GetResultItemsTest()
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
                    SeedData.Initialize(context);
                    var service = new EfDataStoreSvr(context);

                    var results = await service.GetResultItems("Test", 1);

                    Assert.Equal(5, results.Count());
                    Assert.Equal("/information", results.ElementAt(0).Url);
                    Assert.True(results.ElementAt(0).IsSuccessStatusCode);
                    Assert.Equal(100, results.ElementAt(0).StatusCode);
                    Assert.Equal(1, results.ElementAt(0).HeaderLength);
                    Assert.Equal(2, results.ElementAt(0).ResponseLength);
                    Assert.Equal(3, results.ElementAt(0).TotalLength);
                    Assert.Equal(1, results.ElementAt(0).RequestTicks);
                    Assert.Equal(2, results.ElementAt(0).ResponseTicks);
                    Assert.Equal(3, results.ElementAt(0).TotalTicks);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Add_writes_to_database()
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
                }

                // Run the test against one instance of the context
                using (var context = new EfDbContext(options))
                {
                    var service = new EfDataStoreSvr(context);
                    //service.Add("http://sample.com");
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new EfDbContext(options))
                {
                    //Assert.Equal(1, context.Blogs.Count());
                    //Assert.Equal("http://sample.com", context.Blogs.Single().Url);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
