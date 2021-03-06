using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using WeatherAPI.DbContext;
using WeatherAPI.Extensions;
using WeatherAPI.Models;
using WeatherAPI.Repositories;
using WeatherAPI.Services.BBoxServices;
using WeatherAPI.Services.ForecastServices;
using WeatherAPI.Services.HistoricalServices;

namespace WeatherAPI
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
            services.AddDbContext<WeatherDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IRepository<City>, CityRepository>();
            
            services.AddOpenWeatherService(
                new Uri(Configuration["OpenWeatherSettings:BaseApiUrl"]))
                .AddTransient<IPointsWeatherService, PointsWeatherService>();

            services.AddOpenWeatherForecastService(
                new Uri(Configuration["OpenWeatherSettings:BaseApiUrl"]))
                .AddTransient<IPointWeatherForecastService, PointWeatherForecastService>();

            services.AddOpenWeatherHistoricalService(
                    new Uri(Configuration["OpenWeatherSettings:BaseApiUrl"]))
                .AddTransient<IPointWeatherHistoricalService, PointWeatherHistoricalService>();
            
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins(
                            "https://localhost:5001", 
                            "http://localhost:5000",
                            "http://localhost:4200",
                            "https://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddControllers().AddNewtonsoftJson();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "WeatherAPI", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}