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
    string GetDataUsingDataContract(SAP_Cols_NCode composite);
}

public class SAP_Cols_NCode
{
    public string Sub_Division_Code { get; set; }
    public string Mru { get; set; }
    public string Connected_Pole_Ini_Number { get; set; }
    public string Neighbor_Meter_No { get; set; }
    public string Street_Name { get; set; }
    public string Installation { get; set; }
    public string Mr_Doc_No { get; set; }
    public string Scheduled_Mrdate { get; set; }
    public string Meter_Number { get; set; }
    public string Manufacturer_Sr_No { get; set; }
    public string Manufacturer_Name { get; set; }
    public string Contract_Account_Number { get; set; }
    public string Consumption_Kwh { get; set; }
    public string Consumption_Kwah { get; set; }
    public string Consumption_Kva { get; set; }
    public string Cur_Meter_Reading_Kwh { get; set; }
    public string Cur_Meter_Reading_Kva { get; set; }
    public string Cur_Meter_Reading_Kvah { get; set; }
    public string Cur_Meter_Reading_Date { get; set; }
    public string Cur_Meter_Reading_Time { get; set; }
    public string Cur_Meter_Reader_Note { get; set; }
    public string Prv_Meter_Reading_Kwh { get; set; }
    public string Prv_Meter_Reading_Kva { get; set; }
    public string Prv_Meter_Reading_Kwah { get; set; }
    public string Prv_Meter_Reading_Date { get; set; }
    public string Prv_Meter_Reading_Time { get; set; }
    public string Prv_Meter_Reader_Note { get; set; }
    public string Octroi_Flag { get; set; }
    public string Sop { get; set; }
    public string Ed { get; set; }
    public string Octroi { get; set; }
    public string Dssf { get; set; }
    public string Surcharge_Leived { get; set; }
    public string Service_Rent { get; set; }
    public string Meter_Rent { get; set; }
    public string Service_Charge { get; set; }
    public string Monthly_Min_Charges { get; set; }
    public string Pf_Surcharge { get; set; }
    public string Pf_Incentive { get; set; }
    public string Demand_Charges { get; set; }
    public string Fixedcharges { get; set; }
    public string Voltage_Surcharge { get; set; }
    public string Peakload_Exemption_Charges { get; set; }
    public string Sundry_Charges { get; set; }
    public string Miscellaneous_Charges { get; set; }
    public string Fuel_Adjustment { get; set; }
    public string Bill_Number { get; set; }
    public string No_Of_Days_Billed { get; set; }
    public string Bill_Cycle { get; set; }
    public string Bill_Date { get; set; }
    public string Due_Date { get; set; }
    public string Bill_Type { get; set; }
    public string Payment_Amount { get; set; }
    public string Payment_Mode { get; set; }
    public string Check_No { get; set; }
    public string Bank_Name { get; set; }
    public string Payment_Id { get; set; }
    public string Ifsc_Code { get; set; }
    public string Micrcode { get; set; }
    public string Payment_Date { get; set; }
    public string Payment_Remark { get; set; }
    public string Tot_Billamount { get; set; }
    public string Sbm_Number { get; set; }
    public string Meter_Reader_Name { get; set; }
    public string Inhouse_Outsourced_Sbm { get; set; }
    public string Transfromercode { get; set; }
    public string Mcb_Rent { get; set; }
    public string Lpsc { get; set; }
    public string Tot_Amt_Due_Date { get; set; }
    public string Tot_Sop_Ed_Oct { get; set; }
    public string Subdivisionname { get; set; }
    public string Consumerlegacynumber { get; set; }
    public string Consumername { get; set; }
    public string Housenumber { get; set; }
    public string Address { get; set; }
    public string Category { get; set; }
    public string Subcategory { get; set; }
    public string Billingcycle { get; set; }
    public string Billinggroup { get; set; }
    public string Phasecode { get; set; }
    public string Sanctionedload_Kw { get; set; }
    public string Totaloldmeter_Cons_Unit_Kwh { get; set; }
    public string Totaloldmeter_Cons_Unit_Kvah { get; set; }
    public string Newmeterinitialreading_Kwh { get; set; }
    public string Newmeterinitialreading_Kvah { get; set; }
    public string Sundrychrs_Sop { get; set; }
    public string Sundrychrs_Ed { get; set; }
    public string Sundrychrs_Octrai { get; set; }
    public string Sundryallowances_Sop { get; set; }
    public string Sundryallowances_Ed { get; set; }
    public string Sundryallowances_Octrai { get; set; }
    public string Otherchrs { get; set; }
    public string Roundadjustmentamnt { get; set; }
    public string Adjustmentamt_Sop { get; set; }
    public string Adjustmentamt_Ed { get; set; }
    public string Adjustmentamt_Octrai { get; set; }
    public string Provisionaladjustmentamt_Sop { get; set; }
    public string Provisionaladjustmentamt_Ed { get; set; }
    public string Provadjustmentamt_Octrai { get; set; }
    public string Arrearsopcurrentyear { get; set; }
    public string Arrearedcurrentyear { get; set; }
    public string Arrearoctraicurrentyear { get; set; }
    public string Arrearsoppreviousyear { get; set; }
    public string Arrearedpreviousyear { get; set; }
    public string Arrearoctraipreviousyear { get; set; }
    public string Advancecons_Deposit { get; set; }
    public string Complaintcenterphonenumber { get; set; }
    public string Metersecurityamt { get; set; }
    public string Nearestcashcounter { get; set; }
    public string Multiplicationfactor { get; set; }
    public string Overallmf { get; set; }
    public string Contractedload_Kva { get; set; }
    public string Miscexpensesdetails { get; set; }
    public string Previouskwhcycle_1 { get; set; }
    public string Previouskwhcycle_2 { get; set; }
    public string Previouskwhcycle_3 { get; set; }
    public string Previouskwhcycle_4 { get; set; }
    public string Previouskwhcycle_5 { get; set; }
    public string Previouskwhcycle_6 { get; set; }
    public string Rounding_Present { get; set; }
    public string Estimation_Type { get; set; }
    public string Rectype { get; set; }
    public string Mr_Doc_No_Hash { get; set; }

