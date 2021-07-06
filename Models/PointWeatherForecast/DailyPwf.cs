using System;

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
    }
}