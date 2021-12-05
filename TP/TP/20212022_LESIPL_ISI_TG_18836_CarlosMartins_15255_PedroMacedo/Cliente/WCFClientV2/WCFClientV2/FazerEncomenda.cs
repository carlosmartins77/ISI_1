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
    public partial class FazerEncomenda : Form
    {
        public FazerEncomenda()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();       
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Criar um objeto encomenda que vai receber os dados inseridos pelo USER
                Encomenda encomenda = new Encomenda()
                {
                    IdProduto = int.Parse(textBoxProductID.Text),
                    Quantidade = int.Parse(textBoxQuantity.Text)
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

                webClient.UploadString("http://localhost:50151/Service.svc/rest/AddProductToOrder", "POST", data);
            }
            catch (ArgumentNullException x) // Caso o valor inserido seja invalido
            {
                MessageBox.Show("Valor invalido" + x.Message);
                
            }
            catch(Exception x) // Qualquer outro erro
            {
                MessageBox.Show("Erro" + x.Message);
            }
        }

        private void FazerEncomenda_Load(object sender, EventArgs e)
        {

        }
    }
}
