using System.Data.SqlClient;
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

   // public IConfigurationRoot Configuration { get; set; }
    public IConfiguration Configuration { get; }

    private static readonly log4net.ILog fLog = log4net.LogManager.GetLogger(typeof(Startup));

    public Startup(ILogger<Startup> logger, IConfiguration configuration)
    {
      Configuration = configuration;
    }


    public void ConfigureServices(IServiceCollection services)
    {
      services
        .AddMvc()
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      var tmp = Configuration
        .GetSection("DbConnectionConfiguration:ConnectionString");
      //add to function preparing env, make factory here
      MyEnvironment.DefaultDbConnectionString = Configuration
       .GetSection("DbConnectionConfiguration:ConnectionString")
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

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
      {
        DataContext context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
        context.Database.Migrate();
      }


      loggerFactory.AddLog4Net();
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
