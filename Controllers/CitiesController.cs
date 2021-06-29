using System;
using System.Collections.Generic;
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
        
        [HttpGet("Get/{city?}")]
        public async Task<IActionResult> GetCities([FromRoute] string city)
        {
            try
            {
                var result = Enumerable.Empty<City>();

                if (!string.IsNullOrEmpty(city))
                {
                    result = await _repository.GetByPartialMatch(city);
                }
                

                var counter = result.ToList();
                if (counter.Any())
                {
                    _logger.LogInformation("[{Time}]: The number of returned cities: {Count}", 
                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), counter.Count);
                    return Ok(result);
                }
                _logger.LogInformation("[{Time}]: The city was not found", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                return Ok(new {detail = "The city was not found"});
            }
            catch (Exception e)
            {
                _logger.LogError("[{Time}]: {Msg}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"),  e.Message);
                return Problem(e.Message, null, null, e.Source);
            }
        }
    }
}