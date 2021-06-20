using Newtonsoft.Json;

namespace WeatherAPI.Models.WeatherBBox
{
    public class Snow
    {
        [JsonProperty("1h")] 
        public float? H1 { get; set; }
        
        [JsonProperty("3h")] 
        public float? H3 { get; set; }
    }
}