using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherAPI.Models.WeatherForecast
{
    public class AlertsWf
    {
        [JsonProperty("sender_name")]
        public string SenderName { get; set; }
        
        [JsonProperty("event")]
        public string Event { get; set; }
        
        [JsonProperty("start")]
        public long? Start { get; set; }
        
        [JsonProperty("end")]
        public long? End { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("tags")]
        public IEnumerable<string> Tags { get; set; }
    }
}