using System.Threading.Tasks;
using WeatherAPI.Models.WeatherForecast;

namespace WeatherAPI.Services.ForecastServices
{
    public interface IOpenWeatherForecastService
    {
        Task<WeatherForecast> GetWeatherForecastAsync(
            double lat, double lon, string units = "metric", string lang = "en");
    }
}