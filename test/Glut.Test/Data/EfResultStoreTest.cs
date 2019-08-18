using Glut.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ApprovalTests.Reporters;
using ApprovalTests;
using Newtonsoft.Json;
using Moq;
using Glut.Interface;

namespace Glut.Data
{
    public class EfResultStoreTest
    {
        [Theory]
        [InlineData("None", 0)]
        [InlineData("Test", 1)]
        public void GetProjectLastRunIdTest(string projectName, int expected)
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
                    var service = new EfResultStore(context, new EnvironmentService());

                    var actual = service.GetProjectLastRunId(projectName);

                    Assert.Equal(expected, actual);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void AddTest()
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
                    var environment = new Mock<IEnvironment>();
                    environment.Setup(x => x.SystemDateTimeUtc).Returns(new DateTime(2019, 8, 1));
                    environment.Setup(x => x.UserName).Returns("TestUser");

                    context.Database.EnsureCreated();
                    var service = new EfResultStore(context, environment.Object);

                    var projectName = "TestProject";
                    var attributes = new Dictionary<string, string>();
                    attributes.Add(nameof(projectName), projectName);

                    var start = new DateTime(2019, 8, 1);
                    var result = new ThreadResult();
                    result.Add("/", start, start.AddSeconds(1), true, 200, 100, 200, 300, 400, "Headers", new Exception("Test"));

                    service.Add(projectName, -1, attributes, result);

                    var projects = context.Projects.ToArray();
                    var runAttributes = context.RunAttributes.ToArray();
                    var results = context.Results.ToArray();

                    var sb = new StringBuilder();
                    sb.Append(JsonConvert.SerializeObject(projects));
                    sb.AppendLine();
                    sb.Append(JsonConvert.SerializeObject(runAttributes));
                    sb.AppendLine();
                    sb.Append(JsonConvert.SerializeObject(results));

                    Approvals.Verify(sb.ToString());
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
