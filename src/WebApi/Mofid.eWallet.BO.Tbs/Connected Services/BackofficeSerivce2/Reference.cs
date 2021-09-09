﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BackofficeService2
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ExceptionMessage", Namespace="http://schemas.datacontract.org/2004/07/TadbirPardaz.TBS.Domain.Entities.Customer" +
        "Management")]
    public partial class ExceptionMessage : object
    {
        
        private int ErrorCodeField;
        
        private string MessageField;
        
        private System.Collections.Generic.KeyValuePair<string, BackofficeService2.PropertyValidatorReason>[] PropertyErrorsField;
        
        private string RawMessageField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ErrorCode
        {
            get
            {
                return this.ErrorCodeField;
            }
            set
            {
                this.ErrorCodeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message
        {
            get
            {
                return this.MessageField;
            }
            set
            {
                this.MessageField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.KeyValuePair<string, BackofficeService2.PropertyValidatorReason>[] PropertyErrors
        {
            get
            {
                return this.PropertyErrorsField;
            }
            set
            {
                this.PropertyErrorsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RawMessage
        {
            get
            {
                return this.RawMessageField;
            }
            set
            {
                this.RawMessageField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PropertyValidatorReason", Namespace="http://schemas.datacontract.org/2004/07/TadbirPardaz.Infrastructure.Entities.Exce" +
        "ptions")]
    public enum PropertyValidatorReason : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Empty = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        InvalidFormat = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Duplicate = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        InvalidData = 4,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CustomHeader", Namespace="http://schemas.datacontract.org/2004/07/TadbirPardaz.TBS.ServiceHost.Domain")]
    public partial class CustomHeader : object
    {
        
        private string AuthenticationTypeField;
        
        private string DelegateField;
        
        private string IpAddressField;
        
        private bool IsAuthenticatedField;
        
        private string NameField;
        
        private string NameSpaceField;
        
        private string PasswordField;
        
        private string[] RolesField;
        
        private string UserIdField;
        
        private string UserNameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AuthenticationType
        {
            get
            {
                return this.AuthenticationTypeField;
            }
            set
            {
                this.AuthenticationTypeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Delegate
        {
            get
            {
                return this.DelegateField;
            }
            set
            {
                this.DelegateField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IpAddress
        {
            get
            {
                return this.IpAddressField;
            }
            set
            {
                this.IpAddressField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsAuthenticated
        {
            get
            {
                return this.IsAuthenticatedField;
            }
            set
            {
                this.IsAuthenticatedField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NameSpace
        {
            get
            {
                return this.NameSpaceField;
            }
            set
            {
                this.NameSpaceField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password
        {
            get
            {
                return this.PasswordField;
            }
            set
            {
                this.PasswordField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] Roles
        {
            get
            {
                return this.RolesField;
            }
            set
            {
                this.RolesField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserId
        {
            get
            {
                return this.UserIdField;
            }
            set
            {
                this.UserIdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName
        {
            get
            {
                return this.UserNameField;
            }
            set
            {
                this.UserNameField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BackofficeSerivce2.ITBSDefaultExternalService")]
    public interface ITBSDefaultExternalService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITBSDefaultExternalService/ThrowException", ReplyAction="http://tempuri.org/ITBSDefaultExternalService/ThrowExceptionResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(BackofficeService2.ExceptionMessage), Action="http://tempuri.org/ITBSDefaultExternalService/ThrowExceptionExceptionMessageFault" +
            "", Name="ExceptionMessage", Namespace="http://schemas.datacontract.org/2004/07/TadbirPardaz.TBS.Domain.Entities.Customer" +
            "Management")]
        System.Threading.Tasks.Task ThrowExceptionAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITBSDefaultExternalService/GetEmptyMessage", ReplyAction="http://tempuri.org/ITBSDefaultExternalService/GetEmptyMessageResponse")]
        System.Threading.Tasks.Task<BackofficeService2.ExceptionMessage> GetEmptyMessageAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITBSDefaultExternalService/GetEmptyHeader", ReplyAction="http://tempuri.org/ITBSDefaultExternalService/GetEmptyHeaderResponse")]
        System.Threading.Tasks.Task<BackofficeService2.CustomHeader> GetEmptyHeaderAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface ITBSDefaultExternalServiceChannel : BackofficeService2.ITBSDefaultExternalService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class TBSDefaultExternalServiceClient : System.ServiceModel.ClientBase<BackofficeService2.ITBSDefaultExternalService>, BackofficeService2.ITBSDefaultExternalService
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public TBSDefaultExternalServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(TBSDefaultExternalServiceClient.GetBindingForEndpoint(endpointConfiguration), TBSDefaultExternalServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public TBSDefaultExternalServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(TBSDefaultExternalServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public TBSDefaultExternalServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(TBSDefaultExternalServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public TBSDefaultExternalServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task ThrowExceptionAsync()
        {
            return base.Channel.ThrowExceptionAsync();
        }
        
        public System.Threading.Tasks.Task<BackofficeService2.ExceptionMessage> GetEmptyMessageAsync()
        {
            return base.Channel.GetEmptyMessageAsync();
        }
        
        public System.Threading.Tasks.Task<BackofficeService2.CustomHeader> GetEmptyHeaderAsync()
        {
            return base.Channel.GetEmptyHeaderAsync();
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ITBSDefaultExternalService))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ITBSDefaultExternalService1))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ITBSDefaultExternalService1))
            {
                return new System.ServiceModel.EndpointAddress("http://tbs.emofid.com/TadbirPardaz.TBSDefaultExternalService");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_ITBSDefaultExternalService,
            
            BasicHttpBinding_ITBSDefaultExternalService1,
        }
    }
}
