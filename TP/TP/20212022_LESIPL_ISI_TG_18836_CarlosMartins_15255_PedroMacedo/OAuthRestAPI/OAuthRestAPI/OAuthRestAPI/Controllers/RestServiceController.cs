using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OAuthRestAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OAuthRestAPI.Controllers
{
    public class RestServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("ListProducts")]
        public List<Produto> ListProducts()
        {
            // Cria uma lista para armazenar os produtos
            ArrayList produtos = new ArrayList();

            // Conexao
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString);
            string cs = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=TP_ISI;Integrated Security=True";
            //2º OpenConnection
            SqlConnection con = new SqlConnection(cs);

            try
            {
                con.Open();
                // Comando com a query
                SqlCommand getProducts = new SqlCommand("SELECT Id_Produto, Sku, Nome, Preco FROM Produto", con);

                // Agora lêmos a tabela
                SqlDataReader reader = getProducts.ExecuteReader();

                try
                {
                    // Percorre o DataReader
                    while (reader.Read())
                    {
                        // Cria um objeto Produto
                        Produto p = new Produto();

                        // Instancia os parametros
                        p.IdProduto = (int)reader["Id_Produto"];
                        p.Nome = (string)reader["Nome"];
                        p.Sku = (string)reader["Sku"];
                        p.Preco = (double)reader["Preco"];

                        // Insere no arrayList
                        produtos.Add(p);
                    }
                }
                finally
                {
                    // Fecha o DataReader
                    reader.Close();
                }
            }
            finally
            {
                // Fecha a conexão
                con.Close();
            }

            // Converter numa lista genérica com LINQ
            List<Produto> list = produtos.Cast<Produto>().ToList();
            return list;

        }

        // <summary>
        /// Função auxiliar responsavel por verificar se um certo produto já existe na BD
        /// </summary>
        /// <param name="nome"> Nome do produtoXML </param>
        /// <returns>True se existir, false se não</returns>
        [HttpGet("FindProduct")]
        public bool FindProduct(string nome)
        {
            string cs = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=TP_ISI;Integrated Security=True";
            //2º OpenConnection
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            // Comando com a query
            SqlCommand checkProduct = new SqlCommand("SELECT * FROM Produto WHERE Nome = @nome", con);

            // Parameterização do campo nome
            checkProduct.Parameters.Add("@nome", SqlDbType.NVarChar).Value = nome;

            // Agora lêmos a tabela e verificamos se ela possui linhas(se tiver o nome existe)
            SqlDataReader reader = checkProduct.ExecuteReader();

            if (reader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Função responsavel por criar um produto.
        /// Insere nas tabela VIsita e faz o UPDATE na tabela Pessoas de acordo o seu conteudo.
        /// </summary>
        /// <param name="p"> Objeto Produto, utilizado para fazer o pedido REST. Tive que criar uma classe obrigatoriamente porque não posso passar mais de 1 parametro </param>
        /// <returns>true se for inserido, false se não</returns>
        [HttpPost("CreateProduct")]
        public bool CreateProduct(Produto p)
        {
            //Criar um dataset para receber o produto
            DataSet ds = new DataSet();

            // Ligação à BD
            string cs = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=TP_ISI;Integrated Security=True";
            //2º OpenConnection
            SqlConnection con = new SqlConnection(cs);

            // Query
            string query = "INSERT INTO Produto (Nome, Preco, Sku) VALUES(@Nome, @Preco, @Sku)";

            // Executar
            SqlDataAdapter da = new SqlDataAdapter(query, con);

            // Parameterização 
            da.SelectCommand.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = p.Nome;
            da.SelectCommand.Parameters.Add("@Preco", SqlDbType.Float).Value = p.Preco;
            da.SelectCommand.Parameters.Add("@Sku", SqlDbType.NVarChar).Value = p.Sku;


            // SE encontrar o produto na tabela
            if (FindProduct(p.Nome) == true)
            {
                return false; //Não fazer nada
            }
            else
            {
                da.Fill(ds, "Produto"); //Adicionar à tabela
                return true;
            }
        }

        /// <summary>
        /// Fução responsavel por apagar um produto.
        /// Devolve uma lista de produtos em XML
        /// </summary>
        /// <param name="sku"> Código do produto. Procura o SKU na lista e apaga o produto com o respetivo SKU.</param>
        /// <returns>Um bool a caracterizar o resultado da operaçao (True- Apagado /false- nada)"</returns>
        [HttpDelete("DeleteProduct")]
        public bool DeleteProduct(string sku)
        {
            // Ligação à BD
            string cs = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=TP_ISI;Integrated Security=True";
            //2º OpenConnection
            SqlConnection con = new SqlConnection(cs);

            // Ligação
            using (con)
            {
                SqlCommand cmd = new SqlCommand();
                // Criar um comando
                SqlDataAdapter da = new SqlDataAdapter
                {
                    DeleteCommand = new SqlCommand("DELETE FROM Produto WHERE Sku = @Sku ", con)
                };

                //Instanciar parâmetros
                da.DeleteCommand.Parameters.Add("@sku", SqlDbType.NVarChar).Value = sku;

                con.Open();
                int result = da.DeleteCommand.ExecuteNonQuery();
                con.Close();

                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }


        /// <summary>
        /// Fução responsavel por alterar o preco de um produto
        /// Procura o SKU na lista e altera o preco do mesmo
        /// </summary>
        /// <param name="sku"> Código do produto. 
        /// <param name="preco"> Novo preco do produto.

        [HttpPut("UpdatePrice")]
        public bool UpdatePrice(double preco, string sku)
        {

            // Ligação à BD
            string cs = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=TP_ISI;Integrated Security=True";
            //2º OpenConnection
            SqlConnection con = new SqlConnection(cs);

            // Query para alterar o preco do produto
            SqlDataAdapter daUpdate = new SqlDataAdapter
            {
                UpdateCommand = new SqlCommand("UPDATE Produto SET PRECO = @Preco WHERE SKU = @SKU", con)
            };

            // Parameterização
            daUpdate.UpdateCommand.Parameters.Add("@SKU", SqlDbType.NVarChar).Value = sku;
            daUpdate.UpdateCommand.Parameters.Add("@Preco", SqlDbType.Float).Value = preco;


            try
            {
                // Abre a conexão 
                con.Open();
                // Executa o UpdateCommand (Query)
                daUpdate.UpdateCommand.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }

}
