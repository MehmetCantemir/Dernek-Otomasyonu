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
using System.Windows.Input;
using VeriKatmani;

namespace Dernek
{
    public partial class tarih : Form
    {
        public tarih()
        {
            InitializeComponent();
            DateTime istenenTarih = new DateTime(2023, 1, 1);
            dateBaslangic.Value = istenenTarih;
            dateBitis.Value = istenenTarih;

        }


        VeriTabaniBaglantisi tabaniBaglantisi = new VeriTabaniBaglantisi();


        public void Listele()
        {
            DateTime baslangicTarihi = dateBaslangic.Value;
            DateTime bitisTarihi = dateBitis.Value;
            
            DataTable sonuclar= AidatDurumuGetir(baslangicTarihi, bitisTarihi);
            dataGridView1.DataSource= sonuclar;
        }


        public DataTable AidatDurumuGetir(DateTime baslangicTarihi , DateTime bitisTarihi)
        {
            string query = "SELECT Kisi.TC, Kisi.Adi, Kisi.Soyadi, Aidat.AidatMiktari, Aidat.OdendiMi, Aidat.AidatTarihi, Aidat.Borc " +
                          "FROM Kisi " +
                          "INNER JOIN Aidat ON Kisi.TC = Aidat.TC " +
                          "WHERE Aidat.AidatTarihi BETWEEN @BaslangicTarihi AND @BitisTarihi";
            OleDbCommand komut = new OleDbCommand(query, tabaniBaglantisi.connection());
            komut.Parameters.AddWithValue("@BaslangicTarihi", baslangicTarihi);
            komut.Parameters.AddWithValue("@BitisTarihi", bitisTarihi);

            OleDbDataAdapter adapter =  new OleDbDataAdapter(komut);
            DataTable dt=new DataTable();
            adapter.Fill(dt);

            return dt;

        }
        private void tarih_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Listele();
        }
    }
}
