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
using Microsoft.VisualBasic;

namespace otel_otomasyon
{
    public partial class anaPanel : Form
    {
        public static SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-TU9KLQF\\ABC;Initial Catalog=otel_otomasyon_database;Integrated Security=True");
        BindingSource bs = new BindingSource();
        DataSet dsmusteri = new DataSet();
        DataSet dspersonel = new DataSet();
        BindingSource bs2 = new BindingSource();
        DataSet ds = new DataSet();
        SqlDataReader dr;
        SqlDataReader dr2;
        BindingSource bss2 = new BindingSource();
        DataTable dt = new DataTable();
        string[] personelbilgileri = new string[13];
        int b = -1, katno, kats;
        bool katVarMi = false;
        bool odaEklemekİcinKatVarMi = false;
        string[] TCler = new string[100];



        public anaPanel()
        {
            InitializeComponent();
        }

        void mustericek()// grid de müsterinin kaldığı oda no su yazmıyor eklenebilir.
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlDataAdapter damusteri = new SqlDataAdapter("select * from musteriBilgileri where musteriOdadaMi='1' and musteriOdadaMi='1'", baglanti);//müşteri tablosu
            dsmusteri.Clear();
            damusteri.Fill(dsmusteri);
            dataGridMusteri.DataSource = dsmusteri.Tables[0];
            baglanti.Close();
        }

