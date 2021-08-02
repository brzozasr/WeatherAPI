using System.Threading.Tasks;
using WeatherAPI.Models.WeatherHistorical;

namespace WeatherAPI.Services.HistoricalServices
{
    public interface IOpenWeatherHistoricalService
    {
        Task<WeatherHistorical> GetWeatherHistoricalAsync(
            double lat, double lon, long unixTime,  string units = "metric", string lang = "en");
    }
}