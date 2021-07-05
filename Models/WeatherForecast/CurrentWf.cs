using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherAPI.Models.WeatherForecast
{
    public class CurrentWf
    {
        [JsonProperty("dt")]
        public long Dt { get; set; }
        
        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }
        
        [JsonProperty("sunset")]
        public long Sunset { get; set; }
        
        [JsonProperty("temp")]
        public float Temp { get; set; }
        
        [JsonProperty("feels_like")]
        public float FeelsLike { get; set; }
        
        [JsonProperty("pressure")]
        public float Pressure { get; set; }
        
        [JsonProperty("humidity")]
        public float Humidity { get; set; }
        
        [JsonProperty("dew_point")]
        public float DewPoint { get; set; }
        
        [JsonProperty("uvi")]
        public float Uvi { get; set; }
        
        [JsonProperty("clouds")]
        public int Clouds { get; set; }
        
        [JsonProperty("visibility")]
        public int Visibility { get; set; }
        
        [JsonProperty("wind_speed")]
        public float WindSpeed { get; set; }
        
        [JsonProperty("wind_deg")]
        public int WindDeg { get; set; }
        
        [JsonProperty("wind_gust")]
        public float? WindGust { get; set; }
        
        [JsonProperty("weather")]
        public IEnumerable<WeatherCurrWf> Weathers { get; set; }
        
        [JsonProperty("rain")]
        public RainCurrWf Rain { get; set; }
        
        [JsonProperty("snow")]
        public SnowCurrWf Snow { get; set; }
    }
}