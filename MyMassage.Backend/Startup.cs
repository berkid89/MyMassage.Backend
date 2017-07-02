using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Squire.Core.Middlewares;

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

            app.UseGlobalErrorHandler(true);
            app.UseCorrelationToken();
            app.UseRequestLogging();
            app.UsePerformanceLogging(1100);

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
              .WriteTo.MongoDB($"{settings.DatabaseUrl}/{settings.DatabaseName}")
              .CreateLogger();
        }
    }
}
