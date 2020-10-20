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
    public partial class Recommendations : System.Web.UI.Page
    {
        WorkshopDataContext dc = new WorkshopDataContext();
        private int RecID
        {
            get
            {
                if (ViewState["_RecID"] != null)
                {
                    return Convert.ToInt32(ViewState["_RecID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_RecID", value);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            permissions(dc);
            Page.Title = "التوصيات";
            if (!IsPostBack)
            {
                radio_SupportType.SelectedIndex = -1;
            }
        }
       



        protected void radio_SupportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var RecType =int.Parse(radio_SupportType.SelectedValue.ToString());
            var d = dc.ConfigDetails.Where(x => (x.RecType == RecType || x.RecType == 2) && x.MasterId ==18).Select(x => new { x.ConfigDetailId, x.ConfigDetailName });
            ddl_WorkId.DataSource = d;
            ddl_WorkId.TextField = "ConfigDetailName";
            ddl_WorkId.ValueField = "ConfigDetailId";
            ddl_WorkId.DataBind();
        }

        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()),49);
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;
            #region validation
            if (ddl_WorkId.SelectedItem == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل اسم العمل";
                return;
            }
            if (txtRecDesc.Text == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل التوصيه";
                return;
            }

            #endregion
            if (RecID > 0)
            {
                #region Update
                if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية التعديل";
                    return;
                }
                Recommendation obj = dc.Recommendations.Single(x => x.RecID == RecID);
                obj.RecDesc = txtRecDesc.Text;
                obj.SupportType = radio_SupportType.SelectedItem.Value.ToString() == "0" ? true : false; 
                obj.ConfigDetailId = int.Parse(ddl_WorkId.SelectedItem.Value.ToString());
                dc.SubmitChanges();
                result = obj.RecID;
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
                Recommendation obj = new Recommendation();
                obj.RecDesc = txtRecDesc.Text;
                obj.ConfigDetailId = int.Parse(ddl_WorkId.SelectedItem.Value.ToString());
                obj.SupportType = radio_SupportType.SelectedItem.Value.ToString() == "0" ? false : true;//Convert.ToBoolean(radio_SupportType.SelectedItem.Value.ToString());
                dc.Recommendations.InsertOnSubmit(obj);
                dc.SubmitChanges();
                result = obj.RecID;
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
            ddl_WorkId.Text = String.Empty;
            ddl_WorkId.Value = null;
            txtRecDesc.Text = String.Empty;
        }

        private void FillGrid()
        {
            if (ddl_WorkId.SelectedItem != null)
            {
                gvRecommendations.DataSource = from p in dc.Recommendations where p.ConfigDetailId == int.Parse(ddl_WorkId.SelectedItem.Value.ToString())
                                               select new {p.RecID, p.ConfigDetail.ConfigDetailName, p.RecDesc,
                                                   SupportType=p.SupportType!=true? "دعم فني" : "طلب صيانه"
                                               };
                gvRecommendations.DataBind();
            }
            }

        protected void ddl_WorkId_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

       

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;



            RecID = Convert.ToInt32(gvRecommendations.DataKeys[row.RowIndex].Value);

            var query = (from p in dc.Recommendations where p.RecID.Equals(RecID) select new {p.SupportType, p.RecID, p.RecDesc , p.ConfigDetailId }).SingleOrDefault();
            txtRecDesc.Text = query.RecDesc;
            radio_SupportType.SelectedIndex=query.SupportType==false?1:0;
            radio_SupportType.SelectedValue = query.SupportType == false ? "1" : "0";
            ddl_WorkId.Value = query.ConfigDetailId.ToString();
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
            var query = from p in dc.Recommendations where p.RecID.Equals(Convert.ToInt32(gvRecommendations.DataKeys[row.RowIndex].Value)) select p;
            try
            {
                foreach (var item in query)
                {
                    dc.Recommendations.DeleteOnSubmit(item);
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
    }
}
