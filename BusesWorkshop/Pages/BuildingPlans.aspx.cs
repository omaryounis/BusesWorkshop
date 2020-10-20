using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusesWorkshop.DAL.Bus;
using System.Data;
using DevExpress.Web;
using System.Data.SqlClient;
using BusesWorkshop.DAL;

namespace BusesWorkshop
{
    public partial class BuildingPlans : System.Web.UI.Page
    {
        #region "Properties"
      
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
        WorkshopDataContext dc = new WorkshopDataContext();
        DataTable dt;
        #endregion
        #region "Methods"
        private void FillGrid()
        {
            gvPlans.DataSource = from p in dc.BuildingPlans select new { p.PlanName, p.PlanId };
            gvPlans.DataBind();
        }
        private void FillJobs()
        {

            ddl_RequiredJob.DataSource = from p in dc.ConfigDetails where p.MasterId == 18 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_RequiredJob.TextField = "ConfigDetailName";
            ddl_RequiredJob.ValueField = "ConfigDetailId";
            ddl_RequiredJob.DataBind();
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.PeriodicalMaitenencePlan.GetHashCode());
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

        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            permissions(dc);

            Page.Title = "خطة الصيانة الدورية";
            if (!IsPostBack)
            {
                FillJobs();
                #region required job Table
                dt = new DataTable();
                DataColumn dc1 = new DataColumn("RequiredJob");
                DataColumn dc2 = new DataColumn("Description");

                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);


                if (dt.Rows.Count == 0) dt.Rows.Add(dt.NewRow());
                ViewState["RequiredJobs"] = dt;
                grd_WorksNeeded.DataSource = ViewState["RequiredJobs"];
                grd_WorksNeeded.DataBind();
                grd_WorksNeeded.Rows[0].Visible = false;

