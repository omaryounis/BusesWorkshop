using BusesWorkshop.DAL;
using BusesWorkshop.DAL.Bus;
using BusesWorkshop.Pages.PagesPermissions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusesWorkshop.Reports
{
    public partial class rpt_InsuranceAlarm : System.Web.UI.Page
    {
        WorkshopDataContext dc = new WorkshopDataContext();
        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            permissions(dc);
            Page.Title = "تقرير بالسيارات المطلوب تجديد التأمين لها";
            Dev_InsuranceAlarm rp = new Dev_InsuranceAlarm(int.Parse(Session["UserId"].ToString()));
        }
        #endregion


        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Users.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.rpt_InsuranceAlarm.GetHashCode());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["D"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["D"].ToString()))
                    {
                        Response.Redirect(@"..\Pages\Login.aspx", false);
                    }

                }
                else
                { Response.Redirect(@"..\Pages\Login.aspx", false); }
            }
            catch
            {
                Response.Redirect(@"..\Pages\Login.aspx");
            }
        }
    }
}
