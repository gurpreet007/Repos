using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

public class Service_Rcv_SAP_Rec : IService
{
    public string GetDataUsingDataContract(SAP_Cols scols)
	{
        string sql, sql_merge, sql_atomic;

		if (scols == null)
		{
			throw new ArgumentNullException("No object received");
		}

        //do all the validation and processing here
        sql = string.Format(
            @"INSERT INTO ONLINEBILL.SAP_NONSBM(
                tariftyp, circle, division, sub_div, bill_cycle, zz_year, bill_date, opbel, faedn_cash, faedn_cheque, 
                nft_admvlt, nft_supvlt, nft_mtrvlt, vkont, vkona, name, address, date_of_conn, sndgen, sndif, 
                sndpiu, sndsea, sndgen_date, sndif_date, sndpiu_date, sndsea_date, cdgen, cdif, cdpiu, cdsea, 
                cdgen_date, cdif_date, cdpiu_date, cdsea_date, ind_typ, mmc, sea_on_date, sea_off_date, peak_load, peak_load_s_date, 
                peak_load_e_date, pre_read_date, cur_read_date, no_days_bill, meter_owner, mco_date, ctpt, sjo_date, feeder_code, feeder_name, 
                mtr_security_amt, adv_cons_deposit, metetr_read_note, pre_met_red_kwh, pre_met_red_kvah, pre_met_red_kva, curr_met_red_kwh, curr_met_red_kvah, curr_met_red_kva, mtr_multiplier, 
                line_ctr, mtr_ratio, mtr_volt_ratio, overall_multiplier, mmts_corr, add_sup_unit_kwh, add_sup_unit_kvah, add_sup_unit_kva, cur_cons_kwh, cur_cons_kvah, 
                cur_cons_kva, old_pre_met_kwh, old_pre_met_kvah, old_pre_met_kva, old_curr_met_kwh, old_curr_met_kvah, old_curr_met_kva, old_mtr_multiplier, old_line_ctr, old_mtr_ratio, 
                old_mtr_volt_ratio, old_all_multiplier, old_mmts_corr, old_add_sup_kwh, old_add_sup_kvah, old_add_sup_kva, old_cons_kwh, old_cons_kvah, old_cons_kva, tot_cons_kwh, 
                tot_cons_kvah, tot_cons_kva, pf, be_code, concess_cons, ec, mmc_add, pf_int_sur, ht_rbt, fca, 
                dmnd_schr, plec, volt_schr, fix_chrg, ot_chrg, violation_chrg, sop, mtr_rent, mcb_rent, ser_rent, 
                ser_chrg, tot_rentals, pre_round, curr_round, curr_sop, curr_rental, curr_ed, curr_oct, curr_misc, curr_total, 
                adj_sop, adj_rental, adj_ed, adj_oct, adj_misc, adj_total, snd_chrg_sop, snd_chrg_rental, snd_chrg_ed, snd_chrg_oct, 
                snd_chrg_misc, snd_chrg_total, snd_allw_sop, snd_allw_rental, snd_allw_ed, snd_allw_oct, snd_allw_misc, snd_allw_total, arr_pre_sop, arr_pre_rental, 
                arr_pre_ed, arr_pre_oct, arr_pre_misc, arr_pre_total, arr_curr_sop, arr_curr_rental, arr_curr_ed, arr_curr_oct, arr_curr_misc, arr_curr_total, 
                net_sop, net_rental, net_ed, net_oct, net_misc, net_tot, net_payable, lpsc_amnt_2, lpcs_amnt_5, amt_aft_due_dat_2, 
                amt_aft_due_dat_5, cons_kwh_1, cons_kvah_1, cons_kva_1, cons_kwh_2, cons_kvah_2, cons_kva_2, cons_kwh_3, cons_kvah_3, cons_kva_3, 
                cons_kwh_4, cons_kvah_4, cons_kva_4, cons_kwh_5, cons_kvah_5, cons_kva_5, cons_kwh_6, cons_kvah_6, cons_kva_6, cons_kwh_7, 
                cons_kvah_7, cons_kva_7, cons_kwh_8, cons_kvah_8, cons_kva_8, cons_kwh_9, cons_kvah_9, cons_kva_9, cons_kwh_10, cons_kvah_10, 
                cons_kva_10, cons_kwh_11, cons_kvah_11, cons_kva_11, cons_kwh_12, cons_kvah_12, cons_kva_12, cons_kwh_13, cons_kvah_13, cons_kva_13, 
                cons_kwh_14, cons_kvah_14, cons_kva_14, cons_kwh_15, cons_kvah_15, cons_kva_15, cons_kwh_16, cons_kvah_16, cons_kva_16, cons_kwh_17, 
                cons_kvah_17, cons_kva_17, cons_kwh_18, cons_kvah_18, cons_kva_18, cons_kwh_19, cons_kvah_19, cons_kva_19, cons_kwh_20, cons_kvah_20, 
                cons_kva_20, cons_kwh_21, cons_kvah_21, cons_kva_21, cons_kwh_22, cons_kvah_22, cons_kva_22, cons_kwh_23, cons_kvah_23, cons_kva_23, 
                cons_kwh_24, cons_kvah_24, cons_kva_24, variation, tod_schr_cons, tod_schr_amt, tod_rbt_cons, tod_rbt_amt, dated
                ) VALUES (
                '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', 
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
                '{231}', '{232}', '{233}', '{234}', '{235}', '{236}', '{237}', sysdate
                ) ",
                scols.tariftyp, scols.circle, scols.division, scols.sub_div, scols.bill_cycle, scols.zz_year, scols.bill_date, scols.opbel, scols.faedn_cash, scols.faedn_cheque, 
                scols.nft_admvlt, scols.nft_supvlt, scols.nft_mtrvlt, scols.vkont, scols.vkona, scols.name, scols.address, scols.date_of_conn, scols.sndgen, scols.sndif, 
                scols.sndpiu, scols.sndsea, scols.sndgen_date, scols.sndif_date, scols.sndpiu_date, scols.sndsea_date, scols.cdgen, scols.cdif, scols.cdpiu, scols.cdsea, 
                scols.cdgen_date, scols.cdif_date, scols.cdpiu_date, scols.cdsea_date, scols.ind_typ, scols.mmc, scols.sea_on_date, scols.sea_off_date, scols.peak_load, scols.peak_load_s_date, 
                scols.peak_load_e_date, scols.pre_read_date, scols.cur_read_date, scols.no_days_bill, scols.meter_owner, scols.mco_date, scols.ctpt, scols.sjo_date, scols.feeder_code, scols.feeder_name, 
                scols.mtr_security_amt, scols.adv_cons_deposit, scols.metetr_read_note, scols.pre_met_red_kwh, scols.pre_met_red_kvah, scols.pre_met_red_kva, scols.curr_met_red_kwh, scols.curr_met_red_kvah, scols.curr_met_red_kva, scols.mtr_multiplier, 
                scols.line_ctr, scols.mtr_ratio, scols.mtr_volt_ratio, scols.overall_multiplier, scols.mmts_corr, scols.add_sup_unit_kwh, scols.add_sup_unit_kvah, scols.add_sup_unit_kva, scols.cur_cons_kwh, scols.cur_cons_kvah, 
                scols.cur_cons_kva, scols.old_pre_met_kwh, scols.old_pre_met_kvah, scols.old_pre_met_kva, scols.old_curr_met_kwh, scols.old_curr_met_kvah, scols.old_curr_met_kva, scols.old_mtr_multiplier, scols.old_line_ctr, scols.old_mtr_ratio, 
                scols.old_mtr_volt_ratio, scols.old_all_multiplier, scols.old_mmts_corr, scols.old_add_sup_kwh, scols.old_add_sup_kvah, scols.old_add_sup_kva, scols.old_cons_kwh, scols.old_cons_kvah, scols.old_cons_kva, scols.tot_cons_kwh, 
                scols.tot_cons_kvah, scols.tot_cons_kva, scols.pf, scols.be_code, scols.concess_cons, scols.ec, scols.mmc_add, scols.pf_int_sur, scols.ht_rbt, scols.fca, 
                scols.dmnd_schr, scols.plec, scols.volt_schr, scols.fix_chrg, scols.ot_chrg, scols.violation_chrg, scols.sop, scols.mtr_rent, scols.mcb_rent, scols.ser_rent, 
                scols.ser_chrg, scols.tot_rentals, scols.pre_round, scols.curr_round, scols.curr_sop, scols.curr_rental, scols.curr_ed, scols.curr_oct, scols.curr_misc, scols.curr_total, 
                scols.adj_sop, scols.adj_rental, scols.adj_ed, scols.adj_oct, scols.adj_misc, scols.adj_total, scols.snd_chrg_sop, scols.snd_chrg_rental, scols.snd_chrg_ed, scols.snd_chrg_oct, 
                scols.snd_chrg_misc, scols.snd_chrg_total, scols.snd_allw_sop, scols.snd_allw_rental, scols.snd_allw_ed, scols.snd_allw_oct, scols.snd_allw_misc, scols.snd_allw_total, scols.arr_pre_sop, scols.arr_pre_rental, 
                scols.arr_pre_ed, scols.arr_pre_oct, scols.arr_pre_misc, scols.arr_pre_total, scols.arr_curr_sop, scols.arr_curr_rental, scols.arr_curr_ed, scols.arr_curr_oct, scols.arr_curr_misc, scols.arr_curr_total, 
                scols.net_sop, scols.net_rental, scols.net_ed, scols.net_oct, scols.net_misc, scols.net_tot, scols.net_payable, scols.lpsc_amnt_2, scols.lpcs_amnt_5, scols.amt_aft_due_dat_2, 
                scols.amt_aft_due_dat_5, scols.cons_kwh_1, scols.cons_kvah_1, scols.cons_kva_1, scols.cons_kwh_2, scols.cons_kvah_2, scols.cons_kva_2, scols.cons_kwh_3, scols.cons_kvah_3, scols.cons_kva_3, 
                scols.cons_kwh_4, scols.cons_kvah_4, scols.cons_kva_4, scols.cons_kwh_5, scols.cons_kvah_5, scols.cons_kva_5, scols.cons_kwh_6, scols.cons_kvah_6, scols.cons_kva_6, scols.cons_kwh_7, 
                scols.cons_kvah_7, scols.cons_kva_7, scols.cons_kwh_8, scols.cons_kvah_8, scols.cons_kva_8, scols.cons_kwh_9, scols.cons_kvah_9, scols.cons_kva_9, scols.cons_kwh_10, scols.cons_kvah_10, 
                scols.cons_kva_10, scols.cons_kwh_11, scols.cons_kvah_11, scols.cons_kva_11, scols.cons_kwh_12, scols.cons_kvah_12, scols.cons_kva_12, scols.cons_kwh_13, scols.cons_kvah_13, scols.cons_kva_13, 
                scols.cons_kwh_14, scols.cons_kvah_14, scols.cons_kva_14, scols.cons_kwh_15, scols.cons_kvah_15, scols.cons_kva_15, scols.cons_kwh_16, scols.cons_kvah_16, scols.cons_kva_16, scols.cons_kwh_17, 
                scols.cons_kvah_17, scols.cons_kva_17, scols.cons_kwh_18, scols.cons_kvah_18, scols.cons_kva_18, scols.cons_kwh_19, scols.cons_kvah_19, scols.cons_kva_19, scols.cons_kwh_20, scols.cons_kvah_20, 
                scols.cons_kva_20, scols.cons_kwh_21, scols.cons_kvah_21, scols.cons_kva_21, scols.cons_kwh_22, scols.cons_kvah_22, scols.cons_kva_22, scols.cons_kwh_23, scols.cons_kvah_23, scols.cons_kva_23, 
                scols.cons_kwh_24, scols.cons_kvah_24, scols.cons_kva_24, scols.variation, scols.tod_schr_cons, scols.tod_schr_amt, scols.tod_rbt_cons, scols.tod_rbt_amt 
            );

        try
        {
            sql_merge = string.Format("merge into onlinebill.mast_account m1 using " +
                    "(select '{0}' as acno from dual) d on (m1.account_no=d.acno) " +
                    "when matched then update set m1.table_name = '{1}' " +
                    "when not matched then insert (m1.account_no,m1.table_name) values(d.acno,'{1}')",
                    scols.vkont, "ONLINEBILL.SAP_NONSBM");

            sql = sql.Replace(Environment.NewLine, "");
            sql_atomic = string.Format("BEGIN {0}; {1}; END; ", sql, sql_merge);
            OraDBConnection.ExecQry(sql_atomic);
            return string.Format("{0}_{1}", scols.opbel,"SUCCESS");
        }
        catch(Exception ex)
        {
            return string.Format("{0}_{1}", scols.opbel, "FAILURE");
        }
	}
}
