using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherAPI.Models.WeatherBBox
{
    public class WeatherBoxRoot
    {
        [JsonProperty("cod")]
        public int Code { get; set; }
        
        [JsonProperty("calctime")]
        public double Calctime { get; set; }
        
        [JsonProperty("cnt")]
        public int Cnt { get; set; }
        
        [JsonProperty("list")]
        public IList<CityData> ListOfCities { get; set; }
    }
}