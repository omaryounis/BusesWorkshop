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
    public partial class Brands : System.Web.UI.Page
    {
        private int BrandID
        {
            get
            {
                if (ViewState["_BrandID"] != null)
                {
                    return Convert.ToInt32(ViewState["_BrandID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_BrandID", value);
            }
        }
        WorkshopDataContext dc = new WorkshopDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "ماركات السيارات";
            permissions(dc);
            if (!IsPostBack)
            {
                FillGrdBrands();

            }
        }

        private void FillGrdBrands()
        {
            gvBrands.DataSource = from p in dc.Brands select new { p.BrandId, p.BrandName };
            gvBrands.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;

            #region Validation
            
            #endregion
            if (BrandID > 0)
            {
                #region update
                if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية التعديل";
                    return;
                }
                Brand _Brand = dc.Brands.Single(brand => brand.BrandId == BrandID);
                _Brand.BrandName = txt_BrandName.Text ;
                _Brand.Notes = txt_Notes.Text;
                dc.SubmitChanges();
                result = _Brand.BrandId ;
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
                Brand _Brand = new Brand();
                _Brand.BrandName = txt_BrandName.Text;
                _Brand.Notes = txt_Notes.Text;
                dc.Brands.InsertOnSubmit(_Brand);
                dc.SubmitChanges();
                result = _Brand.BrandId;
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


            FillGrdBrands();
        }

        private void EmptyControls()
        {
            txt_BrandName.Text = string.Empty;
            txt_Notes.Text = string.Empty;
            BrandID = 0;
        }

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;

            BrandID = Convert.ToInt32(gvBrands.DataKeys[row.RowIndex].Value);

            var query = (from p in dc.Brands where p.BrandId.Equals(BrandID) select new { p.BrandId, p.BrandName , p.Notes  }).SingleOrDefault();
            txt_BrandName.Text  = query.BrandName ;
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
            var query = from p in dc.Brands where p.BrandId.Equals(Convert.ToInt32(gvBrands.DataKeys[row.RowIndex].Value)) select p;
            try
            {
 foreach (var item in query)
            {
                dc.Brands .DeleteOnSubmit(item);
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
           
            
            FillGrdBrands();
        }

        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.Brands.GetHashCode());
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
