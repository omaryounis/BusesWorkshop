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
    public partial class Classes : System.Web.UI.Page
    {
        WorkshopDataContext dc = new WorkshopDataContext();
        private int ClassID
        {
            get
            {
                if (ViewState["_ClassID"] != null)
                {
                    return Convert.ToInt32(ViewState["_ClassID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_ClassID", value);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "طرازات السيارات";
            permissions(dc);
            if (!IsPostBack)
            {
                FillBrands();
                FillGrdClasses();
            }
        }

        private void FillModels()
        {
            try
            {
                ddl_ModelId.Items.Clear();
                ddl_ModelId.DataSource = from p in dc.Models where p.BrandId.Equals(decimal.Parse(ddl_BrandId.SelectedItem.Value.ToString())) select new { p.ModelId, p.ModelName };
                ddl_ModelId.TextField = "ModelName";
                ddl_ModelId.ValueField = "ModelId";
                ddl_ModelId.DataBind();
            }
            catch { }
           
        }
        private void FillBrands()
        {
            
            ddl_BrandId.DataSource = from p in dc.Brands  select new { p.BrandId, p.BrandName };
            ddl_BrandId.TextField = "BrandName";
            ddl_BrandId.ValueField = "BrandId";
            ddl_BrandId.DataBind();
        }

        protected void ddl_BrandId_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillModels();
            FillGrdClasses();
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
            catch
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "اختر ماركة السيارة";
                return;
            }

            try
            {
                if (ddl_ModelId.SelectedItem.Value == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "اختر موديل السيارة";
                    return;
                }
            }
            catch
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "اختر موديل السيارة";
                return;
            }
            #endregion
            if (ClassID > 0)
            {
                #region update
                if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية التعديل";
                    return;
                }
                Class _Class = dc.Classes.Single(classs => classs.ClassId == ClassID);
                _Class.ModelId  = int.Parse(ddl_ModelId.SelectedItem.Value.ToString());
                _Class.ClassName  = txt_ClassName.Text;
                _Class.Notes = txt_Notes.Text;
                dc.SubmitChanges();
                result = _Class.ClassId ;
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
                Class _Class = new Class();
                _Class.ModelId = int.Parse(ddl_ModelId.SelectedItem.Value.ToString());
                _Class.ClassName = txt_ClassName.Text;
                _Class.Notes = txt_Notes.Text;
                dc.Classes.InsertOnSubmit(_Class);
                dc.SubmitChanges();
                result = _Class.ClassId ;
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

            EmptyControls();




            FillGrdClasses();
           
        }

        private void FillGrdClasses()
        {
            try
            {
                if (ddl_BrandId.SelectedItem.Value != null)
                {
                    gvClasses.DataSource = from p in dc.Classes   where p.Model.BrandId == int.Parse(ddl_BrandId.SelectedItem.Value.ToString()) orderby  p.Model.ModelName ,p.ClassName   ascending select  new { p.ClassId, p.ClassName, p.Model.ModelName, p.Model.Brand.BrandName, p.Notes };
                gvClasses.DataBind();
                
                }
                
            }
            catch { }
            
        }

        private void EmptyControls()
        {
            ddl_BrandId.Value = null;
            ddl_ModelId.Value = null;
            ddl_BrandId.Text = string.Empty;
            ddl_BrandId.Text = string.Empty;
            txt_ClassName.Text = string.Empty;
            txt_Notes.Text = string.Empty;
            ClassID = 0;
        }

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;



            ClassID  = Convert.ToInt32(gvClasses.DataKeys[row.RowIndex].Value);

            var query = (from p in dc.Classes  where p.ClassId.Equals(ClassID) select new { p.Model.ModelId , p.Model.Brand.BrandId , p.ClassName , p.Notes }).SingleOrDefault();
            ddl_BrandId.Value = query.BrandId.ToString();
            ddl_ModelId.Value = query.ModelId.ToString();
            txt_ClassName.Text = query.ClassName ;
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
            var query = from p in dc.Classes  where p.ClassId .Equals(Convert.ToInt32(gvClasses.DataKeys[row.RowIndex].Value)) select p;
            try
            { 
             foreach (var item in query)
            {
                dc.Classes.DeleteOnSubmit(item);
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
           
            
            FillGrdClasses();
        }

        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.Classes.GetHashCode());
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
