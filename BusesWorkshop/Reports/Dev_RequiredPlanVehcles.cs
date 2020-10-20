using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BusesWorkshop.DAL.Bus;
using System.Data;
namespace BusesWorkshop.Reports
{
    public partial class Dev_RequiredPlanVehcles : DevExpress.XtraReports.UI.XtraReport
    {
        private static int? DriverID { get; set; }
        private static int? CompanyID { get; set; }
        private static int? VehcleID { get; set; }
        private static int? UserId { get; set; }

        public Dev_RequiredPlanVehcles()
        {
            InitializeComponent();
        }
        public Dev_RequiredPlanVehcles(int? prmDriverID, int? prmCompanyID, int? prmVehcleID, int? prmUserId)
        {
            InitializeComponent();
            DriverID = prmDriverID;
            CompanyID=prmCompanyID;
            VehcleID = prmVehcleID;
            UserId = prmUserId;
        }
        

        private void Dev_RequiredPlanVehcles_DataSourceDemanded(object sender, EventArgs e)
        {
            
            WorkshopDataContext dc = new WorkshopDataContext();

            DataTable dt = new DataTable();
            dt = (dc.VehclesPlansRequired(CompanyID, VehcleID, DriverID, UserId)).CopyToDataTable();
           
            this.DataSource = dt;
            this.DataMember = dataSet11.Tables["RequiredPlans"].TableName;
        }
        
    }
}
