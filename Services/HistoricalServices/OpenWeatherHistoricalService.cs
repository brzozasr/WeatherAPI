using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherAPI.Extensions;
using WeatherAPI.Models.WeatherForecast;
using WeatherAPI.Models.WeatherHistorical;

namespace WeatherAPI.Services.HistoricalServices
{
    public class OpenWeatherHistoricalService : IOpenWeatherHistoricalService
    {
        private readonly HttpClient _httpClient;
        
        public OpenWeatherHistoricalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<WeatherHistorical> GetWeatherHistoricalAsync(
            double lat, double lon, long unixTime, string units = "metric", string lang = "en")
        {
            try
            {
                Console.WriteLine($"URI Historical params: lat={lat}, lon={lon}, unixTime={unixTime}, units={units}, lang={lang}");
                
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                
                using (var response = await _httpClient.GetAsync(
                    $"onecall/timemachine?lat={lat}&lon={lon}&dt={unixTime}&units={units}&lang={lang}&appid={StartupExtensions.AppSetting["OpenWeatherApiKey:APIKey"]}"))
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if (content.Length > 0)
                    {
                        var weatherHistorical = JsonConvert.DeserializeObject<WeatherHistorical>(content);
                        return weatherHistorical;
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