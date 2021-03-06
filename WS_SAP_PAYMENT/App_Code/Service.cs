﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Diagnostics;

public class SERVICE_SAP_PAYMENT : IService
{
    private EventLog eventLog1;

   //Private const string crypto_key = "1121231234zyx12345123456";
    //private const string crypto_iv = "A1B2C3D4E5F6A7B8";
    public string GetDataUsingDataContract(SAP_Payment_Cols scols)
    {
        string sql, sql_atomic;
        string strRet;

        //System.Threading.Thread.Sleep(15000);
        using (eventLog1 = new EventLog("ePayment", ".", "WS_SAP_PAYMENT"))
        {
            if (scols == null)
            {
                eventLog1.WriteEntry("No Object Received");
                throw new ArgumentNullException("No Object Received");
            }

            //string strcipher = Crypto.Cipher(scols.TXNID, crypto_key, crypto_iv);
            //if (strcipher != scols.TXNID_HASH)
            //{
            //    return string.Format("{0}_{1}", scols.TXNID, "INVALIDHASH");
            //}

            sql = string.Format(
                @"INSERT INTO ONLINEBILL.PAYMENT_SAP(
                MR_DOC_NO, ACNO, BILLCYCLE, BILLGROUP, DUEDATE, 
                AMNT, RCPTNO, RCPTDT, TXNID, TXNDT, 
                DATED
                ) VALUES (
                '{0}', '{1}', '{2}', '{3}', '{4}',
                '{5}', '{6}', '{7}', '{8}', '{9}', 
                sysdate
                ) ",
                scols.MR_DOC_NO, scols.ACNO, scols.BILLCYCLE, scols.BILLGROUP, scols.DUEDATE,
                scols.AMNT, scols.RCPTNO, scols.RCPTDT, scols.TXNID, scols.TXNDT
                );

            sql_atomic = string.Format("BEGIN {0}; END; ", sql).Replace(Environment.NewLine, "");
            try
            {
                OraDBConnection.ExecQry(sql_atomic);
            }
            catch (Exception ex)
            {
                strRet = string.Format("{0}_{1}_{2}", scols.TXNID, "FAILURE", ex.Message);
                eventLog1.WriteEntry(strRet);
                return strRet;
            }
            strRet = string.Format("{0}_{1}", scols.TXNID, "SUCCESS");
            eventLog1.WriteEntry(strRet);
        }
        return strRet;
    }
}
