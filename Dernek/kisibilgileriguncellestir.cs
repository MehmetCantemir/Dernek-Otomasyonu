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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Dernek
{
    public partial class kisibilgileriguncellestir : Form
    {
        public kisibilgileriguncellestir()
        {
            InitializeComponent();
        }

        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\mehme\\Desktop\\Dernek\\Dernek\\Data\\DernekVT.accdb");
        private void kisibilgileriguncellestir_Load(object sender, EventArgs e)
        {
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglan.Open();
            int TC = Convert.ToInt32(textBox1.Text);
            string selectquery = "SELECT * FROM Kisi where TC = @TC";
            OleDbCommand komut = new OleDbCommand(selectquery,baglan);
            komut.Parameters.AddWithValue("@TC", TC);

            OleDbDataAdapter adapter = new OleDbDataAdapter(komut);
            OleDbDataReader reader=komut.ExecuteReader();

            if(reader.Read()) {

                MessageBox.Show("Üye bulundu");
            }
            else
            {
                MessageBox.Show("Üye Yoktur");
            }

            baglan.Close(); 

        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglan.Open();
            string queryUpdate = "UPDATE Kisi SET Aktif=@Aktif WHERE TC=@TC" ;

            OleDbCommand komut = new OleDbCommand(queryUpdate,baglan);
            komut.Parameters.AddWithValue("@TC",Convert.ToInt32(textBox1.Text));
            komut.Parameters.AddWithValue("@Aktif", checkBox1.Checked);
            komut.ExecuteNonQuery();

            bool a = checkBox1.Checked;
            MessageBox.Show(a.ToString());

            baglan.Close();
            MessageBox.Show("Müşteri bilgileri güncellendi");

        }
    }
}



    

   
        

 