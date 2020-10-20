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
using System.Data.SqlClient;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Pages.Definitions
{
    public partial class Models : System.Web.UI.Page
    {
        private int ModelID
        {
            get
            {
                if (ViewState["_ModelID"] != null)
                {
                    return Convert.ToInt32(ViewState["_ModelID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_ModelID", value);
            }
        }


        WorkshopDataContext dc = new WorkshopDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "موديلات السيارات";
            WorkshopDataContext dc = new WorkshopDataContext();
            permissions(dc);
            if (!IsPostBack)
            {
                FillBrands();
                FillGrdModels();
            }
        }

        private void FillGrdModels()
        {
            try
            {
                if (ddl_BrandId.SelectedItem.Value != null)
                {
                    gvModels.DataSource = from p in dc.Models where p.BrandId == int.Parse(ddl_BrandId.SelectedItem.Value.ToString()) select new { p.ModelId, p.ModelName, p.Brand.BrandName };
                    gvModels.DataBind();

                }

            }
            catch {
              
            }
            
        }

        private void FillBrands()
        {
            ddl_BrandId.DataSource = from p in dc.Brands select new { p.BrandId, p.BrandName };
            ddl_BrandId.TextField = "BrandName";
            ddl_BrandId.ValueField = "BrandId";
            ddl_BrandId.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;

            #region Validation
            try
            {
                if (ddl_BrandId.SelectedItem.Value == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "اختر ماركة السيارة";
                    return;
                }
            }
            catch {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "اختر ماركة السيارة";
                return;
            }
            
            #endregion
            if (ModelID > 0)
            {
                #region update
                if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية التعديل";
                    return;
                }
                Model _Model = dc.Models.Single(model => model.ModelId == ModelID);
                _Model.BrandId = int.Parse(ddl_BrandId.SelectedItem.Value.ToString());
                _Model.ModelName = txt_ModelName.Text;
                _Model.Notes = txt_Notes.Text;
                dc.SubmitChanges();
                result = _Model.ModelId; 
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
            Model _Model = new Model();
            _Model.BrandId = int.Parse(ddl_BrandId.SelectedItem.Value.ToString());
            _Model.ModelName = txt_ModelName.Text;
            _Model.Notes = txt_Notes.Text;
            dc.Models.InsertOnSubmit(_Model);
            dc.SubmitChanges();
            result = _Model.ModelId; 
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

FillGrdModels();
            EmptyControls();
            
        }

        private void EmptyControls()
        {
            ddl_BrandId.Value = null;
            ddl_BrandId.Text = null;
            txt_ModelName.Text = string.Empty;
            txt_Notes.Text = string.Empty;
            ModelID = 0;
        }

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;



            ModelID  = Convert.ToInt32(gvModels.DataKeys[row.RowIndex].Value);

            var query = (from p in dc.Models where p.ModelId.Equals(ModelID) select new { p.ModelId , p.BrandId , p.Brand.BrandName , p.ModelName , p.Notes }).SingleOrDefault();
            ddl_BrandId.Value = query.BrandId.ToString(); ;
            txt_ModelName.Text = query.ModelName;
            txt_Notes.Text = query.Notes;

     
        
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
            var query = from p in dc.Models  where p.ModelId.Equals(Convert.ToInt32(gvModels.DataKeys[row.RowIndex].Value)) select p;
            try
            {
                foreach (var item in query)
                {
                    dc.Models.DeleteOnSubmit(item);
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
                FillGrdModels();
        }

        protected void ddl_BrandId_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrdModels();
        }

        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.Models.GetHashCode());
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

    }
}
