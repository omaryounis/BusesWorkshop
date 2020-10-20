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
                    return Convert.ToInt32(ViewState["_BusID"]);
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
            if (!IsPostBack)
            {
                FillBrands();
                gvModels.DataSource = from p in dc.Models select new { p.ModelId , p.ModelName , p.Brand.BrandName };
                gvModels.DataBind();
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
            if (ddl_BrandId.SelectedItem.Value == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "اختر ماركة السيارة";
                return;
            }
            #endregion
            if (ModelID > 0)
            {
                #region update
              
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


            EmptyControls();

        }

        private void EmptyControls()
        {
            ddl_BrandId.Value = null;
            ddl_BrandId.Text = null;
            txt_ModelName.Text = string.Empty;
            txt_Notes.Text = string.Empty;
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
    }
}
