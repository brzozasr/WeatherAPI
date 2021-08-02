using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherAPI.Models.WeatherHistorical
{
    public class WeatherHistorical
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }
        
        [JsonProperty("lon")]
        public double Lon { get; set; }
        
        [JsonProperty("timezone")]
        public string Timezone { get; set; }
        
        [JsonProperty("timezone_offset")]
        public long TimezoneOffset { get; set; }
        
        [JsonProperty("current")]
        public CurrentWh Current { get; set; }
        
        [JsonProperty("hourly")]
        public IEnumerable<HourlyWh> Hourly { get; set; }
    }
}