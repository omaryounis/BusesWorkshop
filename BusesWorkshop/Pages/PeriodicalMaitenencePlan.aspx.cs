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

namespace BusesWorkshop.Pages
{
    public partial class PeriodicalMaitenencePlan : System.Web.UI.Page
    {
        private int PeriodicalPlanID
        {
            get
            {
                if (ViewState["_PeriodicalPlanID"] != null)
                {
                    return Convert.ToInt32(ViewState["_PeriodicalPlanID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_PeriodicalPlanID", value);
            }
        }
        WorkshopDataContext dc = new WorkshopDataContext();
        DataTable dt;
        DataTable dtSpares;
        protected void Page_Load(object sender, EventArgs e)
        {
                permissions(dc );

            Page.Title = "خطة الصيانة الدورية";
            if (!IsPostBack)
            {
                #region required job Table
                dt = new DataTable();
                DataColumn dc1 = new DataColumn("RequiredJob" , typeof(int));
                DataColumn dc2 = new DataColumn("Description",typeof(string));

                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);


                if (dt.Rows.Count == 0) dt.Rows.Add(dt.NewRow());
                ViewState["RequiredJobs"] = dt;
                grd_WorksNeeded.DataSource = ViewState["RequiredJobs"];
                grd_WorksNeeded.DataBind();
                grd_WorksNeeded.Rows[0].Visible = false;

                #endregion
                #region SparParts Table
                dtSpares = new DataTable();
                DataColumn dcSpar1 = new DataColumn("SpareMainId");
                DataColumn dcSpar2 = new DataColumn("SpareId");
                DataColumn dcSpar3 = new DataColumn("SparCount");
                DataColumn dcSpar4 = new DataColumn("Notes");
                dtSpares.Columns.Add(dcSpar1);
                dtSpares.Columns.Add(dcSpar2);
                dtSpares.Columns.Add(dcSpar3);
                dtSpares.Columns.Add(dcSpar4);


                if (dtSpares.Rows.Count == 0) dtSpares.Rows.Add(dtSpares.NewRow());
                ViewState["Spares"] = dtSpares;
                grd_SparParts.DataSource = ViewState["Spares"];
                grd_SparParts.DataBind();
                grd_SparParts.Rows[0].Visible = false;

                #endregion

                FillServices();
            }
            FillGridPlans();
        }

        private void FillGridPlans()
        {
            gvPlans.DataSource = from p in dc.PeriodicalPlans select new { p.maintPlanId, p.PlanName };
            gvPlans.DataBind();
        }

        private void FillServices()
        {
            ddl_RequiredJob.DataSource = from p in dc.ServicesSettings select new { p.ID, p.ServiceName };
            ddl_RequiredJob.TextField = "ServiceName";
            ddl_RequiredJob.ValueField = "ID";
            ddl_RequiredJob.DataBind();
        }

        protected void btnSaveToGrid_Click(object sender, EventArgs e)
        {
            #region validation
            if (ddl_RequiredJob.SelectedItem == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل العمل";
                return;
            
            }
            for (int i = 0; i < grd_WorksNeeded.Rows.Count ; i++)
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
            dr1[0] = ddl_RequiredJob.SelectedItem.Value ;
            dr1[1] = txt_Description.Text;
            dt.Rows.Add(dr1);

            grd_WorksNeeded.DataSource = dt;
            grd_WorksNeeded.DataBind();
            ViewState["RequiredJobs"] = dt;
            if (dt.Rows[1]["RequiredJob"].ToString() == "")
            { 
            grd_WorksNeeded.Rows[0].Visible = false;
            }
            
            ddl_RequiredJob.Text = string.Empty;
            ddl_RequiredJob.Value  = null ;
            txt_Description.Text = string.Empty;
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
           
            if (txt_EveryWhilePerMonth.Text == "" && txt_EveryKm.Text == "")
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا على الاقل يجب ادخال عدد الكيلو مترات او المدة الزمنية المطلوبة لدورة الصيانة";
                return;
            }
           ASPxComboBox ddl_RequiredJobin = (ASPxComboBox)grd_WorksNeeded.Rows[0].FindControl("ddl_RequiredJob");
           if (grd_WorksNeeded.Rows.Count <= 1 && ddl_RequiredJobin.SelectedItem == null )
            {

                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا لا يمكن حفظ خطة صيانة بدون اعمال لها";
                return;

            }
            #endregion
            if (PeriodicalPlanID > 0)
            {
                #region Save 
                if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية التعديل";
                    return;
                }
                PeriodicalPlan _PeriodicalPlan = dc.PeriodicalPlans.Single(x => x.maintPlanId == PeriodicalPlanID );

                _PeriodicalPlan.PlanName = txt_PlanName.Text;

                _PeriodicalPlan.EveryKm = int.Parse(string.IsNullOrEmpty(txt_EveryKm.Text) ? "0" : txt_EveryKm.Text);


                _PeriodicalPlan.EveryWhilePerMonth = int.Parse(string.IsNullOrEmpty(txt_EveryWhilePerMonth.Text) ? "0" : txt_EveryWhilePerMonth.Text);
                DateTime temp;

                _PeriodicalPlan.RequiredSpareParts = txt_RequiredSpareParts.Text;
                _PeriodicalPlan.PlanDescription = txt_PlanDescription.Text;
                //_PeriodicalPlan.EveryWhilePerMonth = 
                //_PeriodicalPlan.
               
                dc.SubmitChanges();
                PeriodicalPlanID = _PeriodicalPlan.maintPlanId;
                #region delete works
                var query = from c in dc.PeriodicPlanDetails where c.maintPlanId.Equals(PeriodicalPlanID) select c;
                foreach (var details in query)
                {
                    dc.PeriodicPlanDetails.DeleteOnSubmit(details);
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
                        PeriodicPlanDetail _PeriodicPlanDetail = new PeriodicPlanDetail();
                        _PeriodicPlanDetail.maintPlanId = PeriodicalPlanID;
                        _PeriodicPlanDetail.RequiredJob = int.Parse(ddl_RequiredJob.SelectedItem.Value.ToString());
                        _PeriodicPlanDetail.Description = lbl_Description.Text;
                        dc.PeriodicPlanDetails.InsertOnSubmit(_PeriodicPlanDetail);
                        dc.SubmitChanges();
                    }
                }
                #endregion


