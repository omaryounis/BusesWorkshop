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
    public partial class Material : System.Web.UI.Page
    {
        WorkshopDataContext dc = new WorkshopDataContext();

        private int MaterialID
        {
            get
            {
                if (ViewState["MaterialID"] != null)
                {
                    return Convert.ToInt32(ViewState["MaterialID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("MaterialID", value);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        
        {
            Page.Title = "الخامات";
            permissions(dc);
            if (!IsPostBack)
            {
                FillMainCategories();
            
            }
        }
        private void FillMainCategories()
        {
            ddl_MainCategory.DataSource = from p in dc.ConfigDetails where p.MasterId == 17 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_MainCategory.TextField = "ConfigDetailName";
            ddl_MainCategory.ValueField = "ConfigDetailId";
            ddl_MainCategory.DataBind();
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.Material.GetHashCode());
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

            #region Validation
            if (ddl_MainCategory.SelectedItem.Value == null)
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "اختر التصنيف";
                return;
            }
           

            #endregion



            if (MaterialID > 0)
            {

                #region updat
                if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
                {
                    divMsg2.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية التعديل";
                    return;
                }
                DAL.Bus.Material _Material = dc.Materials.Single(x => x.MaterialId == MaterialID)  ;
                  
                _Material.CategoryId = int.Parse(ddl_MainCategory.SelectedItem.Value.ToString());
                _Material.MaterialName = txt_MaterialName.Text;
                _Material.MaterialPrice = decimal.Parse( txt_MaterialPrice.Text);
              
                dc.SubmitChanges();
                result = _Material.MaterialId;
                #endregion
            }
            else
            {
                #region save

                if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
                {
                    divMsg2.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                    return;
                }
                DAL.Bus.Material _Material = new BusesWorkshop.DAL.Bus.Material();
                _Material.CategoryId = int.Parse(ddl_MainCategory.SelectedItem.Value.ToString());
                _Material.MaterialName = txt_MaterialName.Text;
                _Material.MaterialPrice = decimal.Parse(txt_MaterialPrice.Text);
                dc.Materials.InsertOnSubmit(_Material);
                dc.SubmitChanges();
                result = _Material.MaterialId;
                #endregion
            }
            if (result > 0)
            {
                divMsg2.Attributes["class"] = "alert alert-success text-right";
                lblResult.Text = "تم الحفظ بنجاح";

            }
            else
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "حدث خطأ أثنا الحفظ";
                // divMsg. = "alert alert-danger text-right"; 
            }

            FillGrid();
            MyClasses.ClearControls(Page);




            
        }

        private void FillGrid()
        {
            if (ddl_MainCategory.SelectedItem != null)
            {
                grd_Material.DataSource = from p in dc.Materials where p.CategoryId == int.Parse(ddl_MainCategory.SelectedItem.Value.ToString()) select new { p.MaterialId, p.MaterialName, p.MaterialPrice };

                grd_Material.DataBind();
            }
        }

        protected void ddl_MainCategory_SelectedIndexChanged(object sender, EventArgs e)
        {


            FillGrid();
       
        }

        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            if (int.Parse(ViewState["AllowDelete"].ToString()) == 0 || ViewState["AllowDelete"] == null)
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ليس لديك صلاحية الحذف";
                return;
            }
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;
            var query = from p in dc.Materials where p.MaterialId.Equals(Convert.ToInt32(grd_Material.DataKeys[row.RowIndex].Value)) select p;
            try
            {
                foreach (var item in query)
                {
                    dc.Materials.DeleteOnSubmit(item);
                }
                dc.SubmitChanges();
            }

            catch (SqlException ex)
            {

                if (ex.Errors[0].Number == 547)
                {
                    divMsg2.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفو السجل المراد حذفة مرتبط بمدخلات اخري";
                    return;
                }


            }


            FillGrid();
        }

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;



            MaterialID = Convert.ToInt32(grd_Material.DataKeys[row.RowIndex].Value);

            var query = (from p in dc.Materials where p.MaterialId.Equals(MaterialID) select new { p.MaterialId, p.MaterialName, p.MaterialPrice, p.CategoryId }).SingleOrDefault();
            ddl_MainCategory.Value = query.CategoryId.ToString();
            txt_MaterialName.Text = query.MaterialName;
            txt_MaterialPrice.Text = query.MaterialPrice.ToString();

        }
    }
}
