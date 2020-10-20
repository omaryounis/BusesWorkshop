using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Group = BusesWorkshop.DAL.Group;
using BusesWorkshop.DAL.Bus;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Pages.PagesPermissions
{
    public partial class GroupPermission : System.Web.UI.Page
    {
        

        #region Page Event

        /// <summary>
        /// Load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "صلاحيات المجموعات";
            WorkshopDataContext dc = new WorkshopDataContext(); 
            permitions(dc);
            if (!Page.IsPostBack)
            {
                FillGroup(dc);
            }
           
        }

        #endregion

        #region Events

        /// <summary>
        /// Selected Index.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex > 0)
            {
                gvGroup.DataSource = GroupPermession.SelectGroup(new WorkshopDataContext(), Convert.ToInt32(ddlGroup.SelectedValue.ToString()));
                gvGroup.DataBind();
            }
        }


        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('عفوا ليس لديك صلاحية التعديل')", true);
                return;
            }
            foreach (GridViewRow item in gvGroup.Rows)
            {
                int result = 0;
                GroupPermession group = new GroupPermession();
                group.ID = Convert.ToInt32(item.Cells[0].Text);
                group.GroupID = Convert.ToInt32(item.Cells[1].Text);
                group.PageID = Convert.ToInt32(item.Cells[2].Text);
                CheckBox chkDisplay = (CheckBox)item.FindControl("chkDisplay");
                if (chkDisplay.Enabled)
                    {group.Display = chkDisplay.Checked; }
               

                CheckBox chkInsert = (CheckBox)item.FindControl("chkInsert");
                if (chkInsert.Enabled)
                { group.InsertA = chkInsert.Checked; }

                CheckBox chkUpdate = (CheckBox)item.FindControl("chkUpdate");
                if (chkUpdate.Enabled)
                { group.UpdateA = chkUpdate.Checked; }

                CheckBox chkDelete = (CheckBox)item.FindControl("chkDelete");
                if (chkDelete.Enabled)
                { group.DeleteA = chkDelete.Checked; }
                result = group.Update(new WorkshopDataContext());
                if (result > 0)
                {
                    mpeMessage.Show();
                }
            }
        }
        protected void gvGroup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {

                DataTable dt = GroupPermession.SelectGroup(new WorkshopDataContext(), Convert.ToInt32(ddlGroup.SelectedValue.ToString()));
                if (dt.Rows[e.Row.RowIndex][6].ToString() == "0")
                {
                    CheckBox chkDisplay = (CheckBox)e.Row.FindControl("chkDisplay");
                    chkDisplay.Enabled = false;
                }
                if (dt.Rows[e.Row.RowIndex][8].ToString() == "0")
                {
                    CheckBox chkInsert = (CheckBox)e.Row.FindControl("chkInsert");
                    chkInsert.Enabled = false;
                }
                if (dt.Rows[e.Row.RowIndex][10].ToString() == "0")
                {
                    CheckBox chkUpdate = (CheckBox)e.Row.FindControl("chkUpdate");
                    chkUpdate.Enabled = false;
                }
                if (dt.Rows[e.Row.RowIndex][12].ToString() == "0")
                {
                    CheckBox chkDelete = (CheckBox)e.Row.FindControl("chkDelete");
                    chkDelete.Enabled = false;
                }
            }
        }
        #endregion

        #region Methods

        private void permitions(WorkshopDataContext dc)
        {
            DataTable dt = Users.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.GroupPermission.GetHashCode());
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

        /// <summary>
        /// Fill Group.
        /// </summary>
        /// <param name="dc"></param>
        private void FillGroup(WorkshopDataContext dc)
        {
            ddlGroup.DataSource = Group.Select(dc, null);
            ddlGroup.DataValueField = "ID";
            ddlGroup.DataTextField = "Name";
            ddlGroup.DataBind();
            ddlGroup.Items.Insert(0, new ListItem("Choose"));
        }

        /// <summary>
        /// Enable check box false while feild equal null.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected bool check(object obj)
        {
            bool ss = true;
            if (obj.ToString() == string.Empty)
            {
                ss = false;
            }
            return ss;
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.GroupPermission.GetHashCode());
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

        #endregion

     

      


    }
}
