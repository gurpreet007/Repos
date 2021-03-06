﻿using System;
using System.Data; 
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmproposalmenu : System.Web.UI.Page
{
    string myaddress = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGrid();
            panNewProp.Visible = false;
            txtpropname.Text = "";
        }
    }
    private void FillGrid()
    {
        string sql;
        DataSet ds;
        sql = "select pno as \"Proposal_No\",pname as \"Proposal_Name\"," +
                "to_char(pdate,'DD-Mon-YYYY') as \"Creation_Date\","+
                "decode(olddesg,9050,'SE',9052,'Sr. XEN') as \"Desg\", " +
                "to_char(fromdate,'dd-MM-YYYY') as \"Start_Date\", to_char(todate,'dd-MM-YYYY') as \"End_Date\", "+
                "oonum as \"O/o Num\", oodate as \"O/o Date\" " +
                "from cadre.rd_proposals WHERE status='" + rbStatus.SelectedValue + "'";
        ds = OraDBConnection.GetData(sql);
        gvProposals.DataSource = ds;
        gvProposals.DataBind();
    }
    protected void btncproposal_Click(object sender, EventArgs e)
    {
        panNewProp.Visible = true;
    }
    protected void gvProposals_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Session["proposalno"] = gvProposals.Rows[e.NewEditIndex].Cells[2].Text.ToString();
        Session["proposalname"] = gvProposals.Rows[e.NewEditIndex].Cells[3].Text.ToString();
        Session["proposaldate"] = gvProposals.Rows[e.NewEditIndex].Cells[4].Text.ToString();
        Response.Redirect("~/redesig.aspx");
    }
    //protected void gvProposals_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    string prono = gvProposals.Rows[e.RowIndex].Cells[1].Text.ToString();
    //    string sql = string.Empty;

    //    sql = "delete from cadre.prop_redesig where propno =" + prono;
    //    OraDBConnection.ExecQry(sql);

    //    sql = "delete from cadre.rd_proposals where pno =" + prono;
    //    OraDBConnection.ExecQry(sql);
    //    FillGrid();
    //}
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        panNewProp.Visible = false;
        txtpropname.Text = "";
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        if (txtpropname.Text == "")
        {
            Utils.ShowMessageBox(this, "Enter Proposal Name");
            return;
        }

        //status: U-Unsaved, S-Saved
        //type: TP-Transfer/Promotion, RD-Redesignation
        string sql = "insert into cadre.rd_proposals(pno,pname,pdate,status) values" +
                     "((select nvl(max(pno),0)+1 from cadre.rd_proposals),"+
                     "'" + txtpropname.Text + "',sysdate,'U')";
        
        bool ret = false;
        try
        {
            ret = OraDBConnection.ExecQry(sql);
            if (ret == false)
            {
                throw new Exception("Error in Creating Proposal");
            }
        }
        catch (Exception ex)
        {
            Utils.ShowMessageBox(this, ex.Message);
            return;
        }
        panNewProp.Visible = false;
        txtpropname.Text = "";
        FillGrid();
    }
    protected void rbStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
        //only show edit and delete buttons in case of Unsaved proposals
        gvProposals.AutoGenerateEditButton = rbStatus.SelectedValue=="U";
        gvProposals.AutoGenerateDeleteButton = rbStatus.SelectedValue == "U";
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        string prono = gvProposals.Rows[((GridViewRow)((LinkButton)sender).Parent.Parent).RowIndex].Cells[2].Text.ToString();
        string sql = string.Empty;

        sql = "delete from cadre.prop_redesig where propno =" + prono;
        OraDBConnection.ExecQry(sql);

        sql = "delete from cadre.rd_proposals where pno =" + prono;
        OraDBConnection.ExecQry(sql);
        FillGrid();
    }
}