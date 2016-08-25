using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace bsensor
{
    public class MyApplication
    {
        private const string MODULE_TAG = "MyApplication";
        private const string NOT_YET_IMPLEMENTED = "Not yet implemented";
        private const string MSG_FILE_ALREADY_OPEN = "File already opened";

        public enum MessageType { Error, Warning, Information};
        public enum QuerySaveProjectResponse { SaveProject, DontSaveProject, CancelOperation };

        // The single instance of MyApplication object
        public static readonly MyApplication instance = new MyApplication();

        // list of trip keys for currently loaded trips
        private List<long>tripKeys = new List<long>();
        private DataSource dataSource = null;

        // Pointer to a trip object
        private SensorTrip currentTrip;
        private Dictionary<long, SensorTrip> trips = new Dictionary<long, SensorTrip>();

        // 
        private Dictionary<string, IList<double>> dictDataSets = new Dictionary<string, IList<double>>();

        // Dictionary of graph color ranges indexed by data set name
        private Dictionary<string, GraphColorRange[]> dictGraphColorRanges = new Dictionary<string, GraphColorRange[]>();

        private ProjectFile projectFile = new ProjectFile();
        private GoogleMapSettings googleMapSettings = new GoogleMapSettings(ProjectSettings.NAME_MAP_SETTINGS);
        private GraphSettings graphSpeedSettings = new GraphSettings(ProjectSettings.NAME_BUS_SPEED_GRAPH_SETTINGS);
        private GraphSettings graphDwellSettings = new GraphSettings(ProjectSettings.NAME_BUS_DWELL_GRAPH_SETTINGS);
        private LoadGraphSettings graphLoadSettings = new LoadGraphSettings(ProjectSettings.NAME_BUS_LOAD_ONS_OFFS_SETTINGS);
        private GraphSettings graphEcgSettings = new GraphSettings(ProjectSettings.NAME_BIKE_ECG_SETTINGS);
        private GraphSettings graphGsrSettings = new GraphSettings(ProjectSettings.NAME_BIKE_GSR_SETTINGS);

        private List<object> properties = new List<object>();

        public GoogleMapSettings GraphMapSettings
        {
            get { return googleMapSettings; }
        }

        public GraphSettings GraphSpeedSettings
        {
            get { return graphSpeedSettings; }
        }

        public GraphSettings GraphDwellSettings
        {
            get { return graphDwellSettings; }
        }

        public LoadGraphSettings GraphLoadSettings
        {
            get { return graphLoadSettings; }
        }

        public GraphSettings GraphEcgSettings
        {
            get { return graphEcgSettings; }
        }

        public GraphSettings GraphGsrSettings
        {
            get { return graphGsrSettings; }
        }

        #region EventAppStatus
        public delegate void EventAppStatus(String status);
        EventAppStatus eventAppStatus;
        public void AddOnEventAppStatus(EventAppStatus e)
        {
            eventAppStatus += e;
        }
        public void RemoveOnEventAppStatus(EventAppStatus e)
        {
            eventAppStatus -= e;
        }
        public void OnEventAppStatus(String status)
        {
            if (null != eventAppStatus)
                eventAppStatus(status);
        }
        #endregion EventAppStatus

        #region EventAppMessage
        public delegate void EventAppMessage(MessageType messageType, String message);
        EventAppMessage eventAppMessage;
        public void AddOnEventAppMessage(EventAppMessage e)
        {
            eventAppMessage += e;
        }
        public void RemoveOnEventAppMessage(EventAppMessage e)
        {
            eventAppMessage -= e;
        }
        public void OnEventAppMessage(MessageType messageType, String message)
        {
            if (null != eventAppMessage)
                eventAppMessage(messageType, message);
        }
        #endregion EventAppMessage

        #region EventNewMap
        public delegate void EventNewMap(String html);
        EventNewMap eventNewMap;
        public void AddOnEventNewMap(EventNewMap e)
        {
            eventNewMap += e;
        }
        public void RemoveOnEventNewMap(EventNewMap e)
        {
            eventNewMap -= e;
        }
        public void OnEventNewMap(String html)
        {
            if (null != eventNewMap)
                eventNewMap(html);
        }
        #endregion EventNewMap

        #region EventTripInfo
        public delegate void EventTripInfo(TripInfo tripInfo);
        EventTripInfo eventTripInfo;
        public void AddOnEventTripInfo(EventTripInfo e)
        {
            eventTripInfo += e;
        }
        public void RemoveOnEventTripInfo(EventTripInfo e)
        {
            eventTripInfo -= e;
        }
        public void OnEventTripInfo(TripInfo tripInfo)
        {
            if (null != eventTripInfo)
                eventTripInfo(tripInfo);
        }
        #endregion EventTripInfo

        #region EventNewTrips
        public delegate void EventNewTrips(IList<long> tripIds);
        EventNewTrips eventNewTrips;
        public void AddOnEventNewTrips(EventNewTrips e)
        {
            eventNewTrips += e;
        }
        public void RemoveOnEventNewTrips(EventNewTrips e)
        {
            eventNewTrips -= e;
        }
        public void OnEventNewTrips(IList<long> tripIds)
        {
            if (null != eventNewTrips)
                eventNewTrips(tripIds);
        }
        #endregion EventNewTrips

        #region EventNewECG
        public delegate void EventNewECG(IList<ECGReading> readings);
        EventNewECG eventNewECG;
        public void AddOnEventNewECG(EventNewECG e)
        {
                eventNewECG += e;
        }
        public void RemoveOnEventNewECG(EventNewECG e)
        {
                eventNewECG -= e;
        }
        public void OnEventNewECG(IList<ECGReading> readings)
        {
            if (null != eventNewECG)
                eventNewECG(readings);
        }
        #endregion EventNewECG

        #region EventShimmerSensorData
        public delegate void EventShimmerSensorData(IList<ShimmerSensorReading> readings);
        EventShimmerSensorData eventShimmerSensorData;
        public void AddOnEventShimmerSensorData(EventShimmerSensorData e)
        {
            eventShimmerSensorData += e;
        }
        public void RemoveOnEventShimmerSensorData(EventShimmerSensorData e)
        {
            eventShimmerSensorData -= e;
        }
        public void OnEventShimmerSensorData(IList<ShimmerSensorReading> readings)
        {
            if (null != eventShimmerSensorData)
                eventShimmerSensorData(readings);
        }
        #endregion EventShimmerSensorData

        #region EventBusData1
        public delegate void EventBusData1();
        EventBusData1 eventBusData1;
        public void AddOnEventBusData1(EventBusData1 e)
        {
            eventBusData1 += e;
        }
        public void RemoveOnEventBusData1(EventBusData1 e)
        {
            eventBusData1 -= e;
        }
        public void OnEventBusData1()
        {
            if (null != eventBusData1)
                eventBusData1();
        }
        #endregion EventBusData1

        #region EventBusData2
        public delegate void EventBusData2(string name1, IList<double> readings1, string name2, IList<double> readings2);
        EventBusData2 eventBusData2;
        public void AddOnEventBusData2(EventBusData2 e)
        {
            eventBusData2 += e;
        }
        public void RemoveOnEventBusData2(EventBusData2 e)
        {
            eventBusData2 -= e;
        }
        public void OnEventBusData2(string name1, IList<double> readings1, string name2, IList<double> readings2)
        {
            if (null != eventBusData2)
                eventBusData2(name1, readings1, name2, readings2);
        }
        #endregion EventBusData2

        #region EventBusData3
        public delegate void EventBusData3(IList<double> readings1);
        EventBusData3 eventBusData3;
        public void AddOnEventBusData3(EventBusData3 e)
        {
            eventBusData3 += e;
        }
        public void RemoveOnEventBusData3(EventBusData3 e)
        {
            eventBusData3 -= e;
        }
        public void OnEventBusData3(IList<double> readings1)
        {
            if (null != eventBusData3)
                eventBusData3(readings1);
        }
        #endregion EventBusData3

        #region EventBusData4
        public delegate void EventBusData4(StopLevelData stopLevelData, GraphSettings graphDwellSettings, LoadGraphSettings graphLoadSettings);
        EventBusData4 eventBusData4;
        public void AddOnEventBusData4(EventBusData4 e)
        {
            eventBusData4 += e;
        }
        public void RemoveOnEventBusData4(EventBusData4 e)
        {
            eventBusData4 -= e;
        }
        public void OnEventBusData4(StopLevelData stopLevelData, GraphSettings graphDwellSettings, LoadGraphSettings graphLoadSettings)
        {
            if (null != eventBusData4)
                eventBusData4(stopLevelData, graphDwellSettings, graphLoadSettings);
        }
        #endregion EventBusData4

        #region EventBusData5
        public delegate void EventBusData5(SpeedData speedData, StopLevelData stopLevelData,
            GraphSettings graphSpeedSettings, GraphSettings graphDwellSettings, LoadGraphSettings graphLoadSettings);
        EventBusData5 eventBusData5;
        public void AddOnEventBusData5(EventBusData5 e)
        {
            eventBusData5 += e;
        }
        public void RemoveOnEventBusData5(EventBusData5 e)
        {
            eventBusData5 -= e;
        }
        public void OnEventBusData5(SpeedData speedData, StopLevelData stopLevelData,
            GraphSettings graphSpeedSettings, GraphSettings graphDwellSettings, LoadGraphSettings graphLoadSettings)
        {
            if (null != eventBusData5)
                eventBusData5(speedData, stopLevelData,
                    graphSpeedSettings, graphDwellSettings, graphLoadSettings);
        }
        #endregion EventBusData5

        #region EventColorSpeedDataPath
        public delegate void EventColorSpeedDataPath(GraphColorRange[] graphColorRanges, int strokeWeight);
        EventColorSpeedDataPath eventColorSpeedDataPath;
        public void AddOnEventColorSpeedDataPath(EventColorSpeedDataPath e)
        {
            eventColorSpeedDataPath += e;
        }
        public void RemoveOnEventColorSpeedDataPath(EventColorSpeedDataPath e)
        {
            eventColorSpeedDataPath -= e;
        }
        public void OnEventColorSpeedDataPath(GraphColorRange[] graphColorRanges, int strokeWeight)
        {
            if (null != eventColorSpeedDataPath)
                eventColorSpeedDataPath(graphColorRanges, strokeWeight);
        }
        #endregion EventColorSpeedDataPath

        #region EventColorGraphData
        public delegate void EventColorGraphData(string dataSetName, GraphColorRange[] graphColorRanges);
        EventColorGraphData eventColorGraphData;
        public void AddOnEventColorGraphData(EventColorGraphData e)
        {
            eventColorGraphData += e;
        }
        public void RemoveOnEventColorGraphData(EventColorGraphData e)
        {
            eventColorGraphData -= e;
        }
        public void OnEventColorGraphData(string dataSetName, GraphColorRange[] graphColorRanges)
        {
            if (null != eventColorGraphData)
                eventColorGraphData(dataSetName, graphColorRanges);
        }
        #endregion EventColorGraphData

        #region EventUpdateDataMarkers
        public delegate void EventUpdateDataMarkers();
        EventUpdateDataMarkers eventUpdateDataMarkers;
        public void AddOnEventUpdateDataMarkers(EventUpdateDataMarkers e)
        {
            eventUpdateDataMarkers += e;
        }
        public void RemoveOnEventUpdateDataMarkers(EventUpdateDataMarkers e)
        {
            eventUpdateDataMarkers -= e;
        }
        public void OnEventUpdateDataMarkers()
        {
            if (null != eventUpdateDataMarkers)
                eventUpdateDataMarkers();
        }
        #endregion EventUpdateDataStopMarkers

        #region EventNewProjectName
        public delegate void EventNewProjectName(String projectName);
        EventNewProjectName eventNewProjectName;
        public void AddOnEventNewProjectName(EventNewProjectName e)
        {
            eventNewProjectName += e;
        }
        public void RemoveOnEventNewProjectName(EventNewProjectName e)
        {
            eventNewProjectName -= e;
        }
        public void OnEventNewProjectName(String projectName)
        {
            if (null != eventNewProjectName)
                eventNewProjectName(projectName);
        }
        #endregion EventNewProjectName

        #region EventSaveProjectFileAs
        public delegate void EventSaveProjectFileAs(out String fileName, out bool cancel);
        EventSaveProjectFileAs eventSaveProjectFileAs;
        public void AddOnEventSaveProjectFileAs(EventSaveProjectFileAs e)
        {
            eventSaveProjectFileAs += e;
        }
        public void RemoveOnEventSaveProjectFileAs(EventSaveProjectFileAs e)
        {
            eventSaveProjectFileAs -= e;
        }
        public void OnEventSaveProjectFileAs(out String fileName, out bool cancel)
        {
            if (null != eventSaveProjectFileAs)
                eventSaveProjectFileAs(out fileName, out cancel);
            else
            {
                fileName = null;
                cancel = false;
            }
        }
        #endregion EventSaveProjectFileAs

        #region EventQuerySaveProject
        public delegate void EventQuerySaveProject(out QuerySaveProjectResponse exitFlag);
        EventQuerySaveProject eventQuerySaveProject;
        public void AddOnEventQuerySaveProject(EventQuerySaveProject e)
        {
            eventQuerySaveProject += e;
        }
        public void RemoveOnEventQuerySaveProject(EventQuerySaveProject e)
        {
            eventQuerySaveProject -= e;
        }
        public void OnEventQuerySaveProject(out QuerySaveProjectResponse exitFlag)
        {
            if (null != eventQuerySaveProject)
                eventQuerySaveProject(out exitFlag);
            else
            {
                exitFlag = QuerySaveProjectResponse.DontSaveProject;
            }
        }
        #endregion EventQuerySaveProject

        #region EventAppExit
        public delegate void EventAppExit();
        EventAppExit eventAppExit;
        public void AddOnEventAppExit(EventAppExit e)
        {
            eventAppExit += e;
        }
        public void RemoveOnEventAppExit(EventAppExit e)
        {
            eventAppExit -= e;
        }
        public void OnEventAppExit()
        {
            if (null != eventAppExit)
                eventAppExit();
        }
        #endregion EventAppExit

        #region EventNewProject
        public delegate void EventNewProject();
        EventNewProject eventNewProject;
        public void AddOnEventNewProject(EventNewProject e)
        {
            eventNewProject += e;
        }
        public void RemoveOnEventNewProject(EventNewProject e)
        {
            eventNewProject -= e;
        }
        public void OnEventNewProject()
        {
            if (null != eventNewProject)
                eventNewProject();
        }
        #endregion EventNewProject

        #region EventProjectChanged
        public delegate void EventProjectChanged();
        EventProjectChanged eventProjectChanged;
        public void AddOnEventProjectChanged(EventProjectChanged e)
        {
            eventProjectChanged += e;
        }
        public void RemoveOnEventProjectChanged(EventProjectChanged e)
        {
            eventProjectChanged -= e;
        }
        public void OnEventProjectChanged()
        {
            if (null != eventProjectChanged)
                eventProjectChanged();
        }
        #endregion EventProjectChanged

        public MyApplication()
        {
            ConfigureSettingsObjects();
            ResetGraphColorRanges();
        }

        private void ConfigureSettingsObjects()
        {
            googleMapSettings.AddOnEventMapSettingsChanged(Map_SettingsChanged);
            graphSpeedSettings.AddOnEventSettingsChanged(Graph_SettingsChanged);
            graphDwellSettings.AddOnEventSettingsChanged(Graph_SettingsChanged);
            graphLoadSettings.AddOnEventSettingsChanged(Graph_SettingsChanged);
            graphEcgSettings.AddOnEventSettingsChanged(Graph_SettingsChanged);
            graphGsrSettings.AddOnEventSettingsChanged(Graph_SettingsChanged);

            graphSpeedSettings.Other1 = graphDwellSettings;
            graphSpeedSettings.Other2 = graphLoadSettings;

            graphDwellSettings.Other1 = graphSpeedSettings;
            graphDwellSettings.Other2 = graphLoadSettings;

            graphLoadSettings.Other1 = graphSpeedSettings;
            graphLoadSettings.Other2 = graphDwellSettings;

            properties.Add(googleMapSettings);
            properties.Add(graphSpeedSettings);
            properties.Add(graphDwellSettings);
            properties.Add(graphLoadSettings);
            properties.Add(graphEcgSettings);
            properties.Add(graphGsrSettings);
        }

        private void UnconfigureSettingsObjects()
        {
            properties.Remove(googleMapSettings);
            properties.Remove(graphSpeedSettings);
            properties.Remove(graphDwellSettings);
            properties.Remove(graphLoadSettings);
            properties.Remove(graphEcgSettings);
            properties.Remove(graphGsrSettings);

            graphSpeedSettings.Other1 = null;
            graphSpeedSettings.Other2 = null;

            graphDwellSettings.Other1 = null;
            graphDwellSettings.Other2 = null;

            graphLoadSettings.Other1 = null;
            graphLoadSettings.Other2 = null;

            googleMapSettings.RemoveOnEventMapSettingsChanged(Map_SettingsChanged);
            graphSpeedSettings.RemoveOnEventSettingsChanged(Graph_SettingsChanged);
            graphDwellSettings.RemoveOnEventSettingsChanged(Graph_SettingsChanged);
            graphLoadSettings.RemoveOnEventSettingsChanged(Graph_SettingsChanged);
            graphEcgSettings.RemoveOnEventSettingsChanged(Graph_SettingsChanged);
            graphGsrSettings.RemoveOnEventSettingsChanged(Graph_SettingsChanged);
        }

        private void ResetGraphColorRanges()
        {
            GraphColorRange[] SpeedGraphColorRanges = new GraphColorRange[1];
            GraphColorRange[] DwellGraphColorRanges = new GraphColorRange[1];
            GraphColorRange[] EcgGraphColorRanges = new GraphColorRange[1];
            GraphColorRange[] GsrGraphColorRanges = new GraphColorRange[1];

            SpeedGraphColorRanges[0] = new GraphColorRange(double.MinValue, double.MaxValue, Color.Black);
            DwellGraphColorRanges[0] = new GraphColorRange(double.MinValue, double.MaxValue, Color.Black);
            EcgGraphColorRanges[0] = new GraphColorRange(double.MinValue, double.MaxValue, Color.Black);
            GsrGraphColorRanges[0] = new GraphColorRange(double.MinValue, double.MaxValue, Color.Black);

            dictGraphColorRanges.Clear();
            dictGraphColorRanges.Add(DataSetName.Speed, SpeedGraphColorRanges);
            dictGraphColorRanges.Add(DataSetName.Dwell, DwellGraphColorRanges);
            dictGraphColorRanges.Add(DataSetName.ECG, EcgGraphColorRanges);
            dictGraphColorRanges.Add(DataSetName.GSR, GsrGraphColorRanges);
        }

        #region MapSettings Event Handlers

        private void Map_SettingsChanged(GoogleMapSettings mapSettings)
        {
            projectFile.Touch();

            //Refresh(mapSettings);
            OnEventUpdateDataMarkers();
        }

        public void Refresh(GoogleMapSettings mapSettings)
        {
            //ues.CopyTo(this.userEditableSettings);
            if (null != currentTrip)
            {
                currentTrip.Update(this, mapSettings);
            }
        }

        private void Graph_SettingsChanged()
        {
            RefreshGraphs();
            projectFile.Touch();
        }

        public void RefreshGraphs()
        {
            if (null != currentTrip)
            {
                currentTrip.UpdateGraph(this);
            }
            OnEventAppStatus("Ready");
        }

        #endregion MapSettings Event Handlers

        public IList<long> TripIDs
        {
            get { return this.tripKeys.AsReadOnly(); }
        }

        public void SetDataSourceBikeData(IList<long> tripIDs)
        {
            dataSource = new DataSourceDatabase();

            foreach (long tripID in tripIDs)
            {
                try
                {
                    currentTrip = new BsensorTrip(tripID);
                    //currentTrip.Update(this);
                    projectFile.Touch();
                }
                catch (Exception ex)
                {
                    Util.LogException(MODULE_TAG, "Trip ID: " + tripID.ToString(),  ex);
                }
                return; // TODO: for now we will NOT display multiple trips
            }
        }

        public void SetDataSourceBusData(string fileName)
        {
            string errMsg;

            if (null == (tripKeys = SensorTrip.ImportTrips(fileName, out errMsg, out trips)))
            {
                OnEventAppMessage(MessageType.Error, errMsg);
                return;
            }

            dataSource = new DataSourceCSVFile(fileName);

            //
            projectFile.Touch();
            OnEventNewTrips(tripKeys);
            OnEventAppStatus("Ready");
        }

        public void SetTrip(int key)
        {
            currentTrip = trips[key];
            currentTrip.Update(this, googleMapSettings);
        }

        /// <summary>
        /// Returns the array of graph colors associated with the given data set
        /// </summary>
        /// <param name="dataSetName">Selects the set of graph color ranges to return</param>
        /// <returns>An array of Graph color ranges</returns>
        public GraphColorRange[] GetGraphColorRanges(string dataSetName)
        {
            if (dictGraphColorRanges.ContainsKey(dataSetName))
            {
                return dictGraphColorRanges[dataSetName];
            }
            return null;
        }

        /// <summary>
        /// Associates an array of graph color ranges to the given data set.
        /// </summary>
        /// <param name="dataSetName"></param>
        /// <param name="graphColorRanges"></param>
        public void AddRanges(string dataSetName, GraphColorRange[] graphColorRanges, bool redraw)
        {
            dictGraphColorRanges[dataSetName] = graphColorRanges;
            projectFile.Touch();
            if (redraw)
            {
                ColorBusSpeedData(dataSetName);
            }
        }

        public void ColorBusSpeedData(string dataSetName)
        {
            GraphColorRange[] graphColorRanges;

            if (null != (graphColorRanges = GetGraphColorRanges(dataSetName)))
            {
                if (dataSetName.Equals(DataSetName.Speed))
                {
                    OnEventColorSpeedDataPath(graphColorRanges, googleMapSettings.SpeedStrokeWeight);
                }
                OnEventColorGraphData(dataSetName, graphColorRanges);
            }

        }

        public void Exit()
        {
            if (projectFile.Touched)
            {
                QuerySaveProjectResponse exitFlag;
                bool cancel;

                OnEventQuerySaveProject(out exitFlag);
                switch (exitFlag)
                {
                    case QuerySaveProjectResponse.SaveProject:
                        SaveProject(out cancel);
                        if (!cancel)
                            OnEventAppExit();
                        break;

                    case QuerySaveProjectResponse.DontSaveProject:
                        OnEventAppExit();
                        break;

                    case QuerySaveProjectResponse.CancelOperation:
                        break;
                }
            }
            else
            {
                OnEventAppExit();
            }
        }

        public void SaveProject(out bool cancel)
        {
            SaveProjectAs(projectFile.FileName, out cancel);
        }

        public void SaveProjectAs(out bool cancel)
        {
            SaveProjectAs(null, out cancel);
        }

        /// <summary>
        /// Saves the project to the given file name.  Assumptions are that
        /// the user has already indicated that the file can be overwritten
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveProjectAs(string fileName, out bool cancel)
        {
            cancel = false;

            // If the file name is blank, get a filename
            if (String.IsNullOrWhiteSpace(fileName))
            {
                OnEventSaveProjectFileAs(out fileName, out cancel);
            }

            // If user hasn't cancelled operation save file with given file name
            if (!cancel)
            {
                projectFile.SaveAs(fileName, dataSource, dictGraphColorRanges, properties);
                OnEventNewProjectName(Util.ProjectNameFromFileName(fileName));
            }
        }

        public void NewProject()
        {
            bool cancel;

            // Ask user to save the current project if it has changed,
            //  or allow user to cancel the New Project operation
            QuerySaveProject(out cancel);
            if (cancel)
            {
                return;
            }

            tripKeys.Clear();
            trips.Clear();
            dictDataSets.Clear();
            currentTrip = null;
            ResetGraphColorRanges();

            projectFile.Clear();
            OnEventNewProject();
            OnEventNewProjectName("untitled");
            OnEventProjectChanged();
            OnEventAppStatus("Ready");
        }

        public void QuerySaveProject(out bool cancel)
        {
            cancel = false;

            if (projectFile.Touched)
            {
                QuerySaveProjectResponse response;

                OnEventQuerySaveProject(out response);
                switch (response)
                {
                    case QuerySaveProjectResponse.SaveProject:
                        SaveProject(out cancel);
                        break;

                    case QuerySaveProjectResponse.DontSaveProject:
                        break;

                    case QuerySaveProjectResponse.CancelOperation:
                        cancel = true;
                        return;

                    default:
                        throw new Exception("Invalid response encountered");
                }
            }
        }

        public void OpenProject(string fileName)
        {
            string errMsg;
            bool cancel;
            DataSourceCSVFile dataSourceCSVFile;
            DataSourceDatabase dataSourceDatabase;

            // Ignore trying to reopen the same project file
            if (fileName.Equals(projectFile.FileName))
            {
                OnEventAppMessage(MessageType.Information, MSG_FILE_ALREADY_OPEN);
                return;
            }

            // Ask user to save the current project if it has changed,
            //  or allow user to cancel the Open project operation
            QuerySaveProject(out cancel);
            if (cancel)
            {
                return;
            }

            if (projectFile.OpenFile(fileName, out errMsg))
            {
                OnEventNewProjectName(Util.ProjectNameFromFileName(fileName));

                ResetGraphColorRanges();

                GraphColorRange[] graphColorRanges;
                foreach (string dataSetName in projectFile.DictGraphColorRanges.Keys)
                {
                    graphColorRanges = projectFile.DictGraphColorRanges[dataSetName];
                    AddRanges(dataSetName, graphColorRanges, false);
                }

                if (null != (dataSourceCSVFile = projectFile.DataSource as DataSourceCSVFile))
                {
                    SetDataSourceBusData(dataSourceCSVFile.FileName);
                }
                else if (null != (dataSourceDatabase = projectFile.DataSource as DataSourceDatabase))
                {
                    //ImportFileTripDB(((ProjectFileTripDB1)trip).TripID);
                }

                foreach (string dataSetName in projectFile.DictGraphColorRanges.Keys)
                {
                    //ColorCoordinateDataSets(dataSetName);
                }

                UnconfigureSettingsObjects();

                if (null != projectFile.GoogleMapSettings)
                {
                    googleMapSettings = projectFile.GoogleMapSettings;
                }

                if (null != projectFile.GraphSpeedSettings)
                {
                    graphSpeedSettings = projectFile.GraphSpeedSettings;
                }

                if (null != projectFile.GraphDwellSettings)
                {
                    graphDwellSettings = projectFile.GraphDwellSettings;
                }

                if (null != projectFile.GraphLoadSettings)
                {
                    graphLoadSettings = projectFile.GraphLoadSettings;
                }

                if (null != projectFile.GraphEcgSettings)
                {
                    graphEcgSettings = projectFile.GraphEcgSettings;
                }

                if (null != projectFile.GraphGsrSettings)
                {
                    graphGsrSettings = projectFile.GraphGsrSettings;
                }

                ConfigureSettingsObjects();

                projectFile.Untouch();

                OnEventProjectChanged();
                OnEventAppStatus("Ready");
            }
            else
            {
                OnEventAppMessage(MessageType.Error, errMsg);
                OnEventAppStatus("File not opened: " + fileName);
            }
        }

        public bool ProjectNotSaved
        {
            get { return projectFile.Touched; }
        }

        private enum EPropertyIndex { Map, GraphSpeed, GraphDwell, GraphLoad, BikeECG, BikeGSR };

        public IList<object> Properties
        {
            get { return properties.AsReadOnly();            }
        }

    }

}
