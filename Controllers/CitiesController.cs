using Microsoft.AspNetCore.Mvc;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : Controller
    {
        [HttpGet("Get/{city}")]
        public IActionResult GetCities(string city)
        {
            return Ok();
        }
    }
}