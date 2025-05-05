using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration.Json;

namespace ShoeStore.Infrastructure.Persistence
{
   public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
   {
      public ApplicationDbContext CreateDbContext(string[] args)
      {
         IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory()) // makes it work from CLI
             .AddJsonFile("appsettings.Development.json")
             .Build();

         var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
         var connectionString = configuration.GetConnectionString("DefaultConnection");

         builder.UseNpgsql(connectionString);

         return new ApplicationDbContext(builder.Options);
      }
   }
}
