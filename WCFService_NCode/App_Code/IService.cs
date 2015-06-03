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
}