using System;
using System.Threading.Tasks;
using WeatherAPI.Extensions;
using WeatherAPI.Models;
using WeatherAPI.Models.PointWeatherForecast;

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
                            ? null
                            : (float?) Math.Round(
                                weatherForecast.CurrentWf.WindGust ?? 0 * 3.6f, 1, MidpointRounding.ToZero)
                    }
                };

                return point;
            }

            return null;
        }
    }
}