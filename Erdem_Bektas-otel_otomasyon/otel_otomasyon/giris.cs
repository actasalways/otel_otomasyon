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
    public partial class giris : Form
    {
        public static SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-TU9KLQF\\ABC;Initial Catalog=otel_otomasyon_database;Integrated Security=True");
        DataSet ds = new DataSet();
        SqlDataReader dr;
        BindingSource bs = new BindingSource();
        public static bool yetkiliMi = true;

        public giris()
        {
            InitializeComponent();
        }

        private void btnYeniUye_Click(object sender, EventArgs e)
        {

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            if (tbKullaniciAdi.Text == "" || tbSifre.Text == "")
            {
                MessageBox.Show("Lütfen boş geçmeyin, yazık", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sorgu = "select personelKullaniciAdi,personelSifre,yetkiliMi from personelBilgileri where personelKullaniciAdi='" + tbKullaniciAdi.Text + "' and personelSifre='" + tbSifre.Text + "'";
                SqlCommand cmd = new SqlCommand(sorgu, baglanti);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    anaPanel f2 = new anaPanel();
                    f2.Show();
                    this.Hide();
                    this.Visible = false;
                    //   this.Dispose();
                    if (dr["yetkiliMi"].ToString()=="1")
                    {
                        yetkiliMi = true;
                    }
                    else
                    {
                        yetkiliMi = false;
                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı!\r\rÖzenerek gir lütfen", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                cmd.Dispose();
            }
            baglanti.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            DialogResult emin_misin = MessageBox.Show("Kapatmak istediğine emin misiniz\r\rBidaha düşün bee", "Başlık Maşlık", MessageBoxButtons.YesNo);
            if (emin_misin == DialogResult.Yes)
            {
                Application.Exit();
            }
            else { }
        }
    }
}
