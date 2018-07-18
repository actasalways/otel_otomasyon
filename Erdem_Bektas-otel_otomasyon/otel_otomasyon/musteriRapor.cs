using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace otel_otomasyon
{
    public partial class musteriRapor : Form
    {
        public musteriRapor()
        {
            InitializeComponent();
        }

        private void musteriRapor_Load(object sender, EventArgs e)
        {
            this.personelBilgileriTableAdapter.Fill(this.otel_otomasyon_databaseDataSet1.personelBilgileri);
            this.musteriBilgileriTableAdapter.Fill(this.DataSet1.musteriBilgileri);
            this.reportViewer1.RefreshReport();
        }


        private void btnFID_Click(object sender, EventArgs e)
        {
            try
            {
                this.musteriBilgileriTableAdapter.FillBy1(this.DataSet1.musteriBilgileri, ((int)(System.Convert.ChangeType(tbID.Text, typeof(int)))));
                this.reportViewer1.RefreshReport();
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            tbTC.Text = "";
        }

        private void btnFTC_Click(object sender, EventArgs e)
        {
            try
            {
                this.musteriBilgileriTableAdapter.FillBy3(this.DataSet1.musteriBilgileri, tbTC.Text);
                this.reportViewer1.RefreshReport();
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            tbID.Text = "";
        }
    }
}
