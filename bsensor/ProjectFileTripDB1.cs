using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    class ProjectFileTripDB1 : ProjectFileTrip
    {
        long tripID;

        public ProjectFileTripDB1(long tripID)
        {
            this.tripID = tripID;
        }

        public long TripID
        {
            get { return tripID; }
        }
    }
}
