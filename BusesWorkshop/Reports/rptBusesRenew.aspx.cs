using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace BusesWorkshop.Reports
{
    public partial class rptBusesRenew : System.Web.UI.Page
    {
        #region "Methods"
        void BindReport()
        {

            Dev_rptBusesRenew dev = new Dev_rptBusesRenew();
        }
        #endregion

        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null && Convert.ToInt32(Session["UserID"].ToString()) > 0)
                {
                    BindReport();

                }
                else
                {
                    Response.Redirect(@"..\Pages\Login.aspx");
                }

                
            }
        }

        #endregion
    }
}
