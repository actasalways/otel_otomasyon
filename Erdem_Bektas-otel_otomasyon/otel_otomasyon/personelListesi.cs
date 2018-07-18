using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace otel_otomasyon
{
    public partial class personelListesi : Form
    {
        public static SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-TU9KLQF\\ABC;Initial Catalog=otel_otomasyon_database;Integrated Security=True");
        DataSet ds = new DataSet();
        public personelListesi()
        {
            InitializeComponent();
        }

        private void personelListesi_Load(object sender, EventArgs e)
        {
            this.personelBilgileriTableAdapter.Fill(this.otel_otomasyon_databaseDataSet1.personelBilgileri);
            this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
