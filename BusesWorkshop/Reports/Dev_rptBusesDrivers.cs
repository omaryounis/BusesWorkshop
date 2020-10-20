using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BusesWorkshop.Reports
{
    public partial class Dev_rptBusesDrivers : DevExpress.XtraReports.UI.XtraReport
    {
      private static  int? BusID { get; set; }

        public Dev_rptBusesDrivers()
        {
            InitializeComponent();
        }

        public Dev_rptBusesDrivers(int? prmBusID)
        {
            InitializeComponent();
            BusID = prmBusID;
        }

        private void Dev_rptBusesDrivers_DataSourceDemanded(object sender, EventArgs e)
        {
            this.usp_BusesDriversTableAdapter.Fill(dataSet11.usp_BusesDrivers, BusID);
        }
    }
}
