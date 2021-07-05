using Newtonsoft.Json;

namespace WeatherAPI.Models.WeatherForecast
{
    public class TempWf
    {
        [JsonProperty("day")]
        public float Day { get; set; }
        
        [JsonProperty("min")]
        public float Min { get; set; }
        
        [JsonProperty("max")]
        public float Max { get; set; }
        
        [JsonProperty("night")]
        public float Night { get; set; }
        
        [JsonProperty("eve")]
        public float Eve { get; set; }
        
        [JsonProperty("morn")]
        public float Morn { get; set; }
    }
}