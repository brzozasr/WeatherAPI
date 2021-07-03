using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherAPI.Services;
using WeatherAPI.Services.BBoxServices;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IPointsWeatherService _points;
        private readonly ILogger _logger;
        
        public WeatherController(ILogger<WeatherController> logger, IPointsWeatherService points)
        {
            _points = points;
            _logger = logger;
        }

        [HttpGet("Get/BBox/{lonLeft:double}/{latBottom:double}/{lonRight:double}/{latTop:double}/{zoom:int}")]
        public async Task<IActionResult> GetWeatherBox([FromRoute] double lonLeft, 
            [FromRoute] double latBottom, [FromRoute] double lonRight, 
            [FromRoute] double latTop, [FromRoute] int zoom)
        {
            try
            {
                var points = await _points.GetPointsWeatherAsync(
                    lonLeft, latBottom, lonRight, latTop, zoom);

                var listPoints = points.ToList();

                if (listPoints.Any() && 
                    listPoints.FirstOrDefault(x => x.Code == 200) != null)
                {
                    _logger.LogInformation("[{Time}]: Weather found for {Num} cities", 
                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), listPoints.Count);
                    return Ok(points);
                }

                if (listPoints.Any() && listPoints.FirstOrDefault()?.Code is not null)
                {
                    _logger.LogWarning("[{Time}]: Something went wrong, error code {Code}",
                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), listPoints.FirstOrDefault()?.Code);
                    return Ok(points);
                }

                if (listPoints.FirstOrDefault()?.Code is null)
                {
                    _logger.LogWarning("[{Time}]: There are no weather stations in the selected area",
                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                }

                return Ok(points);
            }
            catch (Exception e)
            {
                _logger.LogError("[{Time}]: {Msg}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), e.Message);
                return Problem(e.Message, null, null, e.Source);
            }
        }

        [HttpGet("Forecast/Get/Point/{lat:double}/{lon:double}/{units?}/{lang?}")]
        public async Task<IActionResult> GetPointWeatherForecast(
        [FromRoute] double lat, [FromRoute] double lon, [FromRoute] string units, 
        [FromRoute] string lang)
        {
            throw new NotImplementedException();
        }
    }
}