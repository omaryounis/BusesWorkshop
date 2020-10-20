using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BusesWorkshop.DAL.Bus;
using System.Data;
namespace BusesWorkshop.Reports
{
    public partial class Dev_MaintenenceCost : DevExpress.XtraReports.UI.XtraReport
    {
        private static int? VehcleID { get; set; }
        private static string  MaintType { get; set; }
        private static int? PlanId { get; set; }
        private static int? SparId { get; set; }
        private static int? ServiceId { get; set; }
        private static DateTime? startDate { get; set; }
        private static DateTime? EndDate { get; set; }
        private static int? UserId { get; set; }
        public Dev_MaintenenceCost()
        {
            InitializeComponent();
        }
        public Dev_MaintenenceCost(int? prmVehcleID, string prmMaintType, int? prmPlanId, int? prmSparId, int? prmServiceId, DateTime? prmstartDate, DateTime? prmEndDate ,int? prmUserId)
        {
            InitializeComponent();
            VehcleID = prmVehcleID;
            MaintType = prmMaintType;
            PlanId = prmPlanId;
            SparId = prmSparId;
            ServiceId = prmServiceId;
            startDate = prmstartDate;
            EndDate = prmEndDate;
            UserId = prmUserId;
        }

        private void Dev_MaintenenceCost_DataSourceDemanded(object sender, EventArgs e)
        {
            WorkshopDataContext dc = new WorkshopDataContext();

            DataTable dt = new DataTable();
            dt = (dc.MaintenenceCost(VehcleID, startDate, EndDate, MaintType, PlanId, ServiceId, SparId, UserId)).CopyToDataTable();

            this.DataSource = dt;
            this.DataMember = dataSet11.Tables["MaintenenceCost"].TableName;
        }

        private void xrTableCell13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell13.HeightF = 1;
        }
    }
}
