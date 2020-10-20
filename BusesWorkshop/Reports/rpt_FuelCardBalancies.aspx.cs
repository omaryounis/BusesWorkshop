using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusesWorkshop.DAL.Bus;
using BusesWorkshop.Pages.PagesPermissions;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Reports
{
    public partial class rpt_FuelCardBalancies : System.Web.UI.Page
    {
        #region "Properties" 
        WorkshopDataContext dc = new WorkshopDataContext();

        #endregion
        #region "Methods"
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Users.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.rpt_FuelCardBalancies.GetHashCode());
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
        }

        #endregion
    }
}
