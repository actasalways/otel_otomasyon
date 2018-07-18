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
using System.Globalization;

namespace otel_otomasyon
{
    public partial class odamoda : Form
    {
        public static SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-TU9KLQF\\ABC;Initial Catalog=otel_otomasyon_database;Integrated Security=True");
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        BindingSource bs = new BindingSource();
        SqlDataReader dr;
        SqlDataReader dr2;
        int toplamUcret, odaID, kats, odas = 0, kalan;
        int yeniID;
        public static bool yeniEklenenVarMi = false;
        int[] odalar = new int[100];
        bool[] odaSeciliMi = new bool[100];
        int[] hangiOdaSecili = new int[2];








        public odamoda()
        {
            InitializeComponent();
        }

        bool IsNumeric(string text)
        {
            foreach (char chr in text)
            {
                if (!Char.IsNumber(chr)) return false;
            }
            return true;
        }

        void odalariGoster()
        {
            panel1.Controls.Clear();
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            string goster = "select odaID,kattakiOdaAdi,odaKacKisilik,odadaKacKisiVar from Odalar where katAdi='" + geciciKat.Text + "'";
            SqlCommand cmd = new SqlCommand(goster, baglanti);
            dr = cmd.ExecuteReader();
            int i = 0;
            int ust = 40;
            int kactane = 0;
            while (dr.Read())
            {
                Button b = new Button();
                b.AutoSize = false;
                b.Width = 60;
                b.Top = ust;
                if (i % 5 == 0)//kattaki düzenin ayarlanması
                {
                    ust += 40;
                    kactane = 0;
                }
                if (int.Parse(dr["odaKacKisilik"].ToString()) - int.Parse(dr["odadaKacKisiVar"].ToString()) == 0)
                {
                    b.Enabled = false;
                }
                else
                {
                    b.Enabled = true;
                }
                b.Left = kactane * 120;
                b.Name = dr["odaID"].ToString();
                b.Text = dr["kattakiOdaAdi"].ToString();
                b.BackColor = Color.CadetBlue;
                b.TabStop = false;
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 0;
                panel1.Controls.Add(b);
                b.Click += new EventHandler(buton_islem);
                kactane++;
                i++;
            }
            dr.Dispose();
            cmd.Dispose();
            baglanti.Close();
        }

        void kisiSec()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from musteriBilgileri where musteriOdadaMi='1' and musteriOdaID='" + odaID + "'", baglanti);
            da.Fill(dt);
            btnKisiSec.DataBindings.Clear();
            btnKisiSec.ValueMember = "musteriID";
            btnKisiSec.DisplayMember = "musteriAd";
            btnKisiSec.DataSource = dt;

        }

