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
        public EncomendasANDProdutos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CriarProduto criarProduto = new CriarProduto();
            criarProduto.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HttpWebRequest request;
            string url = "http://localhost:50151/Service.svc/rest/ListProducts";

            try
            {
                request = WebRequest.Create(url) as HttpWebRequest;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        string message = String.Format("GET falhou. Recebido HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    DataSet ds = new DataSet();
                    // converte o Stream num DataSet
                    ds.ReadXml(response.GetResponseStream());

                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Não foram encontrados produtos");
            }
            catch(Exception)
            {
                MessageBox.Show("Erro");
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            string url = $"http://localhost:50151/Service.svc/rest/DeleteProduct/{textBoxSKU.Text}";

            WebRequest request = WebRequest.Create(url);
            request.Method = "DELETE"; // Sem isto, o programa não permite o DELETE -> 405

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string message = String.Format("DELETE falhou. Recebido HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
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

            webClient.UploadString("http://localhost:50151/Service.svc/rest/MakeOrder", "POST", data);

            this.Close();
            FazerEncomenda fazerEncomenda = new FazerEncomenda();
            fazerEncomenda.ShowDialog();
        }
    }
}
