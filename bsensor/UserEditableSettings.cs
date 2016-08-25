using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    public class UserEditableSettings
    {
        public static readonly int DEFAULT_BUS_ROUTE_STROKE_WEIGHT = 10;
        public static readonly int DEFAULT_BUS_SPEED_STROKE_WEIGHT = 4;

        private int busRouteStrokeWeight = DEFAULT_BUS_ROUTE_STROKE_WEIGHT;
        private int busSpeedStrokeWeight = DEFAULT_BUS_SPEED_STROKE_WEIGHT;

        public UserEditableSettings()
        {
        }

        public UserEditableSettings(UserEditableSettings ues)
        {
            busRouteStrokeWeight = ues.busRouteStrokeWeight;
        }

        public int BusRouteStrokeWeight
        {
            get { return busRouteStrokeWeight; }
            set { busRouteStrokeWeight = value; }
        }

        public int BusSpeedStrokeWeight
        {
            get { return busSpeedStrokeWeight; }
            set { busSpeedStrokeWeight = value; }
        }

        public void CopyTo(UserEditableSettings ues)
        {
            ues.busRouteStrokeWeight = busRouteStrokeWeight;
            ues.busSpeedStrokeWeight = busSpeedStrokeWeight;
        }
    }
}
