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

            string url = "https://covid19-api.vost.pt/Requests/get_last_update";

            WebRequest request = WebRequest.Create(url);

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string message = String.Format("GET falhou. Recebido HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Root));
                object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                Root jsonResponse = (Root)objResponse;

                labelData.Text = Convert.ToString(jsonResponse.data);
                labelNovosCasos.Text = Convert.ToString(jsonResponse.confirmados_novos);
                labelCasosConfirmados.Text = Convert.ToString(jsonResponse.confirmados);
                labelObitos.Text = Convert.ToString(jsonResponse.obitos);
                labelCasosAtivos.Text = Convert.ToString(jsonResponse.ativos);
                labelInternados.Text = Convert.ToString(jsonResponse.internados);
                labelRTContinente.Text = Convert.ToString(jsonResponse.rt_nacional);

            }


        }

       
    }
}
