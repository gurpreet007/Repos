using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Diagnostics;

public class Service_SAP_NCODE : IService
{
    private EventLog eventLog1;

    private const string crypto_key = "1121231234zyx12345123456";
    private const string crypto_iv = "A1B2C3D4E5F6A7B8";
    public string GetDataUsingDataContract(SAP_Cols_NCode scols)
    {
        string sql, sql_merge, sql_atomic;
        string strRet;

        System.Threading.Thread.Sleep(15000);
        using (eventLog1 = new EventLog("ePayment", ".", "WS_NCODE"))
        {
            if (scols == null)
            {
                eventLog1.WriteEntry("No Object Received");
                throw new ArgumentNullException("No Object Received");
            }

            //string strcipher = Crypto.Cipher(scols.Mr_Doc_No, crypto_key, crypto_iv);
            //if (strcipher != scols.Mr_Doc_No_Hash)
            //{
            //    return string.Format("{0}_{1}", scols.Mr_Doc_No, "INVALIDHASH");
            //}

            sql = string.Format(
                @"INSERT INTO ONLINEBILL.SAP_SBM_GSC(
                sub_division_code,  mru,  connected_pole_ini_number,  neighbor_meter_no,  street_name, 
                installation,  mr_doc_no,  scheduled_mrdate,  meter_number,  manufacturer_sr_no, 
                manufacturer_name,  contract_account_number,  consumption_kwh,  consumption_kwah,  consumption_kva, 
                cur_meter_reading_kwh,  cur_meter_reading_kva,  cur_meter_reading_kvah,  cur_meter_reading_date,  cur_meter_reading_time, 
                cur_meter_reader_note,  prv_meter_reading_kwh,  prv_meter_reading_kva,  prv_meter_reading_kwah,  prv_meter_reading_date, 
                prv_meter_reading_time,  prv_meter_reader_note,  octroi_flag,  sop,  ed, 
                octroi,  dssf,  surcharge_leived,  service_rent,  meter_rent, 
                service_charge,  monthly_min_charges,  pf_surcharge,  pf_incentive,  demand_charges, 
                fixedcharges,  voltage_surcharge,  peakload_exemption_charges,  sundry_charges,  miscellaneous_charges, 
                fuel_adjustment,  bill_number,  no_of_days_billed,  bill_cycle,  bill_date, 
                due_date,  bill_type,  payment_amount,  payment_mode,  check_no, 
                bank_name,  payment_id,  ifsc_code,  micrcode,  payment_date, 
                payment_remark,  tot_billamount,  sbm_number,  meter_reader_name,  inhouse_outsourced_sbm, 
                transfromercode,  mcb_rent,  lpsc,  tot_amt_due_date,  tot_sop_ed_oct, 
                subdivisionname,  consumerlegacynumber,  consumername,  housenumber,  address, 
                category,  subcategory,  billingcycle,  billinggroup,  phasecode, 
                sanctionedload_kw,  totaloldmeter_cons_unit_kwh,  totaloldmeter_cons_unit_kvah,  newmeterinitialreading_kwh,  newmeterinitialreading_kvah, 
                sundrychrs_sop,  sundrychrs_ed,  sundrychrs_octrai,  sundryallowances_sop,  sundryallowances_ed, 
                sundryallowances_octrai,  otherchrs,  roundadjustmentamnt,  adjustmentamt_sop,  adjustmentamt_ed, 
                adjustmentamt_octrai,  provisionaladjustmentamt_sop,  provisionaladjustmentamt_ed,  provadjustmentamt_octrai,  arrearsopcurrentyear, 
                arrearedcurrentyear,  arrearoctraicurrentyear,  arrearsoppreviousyear,  arrearedpreviousyear,  arrearoctraipreviousyear, 
                advancecons_deposit,  complaintcenterphonenumber,  metersecurityamt,  nearestcashcounter,  multiplicationfactor, 
                overallmf,  contractedload_kva,  miscexpensesdetails,  previouskwhcycle_1,  previouskwhcycle_2, 
                previouskwhcycle_3,  previouskwhcycle_4,  previouskwhcycle_5,  previouskwhcycle_6,  rounding_present, 
                estimation_type,  rectype, dtupload, synced,syncdt
                ) VALUES (
                '{0}', '{1}', '{2}', '{3}', '{4}', 
                '{5}', '{6}', to_date('{7}','dd/mm/yyyy'), '{8}', '{9}', 
                '{10}', '{11}', '{12}', '{13}', '{14}', 
                '{15}', '{16}', '{17}', to_date('{18}','dd-mm-yyyy'), '{19}', 
                '{20}', '{21}', '{22}', '{23}', to_date('{24}','dd/mm/yyyy'), 
                '{25}', '{26}', '{27}', '{28}', '{29}', 
                '{30}', '{31}', '{32}', '{33}', '{34}', 
                '{35}', '{36}', '{37}', '{38}', '{39}', 
                '{40}', '{41}', '{42}', '{43}', '{44}', 
                '{45}', '{46}', '{47}', '{48}', to_date('{49}','dd-mm-yyyy'),
                to_date('{50}','dd-mm-yyyy'), '{51}', '{52}', '{53}', '{54}', 
                '{55}', '{56}', '{57}', '{58}', '{59}',
                '{60}', '{61}', '{62}', '{63}', '{64}',
                '{65}', '{66}', '{67}', '{68}', '{69}', 
                '{70}', '{71}', '{72}', '{73}', '{74}', 
                '{75}', '{76}', '{77}', '{78}', '{79}',
                '{80}', '{81}', '{82}', '{83}', '{84}', 
                '{85}', '{86}', '{87}', '{88}', '{89}', 
                '{90}', '{91}', '{92}', '{93}', '{94}',
                '{95}', '{96}', '{97}', '{98}', '{99}', 
                '{100}', '{101}', '{102}', '{103}', '{104}', 
                '{105}', '{106}', '{107}', '{108}', '{109}',
                '{110}', '{111}', '{112}', '{113}', '{114}', 
                '{115}', '{116}', '{117}', '{118}', '{119}', 
                '{120}', '{121}', sysdate, 'NCODE',sysdate
                ) ",
                scols.Sub_Division_Code, scols.Mru, scols.Connected_Pole_Ini_Number, scols.Neighbor_Meter_No, scols.Street_Name, 
                scols.Installation, scols.Mr_Doc_No, scols.Scheduled_Mrdate, scols.Meter_Number, scols.Manufacturer_Sr_No, 
                scols.Manufacturer_Name, scols.Contract_Account_Number, scols.Consumption_Kwh, scols.Consumption_Kwah, scols.Consumption_Kva, 
                scols.Cur_Meter_Reading_Kwh, scols.Cur_Meter_Reading_Kva, scols.Cur_Meter_Reading_Kvah, scols.Cur_Meter_Reading_Date, scols.Cur_Meter_Reading_Time, 
                scols.Cur_Meter_Reader_Note, scols.Prv_Meter_Reading_Kwh, scols.Prv_Meter_Reading_Kva, scols.Prv_Meter_Reading_Kwah, scols.Prv_Meter_Reading_Date, 
                scols.Prv_Meter_Reading_Time, scols.Prv_Meter_Reader_Note, scols.Octroi_Flag, scols.Sop, scols.Ed, 
                scols.Octroi, scols.Dssf, scols.Surcharge_Leived, scols.Service_Rent, scols.Meter_Rent, 
                scols.Service_Charge, scols.Monthly_Min_Charges, scols.Pf_Surcharge, scols.Pf_Incentive, scols.Demand_Charges, 
                scols.Fixedcharges, scols.Voltage_Surcharge, scols.Peakload_Exemption_Charges, scols.Sundry_Charges, scols.Miscellaneous_Charges, 
                scols.Fuel_Adjustment, scols.Bill_Number, scols.No_Of_Days_Billed, scols.Bill_Cycle, scols.Bill_Date, 
                scols.Due_Date, scols.Bill_Type, scols.Payment_Amount, scols.Payment_Mode, scols.Check_No, 
                scols.Bank_Name, scols.Payment_Id, scols.Ifsc_Code, scols.Micrcode, scols.Payment_Date, 
                scols.Payment_Remark, scols.Tot_Billamount, scols.Sbm_Number, scols.Meter_Reader_Name, scols.Inhouse_Outsourced_Sbm, 
                scols.Transfromercode, scols.Mcb_Rent, scols.Lpsc, scols.Tot_Amt_Due_Date, scols.Tot_Sop_Ed_Oct, 
                scols.Subdivisionname, scols.Consumerlegacynumber, scols.Consumername, scols.Housenumber, scols.Address, 
                scols.Category, scols.Subcategory, scols.Billingcycle, scols.Billinggroup, scols.Phasecode, 
                scols.Sanctionedload_Kw, scols.Totaloldmeter_Cons_Unit_Kwh, scols.Totaloldmeter_Cons_Unit_Kvah, scols.Newmeterinitialreading_Kwh, scols.Newmeterinitialreading_Kvah, 
                scols.Sundrychrs_Sop, scols.Sundrychrs_Ed, scols.Sundrychrs_Octrai, scols.Sundryallowances_Sop, scols.Sundryallowances_Ed, 
                scols.Sundryallowances_Octrai, scols.Otherchrs, scols.Roundadjustmentamnt, scols.Adjustmentamt_Sop, scols.Adjustmentamt_Ed, 
                scols.Adjustmentamt_Octrai, scols.Provisionaladjustmentamt_Sop, scols.Provisionaladjustmentamt_Ed, scols.Provadjustmentamt_Octrai, scols.Arrearsopcurrentyear, 
                scols.Arrearedcurrentyear, scols.Arrearoctraicurrentyear, scols.Arrearsoppreviousyear, scols.Arrearedpreviousyear, scols.Arrearoctraipreviousyear, 
                scols.Advancecons_Deposit, scols.Complaintcenterphonenumber, scols.Metersecurityamt, scols.Nearestcashcounter, scols.Multiplicationfactor, 
                scols.Overallmf, scols.Contractedload_Kva, scols.Miscexpensesdetails, scols.Previouskwhcycle_1, scols.Previouskwhcycle_2, 
                scols.Previouskwhcycle_3, scols.Previouskwhcycle_4, scols.Previouskwhcycle_5, scols.Previouskwhcycle_6, scols.Rounding_Present, 
                scols.Estimation_Type, scols.Rectype 
                );

            sql_merge = string.Format("merge into onlinebill.mast_account m1 using " +
                    "(select '{0}' as acno from dual) d on (m1.account_no=d.acno) " +
                    "when matched then update set m1.table_name = '{1}' " +
                    "when not matched then insert (m1.account_no,m1.table_name) values(d.acno,'{1}')",
                    scols.Contract_Account_Number, "ONLINEBILL.SAP_SBM_GSC");

            sql_atomic = string.Format("BEGIN {0}; {1}; END; ", sql, sql_merge).Replace(Environment.NewLine, "");
            try
            {
                OraDBConnection.ExecQry(sql_atomic);
            }
            catch (Exception ex)
            {
                strRet = string.Format("{0}_{1}_{2}", scols.Mr_Doc_No, "F", ex.Message.Replace('_',' '));
                eventLog1.WriteEntry(strRet);
                return strRet;
            }
            strRet = string.Format("{0}_{1}", scols.Mr_Doc_No, "S");
            eventLog1.WriteEntry(strRet);
        }
        return strRet;
    }
}
