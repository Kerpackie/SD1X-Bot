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

        public DbSet<ServerModel> Servers { get; set; }
        public DbSet<HTMLTagModel> HTMLTags { get; set; }
        public DbSet<CSSTagModel> CSSTags { get; set; }
        public DbSet<CSharpTagModel> CSharpTags { get; set; }
    }
}
