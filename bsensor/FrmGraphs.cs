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

namespace bsensor
{
    public partial class FrmGraphs : Form
    {
        private MyApplication myApp = MyApplication.instance;
        private const string MODULE_TAG = "FrmGraphs";
        private IList<double> speeds;
        private IList<double> dwells;
        private IList<BusLoad> busLoads;

        private BsensorCommand cmdAddRanges = null;

        public enum BusChartType
        {
            Range = 21,
            SplineRange = 22,
            RangeColumn = 24
        };

        #region EventGraphCursorPositionChanged
        public delegate void EventGraphCursorPositionChanged(int dataIndex, string dataSetName);
        EventGraphCursorPositionChanged eventGraphCursorPositionChanged;
        public void AddOnEventGraphCursorPositionChanged(EventGraphCursorPositionChanged e)
        {
            eventGraphCursorPositionChanged += e;
        }
        public void RemoveOnEventGraphCursorPositionChanged(EventGraphCursorPositionChanged e)
        {
            eventGraphCursorPositionChanged -= e;
        }
        public void OnEventGraphCursorPositionChanged(int dataIndex, string dataSetName)
        {
            if (null != eventGraphCursorPositionChanged)
                eventGraphCursorPositionChanged(dataIndex, dataSetName);
        }
        #endregion EventGraphCursorPositionChanged

        public FrmGraphs()
        {
            InitializeComponent();
        }

        private void FrmGraphs_Load(object sender, EventArgs e)
        {
            //chart1.Series[0].Points.Clear();
            //chart2.Series[0].Points.Clear();
            //chart3.Series[0].Points.Clear();
            //chart3.Series[1].Points.Clear();
            //chart3.Series[2].Points.Clear();
            chartECG.Series[0].Points.Clear();
            chartGSR.Series[0].Points.Clear();

            cmdAddRanges = new CmdAddRanges(myApp);

            myApp.AddOnEventColorGraphData(EventColorGraphData);
            myApp.AddOnEventShimmerSensorData(Main_EventNewShimmerSensorReadings);
            //ClearBusGraphs1();
            //ClearBusGraphs23();
        }

        public void EventBusData1()
        {
            ClearBusGraphs1();

            udChart1.Minimum = 0;
            udChart1.Maximum = 0;

            udChart2.Minimum = 0;
            udChart2.Maximum = 0;

            udChart3.Minimum = 0;
            udChart3.Maximum = 0;
        }

