using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using lib_PSPCL_SMS;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private string GetCode()
    {
        string s = "0123456789";
        Random r = new Random();
        StringBuilder sb = new StringBuilder(10);
        int i;

        for (i = 0; i < 6; i++)
        {
            sb.Append(s[r.Next(s.Length)]);
        }
        return sb.ToString();
    }
    private void SendSMS(string mobile, string code, string user)
    {
        bool isSent;
        string sql;

        isSent = PSPCL_SMS.SendSMS(mobile, code);
        if (isSent)
        {
            sql = string.Format("insert into onlinebill.recon_smslogincode " +
                    "values(0,sysdate,'{0}','{1}','{2}')", mobile, code, user);
            OraDBConnection.ExecQry(sql);
            lblMessage.Text = "Code is sent on your registered mobile number.";
        }
        else
        {
            lblMessage.Text = "Unable to send code. Please Login Again. ";
        }
    }
    protected void btnGenCode_Click(object sender, EventArgs e)
    {
        string sql;
        string userid = txtUserID.Text.Trim().ToUpper();
        string pass = txtPass.Text.Trim();
        string mobile;
        string code;

        txtCode.Enabled = false;
        btnLogin.Enabled = false;

        if (! Regex.IsMatch(userid, "^[a-zA-Z0-9]+$") 
            || ! Regex.IsMatch(pass, "^[a-zA-Z0-9]+$"))
        {
            lblMessage.Text = "Invalid Username and Password";
            return;
        }

        sql = string.Format("select mobile from onlinebill.recon_user where userid='{0}' and pass='{1}'", userid, pass);
        mobile = OraDBConnection.GetScalar(sql);

        if(mobile == "" && mobile.Length != 10)
        {
            lblMessage.Text = "Invalid Username and Password";
            return;
        }

        code = GetCode();
        Session["code"] = code;
        SendSMS(mobile, code, userid);
        Session["LOGIN_USER"] = userid;
        txtCode.Enabled = true;
        btnLogin.Enabled = true;
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string user_code = txtCode.Text.Trim();
        if (Session["code"] == null)
        {
            return;
        }
        if (! Regex.IsMatch(user_code, "^[0-9]{6}$") 
            || user_code != Session["code"].ToString())
        {
            lblMessage.Text = "Invalid Code Entered.";
            return;
        }
        Session["code"] = string.Empty;
        Response.Redirect("./Default.aspx");
    }
}