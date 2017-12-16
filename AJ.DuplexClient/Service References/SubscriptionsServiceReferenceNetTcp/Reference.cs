﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AJ.DuplexClient.SubscriptionsServiceReferenceNetTcp {
    using System.Runtime.Serialization;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Notification", Namespace="http://schemas.datacontract.org/2004/07/AJ.DuplexService.Subscriptions")]
    public enum Notification : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ClientAdded = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ClientRemoved = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MessageAvailable = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn://ajdotnet.duplexservice/subscriptions", ConfigurationName="SubscriptionsServiceReferenceNetTcp.ISubscriptionService", CallbackContract=typeof(AJ.DuplexClient.SubscriptionsServiceReferenceNetTcp.ISubscriptionServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface ISubscriptionService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="urn://ajdotnet.duplexservice/subscriptions/ISubscriptionService/Subscribe")]
        void Subscribe(string clientName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="urn://ajdotnet.duplexservice/subscriptions/ISubscriptionService/Subscribe")]
        System.Threading.Tasks.Task SubscribeAsync(string clientName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, IsTerminating=true, IsInitiating=false, Action="urn://ajdotnet.duplexservice/subscriptions/ISubscriptionService/Unsubscribe")]
        void Unsubscribe();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, IsTerminating=true, IsInitiating=false, Action="urn://ajdotnet.duplexservice/subscriptions/ISubscriptionService/Unsubscribe")]
        System.Threading.Tasks.Task UnsubscribeAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISubscriptionServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn://ajdotnet.duplexservice/subscriptions/ISubscriptionService/Notify", ReplyAction="urn://ajdotnet.duplexservice/subscriptions/ISubscriptionService/NotifyResponse")]
        void Notify(AJ.DuplexClient.SubscriptionsServiceReferenceNetTcp.Notification notification, string clientName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISubscriptionServiceChannel : AJ.DuplexClient.SubscriptionsServiceReferenceNetTcp.ISubscriptionService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SubscriptionServiceClient : System.ServiceModel.DuplexClientBase<AJ.DuplexClient.SubscriptionsServiceReferenceNetTcp.ISubscriptionService>, AJ.DuplexClient.SubscriptionsServiceReferenceNetTcp.ISubscriptionService {
        
        public SubscriptionServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public SubscriptionServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public SubscriptionServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public SubscriptionServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public SubscriptionServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Subscribe(string clientName) {
            base.Channel.Subscribe(clientName);
        }
        
        public System.Threading.Tasks.Task SubscribeAsync(string clientName) {
            return base.Channel.SubscribeAsync(clientName);
        }
        
        public void Unsubscribe() {
            base.Channel.Unsubscribe();
        }
        
        public System.Threading.Tasks.Task UnsubscribeAsync() {
            return base.Channel.UnsubscribeAsync();
        }
    }
}