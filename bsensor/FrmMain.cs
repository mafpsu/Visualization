using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
namespace bsensor
{
    public partial class FrmMain : Form
    {
        private const string MODULE_TAG = "FrmMain";
        private const string TITLE_PREFIX = "BSENSOR ";
        private const string STATUS_READY = "Ready. ";
        private const string QUERY_SAVE_PROJECT = "Save project changes?";
        private const string TITLE_SAVE_PROJECT = "Save Project";
        private static readonly Color DEFAULT_COLOR_DWELL = Color.Gold;
        private static readonly Color DEFAULT_COLOR_LOAD = Color.Black;
        private static readonly Color DEFAULT_COLOR_LOAD_ONS = Color.Red;
        private static readonly Color DEFAULT_COLOR_LOAD_OFFS = Color.Blue;

        private MyApplication myApp = MyApplication.instance;
        private BsensorCommand cmdSetDataSourceBikeData = null;
        private BsensorCommand cmdSetDataSourceBusData = null;
        private BsensorCommand cmdSaveProjectAs = null;
        private BsensorCommand cmdSaveProject = null;
        private BsensorCommand cmdNewProject = null;
        private BsensorCommand cmdOpenProject = null;
        private BsensorCommand cmdRefreshGraphs = null;
        private BsensorCommand cmdSetTrip = null;
        private BsensorCommand cmdAppExit = null;
        private BsensorCommand cmdAsyncColorMap = null;

        private IList<double> speeds;
        private SpeedData speedData;
        private StopLevelData stopLevelData;
        private FrmGraphs frmGraphs = new FrmGraphs();
        private PlayControlController pcc = null;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                pcc = new PlayControlController(btnPlayPauseForward, btnPlayPauseReverse, 
                    btnStepForward, btnStepReverse, btnStop, tmrPlay);
                pcc.AddOnEventPlayFinished(Main_ShowAllDataMarkers);
                pcc.AddOnEventTimerTick(Main_PlayTimerTick);
                pcc.AddOnEventClearMarkers(Main_ClearAllDataMarkers);

                cmdAppExit = new CmdAppExit(myApp);
                cmdSetDataSourceBikeData = new CmdSetDataSourceBikeData(myApp);
                cmdSetDataSourceBusData = new CmdSetDataSourceBusData(myApp);
                cmdSaveProjectAs = new CmdSaveProjectAs(myApp);
                cmdSaveProject = new CmdSaveProject(myApp);
                cmdNewProject = new CmdNewProject(myApp);
                cmdOpenProject = new CmdOpenProject(myApp);
                cmdRefreshGraphs = new CmdRefreshGraphs(myApp);
                cmdSetTrip = new CmdSetTrip(myApp);
                cmdAsyncColorMap = new CmdAsyncColorMap(myApp);

                myApp.AddOnEventAppExit(Main_AppExit);
                myApp.AddOnEventAppStatus(Main_EventAppStatus);
                myApp.AddOnEventAppMessage(Main_EventAppMessage);
                myApp.AddOnEventNewMap(Main_EventNewMap);
                myApp.AddOnEventNewECG(Main_EventNewECG);
                myApp.AddOnEventBusData1(Main_EventBusData1);
                myApp.AddOnEventBusData2(Main_EventBusData2);
                myApp.AddOnEventBusData3(Main_EventBusData3);
                myApp.AddOnEventBusData4(Main_EventBusData4);
                myApp.AddOnEventBusData5(Main_EventBusData5);
                myApp.AddOnEventTripInfo(Main_TripInfo);
                myApp.AddOnEventColorSpeedDataPath(Main_ColorSpeedDataPath);
                myApp.AddOnEventUpdateDataMarkers(Main_ShowAllDataMarkers);
                myApp.AddOnEventNewTrips(Main_NewTrips);
                myApp.AddOnEventNewProjectName(Main_EventNewProjectName);
                myApp.AddOnEventSaveProjectFileAs(Main_SaveProjectFileAs);
                myApp.AddOnEventQuerySaveProject(Main_QuerySaveBeforeExit);
                myApp.AddOnEventNewProject(Main_NewProject);
                myApp.AddOnEventProjectChanged(Main_ProjectChanged);

                mnuViewGraphTypeRange.Checked = true;
                mnuViewGraphTypeRangeColumn.Checked = false;
                mnuViewGraphTypeRangeSpline.Checked = false;

