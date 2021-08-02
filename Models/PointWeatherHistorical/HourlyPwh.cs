using System;
using System.Collections.Generic;

namespace WeatherAPI.Models.PointWeatherHistorical
{
    public class HourlyPwh
    {
        public DateTime? Dt { get; set; }
        public DateTime? DtLocal { get; set; }
        public float? Temp { get; set; }
        public float? FeelsLike { get; set; }
        public float? Pressure { get; set; }
        public float? Humidity { get; set; }
        public float? DewPoint { get; set; }
        public float? Uvi { get; set; }
        public int? Clouds { get; set; }
        public int? Visibility { get; set; }
        public float? VisibilityKm { get; set; }
        public float? WindSpeed { get; set; }
        public float? WindSpeedKmPerH { get; set; }
        public float? WindGust { get; set; }
        public int? WindDeg { get; set; }
        public WindDirPwh WindDir { get; set; }
        public IEnumerable<WeatherPwh> Weathers { get; set; }
        public RainPwh Rain { get; set; }
        public SnowPwh Snow { get; set; }
    }
}