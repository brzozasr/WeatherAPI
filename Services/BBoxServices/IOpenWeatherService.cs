using System.Threading.Tasks;
using WeatherAPI.Models.WeatherBBox;

namespace WeatherAPI.Services.BBoxServices
{
    public interface IOpenWeatherService
    {
        Task<WeatherBoxRoot> GetOpenWeatherBoxAsync(
            double lonLeft, double latBottom, double lonRight, double latTop, int zoom);
    }
}