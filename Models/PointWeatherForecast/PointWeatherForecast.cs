using System.Collections.Generic;

namespace WeatherAPI.Models.PointWeatherForecast
{
    public class PointWeatherForecast
    {
        public double? Lat { get; set; }
        public double? Lon { get; set; } 
        public string Timezone { get; set; }
        public long? TimezoneOffset { get; set; }
        public CurrentPwf Current { get; set; }
        public IEnumerable<MinutelyPwf> Minutely { get; set; }
        public IEnumerable<HourlyPwf> Hourly { get; set; }
    }
}