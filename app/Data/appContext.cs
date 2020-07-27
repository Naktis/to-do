using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using app.Models;

namespace app.Data
{
    public class appContext : DbContext
    {
        public appContext (DbContextOptions<appContext> options)
            : base(options)
        {
        }

        public DbSet<app.Models.Category> Categories { get; set; }
    }
}
