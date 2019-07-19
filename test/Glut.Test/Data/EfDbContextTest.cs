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
    }
}
