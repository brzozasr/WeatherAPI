using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherAPI.Extensions;
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
                var point = new PointWeatherForecast
                {
                    Lat = weatherForecast.Lat,
                    Lon = weatherForecast.Lon,
                    Timezone = weatherForecast.Timezone,
                    TimezoneOffset = weatherForecast.TimezoneOffset,
                    Current = GetCurrent(weatherForecast),
                    Minutely = GetMinutely(weatherForecast),
                    Hourly = GetHourly(weatherForecast),
                };

                return point;
            }

            return null;
        }

        private CurrentPwf GetCurrent(WeatherForecast weather)
        {
            if (weather.CurrentWf is not null)
            {
                var current = new CurrentPwf
                {
                    DtUtc = DateTimeOffset.FromUnixTimeSeconds(
                        weather.CurrentWf.Dt).DateTime,
                    DtLocal = DateTimeOffset.FromUnixTimeSeconds(
                        weather.CurrentWf.Dt + weather.TimezoneOffset).DateTime,
                    SunriseLocal = DateTimeOffset.FromUnixTimeSeconds(
                        weather.CurrentWf.Sunrise + weather.TimezoneOffset).DateTime,
                    SunsetLocal = DateTimeOffset.FromUnixTimeSeconds(
                        weather.CurrentWf.Sunset + weather.TimezoneOffset).DateTime,
                    Temp = weather.CurrentWf.Temp,
                    FeelsLike = weather.CurrentWf.FeelsLike,
                    Pressure = weather.CurrentWf.Pressure,
                    Humidity = weather.CurrentWf.Humidity,
                    DewPoint = weather.CurrentWf.DewPoint,
                    Uvi = weather.CurrentWf.Uvi,
                    Clouds = weather.CurrentWf.Clouds,
                    VisibilityKm = (float?) Math.Round(weather.CurrentWf.Visibility / 1000.0f, 1,
                        MidpointRounding.ToZero),
                    WindSpeedKmPerH = (float?) Math.Round(
                        weather.CurrentWf.WindSpeed * 3.6f, 1, MidpointRounding.ToZero),
                    WindDir = new WindDir
                    {
                        DirTxt = weather.CurrentWf.WindDeg.DirectionTxt(),
                        DirArrow = weather.CurrentWf.WindDeg.DirectionArrow()
                    },
                    WindGustKmPerH = (float?) Math.Round(
                        weather.CurrentWf.WindGust ?? 0 * 3.6f, 1, MidpointRounding.ToZero) == 0
                        ? null
                        : (float?) Math.Round(
                            weather.CurrentWf.WindGust ?? 0 * 3.6f, 1, MidpointRounding.ToZero),
                    Weathers = GetWeather(weather.CurrentWf.Weathers),
                    Rain = GetRain(weather.CurrentWf.Rain),
                    Snow = GetSnow(weather.CurrentWf.Snow),
                };

                return current;
            }

            return null;
        }

        private IEnumerable<MinutelyPwf> GetMinutely(WeatherForecast weather)
        {
            List<MinutelyPwf> minutely = null;

            if (weather.Minutely is not null)
            {
                minutely = new List<MinutelyPwf>();
                    
                foreach (var min in weather.Minutely)
                {
                    var minute = new MinutelyPwf
                    {
                        DtLocal = DateTimeOffset.FromUnixTimeSeconds(
                            min.Dt + weather.TimezoneOffset).DateTime,
                        Precipitation = min.Precipitation
                    };
                    
                    minutely.Add(minute);
                }
            }

            return minutely;
        }

        private IEnumerable<HourlyPwf> GetHourly(WeatherForecast weather)
        {
            List<HourlyPwf> hourly = null;

            if (weather.Hourly is not null)
            {
                foreach (var hour in weather.Hourly)
                {
                    hourly = new List<HourlyPwf>();
                    
                    var hourlyPwf = new HourlyPwf
                    {
                        DtLocal = DateTimeOffset.FromUnixTimeSeconds(
                            hour.Dt + weather.TimezoneOffset).DateTime,
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

            return hourly;
        }

        private IEnumerable<WeatherPwf> GetWeather(IEnumerable<WeatherWf> weatherWf)
        {
            if (weatherWf is not null)
            {
                var weatherList = new List<WeatherPwf>();
                    
                foreach (var weather in weatherWf)
                {
                    var weatherCurr = new WeatherPwf
                    {
                        Id = weather.Id,
                        Main = weather.Main,
                        Description = weather.Description.FirstLetterToUpper(),
                        Icon = weather.Icon
                    };
                    
                    weatherList.Add(weatherCurr);
                }

                return weatherList;
            }

            return null;
        }

        private RainPwf GetRain(RainWf rainWf)
        {
            if (rainWf is not null)
            {
                var rain = new RainPwf
                {
                    H1 = rainWf.H1
                };
                return rain;
            }

            return null;
        }

        private SnowPwf GetSnow(SnowWf snowWf)
        {
            if (snowWf is not null)
            {
                var snow = new SnowPwf
                {
                    H1 = snowWf.H1
                };
                return snow;
            }

            return null;
        }
    }
}