using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherAPI.Models.PointWeatherHistorical;
using WeatherAPI.Models.WeatherHistorical;
using WeatherAPI.Utilities;

namespace WeatherAPI.Services.HistoricalServices
{
    public class PointWeatherHistoricalService : IPointWeatherHistoricalService
    {
        private readonly IOpenWeatherHistoricalService _openWeatherHistorical;

        public PointWeatherHistoricalService(IOpenWeatherHistoricalService openWeatherHistorical)
        {
            _openWeatherHistorical = openWeatherHistorical;
        }
        
        public async Task<PointWeatherHistorical> GetPointWeatherHistoricalAsync(
            double lat, double lon, string units = "metric", string lang = "en")
        {
            var queryCounter = 5;  // 5 it means => 5 request five API calls (one call for each day)
            var weatherHistoricalList = new List<WeatherHistorical>();
            var unixTimeNow = Util.UnixTimeNow();
            var unixTime = unixTimeNow;
            
            for (var i = 0; i < queryCounter; i++)
            {
                Console.WriteLine($"[{DateTime.Now:yyyy/MM/dd HH:mm:ss.fff}]: Historical API request (counter: {i + 1}, request time: {Util.UnixTimeToDateTime(unixTime)})");
                
                var weatherHistorical = await _openWeatherHistorical.GetWeatherHistoricalAsync(
                    lat, lon, unixTime, units, lang);

                if (weatherHistorical is not null)
                {
                    weatherHistoricalList.Add(weatherHistorical);
                }

                unixTime -= 86400;
            }

            return null;
        }
    }
}