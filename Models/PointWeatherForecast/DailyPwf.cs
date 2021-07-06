using System;
using System.Collections.Generic;

namespace WeatherAPI.Models.PointWeatherForecast
{
    public class DailyPwf
    {
        public DateTime? DtLocal { get; set; }
        public DateTime? SunriseLocal { get; set; }
        public DateTime? SunsetLocal { get; set; }
        public DateTime? MoonriseLocal { get; set; }
        public DateTime? MoonsetLocal { get; set; }
        public float? MoonPhase { get; set; }
        public TempPwf TempPwf { get; set; }
        public FeelsLikePwf FeelsLikePwf { get; set; }
        public float? Pressure { get; set; }
        public float? Humidity { get; set; }
        public float? DewPoint { get; set; }
        public float? WindSpeed { get; set; }
        public WindDir WindDir { get; set; }
        public float? WindGust { get; set; }
        public IEnumerable<WeatherPwf> Weathers { get; set; }
        public int? Clouds { get; set; }
        public float? Pop { get; set; }
        public float? Uvi { get; set; }
        public float? Rain { get; set; }
        public float? Snow { get; set; }
    }
}