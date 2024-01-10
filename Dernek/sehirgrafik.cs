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
using VeriKatmani;
using ZedGraph;

namespace Dernek
{
    public partial class sehirgrafik : Form
    {
        public sehirgrafik()
        {
            InitializeComponent();
        }

        VeriTabaniBaglantisi tabaniBaglantisi = new VeriTabaniBaglantisi();

        private void sehirgrafik_Load(object sender, EventArgs e)
        {
           SehirGrafikCiz();
        }

        public void SehirGrafikCiz()
        {
            ZedGraphControl zedGraphControl = new ZedGraphControl();
            zedGraphControl.Dock = DockStyle.Fill;
            this.Controls.Add(zedGraphControl);


            GraphPane myPane = zedGraphControl.GraphPane;
            myPane.Title.Text = "Şehirlere Göre Üye Dağılımı";
            myPane.XAxis.Title.Text = "Şehir";
            myPane.YAxis.Title.Text = "Üye Sayısı";

            // Şehirlere göre üye sayıları
            DataTable cityData = GetCityDistribution();
            BarItem cityBar = myPane.AddBar("Üye Sayısı", null, cityData.AsEnumerable().Select(row => Convert.ToDouble(row["UyeSayisi"])).ToArray(), System.Drawing.Color.Blue);

            string[] cityLabels = cityData.AsEnumerable().Select(row => row["Sehir"].ToString()).ToArray();
            myPane.XAxis.Scale.TextLabels = new string[] { };

            for (int i = 0; i < cityLabels.Length; i++)
            {
                TextObj textLabel = new TextObj(cityLabels[i], i + 0.5, -0.1);
                textLabel.Location.AlignH = AlignH.Center;
                textLabel.Location.AlignV = AlignV.Top;
                textLabel.FontSpec.Angle = 90;
                myPane.GraphObjList.Add(textLabel);
            }
            zedGraphControl.AxisChange();

        }

        private DataTable GetCityDistribution()
        {
            string query = "SELECT Sehir, COUNT(*) AS UyeSayisi FROM Kisi GROUP BY Sehir";
            OleDbCommand command = new OleDbCommand(query, tabaniBaglantisi.connection());
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }
    }
}
