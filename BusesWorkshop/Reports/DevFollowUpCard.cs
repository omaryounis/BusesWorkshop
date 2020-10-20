using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BusesWorkshop.DAL.Bus;
using System.Data;
namespace BusesWorkshop.Reports
{
    public partial class DevFollowUpCard : DevExpress.XtraReports.UI.XtraReport
    {
        private static int? VehcleID { get; set; }
  
        private static DateTime? startDate { get; set; }
        private static DateTime? EndDate { get; set; }
        public DevFollowUpCard()
        {
            InitializeComponent();
        }
        public DevFollowUpCard(int? prmVehcleID, DateTime? prmstartDate, DateTime? prmEndDate)
        {
            InitializeComponent();
            VehcleID = prmVehcleID;
            EndDate = prmEndDate;
            startDate = prmstartDate;

        }

        private void DevFollowUpCard_DataSourceDemanded(object sender, EventArgs e)
        {
            if (VehcleID == null || EndDate == null || startDate == null) return;
            WorkshopDataContext dc = new WorkshopDataContext();

            DataTable dt = new DataTable();
            dt = (dc.FollowUpCardBetweenTwoDates(VehcleID , startDate , EndDate ).CopyToDataTable());

            this.DataSource = dt;
            this.DataMember = dataSet11.Tables["FollowUpCard"].TableName;
        }

        private void DevFollowUpCard_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void xrLabel5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

           
        }

        private void xrLabel4_AfterPrint(object sender, EventArgs e)
        {

        }
        int Dis = 0;
        int LIT = 0;
        private void xrLabel4ppp_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value != null)
                Dis += Convert.ToInt32(e.Value);
        }

        private void xrLabel2ppp_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value != null)
                LIT += Convert.ToInt32(e.Value);
        }

        private void xrLabel4_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (Dis == 0 || LIT == 0) return;
            xrLabel4.Text = (Dis / LIT).ToString(); 
        }
    }
}
