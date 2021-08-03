namespace WeatherAPI.Models.PointWeatherHistorical
{
    public class AggHumidity : Aggregation
    {
        public float? HumidityMin { get; set; }
        public float? HumidityMax { get; set; }
    }
}