using System;
using System.Collections.Generic;
using System.IO;

namespace bsensor
{
    class FileTripCSV2 : SensorTrip
    {
        private static readonly string DATA_SET_NAME_1 = "BusData.Series1";
        private static readonly string DATA_SET_NAME_2 = "BusData.Series2";

        private string fileName;
        protected List<double> _speeds = new List<double>();
        private List<LatLng> _latlngs = new List<LatLng>();
        protected List<double> _data2 = new List<double>();

        public FileTripCSV2(string fileName, long id, int key, List<LatLng> latlngs, List<double> data1, List<double> data2)
        {
            this.fileName = fileName;
            _latlngs = latlngs;
            _speeds = data1;
            _data2 = data2;
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
            if ((headers.Length == 4) &&
                headers[0].Equals("latitude", StringComparison.OrdinalIgnoreCase) &&
                headers[1].Equals("longitude", StringComparison.OrdinalIgnoreCase) &&
                headers[2].Equals("series1", StringComparison.OrdinalIgnoreCase) &&
                headers[3].Equals("series2", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public override void Update(MyApplication myApp, GoogleMapSettings mapSettings)
        {
            myApp.OnEventTripInfo(new TripInfo(_latlngs.Count));
            myApp.OnEventNewMap((new GoogleTripView(_latlngs, mapSettings)).html);
            myApp.OnEventBusData2(DATA_SET_NAME_1, _speeds, DATA_SET_NAME_2, _data2);
        }

        public override void UpdateGraph(MyApplication myApp)
        {
            myApp.OnEventBusData2(DATA_SET_NAME_1, _speeds, DATA_SET_NAME_2, _data2);
        }

        public static Dictionary<long, SensorTrip> ParseTrips(string fileName, StreamReader reader)
        {
            List<LatLng> latlngs = new List<LatLng>();
            List<double> data1 = new List<double>();
            List<double> data2 = new List<double>();

            int lineNumber = 1;
            string line;
            string[] fields;

            double lat;
            double lng;
            double d1;
            double d2;
            while ((line = reader.ReadLine()) != null)
            {
                ++lineNumber;
                fields = line.Split(CSV_SEPERATOR, 4);
                if ((null != fields) && 
                    (fields.Length == 4) && 
                    Double.TryParse(fields[0], out lat) &&
                    Double.TryParse(fields[1], out lng) &&
                    Double.TryParse(fields[2], out d1) &&
                    Double.TryParse(fields[3], out d2))
                {
                    latlngs.Add(new LatLng(lat, lng));
                    data1.Add(d1);
                    data2.Add(d2);
                }
                else
                {
                    throw new Exception("Invalid entry encountered in CSV file.  Line number: " + lineNumber.ToString());
                }
            }
            Dictionary<long, SensorTrip> newtrips = new Dictionary<long, SensorTrip>();
            newtrips.Add(0, new FileTripCSV2(fileName, 0, 0, latlngs, data1, data2));
            return newtrips;
        }
    }
}
