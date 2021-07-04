using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherAPI.Models.WeatherForecast
{
    public class WeatherForecast
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
        public CurrentWf CurrentWf { get; set; }
        
        [JsonProperty("minutely")]
        public IEnumerable<MinutelyWf> Minutely { get; set; }
        
        [JsonProperty("hourly")]
        public IEnumerable<HourlyWf> Hourly { get; set; }
        
        [JsonProperty("daily")]
        public IEnumerable<DailyWf> Daily { get; set; }
        
        [JsonProperty("alerts")]
        public IEnumerable<AlertsWf> AlertsWf { get; set; }
    }
}