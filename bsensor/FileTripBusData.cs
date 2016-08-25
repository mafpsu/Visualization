using System;
using System.IO;
using System.Collections.Generic;

namespace bsensor
{
    class FileTripBusData : SensorTrip
    {
        public static readonly string BUS_SPEED_DATA_TAG = "<BUS-SPEED-DATA>";
        public static readonly string BUS_STOP_LEVEL_TAG = "<STOP-LEVEL-DATA>";

        private static int lineNumber = 1;
        private static int numBadLines = 0;

        private string fileName;
        private int _vid;
        private StopLevelData stopLevelData;
        private SpeedData speedData;

        public FileTripBusData(string fileName, long id, int vid, SpeedData speedData, StopLevelData stopLevelData)
        {
            this.fileName = fileName;
            this.stopLevelData = stopLevelData;
            this.speedData = speedData;
            _key = _id = id;
            _vid = vid;
        }

        public override IList<LatLng> LatLngs
        {
            get { return speedData.LatLngs; }
        }

        public int Vid
        {
            get { return _vid; }
        }

        public string FileName
        {
            get { return fileName; }
        }

        public static bool IsValidHeaders(string[] headers)
        {
            if ((headers.Length == 1) &&
                headers[0].Equals(BUS_STOP_LEVEL_TAG, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public override void Update(MyApplication myApp, GoogleMapSettings mapSettings)
        {
            myApp.OnEventTripInfo(new TripInfo(speedData.LatLngs.Count));
            myApp.OnEventNewMap((new GoogleTripView(speedData.LatLngs, mapSettings, stopLevelData.LatLngs)).html);
            myApp.OnEventBusData5(speedData, stopLevelData,
                myApp.GraphSpeedSettings,
                myApp.GraphDwellSettings,
                myApp.GraphLoadSettings);

            myApp.ColorBusSpeedData(DataSetName.Speed);
            myApp.ColorBusSpeedData(DataSetName.Dwell);
        }

        public override void UpdateGraph(MyApplication myApp)
        {
            myApp.OnEventBusData5(speedData, stopLevelData,
                myApp.GraphSpeedSettings,
                myApp.GraphDwellSettings,
                myApp.GraphLoadSettings);
        }

        public static Dictionary<long, SensorTrip> ParseTrips(string fileName, StreamReader reader)
        {
            string[] fields;
            string line;

            Dictionary<int, StopLevelData> stopLevelDatas = null;
            Dictionary<long, SensorTrip> sensorTrips = new Dictionary<long, SensorTrip>();

            while ((line = reader.ReadLine()) != null)
            {
                ++lineNumber;

                fields = line.Split(CSV_SEPERATOR);

                if ((null == fields) || (fields.Length == 0))
                {
                    continue;
                }
                else if (((fields.Length == 12) || (fields.Length == 13)) &&
                    fields[0].Equals("OPD_DATE.", StringComparison.OrdinalIgnoreCase) &&
                    fields[1].Equals("VEHICLE_ID", StringComparison.OrdinalIgnoreCase) &&
                    fields[2].Equals("TRIP_NUMBER", StringComparison.OrdinalIgnoreCase) &&
                    fields[3].Equals("STOP_TIME", StringComparison.OrdinalIgnoreCase) &&
                    fields[4].Equals("ARRIVE_TIME", StringComparison.OrdinalIgnoreCase) &&
                    fields[5].Equals("LEAVE_TIME", StringComparison.OrdinalIgnoreCase) &&
                    fields[6].Equals("DWELL", StringComparison.OrdinalIgnoreCase) &&
                    fields[7].Equals("ONS", StringComparison.OrdinalIgnoreCase) &&
                    fields[8].Equals("OFFS", StringComparison.OrdinalIgnoreCase) &&
                    fields[9].Equals("LOAD", StringComparison.OrdinalIgnoreCase) &&
                    fields[10].Equals("GPS_Latitude", StringComparison.OrdinalIgnoreCase) &&
                    fields[11].Equals("GPS_Longitude", StringComparison.OrdinalIgnoreCase))
                {
                    stopLevelDatas = ParseStopLevelData(reader);
                }
                else if (((fields.Length == 7) || (fields.Length == 8)) &&
                    fields[0].Equals("OPD_DATE*", StringComparison.OrdinalIgnoreCase) &&
                    fields[1].Equals("VEHICLE_ID", StringComparison.OrdinalIgnoreCase) &&
                    fields[2].Equals("EVENT_NO_TRIP", StringComparison.OrdinalIgnoreCase) &&
                    fields[3].Equals("ACT_TIME*", StringComparison.OrdinalIgnoreCase) &&
                    fields[4].Equals("GPS_LATITUDE", StringComparison.OrdinalIgnoreCase) &&
                    fields[5].Equals("GPS_LONGITUDE", StringComparison.OrdinalIgnoreCase) &&
                    fields[6].Equals("SPEED MPH", StringComparison.OrdinalIgnoreCase))
                {
                    if (null == (sensorTrips = ParseSpeedData(fileName, reader, stopLevelDatas)))
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            return sensorTrips;
        }

        private static Dictionary<long, SensorTrip> ParseSpeedData(string fileName, StreamReader reader, Dictionary<int, StopLevelData> stopLevelDatas)
        {
            string[] fields;

            Dictionary<long, SensorTrip> newtrips = new Dictionary<long, SensorTrip>();
            List<LatLng> latlngs = new List<LatLng>();
            List<double> speeds = new List<double>();
            List<DateTime> dateTimes = new List<DateTime>();

            string line;

            DateTime dt;
            int vid = -1;
            int currentVid = -1;
            long tid = -1;
            long currentTid = -1;
            double lat;
            double lng;
            double speed;
            int duplicates = 0;
            StopLevelData stopLevelData;

            while ((line = reader.ReadLine()) != null)
            {
                ++lineNumber;

                fields = line.Split(CSV_SEPERATOR);

                if (((fields.Length == 7) || (fields.Length == 8)) &&
                !String.IsNullOrEmpty(fields[0]) &&
                !String.IsNullOrEmpty(fields[3]) &&
                DateTime.TryParse(fields[0] + " " + fields[3], out dt) &&
                int.TryParse(fields[1], out vid) &&
                long.TryParse(fields[2], out tid) &&
                double.TryParse(fields[4], out lat) &&
                double.TryParse(fields[5], out lng) &&
                double.TryParse(fields[6], out speed))
                {
                    if (currentTid == -1)
                    {
                        currentTid = tid;
                    }

                    if (tid == currentTid)
                    {
                        latlngs.Add(new LatLng(lat, lng));
                        speeds.Add(speed);
                        dateTimes.Add(dt);
                        currentVid = vid;
                    }
                    else
                    {
                        // For now, we ignore trips with identical vehicle IDs
                        if (!newtrips.ContainsKey(currentTid))
                        {
                            if (stopLevelDatas.ContainsKey(currentVid))
                            {
                                stopLevelData = stopLevelDatas[currentVid];
                            }
                            else
                            {
                                stopLevelData = null;
                            }

                            // Complete previous trip and start new trip
                            newtrips.Add(currentTid, new FileTripBusData(fileName, currentTid, currentVid, new SpeedData(latlngs, speeds, dateTimes), stopLevelData));
                        }
                        else
                        {
                            duplicates++;
                        }

                        // clear data collected from
                        latlngs = new List<LatLng>();
                        speeds = new List<double>();
                        dateTimes = new List<DateTime>();

                        // S
                        currentTid = tid;
                        latlngs.Add(new LatLng(lat, lng));
                        speeds.Add(speed);
                        dateTimes.Add(dt);
                        currentVid = vid;
                    }
                }
                else if ((fields.Length == 1) && (fields[0].Equals(BUS_STOP_LEVEL_TAG, StringComparison.OrdinalIgnoreCase)))
                {
                    break;
                }
                else
                {
                    numBadLines++;
                }
            }

            if ((latlngs.Count > 0) && (!newtrips.ContainsKey(currentTid)))
            {
                if (stopLevelDatas.ContainsKey(currentVid))
                {
                    stopLevelData = stopLevelDatas[currentVid];
                }
                else
                {
                    stopLevelData = null;
                }

                newtrips.Add(currentTid, new FileTripBusData(fileName, currentTid, currentVid, new SpeedData(latlngs, speeds, dateTimes), stopLevelData));
            }
            return newtrips;
        }

        private static Dictionary<int, StopLevelData> ParseStopLevelData(StreamReader reader)
        {
            Dictionary<int, StopLevelData> stopLevelDatas = new Dictionary<int, StopLevelData>();
            List<LatLng> latlngs = new List<LatLng>();
            List<double> dwells = new List<double>();
            List<BusLoad> busLoads = new List<BusLoad>();
            List<DateTime> dateTimes = new List<DateTime>();

            int lineNumber = 1;
            int numBadLines = 0;
            int numFound = 0;
            string line;
            string[] fields;

            int vid;
            int tid = -1;
            int currentVid = -1;
            DateTime stopTime;
            DateTime arriveTime;
            DateTime leaveTime;
            int dwell;
            int ons;
            int offs;
            int load;
            double lat;
            double lng;
            int duplicates = 0;

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
                    if (currentVid == -1)
                    {
                        currentVid = vid;
                    }

                    if (vid == currentVid)
                    {
                        // Save new values
                        latlngs.Add(new LatLng(lat, lng));
                        dwells.Add(dwell);
                        dateTimes.Add(arriveTime);
                        busLoads.Add(new BusLoad(ons, offs, load));
                    }
                    else
                    {
                        // Add stop level data to the trip with the given vid
                        if (stopLevelDatas.ContainsKey(currentVid))
                        {
                            ++duplicates;
                        }
                        stopLevelDatas[currentVid] = new StopLevelData(latlngs, dwells, busLoads, dateTimes);
                        numFound++;

                        // clear data collected from
                        latlngs = new List<LatLng>();
                        dwells = new List<double>();
                        busLoads = new List<BusLoad>();
                        dateTimes = new List<DateTime>();

                        // Save new values
                        currentVid = vid;
                        latlngs.Add(new LatLng(lat, lng));
                        dwells.Add(dwell);
                        busLoads.Add(new BusLoad(ons, offs, load));
                        dateTimes.Add(arriveTime);
                    }
                }
                else if ((fields.Length == 1) && (fields[0].Equals(BUS_SPEED_DATA_TAG, StringComparison.OrdinalIgnoreCase)))
                {
                    break;
                }
                else
                {
                    numBadLines++;
                    //throw new Exception("Invalid entry encountered in CSV file.  Line number: " + lineNumber.ToString());
                }
            }

            if (latlngs.Count > 0)
            {
                stopLevelDatas[currentVid] = new StopLevelData(latlngs, dwells, busLoads, dateTimes);
                numFound++;
            }
            return stopLevelDatas;
        }
    }
}
