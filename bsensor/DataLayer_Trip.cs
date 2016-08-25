using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    class DataLayer_Trip
    {
        private long _id;
        private Nullable<long> _user_id;
        private long _client_trip_id1;
        private Nullable<long> _n_coord1;

        public DataLayer_Trip(long id, Nullable<long> user_id, int client_trip_id, Nullable<long> n_coord1)
        {
            _id = id;
            _user_id = user_id;
            _client_trip_id1 = client_trip_id;
            _n_coord1 = n_coord1;
        }

        public long id { get { return _id; } }
        public Nullable<long> user_id { get { return _user_id; } }
        public long client_trip_id { get { return _client_trip_id1; } }
        public System.DateTime start { get { return System.DateTime.MinValue; } }
        public System.DateTime stop { get { return System.DateTime.MinValue; } }
        public Nullable<long> n_coord { get { return _n_coord1; } }
    }
}
