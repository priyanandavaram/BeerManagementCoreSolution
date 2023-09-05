using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Repository.Interfaces;
using BeerManagement.Repository.UnitOfWork;
using BeerManagement.Services.Automapper;
using BeerManagement.Services.Interfaces;
using BeerManagement.Services.Services;
using BeerManagement.Web.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace BeerManagement.Web
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
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("BMSConnection")));
            services.AddAutoMapper(typeof(AutoMapperClass).Assembly);
            services.AddScoped<IBeerService, BeerService>();
            services.AddScoped<IBarService, BarService>();
            services.AddScoped<IBreweryService, BreweryService>();
            services.AddScoped<IBarAndBeerService, BarAndBeerService>();
            services.AddScoped<IBreweryAndBeerService, BreweryAndBeerService>();
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
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