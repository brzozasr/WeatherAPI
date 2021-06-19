#nullable disable

namespace WeatherAPI.Models
{
    public partial class City
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public double? CoordLon { get; set; }
        public double? CoordLat { get; set; }
    }
}
