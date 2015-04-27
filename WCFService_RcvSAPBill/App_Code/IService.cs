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
	string GetDataUsingDataContract(SAP_Cols composite);
}

public class SAP_Cols
{
    public string sub_division_code;
    public string mru;
    public string connected_pole_ini_number;
    public string neighbor_meter_no;
    public string street_name;
    public string installation;
    public string mr_doc_no;
    public DateTime scheduled_mrdate;
    public string meter_number;
    public int manufacturer_sr_no;
    public string manufacturer_name;
    public string contract_account_number;
    public float consumption_kwh;
    public float consumption_kwah;
    public float consumption_kva;
    public float cur_meter_reading_kwh;
    public float cur_meter_reading_kva;
    public float cur_meter_reading_kvah;
    public DateTime cur_meter_reading_date;
    public string cur_meter_reading_time;
    public string cur_meter_reader_note;
    public float prv_meter_reading_kwh;
    public float prv_meter_reading_kva;
    public float prv_meter_reading_kwah;
    public DateTime prv_meter_reading_date;
    public string prv_meter_reading_time;
    public string prv_meter_reader_note;
    public string octroi_flag;
    public float sop;
    public float ed;
    public float octroi;
    public float dssf;
    public float surcharge_leived;
    public float service_rent;
    public float meter_rent;
    public float service_charge;
    public float monthly_min_charges;
    public float pf_surcharge;
    public float pf_incentive;
    public float demand_charges;
    public float fixedcharges;
    public float voltage_surcharge;
    public float peakload_exemption_charges;
    public float sundry_charges;
    public float miscellaneous_charges;
    public float fuel_adjustment;
    public string bill_number;
    public int no_of_days_billed;
    public int bill_cycle;
    public string bill_date;
    public string due_date;
    public string bill_type;
    public float payment_amount;
    public string payment_mode;
    public string check_no;
    public string bank_name;
    public string payment_id;
    public string ifsc_code;
    public string micrcode;
    public string payment_date;
    public string payment_remark;
    public float tot_billamount;
    public string sbm_number;
    public string meter_reader_name;
    public string inhouse_outsourced_sbm;
    public string transfromercode;
    public float mcb_rent;
    public float lpsc;
    public float tot_amt_due_date;
    public float tot_sop_ed_oct;
    public string userid;
    public float empid;
    public DateTime dtupload;
    public string synced;
}