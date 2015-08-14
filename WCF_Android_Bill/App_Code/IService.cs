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
    public long LtBillID { get; set; }
    [DataMember]
    public string SubDivisionName { get; set; }
    [DataMember]
    public string AccountNumber { get; set; }
    [DataMember]
    public string ConsumerName { get; set; }
    [DataMember]
    public string Address { get; set; }
    [DataMember]
    public long BillNumber { get; set; }
    [DataMember]
    public DateTime BillDate { get; set; }
    [DataMember]
    public string BillingGroup { get; set; }
    [DataMember]
    public DateTime CashDueDate { get; set; }
    [DataMember]
    public DateTime ChequeDueDate { get; set; }
    [DataMember]
    public string CollectionCentre { get; set; }
    [DataMember]
    public string ComplaintCentrePhone { get; set; }
    [DataMember]
    public string TariffCode { get; set; }
    [DataMember]
    public string PhaseCode { get; set; }
    [DataMember]
    public double MeterMultiplier { get; set; }
    [DataMember]
    public string LineCTRatio { get; set; }
    [DataMember]
    public string MeterCTRatio { get; set; }
    [DataMember]
    public double OverallMultiplyingFactor { get; set; }
    [DataMember]
    public double ConnectedLoad { get; set; }
    [DataMember]
    public long CurrentMeterReading { get; set; }
    [DataMember]
    public DateTime CurrentReadingDate { get; set; }
    [DataMember]
    public long PreviousMeterReading { get; set; }
    [DataMember]
    public DateTime PreviousReadingDate { get; set; }
    [DataMember]
    public long CurrentConsumption { get; set; }
    [DataMember]
    public long PreviousConsumption { get; set; }
    [DataMember]
    public long TotalConsumption { get; set; }
    [DataMember]
    public decimal SecurityDeposit { get; set; }
    [DataMember]
    public string CBillStatus { get; set; }
    [DataMember]
    public long ConcessionUnits { get; set; }
    [DataMember]
    public long BillPeriod { get; set; }
    [DataMember]
    public decimal CurrentSOP { get; set; }
    [DataMember]
    public decimal CurrentED { get; set; }
    [DataMember]
    public decimal CurrentOctroi { get; set; }
    [DataMember]
    public short PreviousRoundAmount { get; set; }
    [DataMember]
    public decimal CurrentMeterRent { get; set; }
    [DataMember]
    public decimal CurrentServiceCharges { get; set; }
    [DataMember]
    public decimal AvgAdjustmentAmount { get; set; }
    [DataMember]
    public decimal FixedCharges { get; set; }
    [DataMember]
    public decimal FuelCostAdjustment { get; set; }
    [DataMember]
    public decimal OtherCharges { get; set; }
    [DataMember]
    public decimal VoltageSurcharge { get; set; }
    [DataMember]
    public decimal PreviousArrears { get; set; }
    [DataMember]
    public decimal CurrentArrears { get; set; }
    [DataMember]
    public decimal SundryCharges { get; set; }
    [DataMember]
    public decimal SundryAllowances { get; set; }
    [DataMember]
    public decimal NetSOP { get; set; }
    [DataMember]
    public decimal NetED { get; set; }
    [DataMember]
    public decimal NetOctroi { get; set; }
    [DataMember]
    public decimal NetAmount { get; set; }
    [DataMember]
    public decimal Surcharge { get; set; }
    [DataMember]
    public decimal GrossAmount { get; set; }
    [DataMember]
    public string AdjustmentAmountDetail { get; set; }
    [DataMember]
    public string SundryChargesDetail { get; set; }
    [DataMember]
    public decimal MMCCharges { get; set; }
    [DataMember]
    public short CurrentRoundAmount { get; set; }
    [DataMember]
    public string BillCycle { get; set; }
    [DataMember]
    public string MeterNo { get; set; }
    [DataMember]
    public string CurrentMeterCode { get; set; }
    [DataMember]
    public string PreviousMeterCode { get; set; }
    [DataMember]
    public long AvgAdjustmentPeriod { get; set; }
    [DataMember]
    public long ConcealedUnits { get; set; }
    [DataMember]
    public string BillYear { get; set; }
    [DataMember]
    public string FinYear { get; set; }
    [DataMember]
    public long Consumption1 { get; set; }
    [DataMember]
    public long Consumption2 { get; set; }
    [DataMember]
    public long Consumption3 { get; set; }
    [DataMember]
    public long Consumption4 { get; set; }
    [DataMember]
    public long Consumption5 { get; set; }
    [DataMember]
    public long Consumption6 { get; set; }
    [DataMember]
    public string LtBillID_Hash { get; set; }

    public override string ToString()
    {
        string str = "LtBillID = " + LtBillID + "\r\n " +
                    "SubDivisionName = " + SubDivisionName + "\r\n " +
                    "AccountNumber = " + AccountNumber + "\r\n " +
                    "ConsumerName = " + ConsumerName + "\r\n " +
                    "Address = " + Address + "\r\n " +
                    "BillNumber = " + BillNumber + "\r\n " +
                    "BillDate = " + BillDate + "\r\n " +
                    "BillingGroup = " + BillingGroup + "\r\n " +
                    "CashDueDate = " + CashDueDate + "\r\n " +
                    "ChequeDueDate = " + ChequeDueDate + "\r\n " +
                    "CollectionCentre = " + CollectionCentre + "\r\n " +
                    "ComplaintCentrePhone = " + ComplaintCentrePhone + "\r\n " +
                    "TariffCode = " + TariffCode + "\r\n " +
                    "PhaseCode = " + PhaseCode + "\r\n " +
                    "MeterMultiplier = " + MeterMultiplier + "\r\n " +
                    "LineCTRatio = " + LineCTRatio + "\r\n " +
                    "MeterCTRatio = " + MeterCTRatio + "\r\n " +
                    "OverallMultiplyingFactor = " + OverallMultiplyingFactor + "\r\n " +
                    "ConnectedLoad = " + ConnectedLoad + "\r\n " +
                    "CurrentMeterReading = " + CurrentMeterReading + "\r\n " +
                    "CurrentReadingDate = " + CurrentReadingDate + "\r\n " +
                    "PreviousMeterReading = " + PreviousMeterReading + "\r\n " +
                    "PreviousReadingDate = " + PreviousReadingDate + "\r\n " +
                    "CurrentConsumption = " + CurrentConsumption + "\r\n " +
                    "PreviousConsumption = " + PreviousConsumption + "\r\n " +
                    "TotalConsumption = " + TotalConsumption + "\r\n " +
                    "SecurityDeposit = " + SecurityDeposit + "\r\n " +
                    "CBillStatus = " + CBillStatus + "\r\n " +
                    "ConcessionUnits = " + ConcessionUnits + "\r\n " +
                    "BillPeriod = " + BillPeriod + "\r\n " +
                    "CurrentSOP = " + CurrentSOP + "\r\n " +
                    "CurrentED = " + CurrentED + "\r\n " +
                    "CurrentOctroi = " + CurrentOctroi + "\r\n " +
                    "PreviousRoundAmount = " + PreviousRoundAmount + "\r\n " +
                    "CurrentMeterRent = " + CurrentMeterRent + "\r\n " +
                    "CurrentServiceCharges = " + CurrentServiceCharges + "\r\n " +
                    "AvgAdjustmentAmount = " + AvgAdjustmentAmount + "\r\n " +
                    "FixedCharges = " + FixedCharges + "\r\n " +
                    "FuelCostAdjustment = " + FuelCostAdjustment + "\r\n " +
                    "OtherCharges = " + OtherCharges + "\r\n " +
                    "VoltageSurcharge = " + VoltageSurcharge + "\r\n " +
                    "PreviousArrears = " + PreviousArrears + "\r\n " +
                    "CurrentArrears = " + CurrentArrears + "\r\n " +
                    "SundryCharges = " + SundryCharges + "\r\n " +
                    "SundryAllowances = " + SundryAllowances + "\r\n " +
                    "NetSOP = " + NetSOP + "\r\n " +
                    "NetED = " + NetED + "\r\n " +
                    "NetOctroi = " + NetOctroi + "\r\n " +
                    "NetAmount = " + NetAmount + "\r\n " +
                    "Surcharge = " + Surcharge + "\r\n " +
                    "GrossAmount = " + GrossAmount + "\r\n " +
                    "AdjustmentAmountDetail = " + AdjustmentAmountDetail + "\r\n " +
                    "SundryChargesDetail = " + SundryChargesDetail + "\r\n " +
                    "MMCCharges = " + MMCCharges + "\r\n " +
                    "CurrentRoundAmount = " + CurrentRoundAmount + "\r\n " +
                    "BillCycle = " + BillCycle + "\r\n " +
                    "MeterNo = " + MeterNo + "\r\n " +
                    "CurrentMeterCode = " + CurrentMeterCode + "\r\n " +
                    "PreviousMeterCode = " + PreviousMeterCode + "\r\n " +
                    "AvgAdjustmentPeriod = " + AvgAdjustmentPeriod + "\r\n " +
                    "ConcealedUnits = " + ConcealedUnits + "\r\n " +
                    "BillYear = " + BillYear + "\r\n " +
                    "FinYear = " + FinYear + "\r\n " +
                    "Consumption1 = " + Consumption1 + "\r\n " +
                    "Consumption2 = " + Consumption2 + "\r\n " +
                    "Consumption3 = " + Consumption3 + "\r\n " +
                    "Consumption4 = " + Consumption4 + "\r\n " +
                    "Consumption5 = " + Consumption5 + "\r\n " +
                    "Consumption6 = " + Consumption6 + "\r\n ";
        return str;
    }
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
