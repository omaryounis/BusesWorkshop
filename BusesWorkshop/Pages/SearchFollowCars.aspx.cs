using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusesWorkshop.Pages.PagesPermissions;
using BusesWorkshop.DAL.Bus;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Pages
{
    public partial class SearchFollowCars : System.Web.UI.Page
    {
        #region "Properties"
        WorkshopDataContext dc = new WorkshopDataContext();
        #endregion

        #region "Methods"
        private void FillVehcles()
        {
            int? id = null;
            if (Session["UserID"] != null)
            {
                id = int.Parse(Session["UserID"].ToString());
            }
            else
            {
                Response.Redirect("../Pages/Login.aspx", false);
            }

            ddl_VehcleId.DataSource = from p in dc.Vehcles where p.Company.UserCompanies.Any(x => x.UserID == id) select new { p.VehcleId, p.PlateNo };
            ddl_VehcleId.TextField = "PlateNo";
            ddl_VehcleId.ValueField = "VehcleId";
            ddl_VehcleId.DataBind();

        }
        private void Search()
        {
            int? VehcleId = null;
            DateTime? StartDate = null;
            DateTime EndDate = DateTime.Now;

            if (ddl_VehcleId.SelectedItem != null)
            {
                VehcleId = int.Parse(ddl_VehcleId.SelectedItem.Value.ToString());
            }
            else
            {
                VehcleId = null;
            }




            if (txt_FromDate.Text != "")
            {
                StartDate = DateTime.Parse(txt_FromDate.Text);
            }
            else
            {
                StartDate = null;
            }



            grd_FollowCards.DataSource = dc.FollowUpCardBetweenTwoDates(VehcleId, StartDate, EndDate).ToList();
            grd_FollowCards.DataBind();
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.SearchFollowCars.GetHashCode());
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

        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            permissions(dc);
            if (!IsPostBack)
            {

                Page.Title = "بحث كروت المتابعة";
                FillVehcles();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
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




            int CustodyId = Convert.ToInt32(grd_FollowCards.DataKeys[row.RowIndex].Value);
            //الحصول على رقم السيارة

            int VehcleId = dc.FollowUpCards.Single(x => x.FollowUpCardId == CustodyId).VehcleId;

            //Confirm Is Last Follow Card

            int LastFollowCardId = dc.FollowUpCards.ToList().Last(x => x.VehcleId == VehcleId).FollowUpCardId;

            if (CustodyId != LastFollowCardId)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا الكارت المختار ليس اخر كارت";
                return;
            }



            //Delete Fuel CARDS
            List<FuelSupply> _FuelSupply = dc.FuelSupplies.Where(x => x.FollowUpCardId == CustodyId).ToList();

            foreach (var item in _FuelSupply)
            {
                dc.FuelSupplies.DeleteOnSubmit(item);
            }
            dc.SubmitChanges();




            //dELETE FOLLOW CARDS
            FollowUpCard _FollowUpCard = dc.FollowUpCards.Single(x => x.FollowUpCardId == CustodyId);
            dc.FollowUpCards.DeleteOnSubmit(_FollowUpCard);
            dc.SubmitChanges();



            //Update Current Reading IN VEHCLES
            FollowUpCard _FollowUpCardlAST = dc.FollowUpCards.ToList().LastOrDefault();
            if (_FollowUpCardlAST == null)
            {
                int? CounterReadingStart = dc.Vehcles.Single(x => x.VehcleId == VehcleId).CounterReadingStart;
                Vehcle _vehcle = dc.Vehcles.Single(x => x.VehcleId == VehcleId);
                _vehcle.CurrentReading = CounterReadingStart;
                dc.SubmitChanges();
            }
            else
            {
                Vehcle _vehcle = dc.Vehcles.Single(x => x.VehcleId == VehcleId);
                _vehcle.CurrentReading = _FollowUpCardlAST.CounterReading;
                dc.SubmitChanges();


            }
            Search();

        }

        protected void grd_FollowCards_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grd_FollowCards_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {



                if (grd_FollowCards.Rows.Count > 0)
                {
                    var lastRow = grd_FollowCards.Rows[grd_FollowCards.Rows.Count - 1];
                    lastRow.FindControl("lnk_Delete").Visible = false;
                }




            }
        }
        #endregion


    }
}

