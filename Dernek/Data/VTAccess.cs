using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;


namespace Dernek.Data
{
    internal class VTAccess
    {
       
    }

    public class Eris
    {
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\mehme\\Desktop\\Dernek\\Dernek\\Data\\DernekVT.accdb");
        public Kisi KisiBilgileri(int TC) {
        
            Kisi kisi = new Kisi();
            baglan.Open();
            string query = "SELECT * FROM Kisi WHERE TC=@TC";
            OleDbCommand cmd = new OleDbCommand(query,baglan);
            cmd.Parameters.AddWithValue("@TC", TC);

            OleDbDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                kisi.TCKimlik = Convert.ToInt32(reader["TC"]);
                kisi.Adi = reader["Adi"].ToString();
                kisi.Soyadi = reader["Soyadi"].ToString();
                kisi.KanGrubu = reader["Kan_Grubu"].ToString();
                kisi.Sehir = reader["Sehir"].ToString();
                kisi.Aktif = reader["Aktif"].ToString();

            }

            baglan.Close();
            return kisi;
        }
    }

}
