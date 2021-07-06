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
                    Daily = GetDaily(weatherForecast),
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
                    DtUtc = UnixTimeToDateTime(weather.CurrentWf.Dt),
                    DtLocal = UnixTimeToDateTimeLocal(weather.CurrentWf.Dt, weather.TimezoneOffset),
                    SunriseLocal = UnixTimeToDateTimeLocal(weather.CurrentWf.Sunrise, weather.TimezoneOffset),
                    SunsetLocal = UnixTimeToDateTimeLocal(weather.CurrentWf.Sunset, weather.TimezoneOffset),
                    Temp = weather.CurrentWf.Temp,
                    FeelsLike = weather.CurrentWf.FeelsLike,
                    Pressure = weather.CurrentWf.Pressure,
                    Humidity = weather.CurrentWf.Humidity,
                    DewPoint = weather.CurrentWf.DewPoint,
                    Uvi = weather.CurrentWf.Uvi,
                    Clouds = weather.CurrentWf.Clouds,
                    VisibilityKm = MToKm(weather.CurrentWf.Visibility),
                    WindSpeedKmPerH = MPerSecToKmPerH(weather.CurrentWf.WindSpeed),
                    WindDir = GetWindDirection(weather.CurrentWf.WindDeg),
                    WindGustKmPerH = MPerSecToKmPerH(weather.CurrentWf.WindGust),
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
            if (weather.Minutely is not null)
            {
                var minutely = new List<MinutelyPwf>();

                foreach (var min in weather.Minutely)
                {
                    var minute = new MinutelyPwf
                    {
                        DtLocal = UnixTimeToDateTimeLocal(min.Dt, weather.TimezoneOffset),
                        Precipitation = min.Precipitation
                    };
                    
                    minutely.Add(minute);
                }

                return minutely;
            }

            return null;
        }

        private IEnumerable<HourlyPwf> GetHourly(WeatherForecast weather)
        {
            if (weather.Hourly is not null)
            {
                var hourly = new List<HourlyPwf>();

                foreach (var hour in weather.Hourly)
                {
                    var hourlyPwf = new HourlyPwf
                    {
                        DtLocal = UnixTimeToDateTimeLocal(hour.Dt, weather.TimezoneOffset),
                        Temp = hour.Temp,
                        FeelsLike = hour.FeelsLike,
                        Pressure = hour.Pressure,
                        Humidity = hour.Humidity,
                        DewPoint = hour.DewPoint,
                        Uvi = hour.Uvi,
                        Clouds = hour.Clouds,
                        VisibilityKm = MToKm(hour.Visibility),
                        WindSpeedKmPerH = MPerSecToKmPerH(hour.WindSpeed),
                        WindDir = GetWindDirection(hour.WindDeg),
                        WindGustKmPerH = MPerSecToKmPerH(hour.WindGust),
                        Weathers = GetWeather(hour.Weathers),
                        Pop = hour.Pop,
                        Rain = GetRain(hour.Rain),
                        Snow = GetSnow(hour.Snow)
                    };
                    
                    hourly.Add(hourlyPwf);
                }

                return hourly;
            }

            return null;
        }

        private IEnumerable<DailyPwf> GetDaily(WeatherForecast weather)
        {
            if (weather.Daily is not null)
            {
                var daily = new List<DailyPwf>();

                foreach (var day in weather.Daily)
                {
                    var dailyPwf = new DailyPwf
                    {
                        DtLocal = UnixTimeToDateTimeLocal(day.Dt, weather.TimezoneOffset),
                        SunriseLocal = UnixTimeToDateTimeLocal(day.Sunrise, weather.TimezoneOffset),
                        SunsetLocal = UnixTimeToDateTimeLocal(day.Sunset, weather.TimezoneOffset),
                        MoonriseLocal = UnixTimeToDateTimeLocal(day.Moonrise, weather.TimezoneOffset),
                        MoonsetLocal = UnixTimeToDateTimeLocal(day.Moonset, weather.TimezoneOffset),
                        MoonPhase = day.MoonPhase,
                    };
                    daily.Add(dailyPwf);
                }

                return daily;
            }

            return null;
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

        private WindDir GetWindDirection(int direction)
        {
            var wind = new WindDir
            {
                DirTxt = direction.DirectionTxt(),
                DirArrow = direction.DirectionArrow()
            };
            return wind;
        }

        private DateTime? UnixTimeToDateTime(long unixTime)
        {
            if (unixTime > 0)
            {
                return DateTimeOffset.FromUnixTimeSeconds(unixTime).DateTime;
            }

            return null;
        }
        
        private DateTime? UnixTimeToDateTimeLocal(long unixTime, long timezoneOffset)
        {
            if (unixTime > 0)
            {
                var localTime = unixTime + timezoneOffset;
                return DateTimeOffset.FromUnixTimeSeconds(localTime).DateTime;
            }

            return null;
        }

        private float? MPerSecToKmPerH(float? mPerSec)
        {
            return (float?) Math.Round(
                mPerSec ?? 0 * 3.6f, 2, MidpointRounding.ToZero) == 0
                ? null
                : (float?) Math.Round(
                    mPerSec ?? 0 * 3.6f, 2, MidpointRounding.ToZero);
        }
        
        private float? MPerSecToKmPerH(float mPerSec)
        {
            return (float?) Math.Round(mPerSec * 3.6f, 2, MidpointRounding.ToZero);
        }

        private float? MToKm(int meters)
        {
            return (float?) Math.Round(meters / 1000.0f, 2,
                MidpointRounding.ToZero);
        }
    }
}