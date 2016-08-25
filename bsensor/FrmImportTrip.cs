using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace bsensor
{
    public partial class FrmImportTrip : Form
    {
        private const string moduleTag = "FrmImportTrip";

        private List<long> tripIDs;

        private DataLayer db = DataLayer.instance;

        public FrmImportTrip()
        {
            InitializeComponent();
        }

        public FrmImportTrip(List<long> tripIDs)
        {
            try
            {
                InitializeComponent();
                this.tripIDs = tripIDs;
            }
            catch (Exception ex)
            {
                Util.LogException(moduleTag, ex);
            }
        }

        private void FrmImportTrip_Load(object sender, EventArgs e)
        {
            try
            {
                List<DataLayer_Trip> trips = new List<DataLayer_Trip>();

                db.GetTrips(trips);

                foreach (DataLayer_Trip trip in trips)
                {
                    if (!tripIDs.Contains(trip.id))
                    {
                        ListViewItem item = new ListViewItem();
                        item.Tag = trip.id;
                        item.Text = trip.id.ToString();
                        item.SubItems.Add(trip.user_id.ToString());
                        item.SubItems.Add(trip.client_trip_id.ToString());
                        item.SubItems.Add(trip.n_coord.ToString());
                        lvTrips.Items.Add(item);
                    }
                }

            }
            catch(Exception ex)
            {
                Util.LogException(moduleTag, ex);
            }
            finally
            {
                btnOK.Enabled = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                tripIDs.Clear();
                foreach (ListViewItem item in lvTrips.Items)
                {
                    if (item.Checked)
                    {
                        tripIDs.Add((long)item.Tag);
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Util.LogException(moduleTag, ex);
            }
            finally
            {
                this.Hide();
            }
        }

        private void lvTrips_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            CheckOKEnabled();
        }

        private void CheckOKEnabled()
        {
            foreach (ListViewItem item in lvTrips.Items)
            {
                if (item.Checked)
                {
                    btnOK.Enabled = true;
                    return;
                }
            }
            btnOK.Enabled = false;
        }
    }
}
