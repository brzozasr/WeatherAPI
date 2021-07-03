using System.Threading.Tasks;
using WeatherAPI.Models;

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
            var pointWeatherForecast = _openWeatherForecast.GetWeatherForecastAsync(
                lat, lon, units, lang);

            return new PointWeatherForecast();
        }
    }
}