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
using System.Collections.Generic;
using System.Collections;

namespace BusesWorkshop.Pages
{
    public partial class Login : System.Web.UI.Page
    {

        #region "Methods"
        bool Check_Login()
        {
            bool result = false;


            if (!string.IsNullOrEmpty(txtName.Text.Trim()) && !string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {


                WorkshopDataContext dc = new WorkshopDataContext();

                DataTable dt = new DataTable();

                var query = from usr in dc.GetTable<User>().Where(us => us.UserName == txtName.Text && us.UserPassword == txtPassword.Text) select new { usr.ID, usr.UserName };
                dt = query.CopyToDataTable();
                if (dt.Rows.Count > 0)
                {
                    result = true;
                    Session["UserID"] = dt.Rows[0]["ID"];
                    Session["UserName"] = dt.Rows[0]["UserName"];
                    DAL.Common.UserIDForSec = int.Parse(dt.Rows[0]["ID"].ToString());


                

                    DataTable CompIds = (from p in dc.UserCompanies
                                         where p.UserID == DAL.Common.UserIDForSec
                                         select p.CompID).CopyToDataTable();
                  
                }
                else
                {
                    result = false;
                }

            }
            return result;
            ViewState["EmpID"] = 1;
        }
        #endregion
        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                        Session.Abandon();

            }
            Page.Title = "الدخول";
            Session["UserID"] = null;

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Check_Login())
            {
                Response.Redirect(@"../pages/Default.aspx");
            }
            else
            {
                lblmsg.Text = "اسم المستخدم أو كلمة المرور غير صحيح";

            }
        }
        #endregion
    }
}
