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
using System.Data.SqlClient;
using BusesWorkshop.Pages.PagesPermissions;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Reports
{
    public partial class Rpt_SuperCardsBalance : System.Web.UI.Page
    {
        #region "Properties"
        WorkshopDataContext dc = new WorkshopDataContext();
        #endregion
        #region "Methods"
        private void FillMainSuperVisors()
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
            ddl_EmployeeId.DataSource = from p in dc.Users where p.ID == id select new { p.ID, p.Name };
            ddl_EmployeeId.TextField = "Name";
            ddl_EmployeeId.ValueField = "ID";
            ddl_EmployeeId.DataBind();
        }

        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Users.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.Rpt_SuperCardsBalance.GetHashCode());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["D"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["D"].ToString()))
                    {
                        Response.Redirect(@"..\Pages\Login.aspx", false);
                    }
                  
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
                FillMainSuperVisors();
            
            }
          

        }
     
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddl_EmployeeId.SelectedItem != null)
            {
                Dev_SuperVisorsCardsBalance rpt = new Dev_SuperVisorsCardsBalance(int.Parse(ddl_EmployeeId.SelectedItem.Value.ToString()), ddl_EmployeeId.SelectedItem.Text.ToString());
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('عفوا ادخل اسم الموظف')", true);
                return;
            }
        }
        #endregion
    }
}
