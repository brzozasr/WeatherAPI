using Newtonsoft.Json;

namespace WeatherAPI.Models.WeatherForecast
{
    public class WeatherDayWf
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("main")]
        public string Main { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}