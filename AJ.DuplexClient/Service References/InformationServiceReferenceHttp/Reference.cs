﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AJ.DuplexClient.InformationServiceReferenceHttp {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MessageItem", Namespace="urn://ajdotnet.duplexservice/datacontract")]
    [System.SerializableAttribute()]
    public partial class MessageItem : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClientNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime TimestampField;
        
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
        public string ClientName {
            get {
                return this.ClientNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ClientNameField, value) != true)) {
                    this.ClientNameField = value;
                    this.RaisePropertyChanged("ClientName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Timestamp {
            get {
                return this.TimestampField;
            }
            set {
                if ((this.TimestampField.Equals(value) != true)) {
                    this.TimestampField = value;
                    this.RaisePropertyChanged("Timestamp");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn://ajdotnet.duplexservice/information", ConfigurationName="InformationServiceReferenceHttp.IInformationService")]
    public interface IInformationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn://ajdotnet.duplexservice/information/IInformationService/SendMessage", ReplyAction="urn://ajdotnet.duplexservice/information/IInformationService/SendMessageResponse")]
        void SendMessage(string clientName, string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn://ajdotnet.duplexservice/information/IInformationService/SendMessage", ReplyAction="urn://ajdotnet.duplexservice/information/IInformationService/SendMessageResponse")]
        System.Threading.Tasks.Task SendMessageAsync(string clientName, string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn://ajdotnet.duplexservice/information/IInformationService/GetMessages", ReplyAction="urn://ajdotnet.duplexservice/information/IInformationService/GetMessagesResponse")]
        AJ.DuplexClient.InformationServiceReferenceHttp.MessageItem[] GetMessages(System.DateTime after);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn://ajdotnet.duplexservice/information/IInformationService/GetMessages", ReplyAction="urn://ajdotnet.duplexservice/information/IInformationService/GetMessagesResponse")]
        System.Threading.Tasks.Task<AJ.DuplexClient.InformationServiceReferenceHttp.MessageItem[]> GetMessagesAsync(System.DateTime after);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn://ajdotnet.duplexservice/information/IInformationService/GetRegisteredClients" +
            "", ReplyAction="urn://ajdotnet.duplexservice/information/IInformationService/GetRegisteredClients" +
            "Response")]
        string[] GetRegisteredClients();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn://ajdotnet.duplexservice/information/IInformationService/GetRegisteredClients" +
            "", ReplyAction="urn://ajdotnet.duplexservice/information/IInformationService/GetRegisteredClients" +
            "Response")]
        System.Threading.Tasks.Task<string[]> GetRegisteredClientsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IInformationServiceChannel : AJ.DuplexClient.InformationServiceReferenceHttp.IInformationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class InformationServiceClient : System.ServiceModel.ClientBase<AJ.DuplexClient.InformationServiceReferenceHttp.IInformationService>, AJ.DuplexClient.InformationServiceReferenceHttp.IInformationService {
        
        public InformationServiceClient() {
        }
        
        public InformationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public InformationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public InformationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public InformationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void SendMessage(string clientName, string message) {
            base.Channel.SendMessage(clientName, message);
        }
        
        public System.Threading.Tasks.Task SendMessageAsync(string clientName, string message) {
            return base.Channel.SendMessageAsync(clientName, message);
        }
        
        public AJ.DuplexClient.InformationServiceReferenceHttp.MessageItem[] GetMessages(System.DateTime after) {
            return base.Channel.GetMessages(after);
        }
        
        public System.Threading.Tasks.Task<AJ.DuplexClient.InformationServiceReferenceHttp.MessageItem[]> GetMessagesAsync(System.DateTime after) {
            return base.Channel.GetMessagesAsync(after);
        }
        
        public string[] GetRegisteredClients() {
            return base.Channel.GetRegisteredClients();
        }
        
        public System.Threading.Tasks.Task<string[]> GetRegisteredClientsAsync() {
            return base.Channel.GetRegisteredClientsAsync();
        }
    }
}
