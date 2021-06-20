using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherAPI.Services;

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
    }
}