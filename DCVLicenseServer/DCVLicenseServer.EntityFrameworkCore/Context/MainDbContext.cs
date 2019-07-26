using DCVLicenseServer.Core;
using DCVLicenseServer.Core.APILicenses;
using DCVLicenseServer.Core.ProductLicense;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Sbp.EntityFrameworkCore.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DCVLicenseServer.EntityFrameworkCore.Context
{
    public class MainDbContext : SbpDbContext
    {
        public DbSet<ProductLicense> ProductLicenses { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<APILicense> APILicenses{ get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }
    }

    /* 这里为了 Migration 迁移使用 */
    /* 迁移命令： Add-Migration -c DemoMainDbContext */
    public class MainDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
    {
        public MainDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<MainDbContext>();
            var connectionString = configuration.GetConnectionString("Default");

            var optionsBuilder = new DbContextOptionsBuilder<MainDbContext>();
            optionsBuilder.UseMySql(connectionString);

            return new MainDbContext(optionsBuilder.Options);
        }

    }

}
