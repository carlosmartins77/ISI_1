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
    public partial class DadosAtuaisCOVID : Form
    {
        public DadosAtuaisCOVID()
        {
            InitializeComponent();
            //Guardar a o URI do serviço externo numa stirng
            string url = "https://covid19-api.vost.pt/Requests/get_last_update";

            // Criar um request e atribuir o valor do URL ao mesmo
            WebRequest request = WebRequest.Create(url);

            // Verificar se a resposta está ok
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string message = String.Format("GET falhou. Recebido HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                // Serializar os produtos numa classe Root
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Root));
                object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                Root jsonResponse = (Root)objResponse;

                // Atribuir o texto das labels com os parametros desejados
                labelData.Text = Convert.ToString(jsonResponse.data);
                labelNovosCasos.Text = Convert.ToString(jsonResponse.confirmados_novos);
                labelCasosConfirmados.Text = Convert.ToString(jsonResponse.confirmados);
                labelObitos.Text = Convert.ToString(jsonResponse.obitos);
                labelCasosAtivos.Text = Convert.ToString(jsonResponse.ativos);
                labelInternados.Text = Convert.ToString(jsonResponse.internados);
                labelRTNac.Text = Convert.ToString(jsonResponse.rt_nacional);

            }


        }

        private void DadosAtuaisCOVID_Load(object sender, EventArgs e)
        {

        }
    }
}
