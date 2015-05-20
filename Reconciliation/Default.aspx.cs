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
        if (!IsPostBack)
        {
            txtdt_txn.Attributes["type"] = "date";
            FillVendors();
            Session["LOGIN_USER"] = "CSC";
            txtdt_txn.Text = "2015-02-02";
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

        tDate = ConvertDate(txtdt_txn.Text);
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

            if(cols.Length != cntCols)
            {
                lblmsg.Text = "Invalid Line No. " + (line + 1);
                return;
            }

            if ( DateTime.Parse(cols[numTxnDate]).ToString("dd/MM/yy") != tDate)
            {
                lblmsg.Text = "Invalid transaction date at Line: " + (line + 1);
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
                break;
            }
        }
        OraDBConnection.ConnectionClose(con);
        sql = "select nvl(LISTAGG(txnid, ',') WITHIN GROUP (ORDER BY txnid),'no_dup') from " +
              "(select txnid from " + tempTable + " group by txnid having count(*)>1)";

        try
        {
            dupNo = OraDBConnection.GetScalar(sql);
            if(dupNo != "no_dup")
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
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        switch (drpfile_ty.SelectedItem.Text)
        {
            case "PAYU":
                PayU("PSPCL",19, 13);
                break;
        }
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

        sql = " select  * from payment where to_char(txndate,'dd-MON-yy')='" + tDate +
            "' and vid='" + drpfile_ty.SelectedItem.Value + "' and  status_p='SUCCESS' order by txnid";
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

        sql_gw = string.Format("select * from {0} where to_char(txndate,'DD/MM/YY')='{1}' and recon_id is null", gw, tDate);
        sql_pay = "select string_agg(distinct vid) as vid,txnid,sum(amt) as amt," +
                    "string_agg(distinct status_p) as status_p from payment where (txnid) in " +
                    "(select txnid from (" + sql_gw + ")) group by txnid";
        sql = "select get_vname(b.vid) as GATEWAY,b.BANK_REF AS BANK_REF,b.PGI_REF as PGI_REF ,b.txnid as txnid ," +
            "to_char(B.SETTLE_DT,'dd-Mon-yy') as SETTLE_DT,b.amt as amt,p.amt as PAY_AMT," +
            "p.status_p as PAY_STATUS,get_vname(p.vid) as PAY_GATEWAY, b.vid as V_VID, p.vid as P_VID " +
            "from (" + sql_gw + ") b left outer join (" + sql_pay + ") p on b.txnid=p.txnid " +
            " where upper(nvl(status_p,'blank')) <> 'SUCCESS' or " +
            "p.amt <> b.amt or p.vid <> b.vid order by to_number(b.txnid)";

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
        GridViewRow gr;

        //getting grid row of the image button that was clicked
        gr = (System.Web.UI.WebControls.GridViewRow)((System.Web.UI.WebControls.ImageButton)sender).Parent.Parent;

        txnid = ((Label)gr.Cells[3].Controls[1]).Text.Trim();
        amt = ((Label)gr.Cells[5].Controls[1]).Text;
        pay_amt = ((Label)gr.Cells[6].Controls[1]).Text;
        v_gw = ((Label)gr.Cells[9].Controls[1]).Text;
        pay_gw = ((Label)gr.Cells[10].Controls[1]).Text;

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
        sql += string.Format("update onlinebill.payment set status_p='SUCCESS', receiptno=RECEIPT_NO.NEXTVAL, receiptdt=sysdate where txnid ='{0}'; ", txnid);
        sql += string.Format("insert into recon_audit values('{0}', '{1}', RECEIPT_NO.CURRVAL, sysdate, '{2}'); ",txnid, v_gw, Session["LOGIN_USER"]);
        sql += "END; ";
        try
        {
            OraDBConnection.ExecQry(sql);
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error while Reconciling Record. " + ex.Message;
            return;
        }
        lblmsg.Text = "Record Reconciled. TXNID: "+ txnid;
        btnShowUnmatched_Click(null, new EventArgs());
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
                " string_agg(distinct trunc(b.txndate)), string_agg(distinct trunc(b.settle_dt)) from ({2}) b " +
                " left outer join ({3}) p on b.txnid=p.txnid",sqlMatched,sqlUnmatched, sql_gw, sql_pay);

        try
        {
            using (DataSet ds = OraDBConnection.GetData(sql))
            {
                lbltotal.Text = ds.Tables[0].Rows[0][0].ToString();
                lblupdtd.Text = ds.Tables[0].Rows[0][1].ToString();
                lblpend.Text = ds.Tables[0].Rows[0][2].ToString();
                lbltxn.Text = ds.Tables[0].Rows[0][3].ToString();
                lblsettl.Text = ds.Tables[0].Rows[0][4].ToString();
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error: " + ex.Message;
            lbltotal.Text = string.Empty;
            lblupdtd.Text = string.Empty;
            lblpend.Text = string.Empty;
            lbltxn.Text = string.Empty;
            lblsettl.Text = string.Empty;
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
        sql = string.Format("select p.*, b.* from ({0}) b left outer join ({1}) p on b.txnid=p.txnid {2} order by to_number(b.txnid)",
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

        //convert transaction date format
        tDate = ConvertDate(txtdt_txn.Text,outformat: "dd-MMM-yy");
        if (string.IsNullOrEmpty(tDate))
        {
            lblmsg.Text = "Date Error";
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

        sqlSum   = string.Format("select sum(amt) from {0} where RECON_ID is null and TXNDATE = '{1}'", gw, tDate);
        sqlCount = string.Format("select count(*) from {0} where RECON_ID is null and TXNDATE = '{1}'", gw, tDate);

        recCount = int.Parse(OraDBConnection.GetScalar(sqlCount));

        if (recCount == 0)
        {
            lblmsg.Text = "Nothing pending for date: " + tDate;
            return;
        }

        //make atomic block to perform finalise actions
        sql = "DECLARE ";
        sql += "curRID number; ";
        sql += "cntPending number; ";
        sql += "BEGIN ";
        sql += string.Format("select count(*) into cntPending from {0} where RECON_ID is null and TXNDATE = '{1}'; ", gw, tDate);
        sql += "if cntPending = 0 then    RETURN;  end if; ";
        sql += "select rid_seq.nextval into curRID from dual; ";
        sql += string.Format("insert into onlinebill.recon_upto(rid, vid, tdate, tcount, tamount, rdate, ruser) values" +
            "(curRID, {0}, '{1}', ({2}), ({3}), sysdate, '{4}'); ", vid, tDate, sqlCount, sqlSum, Session["LOGIN_USER"]);
        sql += string.Format("update {0} set recon_id = RID_SEQ.CURRVAL where recon_id is null and txndate = '{1}'; ", gw, tDate);
        sql += string.Format("update onlinebill.payment set recon_id = RID_SEQ.CURRVAL where txnid in "+
            "(SELECT txnid FROM {0} WHERE recon_id = curRID); ", gw);
        //payu is currently set as B2B for testing
        if (isB2B)
        {
            sql += string.Format("update vendor_wallet set dueamt = nvl(dueamt,0) + " +
                "(select nvl(sum(amt),0) from payment where recon_id = curRID) " +
                " where vid = {0}; ", vid);
        }
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
}