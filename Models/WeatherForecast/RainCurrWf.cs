using Newtonsoft.Json;

namespace WeatherAPI.Models.WeatherForecast
{
    public class RainCurrWf
    {
        [JsonProperty("1h")]
        public float? H1 { get; set; }
    }
}