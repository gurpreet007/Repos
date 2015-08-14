using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class rptabstract : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LOGIN_USER"] == null)
        {
            Response.Redirect("./Login.aspx");
        }
        if (!IsPostBack){
            lblrpt_name.Text = Request.QueryString["rpt"];
        }

    }
    protected void lbhome_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Default.aspx");
    }
    public void get_abs_rpt_para(string mth, string yr, out string cur_apr_year, out string last_mon_year,out string tdate,out string sap)
    {
        cur_apr_year = ((mth == "01" ||
                        mth == "02" ||
                        mth == "03") ?
                        int.Parse(yr) - 1 :
                        int.Parse(yr)).ToString() + "04";
        
        if (mth == "01")
        {
            //for 201501 set 201412
            last_mon_year = (int.Parse(yr) - 1).ToString() + "12";
        }
        else if (mth == "04")
        {
            //for 201504 set 201504
            last_mon_year = yr + "04";
        }
        else
        {
            //for 201507 set 201506
            last_mon_year = yr +
                (int.Parse(mth) - 1).ToString().PadLeft(2, '0');
        }
        tdate = string.Concat(yr, mth);
        sap = string.Empty;
        if (lblrpt_name.Text == "SAP_ABSTRACT_REPORT")
        {
            sap = "Y";
        }
        else if (lblrpt_name.Text == "NONSAP_ABSTRACT_REPORT")
        {
            sap = "N";
        }
      
    }
    private string GET_QRY(string sap,string cur_apr_year, string last_mon_year, string tdate)
    {
        string sql;
        string sql_final;
        string div_code=string.Empty;
        string circle_code = string.Empty;
        if (sap == "Y") {
            div_code = " substr(CODE_SDIV,1,3)";
                    }
        else if (sap == "N") {
            div_code = " rpad(substr(acno,1,2),3,'0')";
                 }
        sql = string.Format(" select {0} as DIV_CODE,count(distinct txnid) as TXN_DURING,sum(amt) as AMT_DURING,0 as TXN_UPTO,0 as AMT_UPTO " +  
                            " from onlinebill.payment where recon_id is not null and if_sap='{1}' and to_char(txndate,'yyyymm')= '{2}' group by {0} " +
                            " union all " +  
                            " select {0} as DIV_CODE, 0 TXN_DURING,0 as AMT_DURING,count(distinct txnid) as TXN_UPTO,sum(amt) as AMT_UPTO "  +
                            " from onlinebill.payment where recon_id is not null and if_sap='{1}' and to_char(txndate,'yyyymm') " +
                            " between '{3}' and '{4}' group by {0} ",div_code,sap,tdate,cur_apr_year,last_mon_year) ;
        sql =   " select DIV_CODE,sum(TXN_DURING) as TXN_DURING,sum(AMT_DURING) as AMT_DURING, " +
                " sum(TXN_UPTO) as TXN_UPTO,sum(AMT_UPTO) as AMT_UPTO, sum(TXN_DURING) + sum(TXN_UPTO) as TOTAL_TXN," +
                " sum(AMT_DURING) + sum(AMT_UPTO) as TOTAL_AMT from (" + sql + ") group by DIV_CODE";

        sql_final = string.Format("select DIV_CODE, GET_CDSNAME(DIV_CODE,'{0}','D') as DIV_NAME,"+
                                    " GET_CDSNAME(DIV_CODE,'{0}','C') as CIRCLE_NAME," +
                                    " TXN_UPTO,AMT_UPTO,TXN_DURING,AMT_DURING,TOTAL_TXN,TOTAL_AMT " +
                                    " from ({1}) a order by DIV_CODE",sap,sql);
        return sql_final;
    }
    protected void btnview_Click(object sender, EventArgs e)
    {

        string sql;
        string cur_apr_year;
        string last_mon_year;
        string tdate;
        string mth = drpmonth.SelectedItem.Text;
        string yr = drpyear.SelectedItem.Text;
        DataSet ds;
        string sap=string.Empty;
        GridView1.DataSource = null;
        GridView1.DataBind();
        lblmsg.Text = string.Empty;
        if (drpmonth.SelectedItem.Value == "0" || drpyear.SelectedItem.Value == "0")
        {
            lblmsg.Text = "Please select month and year";
            return;
        }
               
        get_abs_rpt_para(mth, yr, out cur_apr_year, out last_mon_year, out tdate,out sap);
        sql = GET_QRY(sap,cur_apr_year, last_mon_year, tdate);
        GridView1.DataSource = OraDBConnection.GetData(sql);
        GridView1.DataBind();
    }
    protected void btnexp_Click(object sender, EventArgs e)
    {
        string sql;
        string cur_apr_year;
        string last_mon_year;
        string tdate;
        string mth = drpmonth.SelectedItem.Text;
        string yr = drpyear.SelectedItem.Text;
        string sap = string.Empty;
        lblmsg.Text = string.Empty;
        if (drpmonth.SelectedItem.Value == "0" || drpyear.SelectedItem.Value == "0")
        {
            lblmsg.Text = "Please select month and year";
            return;
        }
        get_abs_rpt_para(mth, yr, out cur_apr_year, out last_mon_year, out tdate, out sap);
        sql = GET_QRY(sap,cur_apr_year, last_mon_year, tdate);
        Common.DownloadXLS(sql, lblrpt_name.Text + ".xls", this); 
        
    }
}