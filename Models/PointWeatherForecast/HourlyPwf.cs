using System;
using System.Collections.Generic;

namespace WeatherAPI.Models.PointWeatherForecast
{
    public class HourlyPwf
    {
        public DateTime? DtLocal { get; set; }
        public float? Temp { get; set; }
        public float? FeelsLike { get; set; }
        public float? Pressure { get; set; }
        public float? Humidity { get; set; }
        public float? DewPoint { get; set; }
        public float? Uvi { get; set; }
        public int? Clouds { get; set; }
        public float? VisibilityKm { get; set; }
        public float? WindSpeed { get; set; }
        public float? WindSpeedKm { get; set; }
        public WindDir WindDir { get; set; }
        public float? WindGust { get; set; }
        public IEnumerable<WeatherPwf> Weathers { get; set; }
        public float? Pop { get; set; }
        public RainPwf Rain { get; set; }
        public SnowPwf Snow { get; set; }
    }
}