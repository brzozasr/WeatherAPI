namespace WeatherAPI.Models.PointWeatherHistorical
{
    public class AggPressure : Aggregation
    {
        public float? PressureMin { get; set; }
        public float? PressureMax { get; set; }
    }
}