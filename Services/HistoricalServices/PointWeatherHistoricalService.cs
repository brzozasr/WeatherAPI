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

                if (historical.Hourly is not null && historical.Hourly.Count > 0)
                {
                    historical.AggTemp = GetMinMaxTemp(historical.Hourly);
                    historical.AggFeelsLike = GetMinMaxFeelsLike(historical.Hourly);
                    historical.AggPressure = GetMinMaxPressure(historical.Hourly);
                    historical.AggHumidity = GetMinMaxHumidity(historical.Hourly);
                    historical.AggDewPoint = GetMinMaxDewPoint(historical.Hourly);
                    historical.AggUvi = GetMinMaxUvi(historical.Hourly);
                    historical.AggVisibility = GetMinMaxVisibility(historical.Hourly);
                    historical.AggWindSpeed = GetMinMaxWindSpeed(historical.Hourly);
                    historical.AggRain = GetMinMaxRain(historical.Hourly);
                    historical.AggSnow = GetMinMaxSnow(historical.Hourly);
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

        private AggTemp GetMinMaxTemp(List<HourlyPwh> hourlyList)
        {
            var min = hourlyList.OrderBy(x => x.Temp).FirstOrDefault();
            var max = hourlyList.OrderByDescending(x => x.Temp).FirstOrDefault();
            var temps = new AggTemp
            {
                DtMin = min?.Dt,
                DtLocalMin = min?.DtLocal,
                TempMin = min?.Temp,
                DtMax = max?.Dt,
                DtLocalMax = max?.DtLocal,
                TempMax = max?.Temp
            };
            return temps;
        }
        
        private AggFeelsLike GetMinMaxFeelsLike(List<HourlyPwh> hourlyList)
        {
            var min = hourlyList.OrderBy(x => x.FeelsLike).FirstOrDefault();
            var max = hourlyList.OrderByDescending(x => x.FeelsLike).FirstOrDefault();
            var feelsLike = new AggFeelsLike
            {
                DtMin = min?.Dt,
                DtLocalMin = min?.DtLocal,
                FeelsLikeMin = min?.FeelsLike,
                DtMax = max?.Dt,
                DtLocalMax = max?.DtLocal,
                FeelsLikeMax = max?.FeelsLike
            };
            return feelsLike;
        }
        
        private AggPressure GetMinMaxPressure(List<HourlyPwh> hourlyList)
        {
            var min = hourlyList.OrderBy(x => x.Pressure).FirstOrDefault();
            var max = hourlyList.OrderByDescending(x => x.Pressure).FirstOrDefault();
            var pressure = new AggPressure
            {
                DtMin = min?.Dt,
                DtLocalMin = min?.DtLocal,
                PressureMin = min?.Pressure,
                DtMax = max?.Dt,
                DtLocalMax = max?.DtLocal,
                PressureMax = max?.Pressure
            };
            return pressure;
        }
        
        private AggHumidity GetMinMaxHumidity(List<HourlyPwh> hourlyList)
        {
            var min = hourlyList.OrderBy(x => x.Humidity).FirstOrDefault();
            var max = hourlyList.OrderByDescending(x => x.Humidity).FirstOrDefault();
            var humidity = new AggHumidity
            {
                DtMin = min?.Dt,
                DtLocalMin = min?.DtLocal,
                HumidityMin = min?.Humidity,
                DtMax = max?.Dt,
                DtLocalMax = max?.DtLocal,
                HumidityMax = max?.Humidity
            };
            return humidity;
        }
        
        private AggDewPoint GetMinMaxDewPoint(List<HourlyPwh> hourlyList)
        {
            var min = hourlyList.OrderBy(x => x.DewPoint).FirstOrDefault();
            var max = hourlyList.OrderByDescending(x => x.DewPoint).FirstOrDefault();
            var dewPoint = new AggDewPoint
            {
                DtMin = min?.Dt,
                DtLocalMin = min?.DtLocal,
                DewPointMin = min?.DewPoint,
                DtMax = max?.Dt,
                DtLocalMax = max?.DtLocal,
                DewPointMax = max?.DewPoint
            };
            return dewPoint;
        }
        
        private AggUvi GetMinMaxUvi(List<HourlyPwh> hourlyList)
        {
            var min = hourlyList.OrderBy(x => x.Uvi).FirstOrDefault();
            var max = hourlyList.OrderByDescending(x => x.Uvi).FirstOrDefault();
            var uvi = new AggUvi
            {
                DtMin = min?.Dt,
                DtLocalMin = min?.DtLocal,
                UviMin = min?.Uvi,
                DtMax = max?.Dt,
                DtLocalMax = max?.DtLocal,
                UviMax = max?.Uvi
            };
            return uvi;
        }
        
        private AggVisibility GetMinMaxVisibility(List<HourlyPwh> hourlyList)
        {
            var min = hourlyList.OrderBy(x => x.VisibilityKm).FirstOrDefault();
            var max = hourlyList.OrderByDescending(x => x.VisibilityKm).FirstOrDefault();
            var visibility = new AggVisibility
            {
                DtMin = min?.Dt,
                DtLocalMin = min?.DtLocal,
                VisibilityKmMin = min?.VisibilityKm,
                DtMax = max?.Dt,
                DtLocalMax = max?.DtLocal,
                VisibilityKmMax = max?.VisibilityKm
            };
            return visibility;
        }
        
        private AggWindSpeed GetMinMaxWindSpeed(List<HourlyPwh> hourlyList)
        {
            var min = hourlyList.OrderBy(x => x.WindSpeed).FirstOrDefault();
            var max = hourlyList.OrderByDescending(x => x.WindSpeed).FirstOrDefault();
            var windSpeed = new AggWindSpeed
            {
                DtMin = min?.Dt,
                DtLocalMin = min?.DtLocal,
                WindSpeedMin = min?.WindSpeed,
                DtMax = max?.Dt,
                DtLocalMax = max?.DtLocal,
                WindSpeedMax = max?.WindSpeed
            };
            return windSpeed;
        }
        
        private AggRain GetMinMaxRain(List<HourlyPwh> hourlyList)
        {
            var min = hourlyList.Where(x => x.Rain != null)
                .OrderBy(x => x.Rain.H1).FirstOrDefault();
            var max = hourlyList.Where(x => x.Rain != null)
                .OrderByDescending(x => x.Rain.H1).FirstOrDefault();

            if (min is not null || max is not null)
            {
                var rain = new AggRain
                {
                    DtMin = min?.Dt,
                    DtLocalMin = min?.DtLocal,
                    RainMin = min?.Rain.H1,
                    DtMax = max?.Dt,
                    DtLocalMax = max?.DtLocal,
                    RainMax = max?.Rain.H1
                };
                return rain;
            }

            return null;
        }
        
        private AggSnow GetMinMaxSnow(List<HourlyPwh> hourlyList)
        {
            var min = hourlyList.Where(x => x.Snow != null)
                .OrderBy(x => x.Snow.H1).FirstOrDefault();
            var max = hourlyList.Where(x => x.Snow != null)
                .OrderByDescending(x => x.Snow.H1).FirstOrDefault();

            if (min is not null || max is not null)
            {
                var snow = new AggSnow
                {
                    DtMin = min?.Dt,
                    DtLocalMin = min?.DtLocal,
                    SnowMin = min?.Snow.H1,
                    DtMax = max?.Dt,
                    DtLocalMax = max?.DtLocal,
                    SnowMax = max?.Snow.H1
                };
                return snow;
            }

            return null;
        }
    }
}