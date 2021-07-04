using Newtonsoft.Json;

namespace WeatherAPI.Models.WeatherForecast
{
    public class SnowCurrWf
    {
        [JsonProperty("1h")]
        public float? H1 { get; set; }
    }
}