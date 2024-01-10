
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
using System.Net.Mail;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using VeriKatmani;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using System.Net.Mail;
using Org.BouncyCastle.Crypto.Macs;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;



namespace Dernek
{
    public partial class aidatGoster : Form
    {

        string[] Scopes = { GmailService.Scope.GmailSend };
        string ApplicationName = "Dernek Otomasyonu";

        public aidatGoster()
        {
            InitializeComponent();
            dataGridView1.Columns.Add("TC", "TC");
            dataGridView1.Columns.Add("Adi", "Adı");
            dataGridView1.Columns.Add("Soyadi", "Soyadı");
            dataGridView1.Columns.Add("ToplamBorc", "Toplam Borç");
        }

      
        VeriTabaniBaglantisi tabaniBaglantisi = new VeriTabaniBaglantisi();
        private void button1_Click(object sender, EventArgs e)
        {
            //Butona her basıldığında listbox nesnesini temizleme 
            listBox1.Items.Clear();

            // Kişilerin aidat bilgilerini göstermeye yarayacak method
            aidatbilgilerinigoster();
        }



        public void SendEmail()
        {
            string sql = "SELECT Kisi.TC , Kisi.Adi , Kisi.Soyadi,Kisi.EMail,SUM(Aidat.Borc) AS ToplamBorc " +
                "FROM Kisi" + " LEFT JOIN Aidat ON Kisi.TC = Aidat.TC " + " GROUP BY Kisi.TC , Kisi.Adi,Kisi.Soyadi,Kisi.EMail";
            tabaniBaglantisi.connection();
            OleDbCommand command = new OleDbCommand(sql, tabaniBaglantisi.connection());
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string Email = reader["EMail"].ToString();

                // Kontrol ekle: Borç alanı NULL ise, 0 olarak kabul edilir.
             
                if (reader["ToplamBorc"] != DBNull.Value)
                {
                    sumofborc = Convert.ToDecimal(reader["ToplamBorc"]);
                }
                else
                {
                    sumofborc= 0;
                }
              
                try
                {

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential("dernekotomasyon@gmail.com", "fpfg oumc terx pezp");
                    smtpClient.EnableSsl = true;

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("dernekotomasyon@gmail.com");
                    mailMessage.To.Add(Email);
                    
                    mailMessage.Subject = "Borç Hakkında";
                    mailMessage.Body = "Toplam Borcunuz = "+sumofborc;
                    smtpClient.Send(mailMessage);
                    MessageBox.Show("Mail Gönderildi");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("E-posta gönderme hatası: " + ex.Message);
                }
            }

            tabaniBaglantisi.connection().Close();

        }
        
        public void aidatbilgilerinigoster()
        {
            int TC = Convert.ToInt32(textBox1.Text);
            Kisi kisi = new Kisi();
            tabaniBaglantisi.connection();
            //Kişi tablosundan textboxt'a girilen tc bilgisine göre bilgi çekme
            string query = "SELECT * FROM Kisi WHERE TC=@TC";
            OleDbCommand cmd = new OleDbCommand(query,tabaniBaglantisi.connection());
            cmd.Parameters.AddWithValue("@TC", TC);
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                kisi.TCKimlik = Convert.ToInt32(reader["TC"]);

            }

            Aidat aidat = new Aidat();
            string query1 = "SELECT * FROM Aidat WHERE TC=@TC";
            OleDbCommand cmd1 = new OleDbCommand(query1, tabaniBaglantisi.connection());
            cmd1.Parameters.AddWithValue("@TC", TC);
            OleDbDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                aidat.AidatID = Convert.ToInt16(reader1["AidatID"]);
                aidat.TC = Convert.ToInt32(reader1["TC"]);
                aidat.AidatMiktari = Convert.ToInt16(reader1["AidatMiktari"]);
                aidat.Odendimi = Convert.ToBoolean(reader1["OdendiMi"]);
                aidat.AidatAyi = reader1["AidataAyi"].ToString();
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
            tabaniBaglantisi.connection().Close();   
        }
        decimal sumofborc;
        public void  toplamborc()
        {
            string sql = "SELECT Kisi.TC , Kisi.Adi , Kisi.Soyadi,Kisi.EMail,SUM(Aidat.Borc) AS ToplamBorc "+
                "FROM Kisi"+" LEFT JOIN Aidat ON Kisi.TC = Aidat.TC "+" GROUP BY Kisi.TC , Kisi.Adi,Kisi.Soyadi,Kisi.EMail";

            tabaniBaglantisi.connection();
            OleDbCommand command = new OleDbCommand(sql, tabaniBaglantisi.connection());
            OleDbDataReader reader = command.ExecuteReader();
            Kisi kisi=new Kisi();
            while (reader.Read())
            {
               // Borçları Datagriedview'de gösterme
                kisi.TCKimlik = Convert.ToInt32(reader["TC"]);
                kisi.Adi = reader["Adi"].ToString();
                kisi.Soyadi = reader["Soyadi"].ToString();
                kisi.EMail = reader["EMail"].ToString();
                sumofborc = reader["ToplamBorc"] != DBNull.Value ? Convert.ToDecimal(reader["ToplamBorc"]) : 0;
                dataGridView1.Rows.Add(kisi.TCKimlik, kisi.Adi, kisi.Soyadi, sumofborc);
            }
            tabaniBaglantisi.connection().Close();
        }



        string Base64UrlEncode(string input)
        {
            var data = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(data).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }
        public void MailGonder()
        {


            string sql = "SELECT Kisi.TC , Kisi.Adi , Kisi.Soyadi,Kisi.EMail,SUM(Aidat.Borc) AS ToplamBorc " +
                "FROM Kisi" + " LEFT JOIN Aidat ON Kisi.TC = Aidat.TC " + " GROUP BY Kisi.TC , Kisi.Adi,Kisi.Soyadi,Kisi.EMail";
            tabaniBaglantisi.connection();
            OleDbCommand command = new OleDbCommand(sql, tabaniBaglantisi.connection());
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string Email = reader["EMail"].ToString();
                int toplamborc = Convert.ToInt32(reader["ToplamBorc"]);

                UserCredential credential;
                using (FileStream stream = new FileStream(Application.StartupPath + @"/credantials.json", FileMode.Open, FileAccess.Read))
                {

                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    path = Path.Combine(path, ".credentials/gmail-dotnet-quickstart.json");
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(path, true)).Result;

                }

                string message = $"To: {Email}\r\nSubject: Dernek Otomasyonu Borç Bilgilendirmesi \r\nContent-Type: text/html;charset=utf-8\r\n\r\n<h1>{toplamborc.ToString()}</h1>";
                //call your gmail service
                var service = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = credential, ApplicationName = ApplicationName });
                var msg = new Google.Apis.Gmail.v1.Data.Message();
                msg.Raw = Base64UrlEncode(message.ToString());
                service.Users.Messages.Send(msg, "me").Execute();
                MessageBox.Show("Your email has been successfully sent !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            
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

        private void button2_Click(object sender, EventArgs e)
        {
            pdfYazdir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            toplamborc();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void aidatGoster_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            SendEmail();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
