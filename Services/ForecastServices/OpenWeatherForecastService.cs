using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WeatherAPI.Extensions;
using WeatherAPI.Models.WeatherForecast;
using Newtonsoft.Json;

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
            try
            {
                Console.WriteLine($"URI params: lat={lat}, lon={lon}, units={units}, lang={lang}");

                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                
                using (var response = await _httpClient.GetAsync(
                    $"onecall?lat={lat}&lon={lon}&units={units}&lang={lang}&appid={StartupExtensions.AppSetting["OpenWeatherApiKey:APIKey"]}"))
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if (content.Length > 0)
                    {
                        var weatherForecast = JsonConvert.DeserializeObject<WeatherForecast>(content);
                        return weatherForecast;
                    }
                }

                return null;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
    }
}