﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// This source code was auto-generated by wsdl, Version=4.0.30319.1.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="SI_BillingData_SBM_WebService_OBBinding", Namespace="http://pspcl.com/xi/SBM/IF0066_BillingData_Web_Service_100")]
public partial class WebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    private System.Threading.SendOrPostCallback SI_BillingData_SBM_WebService_OBOperationCompleted;
    
    /// <remarks/>
    public WebService() {
        this.Url = "http://sapdevpi1:50200/XISOAPAdapter/MessageServlet?senderParty=&senderService=SB" +
            "M_DV&receiverParty=&receiverService=&interface=SI_BillingData_SBM_WebService_OB&" +
            "interfaceNamespace=http%3A%2F%2Fpspcl.com%2Fxi%2FSBM%2FIF0066_BillingData_Web_Se" +
            "rvice_100";
    }
    
    /// <remarks/>
    public event SI_BillingData_SBM_WebService_OBCompletedEventHandler SI_BillingData_SBM_WebService_OBCompleted;
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
    [return: System.Xml.Serialization.XmlArrayAttribute("MT_SBMWebService_Res", Namespace="http://pspcl.com/xi/SBM/IF0066_BillingData_Web_Service_100")]
    [return: System.Xml.Serialization.XmlArrayItemAttribute("Records", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
    public DT_SBMWebService_ResRecords[] SI_BillingData_SBM_WebService_OB([System.Xml.Serialization.XmlArrayAttribute(Namespace="http://pspcl.com/xi/SBM/IF0066_BillingData_Web_Service_100")] [System.Xml.Serialization.XmlArrayItemAttribute("BillingData_SBM", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)] DT_SBMWebService_ReqBillingData_SBM[] MT_BillingData_SBM_WebService) {
        object[] results = this.Invoke("SI_BillingData_SBM_WebService_OB", new object[] {
                    MT_BillingData_SBM_WebService});
        return ((DT_SBMWebService_ResRecords[])(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginSI_BillingData_SBM_WebService_OB(DT_SBMWebService_ReqBillingData_SBM[] MT_BillingData_SBM_WebService, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("SI_BillingData_SBM_WebService_OB", new object[] {
                    MT_BillingData_SBM_WebService}, callback, asyncState);
    }
    
    /// <remarks/>
    public DT_SBMWebService_ResRecords[] EndSI_BillingData_SBM_WebService_OB(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((DT_SBMWebService_ResRecords[])(results[0]));
    }
    
    /// <remarks/>
    public void SI_BillingData_SBM_WebService_OBAsync(DT_SBMWebService_ReqBillingData_SBM[] MT_BillingData_SBM_WebService) {
        this.SI_BillingData_SBM_WebService_OBAsync(MT_BillingData_SBM_WebService, null);
    }
    
    /// <remarks/>
    public void SI_BillingData_SBM_WebService_OBAsync(DT_SBMWebService_ReqBillingData_SBM[] MT_BillingData_SBM_WebService, object userState) {
        if ((this.SI_BillingData_SBM_WebService_OBOperationCompleted == null)) {
            this.SI_BillingData_SBM_WebService_OBOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSI_BillingData_SBM_WebService_OBOperationCompleted);
        }
        this.InvokeAsync("SI_BillingData_SBM_WebService_OB", new object[] {
                    MT_BillingData_SBM_WebService}, this.SI_BillingData_SBM_WebService_OBOperationCompleted, userState);
    }
    
    private void OnSI_BillingData_SBM_WebService_OBOperationCompleted(object arg) {
        if ((this.SI_BillingData_SBM_WebService_OBCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.SI_BillingData_SBM_WebService_OBCompleted(this, new SI_BillingData_SBM_WebService_OBCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    public new void CancelAsync(object userState) {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://pspcl.com/xi/SBM/IF0066_BillingData_Web_Service_100")]
public partial class DT_SBMWebService_ReqBillingData_SBM {
    
    private string sUB_DIVISION_CODEField;
    
    private string mRUField;
    
    private string connected_Pole_INI_NumberField;
    
    private string nEIGHBOR_METER_NOField;
    
    private string sTREET_NAMEField;
    
    private string iNSTALLATIONField;
    
    private string mR_DOC_NOField;
    
    private string sCHEDULED_MRDATEField;
    
    private string mETER_NUMBERField;
    
    private string mANUFACTURER_SR_NOField;
    
    private string mANUFACTURER_NAMEField;
    
    private string cONTRACT_ACCOUNT_NUMBERField;
    
    private string cONSUMPTION_KWHField;
    
    private string cONSUMPTION_KWAHField;
    
    private string cONSUMPTION_KVAField;
    
    private string cUR_METER_READING_KWHField;
    
    private string cUR_METER_READING_KVAField;
    
    private string cUR_METER_READING_KVAHField;
    
    private string cUR_METER_READING_DATEField;
    
    private string cUR_METER_READING_TIMEField;
    
    private string cUR_METER_READER_NOTEField;
    
    private string pRV_METER_READING_KWHField;
    
    private string pRV_METER_READING_KVAField;
    
    private string pRV_METER_READING_KWAHField;
    
    private string pRV_METER_READING_DATEField;
    
    private string pRV_METER_READING_TIMEField;
    
    private string pRV_METER_READER_NOTEField;
    
    private string oCTROI_FLAGField;
    
    private string sOPField;
    
    private string edField;
    
    private string oCTROIField;
    
    private string dSSFField;
    
    private string sURCHARGE_LEIVEDField;
    
    private string sERVICE_RENTField;
    
    private string mETER_RENTField;
    
    private string sERVICE_CHARGEField;
    
    private string mONTHLY_MIN_CHARGESField;
    
    private string pF_SURCHARGEField;
    
    private string pF_INCENTIVEField;
    
    private string dEMAND_CHARGESField;
    
    private string fIXEDCHARGESField;
    
    private string vOLTAGE_SURCHARGEField;
    
    private string pEAKLOAD_EXEMPTION_CHARGESField;
    
    private string sUNDRY_CHARGESField;
    
    private string mISCELLANEOUS_CHARGESField;
    
    private string fUEL_ADJUSTMENTField;
    
    private string bILL_NUMBERField;
    
    private string nO_OF_DAYS_BILLEDField;
    
    private string bILL_CYCLEField;
    
    private string bILL_DATEField;
    
    private string dUE_DATEField;
    
    private string bILL_TYPEField;
    
    private string pAYMENT_AMOUNTField;
    
    private string pAYMENT_MODEField;
    
    private string cHECK_NOField;
    
    private string bANK_NAMEField;
    
    private string pAYMENT_IDField;
    
    private string iFSC_CODEField;
    
    private string mICRCODEField;
    
    private string pAYMENT_DATEField;
    
    private string pAYMENT_REMARKField;
    
    private string tOT_BILLAMOUNTField;
    
    private string sBM_NUMBERField;
    
    private string mETER_READER_NAMEField;
    
    private string iNHOUSE_OUTSOURCED_SBMField;
    
    private string transfromerCodeField;
    
    private string mCB_RENTField;
    
    private string lPSCField;
    
    private string tOT_AMT_DUE_DATEField;
    
    private string tOT_SOP_ED_OCTField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SUB_DIVISION_CODE {
        get {
            return this.sUB_DIVISION_CODEField;
        }
        set {
            this.sUB_DIVISION_CODEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string MRU {
        get {
            return this.mRUField;
        }
        set {
            this.mRUField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Connected_Pole_INI_Number {
        get {
            return this.connected_Pole_INI_NumberField;
        }
        set {
            this.connected_Pole_INI_NumberField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string NEIGHBOR_METER_NO {
        get {
            return this.nEIGHBOR_METER_NOField;
        }
        set {
            this.nEIGHBOR_METER_NOField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string STREET_NAME {
        get {
            return this.sTREET_NAMEField;
        }
        set {
            this.sTREET_NAMEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string INSTALLATION {
        get {
            return this.iNSTALLATIONField;
        }
        set {
            this.iNSTALLATIONField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string MR_DOC_NO {
        get {
            return this.mR_DOC_NOField;
        }
        set {
            this.mR_DOC_NOField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SCHEDULED_MRDATE {
        get {
            return this.sCHEDULED_MRDATEField;
        }
        set {
            this.sCHEDULED_MRDATEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string METER_NUMBER {
        get {
            return this.mETER_NUMBERField;
        }
        set {
            this.mETER_NUMBERField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string MANUFACTURER_SR_NO {
        get {
            return this.mANUFACTURER_SR_NOField;
        }
        set {
            this.mANUFACTURER_SR_NOField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string MANUFACTURER_NAME {
        get {
            return this.mANUFACTURER_NAMEField;
        }
        set {
            this.mANUFACTURER_NAMEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CONTRACT_ACCOUNT_NUMBER {
        get {
            return this.cONTRACT_ACCOUNT_NUMBERField;
        }
        set {
            this.cONTRACT_ACCOUNT_NUMBERField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CONSUMPTION_KWH {
        get {
            return this.cONSUMPTION_KWHField;
        }
        set {
            this.cONSUMPTION_KWHField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CONSUMPTION_KWAH {
        get {
            return this.cONSUMPTION_KWAHField;
        }
        set {
            this.cONSUMPTION_KWAHField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CONSUMPTION_KVA {
        get {
            return this.cONSUMPTION_KVAField;
        }
        set {
            this.cONSUMPTION_KVAField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CUR_METER_READING_KWH {
        get {
            return this.cUR_METER_READING_KWHField;
        }
        set {
            this.cUR_METER_READING_KWHField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CUR_METER_READING_KVA {
        get {
            return this.cUR_METER_READING_KVAField;
        }
        set {
            this.cUR_METER_READING_KVAField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CUR_METER_READING_KVAH {
        get {
            return this.cUR_METER_READING_KVAHField;
        }
        set {
            this.cUR_METER_READING_KVAHField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CUR_METER_READING_DATE {
        get {
            return this.cUR_METER_READING_DATEField;
        }
        set {
            this.cUR_METER_READING_DATEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CUR_METER_READING_TIME {
        get {
            return this.cUR_METER_READING_TIMEField;
        }
        set {
            this.cUR_METER_READING_TIMEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CUR_METER_READER_NOTE {
        get {
            return this.cUR_METER_READER_NOTEField;
        }
        set {
            this.cUR_METER_READER_NOTEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PRV_METER_READING_KWH {
        get {
            return this.pRV_METER_READING_KWHField;
        }
        set {
            this.pRV_METER_READING_KWHField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PRV_METER_READING_KVA {
        get {
            return this.pRV_METER_READING_KVAField;
        }
        set {
            this.pRV_METER_READING_KVAField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PRV_METER_READING_KWAH {
        get {
            return this.pRV_METER_READING_KWAHField;
        }
        set {
            this.pRV_METER_READING_KWAHField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PRV_METER_READING_DATE {
        get {
            return this.pRV_METER_READING_DATEField;
        }
        set {
            this.pRV_METER_READING_DATEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PRV_METER_READING_TIME {
        get {
            return this.pRV_METER_READING_TIMEField;
        }
        set {
            this.pRV_METER_READING_TIMEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PRV_METER_READER_NOTE {
        get {
            return this.pRV_METER_READER_NOTEField;
        }
        set {
            this.pRV_METER_READER_NOTEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string OCTROI_FLAG {
        get {
            return this.oCTROI_FLAGField;
        }
        set {
            this.oCTROI_FLAGField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SOP {
        get {
            return this.sOPField;
        }
        set {
            this.sOPField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ED {
        get {
            return this.edField;
        }
        set {
            this.edField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string OCTROI {
        get {
            return this.oCTROIField;
        }
        set {
            this.oCTROIField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DSSF {
        get {
            return this.dSSFField;
        }
        set {
            this.dSSFField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SURCHARGE_LEIVED {
        get {
            return this.sURCHARGE_LEIVEDField;
        }
        set {
            this.sURCHARGE_LEIVEDField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SERVICE_RENT {
        get {
            return this.sERVICE_RENTField;
        }
        set {
            this.sERVICE_RENTField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string METER_RENT {
        get {
            return this.mETER_RENTField;
        }
        set {
            this.mETER_RENTField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SERVICE_CHARGE {
        get {
            return this.sERVICE_CHARGEField;
        }
        set {
            this.sERVICE_CHARGEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string MONTHLY_MIN_CHARGES {
        get {
            return this.mONTHLY_MIN_CHARGESField;
        }
        set {
            this.mONTHLY_MIN_CHARGESField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PF_SURCHARGE {
        get {
            return this.pF_SURCHARGEField;
        }
        set {
            this.pF_SURCHARGEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PF_INCENTIVE {
        get {
            return this.pF_INCENTIVEField;
        }
        set {
            this.pF_INCENTIVEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DEMAND_CHARGES {
        get {
            return this.dEMAND_CHARGESField;
        }
        set {
            this.dEMAND_CHARGESField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string FIXEDCHARGES {
        get {
            return this.fIXEDCHARGESField;
        }
        set {
            this.fIXEDCHARGESField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string VOLTAGE_SURCHARGE {
        get {
            return this.vOLTAGE_SURCHARGEField;
        }
        set {
            this.vOLTAGE_SURCHARGEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PEAKLOAD_EXEMPTION_CHARGES {
        get {
            return this.pEAKLOAD_EXEMPTION_CHARGESField;
        }
        set {
            this.pEAKLOAD_EXEMPTION_CHARGESField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SUNDRY_CHARGES {
        get {
            return this.sUNDRY_CHARGESField;
        }
        set {
            this.sUNDRY_CHARGESField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string MISCELLANEOUS_CHARGES {
        get {
            return this.mISCELLANEOUS_CHARGESField;
        }
        set {
            this.mISCELLANEOUS_CHARGESField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string FUEL_ADJUSTMENT {
        get {
            return this.fUEL_ADJUSTMENTField;
        }
        set {
            this.fUEL_ADJUSTMENTField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string BILL_NUMBER {
        get {
            return this.bILL_NUMBERField;
        }
        set {
            this.bILL_NUMBERField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string NO_OF_DAYS_BILLED {
        get {
            return this.nO_OF_DAYS_BILLEDField;
        }
        set {
            this.nO_OF_DAYS_BILLEDField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string BILL_CYCLE {
        get {
            return this.bILL_CYCLEField;
        }
        set {
            this.bILL_CYCLEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string BILL_DATE {
        get {
            return this.bILL_DATEField;
        }
        set {
            this.bILL_DATEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DUE_DATE {
        get {
            return this.dUE_DATEField;
        }
        set {
            this.dUE_DATEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string BILL_TYPE {
        get {
            return this.bILL_TYPEField;
        }
        set {
            this.bILL_TYPEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PAYMENT_AMOUNT {
        get {
            return this.pAYMENT_AMOUNTField;
        }
        set {
            this.pAYMENT_AMOUNTField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PAYMENT_MODE {
        get {
            return this.pAYMENT_MODEField;
        }
        set {
            this.pAYMENT_MODEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CHECK_NO {
        get {
            return this.cHECK_NOField;
        }
        set {
            this.cHECK_NOField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string BANK_NAME {
        get {
            return this.bANK_NAMEField;
        }
        set {
            this.bANK_NAMEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PAYMENT_ID {
        get {
            return this.pAYMENT_IDField;
        }
        set {
            this.pAYMENT_IDField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string IFSC_CODE {
        get {
            return this.iFSC_CODEField;
        }
        set {
            this.iFSC_CODEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string MICRCODE {
        get {
            return this.mICRCODEField;
        }
        set {
            this.mICRCODEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PAYMENT_DATE {
        get {
            return this.pAYMENT_DATEField;
        }
        set {
            this.pAYMENT_DATEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PAYMENT_REMARK {
        get {
            return this.pAYMENT_REMARKField;
        }
        set {
            this.pAYMENT_REMARKField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string TOT_BILLAMOUNT {
        get {
            return this.tOT_BILLAMOUNTField;
        }
        set {
            this.tOT_BILLAMOUNTField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SBM_NUMBER {
        get {
            return this.sBM_NUMBERField;
        }
        set {
            this.sBM_NUMBERField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string METER_READER_NAME {
        get {
            return this.mETER_READER_NAMEField;
        }
        set {
            this.mETER_READER_NAMEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string INHOUSE_OUTSOURCED_SBM {
        get {
            return this.iNHOUSE_OUTSOURCED_SBMField;
        }
        set {
            this.iNHOUSE_OUTSOURCED_SBMField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string TransfromerCode {
        get {
            return this.transfromerCodeField;
        }
        set {
            this.transfromerCodeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string MCB_RENT {
        get {
            return this.mCB_RENTField;
        }
        set {
            this.mCB_RENTField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string LPSC {
        get {
            return this.lPSCField;
        }
        set {
            this.lPSCField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string TOT_AMT_DUE_DATE {
        get {
            return this.tOT_AMT_DUE_DATEField;
        }
        set {
            this.tOT_AMT_DUE_DATEField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string TOT_SOP_ED_OCT {
        get {
            return this.tOT_SOP_ED_OCTField;
        }
        set {
            this.tOT_SOP_ED_OCTField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://pspcl.com/xi/SBM/IF0066_BillingData_Web_Service_100")]
public partial class DT_SBMWebService_ResRecords {
    
    private string docNoField;
    
    private string flagField;
    
    private string messageField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DocNo {
        get {
            return this.docNoField;
        }
        set {
            this.docNoField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Flag {
        get {
            return this.flagField;
        }
        set {
            this.flagField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
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
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
public delegate void SI_BillingData_SBM_WebService_OBCompletedEventHandler(object sender, SI_BillingData_SBM_WebService_OBCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class SI_BillingData_SBM_WebService_OBCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal SI_BillingData_SBM_WebService_OBCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public DT_SBMWebService_ResRecords[] Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((DT_SBMWebService_ResRecords[])(this.results[0]));
        }
    }
}