using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dernek
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void uye_ekle_Click(object sender, EventArgs e)
        {

            uyeEkle uyeekle = new uyeEkle();
            uyeekle.Show();
        }

        private void aidat_grafiksel_Click(object sender, EventArgs e)
        {
            aylikgrafik aylik = new aylikgrafik();
            aylik.Show();
        }

        private void aidat_islemi_Click(object sender, EventArgs e)
        {
            tarih tarih = new tarih();
            tarih.Show();
        }

        private void borc_goruntule_Click(object sender, EventArgs e)
        {
            aidatGoster aidatGoster = new aidatGoster();
            aidatGoster.Show();

        }

        private void uye_bilgilerini_yonet_Click(object sender, EventArgs e)
        {
            kisibilgileriguncellestir veriguncelle = new kisibilgileriguncellestir();
            veriguncelle.Show();
        }

        private void uye_listele_Click(object sender, EventArgs e)
        {
            kangrubu verigoster = new kangrubu();
            verigoster.Show();
        }

        private void sehir_grafiksel_Click(object sender, EventArgs e)
        {
            sehirgrafik sehir = new sehirgrafik();
            sehir.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
