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
    public partial class RegistaIsolados : Form
    {
        DBSoapClient client = new DBSoapClient(); //Establecer ligaçao com o servidor
        public RegistaIsolados()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            try
            {
                client.RegistIsolated(textBox1.Text); //Criar uma funçao que valide a existencia do contacto na BD

                InsereNumeroIsolados form3 = new InsereNumeroIsolados();
                form3.Show();
                this.Close(); //Fechar form de inserir o nif do infetado
            }
            catch (Exception x)
            {

                MessageBox.Show(x.Message);
            }

        }
    }
}
