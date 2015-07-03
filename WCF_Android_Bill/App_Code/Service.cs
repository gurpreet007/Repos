using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Diagnostics;

public class Service : IAndroidService
{
    private EventLog eventLog1;

    #region cyphercode
    private const string crypto_key = "1121231234zyx54321654321";
    private const string crypto_iv = "A1B2C3D4E5F6A7B8";
    #endregion
    public Android_Bill_Ret[] GetDataUsingDataContract(Android_Bill_Cols[] arr_scols)
	{
        #region variables
        string sql, sql_merge, sql_atomic;
        string sqlIns = @"INSERT INTO ONLINEBILL.ANDROID_BILLS(
                    LTBILLID, SUBDIVISIONNAME, ACCOUNTNUMBER, CONSUMERNAME, ADDRESS, 
                    BILLNUMBER, BILLDATE, BILLINGGROUP, CASHDUEDATE, CHEQUEDUEDATE, 
                    COLLECTIONCENTRE, COMPLAINTCENTREPHONE, TARIFFCODE, PHASECODE, METERMULTIPLIER, 
                    LINECTRATIO, METERCTRATIO, OVERALLMULTIPLYINGFACTOR, CONNECTEDLOAD, CURRENTMETERREADING, 
                    CURRENTREADINGDATE, PREVIOUSMETERREADING, PREVIOUSREADINGDATE, CURRENTCONSUMPTION, PREVIOUSCONSUMPTION, 
                    TOTALCONSUMPTION, SECURITYDEPOSIT, CBILLSTATUS, CONCESSIONUNITS, BILLPERIOD, 
                    CURRENTSOP, CURRENTED, CURRENTOCTROI, PREVIOUSROUNDAMOUNT, CURRENTMETERRENT, 
                    CURRENTSERVICECHARGES, AVGADJUSTMENTAMOUNT, FIXEDCHARGES, FUELCOSTADJUSTMENT, OTHERCHARGES, 
                    VOLTAGESURCHARGE, PREVIOUSARREARS, CURRENTARREARS, SUNDRYCHARGES, SUNDRYALLOWANCES, 
                    NETSOP, NETED, NETOCTROI, NETAMOUNT, SURCHARGE, 
                    GROSSAMOUNT, ADJUSTMENTAMOUNTDETAIL, SUNDRYCHARGESDETAIL, MMCCHARGES, CURRENTROUNDAMOUNT, 
                    BILLCYCLE, METERNO, CURRENTMETERCODE, PREVIOUSMETERCODE, AVGADJUSTMENTPERIOD, 
                    CONCEALEDUNITS, BILLYEAR, FINYEAR, CONSUMPTION1, CONSUMPTION2, 
                    CONSUMPTION3, CONSUMPTION4, CONSUMPTION5, CONSUMPTION6, DATED
                    ) VALUES (
                    '{0}',  '{1}',  '{2}',  '{3}',  '{4}', 
                    '{5}',  '{6}',  '{7}',  '{8}',  '{9}', 
                    '{10}', '{11}', '{12}', '{13}', '{14}', 
                    '{15}', '{16}', '{17}', '{18}', '{19}', 
                    '{20}', '{21}', '{22}', '{23}', '{24}', 
                    '{25}', '{26}', '{27}', '{28}', '{29}', 
                    '{30}', '{31}', '{32}', '{33}', '{34}', 
                    '{35}', '{36}', '{37}', '{38}', '{39}', 
                    '{40}', '{41}', '{42}', '{43}', '{44}', 
                    '{45}', '{46}', '{47}', '{48}', '{49}', 
                    '{50}', '{51}', '{52}', '{53}', '{54}', 
                    '{55}', '{56}', '{57}', '{58}', '{59}', 
                    '{60}', '{61}', '{62}', '{63}', '{64}', 
                    '{65}', '{66}', '{67}', '{68}', sysdate
                    ) ";
        string sqlMerge = "merge into onlinebill.mast_account m1 using " +
                    "(select '{0}' as acno from dual) d on (m1.account_no=d.acno) " +
                    "when matched then update set m1.table_name = '{1}' " +
                    "when not matched then insert (m1.account_no,m1.table_name) values(d.acno,'{1}')";
        List<Android_Bill_Ret> lstColsRet = new List<Android_Bill_Ret>();
        #endregion

