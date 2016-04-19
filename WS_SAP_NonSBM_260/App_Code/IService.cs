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
    public string Sub_Div_Code { get; set; }
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
    //new fields
    public string Curr_Water { get; set; }
    public string Adj_Water { get; set; }
    public string Snd_Chrg_Water { get; set; }
    public string Snd_Allw_Water { get; set; }
    public string Arr_Pre_Water { get; set; }
    public string Arr_Curr_Water { get; set; }
    public string Nres1 { get; set; }
    public string Nres2 { get; set; }
    public string Nres3 { get; set; }
    public string Nres4 { get; set; }
    public string Nres5 { get; set; }
    public string Nres6 { get; set; }
    public string Nres7 { get; set; }
    public string Nres8 { get; set; }
    public string Nres9 { get; set; }
    public string Nres10 { get; set; }
    public string Nres11 { get; set; }
    public string Nres12 { get; set; }
    public string Vres1 { get; set; }
    public string Vres2 { get; set; }


    public override string ToString()
    {
        string str = "Tariff_Type = " + Tariff_Type + "\r\n " +
                        "Circle = " + Circle + "\r\n " +
                        "Division = " + Division + "\r\n " +
                        "Sub_Div = " + Sub_Div + "\r\n " +
                        "Sub_Div_Code = " + Sub_Div_Code + "\r\n " +
                        "Bill_Cycle = " + Bill_Cycle + "\r\n " +
                        "Zz_Year = " + Zz_Year + "\r\n " +
                        "Bill_Date = " + Bill_Date + "\r\n " +
                        "Opbel = " + Opbel + "\r\n " +
                        "Faedn_Cash = " + Faedn_Cash + "\r\n " +
                        "Faedn_Cheque = " + Faedn_Cheque + "\r\n " +
                        "Nft_Admvlt = " + Nft_Admvlt + "\r\n " +
                        "Nft_Supvlt = " + Nft_Supvlt + "\r\n " +
                        "Nft_Mtrvlt = " + Nft_Mtrvlt + "\r\n " +
                        "Vkont = " + Vkont + "\r\n " +
                        "Vkona = " + Vkona + "\r\n " +
                        "Name = " + Name + "\r\n " +
                        "Address = " + Address + "\r\n " +
                        "Date_Of_Conn = " + Date_Of_Conn + "\r\n " +
                        "Sndgen = " + Sndgen + "\r\n " +
                        "Sndif = " + Sndif + "\r\n " +
                        "Sndpiu = " + Sndpiu + "\r\n " +
                        "Sndsea = " + Sndsea + "\r\n " +
                        "Sndgen_Date = " + Sndgen_Date + "\r\n " +
                        "Sndif_Date = " + Sndif_Date + "\r\n " +
                        "Sndpiu_Date = " + Sndpiu_Date + "\r\n " +
                        "Sndsea_Date = " + Sndsea_Date + "\r\n " +
                        "Cdgen = " + Cdgen + "\r\n " +
                        "Cdif = " + Cdif + "\r\n " +
                        "Cdpiu = " + Cdpiu + "\r\n " +
                        "Cdsea = " + Cdsea + "\r\n " +
                        "Cdgen_Date = " + Cdgen_Date + "\r\n " +
                        "Cdif_Date = " + Cdif_Date + "\r\n " +
                        "Cdpiu_Date = " + Cdpiu_Date + "\r\n " +
                        "Cdsea_Date = " + Cdsea_Date + "\r\n " +
                        "Ind_Typ = " + Ind_Typ + "\r\n " +
                        "Mmc = " + Mmc + "\r\n " +
                        "Sea_On_Date = " + Sea_On_Date + "\r\n " +
                        "Sea_Off_Date = " + Sea_Off_Date + "\r\n " +
                        "Peak_Load = " + Peak_Load + "\r\n " +
                        "Peak_Load_S_Date = " + Peak_Load_S_Date + "\r\n " +
                        "Peak_Load_E_Date = " + Peak_Load_E_Date + "\r\n " +
                        "Pre_Read_Date = " + Pre_Read_Date + "\r\n " +
                        "Cur_Read_Date = " + Cur_Read_Date + "\r\n " +
                        "No_Days_Bill = " + No_Days_Bill + "\r\n " +
                        "Meter_Owner = " + Meter_Owner + "\r\n " +
                        "Mco_Date = " + Mco_Date + "\r\n " +
                        "Ctpt = " + Ctpt + "\r\n " +
                        "Sjo_Date = " + Sjo_Date + "\r\n " +
                        "Feeder_Code = " + Feeder_Code + "\r\n " +
                        "Feeder_Name = " + Feeder_Name + "\r\n " +
                        "Mtr_Security_Amt = " + Mtr_Security_Amt + "\r\n " +
                        "Adv_Cons_Deposit = " + Adv_Cons_Deposit + "\r\n " +
                        "Metetr_Read_Note = " + Metetr_Read_Note + "\r\n " +
                        "Pre_Met_Red_Kwh = " + Pre_Met_Red_Kwh + "\r\n " +
                        "Pre_Met_Red_Kvah = " + Pre_Met_Red_Kvah + "\r\n " +
                        "Pre_Met_Red_Kva = " + Pre_Met_Red_Kva + "\r\n " +
                        "Curr_Met_Red_Kwh = " + Curr_Met_Red_Kwh + "\r\n " +
                        "Curr_Met_Red_Kvah = " + Curr_Met_Red_Kvah + "\r\n " +
                        "Curr_Met_Red_Kva = " + Curr_Met_Red_Kva + "\r\n " +
                        "Mtr_Multiplier = " + Mtr_Multiplier + "\r\n " +
                        "Line_Ctr = " + Line_Ctr + "\r\n " +
                        "Mtr_Ratio = " + Mtr_Ratio + "\r\n " +
                        "Mtr_Volt_Ratio = " + Mtr_Volt_Ratio + "\r\n " +
                        "Overall_Multiplier = " + Overall_Multiplier + "\r\n " +
                        "Mmts_Corr = " + Mmts_Corr + "\r\n " +
                        "Add_Sup_Unit_Kwh = " + Add_Sup_Unit_Kwh + "\r\n " +
                        "Add_Sup_Unit_Kvah = " + Add_Sup_Unit_Kvah + "\r\n " +
                        "Add_Sup_Unit_Kva = " + Add_Sup_Unit_Kva + "\r\n " +
                        "Cur_Cons_Kwh = " + Cur_Cons_Kwh + "\r\n " +
                        "Cur_Cons_Kvah = " + Cur_Cons_Kvah + "\r\n " +
                        "Cur_Cons_Kva = " + Cur_Cons_Kva + "\r\n " +
                        "Old_Pre_Met_Kwh = " + Old_Pre_Met_Kwh + "\r\n " +
                        "Old_Pre_Met_Kvah = " + Old_Pre_Met_Kvah + "\r\n " +
                        "Old_Pre_Met_Kva = " + Old_Pre_Met_Kva + "\r\n " +
                        "Old_Curr_Met_Kwh = " + Old_Curr_Met_Kwh + "\r\n " +
                        "Old_Curr_Met_Kvah = " + Old_Curr_Met_Kvah + "\r\n " +
                        "Old_Curr_Met_Kva = " + Old_Curr_Met_Kva + "\r\n " +
                        "Old_Mtr_Multiplier = " + Old_Mtr_Multiplier + "\r\n " +
                        "Old_Line_Ctr = " + Old_Line_Ctr + "\r\n " +
                        "Old_Mtr_Ratio = " + Old_Mtr_Ratio + "\r\n " +
                        "Old_Mtr_Volt_Ratio = " + Old_Mtr_Volt_Ratio + "\r\n " +
                        "Old_All_Multiplier = " + Old_All_Multiplier + "\r\n " +
                        "Old_Mmts_Corr = " + Old_Mmts_Corr + "\r\n " +
                        "Old_Add_Sup_Kwh = " + Old_Add_Sup_Kwh + "\r\n " +
                        "Old_Add_Sup_Kvah = " + Old_Add_Sup_Kvah + "\r\n " +
                        "Old_Add_Sup_Kva = " + Old_Add_Sup_Kva + "\r\n " +
                        "Old_Cons_Kwh = " + Old_Cons_Kwh + "\r\n " +
                        "Old_Cons_Kvah = " + Old_Cons_Kvah + "\r\n " +
                        "Old_Cons_Kva = " + Old_Cons_Kva + "\r\n " +
                        "Tot_Cons_Kwh = " + Tot_Cons_Kwh + "\r\n " +
                        "Tot_Cons_Kvah = " + Tot_Cons_Kvah + "\r\n " +
                        "Tot_Cons_Kva = " + Tot_Cons_Kva + "\r\n " +
                        "Pf = " + Pf + "\r\n " +
                        "Be_Code = " + Be_Code + "\r\n " +
                        "Concess_Cons = " + Concess_Cons + "\r\n " +
                        "Ec = " + Ec + "\r\n " +
                        "Mmc_Add = " + Mmc_Add + "\r\n " +
                        "Pf_Int_Sur = " + Pf_Int_Sur + "\r\n " +
                        "Ht_Rbt = " + Ht_Rbt + "\r\n " +
                        "Fca = " + Fca + "\r\n " +
                        "Dmnd_Schr = " + Dmnd_Schr + "\r\n " +
                        "Plec = " + Plec + "\r\n " +
                        "Volt_Schr = " + Volt_Schr + "\r\n " +
                        "Fix_Chrg = " + Fix_Chrg + "\r\n " +
                        "Ot_Chrg = " + Ot_Chrg + "\r\n " +
                        "Violation_Chrg = " + Violation_Chrg + "\r\n " +
                        "Sop = " + Sop + "\r\n " +
                        "Mtr_Rent = " + Mtr_Rent + "\r\n " +
                        "Mcb_Rent = " + Mcb_Rent + "\r\n " +
                        "Ser_Rent = " + Ser_Rent + "\r\n " +
                        "Ser_Chrg = " + Ser_Chrg + "\r\n " +
                        "Tot_Rentals = " + Tot_Rentals + "\r\n " +
                        "Pre_Round = " + Pre_Round + "\r\n " +
                        "Curr_Round = " + Curr_Round + "\r\n " +
                        "Curr_Sop = " + Curr_Sop + "\r\n " +
                        "Curr_Rental = " + Curr_Rental + "\r\n " +
                        "Curr_Ed = " + Curr_Ed + "\r\n " +
                        "Curr_Oct = " + Curr_Oct + "\r\n " +
                        "Curr_Misc = " + Curr_Misc + "\r\n " +
                        "Curr_Total = " + Curr_Total + "\r\n " +
                        "Adj_Sop = " + Adj_Sop + "\r\n " +
                        "Adj_Rental = " + Adj_Rental + "\r\n " +
                        "Adj_Ed = " + Adj_Ed + "\r\n " +
                        "Adj_Oct = " + Adj_Oct + "\r\n " +
                        "Adj_Misc = " + Adj_Misc + "\r\n " +
                        "Adj_Total = " + Adj_Total + "\r\n " +
                        "Snd_Chrg_Sop = " + Snd_Chrg_Sop + "\r\n " +
                        "Snd_Chrg_Rental = " + Snd_Chrg_Rental + "\r\n " +
                        "Snd_Chrg_Ed = " + Snd_Chrg_Ed + "\r\n " +
                        "Snd_Chrg_Oct = " + Snd_Chrg_Oct + "\r\n " +
                        "Snd_Chrg_Misc = " + Snd_Chrg_Misc + "\r\n " +
                        "Snd_Chrg_Total = " + Snd_Chrg_Total + "\r\n " +
                        "Snd_Allw_Sop = " + Snd_Allw_Sop + "\r\n " +
                        "Snd_Allw_Rental = " + Snd_Allw_Rental + "\r\n " +
                        "Snd_Allw_Ed = " + Snd_Allw_Ed + "\r\n " +
                        "Snd_Allw_Oct = " + Snd_Allw_Oct + "\r\n " +
                        "Snd_Allw_Misc = " + Snd_Allw_Misc + "\r\n " +
                        "Snd_Allw_Total = " + Snd_Allw_Total + "\r\n " +
                        "Arr_Pre_Sop = " + Arr_Pre_Sop + "\r\n " +
                        "Arr_Pre_Rental = " + Arr_Pre_Rental + "\r\n " +
                        "Arr_Pre_Ed = " + Arr_Pre_Ed + "\r\n " +
                        "Arr_Pre_Oct = " + Arr_Pre_Oct + "\r\n " +
                        "Arr_Pre_Misc = " + Arr_Pre_Misc + "\r\n " +
                        "Arr_Pre_Total = " + Arr_Pre_Total + "\r\n " +
                        "Arr_Curr_Sop = " + Arr_Curr_Sop + "\r\n " +
                        "Arr_Curr_Rental = " + Arr_Curr_Rental + "\r\n " +
                        "Arr_Curr_Ed = " + Arr_Curr_Ed + "\r\n " +
                        "Arr_Curr_Oct = " + Arr_Curr_Oct + "\r\n " +
                        "Arr_Curr_Misc = " + Arr_Curr_Misc + "\r\n " +
                        "Arr_Curr_Total = " + Arr_Curr_Total + "\r\n " +
                        "Net_Sop = " + Net_Sop + "\r\n " +
                        "Net_Rental = " + Net_Rental + "\r\n " +
                        "Net_Ed = " + Net_Ed + "\r\n " +
                        "Net_Oct = " + Net_Oct + "\r\n " +
                        "Net_Misc = " + Net_Misc + "\r\n " +
                        "Net_Tot = " + Net_Tot + "\r\n " +
                        "Net_Payable = " + Net_Payable + "\r\n " +
                        "Lpsc_Amnt_2 = " + Lpsc_Amnt_2 + "\r\n " +
                        "Lpcs_Amnt_5 = " + Lpcs_Amnt_5 + "\r\n " +
                        "Amt_Aft_Due_Dat_2 = " + Amt_Aft_Due_Dat_2 + "\r\n " +
                        "Amt_Aft_Due_Dat_5 = " + Amt_Aft_Due_Dat_5 + "\r\n " +
                        "Cons_Kwh_1 = " + Cons_Kwh_1 + "\r\n " +
                        "Cons_Kvah_1 = " + Cons_Kvah_1 + "\r\n " +
                        "Cons_Kva_1 = " + Cons_Kva_1 + "\r\n " +
                        "Cons_Kwh_2 = " + Cons_Kwh_2 + "\r\n " +
                        "Cons_Kvah_2 = " + Cons_Kvah_2 + "\r\n " +
                        "Cons_Kva_2 = " + Cons_Kva_2 + "\r\n " +
                        "Cons_Kwh_3 = " + Cons_Kwh_3 + "\r\n " +
                        "Cons_Kvah_3 = " + Cons_Kvah_3 + "\r\n " +
                        "Cons_Kva_3 = " + Cons_Kva_3 + "\r\n " +
                        "Cons_Kwh_4 = " + Cons_Kwh_4 + "\r\n " +
                        "Cons_Kvah_4 = " + Cons_Kvah_4 + "\r\n " +
                        "Cons_Kva_4 = " + Cons_Kva_4 + "\r\n " +
                        "Cons_Kwh_5 = " + Cons_Kwh_5 + "\r\n " +
                        "Cons_Kvah_5 = " + Cons_Kvah_5 + "\r\n " +
                        "Cons_Kva_5 = " + Cons_Kva_5 + "\r\n " +
                        "Cons_Kwh_6 = " + Cons_Kwh_6 + "\r\n " +
                        "Cons_Kvah_6 = " + Cons_Kvah_6 + "\r\n " +
                        "Cons_Kva_6 = " + Cons_Kva_6 + "\r\n " +
                        "Cons_Kwh_7 = " + Cons_Kwh_7 + "\r\n " +
                        "Cons_Kvah_7 = " + Cons_Kvah_7 + "\r\n " +
                        "Cons_Kva_7 = " + Cons_Kva_7 + "\r\n " +
                        "Cons_Kwh_8 = " + Cons_Kwh_8 + "\r\n " +
                        "Cons_Kvah_8 = " + Cons_Kvah_8 + "\r\n " +
                        "Cons_Kva_8 = " + Cons_Kva_8 + "\r\n " +
                        "Cons_Kwh_9 = " + Cons_Kwh_9 + "\r\n " +
                        "Cons_Kvah_9 = " + Cons_Kvah_9 + "\r\n " +
                        "Cons_Kva_9 = " + Cons_Kva_9 + "\r\n " +
                        "Cons_Kwh_10 = " + Cons_Kwh_10 + "\r\n " +
                        "Cons_Kvah_10 = " + Cons_Kvah_10 + "\r\n " +
                        "Cons_Kva_10 = " + Cons_Kva_10 + "\r\n " +
                        "Cons_Kwh_11 = " + Cons_Kwh_11 + "\r\n " +
                        "Cons_Kvah_11 = " + Cons_Kvah_11 + "\r\n " +
                        "Cons_Kva_11 = " + Cons_Kva_11 + "\r\n " +
                        "Cons_Kwh_12 = " + Cons_Kwh_12 + "\r\n " +
                        "Cons_Kvah_12 = " + Cons_Kvah_12 + "\r\n " +
                        "Cons_Kva_12 = " + Cons_Kva_12 + "\r\n " +
                        "Cons_Kwh_13 = " + Cons_Kwh_13 + "\r\n " +
                        "Cons_Kvah_13 = " + Cons_Kvah_13 + "\r\n " +
                        "Cons_Kva_13 = " + Cons_Kva_13 + "\r\n " +
                        "Cons_Kwh_14 = " + Cons_Kwh_14 + "\r\n " +
                        "Cons_Kvah_14 = " + Cons_Kvah_14 + "\r\n " +
                        "Cons_Kva_14 = " + Cons_Kva_14 + "\r\n " +
                        "Cons_Kwh_15 = " + Cons_Kwh_15 + "\r\n " +
                        "Cons_Kvah_15 = " + Cons_Kvah_15 + "\r\n " +
                        "Cons_Kva_15 = " + Cons_Kva_15 + "\r\n " +
                        "Cons_Kwh_16 = " + Cons_Kwh_16 + "\r\n " +
                        "Cons_Kvah_16 = " + Cons_Kvah_16 + "\r\n " +
                        "Cons_Kva_16 = " + Cons_Kva_16 + "\r\n " +
                        "Cons_Kwh_17 = " + Cons_Kwh_17 + "\r\n " +
                        "Cons_Kvah_17 = " + Cons_Kvah_17 + "\r\n " +
                        "Cons_Kva_17 = " + Cons_Kva_17 + "\r\n " +
                        "Cons_Kwh_18 = " + Cons_Kwh_18 + "\r\n " +
                        "Cons_Kvah_18 = " + Cons_Kvah_18 + "\r\n " +
                        "Cons_Kva_18 = " + Cons_Kva_18 + "\r\n " +
                        "Cons_Kwh_19 = " + Cons_Kwh_19 + "\r\n " +
                        "Cons_Kvah_19 = " + Cons_Kvah_19 + "\r\n " +
                        "Cons_Kva_19 = " + Cons_Kva_19 + "\r\n " +
                        "Cons_Kwh_20 = " + Cons_Kwh_20 + "\r\n " +
                        "Cons_Kvah_20 = " + Cons_Kvah_20 + "\r\n " +
                        "Cons_Kva_20 = " + Cons_Kva_20 + "\r\n " +
                        "Cons_Kwh_21 = " + Cons_Kwh_21 + "\r\n " +
                        "Cons_Kvah_21 = " + Cons_Kvah_21 + "\r\n " +
                        "Cons_Kva_21 = " + Cons_Kva_21 + "\r\n " +
                        "Cons_Kwh_22 = " + Cons_Kwh_22 + "\r\n " +
                        "Cons_Kvah_22 = " + Cons_Kvah_22 + "\r\n " +
                        "Cons_Kva_22 = " + Cons_Kva_22 + "\r\n " +
                        "Cons_Kwh_23 = " + Cons_Kwh_23 + "\r\n " +
                        "Cons_Kvah_23 = " + Cons_Kvah_23 + "\r\n " +
                        "Cons_Kva_23 = " + Cons_Kva_23 + "\r\n " +
                        "Cons_Kwh_24 = " + Cons_Kwh_24 + "\r\n " +
                        "Cons_Kvah_24 = " + Cons_Kvah_24 + "\r\n " +
                        "Cons_Kva_24 = " + Cons_Kva_24 + "\r\n " +
                        "Variation = " + Variation + "\r\n " +
                        "Tod_Schr_Cons = " + Tod_Schr_Cons + "\r\n " +
                        "Tod_Schr_Amt = " + Tod_Schr_Amt + "\r\n " +
                        "Tod_Rbt_Cons = " + Tod_Rbt_Cons + "\r\n " +
                        "Tod_Rbt_Amt = " + Tod_Rbt_Amt + "\r\n " +
                        "Curr_Water = " + Curr_Water + "\r\n " +
                        "Adj_Water = " + Adj_Water + "\r\n " +
                        "Snd_Chrg_Water = " + Snd_Chrg_Water + "\r\n " +
                        "Snd_Allw_Water = " + Snd_Allw_Water + "\r\n " +
                        "Arr_Pre_Water = " + Arr_Pre_Water + "\r\n " +
                        "Arr_Curr_Water = " + Arr_Curr_Water + "\r\n " +
                        "Nres1 = " + Nres1 + "\r\n " +
                        "Nres2 = " + Nres2 + "\r\n " +
                        "Nres3 = " + Nres3 + "\r\n " +
                        "Nres4 = " + Nres4 + "\r\n " +
                        "Nres5 = " + Nres5 + "\r\n " +
                        "Nres6 = " + Nres6 + "\r\n " +
                        "Nres7 = " + Nres7 + "\r\n " +
                        "Nres8 = " + Nres8 + "\r\n " +
                        "Nres9 = " + Nres9 + "\r\n " +
                        "Nres10 = " + Nres10 + "\r\n " +
                        "Nres11 = " + Nres11 + "\r\n " +
                        "Nres12 = " + Nres12 + "\r\n " +
                        "Vres1 = " + Vres1 + "\r\n " +
                        "Vres2 = " + Vres2 + "\r\n " +
                        "Opbel_Hash = " + Opbel_Hash;

        return str;
    }
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
    public SAP_Cols_Ret(string opbel, string status, string message = "")
    {
        //sending opbel in the wrongly named Vkont field
        //name not corrected because this is a breaking change at SAP side
        this.Vkont = opbel;
        this.Status = status;
        this.Message = message;
    }
}
