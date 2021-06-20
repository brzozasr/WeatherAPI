using Newtonsoft.Json;

namespace WeatherAPI.Models.WeatherBBox
{
    public class Main
    {
        [JsonProperty("temp")] 
        public float Temp { get; set; }
        
        [JsonProperty("feels_like")] 
        public float FeelsLike { get; set; }
        
        [JsonProperty("temp_min")] 
        public float TempMin { get; set; }
        
        [JsonProperty("temp_max")] 
        public float TempMax { get; set; }
        
        [JsonProperty("pressure")] 
        public int Pressure { get; set; }
        
        [JsonProperty("humidity")] 
        public int Humidity { get; set; }
    }
}