using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    public class ECGReading
    {
        private double _exg1_ch1;
        private double _exg1_ch2;
        private double _exg2_ch1;
        private double _exg2_ch2;

        public ECGReading(double exg1_ch1, double exg1_ch2, double exg2_ch1, double exg2_ch2)
        {
            _exg1_ch1 = exg1_ch1;
            _exg1_ch2 = exg1_ch2;
            _exg2_ch1 = exg2_ch1;
            _exg2_ch2 = exg2_ch2;
        }

        public double exg1_ch1
        {
            get { return _exg1_ch1; }
        }

        public double exg1_ch2
        {
            get { return _exg1_ch2; }
        }

        public double exg2_ch1
        {
            get { return _exg2_ch1; }
        }

        public double exg2_ch2
        {
            get { return _exg2_ch2; }
        }
    }
}
