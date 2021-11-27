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
    [WebInvoke(Method="POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CreateProduct")]
    bool CreateProduct(Produto p);

    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "ListProducts")]
    List<Produto> ListProducts();

    [OperationContract]
    [WebInvoke(Method="DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "DeleteProduct/{sku}")]
    string DeleteProduct(string sku);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "MakeOrder")]
    bool MakeOrder(Encomenda e);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "AddProductToOrder")]
    bool AddProductToOrder(Encomenda e);

}

[DataContract]
public class Produto
{
    #region Atributos
    int idproduto;
    string nome;
    string sku;
    double preco;
    int stock;

    #endregion

    #region Construtores
    public Produto()
    {

    }

    public Produto(int id, string n, string s, double p, int stk)
    {
        idproduto = id;
        nome = n;
        sku = s;
        preco = p;
        stock = stk;
    }

    #endregion

    #region Propriedades
    
    [DataMember]
    public int IdProduto
    {
        get { return idproduto; }
        set { idproduto = value; }
    }

    [DataMember]
    public string Nome
    {
        get { return nome; }
        set { nome = value; }
    }

    [DataMember]
    public string Sku
    {
        get { return sku; }
        set { sku = value; }
    }

    [DataMember]
    public double Preco
    {
        get { return preco; }
        set { preco = value; }
    }

    [DataMember]
    public int Stock
    {
        get { return stock; }
        set { stock = value; }
    }
    #endregion

}

[DataContract]
public class Encomenda : Produto
{
    #region Atributos 
    Produto produtox;
    int quantidade;
    bool estado; //true -> entregue
    int idEquipa;
    DateTime data;

    #endregion

    #region Construtores
    public Encomenda()
    {

    }

    public Encomenda(Produto p, int q, bool e, int id, DateTime d)
    {
        produtox = p;
        quantidade = q;
        estado = e;
        idEquipa = id;
        data = d;
        
    }

    #endregion

    #region Propriedades
    [DataMember]
    public Produto ProdutoX
    {
        get { return produtox; }
        set { produtox = value; }
    }

    [DataMember]
    public int Quantidade
    {
        get { return quantidade; }
        set { quantidade = value; }
    }

    [DataMember]
    public bool Estado
    {
        get { return estado; }
        set { estado = value; }
    }

    [DataMember]
    public int IdEquipa
    {
        get { return idEquipa; }
        set { idEquipa = value; }
    }

    [DataMember]
    public DateTime Data
    {
        get { return data; }
        set { data = value; }
    }

    #endregion
}


#endregion