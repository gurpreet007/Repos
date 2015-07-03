using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

[ServiceContract]
public interface IService
{
    [OperationContract]
    string GetDataUsingDataContract(SAP_Payment_Cols composite);
}

public class SAP_Payment_Cols
{
    public string MR_DOC_NO { get; set; }
    public string ACNO { get; set; }
    public string BILLCYCLE { get; set; }
    public string BILLGROUP { get; set; }
    public string DUEDATE { get; set; }
    public string AMNT { get; set; }
    public string RCPTNO { get; set; }
    public string RCPTDT { get; set; }
    public string TXNID { get; set; }
    public string TXNDT { get; set; }
    public string TXNID_HASH { get; set; }
}
