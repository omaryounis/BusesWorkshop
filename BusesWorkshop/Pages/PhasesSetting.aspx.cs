using BusesWorkshop.DAL;
using BusesWorkshop.DAL.Bus;
using BusesWorkshop.VM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusesWorkshop.Pages
{
    public partial class PhasesSetting : System.Web.UI.Page
    {
        #region "Properties"
        WorkshopDataContext dc = new WorkshopDataContext();
        DataTable dt = new DataTable();
        private int phases_Id
        {
            get
            {
                if (ViewState["_phases_Id"] != null)
                {
                    return Convert.ToInt32(ViewState["_phases_Id"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_phases_Id", value);
            }
        }
        #endregion
        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {

            permissions(dc);
           
            Page.Title = "تهيئه المراحل";
            if (!IsPostBack)
            {
                fillGridView();
            }
           // fillGridView();

        }
      

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (phases_Id > 0)
            {
                Phase phase = dc.Phases.Single(c => c.phases_Id == phases_Id);
                phase.phases_Name = txtPhaseName.Text;

                if (cb_RequestType.SelectedItem != null)
                {
                    phase.requestType = int.Parse(cb_RequestType.SelectedItem.Value.ToString());
                }
                else
                {
                    phase.requestType = null;

                }
                phase.Phase_Order = int.Parse(cb_order.Text.ToString());
                //if (cb_order.SelectedItem != null)
                //{
                //    phase.Phase_Order = int.Parse(cb_order.SelectedItem.Value.ToString());
                //}
                //else
                //{
                //    phase.Phase_Order = null;

                //}
                if (rdbtn_IsActive != null)
                {
                    phase.IsActive = int.Parse(rdbtn_IsActive.SelectedItem.Value.ToString());
                }
                if (rd_Step != null) 
                {
                    phase.phase_Step = int.Parse(rd_Step.SelectedItem.Value.ToString());
                }

                dc.SubmitChanges();
                result = phase.phases_Id;
                //  fillGridView();
            }


            else
            {
                // insert


                //if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
                //{
                //    divMsg2.Attributes["class"] = "alert alert-danger text-right";
                //    lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                //    return;
                //}
                Phase phaseService = new Phase();
                phaseService.IsActive = 0;
                phaseService.phases_Name = txtPhaseName.Text.Trim();
                if (cb_RequestType.SelectedItem != null)
                {
                    phaseService.requestType = int.Parse(cb_RequestType.SelectedItem.Value.ToString());
                }
                else
                {
                    phaseService.requestType = null;

                }

                phaseService.Phase_Order = int.Parse(cb_order.Text.ToString());

                //if (cb_order.SelectedItem != null)
                //{
                //    phaseService.Phase_Order = int.Parse(cb_order.SelectedItem.Value.ToString());
                //}
                //else
                //{
                //    phaseService.Phase_Order = null;

                //}
                if (rdbtn_IsActive != null)
                {
                    phaseService.IsActive = int.Parse(rdbtn_IsActive.SelectedItem.Value.ToString());
                }
                if(rd_Step.SelectedItem != null)
                {
                    phaseService.phase_Step = int.Parse(rd_Step.SelectedItem.Value.ToString());
                }

               dc.Phases.InsertOnSubmit(phaseService);
                dc.SubmitChanges();

                result = phaseService.phases_Id;
            }
            if (result > 0)
            {
                divMsg2.Attributes["class"] = "alert alert-success text-right";
                lblResult.Text = "تم الحفظ بنجاح";

                // txtNotes.Text = txtKMcount.Text = txtCheckDate.Text = string.Empty;

            }
            else
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "حدث خطأ أثنا الحفظ";

            }


            empty_Controls();
            fillGridView();

        }

     

        protected void BtnEditClick(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;
            phases_Id = Convert.ToInt32(GridViewPhase.DataKeys[row.RowIndex].Value);
            var editgrid = from phase in dc.GetTable<Phase>().Where(c => c.phases_Id == phases_Id)
                           select phase;
            dt = editgrid.CopyToDataTable();
            if (dt.Rows.Count > 0)
            {
                txtPhaseName.Text = dt.Rows[0]["phases_Name"].ToString();
                cb_order.Text = dt.Rows[0]["Phase_Order"].ToString();
                cb_RequestType.Value = dt.Rows[0]["requestType"].ToString();
                rdbtn_IsActive.Value = dt.Rows[0]["IsActive"].ToString();
                rd_Step.Value = dt.Rows[0]["phase_Step"].ToString();

            }
        }


        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;
            Phase phase = dc.Phases.Single(c => c.phases_Id == int.Parse(GridViewPhase.DataKeys[row.RowIndex].Value.ToString()));
            try
            {
                phase.Is_Delete = 0; //اتمسح
               // dc.Phases.InsertOnSubmit(phase);
                dc.SubmitChanges();
            }
            catch (SqlException ex)
            {

               // if (ex.Errors[0].Number == 547)
               // {

                    divMsg2.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفو السجل المراد حذفة مرتبط بمدخلات اخري";
                    return;
               // }
            }
            fillGridView();
        }
        #endregion

        #region "Methods"
        public void fillGridView()
        {
            //is active  =1 not deleted
            var gettable = from phase in dc.GetTable<Phase>().Where(c => c.IsActive == 0)
                           select new PhaseSettingVM
                           {
                               phases_Id = phase.phases_Id,
                               requestType = phase.requestType == 0 ? "طلب صيانه" : phase.requestType == 1 ? "دعم فنى " : phase.requestType == 2 ? "الكل" : "",
                               Phase_Order = phase.Phase_Order.ToString() ,//== 1 ? "الاولى" : phase.Phase_Order == 2 ? "الثانيه" : phase.Phase_Order == 3 ? "الثالثه" : "",
                               phases_Name = phase.phases_Name,
                               IsActive = phase.IsActive == 0 ? "مفعل" : phase.IsActive == 1 ? "غير مفعل " : "",
                               phase_Step = phase.phase_Step == 0 ? "مراحل اعتماد" : phase.phase_Step == 1 ? "مراحل توزيع" : phase.phase_Step == 2 ? "مراحل اجراء" : ""

                           };
            //  GridViewPhase.Columns[5].Visible = false;
            dt = gettable.CopyToDataTable();
            if (dt.Rows.Count > 0)
            {
                GridViewPhase.DataSource = dt;
                GridViewPhase.DataBind();
            }
            else
            {
                GridViewPhase.DataSource = null;
                GridViewPhase.DataBind();
            }
        }
        private void empty_Controls()
        {
            txtPhaseName.Text = cb_order.Text = cb_RequestType.Text = string.Empty;
            phases_Id = 0;
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.ServicesSettings.GetHashCode());
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
                        ViewState["AllowDelete"] = 1;
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

      
    }
}