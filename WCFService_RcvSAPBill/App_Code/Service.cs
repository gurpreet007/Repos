using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

public class Service_Rcv_SAP_Rec : IService
{
    public string GetDataUsingDataContract(SAP_Cols scols)
	{
		if (scols == null)
		{
			throw new ArgumentNullException("No object received");
		}

        //do all the validation and processing here
        //

        //return back status to caller
        return string.Format("{0}_{1}",scols.contract_account_number,"SUCCESS");
	}
}
