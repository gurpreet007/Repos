using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

public class common
{
    public const string strAdminName = "ADMIN";
    public const string strUserID = "userID";
    public const string strEmpID = "empID";
    public const string strLocation = "location";
    public const string strName = "name";
    public const string strSAP = "sap";
    public const string strNonSAP = "nonsap";
    public const string strIV = "C05A8F2F1C83121A";
    public const string strErrStyle = "&lt;ERR&gt;";
    public const string strErrLetter = "E";
    public const string strDupLetter = "D";

    public const string keystr = "1234567890abcdefabcdefab";

    public static bool DownloadXLS(string sql, string filename, Page pg)
    {
        System.Data.DataSet ds = OraDBConnection.GetData(sql);

        if (ds.Tables[0].Rows.Count == 0)
        {
            return false;
        }
        DataGrid dg = new DataGrid();
        dg.DataSource = ds;
        dg.DataBind();
        pg.Response.AddHeader("content-disposition", "attachment;filename=" + filename);
        pg.Response.Charset = "";
        pg.Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringwrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlwrite = new System.Web.UI.HtmlTextWriter(stringwrite);
        //htmlwrite.WriteLine("TITLE");
        dg.RenderControl(htmlwrite);
        pg.Response.Write(stringwrite.ToString());
        pg.Response.End();
        dg.Dispose();
        return true;
    }
    public static void FillInfo(System.Web.SessionState.HttpSessionState mySession, Label lblLoggedIn)
    {
        //lblLoggedIn.Text = string.Format("Loc: {0} ({1}),  User: {2} ({3})", 
        //    mySession[strLocation], mySession[strUserID], mySession[strName], mySession[strEmpID]);
        lblLoggedIn.Text = string.Format("Loc: {0} ({1})", mySession[strLocation], mySession[strUserID]);
    }
    public static bool isValidSession(System.Web.SessionState.HttpSessionState mySession)
    {
        //if (mySession[strUserID] == null ||
        //   mySession[strEmpID] == null ||
        //   mySession[strUserID].ToString() == string.Empty ||
        //   mySession[strEmpID].ToString() == string.Empty ||
        //   mySession[strEmpID].ToString().Length != 6)
        if (mySession[strUserID] == null ||
           mySession[strUserID].ToString() == string.Empty)
        {
            return false;
        }
        return true;
    }
    public static string HexAsciiConvert(string hex)
    {

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i <= hex.Length - 2; i += 2)
        {

            sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hex.Substring(i, 2),
            System.Globalization.NumberStyles.HexNumber))));
        }
        return sb.ToString();
    }
    public static string Decrypt(string cipherString, bool useHashing=false)
    {
        byte[] keyArray;
        byte[] keyIv;
        byte[] toEncryptArray = Convert.FromBase64String(cipherString);

        string ivstr = HexAsciiConvert(strIV);
        if (useHashing)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(keystr));
            keyIv = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(ivstr));
            hashmd5.Clear();
        }
        else
        {
            keyArray = ASCIIEncoding.ASCII.GetBytes(keystr);
            keyIv = ASCIIEncoding.ASCII.GetBytes(ivstr);
        }
        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        tdes.KeySize = 192;
        tdes.Key = keyArray;
        tdes.IV = keyIv;
        tdes.Mode = CipherMode.CBC;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        tdes.Clear();
        return UTF8Encoding.UTF8.GetString(resultArray);
    }
}