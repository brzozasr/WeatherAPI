using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherAPI.Models;

namespace WeatherAPI.Services
{
    public class PointsWeatherService : IPointsWeatherService
    {
        private IOpenWeatherService _openWeatherService;
        
        public PointsWeatherService(IOpenWeatherService openWeatherService)
        {
            _openWeatherService = openWeatherService;
        }
        
        public async Task<IEnumerable<PointsWeather>> GetPointsWeatherAsync(
            double lonLeft, double latBottom, double lonRight, double latTop, int zoom)
        {
            var pointsWeather = await _openWeatherService.GetOpenWeatherBoxAsync(
                lonLeft, latBottom, lonRight, latTop, zoom);

            var listOfPoints = new List<PointsWeather>();

            if (pointsWeather is not null && pointsWeather.Code == 200)
            {
                foreach (var item in pointsWeather.ListOfCities)
                {
                    var city = new PointsWeather
                    {
                        Code = pointsWeather.Code,
                        CityId = item.Id,
                        CityName = item.Name,
                        Lon = item.Coords.Lon,
                        Lat = item.Coords.Lat,
                        Temp = item.Mains.Temp,
                        Icon = item.Weathers[0].Icon
                    };
                    
                    listOfPoints.Add(city);
                }

                return listOfPoints;
            }

            var code = new PointsWeather {Code = pointsWeather?.Code};
            listOfPoints.Add(code);

            return listOfPoints;
        }
    }
}