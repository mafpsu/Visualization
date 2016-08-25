using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace bsensor
{
    class FileTripCSV1 : SensorTrip
    {
        private string fileName;
        private List<LatLng> _latlngs = new List<LatLng>();

        public FileTripCSV1(string fileName, long id, int key, List<LatLng> latlngs)
        {
            this.fileName = fileName;
            _latlngs = latlngs;
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
            if ((headers.Length == 2) &&
                headers[0].Equals("latitude", StringComparison.OrdinalIgnoreCase) &&
                headers[1].Equals("longitude", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public override void Update(MyApplication myApp, GoogleMapSettings mapSettings)
        {
            myApp.OnEventTripInfo(new TripInfo(_latlngs.Count));
            myApp.OnEventNewMap((new GoogleTripView(_latlngs, mapSettings)).html);
            myApp.OnEventBusData1();
        }

        public override void UpdateGraph(MyApplication myApp)
        {
            myApp.OnEventBusData1();
        }

        public static Dictionary<long, SensorTrip> ParseTrips(string fileName, StreamReader file)
        {
            List<LatLng> latlngs = new List<LatLng>();

            string line;
            string[] fields;
            double lat;
            double lng;
            int lineNumber = 1;

            // Read the file and display it line by line.
            while ((line = file.ReadLine()) != null)
            {
                ++lineNumber;
                fields = line.Split(CSV_SEPERATOR, 2);
                if ((null != fields) && 
                    (fields.Length == 2) && 
                    Double.TryParse(fields[0], out lat) && 
                    Double.TryParse(fields[1], out lng))
                {
                    latlngs.Add(new LatLng(lat, lng));
                }
                else
                {
                    throw new Exception("Invalid entry encountered in CSV file.  Line number = " + lineNumber.ToString());
                }
            }
            Dictionary<long, SensorTrip> newtrips = new Dictionary<long, SensorTrip>();
            newtrips.Add(0, new FileTripCSV1(fileName, 0, 0, latlngs));
            return newtrips;
        }
    }
}
