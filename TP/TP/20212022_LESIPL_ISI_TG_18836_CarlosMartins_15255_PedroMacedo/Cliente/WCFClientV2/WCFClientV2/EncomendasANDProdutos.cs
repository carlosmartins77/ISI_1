using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WCFClientV2.ServiceReference1;

namespace WCFClientV2
{
    public partial class EncomendasANDProdutos : Form
    {
        DBSoapClient client = new DBSoapClient();
        public EncomendasANDProdutos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {   // Janela para criar o produto
            CriarProduto criarProduto = new CriarProduto();
            // Abrir a janela criada
            criarProduto.Show();
            //Fechar a atual
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Criar uma variavel para receber o request
            HttpWebRequest request;

            // String para guardar o URI
            string url = "http://localhost:50151/Service.svc/rest/ListProducts";

            try
            {   // Atribuir o valor ao request
                request = WebRequest.Create(url) as HttpWebRequest;
                
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {       // Verificar se a resposta obtida é diferente de OK (200)
                    if (response.StatusCode != HttpStatusCode.OK)
                    {   // Se nao for, enviar erro
                        string message = String.Format("GET falhou. Recebido HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    DataSet ds = new DataSet();
                    // Converter o Stream num DataSet
                    ds.ReadXml(response.GetResponseStream());

                    //Popular a dataGrid com o conteudo do DataSet
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            catch (IndexOutOfRangeException) // IndexOutOfRange, neste caso porque como vamos pedir o valor 0 e ele não vai ter nada, entao NULL
            {
                MessageBox.Show("Não foram encontrados produtos");
            }
            catch(Exception) // Qualquer outro erro
            {
                MessageBox.Show("Erro");
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            // Criar uma variavel para receber o request
            HttpWebRequest request;

            // Guardar o valor de um IRL numa string interpolada que por sua vez recebe o input do utilzador na textBoxSKU
            string url = $"http://localhost:50151/Service.svc/rest/DeleteProduct/{textBoxSKU.Text}";

            // Atribuir o valor ao request
            request = WebRequest.Create(url) as HttpWebRequest;

            // Definir o método, por norma o GET é o predefinido
            request.Method = "DELETE"; // Sem isto, o programa não permite o DELETE ->  405

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {       // Verificar se a resposta obtida é diferente de OK (200)
                if (response.StatusCode != HttpStatusCode.OK)
                {   // Se nao for, enviar erro
                    string message = String.Format("DELETE falhou. Recebido HTTP {0}", response.StatusCode);
                }        
            }
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {   //Criar um objeto encomenda para receber o input do utilizador
                Encomenda encomenda = new Encomenda()
                {
                    IdEquipa = int.Parse(textBoxEquipaEncomenda.Text),
                    Data = DateTime.Now
                };

                // Serializa o Produto
                DataContractJsonSerializer objseria = new DataContractJsonSerializer(typeof(Encomenda));
                // Cria uma instancia (1) para guardar o produto(2)
                MemoryStream mem = new MemoryStream();
                objseria.WriteObject(mem, encomenda);

                //Vai buscar o conteudo na MemoryStream e guarda-o na variavel data com encoding UTF8
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;

                // Chama a função MakeOrder, que é um método post e envia o conteúdo data
                webClient.UploadString("http://localhost:50151/Service.svc/rest/MakeOrder", "POST", data);

                //Fechar esta janela
                this.Close();
                // Criar uma janela para aba de FazerEncomenda
                FazerEncomenda fazerEncomenda = new FazerEncomenda();
                // Abrir a janela de FazerEncomenda e não permitir que o utilizador utilize as restantes janelas
                fazerEncomenda.ShowDialog();
            }
            catch (FormatException) // Erro de texto..
            {
                MessageBox.Show("Insira um ID de equipa válido!");
            }
            catch(Exception x) // Qualquer outra exceção não detetada
            {
                MessageBox.Show("Erro!!" + x.Message);
            }
        }

        /// <summary>
        /// Listar encomendas pendentes
        /// </summary>
        private void button5_Click(object sender, EventArgs e)
        {
            //Criar um DataSet para receber os dados
            DataSet ds = new DataSet();
            // Guardar o valor de GetEncomendas (neste caso pendnetes -> false) no DataSet
            ds = client.GetEncomendas(false);
            //Popular a dataGrid com o resultado do DataSet
            dataGridView1.DataSource = ds.Tables[0];
        }

        /// <summary>
        /// Listar encomendas entregues
        /// </summary>
        private void button7_Click(object sender, EventArgs e)
        {
            //Criar um DataSet para receber os dados
            DataSet ds = new DataSet();
            // Guardar o valor de GetEncomendas (neste caso pendnetes -> false) no DataSet
            ds = client.GetEncomendas(true);
            //Popular a dataGrid com o conteudo do DataSet
            dataGridView1.DataSource = ds.Tables[0];
        }

        /// <summary>
        /// Listar os produtos mais encomendados
        /// </summary>
        private void button6_Click(object sender, EventArgs e)
        {
            //Criar um DataSet para receber os dados
            DataSet ds = new DataSet();
            // Popular o DataSet com os 5 produtos mais encomendados
            ds = client.GetMostOrderedProducts();
            //Popular a dataGrid com o resultado do DataSet
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
