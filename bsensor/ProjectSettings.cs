using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    public class ProjectSettings
    {
        private string name;

        public const string NAME_MAP_SETTINGS = "Map";
        public const string NAME_BUS_SPEED_GRAPH_SETTINGS = "Speed Graph";
        public const string NAME_BUS_DWELL_GRAPH_SETTINGS = "Dwell Graph";
        public const string NAME_BUS_LOAD_ONS_OFFS_SETTINGS = "Load-Ons-Offs Graph";
        public const string NAME_BIKE_ECG_SETTINGS = "ECG Graph";
        public const string NAME_BIKE_GSR_SETTINGS = "GSR Graph";

        public const string XML_NAME_MAP_SETTINGS = "google-map";
        public const string XML_NAME_BUS_SPEED_GRAPH_SETTINGS = "speed-graph";
        public const string XML_NAME_BUS_DWELL_GRAPH_SETTINGS = "dwell-graph";
        public const string XML_NAME_BUS_LOAD_ONS_OFFS_SETTINGS = "load-ons-offs-graph";
        public const string XML_NAME_BIKE_ECG_SETTINGS = "ecg-graph";
        public const string XML_NAME_BIKE_GSR_SETTINGS = "gsr-graph";

        public const string XML_ATTR_NAME = "name";
        public const string XML_ATTR_VALUE = "value";

        public const string XML_ELEM_DATA_SOURCES = "data-sources";
        public const string XML_ELEM_COLOR_RANGES = "color-ranges";
        public const string XML_ELEM_SETTINGS = "settings";

        public const string XML_ELEM_GOOGLE_MAP_SETTINGS = "google-map-settings";
        public const string XML_ELEM_GOOGLE_MAP_DISPLAY_DATA = "map-display-data";
        public const string XML_ELEM_GOOGLE_MAP_FILL_OPACITY = "fill-opacity";
        public const string XML_ELEM_GOOGLE_MAP_STROKE_OPACITY = "stroke-opacity";
        public const string XML_ELEM_GOOGLE_MAP_DWELL_SCALE = "dwell-scale-factor";
        public const string XML_ELEM_GOOGLE_MAP_LOAD_SCALE = "load-scale-factor";
        public const string XML_ELEM_GOOGLE_MAP_ROUTE_STROKE_WEIGHT = "route-stroke-weight";
        public const string XML_ELEM_GOOGLE_MAP_SPEED_STROKE_WEIGHT = "speed-stroke-weight";

        public const string XML_ELEM_LOAD_GRAPH_SETTINGS = "load-graph-settings";
        public const string XML_ELEM_LOAD_GRAPH_TYPE = "load-graph-type";

        public const string XML_ELEM_GRAPH_SETTINGS = "graph-settings";
        public const string XML_ELEM_GRAPH_CURSOR_MOVEMENT = "cursor-movement";
        public const string XML_ELEM_GRAPH_X_AXIS_TYPE = "x-axis-type";
        public const string XML_ELEM_GRAPH_X_AXIS_MIN = "x-axis-min";
        public const string XML_ELEM_GRAPH_X_AXIS_MAX = "x-axis-max";
        public const string XML_ELEM_GRAPH_X_AXIS_INTERVAL = "x-axis-interval";
        public const string XML_ELEM_GRAPH_SYNC_CURSORS = "sync-cursors";
        public const string XML_ELEM_GRAPH_SYNC_SETTINGS = "sync-settings";
        public const string XML_ELEM_GRAPH_ENABLE_Y_CURSOR = "enable-y-cursor";

        public ProjectSettings(string name)
        {
            this.name = name;
        }

        [BrowsableAttribute(false),
            DefaultValueAttribute(false)]
        public string Name
        {
            get { return name; }
        }

        public override string ToString()
        {
            return name;
        }
    }
}
