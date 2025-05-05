using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoeDb.Application.Configuration;
using ShoeDb.Application.Implimentation.Interfaces;
using ShoeStore.Application.Abstractions.Interfaces;
using ShoeStore.Application.Abstractions.Iservices;
using ShoeStore.Application.Abstractions.Navision.IRepository;
using ShoeStore.Application.Abstractions.Navision.IServices;
using ShoeStore.Application.Configuration;
using ShoeStore.Application.Implimentation.Interfaces;
using ShoeStore.Application.Implimentation.Services;

namespace ShoeStore.Application.Utilities
{
   public static class DependencyInjection
   {
      public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
      {
         // Registering services for application logic
         services.AddScoped<IAccountService, AccountService>();
         services.AddScoped<IServiceManager, ServiceManager>();
         services.AddScoped<IJwtService, JwtService>();
         services.AddScoped<ISmsSender, SmsSender>();
         services.AddScoped<ICategoryService, CategoryService>();
         services.AddScoped<IUnitOfWork, UnitOfWork>();
         return services;
      }
   }

}
