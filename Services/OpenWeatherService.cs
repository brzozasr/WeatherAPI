using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherAPI.Extensions;
using WeatherAPI.Models.WeatherBBox;

namespace WeatherAPI.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly HttpClient _httpClient;

        public OpenWeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherBoxRoot> GetOpenWeatherBoxAsync(
            double lonLeft, double latBottom, double lonRight, double latTop, int zoom)
        {
            try
            {
                WeatherBoxRoot listOfCitiesWeather;
                
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                using (var response = await _httpClient.GetAsync(
                        $"box/city?bbox={lonLeft},{latBottom},{lonRight},{latTop},{zoom}&appid={StartupExtensions.AppSetting["OpenWeatherApiKey:APIKey"]}"))
                {
                    //response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    listOfCitiesWeather = JsonConvert.DeserializeObject<WeatherBoxRoot>(content);;
                }

                return listOfCitiesWeather;
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