using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    public class BusLoad
    {
        private int ons;
        private int offs;
        private int load;

        public BusLoad(int ons, int offs, int load)
        {
            this.ons = ons;
            this.offs = offs;
            this.load = load;
        }

        public int Ons
        {
            get { return ons; }
        }

        public int Offs
        {
            get { return offs; }
        }

        public int Load
        {
            get { return load; }
        }
    }
}
