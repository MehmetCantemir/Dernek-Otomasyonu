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
    public partial class Form1 : Form
    {

        VTConnection VTBaglantisi = new VTConnection();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /* VTBaglantisi.VTConnect();
             VTBaglantisi.baglantiKontrol();
             string[] KisiAdlari = new string[10];
             KisiAdlari = VTBaglantisi.veriCek();
             label1.Text = KisiAdlari[0];
             label2.Text = KisiAdlari[1];
             VTBaglantisi.veriCek();
             VTBaglantisi.VTDisConnect();*/
            
            /*DataAccess access = new DataAccess();
            int TC = 1;
            Kisi kisi = access.KisiBilgileri(TC);
            Aidat aidat = access.AidatBilgileri(TC);
            

            Eris accesss=new Eris();
            int TC1 = 2;
            Kisi kisi1 =accesss.KisiBilgileri(TC1);




            if (kisi1 != null)
            {
                label3.Text = "TC Kimlik = " + kisi1.TCKimlik.ToString();
                label4.Text = "Adı = " + kisi1.Adi;
                label5.Text = "Soyadı = " + kisi1.Soyadi;
                label6.Text = "Kan Grubu = " + kisi1.KanGrubu;
                label7.Text = "Şehir = " + kisi1.Sehir;
                label8.Text = "Aktif = " + kisi1.Aktif;

            }

            else
            {
                MessageBox.Show("Kullanıcı yok!!!");
            }

            /*if (aidat != null)
            {
                label9.Text = "Aidat Miktarı = " + aidat.AidatMiktari.ToString();
            }
            else
            {
                MessageBox.Show("Kullanıcı yok!!!");
            }
            */
            

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            uyeEkle uyeekle = new uyeEkle();
            uyeekle.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            kangrubu verigoster = new kangrubu();
            verigoster.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            kisibilgileriguncellestir veriguncelle = new kisibilgileriguncellestir();
            veriguncelle.Show();
        }
    }

}

