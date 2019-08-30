using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SocialAnalyser.Configs;
using SocialAnalyser.Entities;
using SocialAnalyser.IoC;
using SocialAnalyser.Middlewares;

namespace SocialAnalyser
{
  public class Startup
  {

    public IConfigurationRoot Configuration { get; set; }

    public Startup(IHostingEnvironment env)
    {
      //register by env as appsettings.{Environment}.json
      IConfigurationBuilder builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

      Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services
        .AddMvc()
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      //add to function preparing env, make factory here
      MyEnvironment.DefaultDbConnectionString = Configuration
        .GetSection("DbConnectionConfiguration")
        .Value;

      services.AddDbContext<DataContext>(SetupDbContext);
      //services.AddSwaggerGen(SwaggerConfig.SetupSwaggerGen);

      // Add functionality to inject IOptions<T>
      //services.AddOptions();

      // Add our Config object so it can be injected for controller
      //services.Configure<DbConnectionConfiguration>(Configuration.GetSection("DbConnectionConfiguration"));

      services.AddFactories();
      services.AddHandlers();
      services.AddRepositories();
      services.AddServices();
      services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseMvc();

      app.Run(async (context) =>
      {
        await context.Response.WriteAsync("Hello World!");
      });

      app.UseCors(CorsConfig.SetupCors);

      app.UseMiddleware<ExceptionMiddleware>();
      app.UseMiddleware<DbTransactionMiddleware>();

      //app.UseSwagger(SwaggerConfig.SetupSwagger);
      //app.UseSwaggerUI(SwaggerConfig.SetupSwaggerUI);
    }

    private void SetupDbContext(DbContextOptionsBuilder optionsBuilder)
    {
      //OBSOLOTE
      LoggerFactory f = new LoggerFactory(new[] { new ConsoleLoggerProvider((m, l) => l == LogLevel.Information, true) });
      // TODO zmazat po pridani ILoggerFactory do ServiceCollection
      optionsBuilder.UseLoggerFactory(f);
      optionsBuilder.UseSqlServer(MyEnvironment.DbConnectionString);
    }
  }
}
