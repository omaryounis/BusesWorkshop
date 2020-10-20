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
using BusesWorkshop.DAL.Bus;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Pages
{
    public partial class UserCompanies : System.Web.UI.Page
    {

        #region "Properties"
        private int UserID
        {
            get
            {
                if (ViewState["_UserID"] != null)
                {
                    return Convert.ToInt32(ViewState["_UserID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_UserID", value);
            }
        }

        #endregion

        #region "Methods"

        private void fill_Users()
        {
            WorkshopDataContext dc = new WorkshopDataContext();

            DataTable dt = new DataTable();
            var query = from usr in dc.GetTable<User>() select usr;
            dt = query.CopyToDataTable();

            ddlUsers.Items.Clear();


            if (dt.Rows.Count > 0)
            {
                ddlUsers.DataSource = dt;
                ddlUsers.DataTextField = "Name";
                ddlUsers.DataValueField = "ID";
                ddlUsers.DataBind();

                ddlUsers.Items.Insert(0, new ListItem("---- اختر مستخدم ----", "0"));


            }
            else
            {
                ddlUsers.DataSource = null;

                ddlUsers.DataBind();
            }

        }

        private void fill_Companies()
        {
            WorkshopDataContext dc = new WorkshopDataContext();

            DataTable dt = new DataTable();
            var query = from cmp in dc.GetTable<Company>() select cmp;
            dt = query.CopyToDataTable();

            chkCompanies.Items.Clear();

            if (dt.Rows.Count > 0)
            {
                chkCompanies.DataSource = dt;
                chkCompanies.DataTextField = "CompName";
                chkCompanies.DataValueField = "ID";
                chkCompanies.DataBind();

            }

            else
            {
                chkCompanies.DataSource = null;
                chkCompanies.DataBind();

            }
        }

        private void fill_Grid()
        {
            WorkshopDataContext dc = new WorkshopDataContext();
            DataTable dt = new DataTable();
            dt = dc.usp_UserCompanies_Select(null, null).Select(row => new
            {
                UserID = row.UserID,
                UserName = row.UserName
            })
                        .Distinct().CopyToDataTable();


            if (dt.Rows.Count > 0)
            {
                gvUsers.DataSource = dt;
                gvUsers.DataBind();
            }
            else
            {
                gvUsers.DataSource = null;
                gvUsers.DataBind();
            }
        }

        void clear()
        {
            ddlUsers.SelectedIndex = 0;

            foreach (ListItem cb in chkCompanies.Items)
            {
                cb.Selected = false;
            }

        }

        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.UserCompanies.GetHashCode());
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
                        ViewState["AllowDelete"] = 0;
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
            Page.Title = "صلاحيات المستخدمين";
            WorkshopDataContext dc = new WorkshopDataContext();
            permissions(dc);

            if (!IsPostBack)
            {
                fill_Users();
                fill_Companies();
                fill_Grid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                return;
            }
            try
            {
                int usrID = 0;


                if (ddlUsers.SelectedIndex > 0)
                {
                    usrID = Convert.ToInt32(ddlUsers.SelectedValue);

                    WorkshopDataContext dc = new WorkshopDataContext();

                    var records = dc.UserCompanies.Where(item => item.UserID == usrID);
                    foreach (var item in records)
                        dc.UserCompanies.DeleteOnSubmit(item);



                    foreach (ListItem cb in chkCompanies.Items)
                    {
                        if (cb.Selected)
                        {
                            UserCompany tblUserComp = new UserCompany();
                            tblUserComp.CompID = Convert.ToInt32(cb.Value);
                            tblUserComp.UserID = usrID;
                            dc.UserCompanies.InsertOnSubmit(tblUserComp);
                            dc.SubmitChanges();
                        }
                    }
                }

                divMsg.Attributes["class"] = "alert alert-success text-right";
                lblResult.Text = "تم الحفظ بنجاح";

                usrID = 0;
            }
            catch 
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "حدث خطأ أثناء الحفظ";
            }

            
            clear();
        }

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            if (int.Parse(ViewState["AllowDelete"].ToString()) == 0 || ViewState["AllowDelete"] == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ليس لديك صلاحية الحذف";
                return;
            }
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;


            UserID = Convert.ToInt32(gvUsers.DataKeys[row.RowIndex].Value);

            WorkshopDataContext dc = new WorkshopDataContext();

            DataTable dt = new DataTable();
            dt = dc.usp_UserCompanies_Select(null, UserID).CopyToDataTable();


            if (dt.Rows.Count > 0)
            {
                ddlUsers.SelectedValue = UserID.ToString();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    foreach (ListItem cb in chkCompanies.Items )
                    {
                        if (cb.Value == dt.Rows[i]["CompID"].ToString())
                        {
                            
                            cb.Selected = true;
                        }
                    }
                }
            
            }
        }
        #endregion



    }
}