                UpdatePropertyGrid();

                frmGraphs.AddOnEventGraphCursorPositionChanged(Main_EventGraphCursorPositionChanged);
                frmGraphs.Show();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void UpdatePropertyGrid()
        {
            cbProperties.Items.Clear();
            foreach (object properties in myApp.Properties)
            {
                cbProperties.Items.Add(properties);
            }
            cbProperties.SelectedIndex = 0;
        }

        private void Main_EventNewProjectName(string projectName)
        {
            Text = "BSENSOR: " + projectName;
        }

        private void Main_EventAppStatus(String status)
        {
            try
            {
                LogStatus(status);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void Main_EventAppMessage(MyApplication.MessageType messageType, String message)
        {
            try
            {
                switch (messageType)
                {
                    case MyApplication.MessageType.Error:
                        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case MyApplication.MessageType.Warning:
                        MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case MyApplication.MessageType.Information:
                        MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void Main_EventNewMap(String html)
        {
            try
            {
                webBrowser1.DocumentText = html;
                tmrAsyncColor.Enabled = true;
                LogStatus("Trip loaded.");
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void Main_EventNewECG(IList<ECGReading> readings)
        {
            try {
                frmGraphs.EventNewECG(readings);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void Main_EventBusData1()
        {
            try
            {
                frmGraphs.EventBusData1();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
        
        private void Main_EventBusData2(string name1, IList<double> data1, string name2, IList<double> data2)
        {
            try
            {
                frmGraphs.EventBusData2(name1, data1, name2, data2);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void Main_EventBusData3(IList<double> speeds)
        {
            try
            {
                this.speeds = speeds;
                frmGraphs.EventBusData3(speeds);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void Main_EventBusData4(StopLevelData stopLevelData, GraphSettings graphDwellSettings, LoadGraphSettings graphLoadSettings)
        {
            try
            {
                pcc.StopLevelData = this.stopLevelData = stopLevelData;

                frmGraphs.EventBusData4(stopLevelData, graphDwellSettings, graphLoadSettings);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void Main_EventBusData5(SpeedData speedData, StopLevelData stopLevelData,
            GraphSettings graphSpeedSettings, GraphSettings graphDwellSettings, LoadGraphSettings graphLoadSettings)
        {
            try
            {
                this.speedData = speedData;
                this.speeds = speedData.Speeds;
                pcc.StopLevelData = this.stopLevelData = stopLevelData;
                frmGraphs.EventBusData5(speedData, stopLevelData, 
                    graphSpeedSettings, graphDwellSettings, graphLoadSettings);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void Main_EventGraphCursorPositionChanged(int dataIndex, string dataSetName)
        {
            LatLng latLng = null;

            try
            {
                if (dataSetName.Equals(DataSetName.Speed) &&
                (null != speedData) &&
                (null != speedData.LatLngs) &&
                (dataIndex < speedData.LatLngs.Count))
                {
                    latLng = speedData.LatLngs[dataIndex];
                }
                else if (dataSetName.Equals(DataSetName.StopLevel) &&
                    (null != stopLevelData) &&
                    (null != stopLevelData.LatLngs) &&
                    (dataIndex < stopLevelData.LatLngs.Count))
                {
                    latLng = stopLevelData.LatLngs[dataIndex];
                }

                if (null != latLng)
                {
                    SetCursorMarker(latLng.Latitude, latLng.Longitude);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void SetCursorMarker(double lat, double lng)
        {
            if (webBrowser1.Document != null)
            {
                Object[] objArray = new Object[3];
                objArray[0] = (Object)lat;
                objArray[1] = (Object)lng;
                webBrowser1.Document.InvokeScript("setcursormarker", objArray);
            }
        }

        private void Main_TripInfo(TripInfo tripInfo)
        {
            LogNumPoints(tripInfo.NumPoints);
        }

        private void Main_NewProject()
        {
            try
            {
                frmGraphs.EventNewProject();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void Main_ProjectChanged()
        {
            try
            {
                UpdatePropertyGrid();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void Main_NewTrips(IList<long> Keys)
        {
            cbTripIDs.Items.Clear();
            foreach(long key in Keys)
            {
                cbTripIDs.Items.Add(key.ToString());
            }

            cbTripIDs.SelectedIndex = 0;
        }

        public void Main_ColorSpeedDataPath(GraphColorRange[] graphColorRanges, int strokeWeight)
        {
            if (null != graphColorRanges)
            {
                foreach (GraphColorRange g in graphColorRanges)
                {
                    ColorSpeedDataPath(g.Min, g.Max, g.Color, strokeWeight);
                }
            }
        }

        public void ColorSpeedDataPath(double lowValue, double highValue, Color color, int strokeWeight)
        {
            if (null != speeds)
            {
                double value;
                bool inRange = false;
                int first = -1;
                int last = -1;
                int i;

                for (i = 1; i < speeds.Count; ++i)
                {
                    value = speeds[i];
                    if ((value > lowValue) && (value <= highValue))
                    {
                        if (!inRange)
                        {
                            first = i - 1;
                            inRange = true;
                        }
                    }
                    else
                    {
                        if (inRange)
                        {
                            last = i - 1;
                            inRange = false;
                            WebBrowser1_ColorSpeedDataPath(first, last, color, strokeWeight);
                        }
                    }
                }

                if (inRange)
                {
                    last = i - 1;
                    WebBrowser1_ColorSpeedDataPath(first, last, color, strokeWeight);
                }
            }
        }

        private void WebBrowser1_ColorSpeedDataPath(int first, int last, Color color, int strokeWeight)
        {
            try
            {
                if (webBrowser1.Document != null)
                {
                    Object[] objArray = new Object[4];
                    objArray[0] = (Object)first;
                    objArray[1] = (Object)last;
                    objArray[2] = (Object)Util.ToRGB(color);
                    objArray[3] = (Object)strokeWeight;
                    webBrowser1.Document.InvokeScript("color_coordinate", objArray);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// Exits the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            try
            {
                cmdAppExit.Execute();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        public void Main_QuerySaveBeforeExit(out MyApplication.QuerySaveProjectResponse exitFlag)
        {
            DialogResult result;

            if (DialogResult.Yes == (result = MessageBox.Show(QUERY_SAVE_PROJECT, TITLE_SAVE_PROJECT, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)))
            {
                exitFlag = MyApplication.QuerySaveProjectResponse.SaveProject;
            }
            else if (DialogResult.No == result)
            {
                exitFlag = MyApplication.QuerySaveProjectResponse.DontSaveProject;
            }
            else
            {
                exitFlag = MyApplication.QuerySaveProjectResponse.CancelOperation;
            }
        }

        public void Main_AppExit()
        {
            this.Close();
        }

        /// <summary>
        /// Opens a trip file and displays the trip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            try
            {
                dlgOpenFile.Filter = "Project files (*.bsn)|*.bsn";
                dlgOpenFile.FilterIndex = 0;
                dlgOpenFile.RestoreDirectory = true;
                dlgOpenFile.ValidateNames = true;

                if (dlgOpenFile.ShowDialog() == DialogResult.OK)
                {
                    String fileName = dlgOpenFile.FileName;
                    LogStatus("Opening file " + fileName + "...");
                    cmdOpenProject.Execute(fileName);
                }
                else
                {
                    LogStatus(STATUS_READY);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileImport_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void LogStatus(string message)
        {
            ssStatus.Items[0].Text = message;
        }

        private void LogNumPoints(int numPoints)
        {
            ssStatus.Items[1].Text = numPoints.ToString() + " points";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        private void LogException(Exception ex)
        {
            Util.LogException(MODULE_TAG, ex);
            ssStatus.Items[0].Text = ex.Message;
        }

        private void setTitle(IList<long> tripIDs)
        {
            this.Text = TITLE_PREFIX;
            foreach (long tripId in tripIDs)
            {
                this.Text += (" " + tripId.ToString());
            }
        }

        private void setTitle(string fileTitle)
        {
            this.Text = TITLE_PREFIX + fileTitle;
        }

        private void mnuDataSourceBikeData_Click(object sender, EventArgs e)
        {
            try
            {
                LogStatus("Loading trip...");
                List<long> tripIDs = new List<long>(myApp.TripIDs);

                FrmImportTrip frmImportTrip = new FrmImportTrip(tripIDs);

                if (DialogResult.OK == frmImportTrip.ShowDialog())
                {
                    //this.tabControl1.Controls.Remove(this.tabGSR);
                    cmdSetDataSourceBikeData.Execute(tripIDs);
                    setTitle(tripIDs);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void mnuDataSourceBusData_Click(object sender, EventArgs e)
        {
            try
            {
                dlgOpenFile.Filter = "Bus data files (*.bsd)|*.bsd|All files (*.*)|*.*";
                dlgOpenFile.FilterIndex = 1;
                dlgOpenFile.RestoreDirectory = true;
                dlgOpenFile.ValidateNames = true;

                if (dlgOpenFile.ShowDialog() == DialogResult.OK)
                {
                    String fileName = dlgOpenFile.FileName;
                    LogStatus("Opening file " + fileName + "...");
                    cmdSetDataSourceBusData.Execute(fileName);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.ShowDialog();
        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            try {
                cmdSaveProject.Execute();
                LogStatus(STATUS_READY);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void mnuFileSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                cmdSaveProjectAs.Execute();
                LogStatus(STATUS_READY);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void Main_SaveProjectFileAs(out String fileName, out bool cancel)
        {
            dlgSaveFile.OverwritePrompt = true;
            dlgSaveFile.AddExtension = true;
            dlgSaveFile.CheckPathExists = true;
            dlgSaveFile.DefaultExt = "bsn";
            dlgSaveFile.Filter = "Project files(*.bsn) | *.bsn";
            dlgSaveFile.FilterIndex = 0;
            if (DialogResult.OK == dlgSaveFile.ShowDialog())
            {
                fileName = dlgSaveFile.FileName;
                cancel = false;
            }
            else
            {
                fileName = null;
                cancel = true;
            }
        }

        private void mnuFileNew_Click(object sender, EventArgs e)
        {
            try
            {
                cmdNewProject.Execute();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void cbGraphType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmdRefreshGraphs.Execute();
            }
            catch(Exception ex)
            {
                LogException(ex);
            }
        }

        private void cbTripIDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                int key;
                string txt;
                //cmdSetTrip.Execute(cbTripIDs.SelectedIndex);

                txt = cbTripIDs.SelectedItem.ToString();
                if (int.TryParse(txt, out key))
                {
                    cmdSetTrip.Execute(key);
                    //timer1.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void mnuViewGraphTypeRange_Click(object sender, EventArgs e)
        {
            /*frmGraphs.ChartType = FrmGraphs.BusChartType.Range;

            mnuViewGraphTypeRange.Checked = true;
            mnuViewGraphTypeRangeColumn.Checked = false;
            mnuViewGraphTypeRangeSpline.Checked = false;
            cmdRefreshGraphs.Execute();*/
        }

        private void mnuViewGraphTypeRangeColumn_Click(object sender, EventArgs e)
        {
            /*frmGraphs.ChartType = FrmGraphs.BusChartType.RangeColumn;

            mnuViewGraphTypeRange.Checked = false;
            mnuViewGraphTypeRangeColumn.Checked = true;
            mnuViewGraphTypeRangeSpline.Checked = false;
            cmdRefreshGraphs.Execute();*/
        }

        private void mnuViewGraphTypeRangeSpline_Click(object sender, EventArgs e)
        {
            /*frmGraphs.ChartType = FrmGraphs.BusChartType.SplineRange;

            mnuViewGraphTypeRange.Checked = false;
            mnuViewGraphTypeRangeColumn.Checked = false;
            mnuViewGraphTypeRangeSpline.Checked = true;
            cmdRefreshGraphs.Execute();*/
        }

        private void tmrAsyncColor_Tick(object sender, EventArgs e)
        {
            try
            {
                tmrAsyncColor.Enabled = false;
                Main_ShowAllDataMarkers();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        // ********************************************************************************
        // *
        // ********************************************************************************

        private void btnPlayPauseForward_Click(object sender, EventArgs e)
        {
            try
            {
                pcc.PlayPauseForward();
            }
            catch (Exception ex)
            {
                Util.LogException(MODULE_TAG, ex);
            }
        }

        private void btnPlayPauseReverse_Click(object sender, EventArgs e)
        {
            try
            {
                pcc.PlayPauseReverse();
            }
            catch (Exception ex)
            {
                Util.LogException(MODULE_TAG, ex);
            }
        }

        private void btnStepForward_Click(object sender, EventArgs e)
        {
            try
            {
                pcc.StepForward();
            }
            catch (Exception ex)
            {
                Util.LogException(MODULE_TAG, ex);
            }
        }

        private void btnStepReverse_Click(object sender, EventArgs e)
        {
            try
            {
                pcc.StepReverse();
            }
            catch (Exception ex)
            {
                Util.LogException(MODULE_TAG, ex);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                pcc.Stop();
            }
            catch (Exception ex)
            {
                Util.LogException(MODULE_TAG, ex);
            }
        }


        // ********************************************************************************
        // *
        // ********************************************************************************

        public void Main_ShowAllDataMarkers()
        {
            Main_UpdateDataMarkers(null);
        }

        public void Main_PlayTimerTick(int markerIndex, bool isForward)
        {
            if (isForward)
            {
                Main_UpdateDataMarkers(markerIndex);
            }
            else
            {
                Main_ClearDataMarkers(markerIndex);
            }
        }

        public void Main_ClearAllDataMarkers()
        {
            Main_ClearDataMarkers(null);
        }

        public void Main_ClearDataMarkers(int? markerIndex)
        {
            if ((null != stopLevelData) && (stopLevelData.LatLngs.Count != 0))
            {
                LatLng latlng;
                Color markerColor = Color.White;
                string dataLabel = "";
                int firstIndex;
                int lastIndex;

                firstIndex = markerIndex ?? 0;
                lastIndex = markerIndex ?? stopLevelData.LatLngs.Count - 1;

                for (int i = firstIndex; i <= lastIndex; ++i)
                {
                    latlng = stopLevelData.LatLngs[i];

                    switch (myApp.GraphMapSettings.Data)
                    {
                        case GoogleMapSettings.EMapDisplayData.Dwell:

                            Main_UpdateDataMarker(i, stopLevelData.Dwells[i], markerColor, myApp.GraphMapSettings.DwellScaleFactor, dataLabel, 0.0, 0.0);
                            break;

                        case GoogleMapSettings.EMapDisplayData.Load:

                            Main_UpdateDataMarker(i, stopLevelData.BusLoads[i].Load, markerColor, myApp.GraphMapSettings.LoadScaleFactor, dataLabel, 0.0, 0.0);
                            break;

                        case GoogleMapSettings.EMapDisplayData.Ons:

                            Main_UpdateDataMarker(i, stopLevelData.BusLoads[i].Ons, markerColor, myApp.GraphMapSettings.LoadScaleFactor, dataLabel, 0.0, 0.0);
                            break;

                        case GoogleMapSettings.EMapDisplayData.Offs:

                            Main_UpdateDataMarker(i, stopLevelData.BusLoads[i].Offs, markerColor, myApp.GraphMapSettings.LoadScaleFactor, dataLabel, 0.0, 0.0);
                            break;
                    }
                }
            }
        }

        public void Main_UpdateDataMarkers(int? markerIndex)
        {
            GraphColorRange[] dwellColorRanges = myApp.GetGraphColorRanges(DataSetName.Dwell);
            GraphColorRange[] speedColorRanges = myApp.GetGraphColorRanges(DataSetName.Speed);

            if ((null != stopLevelData) && (stopLevelData.LatLngs.Count != 0))
            {
                LatLng latlng;
                Color dwellColor;
                string dataLabel;
                int firstIndex;
                int lastIndex;

                firstIndex = markerIndex ?? 0;
                lastIndex = markerIndex ?? stopLevelData.LatLngs.Count - 1;

                for (int i = firstIndex; i <= lastIndex; ++i)
                {
                    latlng = stopLevelData.LatLngs[i];

                    dataLabel = BusLoadLabel(myApp.GraphMapSettings.Data, stopLevelData.BusLoads[i], stopLevelData.Dwells[i]);

                    switch (myApp.GraphMapSettings.Data)
                    {
                        case GoogleMapSettings.EMapDisplayData.Dwell:

                            dwellColor = DwellColor(stopLevelData.Dwells[i], dwellColorRanges);
                            Main_UpdateDataMarker(i, stopLevelData.Dwells[i], dwellColor, 
                                myApp.GraphMapSettings.DwellScaleFactor, dataLabel,
                                myApp.GraphMapSettings.FillOpacity, myApp.GraphMapSettings.StrokeOpacity);
                            break;

                        case GoogleMapSettings.EMapDisplayData.Load:

                            Main_UpdateDataMarker(i, stopLevelData.BusLoads[i].Load, DEFAULT_COLOR_LOAD, 
                                myApp.GraphMapSettings.LoadScaleFactor, dataLabel,
                                myApp.GraphMapSettings.FillOpacity, myApp.GraphMapSettings.StrokeOpacity);
                            break;

                        case GoogleMapSettings.EMapDisplayData.Ons:

                            Main_UpdateDataMarker(i, stopLevelData.BusLoads[i].Ons, DEFAULT_COLOR_LOAD_ONS, 
                                myApp.GraphMapSettings.LoadScaleFactor, dataLabel,
                                myApp.GraphMapSettings.FillOpacity, myApp.GraphMapSettings.StrokeOpacity);
                            break;

                        case GoogleMapSettings.EMapDisplayData.Offs:

                            Main_UpdateDataMarker(i, stopLevelData.BusLoads[i].Offs, DEFAULT_COLOR_LOAD_OFFS, 
                                myApp.GraphMapSettings.LoadScaleFactor, dataLabel,
                                myApp.GraphMapSettings.FillOpacity, myApp.GraphMapSettings.StrokeOpacity);
                            break;
                    }
                }
            }


            if ((null != speedData) && (null == markerIndex))
            {
                Main_UpdateRoute(myApp.GraphMapSettings.RouteStrokeWeight);
                Main_ColorSpeedDataPath(speedColorRanges, myApp.GraphMapSettings.SpeedStrokeWeight);
            }
        }

        // ********************************************************************************
        // *
        // ********************************************************************************

        private string BusLoadLabel(GoogleMapSettings.EMapDisplayData e, BusLoad busLoad, double dwell)
        {
            switch (e)
            {
                case GoogleMapSettings.EMapDisplayData.Dwell:
                    return "On:" + busLoad.Ons + "\nOff:" + busLoad.Offs + "\nLoad: " + busLoad.Load  + "\nDwell: <" + dwell + ">";

                case GoogleMapSettings.EMapDisplayData.Load:
                    return "On:" + busLoad.Ons + "\nOff:" + busLoad.Offs +"\nLoad: <" + busLoad.Load  + ">\nDwell: " + dwell;

                case GoogleMapSettings.EMapDisplayData.Ons:
                    return "On: <" + busLoad.Ons + ">\nOff:" + busLoad.Offs + "\nLoad: " + busLoad.Load + "\nDwell: " + dwell;

                case GoogleMapSettings.EMapDisplayData.Offs:
                    return "On:" + busLoad.Ons + "\nOff: <" + busLoad.Offs + ">\nLoad: " + busLoad.Load + "\nDwell: " + dwell;
            }
            return null;
        }

        private Color DwellColor(double dwell, GraphColorRange[] graphColorRanges)
        {
            Color dwellColor = DEFAULT_COLOR_DWELL;

            foreach (GraphColorRange g in graphColorRanges)
            {
                if ((g.Min <= dwell) && (dwell <= g.Max))
                {
                    dwellColor = g.Color;
                    break;
                }
            }
            return dwellColor;
        }

        private void Main_UpdateRoute(int routeStrokeWeight)
        {
            if (webBrowser1.Document != null)
            {
                Object[] objArray = new Object[1];
                objArray[0] = (Object)routeStrokeWeight;
                webBrowser1.Document.InvokeScript("update_route", objArray);
            }
        }

        private void Main_UpdateDataMarker(int markerIndex, double data, Color color, double scaleFactor, string dataLabel,
            double fillOpacity, double strokeOpacity)
        {
            if (webBrowser1.Document != null)
            {
                Object[] objArray = new Object[6];
                objArray[0] = (Object)markerIndex;
                objArray[1] = (Object)(data / scaleFactor);
                objArray[2] = (Object)Util.ToRGB(color);
                objArray[3] = (Object)fillOpacity;
                objArray[4] = (Object)strokeOpacity;
                objArray[5] = (Object)dataLabel;
                webBrowser1.Document.InvokeScript("update_data_marker", objArray);
            }
        }

        private void mnuViewProperties_Click(object sender, EventArgs e)
        {
            try
            {
                mnuViewProperties.Checked = !mnuViewProperties.Checked;
                scMain.Panel2Collapsed = !mnuViewProperties.Checked;
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void cbProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                propertyGrid1.SelectedObject = cbProperties.SelectedItem;
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void lblCloseProperties_Click(object sender, EventArgs e)
        {
            scMain.Panel2Collapsed = true;
            mnuViewProperties.Checked = false;
        }
    }
}

