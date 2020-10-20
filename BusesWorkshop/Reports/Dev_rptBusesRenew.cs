using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BusesWorkshop.Reports
{
    public partial class Dev_rptBusesRenew : DevExpress.XtraReports.UI.XtraReport
    {
        
        public Dev_rptBusesRenew()
        {
            InitializeComponent();
        }

        private void Dev_rptBusesRenew_DataSourceDemanded(object sender, EventArgs e)
        {
            this.usp_Buses_RenewDatesTableAdapter.Fill(dataSet11.usp_Buses_RenewDates);
        }

    }
}
