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

    [DataMember]
    public string Sub_division_code
    {
        get { return sub_division_code; }
        set { sub_division_code = value; }
    }

    string mru;

    [DataMember]
    public string Mru
    {
        get { return mru; }
        set { mru = value; }
    }

    string connected_pole_ini_number;

    [DataMember]
    public string Connected_pole_ini_number
    {
        get { return connected_pole_ini_number; }
        set { connected_pole_ini_number = value; }
    }

    string neighbor_meter_no;

    [DataMember]
    public string Neighbor_meter_no
    {
        get { return neighbor_meter_no; }
        set { neighbor_meter_no = value; }
    }

    string street_name;

    [DataMember]
    public string Street_name
    {
        get { return street_name; }
        set { street_name = value; }
    }

    string installation;

    [DataMember]
    public string Installation
    {
        get { return installation; }
        set { installation = value; }
    }

    string mr_doc_no;

    [DataMember]
    public string Mr_doc_no
    {
        get { return mr_doc_no; }
        set { mr_doc_no = value; }
    }

    DateTime scheduled_mrdate;

    [DataMember]
    public DateTime Scheduled_mrdate
    {
        get { return scheduled_mrdate; }
        set { scheduled_mrdate = value; }
    }

    string meter_number;

    [DataMember]
    public string Meter_number
    {
        get { return meter_number; }
        set { meter_number = value; }
    }

    int manufacturer_sr_no;

    [DataMember]
    public int Manufacturer_sr_no
    {
        get { return manufacturer_sr_no; }
        set { manufacturer_sr_no = value; }
    }

    string manufacturer_name;

    [DataMember]
    public string Manufacturer_name
    {
        get { return manufacturer_name; }
        set { manufacturer_name = value; }
    }

    string contract_account_number;

    [DataMember]
    public string Contract_account_number
    {
        get { return contract_account_number; }
        set { contract_account_number = value; }
    }

    float consumption_kwh;

    [DataMember]
    public float Consumption_kwh
    {
        get { return consumption_kwh; }
        set { consumption_kwh = value; }
    }

    float consumption_kwah;

    [DataMember]
    public float Consumption_kwah
    {
        get { return consumption_kwah; }
        set { consumption_kwah = value; }
    }

    float consumption_kva;

    [DataMember]
    public float Consumption_kva
    {
        get { return consumption_kva; }
        set { consumption_kva = value; }
    }

    float cur_meter_reading_kwh;

    [DataMember]
    public float Cur_meter_reading_kwh
    {
        get { return cur_meter_reading_kwh; }
        set { cur_meter_reading_kwh = value; }
    }

    float cur_meter_reading_kva;

    [DataMember]
    public float Cur_meter_reading_kva
    {
        get { return cur_meter_reading_kva; }
        set { cur_meter_reading_kva = value; }
    }

    float cur_meter_reading_kvah;

    [DataMember]
    public float Cur_meter_reading_kvah
    {
        get { return cur_meter_reading_kvah; }
        set { cur_meter_reading_kvah = value; }
    }

    DateTime cur_meter_reading_date;

    [DataMember]
    public DateTime Cur_meter_reading_date
    {
        get { return cur_meter_reading_date; }
        set { cur_meter_reading_date = value; }
    }

    string cur_meter_reading_time;

    [DataMember]
    public string Cur_meter_reading_time
    {
        get { return cur_meter_reading_time; }
        set { cur_meter_reading_time = value; }
    }

    string cur_meter_reader_note;

    [DataMember]
    public string Cur_meter_reader_note
    {
        get { return cur_meter_reader_note; }
        set { cur_meter_reader_note = value; }
    }

    float prv_meter_reading_kwh;

    [DataMember]
    public float Prv_meter_reading_kwh
    {
        get { return prv_meter_reading_kwh; }
        set { prv_meter_reading_kwh = value; }
    }

    float prv_meter_reading_kva;

    [DataMember]
    public float Prv_meter_reading_kva
    {
        get { return prv_meter_reading_kva; }
        set { prv_meter_reading_kva = value; }
    }

    float prv_meter_reading_kwah;

    [DataMember]
    public float Prv_meter_reading_kwah
    {
        get { return prv_meter_reading_kwah; }
        set { prv_meter_reading_kwah = value; }
    }

    DateTime prv_meter_reading_date;

    [DataMember]
    public DateTime Prv_meter_reading_date
    {
        get { return prv_meter_reading_date; }
        set { prv_meter_reading_date = value; }
    }

    string prv_meter_reading_time;

    [DataMember]
    public string Prv_meter_reading_time
    {
        get { return prv_meter_reading_time; }
        set { prv_meter_reading_time = value; }
    }

    string prv_meter_reader_note;

    [DataMember]
    public string Prv_meter_reader_note
    {
        get { return prv_meter_reader_note; }
        set { prv_meter_reader_note = value; }
    }

    string octroi_flag;

    [DataMember]
    public string Octroi_flag
    {
        get { return octroi_flag; }
        set { octroi_flag = value; }
    }

    float sop;

    [DataMember]
    public float Sop
    {
        get { return sop; }
        set { sop = value; }
    }

    float ed;

    [DataMember]
    public float Ed
    {
        get { return ed; }
        set { ed = value; }
    }
    float octroi;

    public float Octroi
    {
        get { return octroi; }
        set { octroi = value; }
    }
    float dssf;

    public float Dssf
    {
        get { return dssf; }
        set { dssf = value; }
    }
    float surcharge_leived;

    public float Surcharge_leived
    {
        get { return surcharge_leived; }
        set { surcharge_leived = value; }
    }
    float service_rent;

    public float Service_rent
    {
        get { return service_rent; }
        set { service_rent = value; }
    }
    float meter_rent;

    public float Meter_rent
    {
        get { return meter_rent; }
        set { meter_rent = value; }
    }
    float service_charge;

    public float Service_charge
    {
        get { return service_charge; }
        set { service_charge = value; }
    }
    float monthly_min_charges;

    public float Monthly_min_charges
    {
        get { return monthly_min_charges; }
        set { monthly_min_charges = value; }
    }
    float pf_surcharge;

    public float Pf_surcharge
    {
        get { return pf_surcharge; }
        set { pf_surcharge = value; }
    }
    float pf_incentive;

    public float Pf_incentive
    {
        get { return pf_incentive; }
        set { pf_incentive = value; }
    }
    float demand_charges;

    public float Demand_charges
    {
        get { return demand_charges; }
        set { demand_charges = value; }
    }
    float fixedcharges;

    public float Fixedcharges
    {
        get { return fixedcharges; }
        set { fixedcharges = value; }
    }
    float voltage_surcharge;

    public float Voltage_surcharge
    {
        get { return voltage_surcharge; }
        set { voltage_surcharge = value; }
    }
    float peakload_exemption_charges;

    public float Peakload_exemption_charges
    {
        get { return peakload_exemption_charges; }
        set { peakload_exemption_charges = value; }
    }
    float sundry_charges;

    public float Sundry_charges
    {
        get { return sundry_charges; }
        set { sundry_charges = value; }
    }
    float miscellaneous_charges;

    public float Miscellaneous_charges
    {
        get { return miscellaneous_charges; }
        set { miscellaneous_charges = value; }
    }
    float fuel_adjustment;

    public float Fuel_adjustment
    {
        get { return fuel_adjustment; }
        set { fuel_adjustment = value; }
    }
    string bill_number;

    public string Bill_number
    {
        get { return bill_number; }
        set { bill_number = value; }
    }
    int no_of_days_billed;

    public int No_of_days_billed
    {
        get { return no_of_days_billed; }
        set { no_of_days_billed = value; }
    }
    int bill_cycle;

    public int Bill_cycle
    {
        get { return bill_cycle; }
        set { bill_cycle = value; }
    }
    string bill_date;

    public string Bill_date
    {
        get { return bill_date; }
        set { bill_date = value; }
    }
    string due_date;

    public string Due_date
    {
        get { return due_date; }
        set { due_date = value; }
    }
    string bill_type;

    public string Bill_type
    {
        get { return bill_type; }
        set { bill_type = value; }
    }
    float payment_amount;

    public float Payment_amount
    {
        get { return payment_amount; }
        set { payment_amount = value; }
    }
    string payment_mode;

    public string Payment_mode
    {
        get { return payment_mode; }
        set { payment_mode = value; }
    }
    string check_no;

    public string Check_no
    {
        get { return check_no; }
        set { check_no = value; }
    }
    string bank_name;

    public string Bank_name
    {
        get { return bank_name; }
        set { bank_name = value; }
    }
    string payment_id;

    public string Payment_id
    {
        get { return payment_id; }
        set { payment_id = value; }
    }
    string ifsc_code;

    public string Ifsc_code
    {
        get { return ifsc_code; }
        set { ifsc_code = value; }
    }
    string micrcode;

    public string Micrcode
    {
        get { return micrcode; }
        set { micrcode = value; }
    }
    string payment_date;

    public string Payment_date
    {
        get { return payment_date; }
        set { payment_date = value; }
    }
    string payment_remark;

    public string Payment_remark
    {
        get { return payment_remark; }
        set { payment_remark = value; }
    }
    float tot_billamount;

    public float Tot_billamount
    {
        get { return tot_billamount; }
        set { tot_billamount = value; }
    }
    string sbm_number;

    public string Sbm_number
    {
        get { return sbm_number; }
        set { sbm_number = value; }
    }
    string meter_reader_name;

    public string Meter_reader_name
    {
        get { return meter_reader_name; }
        set { meter_reader_name = value; }
    }
    string inhouse_outsourced_sbm;

    public string Inhouse_outsourced_sbm
    {
        get { return inhouse_outsourced_sbm; }
        set { inhouse_outsourced_sbm = value; }
    }
    string transfromercode;

    public string Transfromercode
    {
        get { return transfromercode; }
        set { transfromercode = value; }
    }
    float mcb_rent;

    public float Mcb_rent
    {
        get { return mcb_rent; }
        set { mcb_rent = value; }
    }
    float lpsc;

    public float Lpsc
    {
        get { return lpsc; }
        set { lpsc = value; }
    }
    float tot_amt_due_date;

    public float Tot_amt_due_date
    {
        get { return tot_amt_due_date; }
        set { tot_amt_due_date = value; }
    }
    float tot_sop_ed_oct;

    public float Tot_sop_ed_oct
    {
        get { return tot_sop_ed_oct; }
        set { tot_sop_ed_oct = value; }
    }
    string userid;
    float empid;
    DateTime dtupload;
    string synced;


}