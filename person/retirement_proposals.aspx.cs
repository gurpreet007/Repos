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
                "decode(type,'S','Supperannuation','V','Voluntary') as \"Type\", " +
                "to_char(fromdate,'dd-MM-YYYY') as \"Start_Date\", to_char(todate,'dd-MM-YYYY') as \"End_Date\", "+
                "oonum as \"O/o Num\", oodate as \"O/o Date\" " +
                "from cadre.ret_proposals WHERE status='" + rbStatus.SelectedValue + "'";
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
        string rettype;
        Session["proposalno"] = gvProposals.Rows[e.NewEditIndex].Cells[2].Text.ToString();
        Session["proposalname"] = gvProposals.Rows[e.NewEditIndex].Cells[3].Text.ToString();
        Session["proposaldate"] = gvProposals.Rows[e.NewEditIndex].Cells[4].Text.ToString();
        rettype = gvProposals.Rows[e.NewEditIndex].Cells[5].Text.ToString();
        if (rettype == "Voluntary")
            Response.Redirect("Retirements_Vol.aspx");
        else
            Response.Redirect("Retirements_Sup.aspx");
    }
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
        //type: V-Voluntary, S-Supperannuation
        string sql = "insert into cadre.ret_proposals(pno,pname,pdate,type,status) values" +
                     "((select nvl(max(pno),0)+1 from cadre.ret_proposals),"+
                     "'" + txtpropname.Text + "',sysdate,'"+ drpRetType.SelectedValue +"','U')";
        
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

        sql = "delete from cadre.ret_list where propno =" + prono;
        OraDBConnection.ExecQry(sql);

        sql = "delete from cadre.ret_proposals where pno =" + prono;
        OraDBConnection.ExecQry(sql);
        FillGrid();
    }
}