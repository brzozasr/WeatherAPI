namespace WeatherAPI.Models.PointWeatherHistorical
{
    public class AggWindSpeed : Aggregation
    {
        public float? WindSpeedMin { get; set; }
        public float? WindSpeedMax { get; set; }
    }
}