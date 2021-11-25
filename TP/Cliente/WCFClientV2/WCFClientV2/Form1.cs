using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using WCFClientV2.ServiceReference1;

namespace WCFClientV2
{
    public partial class Form1 : Form
    {
        DBClient client = new DBClient();
        public Form1()
        {
            InitializeComponent();
            
        }
        private void Show_Data()
        {
            DataSet ds = new DataSet();
            ds = client.GetAllPessoas();
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Show_Data();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegistaContagiado form2 = new RegistaContagiado();

            form2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "d:\\temp\\Data";
                openFileDialog.Filter = "xml files (*.xml)All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                        MessageBox.Show(fileContent);

                    }

                    client.RelatorioPSP(fileContent);

                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "d:\\temp\\Data";
                openFileDialog.Filter = "json files (*.json)All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                        MessageBox.Show(fileContent);

                    }

                    client.RelatorioGNR(fileContent);

                }
            }
        }
    }
}
