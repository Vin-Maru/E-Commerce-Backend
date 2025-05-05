using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoeStore.Infrastructure.Configuration;
using ShoeStore.Infrastructure.Persistence;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Application.Abstractions.Navision.IRepository;
using ShoeStore.Infrastructure.Implimentation.Repository;
using ShoeStore.Application.Implimentation.Interfaces;
namespace ShoeStore.Infrastructure.Utilities
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<ODataSettings>(options => configuration.GetConnectionString("ODataSettings"));

            var ConnString = configuration.GetConnectionString("ConnectionStrings");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")
                )
            );



         services.AddScoped<ICategoryRepository, CategoryRepository>();
         services.AddScoped<ISaveChanges, SaveChanges>();
         return services;
        }
    }
}