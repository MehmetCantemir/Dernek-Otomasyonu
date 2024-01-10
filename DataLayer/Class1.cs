using System.Data.OleDb;


namespace DataLayer
{
    public class Class1
    {


        
        //Veri tabanı erişim komutu ve yolu
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\mehme\\Desktop\\Dernek\\Dernek\\Data\\DernekVT.accdb");
        
        //Veri tabanı bağlantısı açma komutu
        public void VTConnect()
        {
            baglan.Open();
        }

        //Veri tabanı bağlantısı kapama komutu
        public void VTDisConnect()
        {
            baglan.Close();
        }

        // Veri tabanındaki "Aidat" tablosu içeriği
        public class Aidat
        {
            public int AidatID { get; set; }
            public int TC { get; set; } 
            public int AidatMiktari { get; set; }
            public bool Odendimi { get; set; }
            public string AidatAyi { get; set; }
            public Kisi Kisi { get; set; }

        }

        // Veri tabanındaki "Kişi" tablosu içeriği
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


    }
}
