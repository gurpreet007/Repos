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
    SAP_Cols_Ret[] GetDataUsingDataContract(SAP_Cols[] sap_cols);
}

public class SAP_Cols
{
    public string Tariff_Type { get; set; }
    public string Circle { get; set; }
    public string Division { get; set; }
    public string Sub_Div { get; set; }
    public string Bill_Cycle { get; set; }
    public string Zz_Year { get; set; }
    public string Bill_Date { get; set; }
    public string Opbel { get; set; }
    public string Faedn_Cash { get; set; }
    public string Faedn_Cheque { get; set; }
    public string Nft_Admvlt { get; set; }
    public string Nft_Supvlt { get; set; }
    public string Nft_Mtrvlt { get; set; }
    public string Vkont { get; set; }
    public string Vkona { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Date_Of_Conn { get; set; }
    public string Sndgen { get; set; }
    public string Sndif { get; set; }
    public string Sndpiu { get; set; }
    public string Sndsea { get; set; }
    public string Sndgen_Date { get; set; }
    public string Sndif_Date { get; set; }
    public string Sndpiu_Date { get; set; }
    public string Sndsea_Date { get; set; }
    public string Cdgen { get; set; }
    public string Cdif { get; set; }
    public string Cdpiu { get; set; }
    public string Cdsea { get; set; }
    public string Cdgen_Date { get; set; }
    public string Cdif_Date { get; set; }
    public string Cdpiu_Date { get; set; }
    public string Cdsea_Date { get; set; }
    public string Ind_Typ { get; set; }
    public string Mmc { get; set; }
    public string Sea_On_Date { get; set; }
    public string Sea_Off_Date { get; set; }
    public string Peak_Load { get; set; }
    public string Peak_Load_S_Date { get; set; }
    public string Peak_Load_E_Date { get; set; }
    public string Pre_Read_Date { get; set; }
    public string Cur_Read_Date { get; set; }
    public string No_Days_Bill { get; set; }
    public string Meter_Owner { get; set; }
    public string Mco_Date { get; set; }
    public string Ctpt { get; set; }
    public string Sjo_Date { get; set; }
    public string Feeder_Code { get; set; }
    public string Feeder_Name { get; set; }
    public string Mtr_Security_Amt { get; set; }
    public string Adv_Cons_Deposit { get; set; }
    public string Metetr_Read_Note { get; set; }
    public string Pre_Met_Red_Kwh { get; set; }
    public string Pre_Met_Red_Kvah { get; set; }
    public string Pre_Met_Red_Kva { get; set; }
    public string Curr_Met_Red_Kwh { get; set; }
    public string Curr_Met_Red_Kvah { get; set; }
    public string Curr_Met_Red_Kva { get; set; }
    public string Mtr_Multiplier { get; set; }
    public string Line_Ctr { get; set; }
    public string Mtr_Ratio { get; set; }
    public string Mtr_Volt_Ratio { get; set; }
    public string Overall_Multiplier { get; set; }
    public string Mmts_Corr { get; set; }
    public string Add_Sup_Unit_Kwh { get; set; }
    public string Add_Sup_Unit_Kvah { get; set; }
    public string Add_Sup_Unit_Kva { get; set; }
    public string Cur_Cons_Kwh { get; set; }
    public string Cur_Cons_Kvah { get; set; }
    public string Cur_Cons_Kva { get; set; }
    public string Old_Pre_Met_Kwh { get; set; }
    public string Old_Pre_Met_Kvah { get; set; }
    public string Old_Pre_Met_Kva { get; set; }
    public string Old_Curr_Met_Kwh { get; set; }
    public string Old_Curr_Met_Kvah { get; set; }
    public string Old_Curr_Met_Kva { get; set; }
    public string Old_Mtr_Multiplier { get; set; }
    public string Old_Line_Ctr { get; set; }
    public string Old_Mtr_Ratio { get; set; }
    public string Old_Mtr_Volt_Ratio { get; set; }
    public string Old_All_Multiplier { get; set; }
    public string Old_Mmts_Corr { get; set; }
    public string Old_Add_Sup_Kwh { get; set; }
    public string Old_Add_Sup_Kvah { get; set; }
    public string Old_Add_Sup_Kva { get; set; }
    public string Old_Cons_Kwh { get; set; }
    public string Old_Cons_Kvah { get; set; }
    public string Old_Cons_Kva { get; set; }
    public string Tot_Cons_Kwh { get; set; }
    public string Tot_Cons_Kvah { get; set; }
    public string Tot_Cons_Kva { get; set; }
    public string Pf { get; set; }
    public string Be_Code { get; set; }
    public string Concess_Cons { get; set; }
    public string Ec { get; set; }
    public string Mmc_Add { get; set; }
    public string Pf_Int_Sur { get; set; }
    public string Ht_Rbt { get; set; }
    public string Fca { get; set; }
    public string Dmnd_Schr { get; set; }
    public string Plec { get; set; }
    public string Volt_Schr { get; set; }
    public string Fix_Chrg { get; set; }
    public string Ot_Chrg { get; set; }
    public string Violation_Chrg { get; set; }
    public string Sop { get; set; }
    public string Mtr_Rent { get; set; }
    public string Mcb_Rent { get; set; }
    public string Ser_Rent { get; set; }
    public string Ser_Chrg { get; set; }
    public string Tot_Rentals { get; set; }
    public string Pre_Round { get; set; }
    public string Curr_Round { get; set; }
    public string Curr_Sop { get; set; }
    public string Curr_Rental { get; set; }
    public string Curr_Ed { get; set; }
    public string Curr_Oct { get; set; }
    public string Curr_Misc { get; set; }
    public string Curr_Total { get; set; }
    public string Adj_Sop { get; set; }
    public string Adj_Rental { get; set; }
    public string Adj_Ed { get; set; }
    public string Adj_Oct { get; set; }
    public string Adj_Misc { get; set; }
    public string Adj_Total { get; set; }
    public string Snd_Chrg_Sop { get; set; }
    public string Snd_Chrg_Rental { get; set; }
    public string Snd_Chrg_Ed { get; set; }
    public string Snd_Chrg_Oct { get; set; }
    public string Snd_Chrg_Misc { get; set; }
    public string Snd_Chrg_Total { get; set; }
    public string Snd_Allw_Sop { get; set; }
    public string Snd_Allw_Rental { get; set; }
    public string Snd_Allw_Ed { get; set; }
    public string Snd_Allw_Oct { get; set; }
    public string Snd_Allw_Misc { get; set; }
    public string Snd_Allw_Total { get; set; }
    public string Arr_Pre_Sop { get; set; }
    public string Arr_Pre_Rental { get; set; }
    public string Arr_Pre_Ed { get; set; }
    public string Arr_Pre_Oct { get; set; }
    public string Arr_Pre_Misc { get; set; }
    public string Arr_Pre_Total { get; set; }
    public string Arr_Curr_Sop { get; set; }
    public string Arr_Curr_Rental { get; set; }
    public string Arr_Curr_Ed { get; set; }
    public string Arr_Curr_Oct { get; set; }
    public string Arr_Curr_Misc { get; set; }
    public string Arr_Curr_Total { get; set; }
    public string Net_Sop { get; set; }
    public string Net_Rental { get; set; }
    public string Net_Ed { get; set; }
    public string Net_Oct { get; set; }
    public string Net_Misc { get; set; }
    public string Net_Tot { get; set; }
    public string Net_Payable { get; set; }
    public string Lpsc_Amnt_2 { get; set; }
    public string Lpcs_Amnt_5 { get; set; }
    public string Amt_Aft_Due_Dat_2 { get; set; }
    public string Amt_Aft_Due_Dat_5 { get; set; }
    public string Cons_Kwh_1 { get; set; }
    public string Cons_Kvah_1 { get; set; }
    public string Cons_Kva_1 { get; set; }
    public string Cons_Kwh_2 { get; set; }
    public string Cons_Kvah_2 { get; set; }
    public string Cons_Kva_2 { get; set; }
    public string Cons_Kwh_3 { get; set; }
    public string Cons_Kvah_3 { get; set; }
    public string Cons_Kva_3 { get; set; }
    public string Cons_Kwh_4 { get; set; }
    public string Cons_Kvah_4 { get; set; }
    public string Cons_Kva_4 { get; set; }
    public string Cons_Kwh_5 { get; set; }
    public string Cons_Kvah_5 { get; set; }
    public string Cons_Kva_5 { get; set; }
    public string Cons_Kwh_6 { get; set; }
    public string Cons_Kvah_6 { get; set; }
    public string Cons_Kva_6 { get; set; }
    public string Cons_Kwh_7 { get; set; }
    public string Cons_Kvah_7 { get; set; }
    public string Cons_Kva_7 { get; set; }
    public string Cons_Kwh_8 { get; set; }
    public string Cons_Kvah_8 { get; set; }
    public string Cons_Kva_8 { get; set; }
    public string Cons_Kwh_9 { get; set; }
    public string Cons_Kvah_9 { get; set; }
    public string Cons_Kva_9 { get; set; }
    public string Cons_Kwh_10 { get; set; }
    public string Cons_Kvah_10 { get; set; }
    public string Cons_Kva_10 { get; set; }
    public string Cons_Kwh_11 { get; set; }
    public string Cons_Kvah_11 { get; set; }
    public string Cons_Kva_11 { get; set; }
    public string Cons_Kwh_12 { get; set; }
    public string Cons_Kvah_12 { get; set; }
    public string Cons_Kva_12 { get; set; }
    public string Cons_Kwh_13 { get; set; }
    public string Cons_Kvah_13 { get; set; }
    public string Cons_Kva_13 { get; set; }
    public string Cons_Kwh_14 { get; set; }
    public string Cons_Kvah_14 { get; set; }
    public string Cons_Kva_14 { get; set; }
    public string Cons_Kwh_15 { get; set; }
    public string Cons_Kvah_15 { get; set; }
    public string Cons_Kva_15 { get; set; }
    public string Cons_Kwh_16 { get; set; }
    public string Cons_Kvah_16 { get; set; }
    public string Cons_Kva_16 { get; set; }
    public string Cons_Kwh_17 { get; set; }
    public string Cons_Kvah_17 { get; set; }
    public string Cons_Kva_17 { get; set; }
    public string Cons_Kwh_18 { get; set; }
    public string Cons_Kvah_18 { get; set; }
    public string Cons_Kva_18 { get; set; }
    public string Cons_Kwh_19 { get; set; }
    public string Cons_Kvah_19 { get; set; }
    public string Cons_Kva_19 { get; set; }
    public string Cons_Kwh_20 { get; set; }
    public string Cons_Kvah_20 { get; set; }
    public string Cons_Kva_20 { get; set; }
    public string Cons_Kwh_21 { get; set; }
    public string Cons_Kvah_21 { get; set; }
    public string Cons_Kva_21 { get; set; }
    public string Cons_Kwh_22 { get; set; }
    public string Cons_Kvah_22 { get; set; }
    public string Cons_Kva_22 { get; set; }
    public string Cons_Kwh_23 { get; set; }
    public string Cons_Kvah_23 { get; set; }
    public string Cons_Kva_23 { get; set; }
    public string Cons_Kwh_24 { get; set; }
    public string Cons_Kvah_24 { get; set; }
    public string Cons_Kva_24 { get; set; }
    public string Variation { get; set; }
    public string Tod_Schr_Cons { get; set; }
    public string Tod_Schr_Amt { get; set; }
    public string Tod_Rbt_Cons { get; set; }
    public string Tod_Rbt_Amt { get; set; }
    public string Opbel_Hash { get; set; }
}

[DataContract]
public class SAP_Cols_Ret
{
    [DataMember]
    public string Vkont { get; private set; }
    [DataMember]
    public string Status { get; private set; }
    [DataMember]
    public string Message { get; private set; }
    public SAP_Cols_Ret(string vkont, string status, string message = "")
    {
        this.Vkont = vkont;
        this.Status = status;
        this.Message = message;
    }
}
