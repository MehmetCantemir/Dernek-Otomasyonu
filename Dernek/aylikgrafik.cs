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
    public partial class aylikgrafik : Form
    {
        public aylikgrafik()
        {
            InitializeComponent();
        }

        VeriTabaniBaglantisi tabaniBaglantisi = new VeriTabaniBaglantisi();
        

        private void aylikgrafik_Load(object sender, EventArgs e)
        {
            GrafikCiz();
        }

        private void GrafikCiz()
        {
            ZedGraphControl zedGraphControl = new ZedGraphControl();
            zedGraphControl.Dock = DockStyle.Fill;
            this.Controls.Add(zedGraphControl);

            GraphPane myPane = zedGraphControl.GraphPane;
            myPane.Title.Text = "Aidat Gelirinin Grafiği";
            myPane.XAxis.Title.Text = "Zaman";
            myPane.YAxis.Title.Text = "Aidat Geliri";


            DataTable monthlyData = GetMonthlyAidatData();
            PointPairList monthlyPoints = new PointPairList();


            foreach (DataRow row in monthlyData.Rows)
            {
                DateTime date = DateTime.Parse(row["AidatTarihi"].ToString());
                double amount = Convert.ToDouble(row["ToplamAidatMiktari"]);
                monthlyPoints.Add(new XDate(date), amount);
            }

            LineItem monthlyLine = myPane.AddCurve("Aylık Aidat Geliri", monthlyPoints, System.Drawing.Color.Blue, SymbolType.Circle);
            monthlyLine.Line.IsSmooth = true;

            DataTable yearlyData = GetYearlyAidatData();
            PointPairList yearlyPoints = new PointPairList();

            foreach (DataRow row in yearlyData.Rows)
            {
                int year = Convert.ToInt32(row["Yil"]);
                double amount = Convert.ToDouble(row["ToplamAidatMiktari"]);
                yearlyPoints.Add(new XDate(new DateTime(year, 1, 1)), amount);
            }

            LineItem yearlyLine = myPane.AddCurve("Yıllık Aidat Geliri", yearlyPoints, System.Drawing.Color.Red, SymbolType.Circle);
            yearlyLine.Line.IsSmooth = true;
            zedGraphControl.AxisChange();
        }


        private DataTable GetMonthlyAidatData()
        {
            string query = "SELECT AidatTarihi,SUM(AidatMiktari) AS ToplamAidatMiktari FROM Aidat GROUP BY AidatTarihi";
            using (OleDbCommand command = new OleDbCommand(query, tabaniBaglantisi.connection()))
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        private DataTable GetYearlyAidatData()
        {
            string query = "SELECT YEAR(AidatTarihi) AS Yil, SUM(AidatMiktari) AS ToplamAidatMiktari FROM Aidat GROUP BY YEAR(AidatTarihi)";
            using (OleDbCommand command = new OleDbCommand(query, tabaniBaglantisi.connection()))
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
    }
}
