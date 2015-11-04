using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Net;
namespace doecc_scheduler
{
    class Program
    {
        #region constants
        private const string ORACLE_DT_FORMAT = "yyyymmdd";
        private const string DOTNET_DT_FORMAT = "yyyyMMdd";
        private const string DOTNET_HUMAN_DT_FORMAT = "dd-MMM-yyyy";
        private const string ORACLE_HUMAN_DT_FORMAT = "dd-Mon-yyyy";
        private const string DOTNET_HUMAN_SHORT_DT_FORMAT = "dd/MM/yyyy";
        private const string LOG_FILE = "scheduler_log.txt";
        private const string FROM_ADDRESS = "seitpspcl@gmail.com";
        private const string EMAIL_AO_BANKING = "aobankingludhiana@gmail.com"; // "aobankingludhiana@gmail.com";
        private const string EMAIL_CBC_LDH = "computercellludhiana@gmail.com"; //"computercellludhiana@gmail.com";
        private const string EMAIL_GURPREET = "gurpreet007@gmail.com";
        private const string EMAIL_BHUPINDER = "bsbhogal@gmail.com";
        private const string DISP_NAME = "PSPCL";
        #endregion
        #region utility_functions
        private string Date_GetToday()
        {
            return DateTime.Today.ToString(DOTNET_DT_FORMAT, CultureInfo.InvariantCulture);
        }
        private string Date_GetCurMonth(int day)
        {
            DateTime dt;
            string ret;
            try
            {
                dt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, day);
                ret = dt.ToString(DOTNET_DT_FORMAT, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                ret = null;
            }
            return ret;
        }
        private string Date_GetPrevMonth(int day)
        {
            DateTime dt;
            string ret;
            try
            {
                dt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, day);
                dt = dt.AddMonths(-1);
                ret = dt.ToString(DOTNET_DT_FORMAT, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                ret = null;
            }
            return ret;
        }
        private string Date_GetLastDay_PrevMonth()
        {
            DateTime dt;
            string ret;
            try
            {
                dt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                dt = dt.AddDays(-1);
                ret = dt.ToString(DOTNET_DT_FORMAT, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                ret = null;
            }
            return ret;
        }
        private string Get_LCM_Minus_One()
        {
            string sql;

            sql = "select to_char(min(max(txndate)) - 1,'" + ORACLE_DT_FORMAT + 
                "') from payment where " +
                "recon_id is not null and if_sap = 'N' "+
                "and elid is null group by vid";

            return OraDBConnection.GetScalar(sql);
        }
        private string Get_LCM_Plus_One()
        {
            string sql;

            sql = "select to_char(min(max(txndate)) + 1,'" + ORACLE_DT_FORMAT +
                "') from payment where " +
                "recon_id is not null and if_sap = 'N' " +
                "and elid is null group by vid";

            return OraDBConnection.GetScalar(sql);
        }
        private string GetNoTxnCount(string dtLCMPO, string toDay)
        {
            string sql = string.Format("select count(*) from payment "+
                "where to_char(TXNDATE,'yyyymmdd') between '{0}' and '{1}' "+
                "and STATUS_P = 'SUCCESS' and if_sap='N'",dtLCMPO, toDay);
            return OraDBConnection.GetScalar(sql);
        }
        private void LogWrite(string line, params object[] args)
        {
            string str = string.Format(DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt",CultureInfo.InvariantCulture) + ": " + line + Environment.NewLine, args);
            File.AppendAllText(LOG_FILE, str);
            Console.Write(str);
        }
        private string ConvertDate(string strDate, string informat = "yyyy-MM-dd",
                           string outformat = "dd'/'MM'/'yy")
        {
            string convDate;
            try
            {
                convDate = DateTime.ParseExact(strDate, informat,
                    CultureInfo.InvariantCulture).ToString(outformat).ToUpper();
            }
            catch
            {
                return null;
            }
            return convDate;
        }
        private bool SendEmail(string fromAdd, string dispName, string toAdd, string subject, string body, Attachment attachedFile = null)
        {
            MailAddressCollection addColl = new MailAddressCollection();
            addColl.Add(toAdd);
            return SendEmail(fromAdd, dispName, addColl, subject, body, attachedFile);
        }
        private bool SendEmail(string fromAdd, string dispName, MailAddressCollection toAdds, string subject, string body, Attachment attachedFile = null)
        {
            MailMessage mailMsg = new MailMessage();
            mailMsg.From = new MailAddress(fromAdd, dispName);
            foreach (MailAddress toAdd in toAdds)
            {
                mailMsg.To.Add(toAdd);
            }
            mailMsg.Subject = subject;
            mailMsg.Body = body;
            if (attachedFile != null)
            {
                mailMsg.Attachments.Add(attachedFile);
            }

            //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(mailMsg.From.Address, "pspcl123");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(mailMsg);
            return true;
        }
        #endregion
        
        private void SendReconUptoEmail()
        {
            string sql = "select mpv.VNAME, rd from " +
                "(select  vid, to_char(max(txndate),'"+ORACLE_HUMAN_DT_FORMAT+"') rd from payment where "+
                "recon_id is not null and if_sap = 'N' and elid is null group by vid) n " +
                "inner join MASTER_PAYMENT_VENDOR mpv on mpv.VID = n.vid";
            string subject = "Recon Upto Dates";
            string content = "";
            DataSet ds = OraDBConnection.GetData(sql);

            if (ds.Tables[0].Rows.Count == 0)
            {
                //no record is pending reconciliation
                return;
            }
            foreach (DataRow drow in ds.Tables[0].Rows)
            {
                content += string.Format("{0} {1}{2}",(drow["vname"]+":").PadRight(10), drow["rd"], Environment.NewLine);
            }
            Console.WriteLine(content);

            //call SendEMail
            MailAddressCollection toAdds = new MailAddressCollection();
            toAdds.Add(EMAIL_GURPREET);
            toAdds.Add(EMAIL_BHUPINDER);
            toAdds.Add(EMAIL_CBC_LDH);
            toAdds.Add(EMAIL_AO_BANKING);
            SendEmail(FROM_ADDRESS, DISP_NAME, toAdds, subject, content);
        }
        private bool SendScheduledEmail(DataRowCollection drcArrEmails, DataRowCollection drcArrRecs, 
            string dtFrom, string dtTo, string circode, string cirname, string cat, string amount)
        {
            StringBuilder paymentData = new StringBuilder(10000);
            string fileName;
            MailAddressCollection toAdds = new MailAddressCollection();
            Attachment attachedFile;
            string subject;
            string body;
            bool retVal;

            //create filename
            fileName = circode + "-" + cat + "-" + dtFrom + "-" + dtTo + ".txt";

            //create attachment
            foreach (DataRow dr in drcArrRecs)
            {
                paymentData.Append(dr["payment_data"].ToString()).Append("\r\n");
            }
            attachedFile = new Attachment(new MemoryStream(Encoding.UTF8.GetBytes(paymentData.ToString())), fileName);

            //set to address
            foreach (DataRow drow in drcArrEmails)
                toAdds.Add(new MailAddress(drow["email"].ToString()));

            toAdds.Add(new MailAddress(EMAIL_GURPREET));
            toAdds.Add(new MailAddress(EMAIL_BHUPINDER));

            //set subject
            subject = string.Format("E-Payment Data from {0}-{1} Circle Code-{2} Category-{3}",
                ConvertDate(dtFrom, DOTNET_DT_FORMAT), ConvertDate(dtTo, DOTNET_DT_FORMAT), circode, cat);

            //set body
            body = string.Format("Please find attached the E-Payment Data: "+
                "\n\nFrom: {0}-{1} \n\nCircle Code: {2} \n\nCategory: {3} "+
                "\n\nNum. of Records: {4} {5} \n\nAmount: \u20B9 {6}. ",
                ConvertDate(dtFrom, DOTNET_DT_FORMAT), ConvertDate(dtTo, DOTNET_DT_FORMAT), 
                circode, cat, drcArrRecs.Count, drcArrRecs.Count == 1 ? "Record" : "Records", amount);

            //send email
            retVal = SendEmail(FROM_ADDRESS, DISP_NAME, toAdds, subject, body, attachedFile);
            LogWrite(retVal?"Mail Sent": "Unable to send email");
            return retVal;
        }
        private void Process_Schedule(int sid, string dtFrom, string dtTo, string circode, string cirname, string cat)
        {
            string recordCond;
            string sql;
            int recCount;
            DataSet dsArrRecs;
            DataSet dsArrEmails;
            string doecc_cat;
            bool emailStatus = false;
            string amount;

            LogWrite("Processing SID {0}", sid);

            StringBuilder sbRecordCond = new StringBuilder(500);
            sbRecordCond.AppendFormat("substr(acno,1,1) = '{0}' and ", circode);
            sbRecordCond.AppendFormat("tbl_name = '{0}' and ", cat);
            sbRecordCond.AppendFormat("recon_id is not null and ");
            sbRecordCond.AppendFormat("if_sap = 'N' and ");
            sbRecordCond.AppendFormat("elid is null and ");
            sbRecordCond.AppendFormat("to_char(txndate,'{0}') between '{1}' and '{2}' ", ORACLE_DT_FORMAT, dtFrom, dtTo);

            recordCond = sbRecordCond.ToString();
            recCount = int.Parse(OraDBConnection.GetScalar("select nvl(count(*),0) from payment where "+ recordCond));
            if (recCount == 0)
            {
                LogWrite("SID {0} has 0 records", sid);
                return;
            }
            LogWrite("SID {0} has {1} records", sid, recCount);

            sql = "SELECT SUBSTR(TRIM(ACNO),1,1) " +
                                        "|| 'EB51' " +
                                        "|| TRIM(ACNO) " +
                                        "|| LPAD(TRIM(AMT),7,'0') " +
                                        "|| '1' " +
                                        "|| TO_CHAR(TXNDATE,'DDMMYYYY') " +
                                        "|| LPAD(NVL(TRIM(RECEIPTNO),0),9,'0') " +
                                        "|| DECODE(TRIM(TBL_NAME), 'DSBELOW10KW','DS','DSABOVE10KW','GC',TRIM(TBL_NAME)) " +
                                        "PAYMENT_DATA FROM PAYMENT WHERE " + recordCond;

            dsArrRecs = OraDBConnection.GetData(sql);
            amount = OraDBConnection.GetScalar("select sum(amt) amount from payment where " + recordCond);
            dsArrEmails = OraDBConnection.GetData("select email from emails where sid = " + sid);

            doecc_cat = cat == "DSBELOW10KW" ? "DS" : cat == "DSABOVE10KW" ? "GC" : cat;
            emailStatus = SendScheduledEmail(dsArrEmails.Tables[0].Rows, dsArrRecs.Tables[0].Rows, dtFrom, dtTo, circode, cirname, doecc_cat, amount);
            if (emailStatus)
            {
                sql = "BEGIN ";
                sql += string.Format("insert into DOECC_EMAIL_LOG values(DOECC_EMLID.NEXTVAL, {0}, {1}, sysdate); ", sid, recCount);
                sql += string.Format("update payment set elid=DOECC_EMLID.CURRVAL where {0};", recordCond);
                sql += "END;";
                try
                {
                    OraDBConnection.ExecQry(sql);
                }
                catch (Exception ex)
                {
                    LogWrite("Error handling SID {0} - {1}", sid, ex.Message);
                }
            }
            else
            {
                LogWrite("Unable to send email for SID {0}", sid);
            }
        }
        private void Do_All_Schedules()
        {
            string dtToday;
            string dtLCMMO;
            string dtLCMPO;
            int sid;
            int fromDay;
            int toDay;
            int sendDay;
            int todayDay;
            string circode;
            string cirname;
            string cat;
            string dtFrom;
            string dtTo;
            string sql;
            string contentPending;
            string noTxnCnt;
            DataSet ds_schedules;

            dtToday = Date_GetToday();
            todayDay = DateTime.Today.Day;
            dtLCMMO = Get_LCM_Minus_One();
            dtLCMPO = Get_LCM_Plus_One();
            if (string.IsNullOrEmpty(dtLCMMO))
            {
                LogWrite("Payment Table is Empty!");
                return;
            }
            sql = "select sid, dt_from, dt_to, dt_send, cir_code, cir_name, cat from MASTER_SCHEDULE where active = 1";
            ds_schedules = OraDBConnection.GetData(sql);
            foreach (DataRow schd in ds_schedules.Tables[0].Rows)
            {
                sid = int.Parse(schd["sid"].ToString());
                fromDay = int.Parse(schd["dt_from"].ToString());
                toDay = int.Parse(schd["dt_to"].ToString());
                sendDay = int.Parse(schd["dt_send"].ToString());
                circode = schd["cir_code"].ToString();
                cirname = schd["cir_name"].ToString();
                cat = schd["cat"].ToString();
                if (sendDay > todayDay)
                {
                    LogWrite("SID {0}'s Send Day is in future. Send Day: {1}, Today: {2}", sid, sendDay, todayDay);
                    continue;
                }

                if (toDay == 99)
                {
                    dtFrom = Date_GetPrevMonth(fromDay);
                    dtTo = Date_GetLastDay_PrevMonth();
                }
                else if (fromDay >= toDay)
                {
                    dtFrom = Date_GetPrevMonth(fromDay);
                    dtTo = Date_GetCurMonth(toDay);
                }
                else
                {
                    dtFrom = Date_GetCurMonth(fromDay);
                    dtTo = Date_GetCurMonth(toDay);
                }

                noTxnCnt = GetNoTxnCount(dtLCMPO, dtTo);
                if ((int.Parse(dtTo) <= int.Parse(dtLCMMO)) || noTxnCnt == "0")
                {
                    Process_Schedule(sid, dtFrom, dtTo, circode, cirname, cat);
                }
                else
                {
                    contentPending = string.Format("SID {0}, Circle {1} ({2}), Category {3} skipped due to pending reconcilation. Due Date: {4}, Last Recon: {5}",
                        sid, circode, cirname, cat, ConvertDate(dtTo, "yyyyMMdd","dd-MM-yyyy"), ConvertDate(dtLCMMO, "yyyyMMdd", "dd-MM-yyyy"));
                    LogWrite(contentPending);
                    SendEmail(FROM_ADDRESS, DISP_NAME, EMAIL_AO_BANKING, "Warning: Pending Recon", contentPending);
                }
            }
        }
        private void Start_Scheduler()
        {
            LogWrite("===================Starting===================");
            SendReconUptoEmail();
            Do_All_Schedules();
        }
        static void Main(string[] args)
        {
            (new Program()).Start_Scheduler();
        }
    }
}
