using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherAPI.Services.BBoxServices;
using WeatherAPI.Services.ForecastServices;
using WeatherAPI.Services.HistoricalServices;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IPointsWeatherService _points;
        private readonly IPointWeatherForecastService _forecast;
        private readonly IPointWeatherHistoricalService _historical;
        private readonly ILogger _logger;

        public WeatherController(ILogger<WeatherController> logger, IPointsWeatherService points,
            IPointWeatherForecastService forecast, IPointWeatherHistoricalService historical)
        {
            _points = points;
            _forecast = forecast;
            _historical = historical;
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
                    _logger.LogInformation("[{Time}]: (Map) Weather found for {Num} cities",
                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), listPoints.Count);
                    return Ok(points);
                }

                if (listPoints.Any() && listPoints.FirstOrDefault()?.Code is not null)
                {
                    _logger.LogWarning("[{Time}]: (Map) Something went wrong, error code {Code}",
                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), listPoints.FirstOrDefault()?.Code);
                    return Ok(points);
                }

                if (listPoints.FirstOrDefault()?.Code is null)
                {
                    _logger.LogWarning("[{Time}]: (Map) There are no weather stations in the selected area",
                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                }

                return Ok(points);
            }
            catch (Exception e)
            {
                _logger.LogError("[{Time}]: (Map) {Msg}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), e.Message);
                return Problem(e.Message, null, null, e.Source);
            }
        }

        [HttpGet("Forecast/Get/Point/{lat:double}/{lon:double}/{units?}/{lang?}")]
        public async Task<IActionResult> GetPointWeatherForecast(
            [FromRoute] double lat, [FromRoute] double lon, [FromRoute] string units = "metric",
            [FromRoute] string lang = "en")
        {
            try
            {
                var point = await _forecast.GetPointWeatherForecastAsync(lat, lon, units, lang);

                if (point is not null && point.StatusCode == 200)
                {
                    _logger.LogInformation(
                        "[{Time}]: (Forecast) The weather for a point with coordinates ({Lat}, {Lon}) and a timezone {Timezone}",
                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), lat, lon, point.Timezone);

                    return Ok(point);
                }

                if (point is not null && point.StatusCode == 204)
                {
                    _logger.LogWarning("[{Time}]: (Forecast) 204 No Content, the HTTP response has no content",
                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                }
                else if (point is null)
                {
                    _logger.LogWarning("[{Time}]: (Forecast) Something went wrong and the response is null",
                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                }
                else
                {
                    _logger.LogWarning("[{Time}]: (Forecast) Something went wrong, unknown error",
                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                }

                return Ok(point);
            }
            catch (Exception e)
            {
                _logger.LogError("[{Time}]: (Forecast) {Msg}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), e.Message);
                return Problem(e.Message, null, 500, e.Source);
            }
        }

        [HttpGet("Historical/Get/Point/{lat:double}/{lon:double}/{units?}/{lang?}")]
        public async Task<IActionResult> GetPointWeatherHistorical(
            [FromRoute] double lat, [FromRoute] double lon, [FromRoute] string units = "metric",
            [FromRoute] string lang = "en")
        {
            try
            {
                var point = await _historical.GetPointWeatherHistoricalAsync(lat, lon, units, lang);

                if (point is not null && point.StatusCode == 200)
                {
                    _logger.LogInformation(
                        "[{Time}]: (Historical) The weather for a point with coordinates ({Lat}, {Lon}) and a timezone {Timezone}",
                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), lat, lon, point.Timezone);

                    return Ok(point);
                }

                if (point is null)
                {
                    _logger.LogWarning("[{Time}]: (Historical) Something went wrong and the response is null",
                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                }
                else
                {
                    _logger.LogWarning("[{Time}]: (Historical) Something went wrong, unknown error",
                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                }

                return Ok(point);
            }
            catch (Exception e)
            {
                _logger.LogError("[{Time}]: (Historical) {Msg}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), e.Message);
                return Problem(e.Message, null, 500, e.Source);
            }
        }
    }
}