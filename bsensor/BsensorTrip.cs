using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    public class BsensorTrip : SensorTrip
    {
        private long tripId;
        private List<ECGReading> ecgReadings = new List<ECGReading>();
        private List<int> shimmerIDs = new List<int>();
        private IDictionary<int, IList<ShimmerSensorReading>> sensorsReadings = new Dictionary<int, IList<ShimmerSensorReading>>();
        private List<LatLng> _latlngs = new List<LatLng>();

        public BsensorTrip(long tripId)
        {
            List<long?> coordIDs = new List<long?>();

            this.tripId = tripId;

            // Get trip points
            DataLayer.instance.AppendTrip(tripId, _latlngs);

            // Get ECG data
            DataLayer.instance.getECG(tripId, ecgReadings);

            // Get coordinate IDs
            DataLayer.instance.getCoordIDs(tripId, coordIDs);

            // Get any Shimmer IDs
            DataLayer.instance.getShimmerIDs(tripId, coordIDs, shimmerIDs);


            List<ShimmerSensorReading> readings = null;
            // get Shimmer readings for all shimmer devices
            foreach (int shimmerID in shimmerIDs)
            {
                // Get Shimmer readings for a specific shimmer device
                readings = new List<ShimmerSensorReading>();
                DataLayer.instance.GetShimmerSensorReadings(tripId, coordIDs, shimmerID, readings);

                sensorsReadings.Add(shimmerID, readings);
            }
        }

        public override IList<LatLng> LatLngs
        {
            get { return _latlngs.AsReadOnly(); }
        }

        public IList<ECGReading> ECGReadings
        {
            get { return ecgReadings; }
        }

        public IList<int> ShimmerIDs
        {
            get { return shimmerIDs; }
        }

        public IList<ShimmerSensorReading> SensorReadings(int id)
        {
            return sensorsReadings[id];
        }

        public long TripID
        {
            get { return tripId; }
        }

        public override void Update(MyApplication myApp, GoogleMapSettings mapSettings)
        {
            myApp.OnEventTripInfo(new TripInfo(_latlngs.Count));
            myApp.OnEventNewMap((new GoogleTripView(_latlngs, mapSettings)).html);

            if (null != ecgReadings)
            {
                myApp.OnEventNewECG(ecgReadings);
            }

            // get Shimmer readings for all shimmer devices
            foreach (int id in shimmerIDs)
            {
               myApp.OnEventShimmerSensorData(sensorsReadings[id]);
            }
        }

        public override void UpdateGraph(MyApplication myApp)
        {
        }
    }
}
