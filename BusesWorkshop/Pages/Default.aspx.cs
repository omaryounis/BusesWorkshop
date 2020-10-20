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
namespace BusesWorkshop.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        WorkshopDataContext dc = new WorkshopDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            dc.AlarmSave();
         // dc.MaintReqAlarmSave();

        }
    }
}
