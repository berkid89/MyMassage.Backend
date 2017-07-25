using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Squire.Core.Middlewares;
using System.Text;

namespace MyMassage.Backend
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        private readonly ILogger logger;
        private readonly ISettings settings;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            settings = new Settings(Configuration);
            logger = ConfigureLogger(settings);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(p => settings);
            services.AddScoped(p => logger);

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

            app.UseGlobalErrorHandler(env.IsDevelopment());
            app.UseCorrelationToken();
            app.UseRequestLogging();
            app.UsePerformanceLogging(settings.PerformaceWarningMinimumInMS);

            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.AppSecret)),
                }
            });

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "api/v1/{controller}/{action}/{id?}");
            });
        }

        private ILogger ConfigureLogger(ISettings settings)
        {
            return new LoggerConfiguration()
              .Enrich.FromLogContext()
              .MinimumLevel.Verbose()
              .WriteTo.ColoredConsole(settings.LogLevel, "{NewLine}{Timestamp:HH:mm:ss} [{Level}] ({CorrelationToken}) {Message}{NewLine}{Exception}")
              .WriteTo.MongoDB($"{settings.Database_Url}/{settings.Database_Name}")
              .CreateLogger();
        }
    }
}
