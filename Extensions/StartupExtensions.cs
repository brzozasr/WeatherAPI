using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherAPI.Services;
using WeatherAPI.Services.BBoxServices;
using WeatherAPI.Services.ForecastServices;
using WeatherAPI.Services.HistoricalServices;

namespace WeatherAPI.Extensions
{
    public static class StartupExtensions
    {
        public static IConfiguration AppSetting { get; }
        
        static StartupExtensions()
        {
            AppSetting = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
        }
        
        public static IServiceCollection AddOpenWeatherService(this IServiceCollection services, Uri baseApiUrl)
        {
            return services.AddSingleton<IOpenWeatherService, OpenWeatherService>(serviceProvider => 
                new OpenWeatherService(new HttpClient
                {
                    BaseAddress = baseApiUrl
                }));
        }

        public static IServiceCollection AddOpenWeatherForecastService(this IServiceCollection service, Uri baseApiUri)
        {
            return service.AddSingleton<IOpenWeatherForecastService, OpenWeatherForecastService>(serviceProvider => 
                new OpenWeatherForecastService(new HttpClient
                {
                    BaseAddress = baseApiUri
                }));
        }
        
        public static IServiceCollection AddOpenWeatherHistoricalService(this IServiceCollection service, Uri baseApiUri)
        {
            return service.AddSingleton<IOpenWeatherHistoricalService, OpenWeatherHistoricalService>(serviceProvider => 
                new OpenWeatherHistoricalService(new HttpClient
                {
                    BaseAddress = baseApiUri
                }));
        }
    }
}