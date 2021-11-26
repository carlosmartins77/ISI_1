using System;
using System.Data;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;
using WCFClientV2.ServiceReference1;

namespace WCFClientV2
{
    public partial class Form1 : Form
    {
        DBSoapClient client = new DBSoapClient();
        public Form1()
        {
            InitializeComponent();
            
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = client.GetAllPessoas();
            dataGridView1.DataSource = ds.Tables[0];
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

        private void button5_Click(object sender, EventArgs e)
        {
            // URI: http://localhost:50151/Service.svc/rest/CheckProduct/{nome}

            //1º Definir URI
            StringBuilder uri = new StringBuilder();
            uri.Append("http://localhost:50151/Service.svc/rest/");
            uri.Append($"FindProduct/{textBoxProduct.Text}");

            #region Prepara Pedido
            HttpWebRequest request = WebRequest.Create(uri.ToString()) as HttpWebRequest;
            #endregion

            #region Faz pedido e analisa resposta
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string message = String.Format("GET falhou. Recebido HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(bool));
                object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                bool jsonResponse = (bool)objResponse;// ou "as Result";

                MessageBox.Show(jsonResponse.ToString());
            }
            #endregion
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EncomendasANDProdutos encomendasAndProdutos = new EncomendasANDProdutos();
            encomendasAndProdutos.Show();
        }
    }
}