                #endregion
            }
            FillGrid();
        }

     

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            #region validation
            if (ddl_RequiredJob.SelectedItem == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل العمل";
                return;

            }
            for (int i = 0; i < grd_WorksNeeded.Rows.Count; i++)
            {
                ASPxComboBox ddl = grd_WorksNeeded.Rows[i].FindControl("ddl_RequiredJob") as ASPxComboBox;
                if (ddl.SelectedItem != null)
                {
                    if (ddl.SelectedItem.Value.ToString() == ddl_RequiredJob.SelectedItem.Value.ToString())
                    {
                        DivalerGrid.Attributes["class"] = "alert alert-danger text-right";
                        LBL_DivalerGrid.Text = "عفوا نفس العمل مقرر لنفس خطة الصيانة";
                        return;
                    }
                }

            }


            #endregion
            dt = (DataTable)ViewState["RequiredJobs"];
            DataRow dr1 = dt.NewRow();
            dr1[0] = ddl_RequiredJob.SelectedItem.Value;
            dr1[1] = txt_Description.Text;
            dt.Rows.Add(dr1);

            grd_WorksNeeded.DataSource = dt;
            grd_WorksNeeded.DataBind();
            ViewState["RequiredJobs"] = dt;
            grd_WorksNeeded.Rows[0].Visible = false;
            ddl_RequiredJob.Text = string.Empty;
            ddl_RequiredJob.Value = null;
            txt_Description.Text = string.Empty;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                return;
            }
            int result = 0;

            #region Validation
            ASPxComboBox ddl_RequiredJobin = (ASPxComboBox)grd_WorksNeeded.Rows[0].FindControl("ddl_RequiredJob");
            if (grd_WorksNeeded.Rows.Count <= 1 && ddl_RequiredJobin.SelectedItem == null )
            {

                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا لا يمكن حفظ خطة صيانة بدون اعمال لها";
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
                BuildingPlan _BuildingPlan = dc.BuildingPlans.Single(x => x.PlanId == PlanID);
                _BuildingPlan.PlanName = txt_PlanName.Text;
                _BuildingPlan.EveryWhileMon =int.Parse(txt_EveryWhilePerMonth.Text);
                _BuildingPlan.PlanDescription = txt_PlanDescription.Text;
                dc.SubmitChanges();
                result = _BuildingPlan.PlanId;
                #endregion



                #region delete works
                var query = from c in dc.BuildingPlanDetails where c.PlanId.Equals(PlanID) select c;
                foreach (var details in query)
                {
                    dc.BuildingPlanDetails.DeleteOnSubmit(details);
                }
                dc.SubmitChanges();

                #endregion
                #region Save Works
                for (int i = 0; i < grd_WorksNeeded.Rows.Count; i++)
                {

                    ASPxComboBox ddl_RequiredJob = (ASPxComboBox)grd_WorksNeeded.Rows[i].FindControl("ddl_RequiredJob");
                    Label lbl_Description = (Label)grd_WorksNeeded.Rows[i].FindControl("lbl_Description");
                    if (ddl_RequiredJob.SelectedItem != null)
                    {
                        BuildingPlanDetail _BuildingPlanDetail = new BuildingPlanDetail();

                        _BuildingPlanDetail.PlanId = result;

                        _BuildingPlanDetail.WorkId = int.Parse(ddl_RequiredJob.SelectedItem.Value.ToString());
                        _BuildingPlanDetail.Description = lbl_Description.Text;
                        dc.BuildingPlanDetails.InsertOnSubmit(_BuildingPlanDetail);
                        dc.SubmitChanges();
                    }
                }
                #endregion

            }

            else
            {
                #region save

                if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                    return;
                }

                BuildingPlan _BuildingPlan = new BuildingPlan();
                _BuildingPlan.PlanName = txt_PlanName.Text;
                _BuildingPlan.EveryWhileMon = int.Parse(txt_EveryWhilePerMonth.Text);
                _BuildingPlan.PlanDescription = txt_PlanDescription.Text;
                dc.BuildingPlans.InsertOnSubmit(_BuildingPlan);
                dc.SubmitChanges();
                result = _BuildingPlan.PlanId;




                for (int i = 0; i < grd_WorksNeeded.Rows.Count; i++)
                {

                    ASPxComboBox ddl_RequiredJob = (ASPxComboBox)grd_WorksNeeded.Rows[i].FindControl("ddl_RequiredJob");
                    Label lbl_Description = (Label)grd_WorksNeeded.Rows[i].FindControl("lbl_Description");
                    if (ddl_RequiredJob.SelectedItem != null)
                    {
                        BuildingPlanDetail _BuildingPlanDetail = new BuildingPlanDetail();

                        _BuildingPlanDetail.PlanId = result;

                        _BuildingPlanDetail.WorkId = int.Parse(ddl_RequiredJob.SelectedItem.Value.ToString());
                        _BuildingPlanDetail.Description = lbl_Description.Text;
                        dc.BuildingPlanDetails.InsertOnSubmit(_BuildingPlanDetail);
                        dc.SubmitChanges();
                    }
                }
                #endregion
            }

            divMsg.Attributes["class"] = "alert alert-success text-right";
            lblResult.Text = "تم الحفظ بنجاح";

            MyClasses.ClearControls(Page);

            dt = (DataTable)ViewState["RequiredJobs"];
            dt.Rows.Clear();
            dt.Rows.Add(dt.NewRow());
            grd_WorksNeeded.DataSource = dt;
            grd_WorksNeeded.DataBind();
            grd_WorksNeeded.Rows[0].Visible = false;
            ViewState["RequiredJobs"] = dt;
            FillGrid();
        }
   

        protected void grd_WorksNeeded_DataBound(object sender, EventArgs e)
        {
          
        }

        protected void grd_WorksNeeded_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ASPxComboBox ddl_RequiredJob = (e.Row.FindControl("ddl_RequiredJob") as ASPxComboBox);
                ddl_RequiredJob.DataSource = from p in dc.ConfigDetails where p.MasterId== 18 select new { p.ConfigDetailId, p.ConfigDetailName };
                ddl_RequiredJob.TextField = "ConfigDetailName";
                ddl_RequiredJob.ValueField = "ConfigDetailId";
                ddl_RequiredJob.DataBind();

                DataRowView dr = e.Row.DataItem as DataRowView;
                ddl_RequiredJob.Value = dr["RequiredJob"].ToString();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string item = e.Row.Cells[0].Text;
                    foreach (Button button in e.Row.Cells[2].Controls.OfType<Button>())
                    {
                        if (button.CommandName == "Delete")
                        {
                            button.Attributes["onclick"] = "if(!confirm('هل تريد الحذف " + item + "?')){ return false; };";
                        }
                    }
                }
            }
        }

        protected void grd_WorksNeeded_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = ViewState["RequiredJobs"] as DataTable;
            dt.Rows[index].Delete();
            ViewState["RequiredJobs"] = dt;
            grd_WorksNeeded.DataSource = ViewState["RequiredJobs"];
            grd_WorksNeeded.DataBind();
            ASPxComboBox ddl_RequiredJobin = (ASPxComboBox)grd_WorksNeeded.Rows[0].FindControl("ddl_RequiredJob");
            if (ddl_RequiredJobin.SelectedItem == null)
            {
                grd_WorksNeeded.Rows[0].Visible = false;
            }
        }

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;

            PlanID = Convert.ToInt32(gvPlans.DataKeys[row.RowIndex].Value);

            var query = (from p in dc.BuildingPlans where p.PlanId.Equals(PlanID) select new { p.PlanName, p.PlanDescription, p.EveryWhileMon }).SingleOrDefault();
            txt_PlanName.Text = query.PlanName;
            txt_EveryWhilePerMonth.Text = query.EveryWhileMon.ToString();
            txt_PlanDescription.Text = query.PlanDescription;



            #region FillPlanGrid
            var queryDet = from p in dc.BuildingPlanDetails where p.PlanId== PlanID select new { p.PlanId , RequiredJob = p.WorkId, p.Description };
            dt = queryDet.CopyToDataTable();


            if (dt.Rows.Count > 0)
            {
                ViewState["RequiredJobs"] = dt;
                grd_WorksNeeded.DataSource = ViewState["RequiredJobs"];
                grd_WorksNeeded.DataBind();
            }
            else
            {
                dt = (DataTable)ViewState["RequiredJobs"];
                dt.Rows.Clear();
                dt.Rows.Add(dt.NewRow());
                grd_WorksNeeded.DataSource = dt;
                grd_WorksNeeded.DataBind();
                grd_WorksNeeded.Rows[0].Visible = false;
                ViewState["RequiredJobs"] = dt;
            }



        


            #endregion
           

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

            PlanID = Convert.ToInt32(gvPlans.DataKeys[row.RowIndex].Value);


            BuildingPlan _BuildingPlan = dc.BuildingPlans.Single(x => x.PlanId == PlanID);
            dc.BuildingPlans.DeleteOnSubmit(_BuildingPlan);
            try
            {
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
