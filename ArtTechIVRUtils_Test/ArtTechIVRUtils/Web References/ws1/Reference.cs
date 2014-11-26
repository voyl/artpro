﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.1.
// 
#pragma warning disable 1591

namespace ArtTechIVRUtils.ws1 {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="GazIvrWebServiceSoapBinding", Namespace="http://tempuri.org/")]
    public partial class GasIvrserviceagent : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback AboneSonDurumOperationCompleted;
        
        private System.Threading.SendOrPostCallback FaturaBilgiTelefonOperationCompleted;
        
        private System.Threading.SendOrPostCallback crmMusteriBulTelefonOperationCompleted;
        
        private System.Threading.SendOrPostCallback crmCagriBulTelefonOperationCompleted;
        
        private System.Threading.SendOrPostCallback SayOkGunuAboneNoOperationCompleted;
        
        private System.Threading.SendOrPostCallback crmMusteriBulAboneNoOperationCompleted;
        
        private System.Threading.SendOrPostCallback SayOkGunuTCNoOperationCompleted;
        
        private System.Threading.SendOrPostCallback FaturaBilgiAboneOperationCompleted;
        
        private System.Threading.SendOrPostCallback crmMusteriBulTCkimlikOperationCompleted;
        
        private System.Threading.SendOrPostCallback FaturaBilgiTCkimlikOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public GasIvrserviceagent() {
            this.Url = global::ArtTechIVRUtils.Properties.Settings.Default.ArtTechIVRUtils_ws1_GasIvr_serviceagent;
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
        public event AboneSonDurumCompletedEventHandler AboneSonDurumCompleted;
        
        /// <remarks/>
        public event FaturaBilgiTelefonCompletedEventHandler FaturaBilgiTelefonCompleted;
        
        /// <remarks/>
        public event crmMusteriBulTelefonCompletedEventHandler crmMusteriBulTelefonCompleted;
        
        /// <remarks/>
        public event crmCagriBulTelefonCompletedEventHandler crmCagriBulTelefonCompleted;
        
        /// <remarks/>
        public event SayOkGunuAboneNoCompletedEventHandler SayOkGunuAboneNoCompleted;
        
        /// <remarks/>
        public event crmMusteriBulAboneNoCompletedEventHandler crmMusteriBulAboneNoCompleted;
        
        /// <remarks/>
        public event SayOkGunuTCNoCompletedEventHandler SayOkGunuTCNoCompleted;
        
        /// <remarks/>
        public event FaturaBilgiAboneCompletedEventHandler FaturaBilgiAboneCompleted;
        
        /// <remarks/>
        public event crmMusteriBulTCkimlikCompletedEventHandler crmMusteriBulTCkimlikCompleted;
        
        /// <remarks/>
        public event FaturaBilgiTCkimlikCompletedEventHandler FaturaBilgiTCkimlikCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/AboneSonDurum", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string AboneSonDurum(string AboneNo) {
            object[] results = this.Invoke("AboneSonDurum", new object[] {
                        AboneNo});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void AboneSonDurumAsync(string AboneNo) {
            this.AboneSonDurumAsync(AboneNo, null);
        }
        
        /// <remarks/>
        public void AboneSonDurumAsync(string AboneNo, object userState) {
            if ((this.AboneSonDurumOperationCompleted == null)) {
                this.AboneSonDurumOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAboneSonDurumOperationCompleted);
            }
            this.InvokeAsync("AboneSonDurum", new object[] {
                        AboneNo}, this.AboneSonDurumOperationCompleted, userState);
        }
        
        private void OnAboneSonDurumOperationCompleted(object arg) {
            if ((this.AboneSonDurumCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AboneSonDurumCompleted(this, new AboneSonDurumCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/FaturaBilgiTelefon", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string FaturaBilgiTelefon(string Telefon) {
            object[] results = this.Invoke("FaturaBilgiTelefon", new object[] {
                        Telefon});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void FaturaBilgiTelefonAsync(string Telefon) {
            this.FaturaBilgiTelefonAsync(Telefon, null);
        }
        
        /// <remarks/>
        public void FaturaBilgiTelefonAsync(string Telefon, object userState) {
            if ((this.FaturaBilgiTelefonOperationCompleted == null)) {
                this.FaturaBilgiTelefonOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFaturaBilgiTelefonOperationCompleted);
            }
            this.InvokeAsync("FaturaBilgiTelefon", new object[] {
                        Telefon}, this.FaturaBilgiTelefonOperationCompleted, userState);
        }
        
        private void OnFaturaBilgiTelefonOperationCompleted(object arg) {
            if ((this.FaturaBilgiTelefonCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FaturaBilgiTelefonCompleted(this, new FaturaBilgiTelefonCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/crmMusteriBulTelefon", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string crmMusteriBulTelefon(string Telefon) {
            object[] results = this.Invoke("crmMusteriBulTelefon", new object[] {
                        Telefon});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void crmMusteriBulTelefonAsync(string Telefon) {
            this.crmMusteriBulTelefonAsync(Telefon, null);
        }
        
        /// <remarks/>
        public void crmMusteriBulTelefonAsync(string Telefon, object userState) {
            if ((this.crmMusteriBulTelefonOperationCompleted == null)) {
                this.crmMusteriBulTelefonOperationCompleted = new System.Threading.SendOrPostCallback(this.OncrmMusteriBulTelefonOperationCompleted);
            }
            this.InvokeAsync("crmMusteriBulTelefon", new object[] {
                        Telefon}, this.crmMusteriBulTelefonOperationCompleted, userState);
        }
        
        private void OncrmMusteriBulTelefonOperationCompleted(object arg) {
            if ((this.crmMusteriBulTelefonCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.crmMusteriBulTelefonCompleted(this, new crmMusteriBulTelefonCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/crmCagriBulTelefon", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string crmCagriBulTelefon(string Telefon) {
            object[] results = this.Invoke("crmCagriBulTelefon", new object[] {
                        Telefon});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void crmCagriBulTelefonAsync(string Telefon) {
            this.crmCagriBulTelefonAsync(Telefon, null);
        }
        
        /// <remarks/>
        public void crmCagriBulTelefonAsync(string Telefon, object userState) {
            if ((this.crmCagriBulTelefonOperationCompleted == null)) {
                this.crmCagriBulTelefonOperationCompleted = new System.Threading.SendOrPostCallback(this.OncrmCagriBulTelefonOperationCompleted);
            }
            this.InvokeAsync("crmCagriBulTelefon", new object[] {
                        Telefon}, this.crmCagriBulTelefonOperationCompleted, userState);
        }
        
        private void OncrmCagriBulTelefonOperationCompleted(object arg) {
            if ((this.crmCagriBulTelefonCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.crmCagriBulTelefonCompleted(this, new crmCagriBulTelefonCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SayOkGunuAboneNo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SayOkGunuAboneNo(string AboneNo) {
            object[] results = this.Invoke("SayOkGunuAboneNo", new object[] {
                        AboneNo});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void SayOkGunuAboneNoAsync(string AboneNo) {
            this.SayOkGunuAboneNoAsync(AboneNo, null);
        }
        
        /// <remarks/>
        public void SayOkGunuAboneNoAsync(string AboneNo, object userState) {
            if ((this.SayOkGunuAboneNoOperationCompleted == null)) {
                this.SayOkGunuAboneNoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSayOkGunuAboneNoOperationCompleted);
            }
            this.InvokeAsync("SayOkGunuAboneNo", new object[] {
                        AboneNo}, this.SayOkGunuAboneNoOperationCompleted, userState);
        }
        
        private void OnSayOkGunuAboneNoOperationCompleted(object arg) {
            if ((this.SayOkGunuAboneNoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SayOkGunuAboneNoCompleted(this, new SayOkGunuAboneNoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/crmMusteriBulAboneNo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string crmMusteriBulAboneNo(string AboneNo) {
            object[] results = this.Invoke("crmMusteriBulAboneNo", new object[] {
                        AboneNo});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void crmMusteriBulAboneNoAsync(string AboneNo) {
            this.crmMusteriBulAboneNoAsync(AboneNo, null);
        }
        
        /// <remarks/>
        public void crmMusteriBulAboneNoAsync(string AboneNo, object userState) {
            if ((this.crmMusteriBulAboneNoOperationCompleted == null)) {
                this.crmMusteriBulAboneNoOperationCompleted = new System.Threading.SendOrPostCallback(this.OncrmMusteriBulAboneNoOperationCompleted);
            }
            this.InvokeAsync("crmMusteriBulAboneNo", new object[] {
                        AboneNo}, this.crmMusteriBulAboneNoOperationCompleted, userState);
        }
        
        private void OncrmMusteriBulAboneNoOperationCompleted(object arg) {
            if ((this.crmMusteriBulAboneNoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.crmMusteriBulAboneNoCompleted(this, new crmMusteriBulAboneNoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SayOkGunuTCNo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SayOkGunuTCNo(string TCkimlik) {
            object[] results = this.Invoke("SayOkGunuTCNo", new object[] {
                        TCkimlik});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void SayOkGunuTCNoAsync(string TCkimlik) {
            this.SayOkGunuTCNoAsync(TCkimlik, null);
        }
        
        /// <remarks/>
        public void SayOkGunuTCNoAsync(string TCkimlik, object userState) {
            if ((this.SayOkGunuTCNoOperationCompleted == null)) {
                this.SayOkGunuTCNoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSayOkGunuTCNoOperationCompleted);
            }
            this.InvokeAsync("SayOkGunuTCNo", new object[] {
                        TCkimlik}, this.SayOkGunuTCNoOperationCompleted, userState);
        }
        
        private void OnSayOkGunuTCNoOperationCompleted(object arg) {
            if ((this.SayOkGunuTCNoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SayOkGunuTCNoCompleted(this, new SayOkGunuTCNoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/FaturaBilgiAbone", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string FaturaBilgiAbone(string AboneNo) {
            object[] results = this.Invoke("FaturaBilgiAbone", new object[] {
                        AboneNo});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void FaturaBilgiAboneAsync(string AboneNo) {
            this.FaturaBilgiAboneAsync(AboneNo, null);
        }
        
        /// <remarks/>
        public void FaturaBilgiAboneAsync(string AboneNo, object userState) {
            if ((this.FaturaBilgiAboneOperationCompleted == null)) {
                this.FaturaBilgiAboneOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFaturaBilgiAboneOperationCompleted);
            }
            this.InvokeAsync("FaturaBilgiAbone", new object[] {
                        AboneNo}, this.FaturaBilgiAboneOperationCompleted, userState);
        }
        
        private void OnFaturaBilgiAboneOperationCompleted(object arg) {
            if ((this.FaturaBilgiAboneCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FaturaBilgiAboneCompleted(this, new FaturaBilgiAboneCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/crmMusteriBulTCkimlik", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string crmMusteriBulTCkimlik(string TCkimlik) {
            object[] results = this.Invoke("crmMusteriBulTCkimlik", new object[] {
                        TCkimlik});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void crmMusteriBulTCkimlikAsync(string TCkimlik) {
            this.crmMusteriBulTCkimlikAsync(TCkimlik, null);
        }
        
        /// <remarks/>
        public void crmMusteriBulTCkimlikAsync(string TCkimlik, object userState) {
            if ((this.crmMusteriBulTCkimlikOperationCompleted == null)) {
                this.crmMusteriBulTCkimlikOperationCompleted = new System.Threading.SendOrPostCallback(this.OncrmMusteriBulTCkimlikOperationCompleted);
            }
            this.InvokeAsync("crmMusteriBulTCkimlik", new object[] {
                        TCkimlik}, this.crmMusteriBulTCkimlikOperationCompleted, userState);
        }
        
        private void OncrmMusteriBulTCkimlikOperationCompleted(object arg) {
            if ((this.crmMusteriBulTCkimlikCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.crmMusteriBulTCkimlikCompleted(this, new crmMusteriBulTCkimlikCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/FaturaBilgiTCkimlik", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string FaturaBilgiTCkimlik(string TCkimlik) {
            object[] results = this.Invoke("FaturaBilgiTCkimlik", new object[] {
                        TCkimlik});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void FaturaBilgiTCkimlikAsync(string TCkimlik) {
            this.FaturaBilgiTCkimlikAsync(TCkimlik, null);
        }
        
        /// <remarks/>
        public void FaturaBilgiTCkimlikAsync(string TCkimlik, object userState) {
            if ((this.FaturaBilgiTCkimlikOperationCompleted == null)) {
                this.FaturaBilgiTCkimlikOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFaturaBilgiTCkimlikOperationCompleted);
            }
            this.InvokeAsync("FaturaBilgiTCkimlik", new object[] {
                        TCkimlik}, this.FaturaBilgiTCkimlikOperationCompleted, userState);
        }
        
        private void OnFaturaBilgiTCkimlikOperationCompleted(object arg) {
            if ((this.FaturaBilgiTCkimlikCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FaturaBilgiTCkimlikCompleted(this, new FaturaBilgiTCkimlikCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void AboneSonDurumCompletedEventHandler(object sender, AboneSonDurumCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AboneSonDurumCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AboneSonDurumCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void FaturaBilgiTelefonCompletedEventHandler(object sender, FaturaBilgiTelefonCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FaturaBilgiTelefonCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FaturaBilgiTelefonCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void crmMusteriBulTelefonCompletedEventHandler(object sender, crmMusteriBulTelefonCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class crmMusteriBulTelefonCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal crmMusteriBulTelefonCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void crmCagriBulTelefonCompletedEventHandler(object sender, crmCagriBulTelefonCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class crmCagriBulTelefonCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal crmCagriBulTelefonCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void SayOkGunuAboneNoCompletedEventHandler(object sender, SayOkGunuAboneNoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SayOkGunuAboneNoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SayOkGunuAboneNoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void crmMusteriBulAboneNoCompletedEventHandler(object sender, crmMusteriBulAboneNoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class crmMusteriBulAboneNoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal crmMusteriBulAboneNoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void SayOkGunuTCNoCompletedEventHandler(object sender, SayOkGunuTCNoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SayOkGunuTCNoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SayOkGunuTCNoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void FaturaBilgiAboneCompletedEventHandler(object sender, FaturaBilgiAboneCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FaturaBilgiAboneCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FaturaBilgiAboneCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void crmMusteriBulTCkimlikCompletedEventHandler(object sender, crmMusteriBulTCkimlikCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class crmMusteriBulTCkimlikCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal crmMusteriBulTCkimlikCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void FaturaBilgiTCkimlikCompletedEventHandler(object sender, FaturaBilgiTCkimlikCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FaturaBilgiTCkimlikCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FaturaBilgiTCkimlikCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591