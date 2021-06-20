using Newtonsoft.Json;

namespace WeatherAPI.Models.WeatherBBox
{
    public class Wind
    {
        [JsonProperty("speed")] 
        public float Speed { get; set; }
        
        [JsonProperty("deg")] 
        public int Deg { get; set; }
    }
}