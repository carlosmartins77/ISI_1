using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using System.Web.Services;
using System.Xml;


/// <summary>
/// Summary description for Pessoas
/// </summary>
public class DB : IDB
{
    #region Ponto3

    /// <summary>
    /// Dado um nif é feita uma pesquisa na base de dados para verificar a sua existencia
    /// É uma função auxiliar para registar infetados
    /// </summary>
    /// <param name="nif"></param>
    /// <returns>true se o nif existir, false se não existir na BD</returns> 
    [WebMethod(Description = "Verifica NIF")]
    public bool FindNif(string nif)
    {
        DataSet ds = new DataSet();
        // 1º Connection String no Web Config
        string cs = ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString;

        // 2º Open Connection
        SqlConnection con = new SqlConnection(cs);

        // 3º Query
        string query = "SELECT * FROM Pessoa WHERE Nif = @nif";

        SqlDataAdapter da = new SqlDataAdapter(query, con);
        da.SelectCommand.Parameters.Add("@nif", SqlDbType.NVarChar).Value = nif;
        da.Fill(ds, "Pessoa");

        try
        {
            //Guardar o resultado numa tabela 
            DataRow dr = ds.Tables[0].Rows[0];

            // Se o nif na tabela for equivalente ao nif recebido por parametro - true
            if (dr["nif"].Equals(nif))
            {
                return true;
            }
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }

        return true;
        
    }

    /// <summary>
    /// Devolve toda a informação sobre todos as Pessoa
    /// </summary>
    /// <returns></returns>
    [WebMethod(Description = "Selecciona todos os Pessoas")]
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
    [WebMethod(Description = "Registar Caso detetado")]
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

    [WebMethod(Description = "Isolar Pessoas")]
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
        int idVisita;
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
            idVisita = Convert.ToInt32(xmlNode.ChildNodes[0].InnerText);
            dataVisita = Convert.ToDateTime(xmlNode.ChildNodes[1].InnerText);
            idPessoa = Convert.ToInt32(xmlNode.ChildNodes[2].InnerText);
            infetado = Convert.ToSByte(xmlNode.ChildNodes[3].InnerText);
            idEquipa = Convert.ToInt32(xmlNode.ChildNodes[4].InnerText);

            DataSet ds = new DataSet();

            // Ligação à BD
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["tpISIConnectionString"].ConnectionString);

            // Query
            string query = "INSERT INTO Visita (ID_Visita, Data, ID_PessoaFK, Infetado, ID_EquipaFK) VALUES(@idVisita, @dataVisita, @idPessoa, @infetado, @idEquipa)";

            // Executar
            SqlDataAdapter da = new SqlDataAdapter(query, con);

            // Parameterização 
            da.SelectCommand.Parameters.Add("@idVisita", SqlDbType.Int).Value = idVisita;
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
        int idVisita;
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
            JsonElement jsonVisita = temp.GetProperty("CodVisita"); // ConverToInt32 Para nao estourar com nulls
            idVisita = jsonVisita.GetInt32(); 

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
            string query = "INSERT INTO Visita (ID_Visita, ID_PessoaFK, Data, Infetado, ID_EquipaFK) VALUES(@idVisita, @idPessoa, @dataVisita, @infetado, @idEquipa)";

            // Executar
            SqlDataAdapter da = new SqlDataAdapter(query, con);

            // Parameterização 
            da.SelectCommand.Parameters.Add("@idVisita", SqlDbType.Int).Value = idVisita;
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

        #endregion

    }

}
