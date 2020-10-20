using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusesWorkshop.DAL.Bus;
using System.Data;
using BusesWorkshop.Pages.PagesPermissions;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Reports
{
    public partial class Rpt_MaintenenceCost : System.Web.UI.Page
    {
        #region "Properties"
        WorkshopDataContext dc = new WorkshopDataContext();
        #endregion

        #region "Methods"
        private void FillPlans()
        {
            ddl_PlanName.DataSource = from p in dc.PeriodicalPlans select new { p.maintPlanId, p.PlanName };
            ddl_PlanName.TextField = "PlanName";
            ddl_PlanName.ValueField = "maintPlanId";
            ddl_PlanName.DataBind();
        }

        private void FillServices()
        {
            ddl_ServiceName.DataSource = from p in dc.ServicesSettings select new { p.ID, p.ServiceName };
            ddl_ServiceName.TextField = "ServiceName";
            ddl_ServiceName.ValueField = "ID";
            ddl_ServiceName.DataBind();
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
                DataTable dt = Users.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.Rpt_MaintenenceCost.GetHashCode());
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
                Dev_MaintenenceCost rpt = new Dev_MaintenenceCost(null, null, null, null, null, null, null, int.Parse(Session["UserId"].ToString()));
                FillVehcles();
                FillServices();
                FillPlans();
                Page.Title = "تقرير تكلفة الصيانة";
            }
        }


        protected void ddl_VehcleId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_VehcleId.SelectedItem != null)
            {
                var Query = (from p in dc.Vehcles where p.VehcleId == int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()) select new { p.ClassId }).SingleOrDefault();
                ddl_SpareName.DataSource = from p in dc.SpareParts where p.ClassId == int.Parse(Query.ClassId.ToString()) select new { p.SpareId, p.SpareName };
                ddl_SpareName.TextField = "SpareName";
                ddl_SpareName.ValueField = "SpareId";
                ddl_SpareName.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int? VehcleId = null;
            string PlanType = null;
            int? PlanId = null;
            int? ServiceId = null;
            int? SparId = null;
            DateTime? StartDate = null;
            DateTime? EndDate = null;

            if (ddl_VehcleId.SelectedItem != null)
            {
                VehcleId = int.Parse(ddl_VehcleId.SelectedItem.Value.ToString());
            }
            else
            {
                VehcleId = null;
            }

            if (ddl_MainType.SelectedItem != null)
            {
                PlanType = ddl_MainType.SelectedItem.Text;
            }
            else
            {
                PlanType = "null";
            }

            if (ddl_PlanName.SelectedItem != null)
            {
                PlanId = int.Parse(ddl_PlanName.SelectedItem.Value.ToString());
            }
            else
            {
                PlanId = null;
            }
            if (ddl_ServiceName.SelectedItem != null)
            {
                ServiceId = int.Parse(ddl_ServiceName.SelectedItem.Value.ToString());
            }
            else
            {
                ServiceId = null;
            }
            if (ddl_SpareName.SelectedItem != null)
            {
                SparId = int.Parse(ddl_SpareName.SelectedItem.Value.ToString());
            }
            else
            {
                SparId = null;
            }
            if (txt_FromDate.Text != "")
            {
                StartDate = DateTime.Parse(txt_FromDate.Text);
            }
            else
            {
                StartDate = null;
            }
            if (txt_ToDate.Text != "")
            {
                EndDate = DateTime.Parse(txt_ToDate.Text);
            }
            else
            {
                EndDate = null;
            }
            Dev_MaintenenceCost rpt = new Dev_MaintenenceCost(VehcleId, PlanType, PlanId, SparId, ServiceId, StartDate, EndDate, int.Parse(Session["UserId"].ToString()));
        }
        #endregion


    }
}
