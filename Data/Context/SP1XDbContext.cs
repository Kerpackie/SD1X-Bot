using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data.Context
{
    public class SP1XDbContext : DbContext
    {
        public SP1XDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Server> Servers { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<HTMLTag> HTMLTags { get; set; }
        public DbSet<CSSTag> CSSTags { get; set; }
        public DbSet<CSharpTag> CSharpTags { get; set; }
    }
}
