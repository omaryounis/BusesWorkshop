using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BusesWorkshop.DAL.Bus;
using DevExpress.Web;
using System.Data.SqlClient;
using BusesWorkshop.DAL;

namespace BusesWorkshop
{
    public partial class PeriodicPlanToVehcle : System.Web.UI.Page
    {
        #region "Properties"
        WorkshopDataContext dc = new WorkshopDataContext();
        private int PeriodicalPlanToVehcleID
        {
            get
            {
                if (ViewState["_PeriodicalPlanToVehcleID"] != null)
                {
                    return Convert.ToInt32(ViewState["_PeriodicalPlanToVehcleID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_PeriodicalPlanToVehcleID", value);
            }
        }

        #endregion
        #region "Methods"
        private void FillMaintPlan()
        {
            ddl_maintPlanId.DataSource = from p in dc.PeriodicalPlans select new { p.maintPlanId, p.PlanName };
            ddl_maintPlanId.TextField = "PlanName";
            ddl_maintPlanId.ValueField = "maintPlanId";
            ddl_maintPlanId.DataBind();
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
                Response.Redirect("Login.aspx", false);
            }
            ddl_VehcleId.DataSource = from p in dc.Vehcles where p.Company.UserCompanies.Any(x => x.UserID == id) && p.Active == true select new { p.VehcleId, p.PlateNo };
            ddl_VehcleId.TextField = "PlateNo";
            ddl_VehcleId.ValueField = "VehcleId";
            ddl_VehcleId.DataBind();
        }
        private void FillGridWithvehclesPlans()
        {
            try
            {
                if (ddl_VehcleId.SelectedItem.Value != null)
                    gvPlanToVehcles.DataSource = from p in dc.PeriodicalPlanToVehcles where p.VehcleId == int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()) select new { p.PeriodicalPlanToVehcleId, p.PeriodicalPlan.PlanName, p.Vehcle.PlateNo };
                gvPlanToVehcles.DataBind();
            }
            catch { }
        }
        private void EmptyControls()
        {
            ddl_maintPlanId.Value = null;
            ddl_maintPlanId.Text = string.Empty;
            ddl_VehcleId.Value = null;
            ddl_VehcleId.Text = string.Empty;
            txt_EveryKm.Text = string.Empty;
            txt_EveryWhile.Text = string.Empty;
            txt_NextPlanCounter.Text = string.Empty;
            txt_NextPlaneDate.Text = string.Empty;
            txt_Notes.Text = string.Empty;
            PeriodicalPlanToVehcleID = 0;
            chk_IsActive.Checked = false;
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.PeriodicPlanToVehcle.GetHashCode());
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

        #endregion

