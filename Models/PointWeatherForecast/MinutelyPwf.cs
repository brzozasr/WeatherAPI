using System;
using Newtonsoft.Json;

namespace WeatherAPI.Models.PointWeatherForecast
{
    public class MinutelyPwf
    {
        public DateTime? DtLocal { get; set; }
        public float? Precipitation { get; set; }
    }
}