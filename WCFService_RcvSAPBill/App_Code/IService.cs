﻿using System;
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
    public string tariftyp;
    public string circle;
    public string division;
    public string sub_div;
    public string bill_cycle;
    public string zz_year;
    public string bill_date;
    public string opbel;
    public string faedn_cash;
    public string faedn_cheque;
    public string nft_admvlt;
    public string nft_supvlt;
    public string nft_mtrvlt;
    public string vkont;
    public string vkona;
    public string name;
    public string address;
    public string date_of_conn;
    public string sndgen;
    public string sndif;
    public string sndpiu;
    public string sndsea;
    public string sndgen_date;
    public string sndif_date;
    public string sndpiu_date;
    public string sndsea_date;
    public string cdgen;
    public string cdif;
    public string cdpiu;
    public string cdsea;
    public string cdgen_date;
    public string cdif_date;
    public string cdpiu_date;
    public string cdsea_date;
    public string ind_typ;
    public string mmc;
    public string sea_on_date;
    public string sea_off_date;
    public string peak_load;
    public string peak_load_s_date;
    public string peak_load_e_date;
    public string pre_read_date;
    public string cur_read_date;
    public string no_days_bill;
    public string meter_owner;
    public string mco_date;
    public string ctpt;
    public string sjo_date;
    public string feeder_code;
    public string feeder_name;
    public string mtr_security_amt;
    public string adv_cons_deposit;
    public string metetr_read_note;
    public string pre_met_red_kwh;
    public string pre_met_red_kvah;
    public string pre_met_red_kva;
    public string curr_met_red_kwh;
    public string curr_met_red_kvah;
    public string curr_met_red_kva;
    public string mtr_multiplier;
    public string line_ctr;
    public string mtr_ratio;
    public string mtr_volt_ratio;
    public string overall_multiplier;
    public string mmts_corr;
    public string add_sup_unit_kwh;
    public string add_sup_unit_kvah;
    public string add_sup_unit_kva;
    public string cur_cons_kwh;
    public string cur_cons_kvah;
    public string cur_cons_kva;
    public string old_pre_met_kwh;
    public string old_pre_met_kvah;
    public string old_pre_met_kva;
    public string old_curr_met_kwh;
    public string old_curr_met_kvah;
    public string old_curr_met_kva;
    public string old_mtr_multiplier;
    public string old_line_ctr;
    public string old_mtr_ratio;
    public string old_mtr_volt_ratio;
    public string old_all_multiplier;
    public string old_mmts_corr;
    public string old_add_sup_kwh;
    public string old_add_sup_kvah;
    public string old_add_sup_kva;
    public string old_cons_kwh;
    public string old_cons_kvah;
    public string old_cons_kva;
    public string tot_cons_kwh;
    public string tot_cons_kvah;
    public string tot_cons_kva;
    public string pf;
    public string be_code;
    public string concess_cons;
    public string ec;
    public string mmc_add;
    public string pf_int_sur;
    public string ht_rbt;
    public string fca;
    public string dmnd_schr;
    public string plec;
    public string volt_schr;
    public string fix_chrg;
    public string ot_chrg;
    public string violation_chrg;
    public string sop;
    public string mtr_rent;
    public string mcb_rent;
    public string ser_rent;
    public string ser_chrg;
    public string tot_rentals;
    public string pre_round;
    public string curr_round;
    public string curr_sop;
    public string curr_rental;
    public string curr_ed;
    public string curr_oct;
    public string curr_misc;
    public string curr_total;
    public string adj_sop;
    public string adj_rental;
    public string adj_ed;
    public string adj_oct;
    public string adj_misc;
    public string adj_total;
    public string snd_chrg_sop;
    public string snd_chrg_rental;
    public string snd_chrg_ed;
    public string snd_chrg_oct;
    public string snd_chrg_misc;
    public string snd_chrg_total;
    public string snd_allw_sop;
    public string snd_allw_rental;
    public string snd_allw_ed;
    public string snd_allw_oct;
    public string snd_allw_misc;
    public string snd_allw_total;
    public string arr_pre_sop;
    public string arr_pre_rental;
    public string arr_pre_ed;
    public string arr_pre_oct;
    public string arr_pre_misc;
    public string arr_pre_total;
    public string arr_curr_sop;
    public string arr_curr_rental;
    public string arr_curr_ed;
    public string arr_curr_oct;
    public string arr_curr_misc;
    public string arr_curr_total;
    public string net_sop;
    public string net_rental;
    public string net_ed;
    public string net_oct;
    public string net_misc;
    public string net_tot;
    public string net_payable;
    public string lpsc_amnt_2;
    public string lpcs_amnt_5;
    public string amt_aft_due_dat_2;
    public string amt_aft_due_dat_5;
    public string cons_kwh_1;
    public string cons_kvah_1;
    public string cons_kva_1;
    public string cons_kwh_2;
    public string cons_kvah_2;
    public string cons_kva_2;
    public string cons_kwh_3;
    public string cons_kvah_3;
    public string cons_kva_3;
    public string cons_kwh_4;
    public string cons_kvah_4;
    public string cons_kva_4;
    public string cons_kwh_5;
    public string cons_kvah_5;
    public string cons_kva_5;
    public string cons_kwh_6;
    public string cons_kvah_6;
    public string cons_kva_6;
    public string cons_kwh_7;
    public string cons_kvah_7;
    public string cons_kva_7;
    public string cons_kwh_8;
    public string cons_kvah_8;
    public string cons_kva_8;
    public string cons_kwh_9;
    public string cons_kvah_9;
    public string cons_kva_9;
    public string cons_kwh_10;
    public string cons_kvah_10;
    public string cons_kva_10;
    public string cons_kwh_11;
    public string cons_kvah_11;
    public string cons_kva_11;
    public string cons_kwh_12;
    public string cons_kvah_12;
    public string cons_kva_12;
    public string cons_kwh_13;
    public string cons_kvah_13;
    public string cons_kva_13;
    public string cons_kwh_14;
    public string cons_kvah_14;
    public string cons_kva_14;
    public string cons_kwh_15;
    public string cons_kvah_15;
    public string cons_kva_15;
    public string cons_kwh_16;
    public string cons_kvah_16;
    public string cons_kva_16;
    public string cons_kwh_17;
    public string cons_kvah_17;
    public string cons_kva_17;
    public string cons_kwh_18;
    public string cons_kvah_18;
    public string cons_kva_18;
    public string cons_kwh_19;
    public string cons_kvah_19;
    public string cons_kva_19;
    public string cons_kwh_20;
    public string cons_kvah_20;
    public string cons_kva_20;
    public string cons_kwh_21;
    public string cons_kvah_21;
    public string cons_kva_21;
    public string cons_kwh_22;
    public string cons_kvah_22;
    public string cons_kva_22;
    public string cons_kwh_23;
    public string cons_kvah_23;
    public string cons_kva_23;
    public string cons_kwh_24;
    public string cons_kvah_24;
    public string cons_kva_24;
    public string variation;
    public string tod_schr_cons;
    public string tod_schr_amt;
    public string tod_rbt_cons;
    public string tod_rbt_amt;

    //public string sub_division_code;
    //public string mru;
    //public string connected_pole_ini_number;
    //public string neighbor_meter_no;
    //public string street_name;
    //public string installation;
    //public string mr_doc_no;
    //public DateTime scheduled_mrdate;
    //public string meter_number;
    //public int manufacturer_sr_no;
    //public string manufacturer_name;
    //public string contract_account_number;
    //public float consumption_kwh;
    //public float consumption_kwah;
    //public float consumption_kva;
    //public float cur_meter_reading_kwh;
    //public float cur_meter_reading_kva;
    //public float cur_meter_reading_kvah;
    //public DateTime cur_meter_reading_date;
    //public string cur_meter_reading_time;
    //public string cur_meter_reader_note;
    //public float prv_meter_reading_kwh;
    //public float prv_meter_reading_kva;
    //public float prv_meter_reading_kwah;
    //public DateTime prv_meter_reading_date;
    //public string prv_meter_reading_time;
    //public string prv_meter_reader_note;
    //public string octroi_flag;
    //public float sop;
    //public float ed;
    //public float octroi;
    //public float dssf;
    //public float surcharge_leived;
    //public float service_rent;
    //public float meter_rent;
    //public float service_charge;
    //public float monthly_min_charges;
    //public float pf_surcharge;
    //public float pf_incentive;
    //public float demand_charges;
    //public float fixedcharges;
    //public float voltage_surcharge;
    //public float peakload_exemption_charges;
    //public float sundry_charges;
    //public float miscellaneous_charges;
    //public float fuel_adjustment;
    //public string bill_number;
    //public int no_of_days_billed;
    //public int bill_cycle;
    //public string bill_date;
    //public string due_date;
    //public string bill_type;
    //public float payment_amount;
    //public string payment_mode;
    //public string check_no;
    //public string bank_name;
    //public string payment_id;
    //public string ifsc_code;
    //public string micrcode;
    //public string payment_date;
    //public string payment_remark;
    //public float tot_billamount;
    //public string sbm_number;
    //public string meter_reader_name;
    //public string inhouse_outsourced_sbm;
    //public string transfromercode;
    //public float mcb_rent;
    //public float lpsc;
    //public float tot_amt_due_date;
    //public float tot_sop_ed_oct;
    //public string userid;
    //public float empid;
    //public DateTime dtupload;
    //public string synced;
}