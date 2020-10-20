using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusesWorkshop.DAL.Bus;
using System.Data;
using System.Data.SqlClient;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Pages
{
    public partial class PeriodicalPlanToBuilding : System.Web.UI.Page
    {
        #region "Properties"
        WorkshopDataContext dc = new WorkshopDataContext();
        private int PlanID
        {
            get
            {
                if (ViewState["PlanID"] != null)
                {
                    return Convert.ToInt32(ViewState["PlanID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("PlanID", value);
            }
        }

        #endregion

        #region "Methods"
        private void FillPlans()
        {
            ddl_PlanId.DataSource = from p in dc.BuildingPlans select new { p.PlanId, p.PlanName };
            ddl_PlanId.TextField = "PlanName";
            ddl_PlanId.ValueField = "PlanId";
            ddl_PlanId.DataBind();
        }

        private void FillBuildings()
        {
            ddl_BuildingId.DataSource = from p in dc.Buidings where p.Company.UserCompanies.Any(x => x.UserID == int.Parse(Session["UserId"].ToString())) select new { p.BuildingId, p.BuildingName };
            ddl_BuildingId.TextField = "BuildingName";
            ddl_BuildingId.ValueField = "BuildingId";
            ddl_BuildingId.DataBind();
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.rpt_MaintRequests.GetHashCode());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["D"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["D"].ToString()))
                    {
                        Response.Redirect(@"..\Pages\Login.aspx", false);
                    }
                    if (dt.Rows[0]["R"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["R"].ToString()))
                    {
                        ViewState["AllowDelete"] = 0;
                    }
                    else
                    {
                        ViewState["AllowDelete"] = 0;
                    }

                    if (dt.Rows[0]["U"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["U"].ToString()))
                    {
                        ViewState["AllowUpDate"] = 0;
                    }
                    else
                    {
                        ViewState["AllowUpDate"] = 1;
                    }
                    if (dt.Rows[0]["I"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["I"].ToString()))
                    {
                        ViewState["AllowInsert"] = 0;
                    }
                    else
                    {
                        ViewState["AllowInsert"] = 1;
                    }
                }
                else
                {
                    Response.Redirect(@"..\Pages\Login.aspx");
                }
            }
            catch
            {
                Response.Redirect(@"..\Pages\Login.aspx");
            }
        }
        private void FillGrid()
        {
            if (ddl_BuildingId.SelectedItem != null)
            {
                gvPlanToBuilding.DataSource = (from p in dc.PeriodicalPlanToBuildings select new { p.PeriodicalPlanId, p.Buiding.BuildingName, p.BuildingPlan.PlanName }).ToList();
                gvPlanToBuilding.DataBind();
            }
        }
        #endregion

        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            permissions(dc);

            Page.Title = "اسناد خطة صيانة الى مبنى";

            if (!IsPostBack)
            {
                FillBuildings();
                FillPlans();

            }
        }
        protected void ddl_PlanId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_PlanId.SelectedItem != null)
            {
                var query = (from p in dc.BuildingPlans where p.PlanId == int.Parse(ddl_PlanId.SelectedItem.Value.ToString()) select new { p.EveryWhileMon }).SingleOrDefault();
                txt_EveryKm.Text = query.EveryWhileMon.ToString();
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            int result = 0;
            #region Validation


            if (ddl_PlanId.SelectedItem == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "ادخل الخطة";
                return;
            }



            if (ddl_BuildingId.SelectedItem == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "ادخل المبنى";
                return;
            }



            #endregion
            if (PlanID > 0)
            {
                #region update
                if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية التعديل";
                    return;
                }

                DAL.Bus.PeriodicalPlanToBuilding _PeriodicalPlanToBuilding = new DAL.Bus.PeriodicalPlanToBuilding();
                _PeriodicalPlanToBuilding.PlanId = int.Parse(ddl_PlanId.SelectedItem.Value.ToString());
                _PeriodicalPlanToBuilding.BuildingId = int.Parse(ddl_BuildingId.SelectedItem.Value.ToString());
                _PeriodicalPlanToBuilding.NextPlanDate = DateTime.Parse(txt_NextPlaneDate.Text);

                dc.SubmitChanges();

                result = _PeriodicalPlanToBuilding.PeriodicalPlanId;


                #endregion
            }
            else
            {
                #region Save
                DAL.Bus.PeriodicalPlanToBuilding _PeriodicalPlanToBuilding = new DAL.Bus.PeriodicalPlanToBuilding();
                _PeriodicalPlanToBuilding.PlanId = int.Parse(ddl_PlanId.SelectedItem.Value.ToString());
                _PeriodicalPlanToBuilding.BuildingId = int.Parse(ddl_BuildingId.SelectedItem.Value.ToString());
                _PeriodicalPlanToBuilding.NextPlanDate = DateTime.Parse(txt_NextPlaneDate.Text);
                dc.PeriodicalPlanToBuildings.InsertOnSubmit(_PeriodicalPlanToBuilding);
                dc.SubmitChanges();
                result = _PeriodicalPlanToBuilding.PeriodicalPlanId;
                #endregion

            }
            if (result > 0)
            {
                divMsg.Attributes["class"] = "alert alert-success text-right";
                lblResult.Text = "تم الحفظ بنجاح";

            }
            else
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "حدث خطأ أثنا الحفظ";
                // divMsg. = "alert alert-danger text-right"; 
            }
            FillGrid();
            MyClasses.ClearControls(Page);
        }

        protected void ddl_BuildingId_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;

            PlanID = Convert.ToInt32(gvPlanToBuilding.DataKeys[row.RowIndex].Value);

            var query = (from p in dc.PeriodicalPlanToBuildings where p.PeriodicalPlanId.Equals(PlanID) select new { p.NextPlanDate, p.PlanId, p.BuildingId }).SingleOrDefault();

            ddl_BuildingId.Value = query.BuildingId.ToString();
            ddl_PlanId.Value = query.PlanId.ToString();
            txt_NextPlaneDate.Text = query.NextPlanDate.ToShortDateString();
            DateTime temp;
            if (DateTime.TryParse(query.NextPlanDate.ToString(), out temp))
            {

                txt_NextPlaneDate.Text = DateTime.Parse(query.NextPlanDate.ToString()).ToShortDateString();
            }
            else
            {
                txt_NextPlaneDate.Text = "";
            }


        }
        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            if (int.Parse(ViewState["AllowDelete"].ToString()) == 0 || ViewState["AllowDelete"] == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ليس لديك صلاحية الحذف";
                return;
            }
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;
            var query = from p in dc.PeriodicalPlanToBuildings where p.PeriodicalPlanId.Equals(Convert.ToInt32(gvPlanToBuilding.DataKeys[row.RowIndex].Value)) select p;
            try
            {
                foreach (var item in query)
                {
                    dc.PeriodicalPlanToBuildings.DeleteOnSubmit(item);
                }
                dc.SubmitChanges();
            }
            catch (SqlException ex)
            {

                if (ex.Errors[0].Number == 547)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفو السجل المراد حذفة مرتبط بمدخلات اخري";
                    return;
                }


            }


            FillGrid();
        }
        #endregion
    }
}
