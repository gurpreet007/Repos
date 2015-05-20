using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class Common
{
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
}