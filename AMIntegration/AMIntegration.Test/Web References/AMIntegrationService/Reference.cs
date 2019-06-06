﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace WeCare.Test.AMIntegrationService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="AMIntegrationSoap12", Namespace="http://bankmuscat.com/AMIntegrationService")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(EntityBase))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AMIntegrationRes))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AMIntegrationReq))]
    public partial class AMIntegration : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetListofAMCustomerOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public AMIntegration() {
            this.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
            this.Url = global::WeCare.Test.Properties.Settings.Default.WeCare_Test_AMIntegrationService_AMIntegration;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetListofAMCustomerCompletedEventHandler GetListofAMCustomerCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://bankmuscat.com/AMIntegrationService/GetListofAMCustomer", RequestNamespace="http://bankmuscat.com/AMIntegrationService", ResponseNamespace="http://bankmuscat.com/AMIntegrationService", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public GetListofAMCustomerResponse GetListofAMCustomer(GetListofAMCustomerRequest GetListofAMCustomerReq) {
            object[] results = this.Invoke("GetListofAMCustomer", new object[] {
                        GetListofAMCustomerReq});
            return ((GetListofAMCustomerResponse)(results[0]));
        }
        
        /// <remarks/>
        public void GetListofAMCustomerAsync(GetListofAMCustomerRequest GetListofAMCustomerReq) {
            this.GetListofAMCustomerAsync(GetListofAMCustomerReq, null);
        }
        
        /// <remarks/>
        public void GetListofAMCustomerAsync(GetListofAMCustomerRequest GetListofAMCustomerReq, object userState) {
            if ((this.GetListofAMCustomerOperationCompleted == null)) {
                this.GetListofAMCustomerOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetListofAMCustomerOperationCompleted);
            }
            this.InvokeAsync("GetListofAMCustomer", new object[] {
                        GetListofAMCustomerReq}, this.GetListofAMCustomerOperationCompleted, userState);
        }
        
        private void OnGetListofAMCustomerOperationCompleted(object arg) {
            if ((this.GetListofAMCustomerCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetListofAMCustomerCompleted(this, new GetListofAMCustomerCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://bankmuscat.com/AMIntegrationService")]
    public partial class GetListofAMCustomerRequest : AMIntegrationReq {
        
        private string referenceIDField;
        
        private string channelField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string ReferenceID {
            get {
                return this.referenceIDField;
            }
            set {
                this.referenceIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Channel {
            get {
                return this.channelField;
            }
            set {
                this.channelField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(GetListofAMCustomerRequest))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://bankmuscat.com/AMIntegrationService")]
    public partial class AMIntegrationReq {
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AMCustomer))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://bankmuscat.com/AMIntegrationService")]
    public partial class EntityBase {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://bankmuscat.com/AMIntegrationService")]
    public partial class AMCustomer : EntityBase {
        
        private string deviceNoField;
        
        private string deviceTypeField;
        
        private string deviceRegionField;
        
        private string deviceLocationField;
        
        private string statusField;
        
        private string assignToGroupField;
        
        private string bankingWithField;
        
        private string cardTypeField;
        
        private string channelField;
        
        private string debitCardGroupField;
        
        private string creditCardGroupField;
        
        private string cashGroupField;
        
        private string chequeGroupField;
        
        private System.Nullable<System.DateTime> dateCreatedField;
        
        private System.Nullable<System.DateTime> dateModifiedField;
        
        /// <remarks/>
        public string DeviceNo {
            get {
                return this.deviceNoField;
            }
            set {
                this.deviceNoField = value;
            }
        }
        
        /// <remarks/>
        public string DeviceType {
            get {
                return this.deviceTypeField;
            }
            set {
                this.deviceTypeField = value;
            }
        }
        
        /// <remarks/>
        public string DeviceRegion {
            get {
                return this.deviceRegionField;
            }
            set {
                this.deviceRegionField = value;
            }
        }
        
        /// <remarks/>
        public string DeviceLocation {
            get {
                return this.deviceLocationField;
            }
            set {
                this.deviceLocationField = value;
            }
        }
        
        /// <remarks/>
        public string Status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }
        
        /// <remarks/>
        public string AssignToGroup {
            get {
                return this.assignToGroupField;
            }
            set {
                this.assignToGroupField = value;
            }
        }
        
        /// <remarks/>
        public string BankingWith {
            get {
                return this.bankingWithField;
            }
            set {
                this.bankingWithField = value;
            }
        }
        
        /// <remarks/>
        public string CardType {
            get {
                return this.cardTypeField;
            }
            set {
                this.cardTypeField = value;
            }
        }
        
        /// <remarks/>
        public string Channel {
            get {
                return this.channelField;
            }
            set {
                this.channelField = value;
            }
        }
        
        /// <remarks/>
        public string DebitCardGroup {
            get {
                return this.debitCardGroupField;
            }
            set {
                this.debitCardGroupField = value;
            }
        }
        
        /// <remarks/>
        public string CreditCardGroup {
            get {
                return this.creditCardGroupField;
            }
            set {
                this.creditCardGroupField = value;
            }
        }
        
        /// <remarks/>
        public string CashGroup {
            get {
                return this.cashGroupField;
            }
            set {
                this.cashGroupField = value;
            }
        }
        
        /// <remarks/>
        public string ChequeGroup {
            get {
                return this.chequeGroupField;
            }
            set {
                this.chequeGroupField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> DateCreated {
            get {
                return this.dateCreatedField;
            }
            set {
                this.dateCreatedField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> DateModified {
            get {
                return this.dateModifiedField;
            }
            set {
                this.dateModifiedField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(GetListofAMCustomerResponse))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://bankmuscat.com/AMIntegrationService")]
    public partial class AMIntegrationRes {
        
        private int statusCodeField;
        
        private string messageField;
        
        /// <remarks/>
        public int StatusCode {
            get {
                return this.statusCodeField;
            }
            set {
                this.statusCodeField = value;
            }
        }
        
        /// <remarks/>
        public string Message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://bankmuscat.com/AMIntegrationService")]
    public partial class GetListofAMCustomerResponse : AMIntegrationRes {
        
        private string referenceIDField;
        
        private AMCustomer[] listField;
        
        /// <remarks/>
        public string ReferenceID {
            get {
                return this.referenceIDField;
            }
            set {
                this.referenceIDField = value;
            }
        }
        
        /// <remarks/>
        public AMCustomer[] List {
            get {
                return this.listField;
            }
            set {
                this.listField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0")]
    public delegate void GetListofAMCustomerCompletedEventHandler(object sender, GetListofAMCustomerCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2053.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetListofAMCustomerCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetListofAMCustomerCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public GetListofAMCustomerResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((GetListofAMCustomerResponse)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591