using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Idoso
/// </summary>
public class Idoso
{
    #region Atributos

    private string nome;
    private DateTime dataNascimento;
    private string contacto;
    private DateTime dataTeste;
    private bool testado;
    private bool infetado;
    private bool isolamento;
    private string nif;
    private int idLarIdoso;

    #endregion

    #region Propriedades
    public string Nome
    {
        get { return nome; }
        set { nome = value; }
    }

    public DateTime DataNascimento
    {
        get { return dataNascimento; }
        set { dataNascimento = value; }
    }

    public string Contacto
    {
        get { return contacto; }
        set { contacto = value; }
    }

    public DateTime DataTeste
    {
        get { return dataTeste; }
        set { dataTeste = value; }
    }

    public bool Testado
    {
        get { return testado; }
        set { testado = value; }
    }

    public bool Infetado
    {
        get { return infetado; }
        set { infetado = value; }
    }

    public bool Isolamento
    {
        get { return isolamento; }
        set { isolamento = value; }
    }
    public string Nif
    {
        get { return nif; }
        set { nif = value; }
    }

    public int IdLarIdoso
    {
        get { return idLarIdoso; }
        set { idLarIdoso = value; }
    }


    #endregion

    #region Construtores
    //public Idoso()
    //{

    //}

    public Idoso(string nome, DateTime dataNascimento, string contacto, DateTime dataTeste, bool testado, bool infetado, bool isolamento, string nif, int idLarIdoso )
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        Contacto = contacto;
        DataTeste = dataTeste;
        Testado = testado;
        Infetado = infetado;
        Isolamento = isolamento;
        Nif = nif;
        IdLarIdoso = idLarIdoso;
    }
    #endregion

}

public interface IIdoso
{
    #region Propriedades

    string Nome
    {
        get;
        set;
    }

    DateTime DataNascimento
    {
        get;
        set;
    }

    string Contacto
    {
        get;
        set;
    }
    DateTime DataTeste
    {
        get;
        set;
    }

    bool Testado
    {
        get;
        set;
    }

    bool Infetado
    {
        get;
        set;
    }

    bool Isolamento
    {
        get;
        set;
    }

    string Nif
    {
        get;
        set;
    }

    int IdLarIdoso
    {
        get;
        set;
    } 


    #endregion
}