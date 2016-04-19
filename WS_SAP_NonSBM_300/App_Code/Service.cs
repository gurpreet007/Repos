using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Diagnostics;

public class Service_WS_SAP_NonSBM_300 : IService
{
    private EventLog eventLog1;

    #region cyphercode
    private const string crypto_key = "1121231234zyx12345123456";
    private const string crypto_iv = "A1B2C3D4E5F6A7B8";
    #endregion
    public SAP_Cols_Ret[] GetDataUsingDataContract(SAP_Cols[] arr_scols)
    {
        #region variables
        string sql, sql_merge, sql_atomic;
        string sqlIns = @"INSERT INTO ONLINEBILL.SAP_NONSBM(
                    tariftyp, circle, division, sub_div, bill_cycle,
                    zz_year, bill_date, opbel, faedn_cash, faedn_cheque, 
                    nft_admvlt, nft_supvlt, nft_mtrvlt, vkont, vkona, 
                    name, address, date_of_conn, sndgen, sndif, 
                    sndpiu, sndsea, sndgen_date, sndif_date, sndpiu_date, 
                    sndsea_date, cdgen, cdif, cdpiu, cdsea, 
                    cdgen_date, cdif_date, cdpiu_date, cdsea_date, ind_typ, mmc, 
                    sea_on_date, sea_off_date, peak_load, peak_load_s_date, 
                    peak_load_e_date, pre_read_date, cur_read_date, no_days_bill, meter_owner, 
                    mco_date, ctpt, sjo_date, feeder_code, feeder_name, 
                    mtr_security_amt, adv_cons_deposit, metetr_read_note, pre_met_red_kwh, pre_met_red_kvah, 
                    pre_met_red_kva, curr_met_red_kwh, curr_met_red_kvah, curr_met_red_kva, mtr_multiplier, 
                    line_ctr, mtr_ratio, mtr_volt_ratio, overall_multiplier, mmts_corr, 
                    add_sup_unit_kwh, add_sup_unit_kvah, add_sup_unit_kva, cur_cons_kwh, cur_cons_kvah, 
                    cur_cons_kva, old_pre_met_kwh, old_pre_met_kvah, old_pre_met_kva, old_curr_met_kwh, 
                    old_curr_met_kvah, old_curr_met_kva, old_mtr_multiplier, old_line_ctr, old_mtr_ratio, 
                    old_mtr_volt_ratio, old_all_multiplier, old_mmts_corr, old_add_sup_kwh, old_add_sup_kvah, 
                    old_add_sup_kva, old_cons_kwh, old_cons_kvah, old_cons_kva, tot_cons_kwh, 
                    tot_cons_kvah, tot_cons_kva, pf, be_code, concess_cons, 
                    ec, mmc_add, pf_int_sur, ht_rbt, fca, 
                    dmnd_schr, plec, volt_schr, fix_chrg, ot_chrg, 
                    violation_chrg, sop, mtr_rent, mcb_rent, ser_rent, 
                    ser_chrg, tot_rentals, pre_round, curr_round, curr_sop, 
                    curr_rental, curr_ed, curr_oct, curr_misc, curr_total, 
                    adj_sop, adj_rental, adj_ed, adj_oct, adj_misc, 
                    adj_total, snd_chrg_sop, snd_chrg_rental, snd_chrg_ed, snd_chrg_oct, 
                    snd_chrg_misc, snd_chrg_total, snd_allw_sop, snd_allw_rental, snd_allw_ed, 
                    snd_allw_oct, snd_allw_misc, snd_allw_total, arr_pre_sop, arr_pre_rental, 
                    arr_pre_ed, arr_pre_oct, arr_pre_misc, arr_pre_total, arr_curr_sop, 
                    arr_curr_rental, arr_curr_ed, arr_curr_oct, arr_curr_misc, arr_curr_total, 
                    net_sop, net_rental, net_ed, net_oct, net_misc, 
                    net_tot, net_payable, lpsc_amnt_2, lpcs_amnt_5, amt_aft_due_dat_2, 
                    amt_aft_due_dat_5, cons_kwh_1, cons_kvah_1, cons_kva_1, cons_kwh_2, 
                    cons_kvah_2, cons_kva_2, cons_kwh_3, cons_kvah_3, cons_kva_3, 
                    cons_kwh_4, cons_kvah_4, cons_kva_4, cons_kwh_5, cons_kvah_5, 
                    cons_kva_5, cons_kwh_6, cons_kvah_6, cons_kva_6, cons_kwh_7, 
                    cons_kvah_7, cons_kva_7, cons_kwh_8, cons_kvah_8, cons_kva_8, 
                    cons_kwh_9, cons_kvah_9, cons_kva_9, cons_kwh_10, cons_kvah_10, 
                    cons_kva_10, cons_kwh_11, cons_kvah_11, cons_kva_11, cons_kwh_12, 
                    cons_kvah_12, cons_kva_12, cons_kwh_13, cons_kvah_13, cons_kva_13, 
                    cons_kwh_14, cons_kvah_14, cons_kva_14, cons_kwh_15, cons_kvah_15, 
                    cons_kva_15, cons_kwh_16, cons_kvah_16, cons_kva_16, cons_kwh_17, 
                    cons_kvah_17, cons_kva_17, cons_kwh_18, cons_kvah_18, cons_kva_18, 
                    cons_kwh_19, cons_kvah_19, cons_kva_19, cons_kwh_20, cons_kvah_20, 
                    cons_kva_20, cons_kwh_21, cons_kvah_21, cons_kva_21, cons_kwh_22, 
                    cons_kvah_22, cons_kva_22, cons_kwh_23, cons_kvah_23, cons_kva_23, 
                    cons_kwh_24, cons_kvah_24, cons_kva_24, variation, tod_schr_cons, 
                    tod_schr_amt, tod_rbt_cons, tod_rbt_amt, sub_div_code, curr_water, 
                    adj_water, snd_chrg_water, snd_allw_water, arr_pre_water, arr_curr_water,
                    pre_met_red_kwhex, pre_met_red_kvhex, pre_met_red_kvaex, pre_met_red_kwhnet, pre_met_red_kvhnet,
                    pre_met_red_kvanet, curr_met_red_kwhex, curr_met_red_kvhex, curr_met_red_kvaex, curr_met_red_kwhnet,
                    curr_met_red_kvhnet, curr_met_red_kvanet, add_sup_unit_kwhex, add_sup_unit_kvhex, add_sup_unit_kvaex,
                    add_sup_unit_kwhnet, add_sup_unit_kvhnet, add_sup_unit_kvanet, cur_cons_kwhex, cur_cons_kvhex,
                    cur_cons_kvaex, cur_cons_kwhnet, cur_cons_kvhnet, cur_cons_kvanet, pre_met_red_kwhs,
                    pre_met_red_kvahs, pre_met_red_kvas, curr_met_red_kwhs, curr_met_red_kvahs, curr_met_red_kvas,
                    add_sup_unit_kwhs, add_sup_unit_kvahs, add_sup_unit_kvas, cur_cons_kwhs, cur_cons_kvahs,
                    cur_cons_kvas, pre_carry, curr_carry, tot_cons, nres1, 
                    nres2, nres3, nres4, nres5, nres6,
                    nres7, nres8, nres9, nres10, nres11,
                    nres12, nres13, nres14, vres1, vres2,
                    dated
                    ) VALUES (
                    '{0}', '{1}', '{2}', '{3}', '{4}', '{5}',to_date('{6}','yyyymmdd'), '{7}', to_date('{8}','yyyymmdd'), to_date('{9}','yyyymmdd'), '{10}', 
                    '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', 
                    '{21}', '{22}', '{23}', '{24}', '{25}', '{26}', '{27}', '{28}', '{29}', '{30}', 
                    '{31}', '{32}', '{33}', '{34}', '{35}', '{36}', '{37}', '{38}', '{39}', '{40}', 
                    '{41}', '{42}', '{43}', '{44}', '{45}', '{46}', '{47}', '{48}', '{49}', '{50}', 
                    '{51}', '{52}', '{53}', '{54}', '{55}', '{56}', '{57}', '{58}', '{59}', '{60}', 
                    '{61}', '{62}', '{63}', '{64}', '{65}', '{66}', '{67}', '{68}', '{69}', '{70}', 
                    '{71}', '{72}', '{73}', '{74}', '{75}', '{76}', '{77}', '{78}', '{79}', '{80}', 
                    '{81}', '{82}', '{83}', '{84}', '{85}', '{86}', '{87}', '{88}', '{89}', '{90}', 
                    '{91}', '{92}', '{93}', '{94}', '{95}', '{96}', '{97}', '{98}', '{99}', '{100}', 
                    '{101}', '{102}', '{103}', '{104}', '{105}', '{106}', '{107}', '{108}', '{109}', '{110}', 
                    '{111}', '{112}', '{113}', '{114}', '{115}', '{116}', '{117}', '{118}', '{119}', '{120}', 
                    '{121}', '{122}', '{123}', '{124}', '{125}', '{126}', '{127}', '{128}', '{129}', '{130}', 
                    '{131}', '{132}', '{133}', '{134}', '{135}', '{136}', '{137}', '{138}', '{139}', '{140}', 
                    '{141}', '{142}', '{143}', '{144}', '{145}', '{146}', '{147}', '{148}', '{149}', '{150}', 
                    '{151}', '{152}', '{153}', '{154}', '{155}', '{156}', '{157}', '{158}', '{159}', '{160}', 
                    '{161}', '{162}', '{163}', '{164}', '{165}', '{166}', '{167}', '{168}', '{169}', '{170}', 
                    '{171}', '{172}', '{173}', '{174}', '{175}', '{176}', '{177}', '{178}', '{179}', '{180}', 
                    '{181}', '{182}', '{183}', '{184}', '{185}', '{186}', '{187}', '{188}', '{189}', '{190}', 
                    '{191}', '{192}', '{193}', '{194}', '{195}', '{196}', '{197}', '{198}', '{199}', '{200}', 
                    '{201}', '{202}', '{203}', '{204}', '{205}', '{206}', '{207}', '{208}', '{209}', '{210}', 
                    '{211}', '{212}', '{213}', '{214}', '{215}', '{216}', '{217}', '{218}', '{219}', '{220}', 
                    '{221}', '{222}', '{223}', '{224}', '{225}', '{226}', '{227}', '{228}', '{229}', '{230}', 
                    '{231}', '{232}', '{233}', '{234}', '{235}', '{236}', '{237}', '{238}', '{239}', '{240}',
                    '{241}', '{242}', '{243}', '{244}', '{245}', '{246}', '{247}', '{248}', '{249}', '{250}',
                    '{251}', '{252}', '{253}', '{254}', '{255}', '{256}', '{257}', '{258}', '{259}', '{260}',
                    '{261}', '{262}', '{263}', '{264}', '{265}', '{266}', '{267}', '{268}', '{269}', '{270}',
                    '{271}', '{272}', '{273}', '{274}', '{275}', '{276}', '{277}', '{278}', '{279}', '{280}',
                    '{281}', '{282}', '{283}', '{284}', '{285}', '{286}', '{287}', '{288}', '{289}', '{290}',
                    '{291}', '{292}', '{293}', '{294}', '{295}', '{296}', '{297}', '{298}', '{299}', sysdate
                    ) ";
        //string sqlMerge = "merge into onlinebill.mast_account m1 using " +
        //            "(select '{0}' as acno from dual) d on (m1.account_no=d.acno) " +
        //            "when matched then update set m1.table_name = '{1}', m1.cname = '{2}', m1.category = '{3}', if_sap = 'Y', m1.code_sdiv='{4}', m1.updatedt = sysdate " +
        //            "when not matched then insert (m1.account_no, m1.table_name, m1.cname, m1.category, m1.if_sap, m1.code_sdiv, m1.updatedt) "+
        //            "values(d.acno,'{1}','{2}','{3}','Y','{4}', sysdate) ";

