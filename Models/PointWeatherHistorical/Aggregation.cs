using System;

namespace WeatherAPI.Models.PointWeatherHistorical
{
    public abstract class Aggregation
    {
        public DateTime? DtMin { get; set; }
        public DateTime? DtLocalMin { get; set; }
        public DateTime? DtMax { get; set; }
        public DateTime? DtLocalMax { get; set; }
    }
}