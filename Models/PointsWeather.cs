namespace WeatherAPI.Models
{
    public class PointsWeather
    {
        public int? Code { get; set; }
        
        public long? CityId { get; set; }
        
        public string CityName { get; set; }
        
        public double? Lon { get; set; }
        
        public double? Lat { get; set; }
        
        public float? Temp { get; set; }
        
        public string Icon { get; set; }
    }
}