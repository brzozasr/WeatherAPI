using System.Collections.Generic;
using NetTopologySuite.Operation.Overlay;
using NetTopologySuite.Precision;

namespace WeatherAPI.Models.PointWeatherHistorical
{
    public class PointWeatherHistorical
    {
        public int? StatusCode { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; } 
        public string Timezone { get; set; }
        public long? TimezoneOffset { get; set; }
        public AggTemp AggTemp { get; set; }
        public AggFeelsLike AggFeelsLike { get; set; }
        public AggPressure AggPressure { get; set; }
        public AggHumidity AggHumidity { get; set; }
        public AggDewPoint AggDewPoint { get; set; }
        public AggUvi AggUvi { get; set; }
        public AggVisibility AggVisibility { get; set; }
        public AggWindSpeed AggWindSpeed { get; set; }
        public AggRain AggRain { get; set; }
        public AggSnow AggSnow { get; set; }
        public List<HourlyPwh> Hourly { get; set; }
        

        public PointWeatherHistorical()
        {
            Hourly = new List<HourlyPwh>();
        }
    }
}