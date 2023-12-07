using Dernek.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dernek
{
    public partial class uyeEkle : Form
    {
        public uyeEkle()
        {
            InitializeComponent();
        }

        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\mehme\\Desktop\\Dernek\\Dernek\\Data\\DernekVT.accdb");


        private void button1_Click(object sender, EventArgs e)
        {
            addMember();
        }

        public void addMember()
        {
            Kisi kisi = new Kisi();
            baglan.Open(); //Veri tabanı bağlantısı

            //Kullanıcıdan alınan verileri kişi bilgilerinin tutulduğu nesneye aktarılması
            kisi.TCKimlik = Convert.ToInt32(txt_tc.Text);  
            kisi.Adi = txt_ad.Text;
            kisi.Soyadi = txt_soyad.Text;
            kisi.KanGrubu= comboBox1.Text;
            kisi.Sehir=txt_sehir.Text;
            kisi.Aktif = checkBox1.Checked.ToString();  
            
            // Kullanıcıdan alınan verilerin veri tabanına aktarılması
            string veriEkle= "INSERT INTO Kisi(TC,Adi,Soyadi,Kan_Grubu,Sehir,Aktif) " +
                "VALUES (@tc,@ad,@soyad,@kan,@sehir,@aktif)";
            OleDbCommand ekle = new OleDbCommand(veriEkle, baglan);


            ekle.Parameters.AddWithValue("@tc", Convert.ToInt32(kisi.TCKimlik));
            ekle.Parameters.AddWithValue("@ad", kisi.Adi);
            ekle.Parameters.AddWithValue("@soyad", kisi.Soyadi);
            ekle.Parameters.AddWithValue("@kan", kisi.KanGrubu);
            ekle.Parameters.AddWithValue("@sehir", kisi.Sehir);
            ekle.Parameters.AddWithValue("@aktif",Convert.ToBoolean(kisi.Aktif));
            ekle.ExecuteNonQuery();

            // veri tabanı bağlantısının sonlandırılması
            baglan.Close();

            MessageBox.Show("Veri başarıyla eklendi");

        }

        private void uyeEkle_Load(object sender, EventArgs e)
        {

        }
    }
}
