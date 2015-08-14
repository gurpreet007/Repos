using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.OracleClient;
using System.Text.RegularExpressions;
using System.Globalization;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["LOGIN_USER"] = "RECON";
        if (!IsPostBack)
        {
            txtdt_txn.Attributes["type"] = "date";
            FillVendors();
            if (Session["LOGIN_USER"] == null)
            {
                //Session["LOGIN_USER"] = "RECON";
                Response.Redirect("./Login.aspx");
            }
            lblUser.Text = Session["LOGIN_USER"].ToString();
            txtdt_txn.Text = "2015-02-02";
        }
    }
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        switch (drpfile_ty.SelectedItem.Text)
        {
            case "PAYU":
                PayU("PSPCL", 19, 13);
                break;
            case "EASYBILL":
                EBL("EBL", 10, 4);
                break;
            case "BILLDESK":
                EBL("PSEBPG", 18, 5);
                break;
        }
    }
private void DeleteTempTab(string tempTabName)
    {
        try
        {
            OraDBConnection.ExecQry("delete from " + tempTabName);
        }
        catch
        {
            lblmsg.Text = "Error in DB Connection";
        }
    }
    private void PayU(string firstWord, int cntCols,int numTxnDate)
    {
        string sql = string.Empty;
        string startHTML = "<font style='font-size:20px; color: BLACK'>";
        string endHTML = "</font>";
        string strLine = string.Empty;
        string[] lines;
        string[] cols;
        string lblMsg = string.Empty;
        string fileLoc = string.Empty;
        string gw = drpfile_ty.SelectedItem.Text;
        int vid;
        string fileName = gw + ".CSV";
        string tempTable = gw + "_temp";
        string dupNo;
        DataRow drow;
        DataSet ds;
        OracleConnection con;
        string tDate;
        string tDateCheckForm;
        
        tDate = ConvertDate(txtdt_txn.Text, outformat: "yyyyMMdd");
        if (string.IsNullOrEmpty(tDate))
        {
            lblmsg.Text = "Date Error";
            return;
        }
        tDateCheckForm = ConvertDate(txtdt_txn.Text,outformat: "yyyyMMdd");

        vid = int.Parse(drpfile_ty.SelectedItem.Value);

        sql = string.Format("select to_char(max(tdate),'yyyymmdd') lrd, " +
            "to_char(max(tdate)+1,'yyyymmdd') lrd1," +
            "to_char(max(tdate),'dd-Mon-yyyy') lrdhf" +
            " from recon_upto where vid={0}", vid);
        ds = OraDBConnection.GetData(sql);
        drow = ds.Tables[0].Rows[0];

        if (drow["lrd"] != DBNull.Value)
        {
            if (tDateCheckForm != drow["lrd"].ToString() && tDateCheckForm != drow["lrd1"].ToString())
            {
                lblmsg.Text = "Reconciliation is done upto: " + drow["lrdhf"].ToString();
                return;
            }
        }

        if (!Fupld1.HasFile && 
            Path.GetExtension(Fupld1.PostedFile.FileName).ToUpper() != ".CSV")
        {
            lblmsg.Text = "Please select .csv file to import";
            return;
        }

        fileLoc = Server.MapPath(Path.Combine("~", "import_csv", fileName));
        Fupld1.SaveAs(fileLoc);

        lines = File.ReadAllLines(fileLoc);
        
        //delete file from server
        File.Delete(fileLoc);

        //file contains two header lines
        if (lines.Length < 3)
        {
            lblmsg.Text = "File is empty";
            return;
        }

        DeleteTempTab(tempTable);

        //open oracle connection
        con = OraDBConnection.ConnectionOpen();

        for (int line = 0; line < lines.Length; line++)
        {
            strLine = lines[line];
            
            //handle unamtched quotes
            strLine = Regex.Replace(strLine, @",(?=[^""]*""(?:[^""]*""[^""]*"")*[^""]*$)|'|""", "");

            //trim all the individual columns
            cols = strLine.Split(',').Select(s => s.Trim()).ToArray();

            if (cols[0] != firstWord)
            {
                continue;
            }

            if(cols.Length != cntCols)
            {
                lblmsg.Text = "Invalid Line No. " + (line + 1);
DeleteTempTab(tempTable);
                return;
            }

            if (DateTime.ParseExact(cols[numTxnDate].PadLeft(9,'0'), "dd-MMM-yy", CultureInfo.InvariantCulture).ToString("yyyyMMdd") != tDate)
            {
                lblmsg.Text = "Invalid transaction date at Line: " + (line + 1);
DeleteTempTab(tempTable);
                return;
            }

            sql = String.Format("insert into {19} values ('{20}','{0}','{1}','{2}','{3}','{4}'," +
                                    "'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}'," +
                                    "to_date('{13}','dd-mm-yy hh24:mi:ss')," +
                                    "to_date('{14}','dd-mm-yy hh24:mi:ss'),'{15}','{16}','{17}','{18}',sysdate, null) ",
                                    cols[0],cols[1],cols[2],cols[3],cols[4],cols[5],cols[6],cols[7],
                                    cols[8],cols[9],cols[10],cols[11],cols[12],cols[13], cols[14],
                                    cols[15].Replace("-", "0"), cols[16].Replace("-", "0"), cols[17].Replace("-", "0"),
                                    cols[18].Replace("-", "0"), tempTable, vid);

            try
            {
                OraDBConnection.ExecQryOnConnection(con, sql);
            }
            catch
            {
                lblmsg.Text = "Error at Line: " + (line + 1);
 DeleteTempTab(tempTable);
                break;
            }
        }
        OraDBConnection.ConnectionClose(con);
        sql = "select NVL(string_agg(txnid),'no_dup') from " +
              "(select txnid from " + tempTable + " group by txnid having count(*)>1)";

        try
        {
            dupNo = OraDBConnection.GetScalar(sql);
            if(dupNo != "no_dup")
            {
                lblmsg.Text = "Duplicate Transaction found at: " + dupNo;
DeleteTempTab(tempTable);
                return;
            }
        }
        catch
        {
            lblmsg.Text = "Error in DB Connection";
DeleteTempTab(tempTable);
            return;
        }

        sql = "select count(*), sum(gross_amt), sum(charges), sum(stax), sum(amt)," +
            "string_agg(distinct(txndate)), string_agg(distinct(settle_dt)) from " + tempTable;
        ds = OraDBConnection.GetData(sql);

        if (ds.Tables[0].Rows.Count == 0)
        {
            lblmsg.Text = "No record to check.";
 DeleteTempTab(tempTable);
            return;
        }
        
        drow = ds.Tables[0].Rows[0];
        lblmsg.Text = String.Format("{0}Total Records--{1}{3}{2}{0}" +
                                    "Gross Amount--{1}{4}{2}{0}Total Charges--{1}{5}{2}{0}" +
                                    "Total Service Tax--{1}{6}{2}{0}Net Amount--{1}{7}{2}{0}" +
                                    "Txn Dates--{1}{8}{2}{0}Settle Dates--{1}{9}{2}{0}",
                                    "<br>", startHTML, endHTML, drow[0], drow[1], 
                                    drow[2], drow[3], drow[4], drow[5], drow[6]);

        ds.Dispose();
    }
    private void EBL(string firstWord, int cntCols, int numTxnDate)
    {
        string sql = string.Empty;
        string startHTML = "<font style='font-size:20px; color: BLACK'>";
        string endHTML = "</font>";
        string strLine = string.Empty;
        string[] lines;
        string[] cols;
        string lblMsg = string.Empty;
        string fileLoc = string.Empty;
        string gw = drpfile_ty.SelectedItem.Text;
        int vid;
        string fileName = gw + ".CSV";
        string tempTable = gw + "_temp";
        string dupNo;
        DataRow drow;
        DataSet ds;
        OracleConnection con;
        string tDate;
        string tDateCheckForm;

        tDate = ConvertDate(txtdt_txn.Text, outformat: "yyyyMMdd");
        if (string.IsNullOrEmpty(tDate))
        {
            lblmsg.Text = "Date Error";
            return;
        }
        tDateCheckForm = ConvertDate(txtdt_txn.Text, outformat: "yyyyMMdd");

        vid = int.Parse(drpfile_ty.SelectedItem.Value);

        sql = string.Format("select to_char(max(tdate),'yyyymmdd') lrd, " +
            "to_char(max(tdate)+1,'yyyymmdd') lrd1," +
            "to_char(max(tdate),'dd-Mon-yyyy') lrdhf" +
            " from recon_upto where vid={0}", vid);
        ds = OraDBConnection.GetData(sql);
        drow = ds.Tables[0].Rows[0];

        if (drow["lrd"] != DBNull.Value)
        {
            if (tDateCheckForm != drow["lrd"].ToString() && tDateCheckForm != drow["lrd1"].ToString())
            {
                lblmsg.Text = "Reconciliation is done upto: " + drow["lrdhf"].ToString();
                return;
            }
        }

        if (!Fupld1.HasFile &&
            Path.GetExtension(Fupld1.PostedFile.FileName).ToUpper() != ".CSV")
        {
            lblmsg.Text = "Please select .csv file to import";
            return;
        }

        fileLoc = Server.MapPath(Path.Combine("~", "import_csv", fileName));
        Fupld1.SaveAs(fileLoc);

        lines = File.ReadAllLines(fileLoc);

        //delete file from server
        File.Delete(fileLoc);

        //file contains two header lines
        if (lines.Length < 2)
        {
            lblmsg.Text = "File is empty";
            return;
        }

      DeleteTempTab(tempTable);
       //open oracle connection
        con = OraDBConnection.ConnectionOpen();

        for (int line = 0; line < lines.Length; line++)
        {
            strLine = lines[line];

            //handle unamtched quotes
            strLine = Regex.Replace(strLine, @",(?=[^""]*""(?:[^""]*""[^""]*"")*[^""]*$)|'|""", "");

            //trim all the individual columns
            cols = strLine.Split(',').Select(s => s.Trim()).ToArray();

            if (cols[0] != firstWord)
            {
                continue;
            }

            if (cols.Length != cntCols)
            {
                lblmsg.Text = "Invalid Line No. " + (line + 1);
                DeleteTempTab(tempTable);
                return;
            }

            if (DateTime.ParseExact(cols[numTxnDate].PadLeft(10,'0'),"dd/MM/yyyy",CultureInfo.InvariantCulture).ToString("yyyyMMdd") != tDate)
            {
                lblmsg.Text = "Invalid transaction date at Line: " + (line + 1);
                DeleteTempTab(tempTable);
                return;
            }

             sql = String.Format("insert into {10} values ('{11}','{0}','{1}','{2}','{3}'," +
                                    "to_date('{4}','dd/mm/yy hh24:mi:ss')," +
                                    "to_date('{5}','dd/mm/yy hh24:mi:ss'),'{6}','{7}','{8}',to_date('{9}','dd/mm/yy hh24:mi:ss'),sysdate, null) ",
                                    cols[0], cols[1], cols[2], cols[3], cols[4], cols[5], cols[6], cols[7],
                                    cols[8], cols[9], tempTable, vid);

            try
            {
                OraDBConnection.ExecQryOnConnection(con, sql);
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Error at Line: " + (line + 1) + " " + ex.Message;
                DeleteTempTab(tempTable);
                return;
            }
        }
        OraDBConnection.ConnectionClose(con);
        sql = "select NVL(string_agg(txnid),'no_dup') from " +
              "(select txnid from " + tempTable + " group by txnid having count(*)>1)";

        try
        {
            dupNo = OraDBConnection.GetScalar(sql);
            if (dupNo != "no_dup")
            {
                lblmsg.Text = "Duplicate Transaction found at: " + dupNo;
                DeleteTempTab(tempTable);
                return;
            }
        }
        catch
        {
            lblmsg.Text = "Error in DB Connection";
            DeleteTempTab(tempTable);
            return;
        }

        sql = "select count(*), sum(amt),string_agg(distinct(txndate)) from " + tempTable;
        ds = OraDBConnection.GetData(sql);

        if (ds.Tables[0].Rows.Count == 0)
        {
            lblmsg.Text = "No record to check.";
            DeleteTempTab(tempTable);
            return;
        }

        drow = ds.Tables[0].Rows[0];
        lblmsg.Text = String.Format("{0}Total Records--{1}{3}{2}{0}" +
                                    "Amount--{1}{4}{2}{0}" +
                                    "Txn Dates--{1}{5}{2}",
                                    "<br>", startHTML, endHTML, drow[0], drow[1],drow[2]);

        ds.Dispose();
    }
    private void BILLDESK(string firstWord, int cntCols, int numTxnDate)
    {
        string sql = string.Empty;
        string startHTML = "<font style='font-size:20px; color: BLACK'>";
        string endHTML = "</font>";
        string strLine = string.Empty;
        string[] lines;
        string[] cols;
        string lblMsg = string.Empty;
        string fileLoc = string.Empty;
        string gw = drpfile_ty.SelectedItem.Text;
        int vid;
        string fileName = gw + ".CSV";
        string tempTable = gw + "_temp";
        string dupNo;
        DataRow drow;
        DataSet ds;
        OracleConnection con;
        string tDate;
        string tDateCheckForm;

        tDate = ConvertDate(txtdt_txn.Text, outformat: "yyyyMMdd");
        if (string.IsNullOrEmpty(tDate))
        {
            lblmsg.Text = "Date Error";
            return;
        }
        tDateCheckForm = ConvertDate(txtdt_txn.Text, outformat: "yyyyMMdd");

        vid = int.Parse(drpfile_ty.SelectedItem.Value);

        sql = string.Format("select to_char(max(tdate),'yyyymmdd') lrd, " +
            "to_char(max(tdate)+1,'yyyymmdd') lrd1," +
            "to_char(max(tdate),'dd-Mon-yyyy') lrdhf" +
            " from recon_upto where vid={0}", vid);
        ds = OraDBConnection.GetData(sql);
        drow = ds.Tables[0].Rows[0];

        if (drow["lrd"] != DBNull.Value)
        {
            if (tDateCheckForm != drow["lrd"].ToString() && tDateCheckForm != drow["lrd1"].ToString())
            {
                lblmsg.Text = "Reconciliation is done upto: " + drow["lrdhf"].ToString();
                return;
            }
        }

        if (!Fupld1.HasFile &&
            Path.GetExtension(Fupld1.PostedFile.FileName).ToUpper() != ".CSV")
        {
            lblmsg.Text = "Please select .csv file to import";
            return;
        }

        fileLoc = Server.MapPath(Path.Combine("~", "import_csv", fileName));
        Fupld1.SaveAs(fileLoc);

        lines = File.ReadAllLines(fileLoc);

        //delete file from server
        File.Delete(fileLoc);

        //file contains two header lines
        if (lines.Length < 3)
        {
            lblmsg.Text = "File is empty";
            return;
        }

        sql = string.Format("delete from {0}", tempTable);
        OraDBConnection.ExecQry(sql);

        //open oracle connection
        con = OraDBConnection.ConnectionOpen();

        for (int line = 0; line < lines.Length; line++)
        {
            strLine = lines[line];

            //handle unamtched quotes
            strLine = Regex.Replace(strLine, @",(?=[^""]*""(?:[^""]*""[^""]*"")*[^""]*$)|'|""", "");

            //trim all the individual columns
            cols = strLine.Split(',').Select(s => s.Trim()).ToArray();

            if (cols[0] != firstWord)
            {
                continue;
            }

            if (cols.Length != cntCols)
            {
                lblmsg.Text = "Invalid Line No. " + (line + 1);
                return;
            }

            if (DateTime.ParseExact(cols[numTxnDate], "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd") != tDate)
            {
                lblmsg.Text = "Invalid transaction date at Line: " + (line + 1);
                return;
            }

            sql = String.Format("insert into {19} values ('{20}','{0}','{1}','{2}','{3}','{4}'," +
                                    "'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}'," +
                                    "to_date('{5}','dd.mm.yyyy hh24:mi:ss')," +
                                    "to_date('{6}','dd.mm.yyyy'),'{15}','{16}','{17}','{18}',sysdate, null) ",
                                    cols[0], cols[1], cols[2], cols[3], cols[4], cols[5], cols[6], cols[7],
                                    cols[8], cols[9], cols[10], cols[11], cols[12], cols[13], cols[14],
                                    cols[15].Replace("-", "0"), cols[16].Replace("-", "0"), cols[17].Replace("-", "0"),
                                    cols[18].Replace("-", "0"), tempTable, vid);

            try
            {
                OraDBConnection.ExecQryOnConnection(con, sql);
            }
            catch
            {
                lblmsg.Text = "Error at Line: " + (line + 1);
                break;
            }
        }
        OraDBConnection.ConnectionClose(con);
        sql = "select NVL(string_agg(txnid),'no_dup') from " +
              "(select txnid from " + tempTable + " group by txnid having count(*)>1)";

        try
        {
            dupNo = OraDBConnection.GetScalar(sql);
            if (dupNo != "no_dup")
            {
                lblmsg.Text = "Duplicate Transaction found at: " + dupNo;
                return;
            }
        }
        catch
        {
            lblmsg.Text = "Error in DB Connection";
            return;
        }

        sql = "select count(*), sum(gross_amt), sum(charges), sum(stax), sum(amt)," +
            "string_agg(distinct(txndate)), string_agg(distinct(settle_dt)) from " + tempTable;
        ds = OraDBConnection.GetData(sql);

        if (ds.Tables[0].Rows.Count == 0)
        {
            lblmsg.Text = "No record to check.";
            return;
        }

        drow = ds.Tables[0].Rows[0];
        lblmsg.Text = String.Format("{0}Total Records--{1}{3}{2}{0}" +
                                    "Gross Amount--{1}{4}{2}{0}Total Charges--{1}{5}{2}{0}" +
                                    "Total Service Tax--{1}{6}{2}{0}Net Amount--{1}{7}{2}{0}" +
                                    "Txn Dates--{1}{8}{2}{0}Settle Dates--{1}{9}{2}{0}",
                                    "<br>", startHTML, endHTML, drow[0], drow[1],
                                    drow[2], drow[3], drow[4], drow[5], drow[6]);

        ds.Dispose();
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
    private void FillVendors()
    {
        string sql;
        DataSet ds;

        sql = "select vid, vname from master_payment_vendor order by vid";
        ds = OraDBConnection.GetData(sql);
        drpfile_ty.DataSource = ds.Tables[0];
        drpfile_ty.DataTextField = "vname";
        drpfile_ty.DataValueField = "vid";
        drpfile_ty.DataBind();
    }
    private int ReturnCurrentUnmatched(string gw, string dated)
    {
        string sql;
        string sql_gw;
        string sql_pay;
        string sqlUnmatched;
        int? unmatchedCount = null;

        sql_gw = string.Format("select * from {0} where to_char(txndate,'DD-MON-YY')='{1}' and recon_id is null ", gw, dated);
        sql_pay = "select string_agg(distinct vid) as vid,txnid,sum(amt) as amt, " +
                    "string_agg(distinct status_p) as status_p from payment where (txnid) in " +
                    "(select txnid from (" + sql_gw + ")) group by txnid ";
        sqlUnmatched = " where upper(nvl(status_p,'blank')) <> 'SUCCESS' or p.amt <> b.amt or p.vid <> b.vid ";
        sql = string.Format("select count(*) from ({0}) b left outer join ({1}) p on b.txnid=p.txnid {2}", 
            sql_gw, sql_pay, sqlUnmatched);

        try
        {
            unmatchedCount = int.Parse(OraDBConnection.GetScalar(sql));
        }
        catch(Exception ex)
        {
            throw ex;
        }
        return unmatchedCount.Value;
    }
    private bool IfPrevDayUnreconciled(string gw, string dated, int vid)
    {
        string sql;
        //getting prev date from transaction date
        //string prevDate = (new DateTime(int.Parse(dated.Substring(6, 4)), int.Parse(dated.Substring(3, 2)), int.Parse(dated.Substring(0, 2)))).AddDays(-1).ToString("yyyyMMdd");
        
        //check if any unreconciled record is left from any prev. date
        sql = string.Format("select count(*) from payment where "+
            "txnid in (select txnid from {0} where recon_id is null and to_char(txndate,'YYYYMMDD')<'{1}') "+
            "or (recon_id is null and status_p = 'SUCCESS' and vid={2} and to_char(txndate,'YYYYMMDD')<'{1}') "
            , gw, dated, vid);

        return OraDBConnection.GetScalar(sql) == "0";
    }
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        string table = drpfile_ty.SelectedItem.Text;
        string tempTable = table + "_temp";
        string recCount;
        string sql;

        sql = string.Format("select count(*) from {0} where txnid in (select txnid from {1})", table,tempTable);
        recCount = OraDBConnection.GetScalar(sql);
        if (int.Parse(recCount) > 0)
        {
            lblmsg.Text = "Last uploaded file contains duplicate records. Not accepting";
            return;
        }

        sql = "select count(*) from " + tempTable;
        recCount = OraDBConnection.GetScalar(sql);

        sql = "BEGIN ";
        sql += string.Format("insert into {0} select * from {1}; ", table, tempTable);
        sql += string.Format("delete from {0}; ",tempTable);
        sql += "END;";

        try
        {
            OraDBConnection.ExecQry(sql);
        }
        catch
        {
            lblmsg.Text = "Error in accepting file.";
            return;
        }
        lblmsg.Text = "Total Records Accepted: " + recCount;
    }
    protected void btnPaymentDetails_Click(object sender, EventArgs e)
    {
        string sql;
        string gw =  drpfile_ty.SelectedItem.Text;
        string tDate;

        lblmsg.Text = string.Empty;
        tDate = ConvertDate(txtdt_txn.Text, outformat: "dd-MMM-yy");
        if (string.IsNullOrEmpty(tDate))
        {
            lblmsg.Text = "Date Error";
            return;
        }

        sql = " select  rownum as SNo,to_char(txndate,'dd-mm-yy hh24:mi:ss') as Stamp,Cname,to_char(DueDTCash,'dd-mm-yy') as DueDTCash," +
              "to_char(Cheque_Date,'dd-mm-yy') as Cheque_Date,to_char(TxnID) as TxnID ,to_char(ACNO) as ACNO,to_char(bilno) as BillNO," +
              "to_char(BILISSDT,'dd-Mon-yy') as BillDate,AMT,Recon_Id from payment where to_char(txndate,'dd-MON-yy')='" + tDate +
              "' and vid='" + drpfile_ty.SelectedItem.Value + "' and  status_p='SUCCESS' ";

        sql += " union all select null,'','Total','','',to_char(count(*)),'','','',sum(amt),null from (" + sql + ")";
        Common.DownloadXLS(sql, "Payment.xls", this);
    }
    protected void btnShowUnmatched_Click(object sender, EventArgs e)
    {
        string sql;
        string sql_gw;
        string sql_pay;
        string tDate;
        string gw = drpfile_ty.SelectedItem.Text;

        tDate = ConvertDate(txtdt_txn.Text);
        if (string.IsNullOrEmpty(tDate))
        {
            lblmsg.Text = "Date Error";
            return;
        }

        sql_gw = string.Format("select * from onlinebill.{0} where to_char(txndate,'DD/MM/YY')='{1}' and recon_id is null", gw, tDate);
        sql_pay = "select onlinebill.string_agg(distinct vid) as vid,txnid,sum(amt) as amt," +
                    "onlinebill.string_agg(distinct status_p) as status_p from onlinebill.payment where (txnid) in " +
                    "(select txnid from (" + sql_gw + ")) group by txnid";
        sql = "select onlinebill.get_vname(b.vid) as GATEWAY,b.txnid as txnid ," +
              " b.amt as amt,p.amt as PAY_AMT," +
              "p.status_p as PAY_STATUS,onlinebill.get_vname(p.vid) as PAY_GATEWAY, b.vid as V_VID, p.vid as P_VID " +
            "from (" + sql_gw + ") b left outer join (" + sql_pay + ") p on b.txnid=p.txnid " +
            " where upper(nvl(status_p,'blank')) <> 'SUCCESS' or " +
            "p.amt <> b.amt or p.vid <> b.vid order by b.txnid";

        try
        {
            OraDBConnection.FillGrid(ref gvUnmatched, sql);
        }
        catch(Exception ex)
        {
            lblmsg.Text = "Error: " + ex.Message;
            return;
        }
        if (gvUnmatched.Rows.Count == 0)
        {
            lblmsg.Text = "All Matched";
        }
    }
    protected void imgUpdate_Click(object sender, ImageClickEventArgs e)
    {
        string sql;
        string txnid;
        string amt;
        string pay_amt;
        string v_gw;
        string pay_gw;
        string gw;
        GridViewRow gr;

        //getting grid row of the image button that was clicked
        gr = (System.Web.UI.WebControls.GridViewRow)((System.Web.UI.WebControls.ImageButton)sender).Parent.Parent;
        gw = ((Label)gr.Cells[0].Controls[1]).Text.Trim();
        txnid = ((Label)gr.Cells[1].Controls[1]).Text.Trim();
        amt = ((Label)gr.Cells[2].Controls[1]).Text;
        pay_amt = ((Label)gr.Cells[3].Controls[1]).Text;
        v_gw = ((Label)gr.Cells[6].Controls[1]).Text;
        pay_gw = ((Label)gr.Cells[7].Controls[1]).Text;

       if  (v_gw == "2" || v_gw == "3")
       {
        if (v_gw != pay_gw)
        {
            lblmsg.Text = "Gateway Mismatch";
            return;
        }
        if (amt != pay_amt)
        {
            lblmsg.Text = "Amount mismatch";
            return;
        }
        sql = "BEGIN ";
        sql += string.Format("update onlinebill.payment set status_p='SUCCESS', "+
            "receiptno=RECEIPT_NO.NEXTVAL, receiptdt=sysdate where txnid ='{0}' and receiptno is null; ", txnid);
        sql += "IF sql%rowcount = 1 THEN ";
        sql += string.Format("insert into recon_audit values('{0}', '{1}', RECEIPT_NO.CURRVAL, sysdate, '{2}'); ",txnid, v_gw, lblUser.Text);
        sql += "END IF; ";
        sql += "END; ";
        try
        {
            OraDBConnection.ExecQry(sql);
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error while updating Record. " + ex.Message;
            return;
        }
        lblmsg.Text = "Record updated for TXNID: "+ txnid;
        btnShowUnmatched_Click(null, new EventArgs());
    }
       else if (v_gw == "1")
       {
           DataSet ds_gw;
           DataSet ds_pay_temp;
           string sql_gw;
           string sql_pay_temp;
           string vid_gw;
           string txndt;
           string acno;
           string sql_insert;

           #region insert_qry
           sql_gw = string.Format("select VID,BRANCH,AGENCY_CODE,TXNID,upper(trim(ACNO)) as ACNO," +
                                  "to_char(TXNDATE,'dd-Mon-yyyy') as TXNDATE,POSTING_DATE,AMT,PAY_MODE,CHEQUE_NO,to_char(CHEQUE_DATE,'dd-Mon-yyyy') as CHEQUE_DATE" +
                                  " from {0} where trim(txnid)='{1}'", gw, txnid);
           ds_gw = OraDBConnection.GetData(sql_gw);
           
           vid_gw=ds_gw.Tables[0].Rows[0]["VID"].ToString();
           acno=ds_gw.Tables[0].Rows[0]["ACNO"].ToString();
           txndt=ds_gw.Tables[0].Rows[0]["TXNDATE"].ToString();

           sql_pay_temp = string.Format("select TXNID,to_char(TXNDATE,'dd-Mon-yyyy') as TXNDATE,ACNO,nvl(AMT,0) as AMT,BILCYC," +
                         "BILGRP,BILNO,to_char(BILISSDT,'dd-Mon-yyyy') as BILISSDT,to_char(DUEDTCASH,'dd-Mon-yyyy') as DUEDTCASH," +
                         "to_char(DUEDTCHQ,'dd-Mon-yyyy') as DUEDTCHQ,BILYR,CATEGORY,VID,STATUS_P,	MBNO," +
                         "EMAIL,PG_ERR_MSG,PG_ERR_CODE,CNAME,SUBDIVNAME,DIVNAME,CIRNAME,RECEIPTNO,AGENCY_CODE,PAYMENT_MODE," +
                         "BRANCH,CASHDESK,to_char(BILISSDT,'dd-Mon-yyyy') as BILISSDT,CHEQUE_NO,RECEIPTDT," +
                         "BD_AUTH_STATUS,PG_REF_ID,nvl(ED,0) as ED,nvl(OCTRAI,0) as OCTRAI,IF_SAP,nvl(SOP,0) as SOP," +
                         "WATER_SYPPLY,CODE_SDIV,TBL_NAME,nvl(SURCHARGE,0) as SURCHARGE,nvl(TOT_AMT,0) as TOT_AMT," +
                         "nvl(AMT_AFTER_DUEDATE,0) as AMT_AFTER_DUEDATE from onlinebill.payment_easybill " +
                         " where rowid=(select max(rowid) from onlinebill.payment_easybill" +
                         " where vid={0} and to_char(txndate,'dd-Mon-yyyy')='{1}' and upper(trim(acno))='{2}')",vid_gw,txndt,acno);
           
           ds_pay_temp = OraDBConnection.GetData(sql_pay_temp);
           
           if (ds_pay_temp.Tables[0].Rows.Count != 1)
            {
               lblmsg.Text = "No such transaction initiated by PSPCL";
               ds_pay_temp.Dispose();
               ds_gw.Dispose();
               return;
            }
           
           sql_insert = string.Format("insert into onlinebill.payment(TXNID,TXNDATE,ACNO,AMT,BILCYC,"+
                        "BILGRP,BILNO,BILISSDT,DUEDTCASH,DUEDTCHQ,BILYR,CATEGORY,VID,STATUS_P,MBNO,"+
                        "EMAIL,PG_ERR_MSG,PG_ERR_CODE,CNAME,SUBDIVNAME,DIVNAME,CIRNAME,RECEIPTNO,AGENCY_CODE,PAYMENT_MODE,"+
                        "BRANCH,CASHDESK,CHEQUE_DATE,CHEQUE_NO,RECEIPTDT,BD_AUTH_STATUS,PG_REF_ID,ED,OCTRAI,IF_SAP,	SOP,"+
                        "WATER_SYPPLY,CODE_SDIV,TBL_NAME,SURCHARGE,TOT_AMT,AMT_AFTER_DUEDATE) values("+
                        "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},'{13}','{14}'," +
                        "'{15}','{16}','{17}','{18}','{19}','{20}','{21}',{22},'{23}','{24}','{25}','{26}','{27}','{28}',{29}," +
                        "'{30}','{31}',{32},{33},'{34}',{35},'{36}','{37}','{38}',{39},{40},{41});",
                                                                                                
            ds_gw.Tables[0].Rows[0]["TXNID"].ToString(),ds_gw.Tables[0].Rows[0]["TXNDATE"].ToString(),ds_gw.Tables[0].Rows[0]["ACNO"].ToString(),
            ds_gw.Tables[0].Rows[0]["AMT"].ToString(),ds_pay_temp.Tables[0].Rows[0]["BILCYC"].ToString(),ds_pay_temp.Tables[0].Rows[0]["BILGRP"].ToString(),
            ds_pay_temp.Tables[0].Rows[0]["BILNO"].ToString(),ds_pay_temp.Tables[0].Rows[0]["BILISSDT"].ToString(),ds_pay_temp.Tables[0].Rows[0]["DUEDTCASH"].ToString(),
            ds_pay_temp.Tables[0].Rows[0]["DUEDTCHQ"].ToString(),ds_pay_temp.Tables[0].Rows[0]["BILYR"].ToString(),ds_pay_temp.Tables[0].Rows[0]["CATEGORY"].ToString(),
            ds_gw.Tables[0].Rows[0]["VID"].ToString(),"SUCCESS",ds_pay_temp.Tables[0].Rows[0]["MBNO"].ToString(),
            ds_pay_temp.Tables[0].Rows[0]["EMAIL"].ToString(),ds_pay_temp.Tables[0].Rows[0]["PG_ERR_MSG"].ToString(),ds_pay_temp.Tables[0].Rows[0]["PG_ERR_CODE"].ToString(),
            ds_pay_temp.Tables[0].Rows[0]["CNAME"].ToString(),ds_pay_temp.Tables[0].Rows[0]["SUBDIVNAME"].ToString(),ds_pay_temp.Tables[0].Rows[0]["DIVNAME"].ToString(),
            ds_pay_temp.Tables[0].Rows[0]["CIRNAME"].ToString(),"onlinebill.RECEIPT_NO.NEXTVAL",ds_gw.Tables[0].Rows[0]["AGENCY_CODE"].ToString(),
            ds_gw.Tables[0].Rows[0]["PAY_MODE"].ToString(),ds_gw.Tables[0].Rows[0]["BRANCH"].ToString(),ds_pay_temp.Tables[0].Rows[0]["CASHDESK"].ToString(),
            ds_gw.Tables[0].Rows[0]["CHEQUE_DATE"].ToString(),ds_gw.Tables[0].Rows[0]["CHEQUE_NO"].ToString(),"sysdate",
            ds_pay_temp.Tables[0].Rows[0]["BD_AUTH_STATUS"].ToString(),ds_pay_temp.Tables[0].Rows[0]["PG_REF_ID"].ToString(),ds_pay_temp.Tables[0].Rows[0]["ED"].ToString(),
            ds_pay_temp.Tables[0].Rows[0]["OCTRAI"].ToString(),ds_pay_temp.Tables[0].Rows[0]["IF_SAP"].ToString(),ds_pay_temp.Tables[0].Rows[0]["SOP"].ToString(),
            ds_pay_temp.Tables[0].Rows[0]["WATER_SYPPLY"].ToString(),ds_pay_temp.Tables[0].Rows[0]["CODE_SDIV"].ToString(),ds_pay_temp.Tables[0].Rows[0]["TBL_NAME"].ToString(),
            ds_pay_temp.Tables[0].Rows[0]["SURCHARGE"].ToString(),ds_pay_temp.Tables[0].Rows[0]["TOT_AMT"].ToString(),ds_pay_temp.Tables[0].Rows[0]["AMT_AFTER_DUEDATE"].ToString());
          
           #endregion
           sql = "BEGIN ";
           sql += sql_insert;
           sql += "IF sql%rowcount = 1 THEN ";
           sql += string.Format("insert into recon_audit values('{0}', '{1}', RECEIPT_NO.CURRVAL, sysdate, '{2}'); ", txnid, v_gw, lblUser.Text);
           sql += "END IF; ";
           sql += "END; ";
           try
           {
               OraDBConnection.ExecQry(sql);
           }
           catch (Exception ex)
           {
               ds_pay_temp.Dispose();
               ds_gw.Dispose();
               lblmsg.Text = "Error while inserting Record. " + ex.Message;
               return;
           }
           lblmsg.Text = "Record inserted for TXNID: " + txnid;
           btnShowUnmatched_Click(null, new EventArgs());
           ds_pay_temp.Dispose();
           ds_gw.Dispose();
       } 
    }
    protected void btnShowSummary_Click(object sender, EventArgs e)
    {
        string sql;
        string sql_gw;
        string sql_pay;
        string gw = drpfile_ty.SelectedItem.Text;
        string tDate;
        bool showMatched = rblist1.SelectedItem.Text == "Matched";
        string sqlMatched;
        string sqlUnmatched;

        lblmsg.Text = string.Empty;
        tDate = ConvertDate(txtdt_txn.Text);
        if (string.IsNullOrEmpty(tDate))
        {
            lblmsg.Text = "Date Error";
            return;
        }

        sql_gw = string.Format("select * from {0} where to_char(txndate,'DD/MM/YY')='{1}' and recon_id is null", gw, tDate);
        sql_pay = " select txnid,string_agg(distinct vid) as vid,string_agg(distinct status_p) as status_p,sum(amt) as amt from payment " +
                    "where txnid in (select txnid from (" + sql_gw + ")) group by txnid";
        sqlMatched = "upper(nvl(status_p,'blank')) = 'SUCCESS' and p.amt = b.amt and p.vid = b.vid";
        sqlUnmatched = "upper(nvl(status_p,'blank')) <> 'SUCCESS' or p.amt <> b.amt or p.vid <> b.vid";
        sql = string.Format("select count(*), count(case when {0} then 1 end), count(case when {1} then 1 end)," +
                " string_agg(distinct trunc(b.txndate)), sum(b.amt) from ({2}) b " +
                         " left outer join ({3}) p on b.txnid=p.txnid ",sqlMatched,sqlUnmatched, sql_gw, sql_pay);
        sql  += string.Format("union all select count(distinct txnid), nvl(sum(amt),0),0,'',0 as amt from payment "+
            "where to_char(txndate,'DD/MM/YY')='{0}' and recon_id is null and vid = {1} AND status_p = 'SUCCESS'", tDate, drpfile_ty.SelectedValue);
      
        try
        {
            using (DataSet ds = OraDBConnection.GetData(sql))
            {
                lbltotal.Text = ds.Tables[0].Rows[0][0].ToString();
                lblupdtd.Text = ds.Tables[0].Rows[0][1].ToString();
                lblpend.Text = ds.Tables[0].Rows[0][2].ToString();
                lbltxn.Text = ds.Tables[0].Rows[0][3].ToString();
                //lblsettl.Text = ds.Tables[0].Rows[0][4].ToString();
                lblamt.Text = ds.Tables[0].Rows[0][4].ToString() + " / " + ds.Tables[0].Rows[1][1].ToString();
                lbltcount.Text = ds.Tables[0].Rows[0][0].ToString() + " / " + ds.Tables[0].Rows[1][0].ToString();
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error: " + ex.Message;
            lbltotal.Text = string.Empty;
            lblupdtd.Text = string.Empty;
            lblpend.Text = string.Empty;
            lbltxn.Text = string.Empty;
            lblamt.Text = string.Empty;
            //lblsettl.Text = string.Empty;
            return;
        }
    }
    protected void btnDownloadXLS_Click(object sender, EventArgs e)
    {
        string sql_gw;
        string sql_pay;
        string gw = drpfile_ty.SelectedItem.Text;
        string tDate;
        bool showTotal = rblist1.SelectedItem.Text == "Total";
        bool showMatched = rblist1.SelectedItem.Text == "Matched";
        string sqlMatched;
        string sqlUnmatched;
        string sql;
        string recSelection = rblist1.SelectedItem.Text;
        string fileName;

        lblmsg.Text = string.Empty;
        tDate = ConvertDate(txtdt_txn.Text, outformat: "yyyyMMdd");
        if (string.IsNullOrEmpty(tDate))
        {
            lblmsg.Text = "Date Error";
            return;
        }

        fileName = string.Format("{0}_{1}_{2}.xls",gw, tDate, recSelection);
        sql_gw = string.Format("select * from {0} where to_char(txndate,'YYYYMMDD')='{1}' and recon_id is null", gw, tDate);
        sql_pay = "select string_agg(distinct vid) as vid,txnid,sum(amt) as amt," +
                    "string_agg(distinct status_p) as status from payment where (txnid) in " +
                    "(select txnid from (" + sql_gw + ")) group by txnid";
        sqlMatched = " where upper(nvl(status,'blank')) = 'SUCCESS' and p.amt = b.amt and p.vid = b.vid ";
        sqlUnmatched = " where upper(nvl(status,'blank')) <> 'SUCCESS' or p.amt <> b.amt or p.vid <> b.vid ";
        sql = string.Format("select p.*, b.* from ({0}) b left outer join ({1}) p on b.txnid=p.txnid {2} order by b.txnid",
            sql_gw, sql_pay, (showTotal?"":(showMatched ? sqlMatched : sqlUnmatched)));

        Common.DownloadXLS(sql, fileName, this);
    }
    protected void gvUnmatched_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUnmatched.PageIndex = e.NewPageIndex;
        btnShowUnmatched_Click(null, new EventArgs());
    }
    protected void btnFinalise_Click(object sender, EventArgs e)
    {
        string gw = drpfile_ty.SelectedItem.Text;
        int vid = int.Parse(drpfile_ty.SelectedItem.Value);
        string tDate;
        int cntUnmatched;
        bool isB2B = false;
        string sql;
        string sqlWallet = string.Empty;
        string sqlSum;
        string sqlCount;
        int recCount;
        string arcTableName;
        string arcDate;
        bool isPrevReconed = false;
        //convert transaction date format
        tDate = ConvertDate(txtdt_txn.Text,outformat: "dd-MMM-yy");
        if (string.IsNullOrEmpty(tDate))
        {
            lblmsg.Text = "Date Error";
            return;
        }

        // compare successfull transactions in payment table with gateway transactions
        try
        {
            isPrevReconed = IfPrevDayUnreconciled(gw, ConvertDate(txtdt_txn.Text, outformat: "yyyyMMdd"), vid);
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error getting previous reconciliation. " + ex.Message;
            return;
        }

        //there must be no unmatched records pending
        if (!isPrevReconed)
        {
            //lblmsg.Text = "Cannot Finalise as PSPCL records contain more successful transactions than gateway records for the selected date";
            lblmsg.Text = "Error. Previous Day Reconciliation is Pending";
            return;
        }

        //get unmatched records
        try
        {
            cntUnmatched = ReturnCurrentUnmatched(gw, tDate);
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error finding unmatched records. " + ex.Message;
            return;
        }

        //there must be no unmatched records pending
        if (cntUnmatched != 0)
        {
            lblmsg.Text = "Cannot Finalise as Unmatched Records Exist";
            return;
        }

        

        //check if B2B
        sql = "select vtype from master_payment_vendor where vid = " + vid;
        isB2B = OraDBConnection.GetScalar(sql) == "B2B";

        sqlSum   = string.Format("select sum(amt) from {0} where RECON_ID is null and to_char(TXNDATE,'dd-MON-yy') = '{1}'", gw, tDate);
        sqlCount = string.Format("select count(*) from {0} where RECON_ID is null and to_char(TXNDATE,'dd-MON-yy') = '{1}'", gw, tDate);

        recCount = int.Parse(OraDBConnection.GetScalar(sqlCount));

        if (recCount == 0)
        {
            lblmsg.Text = "Nothing pending for date: " + tDate;
            return;
        }

        //create archieve tablename and archieve-till-date
        arcTableName = string.Format("ARCBILL.ARC_{0}_{1}", 
            ConvertDate(tDate, informat: "dd-MMM-yy", outformat: "yyyy"), gw);

        arcDate = DateTime.Parse(tDate).AddDays(-1).ToString("yyyyMMdd");

        //check if archieve table exists, create if not
        sql = "DECLARE ";
        sql += "cnt number; ";
        sql += "BEGIN ";
        sql += string.Format("select count(*) into cnt from dba_tables "+
            "where owner = '{0}' and table_name = '{1}'; ", 
            arcTableName.Split('.')[0],
            arcTableName.Split('.')[1]);
        sql += "if cnt = 1 then return; end if; ";
        sql += string.Format("execute immediate 'create table {0} as select * from onlinebill.{1} where 1=2'; ",arcTableName, gw);
        sql += "COMMIT; END; ";

        try
        {
            OraDBConnection.ExecQry(sql);
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error handling archieve." + ex.Message;
            return;
        }

        //make atomic block to perform finalise actions
        sql = "DECLARE ";
        sql += "curRID number; ";
        sql += "cntPending number; ";
        sql += "sumAmt number(20,4); ";
        sql += "BEGIN ";
        sql += string.Format("select count(*), sum(amt) into cntPending, sumAmt from {0} where RECON_ID is null and to_char(TXNDATE,'dd-MON-yy') = '{1}'; ", gw, tDate);
        sql += "if cntPending = 0 then    RETURN;  end if; ";
        sql += "select rid_seq.nextval into curRID from dual; ";
        sql += string.Format("insert into onlinebill.recon_upto(rid, vid, tdate, tcount, tamount, rdate, ruser) values" +
            "(RID_SEQ.CURRVAL, {0}, '{1}', ({2}), ({3}), sysdate, '{4}'); ", vid, tDate, sqlCount, sqlSum, lblUser.Text);
        sql += string.Format("update {0} set recon_id = RID_SEQ.CURRVAL where recon_id is null and to_char(TXNDATE,'dd-MON-yy') = '{1}'; ", gw, tDate);
        sql += string.Format("update onlinebill.payment set recon_id = RID_SEQ.CURRVAL where txnid in "+
            "(SELECT txnid FROM {0} WHERE recon_id = curRID); ", gw);
        //payu is currently set as B2B for testing
        if (isB2B)
        {
            sql += string.Format("update vendor_wallet set dueamt = nvl(dueamt, 0) + nvl(sumAmt,0) where vid = {0}; ", vid);
        }
        //move recs to archieve table
        sql += string.Format("insert into {0} select * from {1} "+
            "where to_char(txndate,'yyyymmdd') <= '{2}' and vid={3} and recon_id is not null; ", arcTableName, gw, arcDate, vid);
        //delete moved recs from orig table
        sql += string.Format("delete from {0} where to_char(txndate,'yyyymmdd') <= '{1}' and vid={2}  and recon_id is not null; ", gw, arcDate, vid);
        sql += " COMMIT; END; ";

        //run finalise block
        try
        {
            OraDBConnection.ExecQry(sql);
        }
        catch(Exception ex)
        {
            lblmsg.Text = "Error Finalising. " + ex.Message;
            return;
        }
        lblmsg.Text = "Finalised upto " + tDate;
    }
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        MenuItem men = Menu1.SelectedItem;
        string rpt_type = men.Value;
        switch (rpt_type)
        {
            case "Logout":
                Session.Clear();
                Response.Redirect("./Login.aspx");
                break;
        }
    }
  protected void btnDelete_Click(object sender, EventArgs e)
    {
        string gw = drpfile_ty.SelectedItem.Text;
        string tDate = ConvertDate(txtdt_txn.Text,outformat: "yyyyMMdd");
        string sql = string.Format("delete from {0} where recon_id is null and to_char(txndate,'yyyymmdd') = '{1}'", gw, tDate);
        OraDBConnection.ExecQry(sql);
        lblmsg.Text = "Unreconciled Records for Selected Date are Deleted";
    }
    protected void lnkException_Click(object sender, EventArgs e)
    {
        string sql;
        string gw = drpfile_ty.SelectedItem.Text;
        int vid = int.Parse(drpfile_ty.SelectedItem.Value);
        string tDate = ConvertDate(txtdt_txn.Text, outformat: "yyyyMMdd");
        sql = string.Format("select 'FOUND' as , a.* from payment a where " +
            "txnid in (select txnid from {0} where recon_id is null and to_char(txndate,'YYYYMMDD')<'{1}') "
            , gw, tDate);
        sql += " UNION ALL ";
        sql += string.Format("select 'MISSING IN GW', b.* from payment b where " +
            "recon_id is null and status_p = 'SUCCESS' and vid={2} and to_char(txndate,'YYYYMMDD')<'{1}' " +
            "and txnid not in (select txnid from {0} where recon_id is null and to_char(txndate,'YYYYMMDD')<'{1}') "
            , gw, tDate, vid);
        Common.DownloadXLS(sql, "prev_day_unreconciled.xls", this);
    
    }
    }