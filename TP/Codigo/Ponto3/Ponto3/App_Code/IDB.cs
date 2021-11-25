using System;
using System.Data;
using System.ServiceModel;

/// <summary>
/// Summary description for IDB
/// </summary>
/// 
/// 

[ServiceContract]
public interface IDB
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