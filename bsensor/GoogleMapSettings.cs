using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    [DefaultPropertyAttribute("Data")]
    public class GoogleMapSettings : ProjectSettings
    {
        public static readonly int DEFAULT_BUS_ROUTE_STROKE_WEIGHT = 10;
        public static readonly int DEFAULT_BUS_SPEED_STROKE_WEIGHT = 4;

        public enum EMapDisplayData { Dwell, Load, Ons, Offs };
        public enum EDataSource { File, Database };

        public const EMapDisplayData DEFAULT_SETTING_MAP_DISPLAY_DATA = EMapDisplayData.Dwell;
        public const double MIN_DWELL_SCALE_FACTOR = 1;
        public const double MIN_LOAD_SCALE_FACTOR = 0.1;

        public const double DEFAULT_DWELL_SCALE_FACTOR = 20;
        public const double DEFAULT_LOAD_SCALE_FACTOR = 1.5;

        public const double MIN_OPACITY = 0.0;
        public const double MAX_OPACITY = 1.0;
        public const double DEFAULT_OPACITY = 0.8;

        private EMapDisplayData mapDisplayData = DEFAULT_SETTING_MAP_DISPLAY_DATA;
        private EDataSource dataSource;
        private double strokeOpacity = DEFAULT_OPACITY;
        private double fillOpacity = DEFAULT_OPACITY;
        private double dwellScaleFactor = DEFAULT_DWELL_SCALE_FACTOR;
        private double loadScaleFactor = DEFAULT_LOAD_SCALE_FACTOR;
        private string fileName;
        private int busRouteStrokeWeight = DEFAULT_BUS_ROUTE_STROKE_WEIGHT;
        private int busSpeedStrokeWeight = DEFAULT_BUS_SPEED_STROKE_WEIGHT;

        #region EventMapSettingsChanged
        public delegate void EventMapSettingsChanged(GoogleMapSettings mapSettings);
        EventMapSettingsChanged eventMapSettingsChanged;
        public void AddOnEventMapSettingsChanged(EventMapSettingsChanged e)
        {
            eventMapSettingsChanged += e;
        }
        public void RemoveOnEventMapSettingsChanged(EventMapSettingsChanged e)
        {
            eventMapSettingsChanged -= e;
        }
        public void OnEventMapSettingsChanged(GoogleMapSettings mapSettings)
        {
            if (null != eventMapSettingsChanged)
                eventMapSettingsChanged(mapSettings);
        }
        #endregion EventMapSettingsChanged

        public GoogleMapSettings(string name) : base(name)
        {
        }

        public GoogleMapSettings(string name,
            GoogleMapSettings.EMapDisplayData? mapDisplayData,
            double? fillOpacity,
            double? strokeOpacity,
            double? dwellScaleFactor,
            double? loadScaleFactor,
            int? busRouteStrokeWeight,
            int? busSpeedStrokeWeight) : base(name)
        {
            this.mapDisplayData = mapDisplayData ?? DEFAULT_SETTING_MAP_DISPLAY_DATA;
            this.fillOpacity = fillOpacity ?? DEFAULT_OPACITY;
            this.strokeOpacity = strokeOpacity ?? DEFAULT_OPACITY;
            this.dwellScaleFactor = dwellScaleFactor ?? DEFAULT_DWELL_SCALE_FACTOR;
            this.loadScaleFactor = loadScaleFactor ?? DEFAULT_LOAD_SCALE_FACTOR;
            this.busRouteStrokeWeight = busRouteStrokeWeight ?? DEFAULT_BUS_ROUTE_STROKE_WEIGHT;
            this.busSpeedStrokeWeight = busSpeedStrokeWeight ?? DEFAULT_BUS_SPEED_STROKE_WEIGHT;
        }

        [CategoryAttribute("Data Point"),
            DescriptionAttribute("Selects the data represented by the circles on the map."),
            DefaultValue(DEFAULT_SETTING_MAP_DISPLAY_DATA)]
        public EMapDisplayData Data
        {
            get { return mapDisplayData; }
            set
            {
                mapDisplayData = value;
                //OnEventMapDisplayDataChanged(mapDisplayData);
                OnEventMapSettingsChanged(this);
            }
        }

        [CategoryAttribute("Data Point"), 
            DescriptionAttribute("The source of the data."), 
            ReadOnlyAttribute(true)]
        public EDataSource Source
        {
            get { return dataSource; }
        }

        [CategoryAttribute("Data Point"), 
            DescriptionAttribute("The opacity of the outer part of the data circles displayed on the map."),
             DefaultValue(DEFAULT_OPACITY)]
        public double StrokeOpacity
        {
            get { return strokeOpacity; }
            set
            {
                if ((value >= MIN_OPACITY) && (value <= MAX_OPACITY))
                {
                    strokeOpacity = value;
                    OnEventMapSettingsChanged(this);
                }
            }
        }

        [CategoryAttribute("Data Point"),
            DescriptionAttribute("The opacity of the central part of the data circles displayed on the map."),
             DefaultValue(DEFAULT_OPACITY)]
        public double FillOpacity
        {
            get { return fillOpacity; }
            set
            {
                if ((value >= MIN_OPACITY) && (value <= MAX_OPACITY))
                {
                    fillOpacity = value;
                    OnEventMapSettingsChanged(this);
                }
            }
        }

        [CategoryAttribute("Data Point"),
            DescriptionAttribute("Scales the size of the dwell data circles on the map."),
            DefaultValue(DEFAULT_DWELL_SCALE_FACTOR)]
        public double DwellScaleFactor
        {
            get { return dwellScaleFactor; }
            set
            {
                if (value >= MIN_DWELL_SCALE_FACTOR)
                {
                    dwellScaleFactor = value;
                    OnEventMapSettingsChanged(this);
                }
            }
        }

        [CategoryAttribute("Data Point"),
            DescriptionAttribute("Scales the size of the Load/Ons/Offs data circles on the map. (passengers/cm)"),
            DefaultValue(DEFAULT_LOAD_SCALE_FACTOR)]
        public double LoadScaleFactor
        {
            get { return loadScaleFactor; }
            set
            {
                if (value >= MIN_LOAD_SCALE_FACTOR)
                {
                    loadScaleFactor = value;
                    OnEventMapSettingsChanged(this);
                }
            }
        }

        [CategoryAttribute("Data Point"), 
            DescriptionAttribute("Name of file containg the data."),
            ReadOnlyAttribute(true),
            DefaultValue("")]
        public string FileName
        {
            get { return fileName; }
        }

        [CategoryAttribute("Route"), 
            DescriptionAttribute("Controls thickness of the drawn route."),
            DefaultValue(10)]
        public int RouteStrokeWeight
        {
            get { return busRouteStrokeWeight; }
            set
            {
                busRouteStrokeWeight = value;
                OnEventMapSettingsChanged(this);
            }
        }

        [CategoryAttribute("Route"), 
            DescriptionAttribute("Controls thickness of the drawn speed route."),
             DefaultValue(4)]
        public int SpeedStrokeWeight
        {
            get { return busSpeedStrokeWeight; }
            set
            {
                busSpeedStrokeWeight = value;
                OnEventMapSettingsChanged(this);
            }
        }
    }
}
