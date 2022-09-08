namespace Geo.Models
{
    public partial class GeoRepository
    {
        public struct Point
        {
            public double latitude;
            public double longitude;
            public Point(double latitude, double longitude)
            {
                this.latitude = latitude;
                this.longitude = longitude;
            }
        }
    }
}
