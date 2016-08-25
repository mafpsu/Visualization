using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace bsensor
{
    public class GraphColorRange
    {
        private Color color;
        private double min;
        private double max;

        public GraphColorRange(double min, double max, Color color)
        {
            this.min = min;
            this.max = max;
            this.color = color;
        }
        public double Min
        {
            get { return this.min; }
        }
        public double Max
        {
            get { return this.max; }
        }
        public Color Color
        {
            get { return this.color; }
            set { this.color = value; }
        }
    }



}
