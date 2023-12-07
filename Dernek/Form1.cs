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

   
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
            

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

        private void button4_Click(object sender, EventArgs e)
        {
            aidatGoster aidatGoster = new aidatGoster();
            aidatGoster.Show();

           
        }
    }

}

