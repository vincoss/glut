using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Glut.Data
{
    public class EfDbContextTest
    {
        [Fact]
        public void Test()
        {
            var options = new DbContextOptions<EfDbContext>();
            var context = new EfDbContext(options);
        }

        [Fact]
        public void Te()
        {
            string id = "";
            int run = 1;

            if (string.IsNullOrWhiteSpace(id) || run <= 0)
            {
                return;
            }
            Assert.False(true);

        }
    }
}
