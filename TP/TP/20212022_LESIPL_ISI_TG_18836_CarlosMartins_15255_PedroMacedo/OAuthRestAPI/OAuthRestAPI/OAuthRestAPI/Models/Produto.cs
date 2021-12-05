using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuthRestAPI.Models
{
    public class Produto
    {
        #region Atributos
        int idproduto;
        string nome;
        string sku;
        double preco;

        #endregion

        #region Construtores
        public Produto()
        {

        }

        public Produto(int id, string n, string s, double p)
        {
            idproduto = id;
            nome = n;
            sku = s;
            preco = p;
        }

        #endregion

        #region Propriedades

        public int IdProduto
        {
            get { return idproduto; }
            set { idproduto = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }


        public string Sku
        {
            get { return sku; }
            set { sku = value; }
        }


        public double Preco
        {
            get { return preco; }
            set { preco = value; }
        }

        #endregion

    }
}
