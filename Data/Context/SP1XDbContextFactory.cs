using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data.Context
{
    public class SP1XDbContextFactory : IDesignTimeDbContextFactory<SP1XDbContext>
    {
        /// <inheritdoc/>
        public SP1XDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder()
                .UseMySql(
                    configuration["Database"],
                    new MySqlServerVersion(new Version(8, 0, 21)));

            return new SP1XDbContext(optionsBuilder.Options);
        }
    }
}
