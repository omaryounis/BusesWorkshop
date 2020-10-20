using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using BusesWorkshop.DAL.Bus;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Pages.PagesPermissions
{
    public partial class UserGroup : System.Web.UI.Page
    {
        /// <summary>
        /// User Group ID.
        /// </summary>
        /// 
        #region "Properties"
        private long ID
        {
            get
            {
                return ((long)ViewState["userGroupID"]);
            }
            set
            {

                ViewState["userGroupID"] = value;

            }
        }
        #endregion
        #region "Methods"
        private void permitions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Users.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.UserGroup.GetHashCode());
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

        public void SetControls(bool modifer)
        {
            ddlGroup.Enabled = modifer;
            ddlUser.Enabled = modifer;
            btnSave.Enabled = modifer;
        }


        /// <summary>
        /// FillGrid.
        /// </summary>
        /// <param name="dc"></param>
        private void FillGrid(WorkshopDataContext dc)
        {
            gvUserGroup.DataSource = UserGroups.Select(dc, null);
            gvUserGroup.DataBind();
        }

        /// <summary>
        /// Fill Group.
        /// </summary>
        /// <param name="dc"></param>
        private void FillGroup(WorkshopDataContext dc)
        {
            ddlGroup.DataSource = DAL.Group.Select(dc, null);
            ddlGroup.DataValueField = "ID";
            ddlGroup.DataTextField = "Name";
            ddlGroup.DataBind();
            ddlGroup.Items.Insert(0, new ListItem("Choose", "-1"));
        }

        /// <summary>
        /// Fill Page.
        /// </summary>
        /// <param name="dc"></param>
        private void FillUser(WorkshopDataContext dc)
        {
            ddlUser.DataSource = Users.Select(dc);
            ddlUser.DataValueField = "ID";
            ddlUser.DataTextField = "Name";
            ddlUser.DataBind();
            ddlUser.Items.Insert(0, new ListItem("Choose", "-1"));
        }

        #endregion
        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "مجموعات المستخدمين";
            WorkshopDataContext dc = new WorkshopDataContext();
            permitions(dc);
            if (!Page.IsPostBack)
            {
                ID = 0;
                FillUser(dc);
                FillGroup(dc);
                FillGrid(dc);
            }
        }



        /// <summary>
        /// Save and update.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
            {
               DivAlert .Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                return;
            }

            long result = 0;
            DivAlert.Visible = false;
            lblResult.Visible = false;
            WorkshopDataContext dc = new WorkshopDataContext();
            UserGroups user = new UserGroups();
            user.UserID = Convert.ToInt32(ddlUser.SelectedValue.ToString());
            user.GroupID = Convert.ToInt32(ddlGroup.SelectedValue.ToString());
            if (ID == 0)
            {
                result = user.Insert(dc);
            }
            else
            {
                user.ID = ID;
                result = user.Update(dc);
            }
            if (result == -1)
            {
           
                DivAlert.Visible = true;
                lblResult.Visible = true;
               
            }
            else if (result > 0)
            {
                ddlGroup.SelectedIndex = 0;
                ddlUser.SelectedIndex = 0;
                FillGrid(dc);
                btnSave.Text = "حفظ";
                ID = 0;
                permitions(dc);
            }
        }


        /// <summary>
        /// Set Selected Data To Controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnUserGroupEdit_Click(object sender, EventArgs e)
        {

            WorkshopDataContext dc = new WorkshopDataContext();
            LinkButton lnk = (LinkButton)sender;
            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            ddlUser.SelectedValue = clickedRow.Cells[1].Text;
            ddlGroup.SelectedValue = clickedRow.Cells[2].Text;
            ID = long.Parse(clickedRow.Cells[0].Text);
            btnSave.Text = "تعديل";
            SetControls(true);
        }

        protected void lbtnUserGroupDelete_Click(object sender, EventArgs e)
        {
            WorkshopDataContext dc = new WorkshopDataContext();
            LinkButton lnk = (LinkButton)sender;
            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            UserGroups user = new UserGroups();
            user.ID = long.Parse(clickedRow.Cells[0].Text);
            long result = user.Delete(dc);
            if (result > 0)
            {
                FillGrid(dc);
            }
        }
        #endregion
    }
}
