using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace BusesWorkshop.Reports
{
    public partial class Dev_rptBusesServices : DevExpress.XtraReports.UI.XtraReport
    {
        private static int? BusID { get; set; }

        public Dev_rptBusesServices()
        {
            InitializeComponent();
        }

        public Dev_rptBusesServices(int? prmBusID)
        {
            InitializeComponent();
            BusID = prmBusID;
        }

        private void Dev_rptBusesServices_DataSourceDemanded(object sender, EventArgs e)
        {
            this.usp_BusesServices_SelectTableAdapter.Fill(dataSet11.usp_BusesServices_Select, BusID);
        }

    }
}
