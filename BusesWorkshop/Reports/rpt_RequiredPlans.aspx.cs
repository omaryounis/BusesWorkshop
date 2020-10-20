using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusesWorkshop.DAL.Bus;
using System.Data.SqlClient;
using System.Data;
using BusesWorkshop.Pages.PagesPermissions;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Reports
{
    public partial class rpt_RequiredPlans : System.Web.UI.Page
    {
        #region "Properties"
        WorkshopDataContext dc = new WorkshopDataContext();

        #endregion
        #region "Methods"
        private void FillDrivers()
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
            ddl_EmployeeId.DataSource = from p in dc.Employees where p.IsDriver == true && p.Company.UserCompanies.Any(x => x.UserID == id) select new { p.EmployeeId, p.EmployeeName };
            ddl_EmployeeId.TextField = "EmployeeName";
            ddl_EmployeeId.ValueField = "EmployeeId";
            ddl_EmployeeId.DataBind();
        }
        private void FillCompanies()
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


            ddl_CompanyId.DataSource = from p in dc.Companies where p.UserCompanies.Any(x => x.UserID == id) select new { p.ID, p.CompName };
            ddl_CompanyId.TextField = "CompName";
            ddl_CompanyId.ValueField = "ID";
            ddl_CompanyId.DataBind();
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Users.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.rpt_RequiredPlans.GetHashCode());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["D"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["D"].ToString()))
                    {
                        Response.Redirect(@"..\Pages\Login.aspx", false);
                    }

                }
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
            if (!IsPostBack)
            {
                FillCompanies();
                FillDrivers();
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
        }




        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int? empId = null;
            int? CoId = null;
            int? VecId = null;


            if (ddl_EmployeeId.SelectedItem != null)
            {

                empId = int.Parse(ddl_EmployeeId.SelectedItem.Value.ToString());
            }
            else
            {
                empId = null;
            }

            if (ddl_CompanyId.SelectedItem != null)
            {

                CoId = int.Parse(ddl_CompanyId.SelectedItem.Value.ToString());
            }
            else
            {
                CoId = null;
            }
            if (ddl_VehcleId.SelectedItem != null)
            {
                VecId = int.Parse(ddl_VehcleId.SelectedItem.Value.ToString());
            }
            else
            {
                VecId = null;
            }
            Dev_RequiredPlanVehcles rpt = new Dev_RequiredPlanVehcles(empId, CoId, VecId, int.Parse(Session["UserId"].ToString()));

        }
        #endregion

    }
}
