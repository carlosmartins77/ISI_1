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

        #region SKUGenerator
        // Gerador de string aleatória (https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings)

        private static Random random = new Random();
        public static string SkuGenerator(int length)
        {
            const string chars = "0123456789";
            return "SKU" + new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Produto produto = new Produto()
            {
                Nome = textBoxNomeProduto.Text,
                Preco = Convert.ToDouble(textBoxPrecoProduto.Text),
                Stock = Convert.ToInt32(textBoxQuantidade.Text),
                Sku = SkuGenerator(6)
            };

            // Serializa o Produto
            DataContractJsonSerializer objseria = new DataContractJsonSerializer(typeof(Produto));
            // Cria uma instancia (1) para guardar o produto(2)
            MemoryStream mem = new MemoryStream();
            objseria.WriteObject(mem, produto);

            //Vai buscar o conteudo na MemoryStream e guarda-o na variavel data com encoding UTF8
            string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;

            webClient.UploadString("http://localhost:50151/Service.svc/rest/CreateProduct", "POST", data);

            //Como mostrar se deu true ou false? Como aparece no postman?
        }
    }
}
