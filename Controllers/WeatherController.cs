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
        private readonly IOpenWeatherService _openWeatherService;
        private ILogger _logger;
        
        public WeatherController(ILogger<WeatherController> logger, IOpenWeatherService openWeatherService)
        {
            _openWeatherService = openWeatherService;
            _logger = logger;
        }

        [HttpGet("Get/BBox/Weather")]
        public async Task<IActionResult> GetWeatherBox()
        {
            return Ok(await _openWeatherService.GetOpenWeatherBoxAsync(18.149414062500004,51.037939894299356,23.8897705078125,53.396432127095984,8));
        }
    }
}