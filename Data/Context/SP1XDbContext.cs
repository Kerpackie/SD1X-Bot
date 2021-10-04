using Data.Models;
using Microsoft.EntityFrameworkCore;

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
       //public DbSet<Birthday> Birthdays { get; set; }
    }
}
