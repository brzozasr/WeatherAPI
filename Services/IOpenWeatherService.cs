using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherAPI.Models;
using WeatherAPI.Models.WeatherBBox;

namespace WeatherAPI.Services
{
    public interface IOpenWeatherService
    {
        Task<WeatherBoxRoot> GetOpenWeatherBoxAsync(
            double lonLeft, double latBottom, double lonRight, double latTop, int zoom);
    }
}