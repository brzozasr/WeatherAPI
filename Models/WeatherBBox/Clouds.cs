using Newtonsoft.Json;

namespace WeatherAPI.Models.WeatherBBox
{
    public class Clouds
    {
        [JsonProperty("today")] 
        public int Today { get; set; }
    }
}