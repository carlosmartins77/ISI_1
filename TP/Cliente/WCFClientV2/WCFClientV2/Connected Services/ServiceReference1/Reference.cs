﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCFClientV2.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Produto", Namespace="http://schemas.datacontract.org/2004/07/")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(WCFClientV2.ServiceReference1.Encomenda))]
    public partial class Produto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdProdutoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NomeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double PrecoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SkuField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StockField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdProduto {
            get {
                return this.IdProdutoField;
            }
            set {
                if ((this.IdProdutoField.Equals(value) != true)) {
                    this.IdProdutoField = value;
                    this.RaisePropertyChanged("IdProduto");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Nome {
            get {
                return this.NomeField;
            }
            set {
                if ((object.ReferenceEquals(this.NomeField, value) != true)) {
                    this.NomeField = value;
                    this.RaisePropertyChanged("Nome");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Preco {
            get {
                return this.PrecoField;
            }
            set {
                if ((this.PrecoField.Equals(value) != true)) {
                    this.PrecoField = value;
                    this.RaisePropertyChanged("Preco");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Sku {
            get {
                return this.SkuField;
            }
            set {
                if ((object.ReferenceEquals(this.SkuField, value) != true)) {
                    this.SkuField = value;
                    this.RaisePropertyChanged("Sku");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Stock {
            get {
                return this.StockField;
            }
            set {
                if ((this.StockField.Equals(value) != true)) {
                    this.StockField = value;
                    this.RaisePropertyChanged("Stock");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Encomenda", Namespace="http://schemas.datacontract.org/2004/07/")]
    [System.SerializableAttribute()]
    public partial class Encomenda : WCFClientV2.ServiceReference1.Produto {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool EstadoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdEquipaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WCFClientV2.ServiceReference1.Produto ProdutoXField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int QuantidadeField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Data {
            get {
                return this.DataField;
            }
            set {
                if ((this.DataField.Equals(value) != true)) {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Estado {
            get {
                return this.EstadoField;
            }
            set {
                if ((this.EstadoField.Equals(value) != true)) {
                    this.EstadoField = value;
                    this.RaisePropertyChanged("Estado");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdEquipa {
            get {
                return this.IdEquipaField;
            }
            set {
                if ((this.IdEquipaField.Equals(value) != true)) {
                    this.IdEquipaField = value;
                    this.RaisePropertyChanged("IdEquipa");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WCFClientV2.ServiceReference1.Produto ProdutoX {
            get {
                return this.ProdutoXField;
            }
            set {
                if ((object.ReferenceEquals(this.ProdutoXField, value) != true)) {
                    this.ProdutoXField = value;
                    this.RaisePropertyChanged("ProdutoX");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Quantidade {
            get {
                return this.QuantidadeField;
            }
            set {
                if ((this.QuantidadeField.Equals(value) != true)) {
                    this.QuantidadeField = value;
                    this.RaisePropertyChanged("Quantidade");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IDBRest")]
    public interface IDBRest {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBRest/FindProduct", ReplyAction="http://tempuri.org/IDBRest/FindProductResponse")]
        bool FindProduct(string nome);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBRest/FindProduct", ReplyAction="http://tempuri.org/IDBRest/FindProductResponse")]
        System.Threading.Tasks.Task<bool> FindProductAsync(string nome);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBRest/CreateProduct", ReplyAction="http://tempuri.org/IDBRest/CreateProductResponse")]
        bool CreateProduct(WCFClientV2.ServiceReference1.Produto p);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBRest/CreateProduct", ReplyAction="http://tempuri.org/IDBRest/CreateProductResponse")]
        System.Threading.Tasks.Task<bool> CreateProductAsync(WCFClientV2.ServiceReference1.Produto p);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBRest/ListProducts", ReplyAction="http://tempuri.org/IDBRest/ListProductsResponse")]
        WCFClientV2.ServiceReference1.Produto[] ListProducts();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBRest/ListProducts", ReplyAction="http://tempuri.org/IDBRest/ListProductsResponse")]
        System.Threading.Tasks.Task<WCFClientV2.ServiceReference1.Produto[]> ListProductsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBRest/DeleteProduct", ReplyAction="http://tempuri.org/IDBRest/DeleteProductResponse")]
        string DeleteProduct(string sku);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBRest/DeleteProduct", ReplyAction="http://tempuri.org/IDBRest/DeleteProductResponse")]
        System.Threading.Tasks.Task<string> DeleteProductAsync(string sku);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBRest/MakeOrder", ReplyAction="http://tempuri.org/IDBRest/MakeOrderResponse")]
        bool MakeOrder(WCFClientV2.ServiceReference1.Encomenda e);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBRest/MakeOrder", ReplyAction="http://tempuri.org/IDBRest/MakeOrderResponse")]
        System.Threading.Tasks.Task<bool> MakeOrderAsync(WCFClientV2.ServiceReference1.Encomenda e);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBRest/AddProductToOrder", ReplyAction="http://tempuri.org/IDBRest/AddProductToOrderResponse")]
        bool AddProductToOrder(WCFClientV2.ServiceReference1.Encomenda e);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBRest/AddProductToOrder", ReplyAction="http://tempuri.org/IDBRest/AddProductToOrderResponse")]
        System.Threading.Tasks.Task<bool> AddProductToOrderAsync(WCFClientV2.ServiceReference1.Encomenda e);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDBRestChannel : WCFClientV2.ServiceReference1.IDBRest, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DBRestClient : System.ServiceModel.ClientBase<WCFClientV2.ServiceReference1.IDBRest>, WCFClientV2.ServiceReference1.IDBRest {
        
        public DBRestClient() {
        }
        
        public DBRestClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DBRestClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DBRestClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DBRestClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool FindProduct(string nome) {
            return base.Channel.FindProduct(nome);
        }
        
        public System.Threading.Tasks.Task<bool> FindProductAsync(string nome) {
            return base.Channel.FindProductAsync(nome);
        }
        
        public bool CreateProduct(WCFClientV2.ServiceReference1.Produto p) {
            return base.Channel.CreateProduct(p);
        }
        
        public System.Threading.Tasks.Task<bool> CreateProductAsync(WCFClientV2.ServiceReference1.Produto p) {
            return base.Channel.CreateProductAsync(p);
        }
        
        public WCFClientV2.ServiceReference1.Produto[] ListProducts() {
            return base.Channel.ListProducts();
        }
        
        public System.Threading.Tasks.Task<WCFClientV2.ServiceReference1.Produto[]> ListProductsAsync() {
            return base.Channel.ListProductsAsync();
        }
        
        public string DeleteProduct(string sku) {
            return base.Channel.DeleteProduct(sku);
        }
        
        public System.Threading.Tasks.Task<string> DeleteProductAsync(string sku) {
            return base.Channel.DeleteProductAsync(sku);
        }
        
        public bool MakeOrder(WCFClientV2.ServiceReference1.Encomenda e) {
            return base.Channel.MakeOrder(e);
        }
        
        public System.Threading.Tasks.Task<bool> MakeOrderAsync(WCFClientV2.ServiceReference1.Encomenda e) {
            return base.Channel.MakeOrderAsync(e);
        }
        
        public bool AddProductToOrder(WCFClientV2.ServiceReference1.Encomenda e) {
            return base.Channel.AddProductToOrder(e);
        }
        
        public System.Threading.Tasks.Task<bool> AddProductToOrderAsync(WCFClientV2.ServiceReference1.Encomenda e) {
            return base.Channel.AddProductToOrderAsync(e);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IDBSoap")]
    public interface IDBSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBSoap/FindNif", ReplyAction="http://tempuri.org/IDBSoap/FindNifResponse")]
        bool FindNif(string nif);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBSoap/FindNif", ReplyAction="http://tempuri.org/IDBSoap/FindNifResponse")]
        System.Threading.Tasks.Task<bool> FindNifAsync(string nif);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBSoap/GetAllPessoas", ReplyAction="http://tempuri.org/IDBSoap/GetAllPessoasResponse")]
        System.Data.DataSet GetAllPessoas();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBSoap/GetAllPessoas", ReplyAction="http://tempuri.org/IDBSoap/GetAllPessoasResponse")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetAllPessoasAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBSoap/RegistInfected", ReplyAction="http://tempuri.org/IDBSoap/RegistInfectedResponse")]
        void RegistInfected(string nif);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBSoap/RegistInfected", ReplyAction="http://tempuri.org/IDBSoap/RegistInfectedResponse")]
        System.Threading.Tasks.Task RegistInfectedAsync(string nif);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBSoap/RegistIsolated", ReplyAction="http://tempuri.org/IDBSoap/RegistIsolatedResponse")]
        void RegistIsolated(string contacto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBSoap/RegistIsolated", ReplyAction="http://tempuri.org/IDBSoap/RegistIsolatedResponse")]
        System.Threading.Tasks.Task RegistIsolatedAsync(string contacto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBSoap/RelatorioPSP", ReplyAction="http://tempuri.org/IDBSoap/RelatorioPSPResponse")]
        void RelatorioPSP(string file);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBSoap/RelatorioPSP", ReplyAction="http://tempuri.org/IDBSoap/RelatorioPSPResponse")]
        System.Threading.Tasks.Task RelatorioPSPAsync(string file);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBSoap/RelatorioGNR", ReplyAction="http://tempuri.org/IDBSoap/RelatorioGNRResponse")]
        void RelatorioGNR(string file);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBSoap/RelatorioGNR", ReplyAction="http://tempuri.org/IDBSoap/RelatorioGNRResponse")]
        System.Threading.Tasks.Task RelatorioGNRAsync(string file);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBSoap/GetEncomendasPendentes", ReplyAction="http://tempuri.org/IDBSoap/GetEncomendasPendentesResponse")]
        System.Data.DataSet GetEncomendasPendentes(bool estado);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDBSoap/GetEncomendasPendentes", ReplyAction="http://tempuri.org/IDBSoap/GetEncomendasPendentesResponse")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetEncomendasPendentesAsync(bool estado);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDBSoapChannel : WCFClientV2.ServiceReference1.IDBSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DBSoapClient : System.ServiceModel.ClientBase<WCFClientV2.ServiceReference1.IDBSoap>, WCFClientV2.ServiceReference1.IDBSoap {
        
        public DBSoapClient() {
        }
        
        public DBSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DBSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DBSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DBSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool FindNif(string nif) {
            return base.Channel.FindNif(nif);
        }
        
        public System.Threading.Tasks.Task<bool> FindNifAsync(string nif) {
            return base.Channel.FindNifAsync(nif);
        }
        
        public System.Data.DataSet GetAllPessoas() {
            return base.Channel.GetAllPessoas();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetAllPessoasAsync() {
            return base.Channel.GetAllPessoasAsync();
        }
        
        public void RegistInfected(string nif) {
            base.Channel.RegistInfected(nif);
        }
        
        public System.Threading.Tasks.Task RegistInfectedAsync(string nif) {
            return base.Channel.RegistInfectedAsync(nif);
        }
        
        public void RegistIsolated(string contacto) {
            base.Channel.RegistIsolated(contacto);
        }
        
        public System.Threading.Tasks.Task RegistIsolatedAsync(string contacto) {
            return base.Channel.RegistIsolatedAsync(contacto);
        }
        
        public void RelatorioPSP(string file) {
            base.Channel.RelatorioPSP(file);
        }
        
        public System.Threading.Tasks.Task RelatorioPSPAsync(string file) {
            return base.Channel.RelatorioPSPAsync(file);
        }
        
        public void RelatorioGNR(string file) {
            base.Channel.RelatorioGNR(file);
        }
        
        public System.Threading.Tasks.Task RelatorioGNRAsync(string file) {
            return base.Channel.RelatorioGNRAsync(file);
        }
        
        public System.Data.DataSet GetEncomendasPendentes(bool estado) {
            return base.Channel.GetEncomendasPendentes(estado);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetEncomendasPendentesAsync(bool estado) {
            return base.Channel.GetEncomendasPendentesAsync(estado);
        }
    }
}
