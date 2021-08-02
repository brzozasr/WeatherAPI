using System.Threading.Tasks;
using WeatherAPI.Models.WeatherForecast;

namespace WeatherAPI.Services.HistoricalServices
{
    public interface IOpenWeatherHistoricalService
    {
        Task<WeatherForecast> GetWeatherHistoricalAsync(
            double lat, double lon, long unixTime,  string units = "metric", string lang = "en");
    }
}