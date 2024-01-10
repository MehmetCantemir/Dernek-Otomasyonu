using Org.BouncyCastle.Asn1.Kisa;
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
using VeriKatmani;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Dernek
{
    public partial class kisibilgileriguncellestir : Form
    {
        public kisibilgileriguncellestir()
        {
            InitializeComponent();
        }

        VeriTabaniBaglantisi tabaniBaglantisi = new VeriTabaniBaglantisi();
        private void kisibilgileriguncellestir_Load(object sender, EventArgs e)
        {
            
            
        }
        int TC;
        private void button1_Click(object sender, EventArgs e)
        {
            tabaniBaglantisi.connection();
            TC = Convert.ToInt32(textBox1.Text);
            string selectquery = "SELECT * FROM Kisi where TC = @TC";
            OleDbCommand komut = new OleDbCommand(selectquery, tabaniBaglantisi.connection());
            komut.Parameters.AddWithValue("@TC", TC);
            OleDbDataAdapter adapter = new OleDbDataAdapter(komut);
            OleDbDataReader reader=komut.ExecuteReader();

            if(reader.Read()) {

                MessageBox.Show("Üye bulundu");
            }
            else
            {
                MessageBox.Show("Üye Yoktur");
            }

            tabaniBaglantisi.connection().Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Kisi kisi= new Kisi();

        
            tabaniBaglantisi.connection();

            kisi.Adi = txt_adi.Text;
            kisi.Soyadi = txt_soyadi.Text;
            kisi.KanGrubu = cb_kan.SelectedItem.ToString();
            kisi.Sehir = txt_sehir.Text;
            kisi.EMail = txt_email.Text;
            bool a = false;
            if (comboBox1.SelectedIndex == 0)
            {
                kisi.Aktif = "True";
                a = true;
            }
            else
            {
                kisi.Aktif= "False";
                a = false;
            }
            string queryUpdate = "UPDATE Kisi SET Adi=@adi,Soyadi=@soyadi,Kan_Grubu=@kangrubu,Sehir=@sehir,Aktif=@Aktif,EMail=@email WHERE TC=@TC";
           
            OleDbCommand komut = new OleDbCommand(queryUpdate, tabaniBaglantisi.connection());
            
            komut.Parameters.AddWithValue("@TC",Convert.ToInt32(textBox1.Text));
            komut.Parameters.AddWithValue("@adi", kisi.Adi);
            komut.Parameters.AddWithValue("@soyadi", kisi.Soyadi);
            komut.Parameters.AddWithValue("@kangrubu", kisi.KanGrubu);
            komut.Parameters.AddWithValue("@sehir", kisi.Sehir);
            komut.Parameters.AddWithValue("@Aktif",a);
            komut.Parameters.AddWithValue("@email", kisi.EMail);
            komut.ExecuteNonQuery();
           
            tabaniBaglantisi.connection().Close();
            MessageBox.Show("Üye bilgileri güncellendi");

        }
    }
}



    

   
        

 