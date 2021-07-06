using System;
using System.Collections.Generic;

namespace WeatherAPI.Models.PointWeatherForecast
{
    public class CurrentPwf
    {
        public DateTime? DtUtc { get; set; }
        public DateTime? DtLocal { get; set; }
        public DateTime? SunriseLocal { get; set; }
        public DateTime? SunsetLocal { get; set; }
        public float? Temp { get; set; }
        public float? FeelsLike { get; set; }
        public float? Pressure { get; set; }
        public float? Humidity { get; set; }
        public float? DewPoint { get; set; }
        public float? Uvi { get; set; }
        public int? Clouds { get; set; }
        public float? VisibilityKm { get; set; }
        public float? WindSpeed { get; set; }
        public WindDir WindDir { get; set; }
        public float? WindGust { get; set; }
        public IEnumerable<WeatherPwf> Weathers { get; set; }
        public RainPwf Rain { get; set; }
        public SnowPwf Snow { get; set; }
    }
}