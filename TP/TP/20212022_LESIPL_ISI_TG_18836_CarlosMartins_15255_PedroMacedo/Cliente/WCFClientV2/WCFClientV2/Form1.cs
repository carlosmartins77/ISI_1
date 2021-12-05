using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;
using System.Xml;
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
            // Criar um data set que vai receber os dados recebidos pela função abaixo
            DataSet ds = new DataSet();
            //Obtem os dados de todas as pessoas
            ds = client.GetAllPessoas();
            // Apresenta os mesmos dados
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

            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    // Diretoria inicial
                    openFileDialog.InitialDirectory = "d:\\temp\\Data";
                    // Tipo de ficheiros a apresentar no explorador
                    openFileDialog.Filter = "xml files (*.xml)All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    // Guardar a diretoria onde o explorador é fechado ou selecionado o ficheiro
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Obter o caminho do ficheiro escohido
                        filePath = openFileDialog.FileName;

                        // Le o conteudo do ficheiro para uma stream
                        var fileStream = openFileDialog.OpenFile();

                        // Criar uma streamreader para ler o conteudo do ficheiro
                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            // Ler até ao fim do ficheiro
                            fileContent = reader.ReadToEnd();
                            // Apresentar uma messagebox com o seu conteudo de modo a confirmar
                            MessageBox.Show(fileContent);

                        }
                        // Executa a funçao do servidor
                        client.RelatorioPSP(fileContent);
                    }
                }
            }
            catch (XmlException xmlexception)
            {
                labelErro.Text = xmlexception.Message;
            }
            catch (Exception exception)
            {
                labelErro.Text = exception.Message;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Variaveis para receber conteudo e caminho
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Diretorio inicial
                openFileDialog.InitialDirectory = "d:\\temp\\Data";
                // Tipo de ficheiros a apresentar no explorador
                openFileDialog.Filter = "json files (*.json)All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                // Salvar a localizaçao da diretoriasempre que é fechado
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Obter o caminho do ficheiro escohido
                    filePath = openFileDialog.FileName;

                    // Le o conteudo do ficheiro para uma stream
                    var fileStream = openFileDialog.OpenFile();
                    
                    // Criar uma streamreader para ler o conteudo do ficheiro
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        // Ler até ao fim do ficheiro
                        fileContent = reader.ReadToEnd();
                        // Apresentar uma messagebox com o seu conteudo de modo a confirmar
                        MessageBox.Show(fileContent);

                    }
                    // Executa a funçao do servidor
                    client.RelatorioGNR(fileContent);

                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EncomendasANDProdutos encomendasAndProdutos = new EncomendasANDProdutos();
            encomendasAndProdutos.Show();
        }

        private void textBoxProduct_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelCasos_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DadosAtuaisCOVID dadosatuais = new DadosAtuaisCOVID();
            dadosatuais.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Criar um data ste que vai receber os dados da funçao abaixo
            DataSet ds = new DataSet();
            // Obtem os dados da funçao GetTestados
            ds = client.GetTestados();
            // Apresenta os mesmos dados
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Criar um data ste que vai receber os dados da funçao abaixo
            DataSet ds = new DataSet();
            // Obtem os dados da funçao GetTestados
            ds = client.GetInfetados();
            // Apresenta os mesmos dados
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

