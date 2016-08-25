using System;
using System.Collections.Generic;
using System.Xml;
using System.Drawing;

namespace bsensor
{
    class ProjectFileParser
    {
        private const string ERRMSG_PROJECT_NODE_NOT_FOUND = "Project node not found.";
        private const string ERRMSG_INVALID_ELEMENT = "Invalid element encountered: ";
        private const string ERRMSG_MALFORMED_PROJECT = "Malformed project file: expected end project element";

        private Dictionary<string, GraphColorRange[]> dictGraphColorRanges = new Dictionary<string, GraphColorRange[]>();
        private DataSource dataSource = null;

        private GoogleMapSettings googleMapSettings = null;
        private GraphSettings graphSpeedSettings = null;
        private GraphSettings graphDwellSettings = null;
        private LoadGraphSettings graphLoadSettings = null;
        private GraphSettings graphEcgSettings = null;
        private GraphSettings graphGsrSettings = null;

        public ProjectFileParser()
        {
        }

        public bool Parse(string fileName, out string errMsg)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.CheckCharacters = true;
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;

            using (XmlReader reader = XmlReader.Create(fileName, settings))
            {
                if (!reader.Read() ||
                    !reader.IsStartElement() ||
                    !reader.Name.Equals("project", StringComparison.OrdinalIgnoreCase))
                {
                    errMsg = ERRMSG_PROJECT_NODE_NOT_FOUND;
                    return false;
                }

                while (reader.Read())
                {
                    if (!reader.IsEmptyElement)
                    {
                        if (reader.IsStartElement())
                        {
                            if (reader.Name.Equals(ProjectSettings.XML_ELEM_DATA_SOURCES))
                            {
                                if (!ParseDataSources(reader, out errMsg))
                                    return false;
                            }
                            else if (reader.Name.Equals(ProjectSettings.XML_ELEM_COLOR_RANGES))
                            {
                                if (!ParseColorRanges(reader, out errMsg))
                                    return false;
                            }
                            else if (reader.Name.Equals(ProjectSettings.XML_ELEM_SETTINGS))
                            {
                                if (!ParseSettings(reader, out errMsg))
                                    return false;
                            }
                            else
                            {
                                errMsg = ERRMSG_INVALID_ELEMENT + reader.Name;
                                return false;
                            }
                        }
                        else if (reader.NodeType == XmlNodeType.EndElement)
                        {
                            break;
                        }
                        else
                        {
                            errMsg = ERRMSG_MALFORMED_PROJECT;
                            return false;
                        }
                    }
                }
            }
            errMsg = "";
            return true;
        }

