using System.Threading.Tasks;
using WeatherAPI.Models.PointWeatherHistorical;

namespace WeatherAPI.Services.HistoricalServices
{
    public interface IPointWeatherHistoricalService
    {
        Task<PointWeatherHistorical> GetPointWeatherHistoricalAsync(
            double lat, double lon, string units = "metric", string lang = "en");
    }
}