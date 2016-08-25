using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    public class FileTripCSV4 : SensorTrip
    {
        private string fileName;
        private IList<BusLoad> busLoads;
        protected List<double> _speeds = new List<double>();
        private List<LatLng> _latlngs = new List<LatLng>();

        public FileTripCSV4(string fileName, long id, int key, List<LatLng> latlngs, List<double> data1, IList<BusLoad> busLoads)
        {
            this.fileName = fileName;
            _latlngs = latlngs;
            _speeds = data1;
            this.busLoads = busLoads;
            _id = id;
            _key = key;
        }

        public override IList<LatLng> LatLngs
        {
            get { return _latlngs.AsReadOnly(); }
        }

        public string FileName
        {
            get { return fileName; }
        }

        public static bool IsValidHeaders(string[] headers)
        {
            if (((headers.Length == 12) || (headers.Length == 13)) &&
                headers[0].Equals("OPD_DATE.", StringComparison.OrdinalIgnoreCase) &&
                headers[1].Equals("VEHICLE_ID", StringComparison.OrdinalIgnoreCase) &&
                headers[2].Equals("TRIP_NUMBER", StringComparison.OrdinalIgnoreCase) &&
                headers[3].Equals("STOP_TIME", StringComparison.OrdinalIgnoreCase) &&
                headers[4].Equals("ARRIVE_TIME", StringComparison.OrdinalIgnoreCase) &&
                headers[5].Equals("LEAVE_TIME", StringComparison.OrdinalIgnoreCase) &&
                headers[6].Equals("DWELL", StringComparison.OrdinalIgnoreCase) &&
                headers[7].Equals("ONS", StringComparison.OrdinalIgnoreCase) &&
                headers[8].Equals("OFFS", StringComparison.OrdinalIgnoreCase) &&
                headers[9].Equals("LOAD", StringComparison.OrdinalIgnoreCase) &&
                headers[10].Equals("GPS_Latitude", StringComparison.OrdinalIgnoreCase) &&
                headers[11].Equals("GPS_Longitude", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public override void Update(MyApplication myApp, GoogleMapSettings mapSettings)
        {
            myApp.OnEventTripInfo(new TripInfo(_latlngs.Count));
            myApp.OnEventNewMap((new GoogleTripView(_latlngs, mapSettings)).html);
            myApp.OnEventBusData4(null, null, null);// Todo: [REVISIT]
        }

        public override void UpdateGraph(MyApplication myApp)
        {
            myApp.OnEventBusData4(null, null, null);  // Todo: [REVISIT]
        }

        public static Dictionary<long, SensorTrip> ParseTrips(string fileName, StreamReader reader)
        {
            List<LatLng> latlngs = new List<LatLng>();
            List<double> data1 = new List<double>();
            List<BusLoad> data3 = new List<BusLoad>();

            int lineNumber = 1;
            int numBadLines = 0;
            string line;
            string[] fields;

            int vid;
            int tid = -1;
            int currentTid = -1;
            DateTime stopTime;
            DateTime arriveTime;
            DateTime leaveTime;
            int dwell;
            int ons;
            int offs;
            int load;
            double lat;
            double lng;
            int key = 0;

            Dictionary<long, SensorTrip> newtrips = new Dictionary<long, SensorTrip>();

            while ((line = reader.ReadLine()) != null)
            {
                ++lineNumber;
                fields = line.Split(CSV_SEPERATOR);
                if ((null != fields) &&
                    (fields.Length >= 12) &&
                    !String.IsNullOrEmpty(fields[0]) &&
                    Int32.TryParse(fields[1], out vid) &&
                    Int32.TryParse(fields[2], out tid) &&
                    !String.IsNullOrEmpty(fields[3]) &&
                    !String.IsNullOrEmpty(fields[4]) &&
                    !String.IsNullOrEmpty(fields[5]) &&
                    Int32.TryParse(fields[6], out dwell) &&
                    Int32.TryParse(fields[7], out ons) &&
                    Int32.TryParse(fields[8], out offs) &&
                    Int32.TryParse(fields[9], out load) &&
                    Double.TryParse(fields[10], out lat) &&
                    Double.TryParse(fields[11], out lng) &&
                    DateTime.TryParse(fields[0] + " " + fields[3], out stopTime) &&
                    DateTime.TryParse(fields[0] + " " + fields[4], out arriveTime) &&
                    DateTime.TryParse(fields[0] + " " + fields[5], out leaveTime))
                {
                    if (currentTid == -1)
                    {
                        currentTid = tid;
                    }

                    if (tid == currentTid)
                    {
                        latlngs.Add(new LatLng(lat, lng));
                        data1.Add(dwell);
                        data3.Add(new BusLoad(ons, offs, load));
                    }
                    else
                    {
                        if (!newtrips.ContainsKey(currentTid))
                        {
                            // Complete previous trip and start new trip
                            newtrips.Add(key, new FileTripCSV4(fileName, currentTid, key, latlngs, data1, data3));
                            key++;
                        }

                        // clear data collected from
                        latlngs = new List<LatLng>();
                        data1 = new List<double>();
                        data3 = new List<BusLoad>();

                        // S
                        currentTid = tid;
                        latlngs.Add(new LatLng(lat, lng));
                        data1.Add(dwell);
                        data3.Add(new BusLoad(ons, offs, load));
                    }
                }
                else
                {
                    numBadLines++;
                    //throw new Exception("Invalid entry encountered in CSV file.  Line number: " + lineNumber.ToString());
                }
            }

            if (latlngs.Count > 0)
            {
                newtrips.Add(key, new FileTripCSV4(fileName, currentTid, key, latlngs, data1, data3));
            }
            return newtrips;
        }
    }
}
