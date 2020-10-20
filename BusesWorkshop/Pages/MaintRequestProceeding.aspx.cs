using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusesWorkshop.DAL.Bus;
using DevExpress.Web;

namespace BusesWorkshop.Pages
{
    public partial class MaintRequestProceeding : System.Web.UI.Page
    {
        #region "Events"

        protected void Page_Load(object sender, EventArgs e)
        {
            WUCMaintRequestProcess.reqPage = MaintRequestProcessingTabPage; 
        }
            protected void MaintRequestProcessingTabPage_ActiveTabChanged(object source, TabControlEventArgs e)
        {
            if (MaintRequestProcessingTabPage.ActiveTabIndex == 0)
            {
                WUCMaintRequestProcess.reqPage.ActiveTabIndex = 0;
                Session["reqPageTabIndex"] = 0;
                // WUCMaintRequestProcess.grd_Requests_SelectedIndexChanged(object sender, EventArgs e);
                WUCMaintRequestProcess.FillGridRequests();
            }
            else if (MaintRequestProcessingTabPage.ActiveTabIndex == 1)
            {
                Session["reqPageTabIndex"] = 1;
                WUCMaintRequestProcess.reqPage.ActiveTabIndex = 1;
                WUCSupportRequestProcess1.FillGridRequests();
            }
        }
        #endregion

        protected void MaintRequestProcessingTabPage_TabClick(object source, TabControlCancelEventArgs e)
        {
            if ( MaintRequestProcessingTabPage.ActiveTabIndex == 0)
            {
                
                WUCMaintRequestProcess.FillGridRequests();
            }
            else if (MaintRequestProcessingTabPage.ActiveTabIndex == 1)
            {

                WUCSupportRequestProcess1.FillGridRequests();
            }
        }
    }
}
