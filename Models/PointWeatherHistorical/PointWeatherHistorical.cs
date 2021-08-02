namespace WeatherAPI.Models.PointWeatherHistorical
{
    public class PointWeatherHistorical
    {
        public int? StatusCode { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; } 
        public string Timezone { get; set; }
        public long? TimezoneOffset { get; set; }
    }
}