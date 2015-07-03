using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

[ServiceContract]
public interface IAndroidService
{
	[OperationContract]
    Android_Bill_Ret[] GetDataUsingDataContract(Android_Bill_Cols[] composite);
}

[DataContract]
public class Android_Bill_Cols
{
    [DataMember]
    public string LtBillID { get; set; }
    [DataMember]
    public string SubDivisionName { get; set; }
    [DataMember]
    public string AccountNumber { get; set; }
    [DataMember]
    public string ConsumerName { get; set; }
    [DataMember]
    public string Address { get; set; }
    [DataMember]
    public string BillNumber { get; set; }
    [DataMember]
    public string BillDate { get; set; }
    [DataMember]
    public string BillingGroup { get; set; }
    [DataMember]
    public string CashDueDate { get; set; }
    [DataMember]
    public string ChequeDueDate { get; set; }
    [DataMember]
    public string CollectionCentre { get; set; }
    [DataMember]
    public string ComplaintCentrePhone { get; set; }
    [DataMember]
    public string TariffCode { get; set; }
    [DataMember]
    public string PhaseCode { get; set; }
    [DataMember]
    public string MeterMultiplier { get; set; }
    [DataMember]
    public string LineCTRatio { get; set; }
    [DataMember]
    public string MeterCTRatio { get; set; }
    [DataMember]
    public string OverallMultiplyingFactor { get; set; }
    [DataMember]
    public string ConnectedLoad { get; set; }
    [DataMember]
    public string CurrentMeterReading { get; set; }
    [DataMember]
    public string CurrentReadingDate { get; set; }
    [DataMember]
    public string PreviousMeterReading { get; set; }
    [DataMember]
    public string PreviousReadingDate { get; set; }
    [DataMember]
    public string CurrentConsumption { get; set; }
    [DataMember]
    public string PreviousConsumption { get; set; }
    [DataMember]
    public string TotalConsumption { get; set; }
    [DataMember]
    public string SecurityDeposit { get; set; }
    [DataMember]
    public string CBillStatus { get; set; }
    [DataMember]
    public string ConcessionUnits { get; set; }
    [DataMember]
    public string BillPeriod { get; set; }
    [DataMember]
    public string CurrentSOP { get; set; }
    [DataMember]
    public string CurrentED { get; set; }
    [DataMember]
    public string CurrentOctroi { get; set; }
    [DataMember]
    public string PreviousRoundAmount { get; set; }
    [DataMember]
    public string CurrentMeterRent { get; set; }
    [DataMember]
    public string CurrentServiceCharges { get; set; }
    [DataMember]
    public string AvgAdjustmentAmount { get; set; }
    [DataMember]
    public string FixedCharges { get; set; }
    [DataMember]
    public string FuelCostAdjustment { get; set; }
    [DataMember]
    public string OtherCharges { get; set; }
    [DataMember]
    public string VoltageSurcharge { get; set; }
    [DataMember]
    public string PreviousArrears { get; set; }
    [DataMember]
    public string CurrentArrears { get; set; }
    [DataMember]
    public string SundryCharges { get; set; }
    [DataMember]
    public string SundryAllowances { get; set; }
    [DataMember]
    public string NetSOP { get; set; }
    [DataMember]
    public string NetED { get; set; }
    [DataMember]
    public string NetOctroi { get; set; }
    [DataMember]
    public string NetAmount { get; set; }
    [DataMember]
    public string Surcharge { get; set; }
    [DataMember]
    public string GrossAmount { get; set; }
    [DataMember]
    public string AdjustmentAmountDetail { get; set; }
    [DataMember]
    public string SundryChargesDetail { get; set; }
    [DataMember]
    public string MMCCharges { get; set; }
    [DataMember]
    public string CurrentRoundAmount { get; set; }
    [DataMember]
    public string BillCycle { get; set; }
    [DataMember]
    public string MeterNo { get; set; }
    [DataMember]
    public string CurrentMeterCode { get; set; }
    [DataMember]
    public string PreviousMeterCode { get; set; }
    [DataMember]
    public string AvgAdjustmentPeriod { get; set; }
    [DataMember]
    public string ConcealedUnits { get; set; }
    [DataMember]
    public string BillYear { get; set; }
    [DataMember]
    public string FinYear { get; set; }
    [DataMember]
    public string Consumption1 { get; set; }
    [DataMember]
    public string Consumption2 { get; set; }
    [DataMember]
    public string Consumption3 { get; set; }
    [DataMember]
    public string Consumption4 { get; set; }
    [DataMember]
    public string Consumption5 { get; set; }
    [DataMember]
    public string Consumption6 { get; set; }
    [DataMember]
    public string LtBillID_Hash { get; set; }
}

[DataContract]
public class Android_Bill_Ret
{
    [DataMember]
    public string ltBillID { get; private set; }
    [DataMember]
    public string Status { get; private set; }
    [DataMember]
    public string Message { get; private set; }
    public Android_Bill_Ret(string ltbillid, string status, string message = "")
    {
        this.ltBillID = ltbillid;
        this.Status = status;
        this.Message = message;
    }
}
