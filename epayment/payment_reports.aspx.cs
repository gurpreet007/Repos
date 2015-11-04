using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
public partial class reports : System.Web.UI.Page
{
    private bool getDates(out string startDate, out string endDate)
    {
        string duration = panActivity_drpDuration.SelectedValue;
        string dtFormat = string.Empty;
        startDate = DateTime.Now.ToString("dd-MMM-yyyy");
        endDate = DateTime.Now.ToString("dd-MMM-yyyy");
        try
        {
            switch (duration)
            {
                case "dates":
                    startDate = sDate.Value;
                    endDate = eDate.Value;
                    dtFormat = startDate.Contains("-") ? "yyyy-MM-dd" : "dd/MM/yyyy";
                    startDate = DateTime.ParseExact(startDate, dtFormat, null).ToString("dd-MMM-yyyy");
                    endDate = DateTime.ParseExact(endDate, dtFormat, null).ToString("dd-MMM-yyyy");
                    break;
                case "day1":
                    startDate = endDate;
                    break;
                case "day2":
                    startDate = DateTime.ParseExact(endDate, "dd-MMM-yyyy", null).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;
                case "day3":
                    startDate = DateTime.ParseExact(endDate, "dd-MMM-yyyy", null).AddDays(-2).ToString("dd-MMM-yyyy");
                    break;
                case "week":
                    startDate = DateTime.ParseExact(endDate, "dd-MMM-yyyy", null).AddDays(-7).ToString("dd-MMM-yyyy");
                    break;
                case "month":
                    startDate = DateTime.ParseExact(endDate, "dd-MMM-yyyy", null).AddMonths(-1).ToString("dd-MMM-yyyy");
                    break;
                case "year":
                    startDate = DateTime.ParseExact(endDate, "dd-MMM-yyyy", null).AddYears(-1).ToString("dd-MMM-yyyy");
                    break;
            }
        }
        catch (System.FormatException)
        {
            lblMsg.Text = "Invalid Date. Use format dd/mm/yyyy.";
            return false;
        }
        return true;
    }
    private void fillVendors()
    {
        DataSet ds;
        string sql = "select vid as ven_val, vname as ven_text from ONLINEBILL.MASTER_PAYMENT_VENDOR order by vname";

        ds = OraDBConnection.GetData(sql);
        drpVendor.DataSource = ds;
        drpVendor.DataValueField = "ven_val";
        drpVendor.DataTextField = "ven_text";
        drpVendor.DataBind();
        ds.Dispose();

        drpVendor.Items.Insert(0, new ListItem("All", "ALL"));
    }
    private void fillCategories(string type = "sapd")
    {
        DataSet ds;
        string sql;
        //string sql = "select "+
        //"distinct decode(if_sap,'Y','SAP_','N','NonSAP_') || upper(trim(category)) as cat_text,"+
        //"upper(trim(category)) as cat_val,"+
        //"if_sap from payment order by if_sap desc, cat_text";

        if (type == "sapd" || type == "saps")
        {
            sql = "select distinct 'SAP_' || trim(substr(category,1,3)) as cat_text, " +
                    "trim(substr(category,1,3)) as cat_val from payment where if_sap = 'Y'";
            txtLoc.Attributes.Add("Placeholder", "e.g 12, 1234");
        }
        else
        {
            sql = "select distinct 'Non_SAP_' || trim(tbl_name) as cat_text, " +
            "trim(tbl_name) as cat_val from payment where if_sap='N' ";
            txtLoc.Attributes.Add("Placeholder", "e.g U, U31");
        }
        if (type == "saps" || type == "nonsaps")
        {
            toHide1.Visible = false;
            toHide2.Visible = false;
            toHide3.Visible = false;
        }
        else
        {
            toHide1.Visible = true;
            toHide2.Visible = true;
            toHide3.Visible = true;
        }
        ds = OraDBConnection.GetData(sql);
        drpCategory.DataSource = ds;
        drpCategory.DataValueField = "cat_val";
        drpCategory.DataTextField = "cat_text";
        drpCategory.DataBind();
        ds.Dispose();
    }
    private void showDetailedReport()
    {
        string sql;
        string sDate = string.Empty;
        string eDate = string.Empty;
        bool isSAP;
        string loc;

        lblMsg.Text = "";
        if (!getDates(out sDate, out eDate))
        {
            //date error, msg already shown, return
            return;
        }

        isSAP = drpType.SelectedValue.StartsWith("sap");
        loc = txtLoc.Value.Trim().PadRight(isSAP ? 4 : 3, '_').ToUpper();

        if ((isSAP && !Regex.IsMatch(loc, "^[1-9][1-9_]{3}$")) ||
            (!isSAP && !Regex.IsMatch(loc, "^[A-Z][1-9_]{2}$")))
        {
            lblMsg.Text = "Invalid Location.";
            return;
        }

        if ((isSAP && !(drpCategory.SelectedItem.Text.Split('_')[0] == "SAP")) ||
            (!isSAP && !(drpCategory.SelectedItem.Text.Split('_')[0] == "Non")))
        {
            lblMsg.Text = "Invalid Category.";
            return;
        }
        sql = string.Format("select * from payment where " +
                "{0} = trim('{1}')" +
                "and txndate between '{2}' and '{3}' " +
                "and upper({4}) like '{5}' " +
                "and {6} and {7} " +
                "and recon_id is not null " +
                "order by txnid",
                isSAP ? "substr(category,1,3)" : "tbl_name", 
                drpCategory.SelectedValue,
                sDate, eDate,
                isSAP ? "code_sdiv" : "substr(acno,1,3)", 
                loc,
                drpPayMode.SelectedValue == "ALL" ? "1=1" : "payment_mode = '" + drpPayMode.SelectedValue + "'",
                drpVendor.SelectedValue == "ALL" ? "1=1" : "vid = '" + drpVendor.SelectedValue + "'");
       
        if (!common.DownloadXLS(sql, "payment.xls", this))
        {
            lblMsg.Text = "No Such Record";
        }
    }
    private void showSummaryReport()
    {
        string sql;
        string sDate = string.Empty;
        string eDate = string.Empty;
        bool isSAP;
        string loc;

        lblMsg.Text = "";
        if (!getDates(out sDate, out eDate))
        {
            //date error, msg already shown, return
            return;
        }

        isSAP = drpType.SelectedValue.StartsWith("sap");
        loc = txtLoc.Value.Trim().PadRight(isSAP ? 4 : 3, '_').ToUpper();

        if ((Session[common.strUserID].ToString().ToUpper() != common.strAdminName) && 
            ((isSAP && !Regex.IsMatch(loc, "^[1-9][1-9_]{3}$")) ||
            (!isSAP && !Regex.IsMatch(loc, "^[A-Z][1-9_]{2}$")))
            )
        {
            lblMsg.Text = "Invalid Location.";
            return;
        }

        if ((isSAP && !(drpCategory.SelectedItem.Text.Split('_')[0] == "SAP")) ||
            (!isSAP && !(drpCategory.SelectedItem.Text.Split('_')[0] == "Non")))
        {
            lblMsg.Text = "Invalid Category.";
            return;
        }
        //0 = ifsap Y/N
        //1 = from date
        //2 = to date
        //3 = sap - code_sdiv, non_sap - substr(acno,1,3)
        //4 = location
        sql = string.Format("select GET_CDSNAME(a.subdivcode,'{0}','S') as SubDivisonName, a.* from ( " +
                "SELECT upper({3}) as subdivcode, " +
                "to_char(txndate,'dd/mm/yy') as dte, " +
                "count(*) num_trans, " +
                "sum(sop) as ssop, sum(ed) as sed, " +
                "sum(octrai) soct, sum(tot_amt) as gross_amount, " +
                "sum(surcharge) as ssur, sum(amt_after_duedate) as net_amount, " +
                "sum(amt) as amt_paid " +
                "FROM payment WHERE txndate BETWEEN '{1}' AND '{2}' " +
                "AND upper({3}) LIKE '{4}' " +
                "AND if_sap = '{5}' "+
                "group by to_char(txndate,'dd/mm/yy'),upper({3})) a " +
                "order by a.dte",
                (isSAP ? "Y" : "N"), sDate, eDate,
                isSAP ? "code_sdiv" : "substr(acno,1,3)", 
                loc,
                isSAP?"Y":"N");

        if (!common.DownloadXLS(sql, "summary.xls", this))
        {
            lblMsg.Text = "No Such Record";
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
            //fillUsers(panActivity_drpUsers, false, true);
            sDate.Attributes["type"] = "date";
            eDate.Attributes["type"] = "date";
            fillCategories();
            fillVendors();
        }
    }
    protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillCategories(drpType.SelectedValue);
    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        if (drpType.SelectedValue == "sapd" || drpType.SelectedValue == "nonsapd")
        {
            showDetailedReport();
        }
        else
        {
            showSummaryReport();
        }

    }
}