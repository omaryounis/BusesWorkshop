using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BusesWorkshop.Reports
{
    public partial class Dev_rptReminder : DevExpress.XtraReports.UI.XtraReport
    {

        private static int? Service_ID { get; set; }

        public Dev_rptReminder()
        {
            InitializeComponent();
        }

        public Dev_rptReminder(int? prmServiceID)
        {
            InitializeComponent();
            Service_ID = prmServiceID;
        }

        private void Dev_rptReminder_DataSourceDemanded(object sender, EventArgs e)
        {
            this.usp_Services_ReminderTableAdapter.Fill(dataSet11.usp_Services_Reminder, Service_ID);
        }

    }
}