        void personelCek()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlDataAdapter dapersonel = new SqlDataAdapter("select * from personelBilgileri", baglanti);//çalışanlar tablosu
            dspersonel.Clear();
            dapersonel.Fill(dspersonel);
            dataGridPersonel.DataSource = dspersonel.Tables[0];
            baglanti.Close();
        }

        void katCek()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlCommand cmd = new SqlCommand("select MAX(katAdi) from Odalar", baglanti);
            string katAdi = cmd.ExecuteScalar().ToString();
            cmd.Dispose();
            tbEklenecekKat.Text = (int.Parse(katAdi) + 1).ToString();
        }

        void yetkiliMi()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            BindingSource bss = new BindingSource();
            SqlDataAdapter da = new SqlDataAdapter("select personelID,unvan,yetkiliMi,personelSifre from personelBilgileri", baglanti);//personel tablosundan 
            DataSet ds = new DataSet();
            da.Fill(ds);
            bss.DataSource = ds.Tables[0];
            dataGridView3.DataSource = bss;
            tbPID.DataBindings.Clear();
            tbPUnvan.DataBindings.Clear();
            lblYetki.DataBindings.Clear();
            tbPID.DataBindings.Add("Text", bss, "personelID");
            tbPUnvan.DataBindings.Add("Text", bss, "unvan");
            lblYetki.DataBindings.Add("Text", bss, "yetkiliMi");
            if (lblYetki.Text == "1") { rbPyetkili.Checked = true; }
            else { rbPyetkisiz.Checked = true; }
        }

        void odalarCek()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            DataSet dss = new DataSet();
            SqlDataAdapter daa = new SqlDataAdapter("select odaID,katAdi,kattakiOdaAdi,odaKullanilabilirMi,odaGunlukUcret,odaKacKisilik from Odalar", baglanti);
            daa.Fill(dss);
            bss2.DataSource = dss.Tables[0];
            dataGridView4.DataSource = bss2;
            tbOdaID.DataBindings.Clear();
            tbOdaNo.DataBindings.Clear();
            tbOdaID.DataBindings.Add("Text", bss2, "odaID");
            tbOdaNo.DataBindings.Add("Text", bss2, "kattakiOdaAdi");
            baglanti.Close();
        }

        bool IsNumeric(string text)
        {
            foreach (char chr in text)
            {
                if (!Char.IsNumber(chr)) return false;
            }
            return true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter dapersonel = new SqlDataAdapter("select * from personelBilgileri", baglanti);//personel tablosu
            dspersonel.Clear();
            dapersonel.Fill(dspersonel);
            bs.Clear();
            bs.DataSource = dspersonel.Tables[0];
            dataGridPersonel.DataSource = bs;
            tbxpad.DataBindings.Add("Text", bs, "personelAd");
            lblpersonelID.DataBindings.Add("Text", bs, "personelID");
            tbxpsifre.DataBindings.Add("Text", bs, "personelSifre");
            tbxptc.DataBindings.Add("Text", bs, "personelTC");
            tbxpsoyad.DataBindings.Add("Text", bs, "personelSoyad");
            lblCinsiyetP.DataBindings.Clear();
            lblCinsiyetP.DataBindings.Add("Text", bs, "personelCinsiyet");
            if (lblCinsiyetP.Text == "Erkek") { rbtnerkek.Checked = true; }
            else { rbtnkadin.Checked = true; }

            tbxptel.DataBindings.Add("Text", bs, "personelTel");
            tbxpeposta.DataBindings.Add("Text", bs, "personelEposta");
            tbxpka.DataBindings.Add("Text", bs, "personelKullaniciAdi");
            tbxpadres.DataBindings.Add("Text", bs, "personelAdres");
            tbxpgorev.DataBindings.Add("Text", bs, "personelGorev");
            tbxmaas.DataBindings.Add("Text", bs, "personelMaas");
            cbxsigorta.DataBindings.Add("Text", bs, "personelSigorta");
            cbPunvan.DataBindings.Add("Text", bs, "unvan");
            baglanti.Close();
            baglanti.Open();

            SqlDataAdapter damusteri = new SqlDataAdapter("select * from musteriBilgileri where musteriOdadaMi='1'", baglanti);//müşteri tablosu
            damusteri.Fill(dsmusteri);
            dataGridMusteri.DataSource = dsmusteri.Tables[0];
            bs2.DataSource = dsmusteri.Tables[0];
            dataGridMusteri.DataSource = bs2;
            lblID.DataBindings.Add("Text", bs2, "musteriID");
            tbMtc.DataBindings.Add("Text", bs2, "musteriTC");
            tbMad.DataBindings.Add("Text", bs2, "musteriAd");
            tbMsoyad.DataBindings.Add("Text", bs2, "musteriSoyad");
            mustDtarihi.DataBindings.Add("Text", bs2, "musteriDtarihi");
            lbcinsiyet.DataBindings.Clear();
            lbcinsiyet.DataBindings.Add("Text", bs2, "musteriCinsiyet");
            if (lbcinsiyet.Text == "Erkek") { rbMerk.Checked = true; }
            else { rbMkadin.Checked = true; }

            lblOdeme.DataBindings.Add("Text", bs2, "odemeAlındiMi");
            if (lblOdeme.Text == "1") { odendiMi.Checked = true; }
            else { odendiMi.Checked = false; }

            tbMtel.DataBindings.Add("Text", bs2, "musteriCepTel");
            tbMeposta.DataBindings.Add("Text", bs2, "musteriEposta");
            tbMadres.DataBindings.Add("Text", bs2, "musteriEvAdresi");
            dtpMgiris.DataBindings.Add("Text", bs2, "musteriOdaGiris");
            dtpMcikis.DataBindings.Add("Text", bs2, "musteriOdaCikis");
            tbMtutar.DataBindings.Add("Text", bs2, "musteriUcret");
            lblOdadakiler.DataBindings.Add("Text", bs2, "musteriOdaID");
            this.dataGridMusteri.Columns["odemeAlındiMi"].Visible = false;//ödeme alındı mı sütunu gizli
            this.dataGridMusteri.Columns["musteriEvAdresi"].Visible = false;
            this.dataGridMusteri.Columns["musteriDtarihi"].Visible = false;

            SqlDataAdapter dacbbox = new SqlDataAdapter("select * from Odalar where odaID='" + lblOdadakiler.Text + "'", baglanti);
            DataTable dtcb = new DataTable();
            dacbbox.Fill(dtcb);
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }

            SqlCommand cmdOdaAdi = new SqlCommand("select kattakiOdaAdi from Odalar where odaID=@odaID", baglanti);
            cmdOdaAdi.Parameters.AddWithValue("@odaID", lblOdadakiler.Text);
            tbOdaAdi.Text = cmdOdaAdi.ExecuteScalar().ToString();

            yetkiliMi();

            DataSet dss = new DataSet();
            BindingSource bss2 = new BindingSource();
            SqlDataAdapter daa = new SqlDataAdapter("select odaID,kattakiOdaAdi,kattakiOdaAdi,odaGunlukUcret,odaKacKisilik,odaKullanilabilirMi from Odalar", baglanti);
            daa.Fill(dss);
            bss2.DataSource = dss.Tables[0];
            dataGridView4.DataSource = bss2;
            tbOdaID.DataBindings.Add("Text", bss2, "odaID");
            tbOdaNo.DataBindings.Add("Text", bss2, "kattakiOdaAdi");
            //      tbOdaFiyat.DataBindings.Add("Text", bss2, "odaGunlukUcret");
            //      tbOdaKapasite.DataBindings.Add("Text", bss2, "odaKacKisilik");
            lblAktifPasif.DataBindings.Add("Text", bss2, "odaKullanilabilirMi");
            if (lblAktifPasif.Text == "1") { rbtnAktif.Checked = true; }
            else { rbtnBakim.Checked = true; }
            katCek();

            odaninEklenecegiKatlarinBelirlenmesi();
            tbOdaninEklenecegiKat.SelectedIndex = kats;

            dataGridMusteri.Columns["musteriOdadaMi"].Visible = false;
            dataGridMusteri.Columns["musteriID"].Visible = false;
            dataGridPersonel.Columns["personelAktifMi"].Visible = false;
            dataGridPersonel.Columns["personelID"].Visible = false;
            dataGridView3.Columns["personelID"].Visible = false;
            dataGridView4.Columns["odaID"].Visible = false;

            baglanti.Close();
        }

        private void btncikiss_Click(object sender, EventArgs e)
        {
            anaPanel f2 = new anaPanel();
            this.Hide();
            f2.ShowDialog();
        }

        private void txtAraAd_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from musteriBilgileri where musteriOdadaMi='1' and musteriAd like '" + txtAraAd.Text + "%'", baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds, "musteriBilgileri");
            dataGridMusteri.DataSource = ds.Tables["musteriBilgileri"];
            baglanti.Close();
            if (txtAraAd.Text == "")
            {
                txtAraTcKimlikNo.Enabled = true;
                txtAraSoyad.Enabled = true;
                txtAraMusteriNo.Enabled = true;
            }
            else
            {
                txtAraTcKimlikNo.Enabled = false;
                txtAraSoyad.Enabled = false;
                txtAraMusteriNo.Enabled = false;
            }
        }

        private void txtAraSoyad_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from musteriBilgileri where musteriOdadaMi='1' and musteriSoyad like '" + txtAraSoyad.Text + "%'", baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds, "musteriBilgileri");
            dataGridMusteri.DataSource = ds.Tables["musteriBilgileri"];
            baglanti.Close();
            if (txtAraSoyad.Text == "")
            {
                txtAraTcKimlikNo.Enabled = true;
                txtAraAd.Enabled = true;
                txtAraMusteriNo.Enabled = true;
            }
            else
            {
                txtAraTcKimlikNo.Enabled = false;
                txtAraAd.Enabled = false;
                txtAraMusteriNo.Enabled = false;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lblCinsiyetP.Text == "Erkek") { rbtnerkek.Checked = true; }
            else { rbtnkadin.Checked = true; }
        }

        private void btnpBilgileriDuzenle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = baglanti;
            cmd.CommandText = "update personelBilgileri set personelID=@personelID, personelTC=@personelTC,personelSifre=@personelSifre,personelAd=@personelAd,personelCinsiyet=@personelCinsiyet,personelTel=@personelTel,personelEposta=@personelEposta,personelAdres=@personelAdres,personelGorev=@personelGorev,personelMaas=@personelMaas,personelSigorta=@personelSigorta where  personelID=@personelID";

            cmd.Parameters.AddWithValue("@personelID", tbxpka.Text);
            cmd.Parameters.AddWithValue("@personelTC", tbxptc.Text);
            cmd.Parameters.AddWithValue("@personelSifre", tbxpsifre.Text);
            cmd.Parameters.AddWithValue("@personelAd", tbxpad.Text);
            cmd.Parameters.AddWithValue("@personelSoyad", tbxpsoyad.Text);
            cmd.Parameters.AddWithValue("@personelTel", tbxptel.Text);
            cmd.Parameters.AddWithValue("@personelEposta", tbxpeposta.Text);
            cmd.Parameters.AddWithValue("@personelAdres", tbxpadres.Text);
            cmd.Parameters.AddWithValue("@personelGorev", tbxpgorev.Text);
            cmd.Parameters.AddWithValue("@personelMaas", tbxmaas.Text);
            cmd.Parameters.AddWithValue("@personelSigorta", cbxsigorta.Text);
            if (rbtnkadin.Checked) { cmd.Parameters.AddWithValue("@personelCinsiyet", rbtnkadin.Text); }
            else { cmd.Parameters.AddWithValue("@personelCinsiyet", rbtnerkek.Text); }
            cmd.ExecuteNonQuery();
            personelCek();
            MessageBox.Show("Kayıt yapıldı.");
        }

        private void btnpSil_Click(object sender, EventArgs e)
        {
            DialogResult x = new DialogResult();
            x = MessageBox.Show("Silmek istediğinize emin misiniz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (x == DialogResult.Yes)//silinsin
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText = "delete from personelBilgileri where personelTC=@personelTC";
                cmd.Parameters.AddWithValue("@personelTC", tbxptc.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt Silindi");
                personelCek();
                baglanti.Close();
            }
            else
            {

            }
        }


        private void btntemizle_Click(object sender, EventArgs e)
        {
            tbMtc.Text = ""; tbMad.Text = ""; tbMsoyad.Text = ""; tbMtel.Text = ""; tbMadres.Text = ""; tbMeposta.Text = "";
            if (rbMkadin.Checked) { rbMkadin.Checked = false; }
            else if (rbMerk.Checked) { rbMerk.Checked = false; }
            DateTimePicker dateTimePicker1 = new DateTimePicker();
            Controls.AddRange(new Control[] { dateTimePicker1 });
            dtpMgiris.Value = dateTimePicker1.Value;
            dtpMcikis.Value = dateTimePicker1.Value;
            odamoda gdhjg = new odamoda();
            gdhjg.ShowDialog();
        }



        private void btnPyenikayit_Click(object sender, EventArgs e)
        {
            btnKaydet.Enabled = true;
            tbxpad.Text = ""; tbxptc.Text = ""; tbxpsoyad.Text = "";
            if (rbtnkadin.Checked) { rbtnkadin.Checked = false; }
            else if (rbtnerkek.Checked) { rbtnerkek.Checked = false; }
            tbxptel.Text = ""; tbxpeposta.Text = ""; tbxpka.Text = ""; tbxpsifre.Text = "";
            tbxpadres.Text = ""; tbxmaas.Text = ""; cbxsigorta.Text = ""; cbPunvan.Text = ""; tbxpgorev.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnguncelleiptal.Enabled = true;
            btnpBilgileriGuncelle.Enabled = true;
            btnguncelleiptal.Visible = true;
            personelbilgileri[0] = tbxptc.Text;//güncelleme iptal için verilerin korunması
            personelbilgileri[1] = tbxpad.Text;
            personelbilgileri[2] = tbxpsoyad.Text;
            if (rbtnkadin.Checked) { personelbilgileri[3] = "kadın"; }
            else { personelbilgileri[3] = "erkek"; }
            personelbilgileri[3] = tbxptel.Text;
            personelbilgileri[4] = tbxpeposta.Text;
            personelbilgileri[5] = tbxpka.Text;
            personelbilgileri[6] = tbxpsifre.Text;
            personelbilgileri[7] = tbxpadres.Text;
            personelbilgileri[8] = tbxpgorev.Text;
            personelbilgileri[9] = tbxmaas.Text;
            personelbilgileri[10] = cbxsigorta.Text;
            personelbilgileri[11] = cbPunvan.Text;
        }

        private void btnpBilgileriGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = baglanti;
            cmd.CommandText = @"update personelBilgileri set personelKullaniciAdi=@personelKullaniciAdi,personelTC=@personelTC,personelSifre=@personelSifre,
                                personelAd=@personelAd,personelSoyad=@personelSoyad,personelCinsiyet=@personelCinsiyet,personelTel=@personelTel,
                                personelEposta=@personelEposta,personelAdres=@personelAdres,personelGorev=@personelGorev,personelMaas=@personelMaas,personelSigorta=@personelSigorta where personelID=@personelID";
            cmd.Parameters.AddWithValue("@personelID", lblpersonelID.Text);
            cmd.Parameters.AddWithValue("@personelKullaniciAdi", tbxpka.Text);
            cmd.Parameters.AddWithValue("@personelTC", tbxptc.Text);
            cmd.Parameters.AddWithValue("@personelSifre", tbxpsifre.Text);
            cmd.Parameters.AddWithValue("@personelAd", tbxpad.Text);
            cmd.Parameters.AddWithValue("@personelSoyad", tbxpsoyad.Text);
            if (rbtnkadin.Checked) { cmd.Parameters.AddWithValue("@personelCinsiyet", rbtnkadin.Text); }
            else { cmd.Parameters.AddWithValue("@personelCinsiyet", rbtnerkek.Text); }
            cmd.Parameters.AddWithValue("@personelTel", tbxptel.Text);
            cmd.Parameters.AddWithValue("@personelEposta", tbxpeposta.Text);
            cmd.Parameters.AddWithValue("@personelAdres", tbxpadres.Text);
            cmd.Parameters.AddWithValue("@personelGorev", tbxpgorev.Text);
            cmd.Parameters.AddWithValue("@personelMaas", tbxmaas.Text);
            cmd.Parameters.AddWithValue("@personelSigorta", cbxsigorta.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Personel Kaydedildi");
            baglanti.Close();
            personelCek();
            btnpBilgileriGuncelle.Enabled = false;
        }

        private void tbPsorgu_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from personelBilgileri where personelAd like '" + tbPsorgu.Text + "%'", baglanti);
            ds.Clear();
            da.Fill(ds, "personelBilgileri");
            dataGridPersonel.DataSource = ds.Tables["personelBilgileri"];
            baglanti.Close();
            if (tbPsorgu.Text == "")
            {
                tbPtcSorgu.Enabled = true;
                tbPsoyadSorgu.Enabled = true;
                tbnosorgu.Enabled = true;
            }
            else
            {
                tbPtcSorgu.Enabled = false;
                tbPsoyadSorgu.Enabled = false;
                tbnosorgu.Enabled = false;
            }
        }

        private void tbPsoyadSorgu_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from personelBilgileri where personelSoyad like '" + tbPsoyadSorgu.Text + "%'", baglanti);
            ds.Clear();
            da.Fill(ds, "personelBilgileri");
            dataGridPersonel.DataSource = ds.Tables["personelBilgileri"];
            baglanti.Close();
            if (tbPsoyadSorgu.Text == "")
            {
                tbPtcSorgu.Enabled = true;
                tbPsorgu.Enabled = true;
                tbnosorgu.Enabled = true;
            }
            else
            {
                tbPtcSorgu.Enabled = false;
                tbPsorgu.Enabled = false;
                tbnosorgu.Enabled = false;
            }

        }

        private void tbPtcSorgu_TextChanged(object sender, EventArgs e)
        {
            if (IsNumeric(tbPtcSorgu.Text))
            {
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from personelBilgileri where personelTC like '" + tbPtcSorgu.Text + "%'", baglanti);
                ds.Clear();
                da.Fill(ds, "personelBilgileri");
                dataGridPersonel.DataSource = ds.Tables["personelBilgileri"];
                baglanti.Close();
                if (tbPtcSorgu.Text == "")
                {
                    tbPsorgu.Enabled = true;
                    tbPsoyadSorgu.Enabled = true;
                    tbnosorgu.Enabled = true;
                }
                else
                {
                    tbPsorgu.Enabled = false;
                    tbPsoyadSorgu.Enabled = false;
                    tbnosorgu.Enabled = false;
                }
            }
            else
            {
                tbPtcSorgu.Text = "";
                MessageBox.Show("Lütfen rakam giriniz:", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbnosorgu_TextChanged(object sender, EventArgs e)
        {
            if (IsNumeric(tbnosorgu.Text))
            {
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from personelBilgileri where personelID like '" + tbnosorgu.Text + "%'", baglanti);
                ds.Clear();
                da.Fill(ds, "personelBilgileri");
                dataGridPersonel.DataSource = ds.Tables["personelBilgileri"];
                baglanti.Close();
                if (tbnosorgu.Text == "")
                {
                    tbPsorgu.Enabled = true;
                    tbPsoyadSorgu.Enabled = true;
                    tbPtcSorgu.Enabled = true;
                }
                else
                {
                    tbPsorgu.Enabled = false;
                    tbPsoyadSorgu.Enabled = false;
                    tbPtcSorgu.Enabled = false;
                }
            }
            else
            {
                tbnosorgu.Text = "";
                MessageBox.Show("Lütfen rakam giriniz:", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool dolularMi()
        {
            if (tbxptc.Text != "" && tbxpad.Text != "" && tbxpsoyad.Text != "" && tbxptel.Text != "" && tbxpka.Text != "" && tbxpsifre.Text != "" && tbxpadres.Text != "" && tbxpgorev.Text != "")
            {
                return true;
            }
            return false;
        }

        private void btnKaydet_Click_1(object sender, EventArgs e)
        {
            if (dolularMi())
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText = @"insert into personelBilgileri(personelKullaniciAdi,personelTC,personelSifre,personelAd,
                                personelSoyad,personelCinsiyet,personelTel,personelEposta,personelAdres,personelGorev,personelMaas,personelSigorta) values 
                                (@personelKullaniciAdi,@personelTC,@personelSifre,@personelAd,@personelSoyad,@personelCinsiyet,@personelTel,@personelEposta,@personelAdres,
                                @personelGorev,@personelMaas,@personelSigorta)";
                cmd.Parameters.AddWithValue("@personelKullaniciAdi", tbxpka.Text);
                cmd.Parameters.AddWithValue("@personelTC", tbxptc.Text);
                cmd.Parameters.AddWithValue("@personelSifre", tbxpsifre.Text);
                cmd.Parameters.AddWithValue("@personelAd", tbxpad.Text);
                cmd.Parameters.AddWithValue("@personelSoyad", tbxpsoyad.Text);
                if (rbtnkadin.Checked) { cmd.Parameters.AddWithValue("@personelCinsiyet", rbtnkadin.Text); }
                else { cmd.Parameters.AddWithValue("@personelCinsiyet", rbtnerkek.Text); }
                cmd.Parameters.AddWithValue("@personelTel", tbxptel.Text);
                cmd.Parameters.AddWithValue("@personelEposta", tbxpeposta.Text);
                cmd.Parameters.AddWithValue("@personelAdres", tbxpadres.Text);
                cmd.Parameters.AddWithValue("@personelGorev", tbxpgorev.Text);
                cmd.Parameters.AddWithValue("@personelMaas", tbxmaas.Text);
                cmd.Parameters.AddWithValue("@personelSigorta", cbxsigorta.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Personel Kaydedildi");
                baglanti.Close();
                personelCek();
                btnKaydet.Enabled = false;
            }
            else
            {
                MessageBox.Show("Lütfen boş girilmemesi gereken yerleri doldurunuz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
                }
                if (!IsNumeric(tbMtutar.Text))
                {
                    MessageBox.Show("Borç Sayı olmalıdır.");

                }
                 /*********************************************************** KONTROL İŞLEMLERİ SON **************************************************/
                /*********************************************************** EKLEME İŞLEMİ **********************************************************/

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
                    cmd.Parameters.AddWithValue("@musteriUcret", tbMtutar.Text);
                    if (odendiMi.Checked) { cmd.Parameters.AddWithValue("@odemeAlındiMi", "1".ToString()); }
                    else { cmd.Parameters.AddWithValue("@odemeAlındiMi", "0".ToString()); }
                    cmd.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Bilgileri Güncelleme İşlemi Başarılı", "Güncelleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mustericek();
                }
            }
        }

        private void btnpSil_Click_1(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Silmek İstediğinize Emin Misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (cevap == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = baglanti;
                command.CommandText = "delete from personelBilgileri where personelID=@personelID";
                command.Parameters.AddWithValue("@personelID", lblpersonelID.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("Kayıt Silindi");
                baglanti.Close();
                personelCek();
            }

        }

        private void txtAraMusteriNo_TextChanged(object sender, EventArgs e)
        {
            if (IsNumeric(txtAraMusteriNo.Text))
            {
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from musteriBilgileri where musteriOdadaMi='1' and musteriID like '" + txtAraMusteriNo.Text + "%'", baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds, "musteriBilgileri");
                dataGridMusteri.DataSource = ds.Tables["musteriBilgileri"];
                baglanti.Close();
                if (txtAraMusteriNo.Text == "")
                {
                    txtAraTcKimlikNo.Enabled = true;
                    txtAraAd.Enabled = true;
                    txtAraSoyad.Enabled = true;
                }
                else
                {
                    txtAraTcKimlikNo.Enabled = false;
                    txtAraAd.Enabled = false;
                    txtAraSoyad.Enabled = false;
                }
            }
            else
            {
                txtAraMusteriNo.Text = "";
                MessageBox.Show("Lütfen rakam giriniz:", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAraTcKimlikNo_TextChanged(object sender, EventArgs e)
        {
            if (IsNumeric(txtAraTcKimlikNo.Text))
            {
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from musteriBilgileri where musteriOdadaMi='1' and musteriTC like '" + txtAraTcKimlikNo.Text + "%'", baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds, "musteriBilgileri");
                dataGridMusteri.DataSource = ds.Tables["musteriBilgileri"];
                baglanti.Close();
                if (txtAraTcKimlikNo.Text == "")
                {
                    txtAraMusteriNo.Enabled = true;
                    txtAraAd.Enabled = true;
                    txtAraSoyad.Enabled = true;
                }
                else
                {
                    txtAraMusteriNo.Enabled = false;
                    txtAraAd.Enabled = false;
                    txtAraSoyad.Enabled = false;
                }
            }
            else
            {
                txtAraTcKimlikNo.Text = "";
                MessageBox.Show("Lütfen rakam giriniz:", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnguncelleiptal_Click(object sender, EventArgs e)
        {
            tbxptc.Text = personelbilgileri[0];
            tbxpad.Text = personelbilgileri[1];
            tbxpsoyad.Text = personelbilgileri[2];
            if (personelbilgileri[3] == "kadın") { rbtnkadin.Checked = true; }
            else { rbtnerkek.Checked = true; }
            tbxptel.Text = personelbilgileri[3];
            tbxpeposta.Text = personelbilgileri[4];
            tbxpka.Text = personelbilgileri[5];
            tbxpsifre.Text = personelbilgileri[6];
            tbxpadres.Text = personelbilgileri[7];
            tbxpgorev.Text = personelbilgileri[8];
            tbxmaas.Text = personelbilgileri[9];
            cbxsigorta.Text = personelbilgileri[10];
            cbPunvan.Text = personelbilgileri[11];
            btnguncelleiptal.Visible = false;
            btnpBilgileriGuncelle.Enabled = false;
        }

        private void btnOdaGuncelle_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Güncelleme İstediğinize Emin Misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (cevap == DialogResult.Yes)
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText = "update Odalar set odaGunlukUcret=@odaGunlukUcret,odaKacKisilik=@odaKacKisilik,odaKullanilabilirMi=@odaKullanilabilirMi where odaID=@odaID";
                cmd.Parameters.AddWithValue("@odaID", tbOdaID.Text);
                if (!rbtnAktif.Checked && !rbtnBakim.Checked)
                {
                    MessageBox.Show("Lütfen oda durumu belirtin ", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (rbtnAktif.Checked) { cmd.Parameters.AddWithValue("@odaKullanilabilirMi", "1"); }
                    else { cmd.Parameters.AddWithValue("@odaKullanilabilirMi", "0"); }
                }
                cmd.Parameters.AddWithValue("@odaGunlukUcret", tbOdaFiyat.Text);
                cmd.Parameters.AddWithValue("@odaKacKisilik", tbOdaKapasite.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Güncelleştirme Yapıldı.");
                //tablonun günc
                DataSet dss = new DataSet();
                BindingSource bss2 = new BindingSource();
                SqlDataAdapter daa = new SqlDataAdapter("select odaID,katAdi,kattakiOdaAdi,odaKullanilabilirMi,odaGunlukUcret,odaKacKisilik from Odalar", baglanti);
                daa.Fill(dss);
                bss2.DataSource = dss.Tables[0];
                dataGridView4.DataSource = bss2;
            }
        }

        void OdaEkle()
        {
            b++;
            int a = int.Parse(tbEklenecekKat.Text);
            string odaAdi = a + "00" + b;
            SqlCommand cmd = new SqlCommand(@"insert into Odalar (katAdi,kattakiOdaAdi,odaKullanilabilirMi,odadaKacKisiVar) values (@katAdi,@kattakiOdaAdi,@odaKullanilabilirMi,@odadaKacKisiVar)", baglanti);
            cmd.Parameters.AddWithValue("@katAdi", tbEklenecekKat.Text);
            int falan = int.Parse(odaAdi.ToString()) + 1;// misal 4001 oldu
            cmd.Parameters.AddWithValue("@kattakiOdaAdi", falan.ToString());
            cmd.Parameters.AddWithValue("@odaKullanilabilirMi", "1");
            cmd.Parameters.AddWithValue("@odadaKacKisiVar", "0");
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        void odaninEklenecegiKatlarinBelirlenmesi()
        {
            tbOdaninEklenecegiKat.Items.Clear();
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlCommand cmd = new SqlCommand("select katAdi from Odalar group by katAdi", baglanti);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tbOdaninEklenecegiKat.Items.Add(int.Parse(dr["katAdi"].ToString()));
            }
            baglanti.Close();
        }

        private void btnKatEkle_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            if (tbKataOdaAdet.Text == "")
            {
                MessageBox.Show("Lütfen oda adedi girin.");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("select katAdi from Odalar", baglanti);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["katAdi"].ToString() == tbEklenecekKat.Text)
                    {
                        katVarMi = true;
                    }
                    else
                    {
                        katVarMi = false;
                    }
                }
                dr.Dispose();
                if (katVarMi)
                {
                    MessageBox.Show("Kat zaten var\nLütfen Oda Ekleme ve Silme Panelini Kullanın.", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    katCek();
                    tbKataOdaAdet.Text = "";
                }
                else
                {
                    for (int i = 0; i < int.Parse(tbKataOdaAdet.Text); i++)
                    {
                        OdaEkle();
                    }
                }
                baglanti.Close();
                if (!katVarMi)
                {
                    MessageBox.Show("Kat Ekleme İşlemi Başarılı", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                odalarCek();
                this.dataGridView4.Columns["odaID"].SortMode = DataGridViewColumnSortMode.Automatic;//oda bilgileri tablosu
                odaninEklenecegiKatlarinBelirlenmesi();
            }
            baglanti.Close();
            Application.Restart();
        }

        private void btnOdaEkle_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlCommand cmds = new SqlCommand("select katAdi from Odalar", baglanti);
            SqlDataReader drs = cmds.ExecuteReader();
            while (drs.Read())
            {
                if (drs["katAdi"].ToString() == tbEklenecekKat.Text)//odanın ekleneceği kat dbde varsa
                {
                    odaEklemekİcinKatVarMi = true;
                }
            }
            drs.Close();
            drs.Dispose();
            if (!odaEklemekİcinKatVarMi)
            {
                SqlCommand cmd = new SqlCommand("select max(kattakiOdaAdi) from Odalar where katAdi='" + tbOdaninEklenecegiKat.Text + "'", baglanti);
                katno = int.Parse(cmd.ExecuteScalar().ToString());// kattaki en yüksek oda numarasını tutar
                katno++;// en yüksek kat numarası tekrarlanmasın sonraki numara verilsin için
                SqlCommand cmd22 = new SqlCommand();
                cmd22.Connection = baglanti;
                cmd22.CommandText = @"insert into Odalar (kattakiOdaAdi,katAdi,odaKullanilabilirMi,odadaKacKisiVar) values (@kattakiOdaAdi,@katAdi,@odaKullanilabilirMi,@odadaKacKisiVar)";
                cmd22.Parameters.AddWithValue("@kattakiOdaAdi", katno.ToString());
                cmd22.Parameters.AddWithValue("@katAdi", tbOdaninEklenecegiKat.SelectedItem.ToString());
                cmd22.Parameters.AddWithValue("@odaKullanilabilirMi", "1".ToString());
                cmd22.Parameters.AddWithValue("@odadaKacKisiVar", "0");

                string kacOlacakKat = tbOdaninEklenecegiKat.SelectedItem.ToString();
                string kacOlsun = Interaction.InputBox("Fiyat Bilgisi", "Odanın Fiyatı giriniz:", "", -1, -1);
                if (kacOlsun == "")
                {
                    MessageBox.Show("Lütfen Fiyat Girerek Ekleyin", "Uyarı", MessageBoxButtons.OK);
                    return;
                }
                if (int.Parse(kacOlsun) < 0)
                {
                    MessageBox.Show("Lütfen Düzgün bir Fiyat Bilgisi Girin", "Uyarı", MessageBoxButtons.OK);
                    return;
                }
                DialogResult cevap = MessageBox.Show("Fiyattan Emin Misiniz?", "Uyarı", MessageBoxButtons.YesNo);
                if (cevap == DialogResult.Yes)
                {
                    cmd22.ExecuteNonQuery();
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }
                    SqlCommand cmdehe = new SqlCommand();
                    cmdehe.Connection = baglanti;
                    cmdehe.CommandText = "update Odalar set odaGunlukUcret=@odaGunlukUcret where kattakiOdaAdi=@kattakiOdaAdi";
                    cmdehe.Parameters.AddWithValue("@kattakiOdaAdi", katno);
                    cmdehe.Parameters.AddWithValue("@odaGunlukUcret", kacOlsun);
                    cmdehe.ExecuteNonQuery();
                }
                else { return; }
                cmd22.Dispose();
                odalarCek();
                MessageBox.Show("Kat Ekleme İşlemi Başarılı", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kat Bilgisi Yok\nİsterseniz Kat Ekleme Panelinden ekleyebilirsiniz.", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            baglanti.Close();
        }

        private void btnOdaSil_Click(object sender, EventArgs e)//olabilir-> //datagridview4 te bu ad yoksa işlem yapmasın hata versin
        {
            string a = Interaction.InputBox("Silinecek Odanın Numarasını Girin:", "Oda No Giriş", "", -1, -1);

            baglanti.Open();
            string sorguSil = "delete from Odalar where kattakiOdaAdi=" + a.ToString() + "";
            SqlCommand cmd = new SqlCommand(sorguSil, baglanti);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            baglanti.Close();
            MessageBox.Show("Silme İşlemi Başarılı", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            odalarCek();//oda bilgileri güncellenmesi
        }

        void kapasiteAzalt(string azaltilacakID)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }

            SqlCommand cmdkacVar = new SqlCommand("select odadaKacKisiVar from Odalar where odaID=@odaID", baglanti);
            cmdkacVar.Parameters.AddWithValue("@odaID", azaltilacakID);
            int odadakilerinSayisi = int.Parse(cmdkacVar.ExecuteScalar().ToString());
            odadakilerinSayisi--;

            SqlCommand cmdupd = new SqlCommand("update Odalar set odadaKacKisiVar=@odadaKacKisiVar where odaID=@odaID", baglanti);
            cmdupd.Parameters.AddWithValue("@odadaKacKisiVar", odadakilerinSayisi.ToString());
            cmdupd.Parameters.AddWithValue("@odaID", azaltilacakID);
            cmdupd.ExecuteNonQuery();
            cmdupd.Dispose();
            MessageBox.Show("işlem tamamlandı.");
            baglanti.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (odendiMi.Checked)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("update musteriBilgileri set musteriOdadaMi=@musteriOdadaMi where musteriID=@musteriID");
                cmd.Connection = baglanti;
                cmd.Parameters.AddWithValue("@musteriID", lblID.Text);
                cmd.Parameters.AddWithValue("musteriOdadaMi", 0.ToString());
                MessageBox.Show("müşteri kaldırıldı.");
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                //odadakiMusteriler-> tablo güncellemesi
                kapasiteAzalt(lblOdadakiler.Text);
                mustericek();
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("Lütfen önce ödeme işlemini gerçekleştiriniz", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            istatistik ist = new istatistik();
            ist.ShowDialog();
        }

        private void btnYetkiliGuncelle_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Güncelleme İstediğinize Emin Misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (cevap == DialogResult.Yes)
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                if (tbPUnvan.Text == "")
                {
                    MessageBox.Show("Lütfen Ünvan belirtin ", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText = "update personelBilgileri set yetkiliMi=@yetkiliMi,unvan=@unvan where personelID=@personelID";
                cmd.Parameters.AddWithValue("@personelID", tbPID.Text);
                cmd.Parameters.AddWithValue("@unvan", tbPUnvan.Text);
                //yetkili mi 
                if (!rbPyetkili.Checked && !rbPyetkisiz.Checked)
                {
                    MessageBox.Show("Lütfen yetki belirtin ", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (rbPyetkili.Checked) { cmd.Parameters.AddWithValue("@yetkiliMi", "1"); }
                    if (rbPyetkisiz.Checked) { cmd.Parameters.AddWithValue("@yetkiliMi", "0"); }
                }
                cmd.ExecuteNonQuery();
                MessageBox.Show("Güncelleştirme Yapıldı.");
                //tablonun günc
                yetkiliMi();
                baglanti.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string kacOlacakKat = Interaction.InputBox("Kat Bilgisi", "Hangi Kattaki Fiyatları Düzenlemek İstediğinizi Giriniz:", "", -1, -1);
            string kacOlsun = Interaction.InputBox("Odaların Fiyatı", "Tüm odalara Verilecek Fiyatı giriniz:", "", -1, -1);
            DialogResult cevap = MessageBox.Show("Fiyattan Emin Misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (cevap == DialogResult.Yes)
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText = "update Odalar set odaGunlukUcret=@odaGunlukUcret where katAdi=@katAdi";
                cmd.Parameters.AddWithValue("@katAdi", kacOlacakKat);
                cmd.Parameters.AddWithValue("@odaGunlukUcret", kacOlsun);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Fiyat Güncellemesi Yapıldı.");
                odalarCek();
            }
            else { return; }
        }

        private void btnKatSil_Click(object sender, EventArgs e)
        {
            string silinecekKat = Interaction.InputBox("Kat Bilgisi", "Hangi Katı Silmek İstediğinizi Giriniz:", "", -1, -1);
            DialogResult cevap = MessageBox.Show("Silmek İstediğinize Emin Misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (cevap == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = baglanti;
                command.CommandText = "delete from Odalar where katAdi=@katAdi";
                command.Parameters.AddWithValue("@katAdi", silinecekKat);
                command.ExecuteNonQuery();
                MessageBox.Show("Kat Silindi");
                baglanti.Close();
                odalarCek();
                odaninEklenecegiKatlarinBelirlenmesi();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            personelListesi pl = new personelListesi();
            pl.Show();
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1 || tabControl1.SelectedIndex == 2)
            {
                if (!giris.yetkiliMi)
                {
                    tabControl1.Visible = false;
                    MessageBox.Show("Buraya Erişim için yetkili değilsiniz", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabControl1.SelectedIndex = 0;
                    tabControl1.Visible = true;
                }
            }
        }

        private void tbOdaFiyat_TextChanged(object sender, EventArgs e)
        {
            if (IsNumeric(tbOdaFiyat.Text)) { }
            else
            {
                tbOdaFiyat.Text = "";
                MessageBox.Show("Lütfen oda Fiyatını Rakam Giriniz:", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbOdaKapasite_TextChanged(object sender, EventArgs e)
        {
            if (IsNumeric(tbOdaKapasite.Text)) { }
            else
            {
                tbOdaKapasite.Text = "";
                MessageBox.Show("Lütfen oda Kapasitesini Rakam Giriniz:", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lblYetki.Text == "1") { rbPyetkili.Checked = true; }
            else { rbPyetkisiz.Checked = true; }
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lblAktifPasif.Text == "1") { rbtnAktif.Checked = true; }
            else { rbtnBakim.Checked = true; }
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            DataTable dt = new DataTable();
            SqlDataAdapter daa = new SqlDataAdapter("select odaGunlukUcret,odaKacKisilik from Odalar", baglanti);
            daa.Fill(dt);
            BindingSource bss2 = new BindingSource();
            bss2.DataSource = dt;
            tbOdaFiyat.DataBindings.Clear();
            tbOdaKapasite.DataBindings.Clear();
            /*       tbOdaFiyat.DataBindings.Add("Text", bss2, "odaGunlukUcret");
                   tbOdaKapasite.DataBindings.Add("Text", bss2, "odaKacKisilik");*/
            baglanti.Close();
        }

        private void tbxptc_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbKataOdaAdet_TextChanged(object sender, EventArgs e)
        {
            if (IsNumeric(tbKataOdaAdet.Text)) { }
            else
            {
                tbKataOdaAdet.Text = "";
                MessageBox.Show("Lütfen rakam giriniz:", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lblOdeme.Text == "1") { odendiMi.Checked = true; }
            else { odendiMi.Checked = false; }
            if (lbcinsiyet.Text == "Erkek") { rbMerk.Checked = true; }
            else { rbMkadin.Checked = true; }
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlCommand cmdOdaAdi = new SqlCommand("select kattakiOdaAdi from Odalar where odaID=@odaID", baglanti);
            cmdOdaAdi.Parameters.AddWithValue("@odaID", lblOdadakiler.Text);
            tbOdaAdi.Text = cmdOdaAdi.ExecuteScalar().ToString();
            baglanti.Close();
        }

        private void tbMtc_TextChanged(object sender, EventArgs e)
        {
            try
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
                        lbcinsiyet.Text = dr["musteriCinsiyet"].ToString();
                        if (lbcinsiyet.Text == "Kadın") { rbMkadin.Checked = true; }
                        else { rbMerk.Checked = true; }
                        lblOdadakiler.Text = dr["musteriOdaID"].ToString();
                        tbMtutar.Text = dr["musteriUcret"].ToString();
                        dtpMgiris.Value = DateTime.Today;
                        dtpMcikis.Value = DateTime.Today;
                    }
                    dr.Close();
                    cmd.Dispose();
                    SqlCommand cemede = new SqlCommand("select kattakiOdaAdi from Odalar where odaID=@odaID", baglanti);
                    cemede.Parameters.AddWithValue("@odaID", lblOdadakiler.Text);
                    tbOdaAdi.Text = cemede.ExecuteScalar().ToString();
                    baglanti.Close();
                }
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Kişi Kayıtlı Değil\nLütfen Yeni Kayıt Yapınız.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbOdaAdi_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnHizliKayit_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select odaKacKisilik,odadaKacKisiVar from Odalar where odaID='" + lblOdadakiler.Text + "'", baglanti);
            dt.Clear();
            da.Fill(dt);
            int kacKisilik = int.Parse(dt.Rows[0]["odaKacKisilik"].ToString());
            int kacVar = int.Parse(dt.Rows[0]["odadaKacKisiVar"].ToString());
            int kalan = kacKisilik - kacVar;
            if (kalan > 0)//odada boş yer varsa
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText = @"insert into musteriBilgileri (musteriOdaID,musteriID,musteriTC,
                                    musteriAd,musteriSoyad,musteriDtarihi,musteriCinsiyet,
                                musteriCepTel,musteriEposta,musteriEvAdresi,musteriOdaGiris,musteriOdaCikis,musteriUcret,musteriOdadaMi) values 
                               (@musteriOdaID,@musteriID,@musteriTC,
                                @musteriAd,@musteriSoyad,@musteriDtarihi,@musteriCinsiyet,@musteriCepTel,@musteriEposta,
                                @musteriEvAdresi,@musteriOdaGiris,@musteriOdaCikis,@musteriUcret,@musteriOdadaMi)";
                cmd.Parameters.AddWithValue("@musteriOdaID", lblOdadakiler.Text);
                cmd.Parameters.AddWithValue("@musteriID", lblID.Text);
                cmd.Parameters.AddWithValue("@musteriTC", tbMtc.Text);
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
                cmd.Parameters.AddWithValue("@musteriUcret", tbMtutar.Text);
                if (odendiMi.Checked) { cmd.Parameters.AddWithValue("@odemeAlındiMi", "1".ToString()); }
                else { cmd.Parameters.AddWithValue("@odemeAlındiMi", "0".ToString()); }
                cmd.Parameters.AddWithValue("@musteriOdadaMi", "1".ToString());
                cmd.ExecuteNonQuery();
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand cmdkacVar = new SqlCommand("select odadaKacKisiVar from Odalar where odaID=@odaID", baglanti);
                cmdkacVar.Parameters.AddWithValue("@odaID", lblOdadakiler.Text);
                int odadakilerinSayisi = int.Parse(cmdkacVar.ExecuteScalar().ToString());
                odadakilerinSayisi++;
                SqlCommand cmdupd = new SqlCommand("update Odalar set odadaKacKisiVar=@odadaKacKisiVar where odaID=@odaID", baglanti);
                cmdupd.Parameters.AddWithValue("@odadaKacKisiVar", odadakilerinSayisi.ToString());
                cmdupd.Parameters.AddWithValue("@odaID", lblOdadakiler.Text);
                cmdupd.ExecuteNonQuery();
                MessageBox.Show("işlem tamamlandı.");
                mustericek();
            }
            else
            {
                MessageBox.Show("Oda Dolu Lütfen Başka bir oda seçin", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnHizliKayit.Visible = false;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            btnHizliKayit.Visible = true;
            tbOdaNo.Text = ""; lblOdadakiler.Text = ""; lblID.Text = "";
            tbMtc.Text = ""; tbMad.Text = ""; tbMsoyad.Text = "";
            mustDtarihi.Value = DateTime.Today;
            tbMtel.Text = ""; tbMadres.Text = ""; tbMeposta.Text = "";
            if (rbMkadin.Checked) { rbMkadin.Checked = false; }
            else if (rbMerk.Checked) { rbMerk.Checked = false; }
            dtpMgiris.Value = DateTime.Today;
            dtpMcikis.Value = DateTime.Today;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            musteriRapor mr = new musteriRapor();
            mr.ShowDialog();
        }


    }

}
