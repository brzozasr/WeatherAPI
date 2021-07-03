using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherAPI.Extensions;
using WeatherAPI.Models.WeatherForecast;

namespace WeatherAPI.Services.ForecastServices
{
    public class OpenWeatherForecastService : IOpenWeatherForecastService
    {
        private HttpClient _httpClient;
        public OpenWeatherForecastService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherForecast> GetWeatherForecastAsync(
            double lat, double lon, string units = "metric", string lang = "en")
        {
            var  requestedUri = 
                    $"onecall?lat={lat}&lon={lon}&units={units}&lang={lang}&appid={StartupExtensions.AppSetting["OpenWeatherApiKey:APIKey"]}";
            
            
            Console.WriteLine($"URI params: lat={lat}, lon={lon}, units={units}, lang={lang}");
            
            return null;
        }
    }
}