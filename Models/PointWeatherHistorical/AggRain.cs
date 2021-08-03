namespace WeatherAPI.Models.PointWeatherHistorical
{
    public class AggRain : Aggregation
    {
        public float? RainMin { get; set; }
        public float? RainMax { get; set; }
    }
}