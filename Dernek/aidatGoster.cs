using Dernek.Data;

using iTextSharp.text.pdf;
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
using System.IO;
using iTextSharp.text;




namespace Dernek
{
    public partial class aidatGoster : Form
    {
        public aidatGoster()
        {
            InitializeComponent();
            dataGridView1.Columns.Add("TC", "TC");
            dataGridView1.Columns.Add("Adi", "Adı");
            dataGridView1.Columns.Add("Soyadi", "Soyadı");
            dataGridView1.Columns.Add("ToplamBorc", "Toplam Borç");
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

        public void toplamborc()
        {
     
            string sql = "SELECT Kisi.TC , Kisi.Adi , Kisi.Soyadi,SUM(Aidat.Borc) AS ToplamBorc "+
                "FROM Kisi"+" LEFT JOIN Aidat ON Kisi.TC = Aidat.TC "+" GROUP BY Kisi.TC , Kisi.Adi,Kisi.Soyadi";

            baglan.Open();
            OleDbCommand command = new OleDbCommand(sql,baglan);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
               
                int TC = Convert.ToInt32(reader["TC"]);
                string adi = reader["Adi"].ToString();
                string soyadi = reader["Soyadi"].ToString();
                decimal toplamBorc = reader["ToplamBorc"] != DBNull.Value ? Convert.ToDecimal(reader["ToplamBorc"]) : 0;
                dataGridView1.Rows.Add(TC, adi, soyadi, toplamBorc);

            }
            baglan.Close();
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

        public void pdfYazdir()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.OverwritePrompt = false;
            saveFileDialog.Title = "PDF Dosyaları";
            saveFileDialog.DefaultExt = "pdf";
            saveFileDialog.Filter = "PDF Dosyası (*.pdf) | *.pdf|Tüm Dosyalar(*.*) | *.*";
            
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                PdfPTable pdftable = new PdfPTable(dataGridView1.ColumnCount);
                pdftable.DefaultCell.Padding = 3;
                pdftable.WidthPercentage = 80;
                pdftable.DefaultCell.BorderWidth = 1;



                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                    pdftable.AddCell(cell);
                }



                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {

                        if (cell.Value != null)
                        {
                            pdftable.AddCell(cell.Value.ToString());
                        }
                        else
                        {
                            pdftable.AddCell(""); // Null değer ise boş hücre ekleyebilirsiniz.
                        }

                    }
                }


                using (FileStream stream = new FileStream(saveFileDialog.FileName + ".pdf", FileMode.Create))
                {
                    
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);// sayfa boyutu.
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    pdfDoc.Add(pdftable);
                    pdfDoc.Close();
                    stream.Close();
                }
                MessageBox.Show("Veriler PDF Olarak kaydedilmiştir.");
            }

           
        
        }

        public void eposta()
        {

        }


        private void button2_Click(object sender, EventArgs e)
        {
            pdfYazdir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            toplamborc();
        }
    }
}
