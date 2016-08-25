using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    public class TripInfo
    {
        private int numPoints;

        public TripInfo(int numPoints)
        {
            this.numPoints = numPoints;
        }

        public int NumPoints
        {
            get { return numPoints; }
        }
    }
}
