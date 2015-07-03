using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace SAP_SBM_UPLOAD
{
    public partial class SAP_SBM_UPLOAD : ServiceBase
    {
        private EventLog eventLog1;
        public SAP_SBM_UPLOAD()
        {
            InitializeComponent();
            //string logName = EventLog.LogNameFromSourceName("SAP_SBM_UPLOAD", ".");
            //if (logName != "ePayment")
            //{
            //    EventLog.DeleteEventSource("SAP_SBM_UPLOAD");
            //}
            eventLog1 = new EventLog("ePayment", ".", "SRV_SAP_SBM_UPLOAD");
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("SAP_SBM_UPLOAD Started");
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000; // 60 seconds
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            eventLog1.WriteEntry("Writing DB Entry", EventLogEntryType.Information);
            OraDBConnection.ExecQry("insert into onlinebill.serv_test values(sysdate)");
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("SAP_SBM_UPLOAD Stopped");
        }
    }
}
