using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherAPI.Models.WeatherBBox
{
    public class CityData
    {
        [JsonProperty("id")] 
        public long Id { get; set; }

        [JsonProperty("dt")] 
        public long Dt { get; set; }

        [JsonProperty("name")] 
        public string Name { get; set; }
        
        [JsonProperty("coord")] 
        public Coord Coords { get; set; }
        
        [JsonProperty("main")] 
        public Main Mains { get; set; }
        
        [JsonProperty("visibility")] 
        public long Visibility { get; set; }
        
        [JsonProperty("wind")] 
        public Wind Winds { get; set; }
        
        [JsonProperty("rain")] 
        public Rain Rains { get; set; }
        
        [JsonProperty("snow")] 
        public Snow Snows { get; set; }
        
        [JsonProperty("clouds")] 
        public Clouds Clouds { get; set; }
        
        [JsonProperty("weather")]
        public IList<Weather> Weathers { get; set; }
    }
}