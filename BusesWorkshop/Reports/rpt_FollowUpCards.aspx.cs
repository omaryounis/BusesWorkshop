using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusesWorkshop.DAL.Bus;
using System.Data;
using BusesWorkshop.Pages.PagesPermissions;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Reports
{
    public partial class rpt_FollowUpCards : System.Web.UI.Page
    {
        #region "Properties"
        WorkshopDataContext dc = new WorkshopDataContext();
        #endregion


        #region "Mehods" 
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
            DevFollowUpCard dev = new DevFollowUpCard();
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Users.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.rpt_FollowUpCards.GetHashCode());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["D"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["D"].ToString()))
                    {
                        Response.Redirect(@"..\Pages\Login.aspx", false);
                    }
                    //if (dt.Rows[0]["I"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["I"].ToString()))
                    //{

                    //    btnSave.Enabled = false;

                    //}
                    //if (dt.Rows[0]["U"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["U"].ToString()))
                    //{
                    //    GridViewUser.Columns[2].Visible = false;


                    //}
                    //if (dt.Rows[0]["R"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["R"].ToString()))
                    //{
                    //    GridViewUser.Columns[3].Visible = false;



                    //}
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

                Page.Title = "تقرير كارت المتابعة";
                FillVehcles();
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int? VehcleId = null;
            DateTime? StartDate = null;
            DateTime? EndDate = null;

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
            if (txt_ToDate.Text != "")
            {
                EndDate = DateTime.Parse(txt_ToDate.Text);
            }
            else
            {
                EndDate = null;
            }
            DevFollowUpCard rpt = new DevFollowUpCard(VehcleId, StartDate, EndDate);
        }
        #endregion

    }
}

