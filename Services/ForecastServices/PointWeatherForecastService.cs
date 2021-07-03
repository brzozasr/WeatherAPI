using System.Threading.Tasks;
using WeatherAPI.Models;

namespace WeatherAPI.Services.ForecastServices
{
    public class PointWeatherForecastService : IPointWeatherForecastService
    {
        public Task<PointWeatherForecast> GetPointWeatherForecastAsync(double lat, double lon, 
            string units = "metric", string lang = "en")
        {
            throw new System.NotImplementedException();
        }
    }
}