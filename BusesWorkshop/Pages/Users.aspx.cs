using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using BusesWorkshop.DAL.Bus;
using System.Data.SqlClient;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Pages.PagesPermissions
{
    public partial class Users : System.Web.UI.Page
    {
        #region "Properties"
        ClsUsers clsuser = new ClsUsers();

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
        #region"Methods"
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Users.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.Users.GetHashCode());
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
            }
            catch
            {
                Response.Redirect(@"..\Pages\Login.aspx");
            }
        }

        private void BindUsersGridview()
        {

            GridViewUser.DataSource = clsuser.SelectUsers();
            GridViewUser.DataBind();
        }
        private void fillControls(int uID)
        {
            DataTable dt = clsuser.SelectUsers(uID);
            if (dt != null)
            {
                txtName.Text = dt.Rows[0]["Name"].ToString();

                txtPasword.Text = dt.Rows[0]["UserPassword"].ToString();

                //txtPasword.TextMode = TextBoxMode.Password;

                txtUserName.Text = dt.Rows[0]["UserName"].ToString();

                chk_IsActive.Checked = bool.Parse(dt.Rows[0]["IsActive"].ToString());
              
            }

        }

        private void emptyControls()
        {
            txtName.Text = string.Empty;
            txtPasword.Text = string.Empty;
            txtUserName.Text = string.Empty;
          
        }
        #endregion

        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "بيانات المستخدمين";
            WorkshopDataContext dc = new WorkshopDataContext();
            permissions(dc);

            if (!Page.IsPostBack)
            {
               // BindBranches();
                BindUsersGridview();

            
                DropDownListBrnachID.SelectedIndex = 0;

            }


        }
       
        protected void Button1_Click(object sender, EventArgs e)
        {

            if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
            {
               divMsg2 .Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                return;
            }

            int result = 0;


            if (UserID > 0)
            {
                result = clsuser.Update_User(UserID,
                                              txtName.Text.Trim(),
                                              txtUserName.Text.Trim(),
                                              txtPasword.Text.Trim(),chk_IsActive.Checked);
                UserID = 0;


            }
            else
            {
                result = clsuser.InsertUser( txtName.Text.Trim(),
                                              txtUserName.Text.Trim(),
                                              txtPasword.Text.Trim() , chk_IsActive.Checked);

            }

            if (result > 0)
            {
                divMsg2.Attributes["class"] = "alert alert-success text-right";

                lblResult.Text = "تم الحفظ بنجاح";

            }
            else
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";

                lblResult.Text = "فشل في الحفظ";
            }

            BindUsersGridview();
            emptyControls();
        }

        protected void GridViewUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int result = 0;
            UserID = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditRow")
            {
                
                fillControls(UserID);

            }
            else if (e.CommandName == "DeleteRow")
            {
                
                result = clsuser.Delete(UserID);

                if (result > 0)
                {
                    lblResult.Text = "تم الحذف بنجاح";
                }
                else
                {
                    lblResult.Text = "فشل في عملية الحذف";
                }
                BindUsersGridview();

            }
        }

        /// <summary>
        /// Get User Permission for specific page
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="userID"></param>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public static DataTable GetUserPermission(WorkshopDataContext dc, int userID, int pageID)
        {
            DataTable permission = (from per in dc.usp_User_PagesPermission(userID, pageID)
                                    select new
                                    {
                                        D = per.Display,
                                        I = per.InsertA,
                                        U = per.UpdateA,
                                        R = per.DeleteA
                                    }).CopyToDataTable();

            return permission;
        }
        /// <summary>
        /// Select
        /// </summary>
        /// <param name="dc">dc</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static DataTable Select(WorkshopDataContext dc )
        {
            DataTable dt = dc.usp_Users_Select().CopyToDataTable();
            return dt;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            WorkshopDataContext dc = new WorkshopDataContext();
            if (int.Parse(ViewState["AllowDelete"].ToString()) == 0 || ViewState["AllowDelete"] == null)
            {
               divMsg2 .Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ليس لديك صلاحية الحذف";
                return;
            }
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;
            var query = from p in dc.Users where p.ID.Equals(Convert.ToInt32(GridViewUser .DataKeys[row.RowIndex].Value)) select p;
            try
            {
                foreach (var item in query)
                {
                    dc.Users.DeleteOnSubmit(item);
                }
                dc.SubmitChanges();
            }
            catch (SqlException ex)
            {

                if (ex.Errors[0].Number == 547)
                {
                   divMsg2 .Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفو السجل المراد حذفة مرتبط بمدخلات اخري";
                    return;
                }


            }


            BindUsersGridview();
        }
        #endregion
    }

}
