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
    public partial class Buildings : System.Web.UI.Page
    {
        WorkshopDataContext dc = new WorkshopDataContext();
        private int BuildingID
        {
            get
            {
                if (ViewState["_BuildingID"] != null)
                {
                    return Convert.ToInt32(ViewState["_BuildingID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_BuildingID", value);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            permissions(dc);
            Page.Title = "المبانى";
            if (!IsPostBack)
            {
                FillCities();
                FillUsages();
                FillBuildingsTypes();
                FillOwnerShip();
                FillCompanies();
            }
        }

        private void FillCompanies()
        {
            ddl_companyId.DataSource = from p in dc.Companies where p.UserCompanies.Any(x => x.UserID == int.Parse(Session["UserId"].ToString())) select new { p.CompName, p.ID };
            ddl_companyId.TextField = "CompName";
            ddl_companyId.ValueField = "ID";
            ddl_companyId.DataBind();
        }

        private void FillOwnerShip()
        {
            ddl_OwnerShipId.DataSource = from p in dc.ConfigDetails where p.MasterId == 16 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_OwnerShipId.TextField = "ConfigDetailName";
            ddl_OwnerShipId.ValueField = "ConfigDetailId";
            ddl_OwnerShipId.DataBind();
        }

        private void FillBuildingsTypes()
        {
            ddl_BuildingTypeId.DataSource = from p in dc.ConfigDetails where p.MasterId == 15 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_BuildingTypeId.TextField = "ConfigDetailName";
            ddl_BuildingTypeId.ValueField = "ConfigDetailId";
            ddl_BuildingTypeId.DataBind();
        }

        private void FillUsages()
        {
            ddl_UsageId.DataSource = from p in dc.ConfigDetails where p.MasterId == 14 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_UsageId.TextField = "ConfigDetailName";
            ddl_UsageId.ValueField = "ConfigDetailId";
            ddl_UsageId.DataBind();
        }
        private void FillCities()
        {
            ddl_CityId.DataSource = from p in dc.ConfigDetails where p.MasterId == 13 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_CityId.TextField = "ConfigDetailName";
            ddl_CityId.ValueField = "ConfigDetailId";
            ddl_CityId.DataBind();
        }
        protected void ddl_CityId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_CityId.SelectedItem != null)
            {

                ddl_DistrictId.DataSource = from p in dc.Districts where p.CityId == int.Parse(ddl_CityId.SelectedItem.Value.ToString()) select new { p.DistrictId, p.DistrictName };
                ddl_DistrictId.TextField = "DistrictName";
                ddl_DistrictId.ValueField = "DistrictId";
                ddl_DistrictId.DataBind();
            }
        }

        protected void ddl_DistrictId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_DistrictId.SelectedItem != null)
            {
                ddl_StreetId.DataSource = from p in dc.Streets where p.DistrictId == int.Parse(ddl_DistrictId.SelectedItem.Value.ToString()) select new { p.StreetId, p.StreetName };
                ddl_StreetId.TextField = "StreetName";
                ddl_StreetId.ValueField = "StreetId";
                ddl_StreetId.DataBind();


            }
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.Buildings.GetHashCode());
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

            if (ddl_companyId.SelectedItem == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل اسم الشركة";
                return;
            }
            #endregion
            if (BuildingID > 0)
            {
                #region Update
                if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية التعديل";
                    return;
                }

                Buiding _Buiding = dc.Buidings.Single(x => x.BuildingId == BuildingID);
                _Buiding.BuildingName = txt_BuildingName.Text;
                _Buiding.CompanyId = int.Parse(ddl_companyId.SelectedItem.Value.ToString());
                if (ddl_StreetId.SelectedItem != null)
                {
                    _Buiding.StreetId = int.Parse(ddl_StreetId.SelectedItem.Value.ToString());
                }
                else
                {
                    _Buiding.StreetId = null;
                }
                _Buiding.PieceNo = txt_PieceNo.Text;
                _Buiding.NorthBorder = txt_NorthBorder.Text;
                _Buiding.EasternBorder = txt_EasternBorder.Text;
                _Buiding.SouthBorder = txt_SouthBorder.Text;
                _Buiding.WesternBorder = txt_WesternBorder.Text;
                _Buiding.BuildingLicinseNo = txt_BuildingLicinseNo.Text;
                if (ddl_UsageId.SelectedItem != null)
                {
                    _Buiding.UsageId = int.Parse(ddl_UsageId.SelectedItem.Value.ToString());
                }
                else
                {
                    _Buiding.UsageId = null;
                }
                _Buiding.Area = txt_Area.Text;
                if (txt_FloorNo.Text != string.Empty)
                {
                    _Buiding.FloorNo = int.Parse(txt_FloorNo.Text);
                }
                else
                {
                    _Buiding.FloorNo = null;
                }
                _Buiding.Water = chk_Water.Checked;
                _Buiding.Electricity = chk_Electricity.Checked;
                _Buiding.Gas = chk_Gas.Checked;
                if (ddl_BuildingTypeId.SelectedItem != null)
                {
                    _Buiding.BuildingTypeId = int.Parse(ddl_BuildingTypeId.SelectedItem.Value.ToString());
                }
                else
                {
                    _Buiding.BuildingTypeId = null;
                }
                if (ddl_OwnerShipId.SelectedItem != null)
                {
                    _Buiding.OwnerShipId = int.Parse(ddl_OwnerShipId.SelectedItem.Value.ToString());
                }
                else
                {
                    _Buiding.OwnerShipId = null;
                }
                _Buiding.Notes = txt_Notes.Text;
                result = _Buiding.BuildingId;
                #endregion

            }


            else
            {
                if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                    return;
                }

                Buiding _Buiding = new Buiding();
                _Buiding.BuildingName = txt_BuildingName.Text;
                _Buiding.CompanyId = int.Parse(ddl_companyId.SelectedItem.Value.ToString());
                if (ddl_StreetId.SelectedItem != null)
                {
                    _Buiding.StreetId = int.Parse(ddl_StreetId.SelectedItem.Value.ToString());
                }
                else
                {
                    _Buiding.StreetId = null;
                }
                _Buiding.PieceNo = txt_PieceNo.Text;
                _Buiding.NorthBorder = txt_NorthBorder.Text;
                _Buiding.EasternBorder = txt_EasternBorder.Text;
                _Buiding.SouthBorder = txt_SouthBorder.Text;
                _Buiding.WesternBorder = txt_WesternBorder.Text;
                _Buiding.BuildingLicinseNo = txt_BuildingLicinseNo.Text;
                if (ddl_UsageId.SelectedItem != null)
                {
                    _Buiding.UsageId = int.Parse(ddl_UsageId.SelectedItem.Value.ToString());
                }
                else
                {
                    _Buiding.UsageId = null;
                }
                _Buiding.Area = txt_Area.Text;
                if (txt_FloorNo.Text != string.Empty)
                {
                    _Buiding.FloorNo = int.Parse(txt_FloorNo.Text);
                }
                else
                {
                    _Buiding.FloorNo = null;
                }
                _Buiding.Water = chk_Water.Checked;
                _Buiding.Electricity = chk_Electricity.Checked;
                _Buiding.Gas = chk_Gas.Checked;
                if (ddl_BuildingTypeId.SelectedItem != null)
                {
                    _Buiding.BuildingTypeId = int.Parse(ddl_BuildingTypeId.SelectedItem.Value.ToString());
                }
                else
                {
                    _Buiding.BuildingTypeId = null;
                }
                if (ddl_OwnerShipId.SelectedItem != null)
                {
                    _Buiding.OwnerShipId = int.Parse(ddl_OwnerShipId.SelectedItem.Value.ToString());
                }
                else
                {
                    _Buiding.OwnerShipId = null;
                }
                _Buiding.Notes = txt_Notes.Text;
                dc.Buidings.InsertOnSubmit(_Buiding);
                dc.SubmitChanges();
                result = _Buiding.BuildingId;


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



        protected void ddl_companyId_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void FillGrid()
        {
            if (ddl_companyId.SelectedItem != null)
            {
                gvBuildings.DataSource = from p in dc.Buidings where p.CompanyId == int.Parse(ddl_companyId.SelectedItem.Value.ToString()) select new { p.BuildingId, p.BuildingName };
                gvBuildings.DataBind();

            }
        }

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;



            BuildingID = Convert.ToInt32(gvBuildings.DataKeys[row.RowIndex].Value);

            var query = (from p in dc.Buidings where p.BuildingId.Equals(BuildingID) select new { p.Area, p.BuildingId, p.BuildingLicinseNo, p.BuildingName, p.BuildingTypeId, p.CompanyId, p.Electricity, p.FloorNo, p.NorthBorder, p.Notes, p.OwnerShipId, p.PieceNo, p.SouthBorder, p.StreetId, p.UsageId, p.Water, p.WesternBorder, p.EasternBorder, p.Street.DistrictId, p.Street.District.CityId, p.Gas }).SingleOrDefault();


            txt_BuildingName.Text = query.BuildingName;
            ddl_companyId.Value = query.CompanyId.ToString();
            ddl_StreetId.Value = query.StreetId.ToString();
            ddl_DistrictId.Value = query.DistrictId.ToString();
            ddl_CityId.Value = query.CityId.ToString();
            txt_PieceNo.Text = query.PieceNo;
            txt_BuildingLicinseNo.Text = query.BuildingLicinseNo;
            ddl_UsageId.Value = query.UsageId.ToString();
            ddl_BuildingTypeId.Value = query.BuildingTypeId.ToString();
            txt_Area.Text = query.Area;
            if (query.FloorNo != null)
            {
                txt_FloorNo.Text = query.FloorNo.ToString();
            }
            else
            {
                txt_FloorNo.Text = "";
            }
            chk_Electricity.Checked = bool.Parse(query.Electricity.ToString());
            chk_Water.Checked = bool.Parse(query.Water.ToString());
            chk_Gas.Checked = bool.Parse(query.Gas.ToString());
            ddl_OwnerShipId.Value = query.OwnerShipId.ToString();




            txt_NorthBorder.Text = query.NorthBorder;
            txt_SouthBorder.Text = query.SouthBorder;
            txt_EasternBorder.Text = query.EasternBorder;
            txt_WesternBorder.Text = query.WesternBorder;
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
            var query = from p in dc.Buidings where p.BuildingId.Equals(Convert.ToInt32(gvBuildings.DataKeys[row.RowIndex].Value)) select p;
            try
            {
                foreach (var item in query)
                {
                    dc.Buidings.DeleteOnSubmit(item);
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
