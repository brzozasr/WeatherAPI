using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeatherAPI.Extensions;
using WeatherAPI.Models;
using WeatherAPI.Repositories;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private IRepository<City> _repository;
        private ILogger _logger;
        public CitiesController(ILogger<CitiesController> logger, IRepository<City> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        
        [HttpGet("Get/{city}")]
        public async Task<IActionResult> GetCities(string city)
        {
            try
            {
                var result = await _repository.GetByPartialMatch(city);

                var counter = result.ToList();
                if (counter.Any())
                {
                    _logger.LogInformation("The number of returned cities: {Count}", counter.Count);
                    return Ok(result);
                }
                _logger.LogInformation("The city was not found");
                return Ok(new {detail = "The city was not found"});
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, null, e.Source);
            }
        }
    }
}