using Newtonsoft.Json;

namespace WeatherAPI.Models.WeatherBBox
{
    public class Coord
    {
        [JsonProperty("Lon")] 
        public double Lon { get; set; }
        
        [JsonProperty("Lat")] 
        public double Lat { get; set; }
    }
}