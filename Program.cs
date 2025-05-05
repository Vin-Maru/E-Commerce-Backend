using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.WebEncoders;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ESSPortal.Api.Middleware;
using ESSPortal.Api.Utilities;
using ShoeStore.Application.Configuration;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Persistence;
using ShoeStore.Infrastructure.Utilities;
using Scalar.AspNetCore;
using AspNetCore.Scalar;
using ShoeStore.Application.Utilities;

var builder = WebApplication.CreateBuilder(args);

string corsOpenPolicy = "OpenCORSPolicy";

// JWT settings
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>() ?? new JwtSettings();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Add core services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
       options.JsonSerializerOptions.ReferenceHandler = null;
       options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
       options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
       options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
       options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddCors(options =>
{
   options.AddPolicy(corsOpenPolicy, policy =>
   {
      policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
   });
});

builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddWebEncoders();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton(serviceProvider =>
{
   var accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
   return new SessionDictionary<string>(accessor, "AppSession");
});

// API Versioning
builder.Services.AddApiVersioning(config =>
{
   config.DefaultApiVersion = new ApiVersion(1, 0);
   config.AssumeDefaultVersionWhenUnspecified = true;
   config.ReportApiVersions = true;
   config.ApiVersionReader = new UrlSegmentApiVersionReader();
});

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
   options.ClaimsIdentity.UserNameClaimType = "Username";
   options.User.RequireUniqueEmail = true;
   options.Password.RequireDigit = true;
   options.Password.RequireLowercase = true;
   options.Password.RequireUppercase = true;
   options.Password.RequireNonAlphanumeric = true;
   options.Password.RequiredLength = 8;
   options.Password.RequiredUniqueChars = 1;
   options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
   options.Lockout.MaxFailedAccessAttempts = 5;
   options.SignIn.RequireConfirmedEmail = false;
   options.SignIn.RequireConfirmedPhoneNumber = false;
   options.SignIn.RequireConfirmedAccount = false;
});

builder.Services.AddAuthentication(options =>
{
   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
   options.SaveToken = true;
   options.RequireHttpsMetadata = false;
   options.TokenValidationParameters = new TokenValidationParameters
   {
      ClockSkew = TimeSpan.Zero,
      ValidIssuer = jwtSettings.Issuer,
      ValidAudience = jwtSettings.Audience,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret!)),
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      RequireExpirationTime = true,
   };
});

// Scalar OpenAPI
builder.Services.AddOpenApi(options =>
{
   options.AddDocumentTransformer((document, context, _) =>
   {
      document.Info = new()
      {
         Title = "ShoeStore Api",
         Version = "v1",
         Description = """
                By Maru.
                """,
         Contact = new()
         {
            Name = "API Support",
            Email = "talktous@eclectics.io",
            Url = new Uri("http://www.eclectics.io/")
         }
      };

      return Task.CompletedTask;
   });
});

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
app.MapOpenApi();

if (app.Environment.IsDevelopment())
{
   app.UseDeveloperExceptionPage();
   app.MapScalarApiReference();
}

app.UseExceptionHandler(_ => { });
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(corsOpenPolicy);
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseScalar(options =>
{
   options.SpecUrl = "/openapi/v1/openapi.json"; // Default path for Scalar's OpenAPI JSON
});

app.MapControllers();
app.Run();
