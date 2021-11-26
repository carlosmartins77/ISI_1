using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WCFClientV2
{
    public partial class InsereNumeroIsolados : Form
    {
        public InsereNumeroIsolados()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool tryAgain = true;
            int nPessoas;

            while (tryAgain)
            {
                try
                {
                    nPessoas = int.Parse(textBox1.Text);
                    tryAgain = false;

                    if (nPessoas == 0)
                    {
                        this.Close();
                    }

                    for (int i = 0; i < nPessoas; i++)
                    {
                        RegistaIsolados form4 = new RegistaIsolados();
                        form4.Show();
                        this.Close(); //Fechar o form de pedir isolados
                    }

                }
                catch (FormatException x)
                {
                    MessageBox.Show("Insira um número valido! " + x.Message);
                    tryAgain = false; //Para nao ficar preso na MessageBox
                }
            }
        }
    }
}
