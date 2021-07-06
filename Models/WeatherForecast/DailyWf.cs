using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherAPI.Models.WeatherForecast
{
    public class DailyWf
    {
        [JsonProperty("dt")]
        public long Dt { get; set; }
        
        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }
        
        [JsonProperty("sunset")]
        public long Sunset { get; set; }
        
        [JsonProperty("moonrise")]
        public long Moonrise { get; set; }
        
        [JsonProperty("moonset")]
        public long Moonset { get; set; }
        
        [JsonProperty("moon_phase")]
        public float MoonPhase { get; set; }
        
        [JsonProperty("temp")]
        public TempWf TempWf { get; set; }
        
        [JsonProperty("feels_like")]
        public FeelsLikeWf FeelsLikeWf { get; set; }
        
        [JsonProperty("pressure")]
        public float Pressure { get; set; }
        
        [JsonProperty("humidity")]
        public float Humidity { get; set; }
        
        [JsonProperty("dew_point")]
        public float DewPoint { get; set; }
        
        [JsonProperty("wind_speed")]
        public float WindSpeed { get; set; }
        
        [JsonProperty("wind_deg")]
        public int WindDeg { get; set; }
        
        [JsonProperty("wind_gust")]
        public float? WindGust { get; set; }
        
        [JsonProperty("weather")]
        public IEnumerable<WeatherWf> Weathers { get; set; }
        
        [JsonProperty("clouds")]
        public int Clouds { get; set; }
        
        [JsonProperty("pop")]
        public float Pop { get; set; }
        
        [JsonProperty("uvi")]
        public float Uvi { get; set; }
        
        [JsonProperty("rain")]
        public float? Rain { get; set; }
        
        [JsonProperty("snow")]
        public float? Snow { get; set; }
    }
}