        string odaNoBul()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlCommand cmd = new SqlCommand("select kattakiOdaAdi from Odalar where odaID=@odaID", baglanti);
            cmd.Parameters.AddWithValue("@odaID", odaID);
            string odaNosu = cmd.ExecuteScalar().ToString();
            baglanti.Close();
            return odaNosu;
        }

        private void buton_islem(object sender, EventArgs e)
        {
            butonMutonTemizle();
            Button btn = (Button)sender;
            lblOdaAdi.Text = btn.Text;
            odaID = int.Parse(btn.Name);
            if (odaSeciliMi[odaID])
            {
                odaSeciliMi[odaID] = false;
                btn.BackColor = Color.CadetBlue;
            }
            else
            {
                odaSeciliMi[odaID] = true;
                btn.BackColor = Color.Green;
            }
            kisiSec();

            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from musteriBilgileri where musteriOdadaMi='1' and musteriOdaID='" + odaID + "'", baglanti);
            da.Fill(dt);
            tbOdaNo.Text = lblOdaAdi.Text;
            lblOdaID.Text = odaID.ToString();
            if (dt.Rows.Count > 0)//odada biri varken
            {
                lblID.Text = dt.Rows[0]["musteriID"].ToString();
                tbMtc.Text = dt.Rows[0]["musteriTC"].ToString();
                tbMad.Text = dt.Rows[0]["musteriAd"].ToString();
                tbMsoyad.Text = dt.Rows[0]["musteriSoyad"].ToString();
                mustDtarihi.Value = Convert.ToDateTime(dt.Rows[0]["musteriDtarihi"].ToString());
                tbMtel.Text = dt.Rows[0]["musteriCepTel"].ToString();
                tbMeposta.Text = dt.Rows[0]["musteriEposta"].ToString();
                tbMadres.Text = dt.Rows[0]["musteriEvAdresi"].ToString();
                lblCinsiyet.Text = dt.Rows[0]["musteriCinsiyet"].ToString();
                if (lblCinsiyet.Text == "Erkek") { rbMerk.Checked = true; }
                else { rbMkadin.Checked = true; }
                dtpMgiris.Value = Convert.ToDateTime(dt.Rows[0]["musteriOdaGiris"].ToString());
                dtpMcikis.Value = Convert.ToDateTime(dt.Rows[0]["musteriOdaCikis"].ToString());
                tbUcret.Text = dt.Rows[0]["musteriUcret"].ToString();
                lblOdendiMi.Text = dt.Rows[0]["odemeAlındiMi"].ToString();
                if (lblOdendiMi.Text == "1") { odendiMi.Checked = true; }
                else { odendiMi.Checked = false; }
            }
            baglanti.Close();
            odadaKacKisiVar();
        }

        void odadaKacKisiVar()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            tbOdaDurum.Text = "";
            SqlCommand cmd123 = new SqlCommand("select odadaKacKisiVar from Odalar where odaID=@odaID", baglanti);
            cmd123.Parameters.AddWithValue("@odaID", lblOdaID.Text);

            int kacVar = int.Parse(cmd123.ExecuteScalar().ToString());
            SqlCommand cmd012 = new SqlCommand("select odaKacKisilik from Odalar where odaID=@odaID", baglanti);
            cmd012.Parameters.AddWithValue("@odaID", lblOdaID.Text);

            int kacKisilik = int.Parse(cmd012.ExecuteScalar().ToString());
            lblodaKacKisilik.Text = kacKisilik.ToString();
            lblodadaKacKisiVar.Text = kacVar.ToString();
            kalan = kacKisilik - kacVar;
            if (kalan == 0)
            {
                tbOdaDurum.Text = "Oda dolu lütfen Başka bir oda seçiniz.";
            }
            else if (kalan == kacKisilik)
            {
                tbOdaDurum.Text = "Oda Boş";
            }
            else if (kalan > 0)
            {
                tbOdaDurum.Text = "Odada yer var";
                lblKacEklenebilir.Text = kalan.ToString();
            }
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
            dr.Close();
            for (int i = 0; i < kats; i++)
            {
                cbKatBilgisi.Items.Add(i + 1);
            }
            cbKatBilgisi.SelectedIndex = 0;
            cmd.Dispose();
            baglanti.Close();
        }

        private void odamoda_Load(object sender, EventArgs e)
        {
            katCek();
            odalariGoster();
        }

        private void cbKatBilgisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            geciciKat.Text = cbKatBilgisi.Text;
            odalariGoster();
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            if (lblOdaAdi.Text != "")
            {
                butonMutonTemizle();
                tbUcret.Enabled = true;
            }
            else
            {
                MessageBox.Show("Lütfen önce oda seçiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void butonMutonTemizle()
        {
            tbMad.Text = ""; tbMadres.Text = ""; tbMeposta.Text = ""; tbMsoyad.Text = ""; tbMtc.Text = ""; tbMtel.Text = "";
            //    tbOdaNo.Text = lblOdaAdi.Text;
            mustDtarihi.Value = DateTime.Today; rbMerk.Checked = false; rbMkadin.Checked = false;
            dtpMcikis.Value = DateTime.Today; dtpMgiris.Value = DateTime.Today; odendiMi.Checked = false;
            lblID.Text = ""; btnKisiSec.Text = "";
        }

        void IDbelirle()
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select MAX(musteriID) from musteriBilgileri", baglanti);
            yeniID = int.Parse(cmd.ExecuteScalar().ToString());
            yeniID++;
            baglanti.Close();
        }

        private void btnYeniKayit_Click(object sender, EventArgs e)
        {
            if (tbMad.Text.Length < 2 || tbMsoyad.Text.Length < 2 || tbMtc.Text.Length < 11 || tbMtel.Text.Length < 10)
            {
                MessageBox.Show("Ad Soyad Bilgisi boş girilemez.\r\rTC Minimum 11 Karakterden Oluşabilir.\r\rTelefon Minimum 11 Karakter rakamdan oluşabilir.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!IsNumeric(tbMtc.Text) || !IsNumeric(tbMtel.Text))
                {
                    MessageBox.Show("TC sadece rakamlardan oluşabilir. \r\rTelefon numarası sadece rakamlardan oluşabilir.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                DateTime giris = dtpMgiris.Value;
                DateTime cikis = dtpMcikis.Value;
                TimeSpan fark = cikis - giris;
                if (fark.Days < 0)
                {
                    MessageBox.Show("Giriş Tarihiniz Çıkış Tarihinizden Büyük Olamaz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (fark.Days == 0)
                {
                    MessageBox.Show("Giriş Tarihiniz Çıkış Tarihiniz ile Aynı Olamaz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (int.Parse(tbUcret.Text.ToString()) < 0)
                {
                    MessageBox.Show("Giriş-Çıkış Tarihlerini Kontrol Ediniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!odendiMi.Checked)//ödeme alındıysa
                {
                    try
                    {
                        toplamUcret = int.Parse(tbUcret.Text);
                        if (tbUcret.Text == "")
                        {
                            toplamUcret = 0;
                        }
                    }
                    catch
                    {
                        if (!IsNumeric(tbUcret.Text))
                        {
                            MessageBox.Show("Borç Sayı olmalıdır.");
                        }
                    }
                }
                /************************************************************************** KONTROL İŞLEMLERİ SON *****************************************/
                /*********************************************************** EKLEME İŞLEMİ*****************************************************************/
                IDbelirle();

                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText = @"insert into musteriBilgileri (musteriID,musteriTC,musteriOdaID,musteriAd,musteriSoyad,musteriDtarihi,musteriCinsiyet,
                                musteriCepTel,musteriEposta,musteriEvAdresi,musteriOdaGiris,musteriOdaCikis,musteriUcret,musteriOdadaMi) values (@musteriID,@musteriTC,@musteriOdaID,
                                @musteriAd,@musteriSoyad,@musteriDtarihi,@musteriCinsiyet,@musteriCepTel,@musteriEposta,@musteriEvAdresi,@musteriOdaGiris,@musteriOdaCikis,@musteriUcret,@musteriOdadaMi)";
                cmd.Parameters.AddWithValue("@musteriID", yeniID.ToString());
                cmd.Parameters.AddWithValue("@musteriTC", tbMtc.Text);
                cmd.Parameters.AddWithValue("@musteriOdaID", odaID.ToString());
                cmd.Parameters.AddWithValue("@musteriAd", tbMad.Text);
                cmd.Parameters.AddWithValue("@musteriSoyad", tbMsoyad.Text);
                cmd.Parameters.AddWithValue("@musteriDtarihi", mustDtarihi.Value);
                if (rbMerk.Checked) { cmd.Parameters.AddWithValue("@musteriCinsiyet", rbMerk.Text); }
                else { cmd.Parameters.AddWithValue("@musteriCinsiyet", rbMkadin.Text); }
                cmd.Parameters.AddWithValue("@musteriCepTel", tbMtel.Text);
                cmd.Parameters.AddWithValue("@musteriEposta", tbMeposta.Text);
                cmd.Parameters.AddWithValue("@musteriEvAdresi", tbMeposta.Text);
                cmd.Parameters.AddWithValue("@musteriOdaGiris", dtpMgiris.Value);
                cmd.Parameters.AddWithValue("@musteriOdaCikis", dtpMcikis.Value);
                cmd.Parameters.AddWithValue("@musteriUcret", tbUcret.Text);
                if (odendiMi.Checked) { cmd.Parameters.AddWithValue("@odemeAlındiMi", "1".ToString()); }
                else { cmd.Parameters.AddWithValue("@odemeAlındiMi", "0".ToString()); }
                cmd.Parameters.AddWithValue("@musteriOdadaMi", "1".ToString());
                cmd.ExecuteNonQuery();
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand cmdkacVar = new SqlCommand("select odadaKacKisiVar from Odalar where odaID=@odaID", baglanti);
                cmdkacVar.Parameters.AddWithValue("@odaID", lblOdaID.Text);
                int odadakilerinSayisi = int.Parse(cmdkacVar.ExecuteScalar().ToString());
                odadakilerinSayisi++;
                SqlCommand cmdupd = new SqlCommand("update Odalar set odadaKacKisiVar=@odadaKacKisiVar where odaID=@odaID", baglanti);
                cmdupd.Parameters.AddWithValue("@odadaKacKisiVar", odadakilerinSayisi.ToString());
                cmdupd.Parameters.AddWithValue("@odaID", lblOdaID.Text);
                cmdupd.ExecuteNonQuery();
                MessageBox.Show("işlem tamamlandı.");
                odadaKacKisiVar();
                MessageBox.Show("Müşteri Kaydetme İşlemi Başarılı", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                kisiSec();
                int a = int.Parse(lblodadaKacKisiVar.Text.ToString());
                a++;
                a = int.Parse(lblKacEklenebilir.Text.ToString());
                if (a > 0)
                {
                    a--;
                }
                lblKacEklenebilir.Text = a.ToString();
                baglanti.Close();
                tbUcret.Enabled = false;
                butonMutonTemizle();
                Application.Restart();
            }
        }

        private void kisisec_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = btnKisiSec.SelectedIndex;
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from musteriBilgileri where musteriOdaID='" + odaID + "'", baglanti);
            da.Fill(dt);
            btnKisiSec.DataBindings.Clear();
            btnKisiSec.ValueMember = "musteriID";
            btnKisiSec.DisplayMember = "musteriAd";
            btnKisiSec.DataSource = dt;
            lblID.Text = dt.Rows[index]["musteriID"].ToString();
            tbMtc.Text = dt.Rows[index]["musteriTC"].ToString();//index. -> satır
            tbMad.Text = dt.Rows[index]["musteriAd"].ToString();
            tbMsoyad.Text = dt.Rows[index]["musteriSoyad"].ToString();
            mustDtarihi.Value = Convert.ToDateTime(dt.Rows[index]["musteriDtarihi"].ToString());
            tbMtel.Text = dt.Rows[index]["musteriCepTel"].ToString();
            tbMeposta.Text = dt.Rows[index]["musteriEposta"].ToString();
            tbMadres.Text = dt.Rows[index]["musteriEvAdresi"].ToString();
            lblCinsiyet.Text = dt.Rows[index]["musteriCinsiyet"].ToString();
            if (lblCinsiyet.Text == "Erkek") { rbMerk.Checked = true; }
            else { rbMkadin.Checked = true; }
            dtpMgiris.Value = Convert.ToDateTime(dt.Rows[index]["musteriOdaGiris"].ToString());
            dtpMcikis.Value = Convert.ToDateTime(dt.Rows[index]["musteriOdaCikis"].ToString());
            tbUcret.Text = dt.Rows[index]["musteriUcret"].ToString();
            lblOdendiMi.Text = dt.Rows[index]["odemeAlındiMi"].ToString();
            if (lblOdendiMi.Text == "1") { odendiMi.Checked = true; }
            else { odendiMi.Checked = false; }
            baglanti.Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (tbMad.Text.Length < 5 || tbMsoyad.Text.Length < 5 || tbMtc.Text.Length < 11 || tbMtel.Text.Length < 11)
            {
                MessageBox.Show("Ad Soyad Bilgisi boş girilemez.\r\rTC Minimum 11 Karakterden Oluşabilir.\r\rTelefon Minimum 11 Karakter rakamdan oluşabilir.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!IsNumeric(tbMtc.Text) || !IsNumeric(tbMtel.Text))
                {
                    MessageBox.Show("TC sadece rakamlardan oluşabilir. \r\rTelefon numarası sadece rakamlardan oluşabilir.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                DateTime giris = dtpMgiris.Value;
                DateTime cikis = dtpMcikis.Value;
                TimeSpan fark = cikis - giris;
                if (fark.Days < 0)
                {
                    MessageBox.Show("Giriş Tarihiniz Çıkış Tarihinizden Büyük Olamaz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (fark.Days == 0)
                {
                    MessageBox.Show("Giriş Tarihiniz Çıkış Tarihiniz ile Aynı Olamaz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!odendiMi.Checked)//ödeme alındıysa
                {
                    try
                    {
                        toplamUcret = int.Parse(tbUcret.Text);
                        if (tbUcret.Text == "")
                        {
                            toplamUcret = 0;
                        }
                    }
                    catch
                    {
                        if (!IsNumeric(tbUcret.Text))
                        {
                            MessageBox.Show("Borç Sayı olmalıdır.");
                        }
                    }
                }
                /*********************************************************** KONTROL İŞLEMLERİ SON **************************************************/
                /*********************************************************** EKLEME İŞLEMİ **********************************************************/

                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                DialogResult cevap = MessageBox.Show("Güncelleme İstediğinize Emin Misiniz?", "Uyarı", MessageBoxButtons.YesNo);
                if (cevap == DialogResult.Yes)
                {
                    baglanti.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = baglanti;
                    cmd.CommandText = @"update musteriBilgileri set musteriTC=@musteriTC, musteriAd=@musteriAd,musteriSoyad=@musteriSoyad,
                                 musteriDtarihi=@musteriDtarihi,musteriCinsiyet=@musteriCinsiyet,musteriCepTel=@musteriCepTel,
                                 musteriEposta=@musteriEposta,musteriEvAdresi=@musteriEvAdresi,
                                 musteriOdaGiris=@musteriOdaGiris,musteriOdaCikis=@musteriOdaCikis,musteriUcret=@musteriUcret,odemeAlındiMi=@odemeAlındiMi
                                 where musteriID=@musteriID";
                    cmd.Parameters.AddWithValue("@musteriID", lblID.Text);
                    cmd.Parameters.AddWithValue("@musteriAd", tbMad.Text);
                    cmd.Parameters.AddWithValue("@musteriSoyad", tbMsoyad.Text);
                    cmd.Parameters.AddWithValue("@musteriTC", tbMtc.Text);
                    cmd.Parameters.AddWithValue("@musteriDtarihi", mustDtarihi.Value);
                    cmd.Parameters.AddWithValue("@musteriCepTel", tbMtel.Text);
                    if (rbMerk.Checked) { cmd.Parameters.AddWithValue("@musteriCinsiyet", rbMerk.Text); }
                    else { cmd.Parameters.AddWithValue("@musteriCinsiyet", rbMkadin.Text); }
                    cmd.Parameters.AddWithValue("@musteriEposta", tbMeposta.Text);
                    cmd.Parameters.AddWithValue("@musteriEvAdresi", tbMadres.Text);
                    cmd.Parameters.AddWithValue("@musteriOdaCikis", dtpMcikis.Value);
                    cmd.Parameters.AddWithValue("@musteriOdaGiris", dtpMgiris.Value);
                    cmd.Parameters.AddWithValue("@musteriUcret", tbUcret.Text);
                    if (odendiMi.Checked) { cmd.Parameters.AddWithValue("@odemeAlındiMi", "1".ToString()); }
                    else { cmd.Parameters.AddWithValue("@odemeAlındiMi", "0".ToString()); }
                    cmd.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Bilgileri Güncelleme İşlemi Başarılı", "Güncelleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void tbMtc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsNumeric(tbMtc.Text))
                {
                    if (tbMtc.Text.Length >= 6)
                    {
                        if (baglanti.State == ConnectionState.Closed)
                        {
                            baglanti.Open();
                        }
                        SqlCommand cmd = new SqlCommand("select * from musteriBilgileri where musteriTC like '%" + tbMtc.Text + "%'", baglanti);
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            lblID.Text = dr["musteriID"].ToString();
                            tbMad.Text = dr["musteriAd"].ToString();
                            tbMsoyad.Text = dr["musteriSoyad"].ToString();
                            mustDtarihi.Value = Convert.ToDateTime(dr["musteriDtarihi"].ToString());
                            tbMtel.Text = dr["musteriCepTel"].ToString();
                            tbMeposta.Text = dr["musteriEposta"].ToString();
                            tbMadres.Text = dr["musteriEvAdresi"].ToString();
                            lblCinsiyet.Text = dr["musteriCinsiyet"].ToString();
                            if (lblCinsiyet.Text == "Kadın") { rbMkadin.Checked = true; }
                            else { rbMerk.Checked = true; }
                            lblOdaID.Text = dr["musteriOdaID"].ToString();
                            tbUcret.Text = dr["musteriUcret"].ToString();
                            dtpMgiris.Value = DateTime.Today;
                            dtpMcikis.Value = DateTime.Today;
                        }
                        dr.Close();
                        cmd.Dispose();
                        SqlCommand cemede = new SqlCommand("select kattakiOdaAdi from Odalar where odaID=@odaID", baglanti);
                        cemede.Parameters.AddWithValue("@odaID", lblOdaID.Text);
                        lblOdaAdi.Text = cemede.ExecuteScalar().ToString();
                        lblOdaAdi.Text = tbOdaNo.Text;
                        odadaKacKisiVar();
                        baglanti.Close();
                    }
                }
                else
                {
                    tbMtc.Text = "";
                    MessageBox.Show("Lütfen rakam giriniz:", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Kişi Kayıtlı Değil\nLütfen Yeni Kayıt Yapınız.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dtpMcikis_ValueChanged(object sender, EventArgs e)
        {
            if (lblOdaID.Text != "" && lblOdaAdi.Text != "")
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand cmd = new SqlCommand("select odaGunlukUcret from Odalar where odaID=@odaID", baglanti);
                cmd.Parameters.AddWithValue("@odaID", lblOdaID.Text);
                int gunlukUcret = int.Parse(cmd.ExecuteScalar().ToString());
                TimeSpan fark = dtpMcikis.Value - dtpMgiris.Value;
                tbUcret.Text = (gunlukUcret * int.Parse(fark.Days.ToString())).ToString();
                baglanti.Close();
            }
        }



    }
}