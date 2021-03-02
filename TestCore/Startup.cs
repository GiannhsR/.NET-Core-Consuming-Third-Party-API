using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCore.DAL.Models;
using TestCore.DAL.RepositoryServices;
using TestCore.HelperClasses;
using TestCore.Services;
using TestCore.Services.BackgroundServices;

namespace TestCore
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
            services.AddRazorPages();

            services.Configure<DataDragonChampionDatabaseSettings>(
                Configuration.GetSection(nameof(DataDragonChampionDatabaseSettings)));

            services.AddSingleton<IDataDragonChampionDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DataDragonChampionDatabaseSettings>>().Value);

            services.Configure<AppSettings>(Configuration.GetSection("API_KEY"));

            services.AddScoped<IRetrieveRegionService>();
            services.AddScoped<IRetrieveApiKeyService>();
            services.AddScoped<ISearchSummonerService>();
            services.AddScoped<IMatchHistoryService>();
            services.AddSingleton<IChampionsService>();
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
