using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using BusesWorkshop.DAL.Bus;

namespace BusesWorkshop.Reports
{
    public partial class DevTotalCostNew : DevExpress.XtraReports.UI.XtraReport
    {

        private static int? VehcleID { get; set; }
        private static DateTime? From = null;
        private static DateTime? To = null;
        public DevTotalCostNew()
        {
            InitializeComponent();
        }

        public DevTotalCostNew(int? prmVehcleID, DateTime? prmFrom, DateTime? prmTo)
        {
            InitializeComponent();
            VehcleID = prmVehcleID;
            From = prmFrom;
            To = prmTo;
        }

        private void DevTotalCostNew_DataSourceDemanded(object sender, EventArgs e)
        {
            if (VehcleID != null)
            {
                WorkshopDataContext dc = new WorkshopDataContext();
                DataTable dt = new DataTable();
                dt = dc.TotalCostSp(VehcleID ,  From , To ).CopyToDataTable();
                this.DataSource = dt;
                //this.DataMember = dataSet11.Tables["TotalCost"].TableName;
                this.DataMember = dataSet11.Tables["TotalCostSp"].TableName;
            }
        }

    }
}
