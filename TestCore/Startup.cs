using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TestCore.DAL;
using TestCore.DAL.Models;
using TestCore.DAL.RepositoryServices;
using TestCore.HelperClasses;
using TestCore.Services;

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

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MyDatabase")));

            services.Configure<DataDragonChampionDatabaseSettings>(
                Configuration.GetSection(nameof(DataDragonChampionDatabaseSettings)));

            services.AddSingleton<IDataDragonChampionDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DataDragonChampionDatabaseSettings>>().Value);

            services.AddDefaultIdentity<ApplicationUser>(options =>
                options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDbContext>();

            services.Configure<AppSettings>(Configuration.GetSection("API_KEY"));

            services.AddScoped<IRetrieveRegionService>();
            services.AddScoped<IRetrieveApiKeyService>();
            services.AddScoped<ISearchSummonerService>();
            services.AddScoped<IMatchHistoryService>();
            services.AddScoped<IMyDatabaseService>();

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
