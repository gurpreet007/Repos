using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceReference1;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strRet;

        //create object for parameters
        ServiceReference1.SAP_Cols scols = new SAP_Cols();

        //create service object
        ServiceReference1.ServiceClient sc = new ServiceClient(); 
        
        //fill up values of parameters
        scols.contract_account_number = "3000045678";
        
        //call service and check the returned value
        strRet = sc.GetDataUsingDataContract(scols);

        //close the service client
        sc.Close();
    }
}