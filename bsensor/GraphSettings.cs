using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    [DefaultPropertyAttribute("XAxisType")]
    public class GraphSettings : ProjectSettings
    {
        public const bool DEFAULT_SETTING_SYNC_CURSORS = true;
        public const bool DEFAULT_SETTING_SYNC_SETTINGS = true;
        public const bool DEFAULT_SETTING_ENABLE_Y_CURSOR = true;
        public const double DEFAULT_SETTING_X_AXIS_MIN = 0.0;
        public const double DEFAULT_SETTING_X_AXIS_MAX = 1.0;
        public const double DEFAULT_SETTING_X_AXIS_INTERVAL = 0.1;

        public enum EXAxisType { TimeScale, Distance, Index };
        public enum ECursorMovement { OnMouseOver, OnMouseClick };

        public const ECursorMovement DEFAULT_CURSOR_MOVEMENT = ECursorMovement.OnMouseClick;
        public const EXAxisType DEFAULT_X_AXIS_TYPE = EXAxisType.TimeScale;

        protected EXAxisType xAxisType = DEFAULT_X_AXIS_TYPE;
        protected double xAxisMin = DEFAULT_SETTING_X_AXIS_MIN;
        protected double xAxisMax = DEFAULT_SETTING_X_AXIS_MAX;
        protected double xAxisInterval = DEFAULT_SETTING_X_AXIS_INTERVAL;
        protected bool syncCursors = DEFAULT_SETTING_SYNC_CURSORS;
        protected bool syncSettings = DEFAULT_SETTING_SYNC_SETTINGS;
        protected bool enableYCursor = DEFAULT_SETTING_ENABLE_Y_CURSOR;
        protected ECursorMovement cursorMovement = ECursorMovement.OnMouseClick;
        protected List<GraphColorRange> graphColorRanges = new List<GraphColorRange>();
        protected GraphSettings other1;
        protected GraphSettings other2;

        #region EventSettingsChanged

        public delegate void EventSettingsChanged();
        EventSettingsChanged eventSettingsChanged;
        public void AddOnEventSettingsChanged(EventSettingsChanged e)
        {
            eventSettingsChanged += e;
        }
        public void RemoveOnEventSettingsChanged(EventSettingsChanged e)
        {
            eventSettingsChanged -= e;
        }
        public void OnEventSettingsChanged()
        {
            if (null != eventSettingsChanged)
                eventSettingsChanged();
        }

        #endregion EventSettingsChanged

        public GraphSettings(string name) : base(name)
        {
        }

        public GraphSettings(string name,
            GraphSettings.ECursorMovement? cursorMovement,
            GraphSettings.EXAxisType? xAxisType,
            double? xAxisMin,
            double? xAxisMax,
            double? xAxisInterval,
            bool? syncCursors,
            bool? syncSettings,
            bool? enableYCursor) : base(name)
        {
            this.cursorMovement = cursorMovement ?? DEFAULT_CURSOR_MOVEMENT;
            this.xAxisType = xAxisType ?? DEFAULT_X_AXIS_TYPE;
            this.xAxisMin = xAxisMin ?? DEFAULT_SETTING_X_AXIS_MIN;
            this.xAxisMax = xAxisMax ?? DEFAULT_SETTING_X_AXIS_MAX;
            this.xAxisInterval = xAxisInterval ?? DEFAULT_SETTING_X_AXIS_INTERVAL;
            this.syncCursors = syncCursors ?? DEFAULT_SETTING_SYNC_CURSORS;
            this.syncSettings = syncSettings ?? DEFAULT_SETTING_SYNC_SETTINGS;
            this.enableYCursor = enableYCursor ?? DEFAULT_SETTING_ENABLE_Y_CURSOR;
        }

        [CategoryAttribute("Cursor Control"),
            DescriptionAttribute("Causes cursor to move on mouse over, or on mouse click."),
            DefaultValue(DEFAULT_CURSOR_MOVEMENT)]
        public ECursorMovement CursorMovement
        {
            get { return cursorMovement; }
            set
            {
                cursorMovement = value;
                OnEventSettingsChanged();

                if (syncSettings)
                {
                    if (other1.cursorMovement != value)  // Note: accessing member (is more efficient)
                    {
                        other1.CursorMovement = value;   // Note: setting property causes changes to propogate to all
                    }
                    if (other2.cursorMovement != value)  // Note: accessing member
                    {
                        other2.CursorMovement = value;   // Note setting property
                    }
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the X axis type to a time scale, distance scale, or data point index.
        /// </summary>
        [CategoryAttribute("X Axis"),
            DescriptionAttribute("Selects the x axis data source."),
            DefaultValue(DEFAULT_X_AXIS_TYPE)]
        public EXAxisType XAxisType
        {
            get { return xAxisType; }
            set
            {
                xAxisType = value;
                OnEventSettingsChanged();

                if (syncSettings)
                {
                    if (other1.xAxisType != value)  // Note accessing member
                    {
                        other1.XAxisType = value;   // Note setting property
                    }
                    if (other2.xAxisType != value)  // Note accessing member
                    {
                        other2.XAxisType = value;   // Note setting property
                    }
                }
            }
        }

        private EXAxisType XAxisTypeNS
        {
            set
            {
                xAxisType = value;
                OnEventSettingsChanged();
            }
        }

        [CategoryAttribute("X Axis"),
            DescriptionAttribute("The minimum value on the x axis.."),
            DefaultValue(DEFAULT_SETTING_X_AXIS_MIN)]
        public double XAxisMin
        {
            get { return xAxisMin; }
            set
            {
                xAxisMin = value;
                OnEventSettingsChanged();

                if (syncSettings)
                {
                    if (other1.xAxisMin != value)  // Note accessing member
                    {
                        other1.XAxisMin = value;   // Note setting property
                    }
                    if (other2.xAxisMin != value)  // Note accessing member
                    {
                        other2.XAxisMin = value;   // Note setting property
                    }
                }
            }
        }

        [CategoryAttribute("X Axis"),
            DescriptionAttribute("The maximum value on the x axis."),
            DefaultValue(DEFAULT_SETTING_X_AXIS_MAX)]
        public double XAxisMax
        {
            get { return xAxisMax; }

            set
            {
                xAxisMax = value;
                OnEventSettingsChanged();

                if (syncSettings)
                {
                    // Note: accessing member for quicker access
                    if (other1.xAxisMax != value)
                    {
                        // Note: setting property causes change event to be fired
                        other1.XAxisMax = value;
                    }

                    // Note: accessing member for quicker access
                    if (other2.xAxisMax != value)
                    {
                        // Note: setting property causes change event to be fired
                        other2.XAxisMax = value;
                    }
                }
            }
        }

        [CategoryAttribute("X Axis"),
            DescriptionAttribute("The interval along the x axis."),
            DefaultValue(DEFAULT_SETTING_X_AXIS_INTERVAL)]
        public double XAxisInterval
        {
            get { return xAxisInterval; }

            set
            {
                xAxisInterval = value;
                OnEventSettingsChanged();

                if (syncSettings)
                {
                    // Note: accessing member for quicker access
                    if (other1.xAxisInterval != value)
                    {
                        // Note: setting property causes change event to be fired
                        other1.XAxisInterval = value;
                    }

                    // Note: accessing member for quicker access
                    if (other2.xAxisInterval != value)
                    {
                        // Note: setting property causes change event to be fired
                        other2.XAxisInterval = value;
                    }
                }
            }
        }

        [CategoryAttribute("Cursor Control"),
            DescriptionAttribute("Move all graph cursors simultaneously."),
            DefaultValue(DEFAULT_SETTING_SYNC_CURSORS)]
        public bool SyncCursors
        {
            get { return syncCursors; }

            set
            {
                syncCursors = value;

                // Note: accessing member so as not to cause propogation
                other1.syncCursors = value;

                // Note: accessing member so as not to cause propogation
                other2.syncCursors = value;
            }
        }

        [CategoryAttribute("Global Settings"),
            DescriptionAttribute("Change all graph settings simultaneously."),
            DefaultValue(DEFAULT_SETTING_SYNC_SETTINGS)]
        public bool SyncSettings
        {
            get { return syncSettings; }

            set
            {
                syncSettings = value;

                // Note: accessing member so as not to cause propogation
                other1.syncSettings = value;

                // Note: accessing member so as not to cause propogation
                other2.syncSettings = value;
            }
        }

        [CategoryAttribute("Cursor Control"),
            DescriptionAttribute("Enable the Y cursor."),
            DefaultValue(DEFAULT_SETTING_ENABLE_Y_CURSOR)]
        public bool EnableYCursor
        {
            get { return enableYCursor; }
            set
            {
                enableYCursor = value;
                OnEventSettingsChanged();

                if (syncSettings)
                {
                    // Note: accessing member for quicker access
                    if (other1.enableYCursor != value)
                    {
                        // Note: setting property causes change event to be fired
                        other1.EnableYCursor = value;
                    }

                    // Note: accessing member for quicker access
                    if (other2.enableYCursor != value)
                    {
                        // Note: setting property causes change event to be fired
                        other2.EnableYCursor = value;
                    }
                }
            }
        }

        [CategoryAttribute("Graph Color"),
                    DescriptionAttribute("Create graph color ranges."),
                    ReadOnlyAttribute(true)]
        public List<GraphColorRange> GraphColorRanges
        {
            get { return graphColorRanges; }
            set { graphColorRanges = value; }
        }

        [BrowsableAttribute(false),
            DefaultValueAttribute(false)]
        public GraphSettings Other1
        {
            get { return other1; }
            set { other1 = value; }
        }

        [BrowsableAttribute(false),
            DefaultValueAttribute(false)]
        public GraphSettings Other2
        {
            get { return other2; }
            set { other2 = value; }
        }
    }
}
