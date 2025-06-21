// File: Data/StationeryShopDbContextFactory.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StationeryShop.Data
{
    public class StationeryShopDbContextFactory : IDesignTimeDbContextFactory<StationeryShopDbContext>
    {
        public StationeryShopDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<StationeryShopDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new StationeryShopDbContext(builder.Options);
        }
    }
}