using IWSN_Backend_Server.Models;
using IWSN_Backend_Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using IWSN_Backend_Server.Model.Settings;
using IWSN_Backend_Server.Models.Settings.Database;
using IWSN_Backend_Server.Model.Settings.Database;
using IWSN_Backend_Server.Settings;

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
            services.Configure<BankAccountDatabaseSettings>(Configuration.GetSection(nameof(BankAccountDatabaseSettings))); // register the BankAccount Database settings
            services.Configure<SensorInfomationDatabaseSettings>(Configuration.GetSection(nameof(SensorInfomationDatabaseSettings)));  // register the Sensor Database settings

            // Add the singleton instances from the given Interface and add it the the services collection
            services.AddSingleton<IBankAccountDatabaseSettings>(sIAccountDB => sIAccountDB.GetRequiredService<IOptions<BankAccountDatabaseSettings>>().Value);
            services.AddSingleton<ISensorInfomationDatabaseSettings>(sISensorDB => sISensorDB.GetRequiredService<IOptions<SensorInfomationDatabaseSettings>>().Value);

            // Add the singleton service instances
            services.AddSingleton<BankAccountService>();
            services.AddSingleton<SensorMeasurementService>();

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
