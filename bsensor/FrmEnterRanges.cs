using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bsensor
{
    public partial class FrmEnterRanges : Form
    {
        private GraphColorRange[] graphColorRanges;

        private static readonly char[] seperators = { ',', ';', ' ', '\t' };

        public FrmEnterRanges()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            double dBound;

            SortedSet<double> ssbounds = new SortedSet<double>();
            string[] sBounds = txtBounds.Text.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            if (sBounds.Length == 0)
            {
                return;
            }

            foreach(string bound in sBounds)
            {
                if (!Double.TryParse(bound, out dBound))
                {
                    MessageBox.Show("Invalid range value encountered", "Invalid Value", MessageBoxButtons.OK);
                    return;
                }
                if (!ssbounds.Contains(dBound))
                {
                    ssbounds.Add(dBound);
                }
            }

            double[] bounds = ssbounds.ToArray<double>();
            int numBounds = ssbounds.Count;

            graphColorRanges = new GraphColorRange[numBounds + 1];
            for (int i = 0; i <= numBounds; ++i)
            {
                if (i == 0) {
                    graphColorRanges[i] = new GraphColorRange(double.MinValue, bounds[i], Color.Red);
                }
                else if (i == numBounds)
                {
                    graphColorRanges[i] = new GraphColorRange(bounds[i-1], double.MaxValue, Color.Red);
                }
                else
                {
                    graphColorRanges[i] = new GraphColorRange(bounds[i - 1], bounds[i], Color.Red);
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    
        public GraphColorRange[] GraphColorRanges
        {
            get { return graphColorRanges; }
        }
    }
}
