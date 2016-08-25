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
    public partial class FrmHighlightRange : Form
    {
        private Color defaultBackgroundColor;
        private double minValue;
        private double maxValue;
        private Color rangeColor = Color.Red;
        private string graphName;

        public FrmHighlightRange(string graphName)
        {
            InitializeComponent();
            this.graphName = graphName;
        }

        private void FrmHighlightRange_Load(object sender, EventArgs e)
        {
            defaultBackgroundColor = txtName.BackColor;
            tbColor.BackColor = rangeColor;
            this.Text = "Highlight Range" + graphName;
        }

        /// <summary>
        /// Handles btnColor Click event.  Displays color dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = rangeColor;
            DialogResult rc = colorDialog1.ShowDialog();

            if (DialogResult.OK == rc)
            {
                tbColor.BackColor = rangeColor = colorDialog1.Color;
            }
        }

        /// <summary>
        /// Handles button OK Click event.  Validates all user input.  Displays error message if
        /// invalid fields found else closes dialog ith OK dialog result.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            txtName.BackColor = defaultBackgroundColor;
            txtLowValue.BackColor = defaultBackgroundColor;
            txtHighValue.BackColor = defaultBackgroundColor;

            txtName.Text = txtName.Text.Trim();
            txtLowValue.Text = txtLowValue.Text.Trim();
            txtHighValue.Text = txtHighValue.Text.Trim();

            // Validate name field
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                txtName.Text = txtName.Text.Trim();
                txtName.BackColor = Color.Yellow;
                MessageBox.Show("Name value field is blank", "Error: Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Validate Min Value field
            else if (string.IsNullOrWhiteSpace(txtLowValue.Text))
            {
                txtLowValue.BackColor = Color.Yellow;
                MessageBox.Show("Min value field is blank", "Error: Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!Double.TryParse(txtLowValue.Text, out minValue))
            {
                txtLowValue.BackColor = Color.Yellow;
                MessageBox.Show("Min value field does not contain a valid value!", "Error: Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Validate Max Value field
            else if (string.IsNullOrWhiteSpace(txtHighValue.Text))
            {
                txtHighValue.BackColor = Color.Yellow;
                MessageBox.Show("Max value field is blank", "Error: Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!Double.TryParse(txtHighValue.Text, out maxValue))
            {
                txtHighValue.BackColor = Color.Yellow;
                MessageBox.Show("Max value field does not contain a valid value!", "Error: Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        public double MinValue
        {
            get { return minValue; }
        }

        public double MaxValue
        {
            get { return maxValue; }
        }

        public Color RangeColor
        {
            get { return rangeColor; }
        }
    }
}
