using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                //Analisar XML
                //GetXMLData(response.GetResponseStream());  
            }


        }
    }
}
