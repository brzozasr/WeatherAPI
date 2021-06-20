using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherAPI.Services;

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
        public async Task<IActionResult> GetWeatherBox(
            double lonLeft, double latBottom, double lonRight, double latTop, int zoom)
        {
            try
            {
                var points = await _points.GetPointsWeatherAsync(
                    lonLeft, latBottom, lonRight, latTop, zoom);

                var listPoints = points.ToList();

                if (listPoints.Any() && 
                    listPoints.FirstOrDefault(x => x.Code == 200) != null)
                {
                    _logger.LogInformation("Weather found for {Num} cities", listPoints.Count);
                    return Ok(points);
                }

                _logger.LogWarning("Something went wrong, error code {Code}",
                    listPoints.FirstOrDefault()?.Code);
                return Ok(points);
            }
            catch (Exception e)
            {
                _logger.LogError("{Msg}", e.Message);
                return Problem(e.Message, null, null, e.Source);
            }
        }
    }
}