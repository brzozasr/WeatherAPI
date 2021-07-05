using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherAPI.Extensions;
using WeatherAPI.Models;
using WeatherAPI.Models.PointWeatherForecast;
using WeatherAPI.Models.WeatherForecast;

namespace WeatherAPI.Services.ForecastServices
{
    public class PointWeatherForecastService : IPointWeatherForecastService
    {
        private IOpenWeatherForecastService _openWeatherForecast;

        public PointWeatherForecastService(IOpenWeatherForecastService openWeatherForecast)
        {
            _openWeatherForecast = openWeatherForecast;
        }

        public async Task<PointWeatherForecast> GetPointWeatherForecastAsync(double lat, double lon,
            string units = "metric", string lang = "en")
        {
            var weatherForecast = await _openWeatherForecast.GetWeatherForecastAsync(
                lat, lon, units, lang);

            if (weatherForecast is not null)
            {
                List<WeatherPwf> weatherCurrList = null;

                if (weatherForecast.CurrentWf.Weathers is not null)
                {
                    weatherCurrList = new List<WeatherPwf>();
                    
                    foreach (var weather in weatherForecast.CurrentWf.Weathers)
                    {
                        var weatherCurr = new WeatherPwf
                        {
                            Id = weather.Id,
                            Main = weather.Main,
                            Description = weather.Description.FirstLetterToUpper(),
                            Icon = weather.Icon
                        };
                    
                        weatherCurrList.Add(weatherCurr);
                    }
                }

                RainPwf rainCurr = null;
                
                if (weatherForecast.CurrentWf.Rain is not null)
                {
                    rainCurr = new RainPwf
                    {
                        H1 = weatherForecast.CurrentWf.Rain.H1
                    };
                }
                
                SnowPwf snowCurr = null;
                
                if (weatherForecast.CurrentWf.Snow is not null)
                {
                    snowCurr = new SnowPwf
                    {
                        H1 = weatherForecast.CurrentWf.Snow.H1
                    };
                }

                List<MinutelyPwf> minutely = null;

                if (weatherForecast.Minutely is not null)
                {
                    minutely = new List<MinutelyPwf>();
                    
                    foreach (var min in weatherForecast.Minutely)
                    {
                        var minute = new MinutelyPwf
                        {
                            DtLocal = DateTimeOffset.FromUnixTimeSeconds(
                                min.Dt + weatherForecast.TimezoneOffset).DateTime,
                            Precipitation = min.Precipitation
                        };
                    
                        minutely.Add(minute);
                    }
                }
                
                List<HourlyPwf> hourly = null;

                if (weatherForecast.Hourly is not null)
                {
                    foreach (var hour in weatherForecast.Hourly)
                    {
                        hourly = new List<HourlyPwf>();
                    
                        var hourlyPwf = new HourlyPwf
                        {
                            DtLocal = DateTimeOffset.FromUnixTimeSeconds(
                                hour.Dt + weatherForecast.TimezoneOffset).DateTime,
                            Temp = hour.Temp,
                            FeelsLike = hour.FeelsLike,
                            Pressure = hour.Pressure,
                            Humidity = hour.Humidity,
                            DewPoint = hour.DewPoint,
                            Uvi = hour.Uvi,
                            Clouds = hour.Clouds,
                            Visibility = hour.Visibility,
                            WindSpeed = hour.WindSpeed,
                            WindDeg = hour.WindDeg,
                            WindGust = hour.WindGust
                        };
                    
                        hourly.Add(hourlyPwf);
                    }
                }

                var point = new PointWeatherForecast
                {
                    Lat = weatherForecast.Lat,
                    Lon = weatherForecast.Lon,
                    Timezone = weatherForecast.Timezone,
                    TimezoneOffset = weatherForecast.TimezoneOffset,
                    Current = new CurrentPwf
                    {
                        DtUtc = DateTimeOffset.FromUnixTimeSeconds(
                            weatherForecast.CurrentWf.Dt).DateTime,
                        DtLocal = DateTimeOffset.FromUnixTimeSeconds(
                            weatherForecast.CurrentWf.Dt + weatherForecast.TimezoneOffset).DateTime,
                        SunriseLocal = DateTimeOffset.FromUnixTimeSeconds(
                            weatherForecast.CurrentWf.Sunrise + weatherForecast.TimezoneOffset).DateTime,
                        SunsetLocal = DateTimeOffset.FromUnixTimeSeconds(
                            weatherForecast.CurrentWf.Sunset + weatherForecast.TimezoneOffset).DateTime,
                        Temp = weatherForecast.CurrentWf.Temp,
                        FeelsLike = weatherForecast.CurrentWf.FeelsLike,
                        Pressure = weatherForecast.CurrentWf.Pressure,
                        Humidity = weatherForecast.CurrentWf.Humidity,
                        DewPoint = weatherForecast.CurrentWf.DewPoint,
                        Uvi = weatherForecast.CurrentWf.Uvi,
                        Clouds = weatherForecast.CurrentWf.Clouds,
                        VisibilityKm = (float?) Math.Round(weatherForecast.CurrentWf.Visibility / 1000.0f, 1,
                            MidpointRounding.ToZero),
                        WindSpeedKmPerH = (float?) Math.Round(
                            weatherForecast.CurrentWf.WindSpeed * 3.6f, 1, MidpointRounding.ToZero),
                        WindDir = new WindDir
                        {
                            DirTxt = weatherForecast.CurrentWf.WindDeg.DirectionTxt(),
                            DirArrow = weatherForecast.CurrentWf.WindDeg.DirectionArrow()
                        },
                        WindGustKmPerH = (float?) Math.Round(
                            weatherForecast.CurrentWf.WindGust ?? 0 * 3.6f, 1, MidpointRounding.ToZero) == 0
                            ? null : (float?) Math.Round(
                                weatherForecast.CurrentWf.WindGust ?? 0 * 3.6f, 1, MidpointRounding.ToZero),
                        Weathers = weatherCurrList,
                        Rain = rainCurr,
                        Snow = snowCurr,
                    },
                    Minutely = minutely,
                    Hourly = hourly,
                };

                return point;
            }

            return null;
        }
    }
}