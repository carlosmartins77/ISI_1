using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WCFClientV2.ServiceReference1;

namespace WCFClientV2
{
    public partial class RegistaContagiado : Form
    {
        DBSoapClient client = new DBSoapClient(); //Establecer ligaçao com o servidor

        public RegistaContagiado()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            try
            {
                if (client.FindNif(textBoxNif.Text) == true)
                {
                    client.RegistInfected(textBoxNif.Text);

                    InsereNumeroIsolados form3 = new InsereNumeroIsolados();
                    form3.Show();
                    this.Close(); //Fechar form de inserir o nif do infetado
                }
                else
                {
                    MessageBox.Show("Insira um NIF válido");
                }
            }
            catch (Exception x)
            {

                MessageBox.Show(x.Message);
            }
            
        }

        private void textBoxNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
