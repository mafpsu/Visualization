using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    public class StopLevelData
    {
        private IList<LatLng> latlngs;
        private IList<double> dwells;
        private IList<BusLoad> busLoads;
        private IList<double> distances;
        private IList<DateTime> dateTimes = new List<DateTime>();

        public StopLevelData(IList<LatLng> latlngs, IList<double> dwells, IList<BusLoad> loadOnOffs, IList<DateTime> dateTimes)
        {
            this.latlngs = latlngs;
            this.busLoads = loadOnOffs;
            this.dwells = dwells;
            this.distances = Util.DistancesFromLatLng(latlngs);
            this.dateTimes = dateTimes;
        }

        public IList<double> Dwells
        {
            get { return dwells; }
        }

        public IList<BusLoad> BusLoads
        {
            get { return busLoads; }
        }

        public IList<LatLng> LatLngs
        {
            get { return latlngs; }
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
