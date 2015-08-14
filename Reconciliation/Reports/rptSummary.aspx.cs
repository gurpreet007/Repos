using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Linq;
using System.Globalization;
public partial class rptSummary : System.Web.UI.Page
{
    private static string IS_SAP;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LOGIN_USER"] == null)
        {
            Response.Redirect("./Login.aspx");
        }
        if (!IsPostBack)
        {
            lblrpt_name.Text = Request.QueryString["rpt"];

            if (lblrpt_name.Text == "SAP_SUMMARY_REPORT" || lblrpt_name.Text == "SAP_TRANSACTION_REPORT")
            {
                IS_SAP = "Y";
            }
            else if (lblrpt_name.Text == "NONSAP_SUMMARY_REPORT" || lblrpt_name.Text == "NONSAP_TRANSACTION_REPORT")
            {
                IS_SAP = "N";
            }
            fill_drp();
        }
    }
    private void fill_drp()
    {
        string sql=string.Empty;
        DataSet ds = null;
        drpcatg.Items.Clear();
        drpcircle.Items.Clear();
        drpzone.Items.Clear();
        drpzone.Items.Add(new ListItem("Select Zone", "none"));
        drpcircle.Items.Add(new ListItem("Select Circle", "ALL"));

        if (IS_SAP == "N")
        {

            //sql = "select 'ALL' as CATG from dual union all select distinct upper(trim(tbl_name)) as CATG from onlinebill.payment where if_sap='N' order by CATG";
            sql = "select 'ALL' as CATG from dual union select distinct upper(trim(category)) as CATG from onlinebill.payment where if_sap='N' order by CATG";
            for (char letter = 'A'; letter <= 'Z'; letter++)
            {
                drpcircle.Items.Add(new ListItem(letter.ToString(), letter.ToString()));
            }
        }
        else if (IS_SAP == "Y")
        {
            drpzone.Visible = true;
            lblzone_cir.Text = "Zone/Circle";
            sql = "select 'ALL' as CATG from dual union select distinct upper(trim(substr(category,1,3))) as CATG from onlinebill.payment where if_sap='Y' order by CATG";
            for (int i = 0; i <= 9; i++)
            {
                drpzone.Items.Add(new ListItem(i.ToString(), i.ToString()));
                drpcircle.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
        if (sql != string.Empty)
        {
            ds = OraDBConnection.GetData(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                drpcatg.DataSource = ds;
                drpcatg.DataTextField = "CATG";
                drpcatg.DataValueField = "CATG";
                drpcatg.DataBind();
            }
            ds.Clear();
            ds.Dispose();
        }
       
    }
    protected void lbhome_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Default.aspx");
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
    private string GET_QRY()
    {
        string sql;
        string sd_code;
        string dt_frm;
        string dt_to;
        string sd_col;
        int no_days;
        string categ_where = "";
        sql = "Error";
        DateTime dt1;
        DateTime dt2;
        dt1 = DateTime.ParseExact(txtdt_frm.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        dt2 = DateTime.ParseExact(txtdt_to.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        no_days = (int)(dt2 - dt1).TotalDays;
        if (no_days > 31)
           {
                lblmsg.Text = "Maximum 31 days are allowed";
                return sql;
            }
        if (drpcircle.SelectedItem.Value == "none" || drpdiv.SelectedItem.Value == "none" || drpSubDiv.SelectedItem.Value == "none")
            {
                lblmsg.Text = "Please Select proper Office";
                return sql;
            }
        if (IS_SAP == "Y" && drpzone.SelectedItem.Text == "none")
            {
                lblmsg.Text = "Please Select proper Zone";
                return sql;
            }
        dt_frm = ConvertDate(txtdt_frm.Text, outformat: "yyyyMMdd");
        dt_to = ConvertDate(txtdt_to.Text, outformat: "yyyyMMdd");

        if (string.IsNullOrEmpty(dt_frm) || string.IsNullOrEmpty(dt_to))
          {
                lblmsg.Text = "Date Error";
                return sql;
          }

        if (lblrpt_name.Text == "SAP_SUMMARY_REPORT" || lblrpt_name.Text == "SAP_TRANSACTION_REPORT")
            {
                if (drpcatg.SelectedValue != "ALL")
                    {
                        categ_where = " and upper(trim(substr(category,1,3))) = '" + drpcatg.SelectedValue + "'";
                    }
            }
        else if (lblrpt_name.Text == "NONSAP_SUMMARY_REPORT" || lblrpt_name.Text == "NONSAP_TRANSACTION_REPORT")
            {
                if (drpcatg.SelectedValue != "ALL")
                    {
                        categ_where = " and upper(trim(category)) = '" + drpcatg.SelectedValue + "'";    
                    //categ_where = " and tbl_name = '" + drpcatg.SelectedValue + "'";
                    }
            }

        sd_code = drpcircle.SelectedItem.Text + drpdiv.SelectedItem.Text + drpSubDiv.SelectedItem.Text;
        sd_code = IS_SAP == "Y" ? drpzone.SelectedItem.Text + sd_code : sd_code;
        sd_col = IS_SAP == "Y" ? " code_sdiv  " : " acno ";
        
        if (lblrpt_name.Text == "SAP_SUMMARY_REPORT" || lblrpt_name.Text == "NONSAP_SUMMARY_REPORT")
            {
              
            //uncomment if SOP,ED,OCTRAI is required 
            //sql = string.Format("select to_char(txndate,'dd-Mon-yy') as TXN_DATE,'{0}' as Sub_Div_Code," +
                //                     " count(distinct txnid) TXN_CNT,sum(sop) as SOP,sum(ED) as ED,sum(octrai) as OCT,sum(tot_amt) as GROSS_AMT," +
                //                     " sum(surcharge) as SURCHARGE, sum(amt_after_duedate) as AMT_AFTER_DUEDATE,sum(amt) as AMT_PAID " +
                //                     " from onlinebill.payment where recon_id is not null and if_sap='{1}' and {4} like '{0}%' and " +
                //                     " to_char(txndate,'yyyymmdd') between '{2}' and '{3}' {5} group by to_char(txndate,'dd-Mon-yy')"
                //                     , sd_code, IS_SAP, dt_frm, dt_to, sd_col, categ_where);
                //sql = string.Format("select TXN_DATE,get_cdsname(Sub_Div_Code,'{0}','S') as Sub_Div_Name,Sub_Div_Code," +
                //                    "TXN_CNT,SOP,ED,OCT,GROSS_AMT,SURCHARGE,AMT_AFTER_DUEDATE,AMT_PAID FROM ({1}) " +
                //                    " order by to_date(txn_date,'dd-Mon-yy')", IS_SAP, sql);
              
            sql = string.Format("select to_char(txndate,'dd-Mon-yy') as TXN_DATE,'{0}' as Sub_Div_Code," +
                                    " count(distinct txnid) TXN_CNT,sum(tot_amt) as GROSS_AMT," +
                                    " sum(surcharge) as SURCHARGE, sum(amt_after_duedate) as AMT_AFTER_DUEDATE,sum(amt) as AMT_PAID " +
                                    " from onlinebill.payment where recon_id is not null and if_sap='{1}' and {4} like '{0}%' and " +
                                    " to_char(txndate,'yyyymmdd') between '{2}' and '{3}' {5} group by to_char(txndate,'dd-Mon-yy')"
                                    , sd_code, IS_SAP, dt_frm, dt_to, sd_col, categ_where);
                sql = string.Format("select TXN_DATE,get_cdsname(Sub_Div_Code,'{0}','S') as Sub_Div_Name,Sub_Div_Code," +
                                    "TXN_CNT,GROSS_AMT,SURCHARGE,AMT_AFTER_DUEDATE,AMT_PAID FROM ({1}) " + 
                                    " order by to_date(txn_date,'dd-Mon-yy')", IS_SAP, sql);
                        }
        if (lblrpt_name.Text == "SAP_TRANSACTION_REPORT" || lblrpt_name.Text == "NONSAP_TRANSACTION_REPORT")
            {
                sql = string.Format("select to_char(txndate,'dd-Mon-yy') as TXN_DATE,GET_VNAME(vid) as GATEWAY,'{0}' as Sub_Div_Code," +
                                    "PG_REF_ID as REF_ID,txnid as TXNID,ACNO,CATEGORY,CNAME,BILISSDT as BILL_DATE,BILNO, " +
                                    " to_char(CHEQUE_DATE,'dd-mm-yy') as CHQ_DUE_DT,to_char(DUEDTCASH,'dd-mm-yy') as CASH_DUEDT," +
                                    "tot_amt as AMT_BEFORE_DUEDT,amt_after_duedate as AMT_AFTER_DUEDT,amt AMT_PAID " +
                                    " from onlinebill.payment where recon_id is not null and if_sap='{1}' and {4} like '{0}%' and " +
                                    " to_char(txndate,'yyyymmdd') between '{2}' and '{3}' {5} order by txndate,acno"
                                    , sd_code, IS_SAP, dt_frm, dt_to, sd_col, categ_where);
            }

        return sql;
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        lblmsg.Text = string.Empty;
        string qry;

        qry = GET_QRY();

        if (qry == "Error")
        {
            return;
        }
        GridView1.DataSource = OraDBConnection.GetData(qry);
        GridView1.DataBind();
    }
}