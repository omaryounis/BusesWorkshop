using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BusesWorkshop.Reports
{
    public partial class VehclesInfo : DevExpress.XtraReports.UI.XtraReport
    {
        private static int? VehcleId = null;
        private static int? BrandId = null;
        private static int? ModelId = null;
        private static int? ClassId = null;
        private static int? CompanyId = null;
        private static int? DriverId = null;
        private static string SuperVisorId = null;
        private static int? CurrentCounterStart = null;
        private static int? CurrentCounterEnd = null;
        private static int? LicenseType = null;
        private static string LisnceNo = null;
        private static DateTime? LicenseExpiryDateStart = null;
        private static DateTime? LicenseExpiryDateEnd = null;
        private static DateTime? InspectionDateStart = null;
        private static DateTime? InspectionDateEnd = null;
        private static int? UserId = 0;
        public VehclesInfo()
        {
            InitializeComponent();
        }
        public VehclesInfo(int? prmVehcleId, int? prmBrandId, int? prmModelId, int? prmClassId, int? prmCompanyId, int? prmDriverId, string prmSuperVisorId, int? prmCurrentCounterStart, int? prmCurrentCounterEnd, int? prmLicenseType, string prmLisnceNo, DateTime? prmLicenseExpiryDateStart, DateTime? prmLicenseExpiryDateEnd, DateTime? prmInspectionDateStart, DateTime? prmInspectionDateEnd, int? prmUserId )
        {
            InitializeComponent();
        VehcleId = prmVehcleId;
            BrandId = prmBrandId;
            ModelId = prmModelId;
            ClassId = prmClassId;
            CompanyId = prmCompanyId;
            DriverId = prmDriverId;
            SuperVisorId = prmSuperVisorId;
            CurrentCounterStart = prmCurrentCounterStart;
            CurrentCounterEnd = prmCurrentCounterEnd;
            LicenseType = prmLicenseType;
            LisnceNo = prmLisnceNo;
            LicenseExpiryDateStart = prmLicenseExpiryDateStart;
            LicenseExpiryDateEnd = prmLicenseExpiryDateEnd;
            InspectionDateStart = prmInspectionDateStart;
            InspectionDateEnd = prmInspectionDateEnd;
            UserId = prmUserId;
        }

        private void VehclesInfo_DataSourceDemanded(object sender, EventArgs e)
        {
      
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BusWorkshopConnectionString"].ToString());
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BusWorkshopConnectionString"].ToString());
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "VehclesSelectAllFirst";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VehcleId", VehcleId ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@CompanyId", CompanyId ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@ClassId", ClassId ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@ModelId", ModelId ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@BrandId", BrandId ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@LicenseType", LicenseType ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@LicenseNo", LisnceNo ?? "NULL");
            cmd.Parameters.AddWithValue("@LicenseExpiryDateStart", LicenseExpiryDateStart ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@LicenseExpiryDateEnd", LicenseExpiryDateEnd ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@InspectioDateStart", InspectionDateStart ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@InspectioDateEnd", InspectionDateEnd ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@SuperVisorId", SuperVisorId ?? "NULL");
            cmd.Parameters.AddWithValue("@CurrentReadingStart", CurrentCounterStart ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@CurrentReadingEnd", CurrentCounterEnd ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@MainDriver", DriverId ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@UserId", UserId ?? Convert.DBNull);
            cmd.Connection = con;
            dt.Load(cmd.ExecuteReader());

            con.Close();


            this.DataSource = dt;
            this.DataMember = dataSet11.Tables["VehclesSelectFirst"].TableName;
        }
         

    }
}
