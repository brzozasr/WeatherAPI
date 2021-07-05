using System;

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
        public int? Visibility { get; set; }
        public float? WindSpeed { get; set; }
        public int? WindDeg { get; set; }
        public float? WindGust { get; set; }
    }
}