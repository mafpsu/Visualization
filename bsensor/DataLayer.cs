using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    class DataLayer
    {
        private const string MODULE_TAG = "DataLayer";

        public static readonly DataLayer instance = new DataLayer();

        private DataLayer()
        {

        }

        public void GetTrips(List<DataLayer_Trip> dltrips)
        {
            using (var db = new BsensorContext())
            {
                var query = from t in db.trips
                            orderby t.id descending
                            select new { t.id, t.user_id, t.client_trip_id, t.n_coord };

                foreach (var trip in query)
                {
                        DataLayer_Trip item = new DataLayer_Trip(
                            trip.id, 
                            trip.user_id,
                            trip.client_trip_id,
                            trip.n_coord);

                        dltrips.Add(item);
                }
            }
        }

        public void GetTrip(long tripID, List<LatLng> latlngs)
        {
            latlngs.Clear();
            AppendTrip(tripID, latlngs);
        }

        public void AppendTrip(long tripID, List<LatLng> latlngs)
        {
            using (var db = new BsensorContext())
            {
                var query = from c in db.coords
                            where c.trip_id == tripID
                            select new { c.latitude, c.longitude };

                foreach (var coord in query)
                {
                    latlngs.Add(new LatLng((double)coord.latitude, (double)coord.longitude));
                }
            }
        }

        public void getECG(long tripID, List<ECGReading> readings)
        {

            using (var db = new BsensorContext())
            {
                var query_coords = from d in db.coords
                                   where d.trip_id == tripID
                                   select new { d.id };

                List<int> coord_ids = query_coords.Select(coord => coord.id).ToList();

                var query_ecg = from ecg in db.shimmer_ecg_readings
                where (coord_ids.Contains(ecg.coord_id))
                select new { ecg.exg1_ch1, ecg.exg1_ch2, ecg.exg2_ch1, ecg.exg2_ch2 };

                foreach (var r in query_ecg)
                {
                    readings.Add(new ECGReading(r.exg1_ch1, r.exg1_ch2, r.exg2_ch1, r.exg2_ch2));
                }
            }
        }

        public void getCoordIDs(long tripID, List<long?> coord_ids)
        {
            using (var db = new BsensorContext())
            {
                var query_coords = from d in db.coords
                                   where d.trip_id == tripID
                                   select new { d.id };

                foreach(var c in query_coords)
                {
                    coord_ids.Add(c.id);
                }
            }
        }

        public void getShimmerIDs(long tripID, List<long?> coord_ids, List<int> sensor_ids)
        {
            using (var db = new BsensorContext())
            {
                var query = (from c in db.shimmer_sensor_readings
                             where coord_ids.Contains(c.coord_id)
                             select new { c.shimmer_sensor_id }).Distinct();

                int length = query.Count();

                foreach (var r in query)
                {
                    if (null != r.shimmer_sensor_id)
                        sensor_ids.Add((int) r.shimmer_sensor_id);
                }
            }
        }

        public void GetShimmerSensorReadings(long tripID, List<long?> coord_ids, int sensorID, List<ShimmerSensorReading> readings)
        {
            using (var db = new BsensorContext())
            {
                var query = from c in db.shimmer_sensor_readings
                            where coord_ids.Contains(c.coord_id) && c.shimmer_sensor_id == sensorID
                            select new { c.num_s, c.avg_0, c.avg_1, c.avg_2 };

                long length = query.Count();

                foreach (var r in query)
                {
                    readings.Add(new ShimmerSensorReading(r.num_s, r.avg_0, r.avg_1, r.avg_2));
                }
            }
        }
    }
}
