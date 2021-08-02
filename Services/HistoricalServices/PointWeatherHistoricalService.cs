using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAPI.Extensions;
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
            var queryCounter = 6;  // 5 it means => 5 request five API calls (one call for each day)
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

            if (weatherHistoricalList.Count > 0)
            {
                int counter = 0;
                var historical = new PointWeatherHistorical();
                
                foreach (var weatherDay in weatherHistoricalList)
                {
                    if (counter == 0)
                    {
                        historical.StatusCode = 200;
                        historical.Lat = weatherDay.Lat;
                        historical.Lon = weatherDay.Lon;
                        historical.Timezone = weatherDay.Timezone;
                        historical.TimezoneOffset = weatherDay.TimezoneOffset;
                    }
                    
                    historical.Hourly.AddRange(GetHourly(weatherDay));
                    
                    counter += 1;
                }

                return historical;
            }

            return null;
        }

        private IEnumerable<HourlyPwh> GetHourly(WeatherHistorical historical)
        {
            if (historical.Hourly is not null && historical.Hourly.Any())
            {
                return historical.Hourly.Select(hourly => new HourlyPwh
                    {
                        Dt = Util.UnixTimeToDateTime(hourly.Dt),
                        DtLocal = Util.UnixTimeToDateTimeLocal(hourly.Dt, historical.TimezoneOffset),
                        Temp = hourly.Temp,
                        FeelsLike = hourly.FeelsLike,
                        Pressure = hourly.Pressure,
                        Humidity = hourly.Humidity,
                        DewPoint = hourly.DewPoint,
                        Uvi = hourly.Uvi,
                        Clouds = hourly.Clouds,
                        Visibility = hourly.Visibility,
                        VisibilityKm = Util.MToKm(hourly.Visibility),
                        WindSpeed = hourly.WindSpeed,
                        WindSpeedKmPerH = Util.MPerSecToKmPerH(hourly.WindSpeed),
                        WindDeg = hourly.WindDeg,
                        WindGust = hourly.WindGust,
                        WindDir = GetWindDirection(hourly.WindDeg),
                        Weathers = GetWeather(hourly.Weathers),
                        Rain = GetRain(hourly.Rain),
                        Snow = GetSnow(hourly.Snow)
                    })
                    .OrderByDescending(x => x.Dt)
                    .ToList();
            }

            return null;
        }
        
        private WindDirPwh GetWindDirection(int direction)
        {
            var wind = new WindDirPwh
            {
                DirTxt = direction.DirectionTxt(),
                DirArrow = direction.DirectionArrow()
            };
            return wind;
        }
        
        private IEnumerable<WeatherPwh> GetWeather(IEnumerable<WeatherWh> weatherWh)
        {
            if (weatherWh is not null)
            {
                var weatherList = new List<WeatherPwh>();
                    
                foreach (var weather in weatherWh)
                {
                    var weatherPwh = new WeatherPwh
                    {
                        Id = weather.Id,
                        Main = weather.Main,
                        Description = weather.Description.FirstLetterToUpper(),
                        Icon = weather.Icon
                    };
                    
                    weatherList.Add(weatherPwh);
                }

                return weatherList;
            }

            return null;
        }
        
        private RainPwh GetRain(RainWh rainWh)
        {
            if (rainWh is not null)
            {
                var rain = new RainPwh
                {
                    H1 = rainWh.H1
                };
                return rain;
            }

            return null;
        }

        private SnowPwh GetSnow(SnowWh snowWh)
        {
            if (snowWh is not null)
            {
                var snow = new SnowPwh
                {
                    H1 = snowWh.H1
                };
                return snow;
            }

            return null;
        }
    }
}