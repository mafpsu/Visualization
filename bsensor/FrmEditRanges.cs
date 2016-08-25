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
    public partial class FrmEditColorRanges : Form
    {
        private GraphColorRange[] graphColorRanges = null;
        private string dataSetName;

        public FrmEditColorRanges(string dataSetName, GraphColorRange[] graphColorRanges)
        {
            InitializeComponent();

            this.dataSetName = dataSetName;

            if (null != graphColorRanges)
            {
                // Make a copy of the current graph Color Ranges
                this.graphColorRanges = new GraphColorRange[graphColorRanges.Count<GraphColorRange>()];
                graphColorRanges.CopyTo(this.graphColorRanges, 0);
            }
        }

        private void btnEnterRanges_Click(object sender, EventArgs e)
        {
            try
            {
                FrmEnterRanges dlg = new FrmEnterRanges();
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    graphColorRanges = dlg.GraphColorRanges;
                    DisplayGraphColorRanges();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void DisplayGraphColorRanges()
        {
            if (null != graphColorRanges)
            {
                lvGraphColorRanges.Items.Clear();
                foreach (GraphColorRange gcr in graphColorRanges)
                {
                    ListViewItem.ListViewSubItem subItemMax = new ListViewItem.ListViewSubItem();
                    subItemMax.Text = gcr.Max.ToString();

                    ListViewItem.ListViewSubItem subItemColor = new ListViewItem.ListViewSubItem();
                    subItemColor.Text = Util.ToRGB(gcr.Color);
                    subItemColor.ForeColor = Color.White;
                    subItemColor.BackColor = gcr.Color;

                    ListViewItem item = new ListViewItem(gcr.Min.ToString());
                    item.Tag = gcr;
                    item.UseItemStyleForSubItems = false;

                    item.SubItems.Add(subItemMax);
                    item.SubItems.Add(subItemColor);
                    lvGraphColorRanges.Items.Add(item);
                }
            }
        }

        public GraphColorRange[] GraphColorRanges
        {
            get { return graphColorRanges; }
        }

        private void lvGraphColorRanges_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                btnEditColor.Enabled = (lvGraphColorRanges.SelectedItems.Count > 0);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void btnEditColor_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem lv = lvGraphColorRanges.SelectedItems[0];
                colorDialog1.Color = lv.SubItems[2].BackColor;
                if (DialogResult.OK == colorDialog1.ShowDialog())
                {
                    lv.SubItems[2].BackColor = colorDialog1.Color;
                    lv.SubItems[2].Text = Util.ToRGB(colorDialog1.Color);
                    lvGraphColorRanges.Invalidate();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            GraphColorRange gcr;

            try
            {
                foreach (ListViewItem lv in lvGraphColorRanges.Items)
                {
                    if (null != (gcr = lv.Tag as GraphColorRange))
                    {
                        gcr.Color = lv.SubItems[2].BackColor;
                    }
                }
            this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                LogException(ex);
                this.DialogResult = DialogResult.Cancel;
            }
            finally
            {
                this.Close();
            }
        }

        private void FrmEditColorRanges_Load(object sender, EventArgs e)
        {
            try {
                this.Text = "Edit Color Ranges for " + dataSetName;
                DisplayGraphColorRanges();
            }
            catch(Exception ex)
            {
                LogException(ex);
            }
        }

        private void LogException(Exception ex)
        {
            MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
