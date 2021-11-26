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
    public partial class CriarProduto : Form
    {
        public CriarProduto()
        {
            InitializeComponent();
        }

        private void CriarProduto_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Produto p = new Produto()
            {
                Nome = textBoxNomeProduto.Text,
                Preco = Convert.ToDouble(textBoxPrecoProduto.Text)
            };

            // Serializa o Produto
            DataContractJsonSerializer objseria = new DataContractJsonSerializer(typeof(Produto));
            // Cria uma instancia (1) para guardar o produto(2)
            MemoryStream mem = new MemoryStream();
            objseria.WriteObject(mem, p);

            //Vai buscar o conteudo na MemoryStream e guarda-o na variavel data com formato UTF8
            string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;

            webClient.UploadString("http://localhost:50151/Service.svc/rest/CreateProduct", "POST", data);

            //Como mostrar se deu true ou false? Como aparece no postman?
        }
    }
}
