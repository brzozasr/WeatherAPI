using System.Threading.Tasks;
using WeatherAPI.Models;
using WeatherAPI.Models.PointWeatherForecast;

namespace WeatherAPI.Services.ForecastServices
{
    public interface IPointWeatherForecastService
    {
        Task<PointWeatherForecast> GetPointWeatherForecastAsync(
            double lat, double lon, string units = "metric", string lang = "en");
    }
}