        private bool ParseDataSources(XmlReader reader, out string errMsg)
        {
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    if (reader.Name.Equals("data-source"))
                    {
                        if (reader.HasAttributes)
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                if (reader.Name.Equals("csvfile"))
                                {
                                    dataSource = new DataSourceCSVFile(reader.Value);
                                }
                                else if (reader.Name.Equals("database"))
                                {
                                    dataSource = new DataSourceDatabase();
                                }
                            }
                        }
                    }
                    else
                    {
                        errMsg = "Invalid data-sources element encountered: " + reader.Name;
                        return false;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement)
                {
                    break;
                }
                else
                {
                    errMsg = "Unexpected element in data-sources";
                    return false;
                }
            }
            errMsg = "";
            return true;
        }

        private bool ParseColorRanges(XmlReader reader, out string errMsg)
        {
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    if (!reader.Name.Equals("ds-color-ranges"))
                    {
                        errMsg = "invalid color-ranges element";
                        return false;
                    }
                    else
                    {
                        if(!ParseDsColorRanges(reader, out errMsg))
                        {
                            return false;
                        }
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement)
                {
                    errMsg = "";
                    return true;
                }
                else
                {
                    errMsg = "Invalid element in color-ranges";
                    return false;
                }
            }
            errMsg = "Unexpected EOF";
            return true;
        }

        private bool ParseSettings(XmlReader reader, out string errMsg)
        {
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    if (reader.Name.Equals(ProjectSettings.XML_ELEM_GOOGLE_MAP_SETTINGS))
                    {
                        if (!ParseGoogleMapSettings(reader, out errMsg))
                        {
                            return false;
                        }
                    }
                    else if (reader.Name.Equals(ProjectSettings.XML_ELEM_LOAD_GRAPH_SETTINGS))
                    {
                        if (!ParseGraphSettings(reader, out errMsg, true))
                        {
                            return false;
                        }
                    }
                    else if (reader.Name.Equals(ProjectSettings.XML_ELEM_GRAPH_SETTINGS))
                    {
                        if (!ParseGraphSettings(reader, out errMsg, false))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        errMsg = "invalid color-ranges element";
                        return false;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement)
                {
                    errMsg = "";
                    return true;
                }
                else
                {
                    errMsg = "Invalid element in color-ranges";
                    return false;
                }
            }
            errMsg = "Unexpected EOF";
            return true;
        }

        private bool ParseGoogleMapSettings(XmlReader reader, out string errMsg)
        {
            bool hasEndTag = false;
            string name;
            string value;

            GoogleMapSettings.EMapDisplayData? mapDisplayData = null;
            double? fillOpacity = null;
            double? strokeOpacity = null;
            double? dwellScale = null;
            double? loadScale = null;
            int? routeStrokeWeight = null;
            int? speedStrokeWeight = null;

            double dResult;
            int iResult;

            errMsg = null;
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    hasEndTag = !reader.IsEmptyElement;

                    name = reader.Name;
                    if (null == (value = reader.GetAttribute("value")))
                    {
                        errMsg = "Invalid element in settings: " + name;
                        return false;
                    }

                    if (name.Equals(ProjectSettings.XML_ELEM_GOOGLE_MAP_DISPLAY_DATA))
                    {
                        if (value.Equals("Dwell"))
                        {
                            mapDisplayData = GoogleMapSettings.EMapDisplayData.Dwell;
                        }
                        else if (value.Equals("Load"))
                        {
                            mapDisplayData = GoogleMapSettings.EMapDisplayData.Load;
                        }
                        else if (value.Equals("Ons"))
                        {
                            mapDisplayData = GoogleMapSettings.EMapDisplayData.Ons;
                        }
                        else if (value.Equals("Offs"))
                        {
                            mapDisplayData = GoogleMapSettings.EMapDisplayData.Offs;
                        }
                        else
                        {
                            errMsg = "Invalid element in settings: " + name + ": " + value;
                            return false;
                        }
                    }
                    else if (name.Equals(ProjectSettings.XML_ELEM_GOOGLE_MAP_FILL_OPACITY))
                    {
                        if (double.TryParse(value, out dResult))
                        {
                            fillOpacity = dResult;
                        }
                    }
                    else if (name.Equals(ProjectSettings.XML_ELEM_GOOGLE_MAP_STROKE_OPACITY))
                    {
                        if (double.TryParse(value, out dResult))
                        {
                            strokeOpacity = dResult;
                        }
                    }
                    else if (name.Equals(ProjectSettings.XML_ELEM_GOOGLE_MAP_DWELL_SCALE))
                    {
                        if (double.TryParse(value, out dResult))
                        {
                            dwellScale = dResult;
                        }
                    }
                    else if (name.Equals(ProjectSettings.XML_ELEM_GOOGLE_MAP_LOAD_SCALE))
                    {
                        if (double.TryParse(value, out dResult))
                        {
                            loadScale = dResult;
                        }
                    }
                    else if (name.Equals(ProjectSettings.XML_ELEM_GOOGLE_MAP_ROUTE_STROKE_WEIGHT))
                    {
                        if (int.TryParse(value, out iResult))
                        {
                            routeStrokeWeight = iResult;
                        }
                    }
                    else if (name.Equals(ProjectSettings.XML_ELEM_GOOGLE_MAP_SPEED_STROKE_WEIGHT))
                    {
                        if (int.TryParse(value, out iResult))
                        {
                            speedStrokeWeight = iResult;
                        }
                    }

                    if (hasEndTag)
                    {
                        reader.Skip(); // skip all children
                        reader.Read(); // read end tag
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement)
                {
                    googleMapSettings = new GoogleMapSettings(ProjectSettings.NAME_MAP_SETTINGS, mapDisplayData, fillOpacity,
                            strokeOpacity, dwellScale, loadScale, routeStrokeWeight, speedStrokeWeight);

                    errMsg = "";
                    return true;
                }
                else
                {
                    errMsg = "Invalid element in settings";
                    return false;
                }
            }
            return false;
        }

        private bool ParseGraphSettings(XmlReader reader, out string errMsg, bool isLoadOnOffGraph)
        {
            errMsg = null;
            bool hasEndTag = false;
            string graphName;
            string name;
            string value;
            double dResult;
            bool bResult;
            GraphSettings graphSettings;

            GraphSettings.ECursorMovement? cursorMovement = null;
            GraphSettings.EXAxisType? xAxisType = null;
            double? xAxisMin = null;
            double? xAxisMax = null;
            double? xAxisInterval = null;
            bool? syncCursors = null;
            bool? syncSettings = null;
            bool? enableYCursor = null;
            LoadGraphSettings.ELoadGraphType? loadGraphType = null;

            if (null == (graphName = reader.GetAttribute("name")))
            {
                errMsg = "Invalid element in graph settings: Missing name";
                return false;
            }

            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    hasEndTag = !reader.IsEmptyElement;

                    name = reader.Name;
                    if (null == (value = reader.GetAttribute(ProjectSettings.XML_ATTR_VALUE)))
                    {
                        errMsg = "Invalid element in settings: " + name;
                        return false;
                    }

                    if (name.Equals(ProjectSettings.XML_ELEM_GRAPH_CURSOR_MOVEMENT))
                    {
                        if (value.Equals("OnMouseOver"))
                        {
                            cursorMovement = GraphSettings.ECursorMovement.OnMouseClick;
                        }
                        else if (value.Equals("OnMouseClick"))
                        {
                            cursorMovement = GraphSettings.ECursorMovement.OnMouseClick;
                        }
                        else
                        {
                            errMsg = "Invalid element in settings: " + name + ": " + value;
                            return false;
                        }
                    }
                    else if (name.Equals(ProjectSettings.XML_ELEM_GRAPH_X_AXIS_TYPE))
                    {
                        if (value.Equals("TimeScale"))
                        {
                            xAxisType = GraphSettings.EXAxisType.TimeScale;
                        }
                        else if (value.Equals("Distance"))
                        {
                            xAxisType = GraphSettings.EXAxisType.Distance;
                        }
                        else if (value.Equals("Index"))
                        {
                            xAxisType = GraphSettings.EXAxisType.Index;
                        }
                        else
                        {
                            errMsg = "Invalid element in settings: " + name + ": " + value;
                            return false;
                        }
                    }
                    else if (name.Equals(ProjectSettings.XML_ELEM_GRAPH_X_AXIS_MIN))
                    {
                        if (double.TryParse(value, out dResult))
                        {
                            xAxisMin = dResult;
                        }
                    }
                    else if (name.Equals(ProjectSettings.XML_ELEM_GRAPH_X_AXIS_MAX))
                    {
                        if (double.TryParse(value, out dResult))
                        {
                            xAxisMax = dResult;
                        }
                    }
                    else if (name.Equals(ProjectSettings.XML_ELEM_GRAPH_X_AXIS_INTERVAL))
                    {
                        if (double.TryParse(value, out dResult))
                        {
                            xAxisInterval = dResult;
                        }
                    }
                    else if (name.Equals(ProjectSettings.XML_ELEM_GRAPH_SYNC_CURSORS))
                    {
                        if (bool.TryParse(value, out bResult))
                        {
                            syncCursors = bResult;
                        }
                    }
                    else if (name.Equals(ProjectSettings.XML_ELEM_GRAPH_SYNC_SETTINGS))
                    {
                        if (bool.TryParse(value, out bResult))
                        {
                            syncSettings = bResult;
                        }
                    }
                    else if (name.Equals(ProjectSettings.XML_ELEM_GRAPH_ENABLE_Y_CURSOR))
                    {
                        if (bool.TryParse(value, out bResult))
                        {
                            enableYCursor = bResult;
                        }
                    }
                    else if (name.Equals(ProjectSettings.XML_ELEM_LOAD_GRAPH_TYPE))
                    {
                        if (value.Equals("Range"))
                        {
                            loadGraphType = LoadGraphSettings.ELoadGraphType.Range;
                        }
                        else if (value.Equals("RangeColumn"))
                        {
                            loadGraphType = LoadGraphSettings.ELoadGraphType.RangeColumn;
                        }
                        else if (value.Equals("RangeSpline"))
                        {
                            loadGraphType = LoadGraphSettings.ELoadGraphType.RangeSpline;
                        }
                        else
                        {
                            errMsg = "Invalid element in settings: " + name + ": " + value;
                            return false;
                        }
                    }

                    if (hasEndTag)
                    {
                        reader.Skip(); // skip all children
                        reader.Read(); // read end tag
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement)
                {
                    if (isLoadOnOffGraph)
                    {
                        graphLoadSettings = new LoadGraphSettings(graphName, cursorMovement, xAxisType, xAxisMin,
                                xAxisMax, xAxisInterval, syncCursors, syncSettings, enableYCursor, loadGraphType);
                    }
                    else
                    {
                        graphSettings = new GraphSettings(graphName, cursorMovement, xAxisType, xAxisMin,
                                xAxisMax, xAxisInterval, syncCursors, syncSettings, enableYCursor);

                        if (graphName.Equals(ProjectSettings.XML_NAME_BUS_SPEED_GRAPH_SETTINGS))
                        {
                            graphSpeedSettings = graphSettings;
                        }
                        else if (graphName.Equals(ProjectSettings.XML_NAME_BUS_DWELL_GRAPH_SETTINGS))
                        {
                            graphDwellSettings = graphSettings;
                        }
                        else if (graphName.Equals(ProjectSettings.XML_NAME_BIKE_ECG_SETTINGS))
                        {
                            graphEcgSettings = graphSettings;
                        }
                        else if (graphName.Equals(ProjectSettings.XML_NAME_BIKE_GSR_SETTINGS))
                        {
                            graphGsrSettings = graphSettings;
                        }
                        else
                        {
                            errMsg = "Unknown element encountered: " + graphName;
                            return false;
                        }
                    }

                    errMsg = "";
                    return true;
                }
                else
                {
                    errMsg = "Invalid element in settings";
                    return false;
                }
            }
            return false;
        }

        private bool ParseDsColorRanges(XmlReader reader, out string errMsg)
        {
            string value;
            string dataSetName;
            double? lbound = null;
            double? ubound = null;
            Color? color = null;
            double dvalue;
            List<GraphColorRange> lgcrs = new List<GraphColorRange>();

            // ds-color-ranges should have one attribute
            if (reader.AttributeCount != 1)
            {
                errMsg = "Invalid number of attributes in ds-color-ranges";
                return false;
            }

            // Read attribute
            reader.MoveToFirstAttribute();
            dataSetName = reader.GetAttribute("data-set-name");
            if (string.IsNullOrWhiteSpace(dataSetName))
            {
                errMsg = "Missing data-set-name attribute";
                return false;
            }

            // read multiple color-range nodes
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    if (!reader.Name.Equals("color-range"))
                    {
                        errMsg = "Invalid ds-color-ranges element encountered";
                        return false;
                    }
                    else if (reader.AttributeCount != 3)
                    {
                        errMsg = "color-range element has wrong number of attributes";
                        return false;
                    }
                    else
                    {
                        reader.MoveToFirstAttribute();
                        // Get lbound attribute
                        value = reader.GetAttribute("lbound");
                        
                        if (string.IsNullOrEmpty(value))
                        {
                            errMsg = "Missing lbound attribute: ";
                            return false;
                        }
                        else
                        {
                            if (value.Equals("-infinity"))
                            {
                                lbound = Double.MinValue;
                            }
                            else if (Double.TryParse(value, out dvalue))
                            {
                                lbound = dvalue;
                            }
                            else
                            {
                                errMsg = "Invalid lbound encountered: " + value;
                                return false;
                            }
                        }

                        // Get ubound attribute
                        reader.MoveToNextAttribute();
                        value = reader.GetAttribute("ubound");

                        if (string.IsNullOrEmpty(value))
                        {
                            errMsg = "Missing ubound attribute: ";
                            return false;
                        }
                        else
                        {
                            if (value.Equals("+infinity"))
                            {
                                ubound = Double.MaxValue;
                            }
                            else if (Double.TryParse(value, out dvalue))
                            {
                                ubound = dvalue;
                            }
                            else
                            {
                                errMsg = "Invalid ubound encountered: " + value;
                                return false;
                            }
                        }

                        // Get color attribute
                        reader.MoveToNextAttribute();
                        value = reader.GetAttribute("color");

                        if (string.IsNullOrEmpty(value))
                        {
                            errMsg = "Null or empty color attribute: ";
                            return false;
                        }
                        else
                        {
                            color = Util.FromText(value);
                        }

                        if ((null != lbound) && (null != ubound) && (null != color))
                        {
                            GraphColorRange g = new GraphColorRange((double)lbound, (double)ubound, (Color)color);
                            lgcrs.Add(g);
                            lbound = null;
                            ubound = null;
                            color = null;
                        }
                        else
                        {
                            errMsg = "Missing ds-color-ranges attribute";
                            return false;
                        }
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement)
                {
                    dictGraphColorRanges.Add(dataSetName, lgcrs.ToArray());
                    errMsg = "";
                    return true;
                }
                else
                {
                    errMsg = "Invalid ds-color-ranges element";
                    return false;
                }
            }
            errMsg = "Unexpected EOF";
            return true;
        }

        public DataSource DataSource
        {
            get { return dataSource; }
        }

        public Dictionary<string, GraphColorRange[]> DictGraphColorRanges
        {
            get { return dictGraphColorRanges; }
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
