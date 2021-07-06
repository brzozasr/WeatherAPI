using System;
using System.Collections.Generic;

namespace WeatherAPI.Models.PointWeatherForecast
{
    public class AlertsPwf
    {
        public string SenderName { get; set; }
        public string Event { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}