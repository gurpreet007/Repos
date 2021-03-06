﻿//#define DEBUG
#undef DEBUG
//#define RUN_ONLY_ONCE
#undef RUN_ONLY_ONCE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
#if DEBUG
using System.Diagnostics;
#endif
namespace Console_SAP70Consume_Array
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.ConsumerLoop();
        }

        private void SetData(DT_SBMWebService_ReqBillingData_SBM sbm, DataRow drow)
        {
            sbm.SUB_DIVISION_CODE = drow["SUB_DIVISION_CODE"].ToString();
            sbm.MRU = drow["MRU"].ToString();
            sbm.Connected_Pole_INI_Number = drow["CONNECTED_POLE_INI_NUMBER"].ToString();
            sbm.NEIGHBOR_METER_NO = drow["NEIGHBOR_METER_NO"].ToString();
            sbm.STREET_NAME = drow["STREET_NAME"].ToString();
            sbm.INSTALLATION = drow["INSTALLATION"].ToString();
            sbm.MR_DOC_NO = drow["MR_DOC_NO"].ToString();
            sbm.SCHEDULED_MRDATE = drow["SCHEDULED_MRDATE"].ToString();
            sbm.METER_NUMBER = drow["METER_NUMBER"].ToString();
            sbm.MANUFACTURER_SR_NO = drow["MANUFACTURER_SR_NO"].ToString();
            sbm.MANUFACTURER_NAME = drow["MANUFACTURER_NAME"].ToString();
            sbm.CONTRACT_ACCOUNT_NUMBER = drow["CONTRACT_ACCOUNT_NUMBER"].ToString();
            sbm.CONSUMPTION_KWH = drow["CONSUMPTION_KWH"].ToString();
            sbm.CONSUMPTION_KWAH = drow["CONSUMPTION_KWAH"].ToString();
            sbm.CONSUMPTION_KVA = drow["CONSUMPTION_KVA"].ToString();
            sbm.CUR_METER_READING_KWH = drow["CUR_METER_READING_KWH"].ToString();
            sbm.CUR_METER_READING_KVA = drow["CUR_METER_READING_KVA"].ToString();
            sbm.CUR_METER_READING_KVAH = drow["CUR_METER_READING_KVAH"].ToString();
            sbm.CUR_METER_READING_DATE = drow["CUR_METER_READING_DATE"].ToString();
            sbm.CUR_METER_READING_TIME = drow["CUR_METER_READING_TIME"].ToString();
            sbm.CUR_METER_READER_NOTE = drow["CUR_METER_READER_NOTE"].ToString();
            sbm.PRV_METER_READING_KWH = drow["PRV_METER_READING_KWH"].ToString();
            sbm.PRV_METER_READING_KVA = drow["PRV_METER_READING_KVA"].ToString();
            sbm.PRV_METER_READING_KWAH = drow["PRV_METER_READING_KWAH"].ToString();
            sbm.PRV_METER_READING_DATE = drow["PRV_METER_READING_DATE"].ToString();
            sbm.PRV_METER_READING_TIME = drow["PRV_METER_READING_TIME"].ToString();
            sbm.PRV_METER_READER_NOTE = drow["PRV_METER_READER_NOTE"].ToString();
            sbm.OCTROI_FLAG = drow["OCTROI_FLAG"].ToString();
            sbm.SOP = drow["SOP"].ToString();
            sbm.ED = drow["ED"].ToString();
            sbm.OCTROI = drow["OCTROI"].ToString();
            sbm.DSSF = drow["DSSF"].ToString();
            sbm.SURCHARGE_LEIVED = drow["SURCHARGE_LEIVED"].ToString();
            sbm.SERVICE_RENT = drow["SERVICE_RENT"].ToString();
            sbm.METER_RENT = drow["METER_RENT"].ToString();
            sbm.SERVICE_CHARGE = drow["SERVICE_CHARGE"].ToString();
            sbm.MONTHLY_MIN_CHARGES = drow["MONTHLY_MIN_CHARGES"].ToString();
            sbm.PF_SURCHARGE = drow["PF_SURCHARGE"].ToString();
            sbm.PF_INCENTIVE = drow["PF_INCENTIVE"].ToString();
            sbm.DEMAND_CHARGES = drow["DEMAND_CHARGES"].ToString();
            sbm.FIXEDCHARGES = drow["FIXEDCHARGES"].ToString();
            sbm.VOLTAGE_SURCHARGE = drow["VOLTAGE_SURCHARGE"].ToString();
            sbm.PEAKLOAD_EXEMPTION_CHARGES = drow["PEAKLOAD_EXEMPTION_CHARGES"].ToString();
            sbm.SUNDRY_CHARGES = drow["SUNDRY_CHARGES"].ToString();
            sbm.MISCELLANEOUS_CHARGES = drow["MISCELLANEOUS_CHARGES"].ToString();
            sbm.FUEL_ADJUSTMENT = drow["FUEL_ADJUSTMENT"].ToString();
            sbm.BILL_NUMBER = drow["BILL_NUMBER"].ToString();
            sbm.NO_OF_DAYS_BILLED = drow["NO_OF_DAYS_BILLED"].ToString();
            sbm.BILL_CYCLE = drow["BILL_CYCLE"].ToString();
            sbm.BILL_DATE = drow["BILL_DATE"].ToString();
            sbm.DUE_DATE = drow["DUE_DATE"].ToString();
            sbm.BILL_TYPE = drow["BILL_TYPE"].ToString();
            sbm.PAYMENT_AMOUNT = drow["PAYMENT_AMOUNT"].ToString();
            sbm.PAYMENT_MODE = drow["PAYMENT_MODE"].ToString();
            sbm.CHECK_NO = drow["CHECK_NO"].ToString();
            sbm.BANK_NAME = drow["BANK_NAME"].ToString();
            sbm.PAYMENT_ID = drow["PAYMENT_ID"].ToString();
            sbm.IFSC_CODE = drow["IFSC_CODE"].ToString();
            sbm.MICRCODE = drow["MICRCODE"].ToString();
            sbm.PAYMENT_DATE = drow["PAYMENT_DATE"].ToString();
            sbm.PAYMENT_REMARK = drow["PAYMENT_REMARK"].ToString();
            sbm.TOT_BILLAMOUNT = drow["TOT_BILLAMOUNT"].ToString();
            sbm.SBM_NUMBER = drow["SBM_NUMBER"].ToString();
            sbm.METER_READER_NAME = drow["METER_READER_NAME"].ToString();
            sbm.INHOUSE_OUTSOURCED_SBM = drow["INHOUSE_OUTSOURCED_SBM"].ToString();
            sbm.TransfromerCode = drow["TRANSFROMERCODE"].ToString();
            sbm.MCB_RENT = drow["MCB_RENT"].ToString();
            sbm.LPSC = drow["LPSC"].ToString();
            sbm.TOT_AMT_DUE_DATE = drow["TOT_AMT_DUE_DATE"].ToString();
            sbm.TOT_SOP_ED_OCT = drow["TOT_SOP_ED_OCT"].ToString();
        }

        private void SAP_SBM_Upload()
        {
            WebService srv = new WebService();
            const int maxRecs = 1000;
            DataSet ds;
            StringBuilder sbSql = new StringBuilder(1000);
            StringBuilder sbUpdate = new StringBuilder(500 * maxRecs);
            DT_SBMWebService_ReqBillingData_SBM[] sbm;
            DT_SBMWebService_ResRecords[] resrec;
            int numRows;

            #region SQLs
            string strSyncCondition = " (SYNCDT IS NULL OR (synced='F' AND TO_CHAR(sysdate,'yyyymmdd') <= TO_CHAR(dtupload + 1,'yyyymmdd'))) ";
            string strUpdate = "UPDATE ONLINEBILL.SAP_SBM_GSC SET SYNCED = '{0}', SYNCMSG = '{1}', SYNCDT = sysdate WHERE MR_DOC_NO='{2}'; ";
            string sqlCount = "SELECT count(*) FROM ONLINEBILL.SAP_SBM_GSC WHERE " + strSyncCondition;

            sbSql.Append("SELECT ");
            sbSql.Append("SUB_DIVISION_CODE, MRU, CONNECTED_POLE_INI_NUMBER, NEIGHBOR_METER_NO, STREET_NAME, ");
            sbSql.Append("INSTALLATION, MR_DOC_NO, to_char(SCHEDULED_MRDATE,'dd-mm-yyyy') as SCHEDULED_MRDATE, METER_NUMBER, MANUFACTURER_SR_NO, ");
            sbSql.Append("MANUFACTURER_NAME, CONTRACT_ACCOUNT_NUMBER, CONSUMPTION_KWH, CONSUMPTION_KWAH, CONSUMPTION_KVA, ");
            sbSql.Append("CUR_METER_READING_KWH, CUR_METER_READING_KVA, CUR_METER_READING_KVAH, to_char(CUR_METER_READING_DATE,'dd-mm-yyyy') as CUR_METER_READING_DATE, CUR_METER_READING_TIME, ");
            sbSql.Append("CUR_METER_READER_NOTE, PRV_METER_READING_KWH, PRV_METER_READING_KVA, PRV_METER_READING_KWAH, to_char(PRV_METER_READING_DATE,'dd-mm-yyyy') as PRV_METER_READING_DATE, ");
            sbSql.Append("PRV_METER_READING_TIME, PRV_METER_READER_NOTE, OCTROI_FLAG, SOP, ED, ");
            sbSql.Append("OCTROI, DSSF, SURCHARGE_LEIVED, SERVICE_RENT, METER_RENT, ");
            sbSql.Append("SERVICE_CHARGE, MONTHLY_MIN_CHARGES, PF_SURCHARGE, PF_INCENTIVE, DEMAND_CHARGES, ");
            sbSql.Append("FIXEDCHARGES, VOLTAGE_SURCHARGE, PEAKLOAD_EXEMPTION_CHARGES, SUNDRY_CHARGES, MISCELLANEOUS_CHARGES, ");
            sbSql.Append("FUEL_ADJUSTMENT, BILL_NUMBER, NO_OF_DAYS_BILLED, BILL_CYCLE, to_char(BILL_DATE,'dd-mm-yyyy') as BILL_DATE, ");
            sbSql.Append("to_char(DUE_DATE,'dd-mm-yyyy') AS DUE_DATE, BILL_TYPE, PAYMENT_AMOUNT, PAYMENT_MODE, CHECK_NO, ");
            sbSql.Append("BANK_NAME, PAYMENT_ID, IFSC_CODE, MICRCODE, PAYMENT_DATE, ");
            sbSql.Append("PAYMENT_REMARK, TOT_BILLAMOUNT, SBM_NUMBER, METER_READER_NAME, INHOUSE_OUTSOURCED_SBM, ");
            sbSql.Append("TRANSFROMERCODE, MCB_RENT, LPSC, TOT_AMT_DUE_DATE, TOT_SOP_ED_OCT ");
            sbSql.Append("FROM ONLINEBILL.SAP_SBM_GSC WHERE " + strSyncCondition + " AND ROWNUM <= ").Append(maxRecs);
            #endregion
            srv.Credentials = new System.Net.NetworkCredential("routesms", "admin@123");

            while (true)
            {
                ds = OraDBConnection.GetData(sbSql.ToString());
                numRows = ds.Tables[0].Rows.Count;
                sbm = new DT_SBMWebService_ReqBillingData_SBM[numRows];

                for (int i = 0; i < numRows; i++)
                {
                    sbm[i] = new DT_SBMWebService_ReqBillingData_SBM();
                    SetData(sbm[i], ds.Tables[0].Rows[i]);
                }

                ds.Clear();
                ds.Dispose();

                try
                {
                    resrec = srv.SI_BillingData_SBM_WebService_OB(sbm);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SAP_SBM_Upload: Problem: " + ex.Message);
                    break;
                }

                sbUpdate.Clear();
                sbUpdate.Append("BEGIN ");
                for (int i = 0; i < resrec.Length; i++)
                {
                    sbUpdate.AppendFormat(strUpdate, resrec[i].Flag, resrec[i].Message, resrec[i].DocNo);
                }
                sbUpdate.Append("END; ");

                if (numRows > 0)
                {
                    try
                    {
                        OraDBConnection.ExecQry(sbUpdate.ToString());
                        Console.WriteLine("SAP_SBM_Upload: " + numRows + " of " + resrec.Length + " Records Transferred");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("SAP_SBM_Upload: Error:" + ex.Message);
                    }
                }

                if ("0" == OraDBConnection.GetScalar(sqlCount))
                {
                    Console.WriteLine("SAP_SBM_Upload: Complete");
                    break;
                }
            }
        }
        private void ConsumerLoop()
        {
            while (true)
            {
                //service 1
                SAP_SBM_Upload();

                //sleep for 1/2 hour
                Console.WriteLine("Sleeping " + DateTime.Now.ToString());
                System.Threading.Thread.Sleep(1800 * 1000);
            }
        }
    }
}