                #endregion
            }
            else
            {
            #region save
            PeriodicalPlan _PeriodicalPlan = new PeriodicalPlan();

            _PeriodicalPlan.PlanName = txt_PlanName.Text;
          
            _PeriodicalPlan.EveryKm = int.Parse(string.IsNullOrEmpty(txt_EveryKm.Text) ? "0" : txt_EveryKm.Text);
            

            _PeriodicalPlan.EveryWhilePerMonth = int.Parse(string.IsNullOrEmpty(txt_EveryWhilePerMonth.Text) ? "0" : txt_EveryWhilePerMonth.Text);
            DateTime temp;
            
            _PeriodicalPlan.RequiredSpareParts = txt_RequiredSpareParts.Text;
            _PeriodicalPlan.PlanDescription = txt_PlanDescription.Text;
            //_PeriodicalPlan.EveryWhilePerMonth = 
            //_PeriodicalPlan.
            dc.PeriodicalPlans.InsertOnSubmit(_PeriodicalPlan);
            dc.SubmitChanges();
            PeriodicalPlanID = _PeriodicalPlan.maintPlanId;

            #region Save Works
            for (int i = 0; i < grd_WorksNeeded.Rows.Count ; i++)
            {

                ASPxComboBox ddl_RequiredJob = (ASPxComboBox)grd_WorksNeeded.Rows[i].FindControl("ddl_RequiredJob");
                Label lbl_Description = (Label)grd_WorksNeeded.Rows[i].FindControl("lbl_Description");
                if (ddl_RequiredJob.SelectedItem != null )
                {
                     PeriodicPlanDetail _PeriodicPlanDetail = new PeriodicPlanDetail();
                _PeriodicPlanDetail.maintPlanId = PeriodicalPlanID;
                _PeriodicPlanDetail.RequiredJob =int.Parse  (ddl_RequiredJob.SelectedItem.Value .ToString());
                _PeriodicPlanDetail.Description = lbl_Description.Text;
                dc.PeriodicPlanDetails.InsertOnSubmit(_PeriodicPlanDetail);
                dc.SubmitChanges();
                }
           
            }
          

            #endregion

            #region Insert Spares

            for (int i = 0; i < grd_SparParts.Rows.Count; i++)
            {
                ASPxComboBox ddl_SpareId = (ASPxComboBox)grd_SparParts.Rows[i].FindControl("ddl_SpareId");
               
                TextBox txt_SparCount = (TextBox)grd_SparParts.Rows[i].FindControl("txt_SparCount");
               
                TextBox txt_Notes = (TextBox)grd_SparParts.Rows[i].FindControl("txt_Notes");


                if (ddl_SpareId.Value == null && txt_SparCount.Text  == "") goto fo;
                using (WorkshopDataContext ctx = new WorkshopDataContext())
                {
                    PeriodicalPlanSpar _PeriodicalPlanSpar = new PeriodicalPlanSpar();
                    _PeriodicalPlanSpar.maintPlanId = PeriodicalPlanID;
                    _PeriodicalPlanSpar.SpareId = int.Parse(ddl_SpareId.Value.ToString());

                    _PeriodicalPlanSpar.SparCount = decimal.Parse(string.IsNullOrEmpty(txt_SparCount.Text) ? "0" : txt_SparCount.Text);

                    _PeriodicalPlanSpar.Notes = txt_Notes.Text;

                    ctx.PeriodicalPlanSpars.InsertOnSubmit(_PeriodicalPlanSpar);
                    ctx.SubmitChanges(); ;
                }
            fo: ;
            }


            #endregion

            divMsg.Attributes["class"] = "alert alert-success text-right";
            lblResult.Text = "تم الحفظ بنجاح";

            #endregion
        
            
            }


       
            txt_PlanName.Text = string.Empty;
            txt_EveryKm.Text = string.Empty;
            txt_EveryWhilePerMonth.Text = string.Empty;
           
