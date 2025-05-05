using AspNetCoreRateLimit;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Extensions.Logging;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using ShoeStore.Application.Configuration;

namespace ShoeStore.Utilities;

public static class DependencyInjection
{
    private static IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();

    public class CorrelationIdEnricher : ILogEventEnricher
    {
        private const string CorrelationIdPropertyName = "CorrelationId";

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var correlationId = GetCorrelationId(); 
            var correlationIdProperty = new LogEventProperty(CorrelationIdPropertyName, new ScalarValue(correlationId));

            logEvent.AddOrUpdateProperty(correlationIdProperty);
        }

        private string GetCorrelationId()
        {
            // Retrieve the correlation ID from the HTTP context if available
            var httpContext = httpContextAccessor.HttpContext;
            var correlationId = httpContext?.Request.Headers["CorrelationId"].FirstOrDefault();

            // If the correlation ID is not available in the HTTP context, generate a new GUID
            if (string.IsNullOrEmpty(correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
            }

            return correlationId;
        }
    }

    public class IPAddressEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var httpContext = httpContextAccessor.HttpContext;
            var remoteIpAddress = httpContext?.Connection?.RemoteIpAddress;

            // Check if the IP address is available and not in IPv6 loopback format
            if (remoteIpAddress != null && !System.Net.IPAddress.IsLoopback(remoteIpAddress))
            {
                var ipAddress = remoteIpAddress.ToString();
                var ipAddressProperty = propertyFactory.CreateProperty("IPAddress", ipAddress);
                logEvent.AddPropertyIfAbsent(ipAddressProperty);
            }
        }
    }
   
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        try
        {
            var assembly = typeof(Program).Assembly;

            services.Configure<JsonSettings>(configuration.GetSection(""));

            var serviceMappings = configuration.GetSection("ServiceMappings").Get<Dictionary<string, string>>();

           // services.AddValidatorsFromAssembly(assembly);


            return services;
        }
        catch (Exception)
        {

            throw;
        }
        
    }

    public static IServiceCollection AddLoggingServices(this IServiceCollection services, IConfiguration configuration)
    {
        try
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .MinimumLevel.Information()
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Serilog", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.With(new CorrelationIdEnricher())
                .Enrich.WithProperty("MachineName", Environment.MachineName)
                .Enrich.With<IPAddressEnricher>()
                .WriteTo.Async(s => s.Console(new CompactJsonFormatter()))
                .WriteTo.Async(s => s.File(
                    path: configuration["Serilog:WriteTo:1:Args:path"]!, // Read path from appsettings.json
                    rollingInterval: RollingInterval.Day,
                    formatter: new JsonFormatter())) // Ensure JSON formatting
                .CreateLogger();

            services.AddSingleton<ILoggerFactory>(provider =>
            {
                return new SerilogLoggerFactory(Log.Logger, true);
            });

            return services;
        }
        catch (Exception)
        {

            throw;
        }

    }

    public static IServiceCollection AddRateLimitingServices(this IServiceCollection services)
    {
        try
        {
            // Add IP Rate Limiting
            services.Configure<IpRateLimitOptions>(options =>
            {
                options.EnableEndpointRateLimiting = true;
                options.StackBlockedRequests = false;
                options.HttpStatusCode = 429;
                options.GeneralRules = new List<RateLimitRule>  // Changed from RateLimit.Rules.RateLimitRule
                {
                    new RateLimitRule
                    {
                        Endpoint = "*",
                        Period = "1s",
                        Limit = 10
                    }
                };
            });

            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            return services;
        }
        catch (Exception)
        {

            throw;
        }
        
    }

    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
    {
        try
        {
            return app.Use(async (context, next) =>
            {
                context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Append("X-Frame-Options", "DENY");
                context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
                context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");

                await next();
            });
        }
        catch (Exception)
        {

            throw;
        }
        
    }


}