        public void EventBusData2(string name1, IList<double> speeds, string name2, IList<double> dwells)
        {
            ClearBusGraphs1();

            this.speeds = speeds;
            this.dwells = dwells;

            int count = Math.Min(speeds.Count, dwells.Count);

            chart1.Series[0].IsVisibleInLegend = true;
            chart2.Series[1].IsVisibleInLegend = true;

            try
            {
                for (int i = 0; i < count; ++i)
                {
                    chart1.Series[0].Points.Add(speeds[i]);
                    chart2.Series[0].Points.Add(dwells[i]);
                }

                if (count > 0)
                {
                    udChart1.Enabled = true;
                    udChart2.Enabled = true;

                    udChart1.Minimum = 0;
                    udChart1.Maximum = count - 1;


                    udChart2.Minimum = 0;
                    udChart2.Maximum = count - 1;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        public void EventBusData3(IList<double> speeds)
        {
            ClearBusGraphs1();

            this.speeds = speeds;

            int count = speeds.Count;

            chart1.Series[0].IsVisibleInLegend = true;

            try
            {
                for (int i = 0; i < count; ++i)
                {
                    chart1.Series[0].Points.Add(speeds[i]);
                    //chart1.Series[0].Points.AddXY(i, data1[i]);
                }

                if (count > 0)
                {
                    udChart1.Enabled = true;
                    udChart1.Minimum = 0;
                    udChart1.Maximum = count - 1;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        public void EventBusData3(SpeedData speedData, GraphSettings graphSpeedSettings)
        {
            ClearBusGraphs1();

            this.speeds = speedData.Speeds;

            int count = speeds.Count;

            chart1.Series[0].IsVisibleInLegend = true;

            try
            {
                for (int i = 0; i < count; ++i)
                {
                    //chart1.Series[0].Points.Add(speeds[i]);
                    chart1.Series[0].Points.AddXY(speedData.Distances[i], speeds[i]);
                }

                if (count > 0)
                {
                    udChart1.Enabled = true;
                    udChart1.Minimum = 0;
                    udChart1.Maximum = count - 1;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        public void EventBusData4(StopLevelData stopLevelData, 
            GraphSettings graphDwellSettings, 
            LoadGraphSettings graphLoadSetting)
        {
            ClearBusGraphs23();

            // Special case to just clear graphs and return
            if (null == stopLevelData)
                return;

            this.dwells = stopLevelData.Dwells;
            this.busLoads = stopLevelData.BusLoads;

            int count = Math.Min(dwells.Count, this.busLoads.Count);

            chart2.Series[0].Name = DataSetName.Dwell;
            chart3.Series[0].Name = DataSetName.Ons;
            chart3.Series[1].Name = DataSetName.Offs;
            chart3.Series[2].Name = DataSetName.Load;

            chart2.Series[0].IsVisibleInLegend = true;
            chart3.Series[0].IsVisibleInLegend = true;
            chart3.Series[1].IsVisibleInLegend = true;
            chart3.Series[2].IsVisibleInLegend = true;

            switch (graphLoadSetting.LoadGraphType)
            {
                case LoadGraphSettings.ELoadGraphType.Range:
                    chart3.Series[0].ChartType = SeriesChartType.Range; // Range, RangeColumn, SplineRange
                    chart3.Series[1].ChartType = SeriesChartType.Range; //
                    break;

                case LoadGraphSettings.ELoadGraphType.RangeColumn:
                    chart3.Series[0].ChartType = SeriesChartType.RangeColumn; // Range, RangeColumn, SplineRange
                    chart3.Series[1].ChartType = SeriesChartType.RangeColumn; //
                    break;

                case LoadGraphSettings.ELoadGraphType.RangeSpline:
                    chart3.Series[0].ChartType = SeriesChartType.SplineRange; // Range, RangeColumn, SplineRange
                    chart3.Series[1].ChartType = SeriesChartType.SplineRange; //
                    break;

                default:
                    return;
            }

            chart3.Series[2].ChartType = SeriesChartType.Line; //

            try
            {
                for (int i = 0; i < count; ++i)
                {
                    //chart2.Series[0].Points.Add(dwells[i]); // Dwell
                    chart2.Series[0].Points.AddXY(stopLevelData.Distances[i], dwells[i]);

                    //chart3.Series[0].Points.Add(this.busLoads[i].Load, this.busLoads[i].Load + this.busLoads[i].Ons); // Ons
                    //chart3.Series[1].Points.Add(this.busLoads[i].Load - this.busLoads[i].Offs, this.busLoads[i].Load); // Offs
                    //chart3.Series[2].Points.Add(this.busLoads[i].Load); // Load

                    chart3.Series[0].Points.AddXY(stopLevelData.Distances[i], this.busLoads[i].Load, this.busLoads[i].Load + this.busLoads[i].Ons); // Ons
                    chart3.Series[1].Points.AddXY(stopLevelData.Distances[i], this.busLoads[i].Load - this.busLoads[i].Offs, this.busLoads[i].Load); // Offs
                    chart3.Series[2].Points.AddXY(stopLevelData.Distances[i], this.busLoads[i].Load); // Load

                    chart3.Series[0].Color = Color.Red;
                    chart3.Series[1].Color = Color.Blue;
                    chart3.Series[2].Color = Color.Black;
                }

                if (count > 0)
                {
                    udChart2.Enabled = true;
                    udChart3.Enabled = true;

                    udChart2.Minimum = 0;
                    udChart2.Maximum = count - 1;

                    udChart3.Minimum = 0;
                    udChart3.Maximum = count - 1;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        public void EventBusData5(SpeedData speedData, StopLevelData stopLevelData,
            GraphSettings graphSpeedSettings, GraphSettings graphDwellSettings, LoadGraphSettings graphLoadSettings)
        {
            double maxDistance = 0;

            DateTime maxDateTime = DateTime.MinValue;
            DateTime minDateTime = DateTime.MinValue;

            if (null == stopLevelData)
            {
                maxDistance = Math.Ceiling(Util.MaxValue(speedData.Distances));
                minDateTime = Util.MinValue(speedData.DateTimes);
                maxDateTime = Util.MaxValue(speedData.DateTimes);
            }
            else
            {
                maxDistance = Math.Ceiling(Util.MaxValue(speedData.Distances, stopLevelData.Distances));
                minDateTime = Util.MinValue(speedData.DateTimes, stopLevelData.DateTimes);
                maxDateTime = Util.MaxValue(speedData.DateTimes, stopLevelData.DateTimes);
            }

            // Speed Data Section ------------------------------------------------------------

            ClearBusGraphs1();

            this.speeds = speedData.Speeds;

            int count = speeds.Count;

            chart1.Series[0].Name = DataSetName.Speed;
            chart1.Series[0].IsVisibleInLegend = true;

            try
            {
                switch (graphSpeedSettings.XAxisType)
                {
                    case GraphSettings.EXAxisType.Distance:

                        chart1.Series[0].IsXValueIndexed = false;
                        chart1.Series[0].XValueType = ChartValueType.Double;
                        chart1.ChartAreas[0].AxisX.LabelStyle.Format = "";
                        chart1.ChartAreas[0].AxisX.Interval = 0;
                        chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Number;
                        chart1.ChartAreas[0].AxisX.IntervalOffset = 0;

                        for (int i = 0; i < count; ++i)
                        {
                            chart1.Series[0].Points.AddXY(speedData.Distances[i], speeds[i]);
                        }

                        chart1.ChartAreas[0].AxisX.Minimum = 0;
                        chart1.ChartAreas[0].AxisX.Maximum = maxDistance;
                        break;

                    case GraphSettings.EXAxisType.TimeScale:

                        chart1.Series[0].IsXValueIndexed = false;
                        chart1.Series[0].XValueType = ChartValueType.DateTime;
                        chart1.ChartAreas[0].AxisX.LabelStyle.Format = "hh:mm";
                        chart1.ChartAreas[0].AxisX.Interval = 10;
                        chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Minutes;
                        chart1.ChartAreas[0].AxisX.IntervalOffset = 1;

                        for (int i = 0; i < count; ++i)
                        {
                            chart1.Series[0].Points.AddXY(speedData.DateTimes[i], speeds[i]);
                        }

                        //chart1.Series[0].XValueType = ChartValueType.DateTime;
                        chart1.ChartAreas[0].AxisX.Minimum = minDateTime.ToOADate();
                        chart1.ChartAreas[0].AxisX.Maximum = maxDateTime.ToOADate();
                        break;

                    case GraphSettings.EXAxisType.Index:

                        chart1.Series[0].IsXValueIndexed = false;
                        chart1.Series[0].XValueType = ChartValueType.Double;
                        chart1.ChartAreas[0].AxisX.LabelStyle.Format = "";
                        chart1.ChartAreas[0].AxisX.Interval = 0;
                        chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Number;
                        chart1.ChartAreas[0].AxisX.IntervalOffset = 0;

                        for (int i = 0; i < count; ++i)
                        {
                            chart1.Series[0].Points.Add(speeds[i]);
                        }

                        chart1.ChartAreas[0].AxisX.Minimum = 0;
                        //chart1.ChartAreas[0].AxisX.Maximum = chart1.Series[0].Points.Count - 1;
                        chart1.ChartAreas[0].AxisX.Maximum = chart1.Series[0].Points.Count;
                        break;
                }

                if (count > 0)
                {
                    udChart1.Enabled = true;
                    udChart1.Minimum = 0;
                    udChart1.Maximum = count - 1;
                }

                // Stop Level Data -------------------------------------------------------------------

                ClearBusGraphs23();

                if (null != stopLevelData)
                {

                    this.dwells = stopLevelData.Dwells;
                    this.busLoads = stopLevelData.BusLoads;

                    count = Math.Min(dwells.Count, this.busLoads.Count);

                    chart2.Series[0].Name = DataSetName.Dwell;
                    chart3.Series[0].Name = DataSetName.Ons;
                    chart3.Series[1].Name = DataSetName.Offs;
                    chart3.Series[2].Name = DataSetName.Load;

                    chart2.Series[0].IsVisibleInLegend = true;
                    chart3.Series[0].IsVisibleInLegend = true;
                    chart3.Series[1].IsVisibleInLegend = true;
                    chart3.Series[2].IsVisibleInLegend = true;

                    switch (graphLoadSettings.LoadGraphType)
                    {
                        case LoadGraphSettings.ELoadGraphType.Range:
                            chart3.Series[0].ChartType = SeriesChartType.Range; // Range, RangeColumn, SplineRange
                            chart3.Series[1].ChartType = SeriesChartType.Range; //
                            break;

                        case LoadGraphSettings.ELoadGraphType.RangeColumn:
                            chart3.Series[0].ChartType = SeriesChartType.RangeColumn; // Range, RangeColumn, SplineRange
                            chart3.Series[1].ChartType = SeriesChartType.RangeColumn; //
                            break;

                        case LoadGraphSettings.ELoadGraphType.RangeSpline:
                            chart3.Series[0].ChartType = SeriesChartType.SplineRange; // Range, RangeColumn, SplineRange
                            chart3.Series[1].ChartType = SeriesChartType.SplineRange; //
                            break;

                        default:
                            return;
                    }

                    chart3.Series[2].ChartType = SeriesChartType.Line; //

                    // Dwell Graph
                    switch (graphDwellSettings.XAxisType)
                    {
                        case GraphSettings.EXAxisType.Distance:

                            chart2.Series[0].IsXValueIndexed = false;
                            chart2.Series[0].XValueType = ChartValueType.Double;
                            chart2.ChartAreas[0].AxisX.LabelStyle.Format = "";
                            chart2.ChartAreas[0].AxisX.Interval = 0;
                            chart2.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Number;
                            chart2.ChartAreas[0].AxisX.IntervalOffset = 0;

                            for (int i = 0; i < count; ++i)
                            {
                                chart2.Series[0].Points.AddXY(stopLevelData.Distances[i], dwells[i]);
                            }
                            chart2.ChartAreas[0].AxisX.Minimum = 0.0;
                            chart2.ChartAreas[0].AxisX.Maximum = maxDistance;
                            break;

                        case GraphSettings.EXAxisType.TimeScale:

                            chart2.Series[0].IsXValueIndexed = false;
                            chart2.Series[0].XValueType = ChartValueType.DateTime;
                            chart2.ChartAreas[0].AxisX.LabelStyle.Format = "hh:mm";
                            chart2.ChartAreas[0].AxisX.Interval = 10;
                            chart2.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Minutes;
                            chart2.ChartAreas[0].AxisX.IntervalOffset = 1;

                            for (int i = 0; i < count; ++i)
                            {
                                chart2.Series[0].Points.AddXY(stopLevelData.DateTimes[i], dwells[i]);
                            }

                            chart2.Series[0].XValueType = ChartValueType.DateTime;
                            chart2.ChartAreas[0].AxisX.Minimum = minDateTime.ToOADate();
                            chart2.ChartAreas[0].AxisX.Maximum = maxDateTime.ToOADate();
                            break;

                        case GraphSettings.EXAxisType.Index:

                            chart2.Series[0].IsXValueIndexed = false;
                            chart2.Series[0].XValueType = ChartValueType.Double;
                            chart2.ChartAreas[0].AxisX.LabelStyle.Format = "";
                            chart2.ChartAreas[0].AxisX.Interval = 0;
                            chart2.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Number;
                            chart2.ChartAreas[0].AxisX.IntervalOffset = 0;

                            for (int i = 0; i < count; ++i)
                            {
                                chart2.Series[0].Points.Add(dwells[i]); // Dwell
                            }

                            chart2.ChartAreas[0].AxisX.Minimum = 0.0;
                            //chart2.ChartAreas[0].AxisX.Maximum = chart2.Series[0].Points.Count - 1;
                            chart2.ChartAreas[0].AxisX.Maximum = chart2.Series[0].Points.Count;
                            break;
                    }

                    // Load/Ons/Offs Graph
                    switch (graphLoadSettings.XAxisType)
                    {
                        case GraphSettings.EXAxisType.Distance:

                            chart3.Series[0].IsXValueIndexed = false;
                            chart3.Series[1].IsXValueIndexed = false;
                            chart3.Series[2].IsXValueIndexed = false;

                            chart3.Series[0].XValueType = ChartValueType.Double;
                            chart3.ChartAreas[0].AxisX.LabelStyle.Format = "";
                            chart3.ChartAreas[0].AxisX.Interval = 0;
                            chart3.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Number;
                            chart3.ChartAreas[0].AxisX.IntervalOffset = 0;

                            for (int i = 0; i < count; ++i)
                            {
                                chart3.Series[0].Points.AddXY(stopLevelData.Distances[i], this.busLoads[i].Load, this.busLoads[i].Load + this.busLoads[i].Ons); // Ons
                                chart3.Series[1].Points.AddXY(stopLevelData.Distances[i], this.busLoads[i].Load - this.busLoads[i].Offs, this.busLoads[i].Load); // Offs
                                chart3.Series[2].Points.AddXY(stopLevelData.Distances[i], this.busLoads[i].Load); // Load
                                chart3.Series[0].Color = Color.Red;
                                chart3.Series[1].Color = Color.Blue;
                                chart3.Series[2].Color = Color.Black;
                            }
                            chart3.ChartAreas[0].AxisX.Minimum = 0;
                            chart3.ChartAreas[0].AxisX.Maximum = maxDistance;
                            break;

                        case GraphSettings.EXAxisType.TimeScale:

                            chart3.Series[0].IsXValueIndexed = false;
                            chart3.Series[1].IsXValueIndexed = false;
                            chart3.Series[2].IsXValueIndexed = false;

                            chart3.Series[0].XValueType = ChartValueType.DateTime;
                            chart3.ChartAreas[0].AxisX.LabelStyle.Format = "hh:mm";
                            chart3.ChartAreas[0].AxisX.Interval = 10;
                            chart3.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Minutes;
                            chart3.ChartAreas[0].AxisX.IntervalOffset = 1;

                            for (int i = 0; i < count; ++i)
                            {
                                chart3.Series[0].Points.AddXY(stopLevelData.DateTimes[i], this.busLoads[i].Load, this.busLoads[i].Load + this.busLoads[i].Ons); // Ons
                                chart3.Series[1].Points.AddXY(stopLevelData.DateTimes[i], this.busLoads[i].Load - this.busLoads[i].Offs, this.busLoads[i].Load); // Offs
                                chart3.Series[2].Points.AddXY(stopLevelData.DateTimes[i], this.busLoads[i].Load); // Load
                                chart3.Series[0].Color = Color.Red;
                                chart3.Series[1].Color = Color.Blue;
                                chart3.Series[2].Color = Color.Black;
                            }

                            chart3.Series[0].XValueType = ChartValueType.DateTime;
                            chart3.ChartAreas[0].AxisX.Minimum = minDateTime.ToOADate();
                            chart3.ChartAreas[0].AxisX.Maximum = maxDateTime.ToOADate();
                            break;

                        case GraphSettings.EXAxisType.Index:

                            chart3.Series[0].IsXValueIndexed = false;
                            chart3.Series[1].IsXValueIndexed = false;
                            chart3.Series[2].IsXValueIndexed = false;

                            chart3.Series[0].XValueType = ChartValueType.Double;
                            chart3.ChartAreas[0].AxisX.LabelStyle.Format = "";
                            chart3.ChartAreas[0].AxisX.Interval = 0;
                            chart3.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Number;
                            chart3.ChartAreas[0].AxisX.IntervalOffset = 0;

                            for (int i = 0; i < count; ++i)
                            {
                                chart3.Series[0].Points.Add(this.busLoads[i].Load, this.busLoads[i].Load + this.busLoads[i].Ons); // Ons
                                chart3.Series[1].Points.Add(this.busLoads[i].Load - this.busLoads[i].Offs, this.busLoads[i].Load); // Offs
                                chart3.Series[2].Points.Add(this.busLoads[i].Load); // Load
                                chart3.Series[0].Color = Color.Red;
                                chart3.Series[1].Color = Color.Blue;
                                chart3.Series[2].Color = Color.Black;
                            }

                            chart3.ChartAreas[0].AxisX.Minimum = 0.0;
                            //chart3.ChartAreas[0].AxisX.Maximum = chart3.Series[0].Points.Count - 1;
                            chart3.ChartAreas[0].AxisX.Maximum = chart3.Series[0].Points.Count;
                            break;
                    }

                    chart1.ChartAreas[0].AxisX.Title = GetXAxisTitle(graphSpeedSettings);
                    chart2.ChartAreas[0].AxisX.Title = GetXAxisTitle(graphDwellSettings);
                    chart3.ChartAreas[0].AxisX.Title = GetXAxisTitle(graphLoadSettings);

                    if (count > 0)
                    {
                        udChart2.Enabled = true;
                        udChart3.Enabled = true;

                        udChart2.Minimum = 0;
                        udChart2.Maximum = count - 1;

                        udChart3.Minimum = 0;
                        udChart3.Maximum = count - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private string GetXAxisTitle(GraphSettings graphSettings)
        {
            switch (graphSettings.XAxisType)
            {
                case GraphSettings.EXAxisType.Distance:
                    return "Distance (mi)";

                case GraphSettings.EXAxisType.TimeScale:
                    return "Time (hh:mm)";

                case GraphSettings.EXAxisType.Index:
                    return "Index";

                default:
                    return "unavailable";
            }
        }

        public void EventNewECG(IList<ECGReading> readings)
        {
            int count = 0;

            //chartECG.Series.Clear();

            try
            {
                foreach (ECGReading r in readings)
                {
                    ++count;
                    if (count > 1000)
                        return;
                    else if (count > 50)
                    {
                        chartECG.Series[0].Points.Add(r.exg1_ch1);
                        chartECG.Series[1].Points.Add(r.exg1_ch2);
                        chartECG.Series[2].Points.Add(r.exg2_ch1);
                        chartECG.Series[3].Points.Add(r.exg2_ch2);
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void ClearBusGraphs1()
        {
            speeds = null;

            // Clear data points for chart 1
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Name = "";

            udChart1.Enabled = false;

            chart1.Series[0].IsVisibleInLegend = false;
        }

        private void ClearBusGraphs23()
        {
            dwells = null;
            busLoads = null;

            // Clear data points for chart 1
            chart2.Series[0].Points.Clear();
            chart2.Series[0].Name = "";

            // Clear data points for chart 2
            chart3.Series[0].Points.Clear();
            chart3.Series[0].Name = "";
            chart3.Series[1].Points.Clear();
            chart3.Series[1].Name = " ";
            chart3.Series[2].Points.Clear();
            chart3.Series[2].Name = "  ";

            udChart2.Enabled = false;
            udChart3.Enabled = false;

            chart2.Series[0].IsVisibleInLegend = false;
            chart3.Series[0].IsVisibleInLegend = false;
            chart3.Series[1].IsVisibleInLegend = false;
            chart3.Series[2].IsVisibleInLegend = false;
        }

        public void EventNewProject()
        {
            ClearBusGraphs1();
            ClearBusGraphs23();
        }

        public void EventColorGraphData(string dataSetName, GraphColorRange[] graphColorRanges)
        {
            foreach (GraphColorRange g in graphColorRanges)
            {
                EventColorGraphData(dataSetName, g.Min, g.Max, g.Color);
            }
        }

        public void EventColorGraphData(string dataSetName, double lowValue, double highValue, Color color)
        {
            DataPointCollection dataPoints;
            double value;
            bool inRange = false;
            int first = -1;
            int last = -1;
            int i;

            if (dataSetName == DataSetName.Speed)
                dataPoints = chart1.Series[0].Points;
            else if (dataSetName == DataSetName.Dwell)
                dataPoints = chart2.Series[0].Points;
            else
                return;

            for (i = 0; i < dataPoints.Count; ++i)
            {
                value = dataPoints[i].YValues[0];
                if ((value > lowValue) && (value <= highValue))
                {
                    if (!inRange)
                    {
                        first = i;
                        inRange = true;
                    }
                }
                else
                {
                    if (inRange)
                    {
                        last = i - 1;
                        inRange = false;
                        EventColorGraphData(dataPoints, first, last, color);
                    }
                }
            }

            if (inRange)
            {
                last = i - 1;
                EventColorGraphData(dataPoints, first, last, color);
            }
        }

        public void EventColorGraphData(DataPointCollection dataPoints, int first, int last, Color color)
        {

            for (int i = first; i <= last; ++i)
            {
                dataPoints[i].Color = color;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public void LogException(Exception ex)
        {
            //Util.LogException(MODULE_TAG, ex);
            //ssStatus.Items[0].Text = ex.Message;
        }

        private void udChart1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                bool isIndex = myApp.GraphSpeedSettings.XAxisType == GraphSettings.EXAxisType.Index;
                int dataIndex = (int)udChart1.Value;

                if (isIndex)
                {
                    chart1.ChartAreas[0].CursorX.Position = dataIndex;
                }
                else
                {
                    chart1.ChartAreas[0].CursorX.Position = chart1.Series[0].Points[dataIndex].XValue;
                }

                WriteChartLegendInfo(chart1, DataSetName.Speed, isIndex, dataIndex);

                OnEventGraphCursorPositionChanged(dataIndex, DataSetName.Speed);


                /*int chartIndex = (int)udChart1.Value;
                int dataIndex = chartIndex - 1;

                //chart1.ChartAreas[0].CursorX.Position = chartIndex;
                chart1.ChartAreas[0].CursorX.Position = chart1.Series[0].Points[dataIndex].XValue;

                if (null != dwells)
                    UpdateCursorData(chart1, FileTripBusData.DATA_SET_NAME_BUS_SPEED, dataIndex, null);*/
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void udChart2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                bool isIndex = myApp.GraphDwellSettings.XAxisType == GraphSettings.EXAxisType.Index;
                int dataIndex = (int)udChart2.Value;

                if (isIndex)
                {
                    chart2.ChartAreas[0].CursorX.Position = dataIndex;
                }
                else
                {
                    chart2.ChartAreas[0].CursorX.Position = chart2.Series[0].Points[dataIndex].XValue;
                }

                WriteChartLegendInfo(chart2, DataSetName.Dwell, isIndex, dataIndex);
                OnEventGraphCursorPositionChanged(dataIndex, DataSetName.StopLevel);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void udChart3_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                bool isIndex = myApp.GraphLoadSettings.XAxisType == GraphSettings.EXAxisType.Index;
                int dataIndex = (int)udChart3.Value;

                if (isIndex)
                {
                    chart3.ChartAreas[0].CursorX.Position = dataIndex;
                }
                else
                {
                    chart3.ChartAreas[0].CursorX.Position = chart3.Series[0].Points[dataIndex].XValue;
                }

                WriteChartLegendInfo(chart3, DataSetName.Load, isIndex, dataIndex);
                OnEventGraphCursorPositionChanged(dataIndex, DataSetName.StopLevel);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private string getDataSetName(object tag)
        {
            string dataSetName = null;
            string sTag;
            int chartIndex;

            if (null != (sTag = tag as string))
            {
                if (int.TryParse(sTag, out chartIndex))
                {
                    switch (chartIndex)
                    {
                        case 0:
                            dataSetName = DataSetName.Speed;
                            break;

                        case 1:
                            dataSetName = DataSetName.Dwell;
                            break;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(dataSetName))
                return null;

            return dataSetName;
        }

        /// <summary>
        /// Detect the context menu opening, and set the tag to the data set name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chartContextMenu_Opening(object sender, CancelEventArgs e)
        {
            string dataSetName;

            ContextMenuStrip contextMenu = sender as ContextMenuStrip;
            if ((null == contextMenu) ||
                (null == (dataSetName = getDataSetName(contextMenu.Items[0].Tag = contextMenu.SourceControl.Tag))))
            {
                e.Cancel = true;
            }
        }

        private void mnuColorMultipleChartRanges_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mnu;

            try
            {
                if (null != (mnu = sender as ToolStripMenuItem))
                {
                    string dataSetName;
                    if (null != (dataSetName = getDataSetName(mnu.Tag)))
                    {
                        FrmEditColorRanges dlg = new FrmEditColorRanges(dataSetName, myApp.GetGraphColorRanges(dataSetName));

                        if (DialogResult.OK == dlg.ShowDialog())
                        {
                            cmdAddRanges.Execute(dataSetName, dlg.GraphColorRanges);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        // MouseMove Handlers
        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (myApp.GraphSpeedSettings.CursorMovement == GraphSettings.ECursorMovement.OnMouseClick)
                {
                    chart1_PositionCursor(e);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void chart2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (myApp.GraphDwellSettings.CursorMovement == GraphSettings.ECursorMovement.OnMouseClick)
                {
                    chart2_PositionCursor(e);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void chart3_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (myApp.GraphLoadSettings.CursorMovement == GraphSettings.ECursorMovement.OnMouseClick)
                {
                    chart3_PositionCursor(e);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        // MouseClick Handlers
        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (myApp.GraphSpeedSettings.CursorMovement == GraphSettings.ECursorMovement.OnMouseOver)
                {
                    chart1_PositionCursor(e);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void chart2_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (myApp.GraphDwellSettings.CursorMovement == GraphSettings.ECursorMovement.OnMouseOver)
                {
                    chart2_PositionCursor(e);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void chart3_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (myApp.GraphLoadSettings.CursorMovement == GraphSettings.ECursorMovement.OnMouseOver)
                {
                    chart3_PositionCursor(e);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        // Cursor position routines
        private void chart1_PositionCursor(MouseEventArgs e)
        {
            bool isIndex = myApp.GraphSpeedSettings.XAxisType == GraphSettings.EXAxisType.Index;
            double xCoordinate;
            int dataIndex;

            bool cursorPositioned = PositionCursor(chart1, e, DataSetName.Speed, udChart1, isIndex, out xCoordinate, out dataIndex);

            if (cursorPositioned)
            {
                //OnEventGraphCursorPositionChanged(dataIndex, FileTripBusData.DATA_SET_NAME_BUS_SPEED);
            }

            // Note: if the x axis is the data index, then don't sync with the other 2 graphs
            if (myApp.GraphSpeedSettings.SyncCursors && cursorPositioned && !isIndex)
            {
                if (myApp.GraphSpeedSettings.XAxisType == myApp.GraphDwellSettings.XAxisType)
                {
                    // Position dwell graph's cursor
                    PositionCursor(chart2, xCoordinate, DataSetName.Dwell, udChart2, isIndex, out dataIndex);
                }

                if (myApp.GraphSpeedSettings.XAxisType == myApp.GraphLoadSettings.XAxisType)
                {
                    // Position Load/Ons/Offs graph's cursor
                    PositionCursor(chart3, xCoordinate, DataSetName.Load, udChart3, isIndex, out dataIndex);
                }
            }
        }

        private void chart2_PositionCursor(MouseEventArgs e)
        {
            bool isIndex = myApp.GraphDwellSettings.XAxisType == GraphSettings.EXAxisType.Index;
            double xCoordinate;
            int dataIndex;
            bool cursorPositioned = PositionCursor(chart2, e, DataSetName.Dwell, udChart2, isIndex, out xCoordinate, out dataIndex);

            if (cursorPositioned)
            {
                //OnEventGraphCursorPositionChanged(dataIndex, FileTripBusData.DATA_SET_NAME_STOP_LEVEL);
            }

            // Note: if the x axis is the data index, then syncing cursors doesn't make since
            if (cursorPositioned && myApp.GraphDwellSettings.SyncCursors)
            {
                if ((myApp.GraphDwellSettings.XAxisType == myApp.GraphSpeedSettings.XAxisType) && !isIndex)
                {
                    // Position speed graph's cursor
                    PositionCursor(chart1, xCoordinate, DataSetName.Speed, udChart1, isIndex, out dataIndex);
                }

                if (myApp.GraphDwellSettings.XAxisType == myApp.GraphLoadSettings.XAxisType)
                {
                    // Position Load/Ons/Offs graph's cursor
                    PositionCursor(chart3, xCoordinate, DataSetName.Load, udChart3, isIndex, out dataIndex);
                }
            }
        }

        private void chart3_PositionCursor(MouseEventArgs e)
        {
            bool isIndex = myApp.GraphLoadSettings.XAxisType == GraphSettings.EXAxisType.Index;
            double xCoordinate;
            int dataIndex;
            bool cursorPositioned = PositionCursor(chart3, e, DataSetName.Load, udChart3, isIndex, out xCoordinate, out dataIndex);

            if (cursorPositioned)
            {
                //OnEventGraphCursorPositionChanged(dataIndex, FileTripBusData.DATA_SET_NAME_STOP_LEVEL);
            }

            if (cursorPositioned && myApp.GraphLoadSettings.SyncCursors)
            {
                // Note: if the x axis is the data index, then don't sync with the speed graph
                if ((myApp.GraphLoadSettings.XAxisType == myApp.GraphSpeedSettings.XAxisType) && !isIndex)
                {
                    // Position speed graph's cursor
                    PositionCursor(chart1, xCoordinate, DataSetName.Speed, udChart1, isIndex, out dataIndex);
                }

                if (myApp.GraphLoadSettings.XAxisType == myApp.GraphDwellSettings.XAxisType)
                {
                    // Position dwell graph's cursor
                    PositionCursor(chart2, xCoordinate, DataSetName.Dwell, udChart2, isIndex, out dataIndex);
                }
            }
        }

        private bool InChartGraphArea(Chart chart, MouseEventArgs e)
        {
            ChartArea chartArea = chart.ChartAreas[0];
            return (chartArea.AxisY.PixelPositionToValue(e.Y) >= chartArea.AxisY.Minimum) &&
                (chartArea.AxisY.PixelPositionToValue(e.Y) <= chartArea.AxisY.Maximum) &&
                (chartArea.AxisX.PixelPositionToValue(e.X) >= chartArea.AxisX.Minimum) &&
                (chartArea.AxisX.PixelPositionToValue(e.X) <= chartArea.AxisX.Maximum);
        }

        private bool PositionCursor(Chart chart, MouseEventArgs e, string dataSetName, NumericUpDown udChart, bool isIndex, 
            out double xCoordinate, out int dataIndex)
        {
            xCoordinate = double.NaN;
            dataIndex = -1;

            // Ignore if this chart has no data points
            if (chart.Series[0].Points.Count == 0)
                return false;

            // Ignore if mouse position is outside chart region
            if (!InChartGraphArea(chart, e))
                return false;

            chart.ChartAreas[0].RecalculateAxesScale();

            // Get x coordinate corresponding to this pixel poistion
            xCoordinate = chart.ChartAreas[0].AxisX.PixelPositionToValue(e.X);

            PositionCursor(chart, xCoordinate, dataSetName, udChart, isIndex, out dataIndex);

            return true;

            #region sample code
            /*if (e.X < 0 || e.Y < 0 || e.Location == prevPos)
                return;

            prevPos = e.Location;
            if (this.graphShowingData == false)
                return;

            Point p = new Point(e.X, e.Y);
            double searchVal = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);

            int dataIndex = 0;
            double dataValue;
            foreach (DataPoint dp in chart1.Series[0].Points)
            {
                if (dp.XValue >= searchVal)
                {
                    chart1.ChartAreas[0].CursorX.SetCursorPosition(dp.XValue);

                    chart1.Series[0].Name = "[" + dataIndex.ToString() + "] = " + chart1.Series[0].Points[dataIndex].YValues[0].ToString();

                    dataValue = chart1.Series[0].Points[dataIndex].YValues[0];



                    foreach (double yD in dp.YValues)
                    {
                        //val_series1.Text = Math.Round(yD, 4).ToString();
                    }
                    break;
                }
                ++dataIndex;
            }*/
            #endregion sample code
        }

        public void PositionCursor(Chart chart, double xCoordinate, string dataSetName, NumericUpDown udChart, bool isIndex, out int dataIndex)
        {
            if (isIndex)
            {
                // Get the closest data point's index corresponding to this x coordinate value
                dataIndex = ClosestIndex(chart, xCoordinate);
            }
            else
            {
                // Get the closest data point's index corresponding to this x coordinate value
                dataIndex = IndexOfClosestXValue(chart, xCoordinate);
            }

            // Update the UpDown control
            udChart.Value = dataIndex;

            if (isIndex)
            {
                chart.ChartAreas[0].CursorX.SetCursorPosition(dataIndex);
            }
            else
            {
                chart.ChartAreas[0].CursorX.SetCursorPosition(chart.Series[0].Points[dataIndex].XValue);
            }

            //WriteChartLegendInfo(chart, dataSetName, isIndex, dataIndex);

        }

        private void WriteChartLegendInfo(Chart chart, string dataSetName, bool isIndex, int dataIndex)
        {
            double ons;
            double offs;
            double load;

            if (isIndex)
            {
                if (dataSetName.Equals(DataSetName.Load))
                {
                    ons = chart.Series[0].Points[dataIndex].YValues[1] - chart.Series[0].Points[dataIndex].YValues[0];
                    offs = chart.Series[1].Points[dataIndex].YValues[1] - chart.Series[1].Points[dataIndex].YValues[0];
                    load = chart.Series[0].Points[dataIndex].YValues[0];

                    chart.Series[0].Name = "Ons[" + dataIndex.ToString() + "] = " + ons.ToString();
                    chart.Series[1].Name = "Offs[" + dataIndex.ToString() + "] = " + offs.ToString();
                    chart.Series[2].Name = "Load[" + dataIndex.ToString() + "] = " + load.ToString();
                }
                else
                {
                    chart.Series[0].Name = dataSetName + "[" + dataIndex.ToString() + "] = " + chart.Series[0].Points[dataIndex].YValues[0].ToString();
                }
            }
            else
            {
                if (dataSetName.Equals(DataSetName.Load))
                {
                    ons = chart.Series[0].Points[dataIndex].YValues[1] - chart.Series[0].Points[dataIndex].YValues[0];
                    offs = chart.Series[1].Points[dataIndex].YValues[1] - chart.Series[1].Points[dataIndex].YValues[0];
                    load = chart.Series[0].Points[dataIndex].YValues[0];

                    chart.Series[0].Name = "Ons[" + dataIndex.ToString() + "] = " + ons.ToString();
                    chart.Series[1].Name = "Offs[" + dataIndex.ToString() + "] = " + offs.ToString();
                    chart.Series[2].Name = "Load[" + dataIndex.ToString() + "] = " + load.ToString() +
                        "       (X=" + chart.Series[0].Points[dataIndex].XValue.ToString() + ")";
                }
                else
                {
                    double p0 = 0.0;
                    double p1 = 0.0;
                    double p2 = 0.0;
                    double p3 = 0.0;
                    double p4 = 0.0;

                    chart.Series[0].Name = dataSetName + "[" + dataIndex.ToString() + "] = (" +
                        chart.Series[0].Points[dataIndex].XValue.ToString() + "," +
                        chart.Series[0].Points[dataIndex].YValues[0].ToString() + ")";

                    p0 = chart.Series[0].Points[0].YValues[0];
                    p1 = chart.Series[0].Points[1].YValues[0];
                    p2 = chart.Series[0].Points[2].YValues[0];
                    p3 = chart.Series[0].Points[3].YValues[0];
                    p4 = chart.Series[0].Points[4].YValues[0];
                }
            }
        }

        private int IndexOfClosestXValue(Chart chart, double xvalue)
        {
            DataPointCollection dps = chart.Series[0].Points;

            double diff;
            double smallestDiff = Math.Abs(dps[0].XValue - xvalue);
            int closestIndex = 0;

            for (int i = 1; i < dps.Count; ++i)
            {
                if ((diff = Math.Abs(dps[i].XValue - xvalue)) < smallestDiff)
                {
                    smallestDiff = diff;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }

        private int ClosestIndex(Chart chart, double xvalue)
        {
            DataPointCollection dps = chart.Series[0].Points;

            double diff;
            double smallestDiff = Math.Abs(0 - xvalue);
            int closestIndex = 0;
            double index;

            for (int i = 1; i < dps.Count; ++i)
            {
                index = i;
                if ((diff = Math.Abs(index - xvalue)) < smallestDiff)
                {
                    smallestDiff = diff;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }

        // ------------------------------------------------------------------------
        // Bike section
        // ------------------------------------------------------------------------

        private void Main_EventNewShimmerSensorReadings(IList<ShimmerSensorReading> readings)
        {
            int count = 0;
            System.Windows.Forms.DataVisualization.Charting.Chart chart;
            //chartECG.Series.Clear();

            try
            {
                chart = NewShimmerSensorChart();

                foreach (ShimmerSensorReading r in readings)
                {
                    ++count;
                    if (count > 1000)
                        return;
                    else if (count > 50)
                    {
                        if (null != r.avg_0) chart.Series[0].Points.Add((double)r.avg_0);
                        if (null != r.avg_1) chart.Series[1].Points.Add((double)r.avg_1);
                        if (null != r.avg_2) chart.Series[2].Points.Add((double)r.avg_2);
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private System.Windows.Forms.DataVisualization.Charting.Chart NewShimmerSensorChart()
        {
            /* System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Chart chartGSR = new System.Windows.Forms.DataVisualization.Charting.Chart();

            ((System.ComponentModel.ISupportInitialize)(chartGSR)).BeginInit();

            System.Windows.Forms.TabPage tabGSR;
            tabGSR = new System.Windows.Forms.TabPage();

            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMapGraph)).BeginInit();
            this.scMapGraph.Panel2.SuspendLayout();
            this.scMapGraph.SuspendLayout();
            this.tabControl1.SuspendLayout();



            tabGSR.SuspendLayout();
            //((System.ComponentModel.ISupportInitialize)(chartGSR)).BeginInit();
            this.SuspendLayout();


            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(tabGSR);
            // 
            // tabGSR
            // 
            tabGSR.Controls.Add(chartGSR);
            tabGSR.Location = new System.Drawing.Point(4, 22);
            tabGSR.Name = "tabGSR";
            tabGSR.Padding = new System.Windows.Forms.Padding(3);
            tabGSR.Size = new System.Drawing.Size(741, 384);
            tabGSR.TabIndex = 0;
            tabGSR.Text = "GSR";
            tabGSR.UseVisualStyleBackColor = true;
            // 
            // chartGSR
            // 
            chartArea1.Name = "ChartArea1";
            chartGSR.ChartAreas.Add(chartArea1);
            chartGSR.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            chartGSR.Legends.Add(legend1);
            chartGSR.Location = new System.Drawing.Point(3, 3);
            chartGSR.Name = "chartGSR";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Series3";
            chartGSR.Series.Add(series1);
            chartGSR.Series.Add(series2);
            chartGSR.Series.Add(series3);
            chartGSR.Size = new System.Drawing.Size(735, 378);
            //chartGSR.TabIndex = 4;
            chartGSR.Text = "chart1";
            // 
            // FrmMain
            // 
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.scMapGraph.Panel1.ResumeLayout(false);
            this.scMapGraph.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMapGraph)).EndInit();
            this.scMapGraph.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabGSR.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(chartGSR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
            return chartGSR;*/
            return null;
        }

        private void chart1_AxisViewChanged(object sender, ViewEventArgs e)
        {
            myApp.GraphSpeedSettings.XAxisMin = e.ChartArea.AxisX.ScaleView.ViewMinimum;
            myApp.GraphSpeedSettings.XAxisMax = e.ChartArea.AxisX.ScaleView.ViewMaximum;

            //chart2.ChartAreas[0].AxisX.ScaleView = e.ChartArea.AxisX.ScaleView;
            //chart3.ChartAreas[0].AxisX.ScaleView = e.ChartArea.AxisX.ScaleView;
        }
    }
}
