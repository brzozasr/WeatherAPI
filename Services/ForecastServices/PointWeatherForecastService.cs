using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherAPI.Extensions;
using WeatherAPI.Models.PointWeatherForecast;
using WeatherAPI.Models.WeatherForecast;
using WeatherAPI.Utilities;

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

            if (weatherForecast is not null && weatherForecast.Timezone is not null)
            {
                var point = new PointWeatherForecast
                {
                    StatusCode = 200,
                    Lat = weatherForecast.Lat,
                    Lon = weatherForecast.Lon,
                    Timezone = weatherForecast.Timezone,
                    TimezoneOffset = weatherForecast.TimezoneOffset,
                    Current = GetCurrent(weatherForecast),
                    Minutely = GetMinutely(weatherForecast),
                    Hourly = GetHourly(weatherForecast),
                    Daily = GetDaily(weatherForecast),
                    Alerts = GetAlerts(weatherForecast)
                };

                return point;
            }

            if (weatherForecast is not null && weatherForecast.Timezone is null)
            {
                var point = new PointWeatherForecast
                {
                    StatusCode = 204,
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
                    DtUtc = Util.UnixTimeToDateTime(weather.CurrentWf.Dt),
                    DtLocal = Util.UnixTimeToDateTimeLocal(weather.CurrentWf.Dt, weather.TimezoneOffset),
                    SunriseLocal = Util.UnixTimeToDateTimeLocal(weather.CurrentWf.Sunrise, weather.TimezoneOffset),
                    SunsetLocal = Util.UnixTimeToDateTimeLocal(weather.CurrentWf.Sunset, weather.TimezoneOffset),
                    Temp = weather.CurrentWf.Temp,
                    FeelsLike = weather.CurrentWf.FeelsLike,
                    Pressure = weather.CurrentWf.Pressure,
                    Humidity = weather.CurrentWf.Humidity,
                    DewPoint = weather.CurrentWf.DewPoint,
                    Uvi = weather.CurrentWf.Uvi,
                    Clouds = weather.CurrentWf.Clouds,
                    VisibilityKm = Util.MToKm(weather.CurrentWf.Visibility),
                    WindSpeed = weather.CurrentWf.WindSpeed,
                    WindSpeedKmPerH = Util.MPerSecToKmPerH(weather.CurrentWf.WindSpeed),
                    WindBeaufortScale = weather.CurrentWf.WindSpeed.BeaufortScale(),
                    WindDir = GetWindDirection(weather.CurrentWf.WindDeg),
                    WindGust = weather.CurrentWf.WindGust,
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
                        DtLocal = Util.UnixTimeToDateTimeLocal(min.Dt, weather.TimezoneOffset),
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
                        DtLocal = Util.UnixTimeToDateTimeLocal(hour.Dt, weather.TimezoneOffset),
                        Temp = hour.Temp,
                        FeelsLike = hour.FeelsLike,
                        Pressure = hour.Pressure,
                        Humidity = hour.Humidity,
                        DewPoint = hour.DewPoint,
                        Uvi = hour.Uvi,
                        Clouds = hour.Clouds,
                        VisibilityKm = Util.MToKm(hour.Visibility),
                        WindSpeed = hour.WindSpeed,
                        WindDir = GetWindDirection(hour.WindDeg),
                        WindGust = hour.WindGust,
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
                        DtLocal = Util.UnixTimeToDateTimeLocal(day.Dt, weather.TimezoneOffset),
                        SunriseLocal = Util.UnixTimeToDateTimeLocal(day.Sunrise, weather.TimezoneOffset),
                        SunsetLocal = Util.UnixTimeToDateTimeLocal(day.Sunset, weather.TimezoneOffset),
                        MoonriseLocal = Util.UnixTimeToDateTimeLocal(day.Moonrise, weather.TimezoneOffset),
                        MoonsetLocal = Util.UnixTimeToDateTimeLocal(day.Moonset, weather.TimezoneOffset),
                        MoonPhase = day.MoonPhase,
                        Temp = GetTemp(day),
                        FeelsLike = GetFeelsLike(day),
                        Pressure = day.Pressure,
                        Humidity = day.Humidity,
                        DewPoint = day.DewPoint,
                        WindSpeed = day.WindSpeed,
                        WindSpeedKmPerH = Util.MPerSecToKmPerH(day.WindSpeed),
                        WindBeaufortScale = day.WindSpeed.BeaufortScale(),
                        WindDir = GetWindDirection(day.WindDeg),
                        WindGust = day.WindGust,
                        Weathers = GetWeather(day.Weathers),
                        Clouds = day.Clouds,
                        Pop = day.Pop,
                        Uvi = day.Uvi,
                        Rain = day.Rain,
                        Snow = day.Snow
                    };
                    daily.Add(dailyPwf);
                }

                return daily;
            }

            return null;
        }

        private IEnumerable<AlertsPwf> GetAlerts(WeatherForecast weather)
        {
            if (weather.AlertsWf is not null)
            {
                var alerts = new List<AlertsPwf>();

                foreach (var alert in weather.AlertsWf)
                {
                    var alertsPwf = new AlertsPwf
                    {
                        SenderName = alert.SenderName,
                        Event = alert.Event,
                        Start = Util.UnixTimeToDateTimeLocal(alert.Start, weather.TimezoneOffset),
                        End = Util.UnixTimeToDateTimeLocal(alert.End, weather.TimezoneOffset),
                        Description = alert.Description,
                        Tags = GetAlertTags(alert)
                    };
                    alerts.Add(alertsPwf);
                }

                return alerts;
            }

            return null;
        }

        private IEnumerable<string> GetAlertTags(AlertsWf alert)
        {
            if (alert is not null)
            {
                var tags = new List<string>();
                
                foreach (var tag in alert.Tags)
                {
                    tags.Add(tag);
                }

                return tags;
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

        private Temp GetTemp(DailyWf daily)
        {
            if (daily is not null)
            {
                var temp = new Temp
                {
                    Day = daily.TempWf.Day,
                    Min = daily.TempWf.Min,
                    Max = daily.TempWf.Max,
                    Night = daily.TempWf.Night,
                    Eve = daily.TempWf.Eve,
                    Morn = daily.TempWf.Morn
                };
                return temp;
            }

            return null;
        }
        
        private FeelsLike GetFeelsLike(DailyWf daily)
        {
            if (daily is not null)
            {
                var feelsLike = new FeelsLike
                {
                    Day = daily.FeelsLikeWf.Day,
                    Night = daily.FeelsLikeWf.Night,
                    Eve = daily.FeelsLikeWf.Eve,
                    Morn = daily.FeelsLikeWf.Morn
                };
                return feelsLike;
            }

            return null;
        }
    }
}