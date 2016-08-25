using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    public class SpeedData
    {
        private IList<LatLng> latlngs;
        private IList<double> speeds;
        private IList<double> distances;
        private IList<DateTime> dateTimes;

        public SpeedData(IList<LatLng> latlngs, IList<double> speeds, IList<DateTime> dateTimes)
        {
            this.latlngs = latlngs;
            this.speeds = speeds;
            this.distances = Util.DistancesFromLatLng(latlngs);
            this.dateTimes = dateTimes;
        }

        public IList<LatLng> LatLngs
        {
            get { return latlngs; }
        }

        public IList<double> Speeds
        {
            get { return speeds; }
        }

        public IList<double> Distances
        {
            get { return distances; }
        }

        public IList<DateTime> DateTimes
        {
            get { return dateTimes; }
        }
    }
}