            txt_RequiredSpareParts.Text = string.Empty;
            txt_PlanDescription.Text = string.Empty;


            dt = (DataTable)ViewState["RequiredJobs"];
            dt.Rows.Clear();
            dt.Rows.Add(dt.NewRow());
            grd_WorksNeeded .DataSource = dt;
            grd_WorksNeeded.DataBind();
            grd_WorksNeeded.Rows[0].Visible = false;
            ViewState["RequiredJobs"] = dt;


            dtSpares = (DataTable)ViewState["Spares"];
            dtSpares.Rows.Clear();
            dtSpares.Rows.Add(dtSpares.NewRow());
            grd_SparParts .DataSource = dtSpares;
            grd_SparParts.DataBind();
            grd_SparParts.Rows[0].Visible = false;
            ViewState["Spares"] = dtSpares;
            PeriodicalPlanID  = 0;


            FillGridPlans();
        }

        protected void grd_SparParts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[0].Text;
                foreach (Button button in e.Row.Cells[3].Controls.OfType<Button>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('هل تريد الحذف " + item + "?')){ return false; };";
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                ASPxComboBox ddl_SpareMainId = (e.Row.FindControl("ddl_SpareMainId") as ASPxComboBox);
                ddl_SpareMainId.DataSource = from p in dc.ConfigDetails where p.MasterId == 1 select new { p.ConfigDetailId, p.ConfigDetailName };
                ddl_SpareMainId.TextField = "ConfigDetailName";
                ddl_SpareMainId.ValueField = "ConfigDetailId";
                ddl_SpareMainId.DataBind();

                DataRowView dr = e.Row.DataItem as DataRowView;
                ddl_SpareMainId.Value = dr["SpareMainId"].ToString();

          
                    
                        //var classid = (from p in dc.Vehcles  select new { p.ClassId }).SingleOrDefault();
                        ASPxComboBox ddl_SpareId = (e.Row.FindControl("ddl_SpareId") as ASPxComboBox);
                        ddl_SpareId.DataSource = from p in dc.SpareParts select new { p.SpareId, p.SpareName };
                        ddl_SpareId.TextField = "SpareName";
                        ddl_SpareId.ValueField = "SpareId";
                        ddl_SpareId.DataBind();
                        DataRowView dr1 = e.Row.DataItem as DataRowView;
                        ddl_SpareId.Value = dr1["SpareId"].ToString();
                   
           



            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                // Find the product drop-down list, you can id (or cell number)
                var ddl_SpareMainId = e.Row.FindControl("ddl_SpareMainId") as ASPxComboBox;
                // var ddlProducts = e.Row.Cells[0].Controls[0]; // Finding by cell number and index
                if (null != ddl_SpareMainId)
                {
                    ddl_SpareMainId.DataSource = from p in dc.ConfigDetails where p.MasterId == 1 select new { p.ConfigDetailId, p.ConfigDetailName };
                    ddl_SpareMainId.TextField = "ConfigDetailName";
                    ddl_SpareMainId.ValueField = "ConfigDetailId";
                    ddl_SpareMainId.DataBind();
                }
            }
        }

        protected void ddl_SpareMainId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASPxComboBox ddl_SpareMainId = grd_SparParts.FooterRow.FindControl("ddl_SpareMainId") as ASPxComboBox;
            ASPxComboBox ddl_SpareId = grd_SparParts.FooterRow.FindControl("ddl_SpareId") as ASPxComboBox;
            ClearGridTextBoxes();

            try
            {
                if (ddl_SpareMainId.SelectedItem.Value != null)
                {
                    ddl_SpareId.Items.Clear();
                    ddl_SpareId.Text = string.Empty;
                    ddl_SpareId.DataSource = from p in dc.SpareParts where p.MainCategory == int.Parse(ddl_SpareMainId.SelectedItem.Value.ToString())  select new { p.SpareId, p.SpareName };
                    ddl_SpareId.TextField = "SpareName";
                    ddl_SpareId.ValueField = "SpareId";
                    ddl_SpareId.DataBind();
                }
            }
            catch { }
        }
        private void ClearGridTextBoxes()
        {
            TextBox txt_SparCount = grd_SparParts.FooterRow.FindControl("txt_SparCount") as TextBox;
            txt_SparCount.Text = string.Empty;
        }

        protected void ddl_VehcleId_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtSpares = (DataTable)ViewState["Spares"];
            dtSpares.Rows.Clear();
            dtSpares.Rows.Add(dtSpares.NewRow());
            grd_SparParts.DataSource = dtSpares;
            grd_SparParts.DataBind();
            grd_SparParts.Rows[0].Visible = false;
            ViewState["Spares"] = dtSpares;
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ASPxComboBox ddl_SpareMainId = (ASPxComboBox)grd_SparParts.FooterRow.FindControl("ddl_SpareMainId");
            ASPxComboBox ddl_SpareId = (ASPxComboBox)grd_SparParts.FooterRow.FindControl("ddl_SpareId");  
            TextBox txt_SparCount = (TextBox)grd_SparParts.FooterRow.FindControl("txt_SparCount");
            TextBox txt_Notes = (TextBox)grd_SparParts.FooterRow.FindControl("txt_Notes");

            #region validation
            try
            {
                if (ddl_SpareMainId.SelectedItem.Value == null)
                {
                    divMsg2.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "ادخل الصنف الرئيسي";
                    return;

                }
            }
            catch
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult2.Text = "ادخل الصنف الرئيسي";
                return;
            }
            try
            {
                if (ddl_SpareId.SelectedItem.Value == null)
                {
                    divMsg2.Attributes["class"] = "alert alert-danger text-right";
                    lblResult2.Text = "ادخل الصنف الفرعي";
                    return;

                }
            }
            catch
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult2.Text = "ادخل الصنف الفرعي";
                return;

            }
            for (int i = 0; i < grd_SparParts.Rows.Count ; i++)
            {
            
                ASPxComboBox ddl_SpareIdItem = (ASPxComboBox)grd_SparParts.Rows[i].FindControl("ddl_SpareId");
                if (ddl_SpareIdItem.Value != null)
                { 
                 if (decimal.Parse(string.IsNullOrEmpty(ddl_SpareIdItem.Value.ToString()) ? "0" : ddl_SpareIdItem.Value.ToString()) == decimal.Parse(string.IsNullOrEmpty(ddl_SpareId.Value.ToString()) ? "0" : ddl_SpareId.Value.ToString()))
                {
                    divMsg2.Attributes["class"] = "alert alert-danger text-right";
                    lblResult2.Text = "عفوا تم ادخال نفس الصنف سابقا";
                    return;
                
                }
                }
               
            }
            #endregion


            dtSpares = (DataTable)ViewState["Spares"];
            DataRow dr1 = dtSpares.NewRow();
            dr1[0] = ddl_SpareMainId.SelectedItem.Value;
            dr1[1] = ddl_SpareId.SelectedItem.Value;
            dr1[2] = txt_SparCount.Text;
            dr1[3] = txt_Notes.Text;

            dtSpares.Rows.Add(dr1);

            grd_SparParts.DataSource = dtSpares;
            grd_SparParts.DataBind();
            ViewState["Spares"] = dtSpares;
            grd_SparParts.Rows[0].Visible = false;


        }

        protected void grd_SparParts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
               
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                int index = gvr.RowIndex;

                DataTable dtSpar= ViewState["Spares"] as DataTable;
                dtSpar.Rows[index].Delete();
                ViewState["Spares"] = dtSpar;
                grd_SparParts.DataSource = ViewState["Spares"];
                grd_SparParts.DataBind();
                grd_SparParts.Rows[0].Visible = false;
           
            }
        }

        protected void btn_deleteFromGrid_Click(object sender, EventArgs e)
        {

        }

        protected void grd_SparParts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.PeriodicalMaitenencePlan .GetHashCode());
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

        protected void grd_WorksNeeded_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ASPxComboBox ddl_RequiredJob = (e.Row.FindControl("ddl_RequiredJob") as ASPxComboBox);
                ddl_RequiredJob.DataSource = from p in dc.ServicesSettings select new { p.ID, p.ServiceName };
                ddl_RequiredJob.TextField = "ServiceName";
                ddl_RequiredJob.ValueField = "ID";
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

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;

            PeriodicalPlanID = Convert.ToInt32(gvPlans.DataKeys[row.RowIndex].Value);

            var query = (from p in dc.PeriodicalPlans where p.maintPlanId.Equals(PeriodicalPlanID) select new { p.PlanName , p.PlanDescription , p.EveryKm , p.EveryWhilePerMonth  }).SingleOrDefault();
            txt_PlanName.Text = query.PlanName;
            txt_EveryKm.Text = query.EveryKm.ToString();
            txt_EveryWhilePerMonth.Text = query.EveryWhilePerMonth.ToString();
            txt_PlanDescription.Text = query.PlanDescription;



            #region FillPlanGrid
            var queryDet = from p in dc.PeriodicPlanDetails where p.maintPlanId == PeriodicalPlanID select new {p.RequiredJob,  p.Description ,p.maintPlanId };
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
            FillGridPlans();

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

            PeriodicalPlanID = Convert.ToInt32(gvPlans.DataKeys[row.RowIndex].Value);


            PeriodicalPlan _PeriodicalPlan = dc.PeriodicalPlans.Single(x => x.maintPlanId == PeriodicalPlanID);
            dc.PeriodicalPlans.DeleteOnSubmit(_PeriodicalPlan);
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

            FillGridPlans();
        }

        protected void gvPlans_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

    }
}

