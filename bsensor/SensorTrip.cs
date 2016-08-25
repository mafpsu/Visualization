using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    public abstract class SensorTrip
    {
        protected static readonly char[] CSV_SEPERATOR = { ',' };
        private static readonly string ERR_MSG_INVALID_FILE= "Invalid Trip file";

        protected long _id;
        protected long _key;

        /*protected List<LatLng> _latlngs = new List<LatLng>();
        protected List<double> _speeds = new List<double>();
        protected List<double> _data2 = new List<double>();
        */

        public static List<long> Keys(Dictionary<long, SensorTrip> trips)
        {
            List<long> keys = new List<long>();

            foreach (long key in trips.Keys)
            {
                keys.Add(trips[key].Key);
            }

            return keys;
        }

        public long ID
        {
            get { return _id; }
        }

        public long Key
        {
            get { return _key; }
        }

        public abstract IList<LatLng> LatLngs
        {
            get;
        }

        public abstract void Update(MyApplication myApp, GoogleMapSettings mapSettings);

        public abstract void UpdateGraph(MyApplication myApp);

        public static List<long> ImportTrips(string fileName, out string errMsg, out Dictionary<long, SensorTrip> trips)
        {
            string line;
            string[] headers;
            errMsg = null;
            trips = null;

            // Read the file and display it line by line.
            using (StreamReader reader = new StreamReader(fileName))
            {
                try
                {
                    if (null == (line = reader.ReadLine()))
                    {
                        errMsg = ERR_MSG_INVALID_FILE;
                    }
                    else if (null == (headers = line.Split(CSV_SEPERATOR)))
                    {
                        errMsg = ERR_MSG_INVALID_FILE;
                    }
                    else if (FileTripBusData.IsValidHeaders(headers))
                    {
                        trips = FileTripBusData.ParseTrips(fileName, reader);
                    }
                    else if (FileTripCSV1.IsValidHeaders(headers))
                    {
                        trips = FileTripCSV1.ParseTrips(fileName, reader);
                    }
                    else if (FileTripCSV2.IsValidHeaders(headers))
                    {
                        trips = FileTripCSV2.ParseTrips(fileName, reader);
                    }
                    else if (FileTripCSV3.IsValidHeaders(headers))
                    {
                        trips = FileTripCSV3.ParseTrips(fileName, reader);
                    }
                    else if (FileTripCSV4.IsValidHeaders(headers))
                    {
                        trips = FileTripCSV4.ParseTrips(fileName, reader);
                    }
                    else
                    {
                        errMsg = ERR_MSG_INVALID_FILE;
                    }
                    reader.Close();



                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }

            if (null != trips)
                return Keys(trips);

            return null;
        }
    }
}
