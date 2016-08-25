using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    public class ShimmerSensorReading
    {
        private long _id = -1;
        private Nullable<long> _coord_id;
        private Nullable<int> _shimmer_sensor_id;
        private Nullable<long> _num_s = -1;
        private Nullable<double> _avg_0;
        private Nullable<double> _avg_1;
        private Nullable<double> _avg_2;
        private Nullable<double> _std_0 = 0;
        private Nullable<double> _std_1 = 0;
        private Nullable<double> _std_2 = 0;

        public ShimmerSensorReading(Nullable<long> num_s, Nullable<double> avg_0, Nullable<double> avg_1, Nullable<double> avg_2)
        {
            _num_s = num_s;
            _avg_0 = avg_0;
            _avg_1 = avg_1;
            _avg_2 = avg_2;
        }

        public ShimmerSensorReading(
            long id, 
            Nullable<long> coord_id,
            Nullable<int> shimmer_sensor_id,
            Nullable<long> num_s,
            Nullable<double> avg_0,
            Nullable<double> avg_1,
            Nullable<double> avg_2,
            Nullable<double> std_0,
            Nullable<double> std_1, 
            Nullable<double> std_2)
        {
            _id = id;
            _coord_id = coord_id;
            _shimmer_sensor_id = shimmer_sensor_id;
            _num_s = num_s;
            _avg_0 = avg_0;
            _avg_1 = avg_1;
            _avg_2 = avg_2;
            _std_0 = std_0;
            _std_1 = std_1;
            _std_2 = std_2;
        }

        public long id { get { return _id; } }
        public Nullable<long> coord_id { get { return _coord_id; } }
        public Nullable<int> shimmer_sensor_id { get { return _shimmer_sensor_id; } }
        public Nullable<long> num_s { get { return _num_s; } }
        public Nullable<double> avg_0 { get { return _avg_0; } }
        public Nullable<double> avg_1 { get { return _avg_1; } }
        public Nullable<double> avg_2 { get { return _avg_2; } }
        public Nullable<double> std_0 { get { return _std_0; } }
        public Nullable<double> std_1 { get { return _std_1; } }
        public Nullable<double> std_2 { get { return _std_2; } }
    }
}
