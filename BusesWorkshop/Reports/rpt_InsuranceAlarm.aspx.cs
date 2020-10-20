using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusesWorkshop.Reports
{
    public partial class rpt_InsuranceAlarm : System.Web.UI.Page
    {
        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "تقرير بالسيارات المطلوب تجديد التأمين لها";
            Dev_InsuranceAlarm rp = new Dev_InsuranceAlarm(int.Parse(Session["UserId"].ToString()));
        }
        #endregion
    }
}
