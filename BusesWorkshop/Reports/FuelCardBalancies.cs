using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BusesWorkshop.DAL.Bus;
using System.Data;

namespace BusesWorkshop.Reports
{
    public partial class FuelCardBalancies : DevExpress.XtraReports.UI.XtraReport
    {
        public FuelCardBalancies()
        {
            InitializeComponent();
        }

        private void FuelCardBalancies_DataSourceDemanded(object sender, EventArgs e)
        {
            WorkshopDataContext dc = new WorkshopDataContext();

            DataTable dt = new DataTable();
            dt = (dc.FuelCardBalances()).CopyToDataTable();

            this.DataSource = dt;
            this.DataMember = dataSet11.Tables["FuelCardBalances"].TableName;
        }

    }
}