        #region  "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            permissions(dc);
            if (!IsPostBack)
            {
                FillVehcles();
                FillMaintPlan();

            }

        }
        protected void ASPxComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddl_maintPlanId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var Query = (from p in dc.PeriodicalPlans where p.maintPlanId == int.Parse(ddl_maintPlanId.SelectedItem.Value.ToString()) select new { p.EveryKm, p.EveryWhilePerMonth }).SingleOrDefault();
                txt_EveryKm.Text = Query.EveryKm.ToString();
                txt_EveryWhile.Text = Query.EveryWhilePerMonth.ToString();
            }
            catch
            {


            }
            //txt_EveryKm.Text = Query.
        }
        protected void ddl_VehcleId_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGridWithvehclesPlans();
            if (ddl_maintPlanId.SelectedItem != null && ddl_VehcleId.SelectedItem != null)
            {
                var queryPlanKk = (from p in dc.PeriodicalPlans where p.maintPlanId == int.Parse(ddl_maintPlanId.SelectedItem.Value.ToString()) select new { p.EveryKm }).SingleOrDefault();
                var queryCurrent = (from p in dc.Vehcles where p.VehcleId == int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()) select new { p.CounterReadingStart, p.CurrentReading }).SingleOrDefault();

                int planKM = int.Parse(queryPlanKk.EveryKm.ToString());
                int currCounter = 0;
                int NextCounter = 0;
                if (int.Parse(string.IsNullOrEmpty(queryCurrent.CurrentReading.ToString()) ? "0" : queryCurrent.CurrentReading.ToString()) == 0)
                {
                    currCounter = int.Parse(string.IsNullOrEmpty(queryCurrent.CounterReadingStart.ToString()) ? "0" : queryCurrent.CounterReadingStart.ToString());
                }
                else
                {
                    currCounter = int.Parse(string.IsNullOrEmpty(queryCurrent.CurrentReading.ToString()) ? "0" : queryCurrent.CurrentReading.ToString());

                }
                NextCounter = planKM + currCounter;

                txt_NextPlanCounter.Text = NextCounter.ToString();


            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            int result = 0;
            #region Validation

            try
            {
                if (ddl_VehcleId.SelectedItem.Value == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "ادخل رقم اللوحة";
                    return;
                }
            }
            catch
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "ادخل رقم اللوحة";
                return;

            }
            try
            {
                if (ddl_maintPlanId.SelectedItem.Value == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "ادخل اسم اللوحة";
                    return;
                }
            }
            catch
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "ادخل اسم اللوحة";
                return;

            }
            if (txt_NextPlanCounter.Text == "" && txt_NextPlaneDate.Text == "")
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا يجب ادخال اما قراءة عداد او تاريخ دورة الصيانة القادم";
                return;
            }
            #endregion


            if (PeriodicalPlanToVehcleID > 0)
            {
                #region update
                if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية التعديل";
                    return;
                }
                PeriodicalPlanToVehcle _PeriodicalPlanToVehcle = dc.PeriodicalPlanToVehcles.Single(pertovec => pertovec.PeriodicalPlanToVehcleId == PeriodicalPlanToVehcleID);
                _PeriodicalPlanToVehcle.maintPlanId = int.Parse(ddl_maintPlanId.SelectedItem.Value.ToString());
                _PeriodicalPlanToVehcle.VehcleId = int.Parse(ddl_VehcleId.SelectedItem.Value.ToString());
                _PeriodicalPlanToVehcle.NextPlanCounter = int.Parse(string.IsNullOrEmpty(txt_NextPlanCounter.Text) ? "0" : txt_NextPlanCounter.Text);
                DateTime temp;
                if (DateTime.TryParse(txt_NextPlaneDate.Text, out temp))
                {
                    _PeriodicalPlanToVehcle.NextPLanDate = DateTime.Parse(txt_NextPlaneDate.Text);
                }
                else
                {
                    _PeriodicalPlanToVehcle.NextPLanDate = null;
                }
                _PeriodicalPlanToVehcle.Notes = txt_Notes.Text;
                _PeriodicalPlanToVehcle.IsActive = bool.Parse(chk_IsActive.Checked.ToString());

                dc.SubmitChanges();
                result = _PeriodicalPlanToVehcle.PeriodicalPlanToVehcleId;
                #endregion
            }
            else
            {
                #region validationForSave
                var query = from p in dc.PeriodicalPlanToVehcles where p.VehcleId == int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()) && p.maintPlanId == int.Parse(ddl_maintPlanId.SelectedItem.Value.ToString()) select new { p };
                if (query.Any())
                {

                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا خطة الصيانة موجودة مسبقا على نفس السيارة";
                    return;

                }

                #endregion


                #region Save

                if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                    return;
                }
                PeriodicalPlanToVehcle _PeriodicalPlanToVehcle = new PeriodicalPlanToVehcle();
                _PeriodicalPlanToVehcle.maintPlanId = int.Parse(ddl_maintPlanId.SelectedItem.Value.ToString());
                _PeriodicalPlanToVehcle.VehcleId = int.Parse(ddl_VehcleId.SelectedItem.Value.ToString());
                _PeriodicalPlanToVehcle.NextPlanCounter = int.Parse(string.IsNullOrEmpty(txt_NextPlanCounter.Text) ? "0" : txt_NextPlanCounter.Text);
                DateTime temp;
                if (DateTime.TryParse(txt_NextPlaneDate.Text, out temp))
                {
                    _PeriodicalPlanToVehcle.NextPLanDate = DateTime.Parse(txt_NextPlaneDate.Text);
                }
                else
                {
                    _PeriodicalPlanToVehcle.NextPLanDate = null;
                }
                _PeriodicalPlanToVehcle.Notes = txt_Notes.Text;
                _PeriodicalPlanToVehcle.IsActive = bool.Parse(chk_IsActive.Checked.ToString());
                dc.PeriodicalPlanToVehcles.InsertOnSubmit(_PeriodicalPlanToVehcle);
                dc.SubmitChanges();
                result = _PeriodicalPlanToVehcle.PeriodicalPlanToVehcleId;
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
            FillGridWithvehclesPlans();
            EmptyControls();
        }


        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;

            PeriodicalPlanToVehcleID = Convert.ToInt32(gvPlanToVehcles.DataKeys[row.RowIndex].Value);

            var query = (from p in dc.PeriodicalPlanToVehcles where p.PeriodicalPlanToVehcleId.Equals(PeriodicalPlanToVehcleID) select new { p.VehcleId, p.maintPlanId, p.NextPlanCounter, p.NextPLanDate, p.PeriodicalPlan.EveryKm, p.PeriodicalPlan.EveryWhilePerMonth, p.Notes, p.IsActive }).SingleOrDefault();

            ddl_maintPlanId.Value = query.maintPlanId.ToString();
            txt_EveryKm.Text = query.EveryKm.ToString();
            txt_EveryWhile.Text = query.EveryWhilePerMonth.ToString();
            ddl_VehcleId.Value = query.VehcleId.ToString();
            txt_NextPlanCounter.Text = query.NextPlanCounter.ToString();
            DateTime temp;
            if (DateTime.TryParse(query.NextPLanDate.ToString(), out temp))
            {

                txt_NextPlaneDate.Text = DateTime.Parse(query.NextPLanDate.ToString()).ToShortDateString();
            }
            else
            {
                txt_NextPlaneDate.Text = "";
            }
            txt_Notes.Text = query.Notes;
            chk_IsActive.Checked = query.IsActive;

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
            var query = from p in dc.PeriodicalPlanToVehcles where p.PeriodicalPlanToVehcleId.Equals(Convert.ToInt32(gvPlanToVehcles.DataKeys[row.RowIndex].Value)) select p;
            try
            {
                foreach (var item in query)
                {
                    dc.PeriodicalPlanToVehcles.DeleteOnSubmit(item);
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


            FillGridWithvehclesPlans();
        }
        #endregion

    }
}
