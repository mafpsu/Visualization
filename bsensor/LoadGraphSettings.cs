using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    [DefaultPropertyAttribute("LoadGraphType")]
    public class LoadGraphSettings : GraphSettings
    {
        public enum ELoadGraphType { Range, RangeColumn, RangeSpline };

        public const ELoadGraphType DEFAULT_LOAD_GRAPH_TYPE = ELoadGraphType.RangeColumn;

        private ELoadGraphType loadGraphType = DEFAULT_LOAD_GRAPH_TYPE;

        public LoadGraphSettings(string name) : base(name)
        {
        }

        public LoadGraphSettings(string name,
            GraphSettings.ECursorMovement? cursorMovement,
            GraphSettings.EXAxisType? xAxisType,
            double? xAxisMin,
            double? xAxisMax,
            double? xAxisInterval,
            bool? syncCursors,
            bool? syncSettings,
            bool? enableYCursor,
            ELoadGraphType? loadGraphType) : base(name)
        {
            this.cursorMovement = cursorMovement ?? GraphSettings.ECursorMovement.OnMouseClick;
            this.xAxisType = xAxisType ?? GraphSettings.EXAxisType.Distance;
            this.xAxisMin = xAxisMin ?? DEFAULT_SETTING_X_AXIS_MIN;
            this.xAxisMax = xAxisMax ?? DEFAULT_SETTING_X_AXIS_MAX;
            this.xAxisInterval = xAxisInterval ?? DEFAULT_SETTING_X_AXIS_INTERVAL;
            this.syncCursors = syncCursors ?? DEFAULT_SETTING_SYNC_CURSORS;
            this.syncSettings = syncSettings ?? DEFAULT_SETTING_SYNC_SETTINGS;
            this.enableYCursor = enableYCursor ?? DEFAULT_SETTING_ENABLE_Y_CURSOR;
            this.loadGraphType = loadGraphType ?? DEFAULT_LOAD_GRAPH_TYPE;
        }

        [CategoryAttribute("X Axis"),
            DescriptionAttribute("Selects the type of graph to display."),
            DefaultValue(DEFAULT_LOAD_GRAPH_TYPE)]
        public ELoadGraphType LoadGraphType
        {
            get { return loadGraphType; }
            set
            {
                loadGraphType = value;
                OnEventSettingsChanged();
            }
        }
    }
}
