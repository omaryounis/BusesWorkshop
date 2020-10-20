using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using BusesWorkshop.DAL.Bus;

namespace BusesWorkshop.Reports
{
    public partial class Dev_InsuranceAlarm : DevExpress.XtraReports.UI.XtraReport
    {
        private static int? UserId = null;
        public Dev_InsuranceAlarm()
        {
            InitializeComponent();
        }
        public Dev_InsuranceAlarm(int?  prmUserId)
        {
            InitializeComponent();
            UserId = prmUserId;
        }

        private void Dev_InsuranceAlarm_DataSourceDemanded(object sender, EventArgs e)
        {
            WorkshopDataContext dc = new WorkshopDataContext();
            DataTable dt = new DataTable();
            dt = (dc.InsuranceAlarmSelect(UserId)).CopyToDataTable();
            this.DataSource = dt;
            this.DataMember = dataSet11.Tables["SelectInsuranceAlarm"].TableName;
        }

    }
}
