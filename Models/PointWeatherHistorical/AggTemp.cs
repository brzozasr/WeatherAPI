namespace WeatherAPI.Models.PointWeatherHistorical
{
    public class AggTemp : Aggregation
    {
        public float? TempMin { get; set; }
        public float? TempMax { get; set; }
    }
}