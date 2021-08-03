namespace WeatherAPI.Models.PointWeatherHistorical
{
    public class AggDewPoint : Aggregation
    {
        public float? DewPointMin { get; set; }
        public float? DewPointMax { get; set; }
    }
}