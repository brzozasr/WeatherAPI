namespace WeatherAPI.Models.PointWeatherHistorical
{
    public class AggFeelsLike : Aggregation
    {
        public float? FeelsLikeMin { get; set; }
        public float? FeelsLikeMax { get; set; }
    }
}