using Newtonsoft.Json;

namespace WeatherAPI.Models.WeatherHistorical
{
    public class SnowWh
    {
        [JsonProperty("1h")]
        public float? H1 { get; set; }
    }
}