        //this is an oracle comment to enable begin end block below
        string sqlMerge = "--";

        List<SAP_Cols_Ret> lstColsRet = new List<SAP_Cols_Ret>();
        #endregion

        using (eventLog1 = new EventLog("ePay_NonSBM", ".", "SAP_NonSBM_300"))
        {
            if (arr_scols == null)
            {
                eventLog1.WriteEntry("No Object Received");
                throw new ArgumentNullException("No Object Received");
            }

            foreach (SAP_Cols scols in arr_scols)
            {
                //eventLog1.WriteEntry(string.Format("Sub Div Code = {0}", scols.Sub_Div_Code));
                #region queries
                sql = string.Format(sqlIns,
                        scols.Tariff_Type, scols.Circle, scols.Division, scols.Sub_Div, scols.Bill_Cycle,
                        scols.Zz_Year, scols.Bill_Date, scols.Opbel, scols.Faedn_Cash, scols.Faedn_Cheque,
                        scols.Nft_Admvlt, scols.Nft_Supvlt, scols.Nft_Mtrvlt, scols.Vkont, scols.Vkona,
                        scols.Name.Replace("'", "`"), scols.Address.Replace("'", "`"), scols.Date_Of_Conn, scols.Sndgen, scols.Sndif,
                        scols.Sndpiu, scols.Sndsea, scols.Sndgen_Date, scols.Sndif_Date, scols.Sndpiu_Date,
                        scols.Sndsea_Date, scols.Cdgen, scols.Cdif, scols.Cdpiu, scols.Cdsea,
                        scols.Cdgen_Date, scols.Cdif_Date, scols.Cdpiu_Date, scols.Cdsea_Date, scols.Ind_Typ,
                        scols.Mmc, scols.Sea_On_Date, scols.Sea_Off_Date, scols.Peak_Load, scols.Peak_Load_S_Date,
                        scols.Peak_Load_E_Date, scols.Pre_Read_Date, scols.Cur_Read_Date, scols.No_Days_Bill, scols.Meter_Owner,
                        scols.Mco_Date, scols.Ctpt, scols.Sjo_Date, scols.Feeder_Code, scols.Feeder_Name,
                        scols.Mtr_Security_Amt, scols.Adv_Cons_Deposit, scols.Metetr_Read_Note, scols.Pre_Met_Red_Kwh, scols.Pre_Met_Red_Kvah,
                        scols.Pre_Met_Red_Kva, scols.Curr_Met_Red_Kwh, scols.Curr_Met_Red_Kvah, scols.Curr_Met_Red_Kva, scols.Mtr_Multiplier,
                        scols.Line_Ctr, scols.Mtr_Ratio, scols.Mtr_Volt_Ratio, scols.Overall_Multiplier, scols.Mmts_Corr,
                        scols.Add_Sup_Unit_Kwh, scols.Add_Sup_Unit_Kvah, scols.Add_Sup_Unit_Kva, scols.Cur_Cons_Kwh, scols.Cur_Cons_Kvah,
                        scols.Cur_Cons_Kva, scols.Old_Pre_Met_Kwh, scols.Old_Pre_Met_Kvah, scols.Old_Pre_Met_Kva, scols.Old_Curr_Met_Kwh,
                        scols.Old_Curr_Met_Kvah, scols.Old_Curr_Met_Kva, scols.Old_Mtr_Multiplier, scols.Old_Line_Ctr, scols.Old_Mtr_Ratio,
                        scols.Old_Mtr_Volt_Ratio, scols.Old_All_Multiplier, scols.Old_Mmts_Corr, scols.Old_Add_Sup_Kwh, scols.Old_Add_Sup_Kvah,
                        scols.Old_Add_Sup_Kva, scols.Old_Cons_Kwh, scols.Old_Cons_Kvah, scols.Old_Cons_Kva, scols.Tot_Cons_Kwh,
                        scols.Tot_Cons_Kvah, scols.Tot_Cons_Kva, scols.Pf, scols.Be_Code, scols.Concess_Cons,
                        scols.Ec, scols.Mmc_Add, scols.Pf_Int_Sur, scols.Ht_Rbt, scols.Fca,
                        scols.Dmnd_Schr, scols.Plec, scols.Volt_Schr, scols.Fix_Chrg, scols.Ot_Chrg,
                        scols.Violation_Chrg, scols.Sop, scols.Mtr_Rent, scols.Mcb_Rent, scols.Ser_Rent,
                        scols.Ser_Chrg, scols.Tot_Rentals, scols.Pre_Round, scols.Curr_Round, scols.Curr_Sop,
                        scols.Curr_Rental, scols.Curr_Ed, scols.Curr_Oct, scols.Curr_Misc, scols.Curr_Total,
                        scols.Adj_Sop, scols.Adj_Rental, scols.Adj_Ed, scols.Adj_Oct, scols.Adj_Misc,
                        scols.Adj_Total, scols.Snd_Chrg_Sop, scols.Snd_Chrg_Rental, scols.Snd_Chrg_Ed, scols.Snd_Chrg_Oct,
                        scols.Snd_Chrg_Misc, scols.Snd_Chrg_Total, scols.Snd_Allw_Sop, scols.Snd_Allw_Rental, scols.Snd_Allw_Ed,
                        scols.Snd_Allw_Oct, scols.Snd_Allw_Misc, scols.Snd_Allw_Total, scols.Arr_Pre_Sop, scols.Arr_Pre_Rental,
                        scols.Arr_Pre_Ed, scols.Arr_Pre_Oct, scols.Arr_Pre_Misc, scols.Arr_Pre_Total, scols.Arr_Curr_Sop,
                        scols.Arr_Curr_Rental, scols.Arr_Curr_Ed, scols.Arr_Curr_Oct, scols.Arr_Curr_Misc, scols.Arr_Curr_Total,
                        scols.Net_Sop, scols.Net_Rental, scols.Net_Ed, scols.Net_Oct, scols.Net_Misc,
                        scols.Net_Tot, scols.Net_Payable, scols.Lpsc_Amnt_2, scols.Lpcs_Amnt_5, scols.Amt_Aft_Due_Dat_2,
                        scols.Amt_Aft_Due_Dat_5, scols.Cons_Kwh_1, scols.Cons_Kvah_1, scols.Cons_Kva_1, scols.Cons_Kwh_2,
                        scols.Cons_Kvah_2, scols.Cons_Kva_2, scols.Cons_Kwh_3, scols.Cons_Kvah_3, scols.Cons_Kva_3,
                        scols.Cons_Kwh_4, scols.Cons_Kvah_4, scols.Cons_Kva_4, scols.Cons_Kwh_5, scols.Cons_Kvah_5,
                        scols.Cons_Kva_5, scols.Cons_Kwh_6, scols.Cons_Kvah_6, scols.Cons_Kva_6, scols.Cons_Kwh_7,
                        scols.Cons_Kvah_7, scols.Cons_Kva_7, scols.Cons_Kwh_8, scols.Cons_Kvah_8, scols.Cons_Kva_8,
                        scols.Cons_Kwh_9, scols.Cons_Kvah_9, scols.Cons_Kva_9, scols.Cons_Kwh_10, scols.Cons_Kvah_10,
                        scols.Cons_Kva_10, scols.Cons_Kwh_11, scols.Cons_Kvah_11, scols.Cons_Kva_11, scols.Cons_Kwh_12,
                        scols.Cons_Kvah_12, scols.Cons_Kva_12, scols.Cons_Kwh_13, scols.Cons_Kvah_13, scols.Cons_Kva_13,
                        scols.Cons_Kwh_14, scols.Cons_Kvah_14, scols.Cons_Kva_14, scols.Cons_Kwh_15, scols.Cons_Kvah_15,
                        scols.Cons_Kva_15, scols.Cons_Kwh_16, scols.Cons_Kvah_16, scols.Cons_Kva_16, scols.Cons_Kwh_17,
                        scols.Cons_Kvah_17, scols.Cons_Kva_17, scols.Cons_Kwh_18, scols.Cons_Kvah_18, scols.Cons_Kva_18,
                        scols.Cons_Kwh_19, scols.Cons_Kvah_19, scols.Cons_Kva_19, scols.Cons_Kwh_20, scols.Cons_Kvah_20,
                        scols.Cons_Kva_20, scols.Cons_Kwh_21, scols.Cons_Kvah_21, scols.Cons_Kva_21, scols.Cons_Kwh_22,
                        scols.Cons_Kvah_22, scols.Cons_Kva_22, scols.Cons_Kwh_23, scols.Cons_Kvah_23, scols.Cons_Kva_23,
                        scols.Cons_Kwh_24, scols.Cons_Kvah_24, scols.Cons_Kva_24, scols.Variation, scols.Tod_Schr_Cons,
                        scols.Tod_Schr_Amt, scols.Tod_Rbt_Cons, scols.Tod_Rbt_Amt, scols.Sub_Div_Code, scols.Curr_Water, 
                        scols.Adj_Water, scols.Snd_Chrg_Water, scols.Snd_Allw_Water, scols.Arr_Pre_Water, scols.Arr_Curr_Water,
                        scols.Pre_Met_Red_Kwhex, scols.Pre_Met_Red_Kvhex, scols.Pre_Met_Red_Kvaex, scols.Pre_Met_Red_Kwhnet, scols.Pre_Met_Red_Kvhnet,
                        scols.Pre_Met_Red_Kvanet, scols.Curr_Met_Red_Kwhex, scols.Curr_Met_Red_Kvhex, scols.Curr_Met_Red_Kvaex, scols.Curr_Met_Red_Kwhnet,
                        scols.Curr_Met_Red_Kvhnet, scols.Curr_Met_Red_Kvanet, scols.Add_Sup_Unit_Kwhex, scols.Add_Sup_Unit_Kvhex, scols.Add_Sup_Unit_Kvaex,
                        scols.Add_Sup_Unit_Kwhnet, scols.Add_Sup_Unit_Kvhnet, scols.Add_Sup_Unit_Kvanet, scols.Cur_Cons_Kwhex, scols.Cur_Cons_Kvhex,
                        scols.Cur_Cons_Kvaex, scols.Cur_Cons_Kwhnet, scols.Cur_Cons_Kvhnet, scols.Cur_Cons_Kvanet, scols.Pre_Met_Red_Kwhs,
                        scols.Pre_Met_Red_Kvahs, scols.Pre_Met_Red_Kvas, scols.Curr_Met_Red_Kwhs, scols.Curr_Met_Red_Kvahs, scols.Curr_Met_Red_Kvas,
                        scols.Add_Sup_Unit_Kwhs, scols.Add_Sup_Unit_Kvahs, scols.Add_Sup_Unit_Kvas, scols.Cur_Cons_Kwhs, scols.Cur_Cons_Kvahs,
                        scols.Cur_Cons_Kvas, scols.Pre_Carry, scols.Curr_Carry, scols.Tot_Cons, scols.Nres1,
                        scols.Nres2, scols.Nres3, scols.Nres4, scols.Nres5, scols.Nres6, scols.Nres7,
                        scols.Nres8, scols.Nres9, scols.Nres10, scols.Nres11, scols.Nres12, scols.Nres13,
                        scols.Nres14, scols.Vres1, scols.Vres2
                    );

                sql_merge = string.Format(sqlMerge, scols.Vkont, "ONLINEBILL.SAP_NONSBM", 
                    scols.Name.Replace("'", "`").ToUpper().Trim(), scols.Tariff_Type.ToUpper().Trim(), scols.Sub_Div_Code.Trim());

                sql_atomic = string.Format("BEGIN {0}; {1}; END; ", sql, sql_merge).Replace(Environment.NewLine, "");
                #endregion 
                try
                {
                   // if (Crypto.Cipher(scols.Opbel, crypto_key, crypto_iv) != scols.Opbel_Hash)
                    //{
                      //  lstColsRet.Add(new SAP_Cols_Ret(scols.Opbel, "F", "Invalid Hash"));
                  //  }
                  //  else
                  //  {
                        OraDBConnection.ExecQry(sql_atomic);
                        lstColsRet.Add(new SAP_Cols_Ret(scols.Opbel, "S"));
                   // }
                }
                catch (Exception ex)
                {
                    lstColsRet.Add(new SAP_Cols_Ret(scols.Opbel, "F", ex.Message.Replace(Environment.NewLine, " ")));
                    eventLog1.WriteEntry(string.Format("{0}_{1}_{2}_{3}", scols.Opbel, "FAILURE", ex.Message, scols.ToString()));
                }
            }
        }
        return lstColsRet.ToArray();
    }
}
