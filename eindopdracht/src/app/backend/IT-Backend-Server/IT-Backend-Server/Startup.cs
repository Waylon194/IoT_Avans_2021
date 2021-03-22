using IWSN_Backend_Server.Models;
using IWSN_Backend_Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using IWSN_Backend_Server.Model.Settings.Database;

namespace IWSN_Backend_Server
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // register the configuration of the Settings
      services.Configure<UserDatabaseSettings>(Configuration.GetSection(nameof(UserDatabaseSettings))); // register the BankAccount Database settings

      // Add the singleton instances from the given Interface and add it the the services collection
      services.AddSingleton<IUserDatabaseSettings>(sIAccountDB => sIAccountDB.GetRequiredService<IOptions<UserDatabaseSettings>>().Value);

      // Add the singleton service instances
      services.AddSingleton<BankAccountService>();

      // add specified controllers 
      services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
