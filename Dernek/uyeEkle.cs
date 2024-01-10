
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VeriKatmani;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Dernek
{
    public partial class uyeEkle : Form
    {
        public uyeEkle()
        {
            InitializeComponent();
        }

     


        private void button1_Click(object sender, EventArgs e)
        {
            addMember();
        }

        public void addMember()
        {
            //Veri katmanı kullanılarak veri tabanı bağlantısı yapılıyor.
            VeriTabaniBaglantisi tabaniBaglantisi = new VeriTabaniBaglantisi();

            //Veri katmanı kullanılarak Kisi nesnesi oluşturuluyor
            Kisi kisi = new Kisi();
            tabaniBaglantisi.connection(); 

            //Kullanıcıdan alınan verileri kişi bilgilerinin tutulduğu nesneye aktarılması   
            kisi.TCKimlik = Convert.ToInt32(txt_tc.Text);  
            kisi.Adi = txt_ad.Text;
            kisi.Soyadi = txt_soyad.Text;
            kisi.KanGrubu= comboBox1.Text;
            kisi.Sehir=txt_sehir.Text;
            kisi.Aktif = checkBox1.Checked.ToString();
            kisi.EMail = textBox1.Text;



            // Kullanıcıdan alınan verilerin veri tabanına aktarılması
            string veriEkle= "INSERT INTO Kisi(TC,Adi,Soyadi,Kan_Grubu,Sehir,Aktif,EMail) " +
                "VALUES (@tc,@ad,@soyad,@kan,@sehir,@aktif,@email)";
            OleDbCommand ekle = new OleDbCommand(veriEkle, tabaniBaglantisi.connection());
            ekle.Parameters.AddWithValue("@tc", Convert.ToInt32(kisi.TCKimlik));
            ekle.Parameters.AddWithValue("@ad", kisi.Adi);
            ekle.Parameters.AddWithValue("@soyad", kisi.Soyadi);
            ekle.Parameters.AddWithValue("@kan", kisi.KanGrubu);
            ekle.Parameters.AddWithValue("@sehir", kisi.Sehir);
            ekle.Parameters.AddWithValue("@aktif",Convert.ToBoolean(kisi.Aktif));


            //Geçerli Eposta Kontrolü
            if (IsValidEmail(kisi.EMail))
            {
            
                ekle.Parameters.AddWithValue("@email", kisi.EMail);
               
            }
            else
            {
                // E-posta adresi geçerli değil
                MessageBox.Show("Geçersiz e-posta adresi. Lütfen doğru bir e-posta adresi girin.");
            }
            
            ekle.ExecuteNonQuery();

            // veri tabanı bağlantısının sonlandırılması
            tabaniBaglantisi.connection().Close();

            MessageBox.Show("Veri başarıyla eklendi");

        }

        private void uyeEkle_Load(object sender, EventArgs e)
        {

        }

        private bool IsValidEmail(string email)
        {
            try
            {
                // E-posta adresini geçerli bir formata kontrol etmek için regex kullanma
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                Regex regex = new Regex(pattern);

                return regex.IsMatch(email);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
