using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriKatmani
{
    public class VeriTabaniBaglantisi
    {
        public OleDbConnection connection()
        {
            // Veri tabanının bilgisayar üzerindeki adresi
            string path = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\mehme\\Desktop\\Dernek\\DataLayer\\Dernek.accdb";

            //veri tabanı bağlantı komutu
            OleDbConnection connection1 = new OleDbConnection(path);

            //veri tabanına bağlantıyı açma komutu
            connection1.Open();
            return connection1;
        }
    }

    }
   public class Aidat
    {
        public int AidatID { get; set; }
        public int TC { get; set; }
       public int AidatMiktari { get; set; }
        public bool Odendimi { get; set; }
        public string AidatAyi { get; set; }
        public Kisi Kisi { get; set; }
    }

    public class Kisi
    {
        public int TCKimlik { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string KanGrubu { get; set; }
        public string Sehir { get; set; }
        public string Aktif { get; set; }
        public string EMail { get; set; }

        public ICollection<Aidat> aidat { get; } = new List<Aidat>();


    }
  

