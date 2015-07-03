#undef DEBUG 
#undef RUN_ONLY_ONCE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
#if DEBUG
using System.Diagnostics;
#endif

namespace Console_SAP_Payment_Post
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.ConsumerLoop();
        }

        private void SetData(DT_PostPayment_ReqPayment sbm, DataRow drow)
        {
            sbm.TRANSACTION_ID = drow["TXNID"].ToString();
            sbm.SOURCE_CODE = drow["SOURCE_CODE"].ToString();
            sbm.AGENCY_CODE = drow["AGENCY_CODE"].ToString();
            sbm.CONTRACT_ACCOUNT = drow["ACNO"].ToString();
            sbm.PAYMENT_AMOUNT = drow["AMT"].ToString();
            sbm.DOCUMENT_DATE = DateTime.Parse(drow["TXNDT"].ToString());
            sbm.DOCUMENT_DATESpecified = true;
            sbm.POSTING_DATE = DateTime.Parse(drow["TXNDT"].ToString());
            sbm.VALUE_DATE = DateTime.Parse(drow["TXNDT"].ToString());
            //passing DOCUMENT_NUMBER as empty, accenture will return 
            //PAYMENT_DOC_NUMB containing contract account number
            sbm.DOCUMENT_NUMBER = "";
            sbm.PAYMENT_MODE = drow["PAYMENT_MODE"].ToString();
            sbm.BRANCH = drow["BRANCH"].ToString();
            sbm.CASHDESK = drow["CASHDESK"].ToString();
            sbm.CHEQUE_NO = drow["CHEQUE_NO"].ToString();
            sbm.CHEQUE_DATESpecified = sbm.CHEQUE_NO != "";
            if (sbm.CHEQUE_NO != "")
            {
                sbm.CHEQUE_DATE = DateTime.Parse(drow["CHEQUE_DATE"].ToString());
            }
            sbm.MICRCODE = "";
        }

        private void ConsumerLoop()
        {
            SAP_Payment_Service srv = new SAP_Payment_Service();
            const int maxTimes = 10;
            DataSet ds;
            StringBuilder sbSql = new StringBuilder(1000);
            StringBuilder sbUpdate = new StringBuilder(500 * maxTimes);
            DT_PostPayment_ReqPayment[] payments;
            int numRows;
#if DEBUG
            Stopwatch sw = new Stopwatch();
            Stopwatch swall = new Stopwatch();
#endif
            DT_PostPayment_ResPAYMENT[] resrec;
#region SQLs
            string strUpdate = "UPDATE ONLINEBILL.PAYMENT SET SYNCED = '{0}', "+
                                "SYNCMSG = '{1}', SYNCDT = sysdate WHERE TXNID='{2}' AND ACNO='{3}'; ";

            string sqlCount = "SELECT count(*) FROM ONLINEBILL.PAYMENT WHERE SYNCDT IS NULL";

            sbSql.Append("SELECT TXNID, ONLINEBILL.GET_SOURCECODE(VID) AS SOURCE_CODE,AGENCY_CODE, ACNO, AMT, ");
            sbSql.Append("TO_CHAR(TXNDATE, 'dd-MON-yyyy') as TXNDT, ");
            sbSql.Append("PAYMENT_MODE, BRANCH, CASHDESK, CHEQUE_NO, TO_CHAR(CHEQUE_DATE,'dd-MON-yyyy') as CHEQUE_DATE ");
            sbSql.Append("FROM ONLINEBILL.PAYMENT WHERE ");
            //sbSql.Append("RECON_ID IS NOT NULL ");                //sync only reconciled
            sbSql.Append("SYNCDT IS NULL ");                        //sync only unsynced
            sbSql.Append("AND IF_SAP = 'Y' ");                      //sync only SAP payments
            sbSql.Append("AND ROWNUM <= ").Append(maxTimes);        //this much rows at a time
            #endregion

            srv.Credentials = new System.Net.NetworkCredential("pidemo", "pidemo");
#if DEBUG
            swall.Start();
#endif
            while (true)
            {
#if DEBUG
                sw.Reset();
                sw.Start();
#endif

                ds = OraDBConnection.GetData(sbSql.ToString());
                numRows = ds.Tables[0].Rows.Count;
#if DEBUG
                Console.WriteLine("Got {0} from DB", numRows);
#endif
                payments = new DT_PostPayment_ReqPayment[numRows];

                for (int i = 0; i < numRows; i++)
                {
                    payments[i] = new DT_PostPayment_ReqPayment();
                    SetData(payments[i], ds.Tables[0].Rows[i]);
                }

                ds.Clear();
                ds.Dispose();

                resrec = srv.MI_PostPayment_OB_SYNC(payments);
                
                sbUpdate.Clear();
                sbUpdate.Append("BEGIN ");
                for (int i = 0; i < resrec.Length; i++)
                {
                    //PAYMENT_DOC_NUMB will contain contract account number
                    sbUpdate.AppendFormat(strUpdate, resrec[i].ErrorCode==""?"S":"F",
                        resrec[i].ErrorCode + "~" + resrec[i].ErrorDescr, 
                        resrec[i].TRANSACTION_ID, resrec[i].PAYMENT_DOC_NUMB);
                }
                sbUpdate.Append("END; ");

                if (numRows > 0)
                {
                    try
                    {
                        OraDBConnection.ExecQry(sbUpdate.ToString());
                    }
                    catch { }
                }
#if DEBUG
                sw.Stop();
                TimeSpan ts = sw.Elapsed;

                //Format and display the TimeSpan value. 
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("{0} Records in {1} ", resrec.Length, elapsedTime);
#endif
                if ("0" == OraDBConnection.GetScalar(sqlCount))
                {
#if RUN_ONLY_ONCE
                    break;
#else
                    //sleep for 1/2 hour
                    Console.WriteLine("Sleeping");
                    System.Threading.Thread.Sleep(1800 * 1000);
#endif
                }
            }
#if DEBUG
            swall.Stop();
            TimeSpan tsall = swall.Elapsed;

            // Format and display the TimeSpan value. 
            string elapsedTime2 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                tsall.Hours, tsall.Minutes, tsall.Seconds,
                tsall.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime2);
#endif
        }
    }
}