    public override string ToString()
    {
        string str = "Sub_Division_Code = " + Sub_Division_Code + "\r\n " +
                        "Mru = " + Mru + "\r\n " +
                        "Connected_Pole_Ini_Number = " + Connected_Pole_Ini_Number + "\r\n " +
                        "Neighbor_Meter_No = " + Neighbor_Meter_No + "\r\n " +
                        "Street_Name = " + Street_Name + "\r\n " +
                        "Installation = " + Installation + "\r\n " +
                        "Mr_Doc_No = " + Mr_Doc_No + "\r\n " +
                        "Scheduled_Mrdate = " + Scheduled_Mrdate + "\r\n " +
                        "Meter_Number = " + Meter_Number + "\r\n " +
                        "Manufacturer_Sr_No = " + Manufacturer_Sr_No + "\r\n " +
                        "Manufacturer_Name = " + Manufacturer_Name + "\r\n " +
                        "Contract_Account_Number = " + Contract_Account_Number + "\r\n " +
                        "Consumption_Kwh = " + Consumption_Kwh + "\r\n " +
                        "Consumption_Kwah = " + Consumption_Kwah + "\r\n " +
                        "Consumption_Kva = " + Consumption_Kva + "\r\n " +
                        "Cur_Meter_Reading_Kwh = " + Cur_Meter_Reading_Kwh + "\r\n " +
                        "Cur_Meter_Reading_Kva = " + Cur_Meter_Reading_Kva + "\r\n " +
                        "Cur_Meter_Reading_Kvah = " + Cur_Meter_Reading_Kvah + "\r\n " +
                        "Cur_Meter_Reading_Date = " + Cur_Meter_Reading_Date + "\r\n " +
                        "Cur_Meter_Reading_Time = " + Cur_Meter_Reading_Time + "\r\n " +
                        "Cur_Meter_Reader_Note = " + Cur_Meter_Reader_Note + "\r\n " +
                        "Prv_Meter_Reading_Kwh = " + Prv_Meter_Reading_Kwh + "\r\n " +
                        "Prv_Meter_Reading_Kva = " + Prv_Meter_Reading_Kva + "\r\n " +
                        "Prv_Meter_Reading_Kwah = " + Prv_Meter_Reading_Kwah + "\r\n " +
                        "Prv_Meter_Reading_Date = " + Prv_Meter_Reading_Date + "\r\n " +
                        "Prv_Meter_Reading_Time = " + Prv_Meter_Reading_Time + "\r\n " +
                        "Prv_Meter_Reader_Note = " + Prv_Meter_Reader_Note + "\r\n " +
                        "Octroi_Flag = " + Octroi_Flag + "\r\n " +
                        "Sop = " + Sop + "\r\n " +
                        "Ed = " + Ed + "\r\n " +
                        "Octroi = " + Octroi + "\r\n " +
                        "Dssf = " + Dssf + "\r\n " +
                        "Surcharge_Leived = " + Surcharge_Leived + "\r\n " +
                        "Service_Rent = " + Service_Rent + "\r\n " +
                        "Meter_Rent = " + Meter_Rent + "\r\n " +
                        "Service_Charge = " + Service_Charge + "\r\n " +
                        "Monthly_Min_Charges = " + Monthly_Min_Charges + "\r\n " +
                        "Pf_Surcharge = " + Pf_Surcharge + "\r\n " +
                        "Pf_Incentive = " + Pf_Incentive + "\r\n " +
                        "Demand_Charges = " + Demand_Charges + "\r\n " +
                        "Fixedcharges = " + Fixedcharges + "\r\n " +
                        "Voltage_Surcharge = " + Voltage_Surcharge + "\r\n " +
                        "Peakload_Exemption_Charges = " + Peakload_Exemption_Charges + "\r\n " +
                        "Sundry_Charges = " + Sundry_Charges + "\r\n " +
                        "Miscellaneous_Charges = " + Miscellaneous_Charges + "\r\n " +
                        "Fuel_Adjustment = " + Fuel_Adjustment + "\r\n " +
                        "Bill_Number = " + Bill_Number + "\r\n " +
                        "No_Of_Days_Billed = " + No_Of_Days_Billed + "\r\n " +
                        "Bill_Cycle = " + Bill_Cycle + "\r\n " +
                        "Bill_Date = " + Bill_Date + "\r\n " +
                        "Due_Date = " + Due_Date + "\r\n " +
                        "Bill_Type = " + Bill_Type + "\r\n " +
                        "Payment_Amount = " + Payment_Amount + "\r\n " +
                        "Payment_Mode = " + Payment_Mode + "\r\n " +
                        "Check_No = " + Check_No + "\r\n " +
                        "Bank_Name = " + Bank_Name + "\r\n " +
                        "Payment_Id = " + Payment_Id + "\r\n " +
                        "Ifsc_Code = " + Ifsc_Code + "\r\n " +
                        "Micrcode = " + Micrcode + "\r\n " +
                        "Payment_Date = " + Payment_Date + "\r\n " +
                        "Payment_Remark = " + Payment_Remark + "\r\n " +
                        "Tot_Billamount = " + Tot_Billamount + "\r\n " +
                        "Sbm_Number = " + Sbm_Number + "\r\n " +
                        "Meter_Reader_Name = " + Meter_Reader_Name + "\r\n " +
                        "Inhouse_Outsourced_Sbm = " + Inhouse_Outsourced_Sbm + "\r\n " +
                        "Transfromercode = " + Transfromercode + "\r\n " +
                        "Mcb_Rent = " + Mcb_Rent + "\r\n " +
                        "Lpsc = " + Lpsc + "\r\n " +
                        "Tot_Amt_Due_Date = " + Tot_Amt_Due_Date + "\r\n " +
                        "Tot_Sop_Ed_Oct = " + Tot_Sop_Ed_Oct + "\r\n " +
                        "Subdivisionname = " + Subdivisionname + "\r\n " +
                        "Consumerlegacynumber = " + Consumerlegacynumber + "\r\n " +
                        "Consumername = " + Consumername + "\r\n " +
                        "Housenumber = " + Housenumber + "\r\n " +
                        "Address = " + Address + "\r\n " +
                        "Category = " + Category + "\r\n " +
                        "Subcategory = " + Subcategory + "\r\n " +
                        "Billingcycle = " + Billingcycle + "\r\n " +
                        "Billinggroup = " + Billinggroup + "\r\n " +
                        "Phasecode = " + Phasecode + "\r\n " +
                        "Sanctionedload_Kw = " + Sanctionedload_Kw + "\r\n " +
                        "Totaloldmeter_Cons_Unit_Kwh = " + Totaloldmeter_Cons_Unit_Kwh + "\r\n " +
                        "Totaloldmeter_Cons_Unit_Kvah = " + Totaloldmeter_Cons_Unit_Kvah + "\r\n " +
                        "Newmeterinitialreading_Kwh = " + Newmeterinitialreading_Kwh + "\r\n " +
                        "Newmeterinitialreading_Kvah = " + Newmeterinitialreading_Kvah + "\r\n " +
                        "Sundrychrs_Sop = " + Sundrychrs_Sop + "\r\n " +
                        "Sundrychrs_Ed = " + Sundrychrs_Ed + "\r\n " +
                        "Sundrychrs_Octrai = " + Sundrychrs_Octrai + "\r\n " +
                        "Sundryallowances_Sop = " + Sundryallowances_Sop + "\r\n " +
                        "Sundryallowances_Ed = " + Sundryallowances_Ed + "\r\n " +
                        "Sundryallowances_Octrai = " + Sundryallowances_Octrai + "\r\n " +
                        "Otherchrs = " + Otherchrs + "\r\n " +
                        "Roundadjustmentamnt = " + Roundadjustmentamnt + "\r\n " +
                        "Adjustmentamt_Sop = " + Adjustmentamt_Sop + "\r\n " +
                        "Adjustmentamt_Ed = " + Adjustmentamt_Ed + "\r\n " +
                        "Adjustmentamt_Octrai = " + Adjustmentamt_Octrai + "\r\n " +
                        "Provisionaladjustmentamt_Sop = " + Provisionaladjustmentamt_Sop + "\r\n " +
                        "Provisionaladjustmentamt_Ed = " + Provisionaladjustmentamt_Ed + "\r\n " +
                        "Provadjustmentamt_Octrai = " + Provadjustmentamt_Octrai + "\r\n " +
                        "Arrearsopcurrentyear = " + Arrearsopcurrentyear + "\r\n " +
                        "Arrearedcurrentyear = " + Arrearedcurrentyear + "\r\n " +
                        "Arrearoctraicurrentyear = " + Arrearoctraicurrentyear + "\r\n " +
                        "Arrearsoppreviousyear = " + Arrearsoppreviousyear + "\r\n " +
                        "Arrearedpreviousyear = " + Arrearedpreviousyear + "\r\n " +
                        "Arrearoctraipreviousyear = " + Arrearoctraipreviousyear + "\r\n " +
                        "Advancecons_Deposit = " + Advancecons_Deposit + "\r\n " +
                        "Complaintcenterphonenumber = " + Complaintcenterphonenumber + "\r\n " +
                        "Metersecurityamt = " + Metersecurityamt + "\r\n " +
                        "Nearestcashcounter = " + Nearestcashcounter + "\r\n " +
                        "Multiplicationfactor = " + Multiplicationfactor + "\r\n " +
                        "Overallmf = " + Overallmf + "\r\n " +
                        "Contractedload_Kva = " + Contractedload_Kva + "\r\n " +
                        "Miscexpensesdetails = " + Miscexpensesdetails + "\r\n " +
                        "Previouskwhcycle_1 = " + Previouskwhcycle_1 + "\r\n " +
                        "Previouskwhcycle_2 = " + Previouskwhcycle_2 + "\r\n " +
                        "Previouskwhcycle_3 = " + Previouskwhcycle_3 + "\r\n " +
                        "Previouskwhcycle_4 = " + Previouskwhcycle_4 + "\r\n " +
                        "Previouskwhcycle_5 = " + Previouskwhcycle_5 + "\r\n " +
                        "Previouskwhcycle_6 = " + Previouskwhcycle_6 + "\r\n " +
                        "Rounding_Present = " + Rounding_Present + "\r\n " +
                        "Estimation_Type = " + Estimation_Type + "\r\n " +
                        "Rectype = " + Rectype + "\r\n " +
                        "Mr_Doc_No_Hash = " + Mr_Doc_No_Hash;
        return str;
    }
}