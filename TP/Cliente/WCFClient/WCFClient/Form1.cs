using System;
using System.Data;
using System.Windows.Forms;
using WCFClient.Lar;

namespace WCFClient
{
    public partial class Form1 : Form
    {
        DBClient client = new DBClient();
        public Form1()
        {

            InitializeComponent();
            Showdata();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            Showdata();

        }

        private void Showdata()
        {
            DataSet ds = new DataSet();
            ds = client.GetAllIdosos();
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

        }
    }
}
