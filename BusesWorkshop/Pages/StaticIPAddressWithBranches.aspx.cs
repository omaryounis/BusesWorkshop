using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusesWorkshop.DAL.Bus;
using System.Data;
using System.Data.SqlClient;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Pages
{
    public partial class StaticIPAddressWithBranches : System.Web.UI.Page
    {
        WorkshopDataContext dc = new WorkshopDataContext();
        private int StaticIPAddressWithBranchID
        {
            get
            {
                if (ViewState["_StaticIPAddressWithBranchID"] != null)
                {
                    return Convert.ToInt32(ViewState["_StaticIPAddressWithBranchID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_StaticIPAddressWithBranchID", value);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            permissions(dc);
            Page.Title = "تهيئة عنوان الشبكه للفروع";

            if (!IsPostBack)
            {
                FillCompanies();
                txt_StaticIP1.Text = null;
                txt_StaticIP2.Text = null;
                txt_StaticIP3.Text = null;
                ddl_CompanyID.Value = null;
                FillGrid();
            }
        }
        private void FillCompanies()
        {

            ddl_CompanyID.DataSource = from p in dc.Companies select new { p.ID, p.CompName };
            ddl_CompanyID.TextField = "CompName";
            ddl_CompanyID.ValueField = "ID";
            ddl_CompanyID.DataBind();

        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()),  Common.PagesEnum.StaticIPAddressWithBranches.GetHashCode());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["D"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["D"].ToString()))
                    {
                        Response.Redirect(@"..\Pages\Login.aspx", false);
                    }
                    if (dt.Rows[0]["D"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["D"].ToString()))
                    {
                        ViewState["AllowDelete"] = 0;
                    }
                    else
                    {
                        ViewState["AllowDelete"] = 1;
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;
            #region validation
            if (ddl_CompanyID.SelectedItem == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل اسم الفرع";
                return;
            }
            if (!IsDigitsOnly(txt_StaticIP1.Text) || !IsDigitsOnly(txt_StaticIP2.Text) || !IsDigitsOnly(txt_StaticIP3.Text))
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا خطأ في كتابة عنوان الشبكه";
                return;
            }
            #endregion
            if (StaticIPAddressWithBranchID > 0)
            {
                #region Update
                if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية التعديل";
                    return;
                }
                StaticIPAddressWithBranch _StaticIPAddressWithBranch =
                dc.StaticIPAddressWithBranches.Single(x => x.ID == StaticIPAddressWithBranchID);
                _StaticIPAddressWithBranch.StaticIP = txt_StaticIP1.Text + "." + txt_StaticIP2.Text + "." + txt_StaticIP3.Text;
                _StaticIPAddressWithBranch.CompanyID = int.Parse(ddl_CompanyID.SelectedItem.Value.ToString());
                dc.SubmitChanges();
                result = _StaticIPAddressWithBranch.ID;
                #endregion

            }
            else
            {
                #region save
                if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                    return;
                }
                StaticIPAddressWithBranch _StaticIPAddressWithBranch = new StaticIPAddressWithBranch();
                _StaticIPAddressWithBranch.StaticIP = txt_StaticIP1.Text + "." + txt_StaticIP2.Text + "." + txt_StaticIP3.Text;
                _StaticIPAddressWithBranch.CompanyID = int.Parse(ddl_CompanyID.SelectedItem.Value.ToString());
                _StaticIPAddressWithBranch.InsertDate = DateTime.Now;
                _StaticIPAddressWithBranch.IsDeleted = false;
                _StaticIPAddressWithBranch.UserID = int.Parse(Session["UserID"].ToString());

                dc.StaticIPAddressWithBranches.InsertOnSubmit(_StaticIPAddressWithBranch);
                dc.SubmitChanges();
                result = _StaticIPAddressWithBranch.ID;
                #endregion

            }

            if (result > 0)
            {
                divMsg.Attributes["class"] = "alert alert-success text-right";
                lblResult.Text = "تم الحفظ بنجاح";

            }
            else
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "حدث خطأ أثنا الحفظ";
                // divMsg. = "alert alert-danger text-right"; 
            }
            FillGrid();
            MyClasses.ClearControls(Page);
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private void FillGrid()
        {

            gvStaticIPAddressWithBranches.DataSource = from p in dc.StaticIPAddressWithBranches
                                                       where p.IsDeleted == false
                                                       select new { p.StaticIP, p.Company.CompName, p.ID };
            gvStaticIPAddressWithBranches.DataBind();

        }
        public void FilterGrid()
        {
            if (ddl_CompanyID.SelectedItem != null)
            {
                gvStaticIPAddressWithBranches.DataSource = from p in dc.StaticIPAddressWithBranches
                                                           where p.CompanyID == int.Parse(ddl_CompanyID.SelectedItem.Value.ToString()) && p.IsDeleted == false
                                                           select new { p.StaticIP, p.Company.CompName, p.ID };
                gvStaticIPAddressWithBranches.DataBind();
            }
        }

        protected void ddl_CompanyID_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }


        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;

            StaticIPAddressWithBranchID = Convert.ToInt32(gvStaticIPAddressWithBranches.DataKeys[row.RowIndex].Value);

            var query = (from p in dc.StaticIPAddressWithBranches where p.ID.Equals(StaticIPAddressWithBranchID) select new { p.StaticIP, p.CompanyID }).SingleOrDefault();
            var StaticIpFromDB = query.StaticIP.Split('.');
            txt_StaticIP1.Text = StaticIpFromDB[0];
            txt_StaticIP2.Text = StaticIpFromDB[1];
            txt_StaticIP3.Text = StaticIpFromDB[2];
            ddl_CompanyID.Value = query.CompanyID.ToString();
        }

        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            if (int.Parse(ViewState["AllowDelete"].ToString()) == 0 || ViewState["AllowDelete"] == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ليس لديك صلاحية الحذف";
                return;
            }
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;
            var query = from p in dc.StaticIPAddressWithBranches where p.ID.Equals(Convert.ToInt32(gvStaticIPAddressWithBranches.DataKeys[row.RowIndex].Value)) select p;
            try
            {
                foreach (var item in query)
                {
                    dc.StaticIPAddressWithBranches.DeleteOnSubmit(item);
                }
                dc.SubmitChanges();
            }

            catch (SqlException ex)
            {

                if (ex.Errors[0].Number == 547)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفو السجل المراد حذفة مرتبط بمدخلات اخري";
                    return;
                }


            }


            FillGrid();
        }
    }
}
