using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
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
    public bool CreateProduct(Produto p)
    {
        DataSet ds = new DataSet();

        // Ligação à BD
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString);

        // Query
        string query = "INSERT INTO Produto (Nome, Preco, Sku) VALUES(@Nome, @Preco, @Sku)";

        // Executar
        SqlDataAdapter da = new SqlDataAdapter(query, con);

        // Parameterização 
        da.SelectCommand.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = p.Nome;
        da.SelectCommand.Parameters.Add("@Preco", SqlDbType.Float).Value = p.Preco;
        da.SelectCommand.Parameters.Add("@Sku", SqlDbType.NVarChar).Value = p.Sku;


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
    /// Fução responsavel por devolver todos os produtos.
    /// Devolve uma lista de produtos em XML
    /// </summary>
    /// <returns>List de produtos</returns>
    public List<Produto> ListProducts()
    {
        // Cria uma lista para armazenar os produtos
        ArrayList produtos = new ArrayList();

        // Conexao
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString);
        try
        {
            con.Open();
            // Comando com a query
            SqlCommand getProducts = new SqlCommand("SELECT Id_Produto, Sku, Nome, Preco FROM Produto", con);

            // Agora lêmos a tabela e verificamos se ela possui linhas(se tiver o nome existe)
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

    /// <summary>
    /// Fução responsavel por apagar um produto.
    /// Devolve uma lista de produtos em XML
    /// </summary>
    /// <param name="sku"> Código do produto. Procura o SKU na lista e apaga o produto com o respetivo SKU.</param>
    /// <returns>Uma string com o resultado. "Sucess ou error"</returns>
    public string DeleteProduct(string sku)
    {
        // Ligação
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString))
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
                return "Deleted succesfully";
            }
            else
            {
                return "Error";
            }
           
        }



    }

    /// <summary>
    /// Fução responsavel por criar uma encomenda.
    /// </summary>
    /// <param name="e"> Objeto encomenda</param>
    /// <returns>True se os dados forem inseridos na BD, false se nao</returns>
    public bool MakeOrder(Encomenda e)
    {
        DataSet ds = new DataSet();

        // Ligação à BD
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString);

        // Query para criar um ID_Encomenda na tabela Encomenda
        string queryCriarEncomenda = "INSERT INTO Encomenda(ID_EquipaFK, Data, Estado)VALUES(@Id_EquipaFK, @Data, 0)";

        // Executar
        SqlDataAdapter da = new SqlDataAdapter(queryCriarEncomenda, con);

        da.SelectCommand.Parameters.Add("@ID_EquipaFK", SqlDbType.Int).Value = e.IdEquipa;
        da.SelectCommand.Parameters.Add("@Data", SqlDbType.Date).Value = e.Data;

        // Para este trabalho usei uma tabela intermediara EncomendasProdutos que guarda o IDProduto e o IDEncomenda e a quantidade
        // Assim conseguimos associar varios produtos a uma encomenda, por exemplo, ID_Encomenda | ID_Produto | Quantidade
        //                                                                                  1    |      1     |      1
        //                                                                                  1    |      2     |      1
        //
        //Com a query anterior é criada uma instancia de uma encomenda com um certo ID. Agora é preciso associar os produtos ao ID da encomenda
        // Para tal, precisamos de obter o ID da encomenda. Para obter o ID da encomenda criada, cheguei à conclusao que podia ir buscar o ultimo registo na tabela
        // Encomenda.  Esquema: https://imgur.com/a/uEtZZEn

        try
        {
            da.Fill(ds, "Encomenda"); //Adicionar à tabela
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }

    /// <summary>
    /// Fução responsavel por adicionar produtos a uma encomenda
    /// </summary>
    /// <param name="e"> Objeto encomenda</param>
    /// <returns>True se os dados forem inseridos na BD, false se nao</returns>
    public bool AddProductToOrder(Encomenda e)
    {
        DataSet ds = new DataSet();

        // Ligação à BD
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString);

        // Query para adicionar o produto à ultima instancia de encomenda criada.
        string queryAdicionarProduto = "INSERT INTO EncomendaProduto(ID_ProdutoFK, ID_EncomendaFK, Quantidade) VALUES(@ID_Produto, (SELECT TOP 1 ID_Encomenda FROM Encomenda ORDER BY ID_Encomenda DESC), @Quantidade)";

        // Executar
        SqlDataAdapter da = new SqlDataAdapter(queryAdicionarProduto, con);

        // Parameterização dos campos
        da.SelectCommand.Parameters.Add("@ID_Produto", SqlDbType.Int).Value = e.IdProduto;
        da.SelectCommand.Parameters.Add("@Quantidade", SqlDbType.Int).Value = e.Quantidade;

        try
        {
            da.Fill(ds, "EncomendaProduto"); //Adicionar à tabela
            return true;
        }
        catch (Exception)
        {
            return false;
        }


    }
    #endregion

    #region Ponto2

    /// <summary>
    /// Fução responsavel por obter encomendas
    /// </summary>
    /// <param name="estado"> Se o estado for true Ele devolve encomendas entregues, se não nao entregues</param>
    /// <returns>True se os dados forem inseridos na BD, false se nao</returns>
    public DataSet GetEncomendas(bool estado)
    {
        // Criar o dataset para receber os dados
        DataSet ds = new DataSet();

        // Connection string para establecer ligaçao com a DB
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString);

        // Obter todas as encomendas com um certo estado (true-> Entregue /false-> Nao entregue)
        string query = @"SELECT Encomenda.ID_EquipaFK, Encomenda.Estado, Encomenda.ID_Encomenda, Produto.ID_Produto, Produto.Nome, Produto.Preco, Encomenda.Data 
                        FROM Encomenda INNER JOIN
                        EncomendaProduto ON Encomenda.ID_Encomenda = EncomendaProduto.ID_EncomendaFK INNER JOIN
                        Produto ON EncomendaProduto.ID_ProdutoFK = Produto.ID_Produto
                        WHERE Encomenda.Estado = @Estado";

        //Executar o comando
        SqlDataAdapter da = new SqlDataAdapter(query, con);

        da.SelectCommand.Parameters.Add("@Estado", SqlDbType.Bit).Value = estado;

        da.Fill(ds);
        con.Close();

        return (ds);
    }

    #endregion

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

        try
        {
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
        catch (XmlException) 
        {
            throw new FaultException("Ficheiro XML inválido", new FaultCode("XmlFileFault"));
        }
        catch (Exception x)
        {
            throw new FaultException("Erro" + x.Message);
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

    #region Ponto 5

    /// <summary>
    /// Função responsavel por enviar os 5 produtos mais requisitados.
    /// </summary
    /// <returns></returns>
    public DataSet GetMostOrderedProducts()
    {
        // Criar o dataset para receber os dados
        DataSet ds = new DataSet();

        // Connection string para establecer ligaçao com a DB
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString);

        // Obter todas as encomendas com um certo estado (true-> Entregue /false-> Nao entregue)
        // Seleciona os 5 primeiros campos(Ordenados por ordem decrescente pq assim da os 5 c mais quantidade)
        // E utiliza um inner join para apenas bsucar o nome do produto
        string query = @"SELECT TOP(5) EncomendaProduto.ID_ProdutoFK, Produto.Nome, SUM(EncomendaProduto.Quantidade) As VendasTotais
                        FROM     EncomendaProduto INNER JOIN Produto ON EncomendaProduto.ID_ProdutoFK = Produto.ID_Produto
                        GROUP BY ID_ProdutoFK, Produto.Nome
                        ORDER BY VendasTotais DESC";

        //Executar o comando
        SqlDataAdapter da = new SqlDataAdapter(query, con);

        da.Fill(ds);
        con.Close();

        return (ds);
    }

    public DataSet GetMostExpensiveTeams()
    {
        // Criar o dataset para receber os dados
        DataSet ds = new DataSet();

        // Connection string para establecer ligaçao com a DB
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString);

        // Obter todas as encomendas com um certo estado (true-> Entregue /false-> Nao entregue)
        string query = @"SELECT TOP(5) EncomendaProduto.ID_ProdutoFK, Produto.Nome, SUM(EncomendaProduto.Quantidade) As VendasTotais
                        FROM     EncomendaProduto INNER JOIN Produto ON EncomendaProduto.ID_ProdutoFK = Produto.ID_Produto
                        GROUP BY ID_ProdutoFK, Produto.Nome
                        ORDER BY VendasTotais DESC";

        //Executar o comando
        SqlDataAdapter da = new SqlDataAdapter(query, con);

        da.Fill(ds);
        con.Close();

        return (ds);
    }

    #endregion

}