        using (eventLog1 = new EventLog("ePayment", ".", "WS_Android_Bill"))
        {
            if (arr_scols == null)
            {
                eventLog1.WriteEntry("No Object Received");
                throw new ArgumentNullException("No Object Received");
            }
            foreach (Android_Bill_Cols scols in arr_scols)
            {
                #region queries
                sql = string.Format(sqlIns,
                        scols.LtBillID, scols.SubDivisionName, scols.AccountNumber, scols.ConsumerName, scols.Address, 
                        scols.BillNumber, scols.BillDate, scols.BillingGroup, scols.CashDueDate, scols.ChequeDueDate, 
                        scols.CollectionCentre, scols.ComplaintCentrePhone, scols.TariffCode, scols.PhaseCode, scols.MeterMultiplier, 
                        scols.LineCTRatio, scols.MeterCTRatio, scols.OverallMultiplyingFactor, scols.ConnectedLoad, scols.CurrentMeterReading, 
                        scols.CurrentReadingDate, scols.PreviousMeterReading, scols.PreviousReadingDate, scols.CurrentConsumption, scols.PreviousConsumption, 
                        scols.TotalConsumption, scols.SecurityDeposit, scols.CBillStatus, scols.ConcessionUnits, scols.BillPeriod, 
                        scols.CurrentSOP, scols.CurrentED, scols.CurrentOctroi, scols.PreviousRoundAmount, scols.CurrentMeterRent, 
                        scols.CurrentServiceCharges, scols.AvgAdjustmentAmount, scols.FixedCharges, scols.FuelCostAdjustment, scols.OtherCharges, 
                        scols.VoltageSurcharge, scols.PreviousArrears, scols.CurrentArrears, scols.SundryCharges, scols.SundryAllowances, 
                        scols.NetSOP, scols.NetED, scols.NetOctroi, scols.NetAmount, scols.Surcharge, 
                        scols.GrossAmount, scols.AdjustmentAmountDetail, scols.SundryChargesDetail, scols.MMCCharges, scols.CurrentRoundAmount, 
                        scols.BillCycle, scols.MeterNo, scols.CurrentMeterCode, scols.PreviousMeterCode, scols.AvgAdjustmentPeriod, 
                        scols.ConcealedUnits, scols.BillYear, scols.FinYear, scols.Consumption1, scols.Consumption2,
                        scols.Consumption3, scols.Consumption4, scols.Consumption5, scols.Consumption6
                    );

                sql_merge = string.Format(sqlMerge, scols.AccountNumber, "ONLINEBILL.ANDROID_BILLS");

                sql_atomic = string.Format("BEGIN {0}; {1}; END; ", sql, sql_merge).Replace(Environment.NewLine, "");
                #endregion
                try
                {
                    if (Crypto.Cipher(scols.LtBillID, crypto_key, crypto_iv) != scols.LtBillID_Hash)
                    {
                        lstColsRet.Add(new Android_Bill_Ret(scols.LtBillID, "F", "Invalid Hash"));
                    }
                    else
                    {
                        OraDBConnection.ExecQry(sql_atomic);
                        lstColsRet.Add(new Android_Bill_Ret(scols.LtBillID, "S"));
                    }
                }
                catch (Exception ex)
                {
                    lstColsRet.Add(new Android_Bill_Ret(scols.LtBillID, "F", ex.Message.Replace(Environment.NewLine, " ")));
                    eventLog1.WriteEntry(string.Format("{0}_{1}_{2}", scols.LtBillID, "FAILURE", ex.Message));
                }
            }

        }
        return lstColsRet.ToArray();
	}
}
