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
    public partial class FrmSettings : Form
    {
        private static readonly string MODULE_TAG = "FrmSettings";

        UserEditableSettings ues = null;

        public FrmSettings(UserEditableSettings userEditableSettings)
        {
            InitializeComponent();

            this.ues = new UserEditableSettings(userEditableSettings);
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            cbBusRouteStrokeWeight.SelectedIndex = ues.BusRouteStrokeWeight - 1;
            cbBusSpeedStrokeWeight.SelectedIndex = ues.BusSpeedStrokeWeight - 1;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cbBusRouteStrokeWeight_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                ues.BusRouteStrokeWeight = int.Parse(cbBusRouteStrokeWeight.SelectedItem.ToString());
            }
            catch(Exception ex)
            {
                Util.LogException(MODULE_TAG, ex);
            }
        }

        private void cbBusSpeedStrokeWeight_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                ues.BusSpeedStrokeWeight = int.Parse(cbBusSpeedStrokeWeight.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                Util.LogException(MODULE_TAG, ex);
            }
        }

        public UserEditableSettings UserEditableSettings
        {
            get { return ues; }
        }
    }
}
