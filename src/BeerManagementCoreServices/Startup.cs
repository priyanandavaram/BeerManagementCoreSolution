using Microsoft.Extensions.DependencyInjection;
using BeerManagementCoreServices.Database;
using BeerManagementCoreServices.Common;
using BeerManagementCoreServices.Interfaces;
using BeerManagementCoreServices.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace BeerManagementCoreServices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();

            services.AddControllers();

            services.AddDbContext<BeerManagementDatabaseContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("BMSConnection")));

            services.AddScoped<IBeerRepository, BeerRepository>();
            services.AddScoped<IBreweryRepository, BreweryRepository>();
            services.AddScoped<IBarRepository, BarRepository>();
            services.AddScoped<ILinkBreweryAndBeerRepository, LinkBreweryAndBeerRepository>();
            services.AddScoped<ILinkBarAndBeerRepository, LinkBarAndBeerRepository>();
            services.AddScoped(typeof(IGenericServiceRepository<>), typeof(GenericServices<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
