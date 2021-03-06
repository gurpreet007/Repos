﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
public class OraDBConnection
{
    private const string conn_str = "user id=onlinebill;password=pspcl;"+
        "data source="+
            "(DESCRIPTION="+
                "(ADDRESS_LIST="+
                    "(ADDRESS=(PROTOCOL=TCP)"+
                             "(HOST=10.61.7.144)"+
                             "(PORT=1521)"+
                    ")"+
                ")(CONNECT_DATA=(SERVICE_NAME=pshr))"+
            ")";
    
    public static DataSet GetData(String sql)
    {
        OracleDataAdapter adp = new OracleDataAdapter(sql, conn_str);
        DataSet ds = new DataSet();
        try
        {
            adp.Fill(ds);
            return ds;
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            ds.Dispose();
            adp.Dispose();
        }
    }
    public static OracleConnection ConnectionOpen()
    {
        OracleConnection con = new OracleConnection(OraDBConnection.conn_str);
        try
        {
            con.Open();
        }
        catch(Exception ex)
        {
            throw ex;
        }
        return con;
    }
    public static void ConnectionClose(OracleConnection con)
    {
        con.Close();
        con.Dispose();
    }
    public static Boolean ExecQryOnConnection(OracleConnection con, string sql)
    {
        OracleCommand cmd = new OracleCommand(sql, con);

        try
        {
            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            cmd.Dispose();
        }
        return true;
    }
    public static Boolean ExecQry(string sql)
    {
        OracleConnection con = new OracleConnection(conn_str);
        OracleCommand cmd = new OracleCommand(sql, con);
        
        //user, time, <sql>
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
            con.Dispose();
            cmd.Dispose();
        }
    }
    public static string GetScalar(string sql)
    {
        OracleConnection con = new OracleConnection(conn_str);
        OracleCommand cmd = new OracleCommand(sql, con);
        object obj;
        try
        {
            con.Open();

            obj = cmd.ExecuteScalar();
            return (obj==null) ? "" : obj.ToString();
        }
        catch(Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
            cmd.Dispose();
            con.Dispose();
        }
    }
    //public static void FillGrid(ref System.Web.UI.WebControls.GridView gv, string sql)
    //{
    //    gv.DataSource = GetData(sql);
    //    gv.DataBind();
    //}
    public static bool ExecProc(string procName)
    {
        OracleConnection con = new OracleConnection(conn_str);
        OracleCommand cmd = new OracleCommand(procName, con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            con.Open();

            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            con.Close();
            con.Dispose();
            cmd.Dispose();
        }
    }
}