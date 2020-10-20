using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusesWorkshop.DAL.Bus;
using System.Data.SqlClient;
using System.Configuration;
using BusesWorkshop.Pages.PagesPermissions;
using System.Data;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Reports
{

    public partial class Dev_Vecles1 : System.Web.UI.Page
    {
        #region "Properties"
        WorkshopDataContext dc = new WorkshopDataContext();
        private int UserIDD
        {
            get
            {
                if (ViewState["_UserID"] != null)
                {
                    return Convert.ToInt32(ViewState["_UserID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_UserID", value);
            }
        }
        private int UserID
        {
            get
            {
                if (ViewState["_UserID"] != null)
                {
                    return Convert.ToInt32(ViewState["_UserID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_VUserID", value);
            }
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]

        #endregion
        #region "Methods"
        private void FillLicenseType()
        {
            ddl_LicenseType.DataSource = from p in dc.ConfigDetails where p.MasterId == 8 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_LicenseType.TextField = "ConfigDetailName";
            ddl_LicenseType.ValueField = "ConfigDetailId";
            ddl_LicenseType.DataBind();
        }
        private void FillDrivers()
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
            ddl_MainDriver.DataSource = from p in dc.Employees where p.IsDriver.Equals(1) && p.Company.UserCompanies.Any(x => x.UserID == id) select new { p.EmployeeName, p.EmployeeId };
            ddl_MainDriver.TextField = "EmployeeName";
            ddl_MainDriver.ValueField = "EmployeeId";
            ddl_MainDriver.DataBind();
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
        private void FillBrands()
        {
            ddl_ModelId.Items.Clear();
            ddl_ClassId.Items.Clear();
            ddl_BrandId.DataSource = from p in dc.Brands select new { p.BrandId, p.BrandName };
            ddl_BrandId.TextField = "BrandName";
            ddl_BrandId.ValueField = "BrandId";
            ddl_BrandId.DataBind();
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
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Users.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.rpt_Vecles1.GetHashCode());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["D"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["D"].ToString()))
                    {
                        Response.Redirect(@"..\Pages\Login.aspx", false);
                    }
                    //if (dt.Rows[0]["I"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["I"].ToString()))
                    //{

                    //    btnSave.Enabled = false;

                    //}
                    //if (dt.Rows[0]["U"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["U"].ToString()))
                    //{
                    //    GridViewUser.Columns[2].Visible = false;


                    //}
                    //if (dt.Rows[0]["R"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["R"].ToString()))
                    //{
                    //    GridViewUser.Columns[3].Visible = false;



                    //}
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

        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            permissions(dc);
            if (Session["UserID"] != null)
            {
                UserIDD = int.Parse(Session["UserID"].ToString());
            }
            else
            {
                Response.Redirect("../pages/login.aspx");
            }
            if (!IsPostBack)
            {
                VehclesInfo rp = new VehclesInfo(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, int.Parse(Session["UserId"].ToString()));
                FillVehcles();
                FillBrands();
                fill_Companies();
                FillDrivers();
                FillLicenseType();
            }
        }

        protected void ddl_BrandId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_ModelId.Items.Clear();
            ddl_ModelId.Text = string.Empty;
            ddl_ClassId.Items.Clear();
            ddl_ClassId.Text = string.Empty;
            if (ddl_BrandId.SelectedItem != null)
            {


                ddl_ModelId.DataSource = from p in dc.Models where p.BrandId == int.Parse(ddl_BrandId.SelectedItem.Value.ToString()) select new { p.ModelId, p.ModelName };
                ddl_ModelId.TextField = "ModelName";
                ddl_ModelId.ValueField = "ModelId";
                ddl_ModelId.DataBind();
            }
        }

        protected void ddl_ModelId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_ClassId.Items.Clear();
            if (ddl_ModelId.SelectedItem != null)
            {


                ddl_ClassId.DataSource = from p in dc.Classes where p.ModelId == int.Parse(ddl_ModelId.SelectedItem.Value.ToString()) select new { p.ClassId, p.ClassName };
                ddl_ClassId.TextField = "ClassName";
                ddl_ClassId.ValueField = "ClassId";
                ddl_ClassId.DataBind();

            }
        }

        public static List<string> SearchPSuperVisors(string prefixText, int count)
        {
            List<string> platNo = new List<string>();
            WorkshopDataContext ctx = new WorkshopDataContext();
            platNo = (from p in ctx.Vehcles where p.SuperVisorId.Contains(prefixText) select p.SuperVisorId).ToList();
            return platNo;
        }




        public static List<string> SearchLicenses(string prefixText, int count)
        {

            List<string> platNo = new List<string>();
            WorkshopDataContext ctx = new WorkshopDataContext();
            platNo = (from p in ctx.Vehcles where p.PlateNo.Contains(prefixText) select p.PlateNo).ToList();
            return platNo;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int? VehcleId = null;
            int? BrandId = null;
            int? ModelId = null;
            int? ClassId = null;
            int? CompanyId = null;
            int? DriverId = null;
            string SuperVisorId = null;
            int? CurrentCounterStart = null;
            int? CurrentCounterEnd = null;
            int? LicenseType = null;
            string LisnceNo = null;
            DateTime? LicenseExpiryDateStart = null;
            DateTime? LicenseExpiryDateEnd = null;
            DateTime? InspectionDateStart = null;
            DateTime? InspectionDateEnd = null;


            if (ddl_VehcleId.SelectedItem != null)
            {
                VehcleId = int.Parse(ddl_VehcleId.SelectedItem.Value.ToString());
            }
            else
            {
                VehcleId = null;
            }
            if (ddl_BrandId.SelectedItem != null)
            {
                BrandId = int.Parse(ddl_BrandId.SelectedItem.Value.ToString());
            }
            else
            {
                BrandId = null;
            }
            if (ddl_ModelId.SelectedItem != null)
            {
                ModelId = int.Parse(ddl_ModelId.SelectedItem.Value.ToString());
            }
            else
            {
                ModelId = null;
            }
            if (ddl_ClassId.SelectedItem != null)
            {
                ClassId = int.Parse(ddl_ClassId.SelectedItem.Value.ToString());
            }
            else
            {
                ClassId = null;
            }
            if (ddl_CompanyId.SelectedItem != null)
            {
                CompanyId = int.Parse(ddl_CompanyId.SelectedItem.Value.ToString());
            }
            else
            {
                CompanyId = null;
            }
            if (ddl_MainDriver.SelectedItem != null)
            {
                DriverId = int.Parse(ddl_MainDriver.SelectedItem.Value.ToString());
            }
            else
            {
                DriverId = null;
            }
            if (txt_SuperVisorId.Text != string.Empty)
            {
                SuperVisorId = txt_SuperVisorId.Text;
            }
            else
            {
                SuperVisorId = null;
            }
            if (txt_CurrentReadingStart.Text != string.Empty)
            {
                CurrentCounterStart = int.Parse(txt_CurrentReadingStart.Text);
            }
            else
            {
                CurrentCounterStart = null;
            }
            if (txt_CurrentReadingEnd.Text != string.Empty)
            {
                CurrentCounterEnd = int.Parse(txt_InspectioDateEnd.Text);
            }
            else
            {
                CurrentCounterEnd = null;
            }
            if (ddl_LicenseType.SelectedItem != null)
            {
                LicenseType = int.Parse(ddl_LicenseType.SelectedItem.Value.ToString());
            }
            else
            {
                LicenseType = null;
            }
            if (txt_LicenseNo.Text != string.Empty)
            {
                LisnceNo = txt_LicenseNo.Text;
            }
            else
            {
                LisnceNo = null;
            }
            if (txt_LicenseExpiryDateStart.Text != string.Empty)
            {
                LicenseExpiryDateStart = DateTime.Parse(txt_LicenseExpiryDateStart.Text);
            }
            else
            {
                LicenseExpiryDateStart = null;
            }
            if (txt_LicenseExpiryDateEnd.Text != string.Empty)
            {
                LicenseExpiryDateEnd = DateTime.Parse(txt_LicenseExpiryDateEnd.Text);
            }
            else
            {
                LicenseExpiryDateEnd = null;
            }
            if (txt_InspectioDateStart.Text != string.Empty)
            {
                InspectionDateStart = DateTime.Parse(txt_InspectioDateStart.Text);
            }
            else
            {
                InspectionDateStart = null;
            }
            if (txt_InspectioDateEnd.Text != string.Empty)
            {
                InspectionDateEnd = DateTime.Parse(txt_InspectioDateEnd.Text);
            }
            else
            {
                InspectionDateEnd = null;
            }
            VehclesInfo rp = new VehclesInfo(VehcleId, BrandId, ModelId, ClassId, CompanyId, DriverId, SuperVisorId, CurrentCounterStart, CurrentCounterEnd, LicenseType, LisnceNo, LicenseExpiryDateStart, LicenseExpiryDateEnd, InspectionDateStart, InspectionDateEnd, int.Parse(Session["UserId"].ToString()));
        }
        #endregion
    }
}







