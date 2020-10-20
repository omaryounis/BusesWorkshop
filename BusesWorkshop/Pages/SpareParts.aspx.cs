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

namespace BusesWorkshop.Pages
{
    public partial class SpareParts : System.Web.UI.Page
    {
        WorkshopDataContext dc = new WorkshopDataContext();

        private int SpareID
        {
            get
            {
                if (ViewState["_SpareID"] != null)
                {
                    return Convert.ToInt32(ViewState["_SpareID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_SpareID", value);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "قطع الغيار";
            permissions(dc);

            if (!IsPostBack)
            {
                FillMainCategories();
                FillBrands();

                //FillGridSpareParts();
            }
        }

        private void FillBrands()
        {
            ddl_ModelId.Items.Clear();
            ddl_ModelId.Text = string.Empty;
            ddl_ClassId.Items.Clear();
            ddl_ModelId.Text = string.Empty;
            ddl_BrandId.DataSource = from p in dc.Brands select new { p.BrandId, p.BrandName };
            ddl_BrandId.TextField = "BrandName";
            ddl_BrandId.ValueField = "BrandId";
            ddl_BrandId.DataBind();
        }

        private void FillMainCategories()
        {
            ddl_MainCategory.DataSource = from p in dc.ConfigDetails where p.MasterId == 1 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_MainCategory.TextField = "ConfigDetailName";
            ddl_MainCategory.ValueField = "ConfigDetailId";
            ddl_MainCategory.DataBind();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region Validation
            try
            {
                if (ddl_ClassId.SelectedItem.Value == null)
                {
                    divMsg2.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "ادخل ماركة السيارة";
                    return;
                }
            }
            catch { }
            try
            {
                if (ddl_MainCategory.SelectedItem.Value == null)
                {
                    divMsg2.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "ادخل ماركة السيارة";
                    return;
                }
            }
            catch
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "ادخل ماركة السيارة";
                return;

            }

            #endregion
            int? result = 0;
            if (SpareID > 0)
            {
                #region Update Spare
                if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
                {
                    divMsg2.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية التعديل";
                    return;
                }
                SparePart _SparePrts = dc.SpareParts.Single(spar => spar.SpareId == SpareID);
                _SparePrts.SpareName = txt_SpareName.Text;
                _SparePrts.MainCategory = int.Parse(ddl_MainCategory.SelectedItem.Value.ToString());
                _SparePrts.ClassId = int.Parse(ddl_ClassId.SelectedItem.Value.ToString());

                _SparePrts.SparePrice = decimal.Parse(string.IsNullOrEmpty(txt_SparePrice.Text) ? "0" : txt_SparePrice.Text);
                _SparePrts.LabourCost = decimal.Parse(string.IsNullOrEmpty(txt_LabourCost.Text) ? "0" : txt_LabourCost.Text);

                _SparePrts.Notes = txt_Notes.Text;
                dc.SubmitChanges();
                result = _SparePrts.SpareId;
                #endregion

            }
            else
            {
                #region Save

                if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
                {
                   divMsg2 .Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                    return;
                }

                SparePart _SparePrts = new SparePart();
                _SparePrts.SpareName = txt_SpareName.Text;
                _SparePrts.MainCategory = int.Parse(ddl_MainCategory.SelectedItem.Value.ToString());
                _SparePrts.ClassId = int.Parse(ddl_ClassId.SelectedItem.Value.ToString());

                _SparePrts.SparePrice = decimal.Parse(string.IsNullOrEmpty(txt_SparePrice.Text) ? "0" : txt_SparePrice.Text);
                _SparePrts.LabourCost = decimal.Parse(string.IsNullOrEmpty(txt_LabourCost.Text) ? "0" : txt_LabourCost.Text);
                _SparePrts.Notes = txt_Notes.Text;
                dc.SpareParts.InsertOnSubmit(_SparePrts);
                dc.SubmitChanges();
                result = _SparePrts.SpareId;
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

            }
            FillGridSpareParts();
            EmptyControls();

        }

        private void FillGridSpareParts()
        {
            try
            {
                if (ddl_ClassId.SelectedItem.Value != null)
                {
                    grd_SpareParts.DataSource = from p in dc.SpareParts where p.ClassId == int.Parse(ddl_ClassId.SelectedItem.Value.ToString()) select new { p.SpareId, p.SpareName, p.Class.Model.Brand.BrandName, p.Class.Model.ModelName, p.Class.ClassName, p.SparePrice, p.LabourCost, p.Notes };
                    grd_SpareParts.DataBind();
                }
                else
                {
                    grd_SpareParts.DataSource = null;
                    grd_SpareParts.DataBind();
                }
            }
            catch 
            {
            
            
            }
            

        }

        private void EmptyControls()
        {

            txt_SpareName.Text = string.Empty;
            txt_SparePrice.Text = string.Empty;
            txt_LabourCost.Text = string.Empty;
            txt_Notes.Text = string.Empty;
            ddl_BrandId.Value = null;
            ddl_BrandId.Text = string.Empty;
            ddl_MainCategory.Value = null;
            ddl_MainCategory.Text = string.Empty;
            ddl_ModelId.Value = null;
            ddl_ModelId.Text = string.Empty;
            ddl_ClassId.Value = null;
            ddl_ClassId.Text = string.Empty;

            SpareID = 0;
            //ddl_SpareUnit.Value = null;
            //ddl_SpareUnit.Text = string.Empty;

        }

        protected void grd_SpareParts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;
            DataTable dt = new DataTable();

            SpareID = Convert.ToInt32(grd_SpareParts.DataKeys[row.RowIndex].Value);


            var query = (from p in dc.SpareParts where p.SpareId.Equals(SpareID) select new { p.Class.Model.ModelId, p.SpareName, p.Class.Model.Brand.BrandId, p.ClassId, p.SparePrice, p.LabourCost, p.Notes, p.MainCategory }).FirstOrDefault();
            txt_SpareName.Text = query.SpareName;
            ddl_MainCategory.Value = query.MainCategory.ToString();
            ddl_BrandId.Value = query.BrandId.ToString();
            ddl_ClassId.Value = query.ClassId.ToString();
            ddl_ModelId.Value = query.ModelId.ToString();
            txt_SparePrice.Text = query.SparePrice.ToString();
            txt_LabourCost.Text = query.LabourCost.ToString();
            txt_Notes.Text = query.Notes;

        }

        protected void lnk_Edit_Click1(object sender, EventArgs e)
        {
            if (int.Parse(ViewState["AllowDelete"].ToString()) == 0 || ViewState["AllowDelete"] == null)
            {
               divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ليس لديك صلاحية الحذف";
                return;
            }
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;
            var _SparePart = from p in dc.SpareParts where p.SpareId.Equals(Convert.ToInt32(grd_SpareParts.DataKeys[row.RowIndex].Value)) select p;
            try
            {
                foreach (var item in _SparePart)
                {
                    dc.SpareParts.DeleteOnSubmit(item);

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
                FillGridSpareParts();
        }

        protected void ddl_BrandId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_BrandId.SelectedItem.Value != null)
                {
                    ddl_ModelId.Items.Clear();
                    ddl_ClassId.Items.Clear();
                    ddl_ModelId.DataSource = from p in dc.Models where p.BrandId == int.Parse(ddl_BrandId.SelectedItem.Value.ToString()) select new { p.ModelId, p.ModelName };
                    ddl_ModelId.TextField = "ModelName";
                    ddl_ModelId.ValueField = "ModelId";
                    ddl_ModelId.DataBind();
                }
            }
            catch { }


        }

        protected void ddl_ModelId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_ModelId.SelectedItem.Value == null)
                    ddl_ClassId.Items.Clear();
                ddl_ClassId.DataSource = from p in dc.Classes where p.ModelId == int.Parse(ddl_ModelId.SelectedItem.Value.ToString()) select new { p.ClassId, p.ClassName };
                ddl_ClassId.TextField = "ClassName";
                ddl_ClassId.ValueField = "ClassId";
                ddl_ClassId.DataBind();
            }
            catch
            {

            }

        }

        protected void ddl_ClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_ClassId.SelectedItem.Value != null)
                {
                    grd_SpareParts.DataSource = from p in dc.SpareParts where p.ClassId == int.Parse(ddl_ClassId.SelectedItem.Value.ToString()) select new { p.SpareId, p.SpareName, p.Class.Model.Brand.BrandName, p.Class.Model.ModelName, p.Class.ClassName, p.SparePrice, p.LabourCost, p.Notes };
                    grd_SpareParts.DataBind();
                }
            }
            catch
            {

            }
         
        }

        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.SpareParts.GetHashCode());
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
