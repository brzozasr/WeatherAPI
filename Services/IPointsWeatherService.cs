using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherAPI.Models;

namespace WeatherAPI.Services
{
    public interface IPointsWeatherService
    {
        Task<IEnumerable<PointsWeather>> GetPointsWeatherAsync(
            double lonLeft, double latBottom, double lonRight, double latTop, int zoom);
    }
}