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
    public partial class istatistik : Form
    {
        public static SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-TU9KLQF\\ABC;Initial Catalog=otel_otomasyon_database;Integrated Security=True");
        DataSet ds = new DataSet();
        SqlDataReader dr;
        int odas, bakimliodas, personels, yetkilis;
        public static int kats;
        public istatistik()
        {
            InitializeComponent();
        }

        void odaCek()
        {
            baglanti.Open();
            string odaSorgu = "select kattakiOdaAdi from Odalar";
            SqlCommand cmd = new SqlCommand(odaSorgu, baglanti);
            dr = cmd.ExecuteReader();
            kats = 1;
            while (dr.Read())
            {
                odas++;
            }
            lbOdaSayisi.Text = odas.ToString();
            cmd.Dispose();
            baglanti.Close();
        }

        void katCek()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            string katSorgu = "select katAdi from Odalar";
            SqlCommand cmd = new SqlCommand(katSorgu, baglanti);
            dr = cmd.ExecuteReader();
            int kats = 0;
            while (dr.Read())
            {
                if (int.Parse(dr["katAdi"].ToString()) > kats)
                {
                    kats = int.Parse(dr["katAdi"].ToString());
                }
            }
            lbKatSayisi.Text = kats.ToString();
            cmd.Dispose();
            baglanti.Close();
        }

        void bakimdakiler()
        {
            baglanti.Open();
            string bakimSorgu = "select odaKullanilabilirMi from Odalar";
            SqlCommand cmd = new SqlCommand(bakimSorgu, baglanti);
            bakimliodas = 0;
            while (dr.Read())
            {
                if (dr["odaKullanilabilirMi"].ToString() == "0")
                {
                   // bakimliodas++;
                }
                //bakimliodas++;
            }
            lbBakimliOda.Text = bakimliodas.ToString();

            cmd.Dispose();
            baglanti.Close();
        }

        void personel()
        {
            baglanti.Open();
            string personelSorgu = "select personelTC from personelBilgileri";
            SqlCommand cmd = new SqlCommand(personelSorgu, baglanti);
            dr = cmd.ExecuteReader();
            personels = 0;
            while (dr.Read())
            {
                personels++;
            }
            lbPersonelSayisi.Text = personels.ToString();
            cmd.Dispose();
            baglanti.Close();
        }

        void yetkililer()
        {
            baglanti.Open();
            string yetkiliSorgu = "select yetkiliMi from personelBilgileri";
            SqlCommand cmd = new SqlCommand(yetkiliSorgu, baglanti);
            dr = cmd.ExecuteReader();
            yetkilis = 0;
            while (dr.Read())
            {
                if (dr["yetkiliMi"].ToString() == "1")
                {
                    yetkilis++;
                }
            }
            lbYetkiliPersonel.Text = yetkilis.ToString();
            cmd.Dispose();
            baglanti.Close();
        }

        private void istatistik_Load(object sender, EventArgs e)
        {
            odaCek();
            katCek();
            bakimdakiler();
            personel();
            yetkililer();
        }
    }
}
