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
    public partial class kangrubu : Form
    {
        public kangrubu()
        {
            InitializeComponent();
        }

        //VT Bağlantı Komutu
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\mehme\\Desktop\\Dernek\\Dernek\\Data\\DernekVT.accdb");

        private void kangrubu_Load(object sender, EventArgs e)
        {

           
        }
        int secim; 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            //Kan Grubu seçeneği seçilirse 2.combobaxda Kan Grubuna ait değerleri gösterir.
            if (comboBox1.SelectedIndex == 0)
            {

                comboBox2.Items.Add("a+");
                comboBox2.Items.Add("a-");
                comboBox2.Items.Add("b+");
                comboBox2.Items.Add("b-");
                comboBox2.Items.Add("ab+");
                comboBox2.Items.Add("ab-");
                comboBox2.Items.Add("0+");
                comboBox2.Items.Add("0-");
                secim = 0;

            }


            // Combobax 1 de şehir seçilirse combobax2 de şehirleri gösterir.
            else if (comboBox1.SelectedIndex == 1)
            {
                comboBox2.Items.Add("Düzce");
                comboBox2.Items.Add("İstanbul");
                comboBox2.Items.Add("Ankara");
                comboBox2.Items.Add("İzmir");
                comboBox2.Items.Add("Sakarya");
                comboBox2.Items.Add("Bolu");
                comboBox2.Items.Add("Zonguldak");
                comboBox2.Items.Add("Kocaeli");
                secim = 1;
            }


            // Combobax 1 de Aktif/Pasif seçilirse combobax2 de bu durumları gösterir.
            else if (comboBox1.SelectedIndex == 2)
            {
                comboBox2.Items.Add("True");
                comboBox2.Items.Add("False");
                secim = 2;

            }

            else 
            {
                MessageBox.Show("Değer seçin");

            }


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //combobax 1 de kan grubu seçilirse combobax 2 deki değere göre VT den bilgileri gösterme
            if (secim == 0)
            {
                string selectedKanGrubu = comboBox2.SelectedItem as string;
                baglan.Open();
                string query = "SELECT * FROM Kisi WHERE Kan_Grubu = @KanGrubu";
                OleDbCommand komut = new OleDbCommand(query, baglan);
                komut.Parameters.AddWithValue("@KanGrubu", selectedKanGrubu);
                OleDbDataAdapter adapter = new OleDbDataAdapter(komut);

                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                baglan.Close();
            }

            //combobax 1 de Şehir seçilirse combobax 2 deki değere göre VT den bilgileri gösterme
            else if (secim == 1)
            { 
                string selectedSehir = comboBox2.SelectedItem as string;
                baglan.Open();
                string query = "SELECT * FROM Kisi WHERE Sehir = @Sehir";
                OleDbCommand komut = new OleDbCommand(query, baglan);
                komut.Parameters.AddWithValue("@Sehir", selectedSehir);
                OleDbDataAdapter adapter = new OleDbDataAdapter(komut);

                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                baglan.Close();


            }

            //combobax 1 de Aktif-Pasif seçilirse combobax 2 deki değere göre VT den bilgileri gösterme
            else if (secim==2) 
            {
                string selectedDurum=comboBox2.SelectedItem as string;
                bool selectedAktif = selectedDurum.Equals("True", StringComparison.OrdinalIgnoreCase);  

                baglan.Open();
                string query = "SELECT * FROM Kisi WHERE Aktif = @Aktif";
                OleDbCommand komut = new OleDbCommand(query, baglan);
                komut.Parameters.AddWithValue("@Aktif", selectedAktif);
                OleDbDataAdapter adapter = new OleDbDataAdapter(komut);

                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                baglan.Close();
            }
        }
    }
}
