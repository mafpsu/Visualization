using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    public class FileTripCSV3 : SensorTrip
    {
        private string fileName;
        protected List<double> _speeds = new List<double>();
        private List<LatLng> _latlngs = new List<LatLng>();

        public FileTripCSV3(string fileName, long id, int key, List<LatLng> latlngs, List<double> speeds)
        {
            this.fileName = fileName;
            _latlngs = latlngs;
            _speeds = speeds;
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
            if (((headers.Length == 7)||(headers.Length == 8)) &&
                headers[0].Equals("OPD_DATE*", StringComparison.OrdinalIgnoreCase) &&
                headers[1].Equals("VEHICLE_ID", StringComparison.OrdinalIgnoreCase) &&
                headers[2].Equals("EVENT_NO_TRIP", StringComparison.OrdinalIgnoreCase) &&
                headers[3].Equals("ACT_TIME*", StringComparison.OrdinalIgnoreCase) &&
                headers[4].Equals("GPS_LATITUDE", StringComparison.OrdinalIgnoreCase) &&
                headers[5].Equals("GPS_LONGITUDE", StringComparison.OrdinalIgnoreCase) &&
                headers[6].Equals("SPEED MPH", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public override void Update(MyApplication myApp, GoogleMapSettings mapSettings)
        {
            myApp.OnEventTripInfo(new TripInfo(_latlngs.Count));
            myApp.OnEventNewMap((new GoogleTripView(_latlngs, mapSettings)).html);
            myApp.OnEventBusData3(_speeds);
        }

        public override void UpdateGraph(MyApplication myApp)
        {
            myApp.OnEventBusData3(_speeds);
        }

        public static Dictionary<long, SensorTrip> ParseTrips(string fileName, StreamReader reader)
        {
            List<LatLng> latlngs = new List<LatLng>();
            List<double> data1 = new List<double>();

            int lineNumber = 1;
            int numBadLines = 0;
            string line;
            string[] fields;

            DateTime dt;
            int vid;
            int tid;
            int currentTid = -1;
            double lat;
            double lng;
            double speed;
            int key = 0;

            Dictionary<long, SensorTrip> newtrips = new Dictionary<long, SensorTrip>();

            while ((line = reader.ReadLine()) != null)
            {
                ++lineNumber;

                fields = line.Split(CSV_SEPERATOR);

                if ((null != fields) &&
                    (fields.Length >= 7) &&
                    !String.IsNullOrEmpty(fields[0]) &&
                    !String.IsNullOrEmpty(fields[3]) &&
                    DateTime.TryParse(fields[0] + " " + fields[3], out dt) &&
                    Int32.TryParse(fields[1], out vid) &&
                    Int32.TryParse(fields[2], out tid) &&
                    Double.TryParse(fields[4], out lat) &&
                    Double.TryParse(fields[5], out lng) &&
                    Double.TryParse(fields[6], out speed))
                {
                    if (currentTid == -1)
                    {
                        currentTid = tid;
                    }

                    if (tid == currentTid)
                    {
                        latlngs.Add(new LatLng(lat, lng));
                        data1.Add(speed);
                    }
                    else
                    {
                        if (!newtrips.ContainsKey(currentTid))
                        {
                            // Complete previous trip and start new trip
                            newtrips.Add(key, new FileTripCSV3(fileName, currentTid, key, latlngs, data1));
                            key++;
                        }

                        // clear data collected from
                        latlngs = new List<LatLng>();
                        data1 = new List<double>();

                        // S
                        currentTid = tid;
                        latlngs.Add(new LatLng(lat, lng));
                        data1.Add(speed);
                    }
                }
                else
                {
                    numBadLines++;
                }
            }

            if (latlngs.Count > 0)
            {
                newtrips.Add(key, new FileTripCSV3(fileName, currentTid, key, latlngs, data1));
            }
            return newtrips;
        }
    }
}
