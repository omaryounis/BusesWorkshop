using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Group = BusesWorkshop.DAL.Group;
using BusesWorkshop.DAL.Bus;
using BusesWorkshop.DAL;
using System.Data;

namespace BusesWorkshop.Pages.PagesPermissions
{
    public partial class Groups : System.Web.UI.Page
    {
        #region Declaration

        /// <summary>
        /// Group ID.
        /// </summary>
        private int ID
        {
            get
            {
                return ((int)ViewState["groupID"]);
            }
            set
            {
                ViewState["groupID"] = value;
            }
        }

       #endregion

       

        #region Page Event

        /// <summary>
        /// Load Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "المجموعة";
            WorkshopDataContext dc = new WorkshopDataContext(); 
            permissions(dc);
            if (!Page.IsPostBack)
            {
                ID = 0;
                FillGrid(dc);
            }
        }

        #endregion

        #region Method

        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.Groups.GetHashCode());
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

        
        /// <summary>
        /// Fill Grid
        /// </summary>
        /// <param name="dc"></param>
        private void FillGrid(WorkshopDataContext dc)
        {
            gvGroup.DataSource = Group.Select(dc, null);
            gvGroup.DataBind();
        }
        #endregion

        #region Events

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
            WorkshopDataContext dc = new WorkshopDataContext();
            Group grp = new Group();
            grp.Name = txtGroup.Text.Trim();

            if (ID == 0)
            {
                ID = grp.Insert(dc);
                if (ID >= 0)
                {
                    DataTable dt = PageActions.Select(dc, null);
                    foreach (DataRow item in dt.Rows)
                    {
                        GroupPermession gp = new GroupPermession();
                        gp.GroupID = ID;
                        gp.PageID = Convert.ToInt32(item["Page_ID"].ToString());

                        if (bool.Parse(item["Display"].ToString()) == true)
                        {
                            gp.Display = false;
                        }
                        else
                        {
                            gp.Display = null;
                        }

                        if (bool.Parse(item["InsertA"].ToString()) == true)
                        {
                            gp.InsertA = false;
                        }
                        else
                        {
                            gp.InsertA = null;
                        }

                        if (bool.Parse(item["UpdateA"].ToString()) == true)
                        {
                            gp.UpdateA = false;
                        }
                        else
                        {
                            gp.UpdateA = null;
                        }

                        if (bool.Parse(item["DeleteA"].ToString()) == true)
                        {
                            gp.DeleteA = false;
                        }
                        else
                        {
                            gp.DeleteA = null;
                        }

                        gp.Insert(dc);
                    }
                    txtGroup.Text = string.Empty;
                    ID = 0;
                    FillGrid(dc);
                }
                else
                {
                    DivAlert.Visible = true;
                    lblResult.Visible = true;
                   // lblResult.Text = "this Group exists before.";
                }
            }
            else
            {
                grp.ID = ID;
                int result = grp.Update(dc);
                if (result == -1)
                {
                    DivAlert.Visible = true;
                    lblResult.Visible = true;//Text = "this Group exists before.";
                }
                else
                {
                    txtGroup.Text = string.Empty;
                    ID = 0; 
                    btnSave.Text = "حفظ";
                    FillGrid(dc);
                }
            }
            permissions(dc);
        }

        /// <summary>
        /// Set Selected Data To Controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnGroupEdit_Click(object sender, EventArgs e)
        {

            btnSave.Text = "تعديل";
            WorkshopDataContext dc = new WorkshopDataContext();
            LinkButton lnk = (LinkButton)sender;
            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;

            txtGroup.Text = clickedRow.Cells[1].Text.Trim();
            ID = Convert.ToInt32(clickedRow.Cells[0].Text.Trim());
            txtGroup.Enabled = true;
            btnSave.Enabled = true;
        }

        /// <summary>
        /// Delete From Grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnGroupDelete_Click(object sender, EventArgs e)
        {
            if (int.Parse(ViewState["AllowDelete"].ToString()) == 0 || ViewState["AllowDelete"] == null)
            {
             DivAlert .Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ليس لديك صلاحية الحذف";
                return;
            }
            WorkshopDataContext dc = new WorkshopDataContext();
            LinkButton lnk = (LinkButton)sender;
            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            Group gp = new Group();
            gp.ID = Convert.ToInt32(clickedRow.Cells[0].Text.Trim());
            int result = gp.Delete(dc);
            if (result > 0)
            {
                gp.Delete(dc);
                FillGrid(dc);
            }
        }

        #endregion


    }
}
