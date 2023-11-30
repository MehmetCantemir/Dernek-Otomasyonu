using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.ComponentModel;

namespace Dernek.Data
{


    //C:\Users\mehme\Desktop\Dernek\Dernek\Data
    internal class VTConnection
    {
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\mehme\\Desktop\\Dernek\\Dernek\\Data\\DernekVT.accdb");


        public void VTConnect()
        { 
            baglan.Open();
        }

        public void VTDisConnect()
        {
            baglan.Close();
        }
        public void baglantiKontrol()
        {
            
            if (baglan.State != ConnectionState.Open)       
                MessageBox.Show("Bağlantı yok");
            
            else         
                MessageBox.Show("Bağlantı var");  
             
        }
        /*
        public string[] veriCek()
        {
            string query = "SELECT * FROM Kisi";
            OleDbCommand veriCek = new OleDbCommand(query, baglan);
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(veriCek);
            
            DataTable data = new DataTable();
            oleDbDataAdapter.Fill(data);
            int i = 0;

            string[] names=new string[data.Columns.Count];
            while (i<=data.Columns.Count)
            {
                names[i] = data.Rows[i]["Adi"].ToString();
                //MessageBox.Show(names[i]);
                //MessageBox.Show(data.Rows[i]["Adi"].ToString());
                i++;
            }
            return names;
            
        }
        */
    }

    public class Tarih
    {
        public int Kimlik { get; set; }
        public int AidatID { get; set; }

        public int Ay {  get; set; }

        public int Yil {  get; set; }
    
    }

    public class Aidat { 
    
        public int AidatId { get; set; }

        public int AidatMiktari { get; set; }
        public bool Odendimi { get; set; }
        public int TC { get; set; }

        Tarih Tarih { get; set; }

    }
    public class Kisi
    { 
        public int TCKimlik { get; set;}
        public string Adi { get; set;}

        public string Soyadi { get; set;}
        public string KanGrubu { get; set;}
        public string Sehir { get; set;}

        public string Aktif { get; set; }

        Aidat aidat { get; set; }


    }

    /*
     * public class DataAccess 
    {
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\mehme\\Desktop\\Dernek\\Dernek\\Data\\DernekVT.accdb");
        public Kisi KisiBilgileri(int TC)
        { 
            Kisi kisi = new Kisi();
            baglan.Open();

            string query = "SELECT * FROM Kisi WHERE TC=@TC";
            OleDbCommand cmd= new OleDbCommand(query,baglan);
            cmd.Parameters.AddWithValue("@TC", TC);

            OleDbDataReader reader = cmd.ExecuteReader();
            
            if(reader.Read())
            {
                kisi.TCKimlik = Convert.ToInt32(reader["TC"]);
                kisi.Adi=reader["Adi"].ToString();
                kisi.Soyadi = reader["Soyadi"].ToString();
                kisi.KanGrubu = reader["Kan_Grubu"].ToString();
                kisi.Sehir = reader["Sehir"].ToString();
                kisi.Aktif = reader["Aktif"].ToString();
            }
            
            baglan.Close();
            return kisi;
        }

        public Aidat AidatBilgileri(int TC)
        {
            Aidat aidat = new Aidat();
            baglan.Open();
            string query = "SELECT * FROM Aidat WHERE TC=@TC";
            OleDbCommand komut = new OleDbCommand(query, baglan);
            komut.Parameters.AddWithValue("@TC", TC);
            OleDbDataReader reader = komut.ExecuteReader();
            if(reader.Read())
            {
                aidat.AidatId = Convert.ToInt32(reader["AidatID"]);
                aidat.TC = Convert.ToInt32(reader["TC"]);
                aidat.AidatMiktari = Convert.ToInt32(reader["AidatMiktari"]);
                aidat.AidatTarihi = Convert.ToDateTime(reader["AidatTarihi"]);
                aidat.Odendimi = Convert.ToBoolean(reader["Odendimi"]);
            }
            baglan.Close();

            return aidat;
        }

        
    
    }
    */

}
