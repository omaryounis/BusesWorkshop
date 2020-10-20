using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using BusesWorkshop.DAL.Bus;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Pages
{
    public partial class District : System.Web.UI.Page
    {
        WorkshopDataContext dc = new WorkshopDataContext();
        private int DistrictID
        {
            get
            {
                if (ViewState["_DsitrictID"] != null)
                {
                    return Convert.ToInt32(ViewState["_DsitrictID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_DsitrictID", value);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            permissions(dc);
            Page.Title = "اسم الحي";
            if (!IsPostBack)
            {
                FillCities();
            }
            
        }

        private void FillCities()
        {
            ddl_CityId.DataSource = from p in dc.ConfigDetails where p.MasterId == 13 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_CityId.TextField = "ConfigDetailName";
            ddl_CityId.ValueField = "ConfigDetailId";
            ddl_CityId.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;
            #region validation
            if (ddl_CityId.SelectedItem == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل اسم المدينة";
                return;
            }
            #endregion
            if (DistrictID > 0)
            {
                #region Update
                if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية التعديل";
                    return;
                }

                BusesWorkshop.DAL.Bus.District _District = dc.Districts.Single(x => x.DistrictId == DistrictID);
                _District.DistrictName = txt_DistrictName.Text;
                _District.CityId =int.Parse( ddl_CityId.SelectedItem.Value.ToString());
                dc.SubmitChanges();
                result = _District.DistrictId;
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
              BusesWorkshop.DAL.Bus.District _District = new BusesWorkshop.DAL.Bus.District();
              _District.DistrictName = txt_DistrictName.Text;
              _District.CityId =int.Parse( ddl_CityId.SelectedItem.Value.ToString());
              dc.Districts.InsertOnSubmit(_District);
              dc.SubmitChanges();
              result = _District.DistrictId;
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

            


      
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.District.GetHashCode());
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

        protected void ddl_CityId_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
           
        }

        private void FillGrid()
        {
            if (ddl_CityId.SelectedItem != null)
            {
                gvdistrict.DataSource = from p in dc.Districts where p.CityId == int.Parse(ddl_CityId.SelectedItem.Value.ToString()) select new { p.DistrictName,CityNAME =  p.ConfigDetail.ConfigDetailName ,p.DistrictId};
                gvdistrict.DataBind();
            }
        }

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;



            DistrictID = Convert.ToInt32(gvdistrict.DataKeys[row.RowIndex].Value);

            var query = (from p in dc.Districts where p.DistrictId.Equals(DistrictID) select new { p.CityId , p.DistrictName}).SingleOrDefault();
            txt_DistrictName.Text = query.DistrictName;
            ddl_CityId.Value = query.CityId.ToString();


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
            var query = from p in dc.Districts where p.DistrictId.Equals(Convert.ToInt32(gvdistrict.DataKeys[row.RowIndex].Value)) select p;
            try
            {
                foreach (var item in query)
                {
                    dc.Districts.DeleteOnSubmit(item);
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
