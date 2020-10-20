using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BusesWorkshop.DAL.Accounting;
using System.Data;
using BusesWorkshop.Pages.PagesPermissions;
using BusesWorkshop.DAL.Bus;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Reports
{
    public partial class Vehcles2 : System.Web.UI.Page
    {
        #region "Properties"
        WorkshopDataContext dc = new WorkshopDataContext();
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        #endregion
        #region "Methods"

        private void FillManufacturingCountries()
        {
            ddl_ManufacturingCountryId.DataSource = from p in dc.ConfigDetails where p.MasterId == 9 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_ManufacturingCountryId.TextField = "ConfigDetailName";
            ddl_ManufacturingCountryId.ValueField = "ConfigDetailId";
            ddl_ManufacturingCountryId.DataBind();
        }

        private void FillCylinders()
        {
            ddl_CylinderId.DataSource = from p in dc.ConfigDetails where p.MasterId == 7 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_CylinderId.TextField = "ConfigDetailName";
            ddl_CylinderId.ValueField = "ConfigDetailId";
            ddl_CylinderId.DataBind();
        }

        private void FillColors()
        {
            ddl_ColorId.DataSource = from p in dc.ConfigDetails where p.MasterId == 10 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_ColorId.TextField = "ConfigDetailName";
            ddl_ColorId.ValueField = "ConfigDetailId";
            ddl_ColorId.DataBind();
        }

        private void FillCC()
        {
            ddl_CC.DataSource = from p in dc.ConfigDetails where p.MasterId == 6 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_CC.TextField = "ConfigDetailName";
            ddl_CC.ValueField = "ConfigDetailId";
            ddl_CC.DataBind();
        }

        private void FillFuels()
        {
            ddl_FuelId.DataSource = from p in dc.ConfigDetails where p.MasterId == 5 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_FuelId.TextField = "ConfigDetailName";
            ddl_FuelId.ValueField = "ConfigDetailId";
            ddl_FuelId.DataBind();
        }
        private void FillVehcles()
        {
            int? id = null;
            if (Session["UserID"] != null)
            {
                id = int.Parse(Session["UserID"].ToString());
            }
            else
            {
                Response.Redirect("../Pages/Login.aspx", false);
            }
            ddl_VehcleId.DataSource = from p in dc.Vehcles where p.Company.UserCompanies.Any(x => x.UserID == id) select new { p.VehcleId, p.PlateNo };
            ddl_VehcleId.TextField = "PlateNo";
            ddl_VehcleId.ValueField = "VehcleId";
            ddl_VehcleId.DataBind();

        }

        void fill_Companies()
        {
            int? id = null;
            if (Session["UserID"] != null)
            {
                id = int.Parse(Session["UserID"].ToString());
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
            WorkshopDataContext dc = new WorkshopDataContext();

            ddl_CompanyId.DataSource = from p in dc.Companies where p.UserCompanies.Any(x => x.UserID == id) select new { p.ID, p.CompName };
            ddl_CompanyId.ValueField = "ID";
            ddl_CompanyId.TextField = "CompName";
            ddl_CompanyId.DataBind();
        }




        public static List<string> SearchMotors(string prefixText, int count)
        {
            List<string> MotorsNo = new List<string>();
            WorkshopDataContext ctx = new WorkshopDataContext();
            MotorsNo = (from p in ctx.Vehcles where p.MotorNo.Contains(prefixText) select p.MotorNo).ToList();
            return MotorsNo;
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]

        public static List<string> SearchBodies(string prefixText, int count)
        {
            List<string> BodiesNo = new List<string>();
            WorkshopDataContext ctx = new WorkshopDataContext();
            BodiesNo = (from p in ctx.Vehcles where p.BodyNo.Contains(prefixText) select p.BodyNo).ToList();
            return BodiesNo;
        }
        #endregion
        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "تقرير بيانات السيارات 2";
            permissions(dc);
            if (!IsPostBack)
            {
                Dev_Vehcles2 RP = new Dev_Vehcles2(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, int.Parse(Session["UserId"].ToString()));
                FillVehcles();
                fill_Companies();
                FillFuels();
                FillCC();
                FillColors();
                FillCylinders();
                FillManufacturingCountries();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int? VehcleId = null;
            int? CompanyId = null;
            int? FuelId = null;
            string MotorNo = null;
            string BodyNo = null;
            int? CC = null;
            int? CounterReadingStartSart = null;
            int? CounterReadingStartEnd = null;
            decimal? AverageFuelConsumptionStart = null;
            decimal? AverageFuelConsumptionEnd = null;
            int? Color = null;
            int? CylenderNo = null;
            int? ManufactureYearStart = null;
            int? ManufactureYearEnd = null;
            int? ManufacturingCountry = null;
            DateTime? StartOperationDateStart = null;
            DateTime? StartOperationDateEnd = null;
            DateTime? EndOperationDateStart = null;
            DateTime? EndOperationDateEnd = null;
            bool? IsActive = null;
            bool? IsInActive = null;


            if (ddl_VehcleId.SelectedItem != null)
            {
                VehcleId = int.Parse(ddl_VehcleId.SelectedItem.Value.ToString());
            }
            else
            {
                VehcleId = null;
            }
            if (ddl_CompanyId.SelectedItem != null)
            {
                CompanyId = int.Parse(ddl_CompanyId.SelectedItem.Value.ToString());
            }
            else
            {
                CompanyId = null;
            }
            if (ddl_FuelId.SelectedItem != null)
            {
                FuelId = int.Parse(ddl_FuelId.SelectedItem.Value.ToString());
            }
            else
            {
                FuelId = null;
            }
            if (txt_MotorNo.Text != string.Empty)
            {
                MotorNo = txt_MotorNo.Text;
            }
            else
            {
                MotorNo = null;
            }
            if (txt_BodyNo.Text != "")
            {
                BodyNo = txt_BodyNo.Text;
            }
            else
            {
                BodyNo = null;
            }
            if (ddl_CC.SelectedItem != null)
            {
                CC = int.Parse(ddl_CC.SelectedItem.Value.ToString());
            }
            else
            {
                CC = null;
            }
            if (txt_CounterReadingStartSart.Text != "")
            {
                CounterReadingStartSart = int.Parse(txt_CounterReadingStartSart.Text);
            }
            else
            {
                CounterReadingStartSart = null;
            }

            if (txt_CounterReadingStartEnd.Text != "")
            {
                CounterReadingStartEnd = int.Parse(txt_CounterReadingStartEnd.Text);
            }
            else
            {
                CounterReadingStartEnd = null;
            }
            if (txt_AverageFuelConsumptionStart.Text != "")
            {
                AverageFuelConsumptionStart = decimal.Parse(txt_AverageFuelConsumptionStart.Text);
            }
            else
            {
                AverageFuelConsumptionStart = null;
            }
            if (txt_AverageFuelConsumptionEnd.Text != "")
            {
                AverageFuelConsumptionEnd = int.Parse(txt_AverageFuelConsumptionEnd.Text);
            }
            else
            {
                AverageFuelConsumptionEnd = null;
            }
            if (ddl_ColorId.SelectedItem != null)
            {
                Color = int.Parse(ddl_ColorId.SelectedItem.Value.ToString());
            }
            else
            {
                Color = null;
            }
            if (ddl_CylinderId.SelectedItem != null)
            {
                CylenderNo = int.Parse(ddl_CylinderId.SelectedItem.Value.ToString());
            }
            else
            {
                CylenderNo = null;
            }
            if (txt_ManufactureYearStart.Text != "")
            {
                ManufactureYearStart = int.Parse(txt_ManufactureYearStart.Text);
            }
            else
            {
                ManufactureYearStart = null;
            }
            if (txt_ManufactureYearEnd.Text != "")
            {
                ManufactureYearEnd = int.Parse(txt_ManufactureYearEnd.Text);
            }
            else
            {
                ManufactureYearEnd = null;
            }
            if (ddl_ManufacturingCountryId.SelectedItem != null)
            {
                ManufacturingCountry = int.Parse(ddl_ManufacturingCountryId.SelectedItem.Value.ToString());
            }
            else
            {
                ManufacturingCountry = null;
            }
            if (txt_StartOperationDateStart.Text != "")
            {
                StartOperationDateStart = DateTime.Parse(txt_StartOperationDateStart.Text);
            }
            else
            {
                StartOperationDateStart = null;
            }
            if (txt_StartOperationDateEnd.Text != "")
            {
                StartOperationDateEnd = DateTime.Parse(txt_StartOperationDateEnd.Text);
            }
            else
            {
                StartOperationDateEnd = null;
            }
            if (txt_EndOperationDateStart.Text != "")
            {
                EndOperationDateStart = DateTime.Parse(txt_EndOperationDateStart.Text);
            }
            else
            {
                EndOperationDateStart = null;
            }
            if (txt_EndOperationDateEnd.Text != "")
            {
                EndOperationDateEnd = DateTime.Parse(txt_EndOperationDateEnd.Text);
            }
            else
            {
                EndOperationDateEnd = null;
            }
            if (chk_IsActive.Checked == true)
            {
                IsActive = true;
            }
            else
            {
                IsActive = null;
            }
            if (chk_IsInActive.Checked == true)
            {
                IsInActive = true;
            }
            else
            {
                IsInActive = null;
            }
            Dev_Vehcles2 RP = new Dev_Vehcles2(VehcleId, CompanyId, FuelId, MotorNo, BodyNo, CC, CounterReadingStartSart, CounterReadingStartEnd, AverageFuelConsumptionStart, AverageFuelConsumptionEnd, Color, CylenderNo, ManufactureYearStart, ManufactureYearEnd, ManufacturingCountry, StartOperationDateStart, StartOperationDateEnd, EndOperationDateStart, EndOperationDateEnd, IsActive, IsInActive, int.Parse(Session["UserId"].ToString()));
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Users.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.Vehcles2.GetHashCode());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["D"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["D"].ToString()))
                    {
                        Response.Redirect(@"..\Pages\Login.aspx", false);
                    }

                }
                else
                { Response.Redirect(@"..\Pages\Login.aspx", false); }
            }
            catch
            {
                Response.Redirect(@"..\Pages\Login.aspx");
            }
        }
        #endregion

    }
}
