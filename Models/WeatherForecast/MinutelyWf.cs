using Newtonsoft.Json;

namespace WeatherAPI.Models.WeatherForecast
{
    public class MinutelyWf
    {
        [JsonProperty("dt")]
        public long Dt { get; set; }
        
        [JsonProperty("precipitation")]
        public float Precipitation { get; set; }
    }
}