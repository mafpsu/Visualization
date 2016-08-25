using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    public class LatLng
    {
        private double lat;
        private double lng;

        public LatLng(double lat, double lng)
        {
            this.lat = lat;
            this.lng = lng;
        }

        public double Latitude
        {
            get { return lat; }
            set { lat = value; }
        }

        public double Longitude
        {
            get { return lng; }
            set { lng = value; }
        }
    }
}
