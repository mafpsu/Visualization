using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace bsensor
{
    public class ProjectFile
    {
        public static readonly string MODULE_TAG = "ProjectFile";
        public enum DataSourceType { File1, File2, MySQL };

        private string fileName;
        private bool touched;
        private DataSource dataSource;
        private Dictionary<string, GraphColorRange[]> dictGraphColorRanges;
        private GoogleMapSettings googleMapSettings = null;
        private GraphSettings graphSpeedSettings = null;
        private GraphSettings graphDwellSettings = null;
        private LoadGraphSettings graphLoadSettings = null;
        private GraphSettings graphEcgSettings = null;
        private GraphSettings graphGsrSettings = null;

        public ProjectFile()
        {

        }

        public void Clear()
        {
            fileName = null;
            dictGraphColorRanges = null;
            dataSource = null;
            touched = false;
        }

        public void Touch()
        {
            touched = true;
        }

        public void Untouch()
        {
            touched = false;
        }

        public bool Touched
        {
            get { return touched; }
        }

        public bool IsNewProject
        {
            get { return fileName == null; }
        }

        public void SaveAs(string fileName, DataSource dataSource, Dictionary<string, GraphColorRange[]> dictGraphColorRanges,
            IList<object> properties)
        {
            if (SaveToFile(fileName, dataSource, dictGraphColorRanges, properties))
            {
                this.fileName = fileName;
            }
        }

        public void Save(DataSource dataSource, Dictionary<string, GraphColorRange[]> dictGraphColorRanges,
            IList<object> properties)
        {
            SaveToFile(this.fileName, dataSource, dictGraphColorRanges, properties);
        }

        public bool SaveToFile(string fileName, DataSource dataSource, Dictionary<string, GraphColorRange[]> dictGraphColorRanges,
            IList<object> properties)
        {
            try {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.NewLineOnAttributes = true;
                XmlWriter writer = XmlWriter.Create(fileName, settings);

                writer.WriteStartElement("project"); // <x:root xmlns:x="123">

                SaveProjectDataSources(writer, dataSource);
                SaveProjectColorRanges(writer, dictGraphColorRanges);
                SaveProperties(writer, properties);

                writer.WriteEndElement(); // project
                writer.Close();
                Untouch();
                this.fileName = fileName;
                return true;
            }
            catch(Exception ex)
            {
                Util.LogException(MODULE_TAG, ex);
            }
            return false;
        }

        public void SaveProperties(XmlWriter writer, IList<object> settingsGroups)
        {
            GoogleMapSettings googleMapSettings;
            LoadGraphSettings loadGraphSettings;
            GraphSettings graphSettings;
            string xmlName;

            writer.WriteStartElement(ProjectSettings.XML_ELEM_SETTINGS);
            foreach(object settings in settingsGroups)
            {
                if (null != (googleMapSettings = settings as GoogleMapSettings))
                {
                    xmlName = ProjectSettings.XML_NAME_MAP_SETTINGS;

                    writer.WriteStartElement(ProjectSettings.XML_ELEM_GOOGLE_MAP_SETTINGS);
                    writer.WriteAttributeString(ProjectSettings.XML_ATTR_NAME, xmlName);
                    WriteProperty(writer, ProjectSettings.XML_ELEM_GOOGLE_MAP_DISPLAY_DATA, googleMapSettings.Data);
                    WriteProperty(writer, ProjectSettings.XML_ELEM_GOOGLE_MAP_FILL_OPACITY, googleMapSettings.FillOpacity);
                    WriteProperty(writer, ProjectSettings.XML_ELEM_GOOGLE_MAP_STROKE_OPACITY, googleMapSettings.StrokeOpacity);
                    WriteProperty(writer, ProjectSettings.XML_ELEM_GOOGLE_MAP_DWELL_SCALE, googleMapSettings.DwellScaleFactor);
                    WriteProperty(writer, ProjectSettings.XML_ELEM_GOOGLE_MAP_LOAD_SCALE, googleMapSettings.LoadScaleFactor);
                    WriteProperty(writer, ProjectSettings.XML_ELEM_GOOGLE_MAP_ROUTE_STROKE_WEIGHT, googleMapSettings.RouteStrokeWeight);
                    WriteProperty(writer, ProjectSettings.XML_ELEM_GOOGLE_MAP_SPEED_STROKE_WEIGHT, googleMapSettings.SpeedStrokeWeight);
                    writer.WriteEndElement(); // property
                }
                else if (null != (loadGraphSettings = settings as LoadGraphSettings))
                {
                    xmlName = ProjectSettings.XML_NAME_BUS_LOAD_ONS_OFFS_SETTINGS;

                    writer.WriteStartElement(ProjectSettings.XML_ELEM_LOAD_GRAPH_SETTINGS);
                    writer.WriteAttributeString(ProjectSettings.XML_ATTR_NAME, xmlName);
                    WriteProperty(writer, ProjectSettings.XML_ELEM_LOAD_GRAPH_TYPE, loadGraphSettings.LoadGraphType);
                    WriteCommonGraphSettings(writer, loadGraphSettings);
                    writer.WriteEndElement(); // property
                }
                else if (null != (graphSettings = settings as GraphSettings))
                {
                    if (graphSettings.Name.Equals(ProjectSettings.NAME_BUS_SPEED_GRAPH_SETTINGS))
                        xmlName = ProjectSettings.XML_NAME_BUS_SPEED_GRAPH_SETTINGS;
                    else if (graphSettings.Name.Equals(ProjectSettings.NAME_BUS_DWELL_GRAPH_SETTINGS))
                        xmlName = ProjectSettings.XML_NAME_BUS_DWELL_GRAPH_SETTINGS;
                    else if (graphSettings.Name.Equals(ProjectSettings.NAME_BIKE_ECG_SETTINGS))
                        xmlName = ProjectSettings.XML_NAME_BIKE_ECG_SETTINGS;
                    else if (graphSettings.Name.Equals(ProjectSettings.NAME_BIKE_GSR_SETTINGS))
                        xmlName = ProjectSettings.XML_NAME_BIKE_GSR_SETTINGS;
                    else
                        throw new Exception("Unknown settings name encountered");

                    writer.WriteStartElement(ProjectSettings.XML_ELEM_GRAPH_SETTINGS);
                    writer.WriteAttributeString(ProjectSettings.XML_ATTR_NAME, xmlName);
                    WriteCommonGraphSettings(writer, graphSettings);
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement(); // properties
        }

        private void WriteCommonGraphSettings(XmlWriter writer, GraphSettings graphSettings)
        {
            WriteProperty(writer, ProjectSettings.XML_ELEM_GRAPH_CURSOR_MOVEMENT, graphSettings.CursorMovement);
            WriteProperty(writer, ProjectSettings.XML_ELEM_GRAPH_X_AXIS_TYPE, graphSettings.XAxisType);
            WriteProperty(writer, ProjectSettings.XML_ELEM_GRAPH_X_AXIS_MIN, graphSettings.XAxisMin);
            WriteProperty(writer, ProjectSettings.XML_ELEM_GRAPH_X_AXIS_MAX, graphSettings.XAxisMax);
            WriteProperty(writer, ProjectSettings.XML_ELEM_GRAPH_X_AXIS_INTERVAL, graphSettings.XAxisInterval);
            WriteProperty(writer, ProjectSettings.XML_ELEM_GRAPH_SYNC_CURSORS, graphSettings.SyncCursors);
            WriteProperty(writer, ProjectSettings.XML_ELEM_GRAPH_SYNC_SETTINGS, graphSettings.SyncSettings);
            WriteProperty(writer, ProjectSettings.XML_ELEM_GRAPH_ENABLE_Y_CURSOR, graphSettings.EnableYCursor);
        }

        private void WriteProperty(XmlWriter writer, string name, object o)
        {
            writer.WriteStartElement(name);
            writer.WriteAttributeString(ProjectSettings.XML_ATTR_VALUE, o.ToString());
            writer.WriteEndElement(); // property
        }

        /// <summary>
        /// Write data-sources elements to project file
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dataSource"></param>
        private void SaveProjectDataSources(XmlWriter writer, DataSource dataSource)
        {
            DataSourceCSVFile dataSourceCSVFile;
            DataSourceDatabase dataSourceDatabase;

            // Write opening tag
            writer.WriteStartElement("data-sources");

            if (null != (dataSourceCSVFile = dataSource as DataSourceCSVFile))
            {
                writer.WriteStartElement("data-source");
                writer.WriteAttributeString("csvfile", dataSourceCSVFile.FileName);
                writer.WriteEndElement(); // data-source
            }
            else if (null != (dataSourceDatabase = dataSource as DataSourceDatabase))
            {
                writer.WriteStartElement("data-source");
                writer.WriteAttributeString("database", "");
                writer.WriteEndElement(); // data-source
            }

            // Write closing tag
            writer.WriteEndElement(); // data-sources
        }

        private void SaveProjectColorRanges(XmlWriter writer, Dictionary<string, GraphColorRange[]> dictGraphColorRanges)
        {
            GraphColorRange[] gcrs;

            // Write color ranges
            writer.WriteStartElement("color-ranges");
            foreach (string dataSetName in dictGraphColorRanges.Keys)
            {
                gcrs = dictGraphColorRanges[dataSetName];
                writer.WriteStartElement("ds-color-ranges");
                writer.WriteAttributeString("data-set-name", dataSetName);

                foreach (GraphColorRange gcr in gcrs)
                {
                    writer.WriteStartElement("color-range");

                    if (gcr.Min == double.MinValue)
                        writer.WriteAttributeString("lbound", "-infinity");
                    else
                        writer.WriteAttributeString("lbound", gcr.Min.ToString());

                    if (gcr.Max == double.MaxValue)
                        writer.WriteAttributeString("ubound", "+infinity");
                    else
                        writer.WriteAttributeString("ubound", gcr.Max.ToString());

                    writer.WriteAttributeString("color", Util.ToRGB(gcr.Color));
                    writer.WriteEndElement(); // color-range
                }
                writer.WriteEndElement(); // ds-color-ranges
            }
            writer.WriteEndElement(); // color-ranges
        }

        public bool OpenFile(string fileName, out string errMsg)
        {
            ProjectFileParser parser = new ProjectFileParser();

            if (parser.Parse(fileName, out errMsg))
            {
                dictGraphColorRanges = parser.DictGraphColorRanges;
                dataSource = parser.DataSource;
                googleMapSettings = parser.GoogleMapSettings;
                graphSpeedSettings = parser.GraphSpeedSettings;
                graphDwellSettings = parser.GraphDwellSettings;
                graphLoadSettings = parser.GraphLoadSettings;
                graphEcgSettings = parser.GraphEcgSettings;
                graphGsrSettings = parser.GraphGsrSettings;

                this.fileName = fileName;
                return true;
            }
            return false;
        }

        public DataSource DataSource
        {
            get { return dataSource; }
        }

        public Dictionary<string, GraphColorRange[]> DictGraphColorRanges
        {
            get { return dictGraphColorRanges; }
        }

        public string FileName
        {
            get { return fileName; }
        }

        public GoogleMapSettings GoogleMapSettings
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
    }

}
