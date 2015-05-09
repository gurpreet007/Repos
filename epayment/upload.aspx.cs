using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;

public partial class upload : System.Web.UI.Page
{
    void UploadNonSAP(common.Categs categ)
    {
        string path = string.Empty;
        string[] lines;
        int oracle_ins = 0;
        int oracle_dup = 0;
        int oracle_err = 0;
        int start = 0;
        string userID = Session["userID"].ToString();
        //string empID = Session["empID"].ToString();
        string dtUpload = DateTime.Now.ToString(common.dtFmtDotNet);
        string strExtension = string.Empty;
        OracleConnection con;

        StringBuilder sbsql = new StringBuilder(2000);

        //capture sessionid
        hidSID.Value = System.Guid.NewGuid().ToString();

        if (FileUpload1.HasFile)
        {
            strExtension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();
            if (strExtension == ".txt" || strExtension == ".web")
            {
                path = Server.MapPath(string.Format("./bills/BILL_{0}.txt", hidSID.Value));
                FileUpload1.SaveAs(path);
            }
            else
            {
                lblMessage.Text = "Invalid File";
                return;
            }
        }

        //open file
        try
        {
            lines = System.IO.File.ReadAllLines(path);
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error Reading File: " + ex.Message;
            return;
        }

        //delete uploaded file
        System.IO.File.Delete(path);

        //if "AccountNo" word at line 0 then start from line 1 else start from line 0
        start = (lines[0].Split(categ.delimiter)[0] == "AccountNo") ? 1 : 0;

        con = OraDBConnection.ConnectionOpen();
        for (int line = start; line < lines.Length; line++)
        {
            //sanitize line
            lines[line] = lines[line].Replace("'", "").Replace("--", "");

            //get fields
            string[] fields = lines[line].Split(categ.delimiter);

            //check the field length (for first record in the file)
            if (fields.Length != categ.numFields && line == start)
            {
                lblMessage.Text = "Error. Invalid File.";
                return;
            }

            //set/reset sbsql
            sbsql.Clear();

            sbsql.Append(string.Format("INSERT INTO {0} VALUES (", categ.tableName));

            //make the insert query
            for (int field = 0; field < fields.Length; field++)
            {
                sbsql.Append("'")
                    .Append(fields[field])
                    .Append("', ");
            }

            //add userID, empID and date_upload at last, we already have comma and space at the end of string
            //sbsql.Append(string.Format("'{0}', '{1}', to_date('{2}','{3}'))", userID, empID, dtUpload, common.dtFmtOracle));
            sbsql.Append(string.Format("'{0}', '{1}', to_date('{2}','{3}'))", userID, '0', dtUpload, common.dtFmtOracle));

            //insert into oracle 
            //Composite Primary Key: ACCOUNTNO, BILLCYCLE, BILLYEAR
            //to handle dups we let the exception come and then continue
            //in case of error, we record the info about record containing error and continue
            
            //end insert query
            sbsql.Append("; ");

            //append merge query
            sbsql.Append(string.Format("merge into onlinebill.mast_account m1 using " +
                "(select '{0}' as acno from dual) d on (m1.account_no=d.acno) " +
                "when matched then update set m1.table_name = '{1}' " +
                "when not matched then insert (m1.account_no,m1.table_name) values(d.acno,'{1}') ",
                fields[categ.posAccountNo].ToUpper(), categ.tableName.ToUpper()));
            try
            {
                //make atomic transaction and execute
                OraDBConnection.ExecQryOnConnection(con, string.Format("BEGIN {0}; END;",sbsql.ToString()));
                oracle_ins++;
            }
            catch (Exception ex)
            {
                sbsql.Clear();
                if (ex.Message.Contains("ORA-00001:"))
                {
                    oracle_dup++;
                    sbsql.Append(string.Format("INSERT INTO ONLINEBILL.DUPBILL"+
                        "(LINENO, ACCOUNTNO, BILLCYCLE, BILLYEAR, SESSIONID, DATED, TYPE) "+
                        "VALUES({0},'{1}','{2}','{3}','{4}',to_date('{5}','{6}'),'{7}')",
                        line + 1, fields[categ.posAccountNo], fields[categ.posBillCycle], 
                        fields[categ.posBillYear], hidSID.Value, dtUpload, common.dtFmtOracle, common.strDupLetter));
                }
                else{
                    oracle_err++;
                    sbsql.Append(string.Format("INSERT INTO ONLINEBILL.DUPBILL"+
                        "(LINENO, ACCOUNTNO, BILLCYCLE, BILLYEAR, SESSIONID, DATED, TYPE) "+
                        "VALUES({0},'{1}','{2}','{3}','{4}',to_date('{5}','{6}'),'{7}')",
                        line + 1, common.strErrStyle, common.strErrStyle, common.strErrStyle, hidSID.Value, dtUpload, common.dtFmtOracle, common.strErrLetter));
                    //sbsql.Append(string.Format("INSERT INTO ONLINEBILL.DUPBILL(LINENO, SESSIONID, DATED, TYPE) "+
                    //        "VALUES({0},'{1}',to_date('{2}','{3}'),'{4}')", line + 1, hidSID.Value, dtUpload, common.dtFmtOracle,"E"));
                }
                OraDBConnection.ExecQryOnConnection(con, sbsql.ToString());
            }
        }

        OraDBConnection.ConnectionClose(con);

        //enable Export Button if dup rows or error rows
        btnExport.Visible = (oracle_dup > 0 || oracle_err > 0);

        //show success message
        lblMessage.Text = String.Format("Done. Total Rows {0}. Inserted {1}. Duplicate {2}. Error {3}", 
            lines.Length, oracle_ins, oracle_dup, oracle_err);

        //insert summary in userrec
        InsertUserRec(oracle_ins, oracle_dup, oracle_err, dtUpload, categ.categName, common.strNonSAP.ToUpper());
    }
    void UploadSAP(common.Categs categ)
    {
        string path = string.Empty;
        string cypherText;
        string plainText;
        string[] lines;
        int oracle_ins = 0;
        int oracle_dup = 0;
        int oracle_err = 0;
        int start = 0;
        string userID = Session["userID"].ToString();
        //string empID = Session["empID"].ToString();
        string dtUpload = DateTime.Now.ToString(common.dtFmtDotNet);
        string strExtension = string.Empty;
        StringBuilder sbsql = new StringBuilder(2000);
        OracleConnection con;
        int actualNumFields;
        string actualTableName;

        //int posSancLoad = 80;

        //capture sessionid
        hidSID.Value = System.Guid.NewGuid().ToString();

        if (FileUpload1.HasFile)
        {
            strExtension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();
            if (strExtension == ".enc")
            {
                path = Server.MapPath(string.Format("./bills/BILL_{0}.txt", hidSID.Value));
                FileUpload1.SaveAs(path);
            }
            else
            {
                lblMessage.Text = "Invalid File";
                return;
            }
        }

        //open file
        try
        {
            cypherText = System.IO.File.ReadAllText(path);
            plainText = common.Decrypt(cypherText);
            lines = plainText.Split(new string[]{Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error Reading File: " + ex.Message;
            return;
        }

        //delete uploaded file
        System.IO.File.Delete(path);

        //skip header line, header lines end with H in the last field
        start = (lines[0].Split(categ.delimiter)[6] == "H") ? 1 : 0;

        //open connection to oracle
        con = OraDBConnection.ConnectionOpen();
        for (int line = start; line < lines.Length; line++)
        {
            //sanitize line
            lines[line] = lines[line].Replace("'", "").Replace("--", "");

            //get fields
            string[] fields = lines[line].Split(categ.delimiter);

            if (fields.Length == 70)
            {
                actualNumFields = 70;
                actualTableName = "ONLINEBILL.SAP_SBM_GSC_70";
            }
            else
            {
                actualNumFields = categ.numFields;
                actualTableName = categ.tableName;
            }

            //check the field length (for first record in the file)
            if (line == start && fields.Length != actualNumFields )
            {
                lblMessage.Text = "Error. Invalid File.";
                return;
            }

            //check the sanctioned load for correct filetype, for the first row only
            //if (line == start && categ.categName == "SAP_DSBELOW10KW" && float.Parse(fields[posSancLoad]) > 10.0)
            //{
            //    lblMessage.Text = "Error. Invalid File.";
            //    return;
            //}
            //else if (line == start && categ.categName == "SAP_DSBELOW20KW" && float.Parse(fields[posSancLoad]) <= 10.0)
            //{
            //    lblMessage.Text = "Error. Invalid File.";
            //    return;
            //}

            //reset sbsql
            sbsql.Clear();

            sbsql.Append(string.Format("INSERT INTO {0} VALUES (", actualTableName));

            //make the insert query
            for (int field = 0; field < fields.Length; field++)
            {
                if (field == 7 || field == 24)
                {
                    sbsql.Append(string.Format("to_date('{0}','dd/mm/yyyy'),",fields[field]));
                }
                else if (field == 18 || field == 49)
                {
                    sbsql.Append(string.Format("to_date('{0}','dd-mm-yyyy'),", fields[field]));
                }
                else
                {
                    sbsql.Append("'");
                    sbsql.Append(fields[field]);
                    sbsql.Append("', ");
                }
            }

            //add userID, empID, date_upload and synched as NULL at last, we already have comma and space at the end of string
            //add semicolon at end of query to enable it to run in atomic BEGIN END block
            sbsql.Append(string.Format("'{0}', '{1}', to_date('{2}','{3}'),NULL); ", userID, '0', dtUpload, common.dtFmtOracle));

            //insert into oracle 
            //Composite Primary Key: ACCOUNTNO, BILLCYCLE, BILLYEAR
            //to handle dups we let the exception come and then continue
            //in case of error, we record the info about record containing error and continue

            //append merge query, with semicolon at end to make part of BEGIN END block
            sbsql.Append(string.Format("merge into onlinebill.mast_account m1 using " +
                "(select '{0}' as acno from dual) d on (m1.account_no=d.acno) " +
                "when matched then update set m1.table_name = '{1}' " +
                "when not matched then insert (m1.account_no,m1.table_name) values(d.acno,'{1}'); ",
                fields[categ.posAccountNo].ToUpper(), actualTableName.ToUpper()));
            try
            {
                //make atomic transaction and execute
                OraDBConnection.ExecQryOnConnection(con, string.Format("BEGIN {0} END;",sbsql.ToString()));
                oracle_ins++;
            }
            catch (Exception ex)
            {
                sbsql.Clear();
                if (ex.Message.Contains("ORA-00001:"))
                {
                    oracle_dup++;
                    sbsql.Append(string.Format("INSERT INTO ONLINEBILL.DUPBILL" +
                        "(LINENO, ACCOUNTNO, BILLCYCLE, BILLYEAR, SESSIONID, DATED, TYPE) " +
                        "VALUES({0},'{1}','{2}','{3}','{4}',to_date('{5}','{6}'),'{7}')",
                        line + 1, fields[categ.posAccountNo], fields[categ.posBillCycle],
                        fields[categ.posBillYear], hidSID.Value, dtUpload, common.dtFmtOracle, common.strDupLetter));
                }
                else
                {
                    oracle_err++;
                    sbsql.Append(string.Format("INSERT INTO ONLINEBILL.DUPBILL" +
                        "(LINENO, ACCOUNTNO, BILLCYCLE, BILLYEAR, SESSIONID, DATED, TYPE) " +
                        "VALUES({0},'{1}','{2}','{3}','{4}',to_date('{5}','{6}'),'{7}')",
                        line + 1, common.strErrStyle, common.strErrStyle, common.strErrStyle, 
                        hidSID.Value, dtUpload, common.dtFmtOracle, common.strErrLetter));
                }
                OraDBConnection.ExecQryOnConnection(con, sbsql.ToString());
            }
        }

        OraDBConnection.ConnectionClose(con);
        //enable Export Button if dup rows or error rows
        btnExport.Visible = (oracle_dup > 0 || oracle_err > 0);

        //show success message
        lblMessage.Text = String.Format("Done. Total Rows {0}. Inserted {1}. Duplicate {2}. Error {3}",
            lines.Length, oracle_ins, oracle_dup, oracle_err);

        //insert summary in userrec
        InsertUserRec(oracle_ins, oracle_dup, oracle_err, dtUpload, categ.categName, common.strSAP.ToUpper());
    }
    void InsertUserRec(int numInsRec, int numDupRec, int numErrRec, string dated, string categName, string billClass)
    {
        string userID = Session["userID"].ToString();
        //string empID = Session["empID"].ToString();
        string sql = string.Empty;

        //sql = string.Format("insert into onlinebill.userrec (userid, empid, insrec, duprec, errrec, dated, categ) " +
        //        "values('{0}', '{1}', '{2}', '{3}', '{4}',to_date('{5}', '{6}'), '{7}')",
        //        userID, empID, numInsRec, numDupRec, numErrRec, dated, common.dtFmtOracle, categName);
        sql = string.Format("insert into onlinebill.userrec (userid, empid, insrec, duprec, errrec, dated, categ, class) " +
                "values('{0}', '{1}', '{2}', '{3}', '{4}',to_date('{5}', '{6}'), '{7}', '{8}')",
                userID, '0', numInsRec, numDupRec, numErrRec, dated, common.dtFmtOracle, categName, billClass);
        try
        {
            OraDBConnection.ExecQry(sql);
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error in saving user record: " + ex.Message;
            return;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!common.isValidSession(Page.Session))
        {
            Response.Redirect("login.aspx");
        }

        if (!IsPostBack)
        {
            common.FillInfo(Page.Session, lblLoggedInAs);
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string billClass = string.Empty;
        string billType = string.Empty;
        
        if(hidBillType.Value == "0") {
            return;
        }
        
        billClass = hidBillType.Value.Split('-')[0];
        billType = hidBillType.Value.Split('-')[1];
        
        if(billClass==common.strSAP){
            switch (billType)
            {
                case "DSBELOW10KW":
                case "DSBELOW20KW":
                    UploadSAP(common.cat_SAP_GSC);
                    break;
                default:
                    lblMessage.Text = "Select a valid Bill Type "+ hidBillType.Value;
                    return;
            }
        }
        else if(billClass == common.strNonSAP)
        {
            switch (billType)
            {
                case "LS":
                    UploadNonSAP(common.cat_LS);
                    break;
                case "SP":
                    UploadNonSAP(common.cat_SP);
                    break;
                case "MS":
                    UploadNonSAP(common.cat_MS);
                    break;
                case "DSBELOW10KW":
                    UploadNonSAP(common.cat_DSBELOW10KW);
                    break;
                case "DSABOVE10KW":
                    UploadNonSAP(common.cat_DSABOVE10KW);
                    break;
                default:
                    lblMessage.Text = "Select a valid Bill Type "+ hidBillType.Value;
                    return;
            }
        }
        else
        {
            lblMessage.Text = "Some error occurred. Try after sometime.";
            return;
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string strFileName;
        string sql = string.Format("select LINENO, ACCOUNTNO, BILLCYCLE, BILLYEAR, DATED, "+
            "decode(TYPE,'E','ERROR','DUPLICATE') as PROBLEM from " +
            "ONLINEBILL.dupbill where SESSIONID = '{0}' ORDER BY LINENO", hidSID.Value);
        strFileName = "not_entered_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        common.DownloadXLS(sql, strFileName, this);
    }
}