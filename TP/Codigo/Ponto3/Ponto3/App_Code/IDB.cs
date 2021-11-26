using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

/// <summary>
/// Summary description for IDB
/// </summary>
/// 
/// 
#region SOAP

[ServiceContract]
public interface IDBSoap
{
    [OperationContract]
    bool FindNif(string nif);

    [OperationContract]
    DataSet GetAllPessoas();

    [OperationContract]
    void RegistInfected(string nif);

    [OperationContract]
    void RegistIsolated(string contacto);

    [OperationContract]
    void RelatorioPSP(string file);

    [OperationContract]
    void RelatorioGNR(string file);

}
#endregion

#region REST 
[ServiceContract]
public interface IDBRest
{
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "FindProduct/{nome}")]
    bool FindProduct(string nome);

    [OperationContract]
    [WebInvoke(ResponseFormat = WebMessageFormat.Json, UriTemplate = "CreateProduct")]
    bool CreateProduct(Produto p);

    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "ListProducts")]
    List<Produto> ListProducts();
}

[DataContract]
public class Produto
{
    #region Atributos
    string nome;
    double preco;

    #endregion

    #region Construtores
    public Produto()
    {

    }

    public Produto(string n, double p)
    {
        nome = n;
        p = preco;
    }

    #endregion

    #region Propriedades
    [DataMember]
    public string Nome
    {
        get { return nome; }
        set { nome = value; }
    }

    [DataMember]
    public double Preco
    {
        get { return preco; }
        set { preco = value; }
    }
    #endregion

}


#endregion