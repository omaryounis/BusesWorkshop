using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace BusesWorkshop.Reports
{
    public partial class Dev_Vehcles2 : DevExpress.XtraReports.UI.XtraReport
    {
       private static int? VehcleId = null;
       private static int? CompanyId = null;
       private static int? FuelId = null;
       private static string MotorNo = null;
       private static string BodyNo = null;
       private static int? CC = null;
       private static int? CounterReadingStartSart = null;
       private static int? CounterReadingStartEnd = null;
       private static decimal? AverageFuelConsumptionStart = null;
       private static decimal? AverageFuelConsumptionEnd = null;
       private static int? Color = null;
       private static int? CylenderNo = null;
       private static int? ManufactureYearStart = null;
       private static int? ManufactureYearEnd = null;
       private static int? ManufacturingCountry = null;
       private static DateTime? StartOperationDateStart = null;
       private static DateTime? StartOperationDateEnd = null;
       private static DateTime? EndOperationDateStart = null;
       private static DateTime? EndOperationDateEnd = null;
       private static bool? IsActive = null;
       private static bool? IsInActive = null;
       private static int? UserId = null;
        public Dev_Vehcles2()
        {
            InitializeComponent();
        }
        public Dev_Vehcles2(int? prmVehcleId, int? prmCompanyId, int? prmFuelId, string prmMotorNo, string prmBodyNo, int? prmCC, int? prmCounterReadingStartSart, int? prmCounterReadingStartEnd, decimal? prmAverageFuelConsumptionStart, decimal? prmAverageFuelConsumptionEnd, int? prmColor, int? prmCylenderNo, int? prmManufactureYearStart, int? prmManufactureYearEnd, int? prmManufacturingCountry, DateTime? prmStartOperationDateStart, DateTime? prmStartOperationDateEnd, DateTime? prmEndOperationDateStart, DateTime? prmEndOperationDateEnd, bool? prmIsActive, bool? prmIsInActive, int? prmUserId)
        {
            InitializeComponent();


            VehcleId = prmVehcleId;
            CompanyId = prmCompanyId;
            FuelId = prmFuelId;
            MotorNo = prmMotorNo;
            BodyNo = prmBodyNo;
            CC = prmCC;
            CounterReadingStartSart = prmCounterReadingStartSart;
            CounterReadingStartEnd = prmCounterReadingStartEnd;
            AverageFuelConsumptionStart = prmAverageFuelConsumptionStart;
            AverageFuelConsumptionEnd = prmAverageFuelConsumptionEnd;
            Color = prmColor;
            CylenderNo = prmCylenderNo;
            ManufactureYearStart = prmManufactureYearStart;
            ManufactureYearEnd = prmManufactureYearEnd;
            ManufacturingCountry = prmManufacturingCountry;
            StartOperationDateStart = prmStartOperationDateStart;
            StartOperationDateEnd = prmStartOperationDateEnd;
            EndOperationDateStart = prmEndOperationDateStart;
            EndOperationDateEnd = prmEndOperationDateEnd;
            IsActive = prmIsActive;
            IsInActive = prmIsInActive;
            UserId = prmUserId;
            
        }

        private void Dev_Vehcles2_DataSourceDemanded(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BusWorkshopConnectionString4"].ToString());

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BusWorkshopConnectionString"].ToString());
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "VehclesSelectAllSecond";
            cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@VehcleId",VehcleId ?? Convert.DBNull) ;
           cmd.Parameters.AddWithValue("@CompanyId", CompanyId ?? Convert.DBNull);
           cmd.Parameters.AddWithValue("@FueL", FuelId ?? Convert.DBNull );
            cmd.Parameters.AddWithValue("@MotorNo",MotorNo ?? "null");
            cmd.Parameters.AddWithValue("@BodyNo",BodyNo ?? "null");
            cmd.Parameters.AddWithValue("@CC",CC ??  Convert.DBNull);
            cmd.Parameters.AddWithValue("@CounterReadingStartSart",CounterReadingStartSart ??  Convert.DBNull);
            cmd.Parameters.AddWithValue("@CounterReadingStartEnd", CounterReadingStartEnd ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@AverageFuelConsumptionStart", AverageFuelConsumptionStart ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@AverageFuelConsumptionEnd", AverageFuelConsumptionEnd ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@Color", Color ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@CylenderNo", CylenderNo ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@ManufactureYearStart", ManufactureYearStart ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@ManufactureYearEnd", ManufactureYearEnd ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@ManufacturingCountry", ManufacturingCountry ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@StartOperationDateStart", StartOperationDateStart ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@StartOperationDateEnd", StartOperationDateEnd ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@EndOperationDateStart", EndOperationDateStart ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@EndOperationDateEnd", EndOperationDateEnd ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@IsActive", IsActive ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@IsInActive", IsInActive ?? Convert.DBNull);
            cmd.Parameters.AddWithValue("@UserId", UserId ?? Convert.DBNull);

            cmd.Connection = con;
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            con.Close();


            this.DataSource = dt;
            this.DataMember = dataSet11.Tables["VehclesSelectSecond"].TableName;
        }
    }
}
