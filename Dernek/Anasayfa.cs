using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VeriKatmani;

namespace Dernek
{
    public partial class Anasayfa : Form
    {

   
        public Anasayfa()
        {
            InitializeComponent();
        }

        VeriTabaniBaglantisi tabaniBaglantisi = new VeriTabaniBaglantisi();

        private void Form1_Load(object sender, EventArgs e)
        { 
            

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
         
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
         

           
        }
        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string kullaniciAdi = textBox2.Text;
            string sifre = textBox1.Text;

            tabaniBaglantisi.connection();

            string query = "SELECT COUNT(*) FROM Kullanici WHERE KullaniciAdi = @kullaniciAdi AND Sifre = @sifre";
            OleDbCommand command = new OleDbCommand(query, tabaniBaglantisi.connection());
            command.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
            command.Parameters.AddWithValue("@sifre", sifre);

            int count = (int)command.ExecuteScalar();

            tabaniBaglantisi.connection().Close();

            if (count > 0)
            {
                Menu menu = new Menu();
                menu.Show();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
            };
        }
    }

}

