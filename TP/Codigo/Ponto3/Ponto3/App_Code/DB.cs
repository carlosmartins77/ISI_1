using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using System.Web.Services;
using System.Xml;


/// <summary>
/// Trabalho Prático 1 - ISI 21/22
/// Autores:
/// 15... - Pedro Macedo
/// 18836 - Carlos Martins
/// Enunciado: https://elearning1.ipca.pt/2122/pluginfile.php/406401/mod_resource/content/1/20212022_LESI_ISI_TG1_Enunciado.pdf
/// Sistema gestão das operações das equipas que vão visitar os lares. (Gestão de infetados, isolados, recursos (para as equipas))
/// </summary>
public class DB : IDBSoap, IDBRest
{
    #region Ponto1
    public int AddRest(string x, string y)
    {
        return (int.Parse(x) + int.Parse(y));
    }

    /// <summary>
    /// Função auxiliar responsavel por verificar se um certo produto já existe na BD
    /// </summary>
    /// <param name="nome"> Nome do produtoXML </param>
    /// <returns>True se existir, false se não</returns>
    public bool FindProduct(string nome)
    {
        // Connection String
        string cs = ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString;

        // Open Connection
        SqlConnection con = new SqlConnection(cs);
        con.Open();

        // Comando com a query
        SqlCommand checkProduct = new SqlCommand("SELECT * FROM Produto WHERE Nome = @nome", con);

        // Parameterização do campo nif
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
    public bool CreateProduct(Produto p)
    {
        DataSet ds = new DataSet();

        // Ligação à BD
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString);

        // Query
        string query = "INSERT INTO Produto (Nome, Preco) VALUES(@Nome, @Preco)";

        // Executar
        SqlDataAdapter da = new SqlDataAdapter(query, con);

        // Parameterização 
        da.SelectCommand.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = p.Nome;
        da.SelectCommand.Parameters.Add("@Preco", SqlDbType.Float).Value = p.Preco;

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

    public List<Produto> ListProducts()
    {
        // Cria uma lista para armazenar os Hoteis
        ArrayList produtos = new ArrayList();

        // Conexao
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString);
        try
        {
            con.Open();
            // Comando com a query
            SqlCommand getProducts = new SqlCommand("SELECT Nome, Preco FROM Produto", con);

            // Agora lêmos a tabela e verificamos se ela possui linhas(se tiver o nome existe)
            SqlDataReader reader = getProducts.ExecuteReader();

            try
            {
                // Percorre o DataReader
                while (reader.Read())
                {
                    //Cria um objeto Produto
                    Produto p = new Produto();

                    // Instancia os parametros
                    p.Nome = (string)reader["Nome"];
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


    public DataSet ListProducts2()
    {
        DataSet ds = new DataSet();

        // Ligação à BD
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString);

        // Query
        string query = "SELECT Nome,Preco FROM Produto";

        // Executar
        SqlDataAdapter da = new SqlDataAdapter(query, con);

        //Encher a tabela com os dados obtidos
        da.Fill(ds, "Produto");

        return (ds);

    }

    #endregion

    //Criar encomenda

    #region Ponto3

    /// <summary>
    /// Dado um nif é feita uma pesquisa na base de dados para verificar a sua existencia
    /// É uma função auxiliar para registar infetados
    /// </summary>
    /// <param name="nif"></param>
    /// <returns>true se o nif existir, false se não existir na BD</returns> 
    public bool FindNif(string nif)
    {
        // Connection String
        string cs = ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString;

        // Open Connection
        SqlConnection con = new SqlConnection(cs);
        con.Open();
        
        // Comando com a query
        SqlCommand search_user = new SqlCommand("SELECT * FROM Pessoa WHERE Nif = @nif", con); 

        // Parameterização do campo nif
        search_user.Parameters.Add("@nif", SqlDbType.NVarChar).Value = nif;

        // Agora lêmos a tabela e verificamos se ela possui linhas(se tiver o nome existe)
        SqlDataReader reader = search_user.ExecuteReader();

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
    /// Função utilizada para retornar toda a informação da tabela Pessoa presente na Base de dados
    /// </summary>
    /// <returns></returns>
    public DataSet GetAllPessoas()
    {
        DataSet ds = new DataSet();

        // 1º Connection String no Web Config
        string cs = ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString;

        // 2º Open Connection
        SqlConnection con = new SqlConnection(cs);

        // 3º Query
        string q = "SELECT * FROM Pessoa";

        // 4º Executar

        SqlDataAdapter da = new SqlDataAdapter(q, con);

        da.Fill(ds, "Pessoa");

        return (ds);
    }

    /// <summary>
    /// Dado um nif de uma Pessoa cujo testou positivo à covid, peço os nifs dos pacientes com os quais este teve contacto e coloco-os em isolamento
    /// </summary>
    /// <param name="nif"></param>
    /// <returns></returns> 
    public void RegistInfected(string nif)
    {
        // Ligação
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand();
            // Criar um update comamand(?)
            SqlDataAdapter da = new SqlDataAdapter
            {
                UpdateCommand = new SqlCommand("UPDATE Pessoa SET Testado = 1, Infetado = 1, Isolamento = 1 WHERE Nif = @nif", con)
            };

            //Instanciar parâmetros
            da.UpdateCommand.Parameters.Add("@nif", SqlDbType.NVarChar).Value = nif;

            con.Open();
            da.UpdateCommand.ExecuteNonQuery();
            con.Close();
        }

    }

    /// <summary>
    /// É utilizada para complementar a função RegistInfected. É chamada apos um caso de infetado ser registado. Basicamente quando
    /// é registado um caso, no Cliente aparece uma textbox a perguntar com qunatas pessoas teve contacto e esta funcao
    /// eecebe o nif e coloca-as em isolamento
    /// </summary>
    /// <param name="nif"></param>
    /// <returns></returns> 
    public void RegistIsolated(string nif)
    {
        // Ligação
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand();
            // Criar um update comamand(?)
            SqlDataAdapter da = new SqlDataAdapter
            {
                UpdateCommand = new SqlCommand("UPDATE Pessoa SET Testado = 1, Infetado = 0, Isolamento = 1 WHERE Nif = @nif", con)
            };

            //Instanciar parâmetros
            da.UpdateCommand.Parameters.Add("@nif", SqlDbType.NVarChar).Value = nif;

            con.Open();
            da.UpdateCommand.ExecuteNonQuery();
            con.Close();
        }

    }

    #endregion

    #region Ponto4

    /// <summary>
    /// Função responsavel por receber um ficheiro XML e interpretar o seu conteudo e enviar para a BD. 
    /// Insere nas tabela VIsita e faz o UPDATE na tabela Pessoas de acordo o seu conteudo.
    /// </summary>
    /// <param name="file"> Ficheiro XML </param>
    /// <returns></returns>
    public void RelatorioPSP(string file)
    {
        //Variaveis para o conteudo da BD
        //int idVisita;
        DateTime dataVisita;
        int idPessoa;
        int infetado;
        int idEquipa;

        //Criar uma nova instancia de um documento XML
        XmlDocument xmlFile = new XmlDocument();
        xmlFile.LoadXml(file);

        //Percorrer os nodes do ficheiro XML
        foreach (XmlNode xmlNode in xmlFile.DocumentElement.ChildNodes) 
        {
            //Atribuir a cada variavel o respetivo filho
            //idVisita = Convert.ToInt32(xmlNode.ChildNodes[0].InnerText);
            dataVisita = Convert.ToDateTime(xmlNode.ChildNodes[0].InnerText);
            idPessoa = Convert.ToInt32(xmlNode.ChildNodes[1].InnerText);
            infetado = Convert.ToSByte(xmlNode.ChildNodes[2].InnerText);
            idEquipa = Convert.ToInt32(xmlNode.ChildNodes[3].InnerText);

            DataSet ds = new DataSet();

            // Ligação à BD
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString);

            // Query
            string query = "INSERT INTO Visita (Data, ID_PessoaFK, Infetado, ID_EquipaFK) VALUES(@dataVisita, @idPessoa, @infetado, @idEquipa)";

            // Executar
            SqlDataAdapter da = new SqlDataAdapter(query, con);

            // Parameterização 
            //da.SelectCommand.Parameters.Add("@idVisita", SqlDbType.Int).Value = idVisita;
            da.SelectCommand.Parameters.Add("@dataVisita", SqlDbType.Date).Value = dataVisita;
            da.SelectCommand.Parameters.Add("@idPessoa", SqlDbType.Int).Value = idPessoa;
            da.SelectCommand.Parameters.Add("@infetado", SqlDbType.Bit).Value = infetado;
            da.SelectCommand.Parameters.Add("@idEquipa", SqlDbType.Int).Value = idEquipa;

            // Se o campo Infetado estiver a 1, temos que fazer o UPDATE da respetiva pessoa na tabela Pessoas
            if (infetado == 1)
            {
                //SqlCommand cmdUpdate = new SqlCommand();
                // Criar um update comamand(?)
                SqlDataAdapter daUpdate = new SqlDataAdapter
                {
                    UpdateCommand = new SqlCommand("UPDATE Pessoa SET Testado = 1, Infetado = 1, Isolamento = 1 WHERE ID_Pessoa = @idPessoa", con)
                };

                //Instanciar parâmetros
                daUpdate.UpdateCommand.Parameters.Add("@idPessoa", SqlDbType.Int).Value = idPessoa;

                con.Open();
                daUpdate.UpdateCommand.ExecuteNonQuery();
                con.Close();
            }
            else if (infetado == 0)
            {
                //SqlCommand cmdUpdate = new SqlCommand();
                // Criar um update comamand(?)
                SqlDataAdapter daUpdate = new SqlDataAdapter
                {
                    UpdateCommand = new SqlCommand("UPDATE Pessoa SET Testado = 1, Infetado = 0, Isolamento = 0 WHERE ID_Pessoa = @idPessoa", con)
                };

                //Instanciar parâmetros
                daUpdate.UpdateCommand.Parameters.Add("@idPessoa", SqlDbType.Int).Value = idPessoa;

                con.Open();
                daUpdate.UpdateCommand.ExecuteNonQuery();
                con.Close();
            }

            // Preencher a tabela Visita
            da.Fill(ds, "Visita");
        }
    }


    /// <summary>
    /// Função responsavel por receber um ficheiro JSON e interpretar o seu conteudo e enviar para a BD. 
    /// Insere nas tabela VIsita e faz o UPDATE na tabela Pessoas de acordo o seu conteudo.
    /// </summary>
    /// <param name="file"> Ficheiro JSON </param>
    /// <returns></returns>
    public void RelatorioGNR(string file)
    {
        //Variaveis para o conteudo da BD
        //int idVisita;
        DateTime dataVisita;
        int idPessoa;
        int infetado;
        int idEquipa;

        //Criar uma nova instancia de um documento JSON
        // Tive que fazer instalaçao no nuGet Package do JSON
        // Interpretação baseada no exemplo do stor -> JSON Aula 9
        JsonDocument jsonFile = JsonDocument.Parse(file);

        JsonElement root = jsonFile.RootElement;
        JsonElement tempJsonElement = root.GetProperty("Visita");

        foreach(JsonElement temp in tempJsonElement.EnumerateArray())
        {
            //JsonElement jsonVisita = temp.GetProperty("CodVisita"); // ConverToInt32 Para nao estourar com nulls
            //idVisita = jsonVisita.GetInt32(); 

            JsonElement jsonData = temp.GetProperty("DataVisita");
            dataVisita = jsonData.GetDateTime();

            JsonElement jsonPessoa = temp.GetProperty("CodPessoa");
            idPessoa = jsonPessoa.GetInt32();

            JsonElement jsonInfetado = temp.GetProperty("Infetado");
            infetado = jsonInfetado.GetSByte();

            JsonElement jsonEquipa = temp.GetProperty("CodEquipa");
            idEquipa = jsonEquipa.GetInt32();

            DataSet ds = new DataSet();

            // Ligação à BD
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString);

            // Query
            string query = "INSERT INTO Visita (ID_PessoaFK, Data, Infetado, ID_EquipaFK) VALUES(@idPessoa, @dataVisita, @infetado, @idEquipa)";

            // Executar
            SqlDataAdapter da = new SqlDataAdapter(query, con);

            // Parameterização 
            //da.SelectCommand.Parameters.Add("@idVisita", SqlDbType.Int).Value = idVisita;
            da.SelectCommand.Parameters.Add("@dataVisita", SqlDbType.Date).Value = dataVisita;
            da.SelectCommand.Parameters.Add("@idPessoa", SqlDbType.Int).Value = idPessoa;
            da.SelectCommand.Parameters.Add("@infetado", SqlDbType.Bit).Value = infetado;
            da.SelectCommand.Parameters.Add("@idEquipa", SqlDbType.Int).Value = idEquipa;

            // Se o campo Infetado estiver a 1, temos que fazer o UPDATE da respetiva pessoa na tabela Pessoas
            if (infetado == 1)
            {
                //SqlCommand cmdUpdate = new SqlCommand();
                // Criar um update comamand(?)
                SqlDataAdapter daUpdate = new SqlDataAdapter
                {
                    UpdateCommand = new SqlCommand("UPDATE Pessoa SET Testado = 1, Infetado = 1, Isolamento = 1 WHERE ID_Pessoa = @idPessoa", con)
                };

                //Instanciar parâmetros
                daUpdate.UpdateCommand.Parameters.Add("@idPessoa", SqlDbType.Int).Value = idPessoa;

                con.Open();
                daUpdate.UpdateCommand.ExecuteNonQuery();
                con.Close();
            }
            else if (infetado == 0)
            {
                //SqlCommand cmdUpdate = new SqlCommand();
                // Criar um update comamand(?)
                SqlDataAdapter daUpdate = new SqlDataAdapter
                {
                    UpdateCommand = new SqlCommand("UPDATE Pessoa SET Testado = 1, Infetado = 0, Isolamento = 0 WHERE ID_Pessoa = @idPessoa", con)
                };

                //Instanciar parâmetros
                daUpdate.UpdateCommand.Parameters.Add("@idPessoa", SqlDbType.Int).Value = idPessoa;

                con.Open();
                daUpdate.UpdateCommand.ExecuteNonQuery();
                con.Close();
            }


            // Preencher a tabela Visita
            da.Fill(ds, "Visita");
        }

        
    
    }
    #endregion

}
