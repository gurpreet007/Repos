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
    private void fillCategories()
    {
        DataSet ds;
        //string sql = "select "+
        //"distinct decode(if_sap,'Y','SAP_','N','NonSAP_') || upper(trim(category)) as cat_text,"+
        //"upper(trim(category)) as cat_val,"+
        //"if_sap from payment order by if_sap desc, cat_text";

        string sql = "select distinct 'Non_SAP_' || tbl_name as cat_text, " +
            "tbl_name as cat_val from payment where if_sap='N' " +
            "union all " +
            "select distinct 'SAP_' || substr(category,1,3) as cat_text, " +
            "substr(category,1,3) as cat_val from payment where if_sap = 'Y'";
        ds = OraDBConnection.GetData(sql);
        drpCategory.DataSource = ds;
        drpCategory.DataValueField = "cat_val";
        drpCategory.DataTextField = "cat_text";
        drpCategory.DataBind();
        ds.Dispose();
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
    protected void btnShowReport_Click(object sender, EventArgs e)
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

        isSAP = drpType.SelectedValue == "sap";
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
                "{0} " +
                "and txndate between '{1}' and '{2}' " +
                "and upper({3}) like '{4}' " +
                "and {5} and {6} " +
                "and recon_id is not null " +
                "order by txnid",
                string.Format("{0} = '{1}'", (isSAP ? "substr(category,1,3)" : "tbl_name"), drpCategory.SelectedValue),
                sDate, eDate,
                isSAP ? "code_sdiv" : "substr(acno,1,3)", loc,
                drpPayMode.SelectedValue == "ALL" ? "1=1" : "payment_mode = '" + drpPayMode.SelectedValue + "'",
                drpVendor.SelectedValue == "ALL" ? "1=1" : "vid = '" + drpVendor.SelectedValue + "'");
        if (!common.DownloadXLS(sql, "payment.xls", this))
        {
            lblMsg.Text = "No Record";
        }
    }
}