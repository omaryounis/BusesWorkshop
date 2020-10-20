using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BusesWorkshop.DAL.Bus;
using System.Data;
namespace BusesWorkshop.Reports
{
    public partial class Dev_SuperVisorsCardsBalance : DevExpress.XtraReports.UI.XtraReport
    {
        private static int? EmployeeID { get; set; }
        private static string EmployeeName { get; set; }
        public Dev_SuperVisorsCardsBalance()
        {
            InitializeComponent();
        }
        public Dev_SuperVisorsCardsBalance(int? prmEmployeeID, string prmEmployeeName)
        {
            InitializeComponent();
            EmployeeID = prmEmployeeID;
            EmployeeName = prmEmployeeName;
        }

        private void Dev_SuperVisorsCardsBalance_DataSourceDemanded(object sender, EventArgs e)
        {
            WorkshopDataContext dc = new WorkshopDataContext();

            DataTable dt = new DataTable();
            dt = dc.FuelCardsBalance(EmployeeID).CopyToDataTable();
            this.Parameters["PrmSuperVisorName"].Value = EmployeeName;
           
            this.DataSource = dt;
 this.DataMember = dataSet11.Tables["FuelCardBalances"].TableName;
            // //PrmSuperVisorName

        }

    }
}
