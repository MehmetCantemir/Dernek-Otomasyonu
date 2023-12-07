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
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout;



namespace Dernek
{
    public partial class aidatGoster : Form
    {
        public aidatGoster()
        {
            InitializeComponent();
        }

        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\mehme\\Desktop\\Dernek\\Dernek\\Data\\DernekVT.accdb");

        private void button1_Click(object sender, EventArgs e)
        {
            int TC = Convert.ToInt32(textBox1.Text);
            Kisi kisi = new Kisi();
            baglan.Open();

            //Kişi tablosundan textboxt'a girilen tc bilgisine göre bilgi çekme
            string query = "SELECT * FROM Kisi WHERE TC=@TC";
            OleDbCommand cmd = new OleDbCommand(query, baglan);
            cmd.Parameters.AddWithValue("@TC", TC);

            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                kisi.TCKimlik = Convert.ToInt32(reader["TC"]);

            }

            Aidat aidat = new Aidat();
            string query1 = "SELECT * FROM Aidat WHERE TC=@TC";
            OleDbCommand cmd1 = new OleDbCommand(query1, baglan);
            cmd1.Parameters.AddWithValue("@TC", TC);



            OleDbDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                aidat.AidatID = Convert.ToInt16(reader1["AidatID"]);
                aidat.TC = Convert.ToInt32(reader1["TC"]);
                aidat.AidatMiktari = Convert.ToInt16(reader1["AidatMiktari"]);
                aidat.Odendimi = Convert.ToBoolean(reader1["OdendiMi"]);
                aidat.AidatAyi = reader1["AidatAyi"].ToString();

                string ay = ayiGetir(aidat.AidatAyi);
                int borc = 0;
                int borc_adeti = 0;

               
               // aidat eğer ödenmediyse otomatik borç atama ve ödemediği borç miktarını belirleme
               if (!aidat.Odendimi)
               {
                   borc += 200;
                   borc_adeti++;
               }

                borcPdf(borc, borc_adeti,TC);


               //aylara göre aidat ödemesi yapılıp yapılmadığını listbox'a yazdırma.
               if (borc == 0)
               {
                  listBox1.Items.Add(ay);
                  listBox1.Items.Add("Aidat ödemesi yapılmamıştır !");

               }
               else
               { 
              
                   listBox1.Items.Add(ay);
                   listBox1.Items.Add("Aidat ödemesi yapılmıştır!");
               }

            }
            baglan.Close();

        }

        public void borcPdf(int borcAdeti , int borcMiktari,int TCKimlik)
        {
            baglan.Open();
            string adi;
            string query = "SELECT Adi FROM Kisi WHERE TCKimlik=@TC";
            OleDbCommand cmd = new OleDbCommand(query, baglan);
            cmd.Parameters.AddWithValue("@TC", TCKimlik);

            Kisi kisi2 = new Kisi();

            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                kisi2.Adi = reader["Adi"].ToString();
            }
            string pdfDosyaYolu = "C:\\Users\\mehme\\Desktop\\PDF";
            var writer = new PdfWriter(pdfDosyaYolu);
            var pdf = new PdfDocument(writer);
            var document= new Document(pdf);

            string eklenecek_veri = kisi2.Adi + " Kişinin Borç adeti = " + borcAdeti.ToString() + " ve toplam borç miktarı = " + borcAdeti.ToString();

            document.Add(new Paragraph().(eklenecek_veri));

        }

        public string ayiGetir(string gelenAy)
        {
            string ay;

            if (gelenAy == "1")
            {
                ay = "OCAK";
            }
            else if (gelenAy == "2")
            {
                ay = " ŞUBAT";
            }
            else if (gelenAy == "3")
            {
                ay = " MART";

            }
            else if (gelenAy == "4")
            {
                ay = " NİSAN";
            }
            else if (gelenAy == "5")
            {
                ay = "MAYIS";
            }
            else if (gelenAy == "6")
            {
                ay = " HAZİRAN";
            }
            else if (gelenAy == "7")
            {
                ay = "TEMUUZ";
            }
            else if (gelenAy == "8")
            {
                ay = "AĞUSTOS";
            }

            else if (gelenAy == "9")
            {
                ay = "EYLÜL";
            }


            else if (gelenAy == "10")
            {
                ay = " EKİM";
            }


            else if (gelenAy == "11")
            {
                ay = " KASIM";
            }

            else if (gelenAy == "12")
            {
                ay = "ARALIK";
            }
            else
                ay = "BÖYLE BİR AY YOKTUR.";

            return ay;

        }
    }
}
