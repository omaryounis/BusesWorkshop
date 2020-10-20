using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusesWorkshop.DAL.Bus;
using Accounting.DAL;
using System.Data;

using BusesWorkshop.DAL.Accounting;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Pages
{
    public partial class AccAstLocations : System.Web.UI.Page
    {
        private int UserID
        {
            get
            {
                if (Session["UserID"] != null)
                {
                    return Convert.ToInt32(Session["UserID"]);
                }
                else
                {
                    return 0;
                }
            }
        }
        WorkshopDataContext dcc = new WorkshopDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            permissions(dcc);
            if (!IsPostBack)
            {
                FillParentLocs();
                FillCompanies();
            }
            BindTree();

        }

        private void BindTree()
        {
            dcAccountingDataContext dc = new dcAccountingDataContext();
            ASPxTreeList1.SettingsBehavior.AutoExpandAllNodes = false;
            ASPxTreeList1.DataSource = dc.usp_AstLocations_Select_ByUserID(UserID).CopyToDataTable();
            ASPxTreeList1.ParentFieldName = "ParentID";
            ASPxTreeList1.KeyFieldName = "ID";
            ASPxTreeList1.DataBind();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            dcAccountingDataContext dc = new dcAccountingDataContext();
            AstLocations alc = new AstLocations();
            int? nullable = null;
            alc.LocationName = txtLocationName.Text;
            alc.ParentID = drpParentAst.SelectedIndex > 0 ? Convert.ToInt32(drpParentAst.SelectedValue) : nullable;
            alc.IsMain = radioListLocType.SelectedIndex == 1 ? true : false;
            alc.IsShown = true;
            alc.IsDefault = true;
            alc.CompanyID = Convert.ToInt32(drpCompanies.SelectedValue);

            if (ViewState["LocID"] != null)
            {
                alc.ID = Convert.ToInt32(ViewState["LocID"]);
                alc.Status = 2;
            }
            else
            {
                alc.Status = 1;
            }
            int result = alc.SaveAstLocation(dc);
            if (result > 0)
            {

                LabelOutputMsg.Text = "تم الحفظ بنجاح";
                alertConfirmDiv.Visible = true;
                alertErrorDiv.Visible = false;
                BindTree();
                FillParentLocs();
                ClearFields();

            }
            else
            {
                lblErrorMsg.Text = "حدثت خطأ اثناء الحفظ";
                alertConfirmDiv.Visible = false;
                alertErrorDiv.Visible = true;
                dc.Connection.Close();
            }
        }

        private void FillFields(int astID)
        {
            dcAccountingDataContext dc = new dcAccountingDataContext();
            AstLocations alc = new AstLocations();
            alc.ID = astID;
            alc.UserID = UserID;
            List<DAL.Accounting.usp_AstLocations_Select_ByUserIDResult> list = alc.SelectAstLocationByID(dc);
            txtLocationName.Text = list[0].LocationName;
            drpCompanies.SelectedValue = list[0].Com_ID.ToString();
            drpParentAst.SelectedValue = list[0].ParentID.ToString();
            radioListLocType.SelectedIndex = list[0].IsMain == true ? 1 : 0;
        }

        private void ClearFields()
        {
            txtLocationName.Text = string.Empty;
            //drpCompanies.SelectedIndex = 0;
            drpCompanies.SelectedValue = clsCompany.GetDefaultCompany(UserID);
            drpParentAst.SelectedIndex = 0;
            radioListLocType.SelectedIndex = -1;
            drpParentAst.SelectedIndex = 0;
        }

        private void BindLocatios()
        {
            AstLocations alc = new AstLocations();
            alc.UserID = UserID;
            dcAccountingDataContext dc = new dcAccountingDataContext();
            alc.SelectAstLocation(dc);
        }

        private void FillCompanies()
        {
            drpCompanies.Items.Clear();
            drpCompanies.DataSource = Accounting.DAL.clsCompany.Select(UserID);
            drpCompanies.DataTextField = "COMName";
            drpCompanies.DataValueField = "COMID";
            drpCompanies.DataBind();
            drpCompanies.Items.Insert(0, new ListItem("--اختر--", "0"));
            drpCompanies.SelectedValue = clsCompany.GetDefaultCompany(UserID);
        }

        private void FillParentLocs()
        {
            AstLocations alc = new AstLocations();
            dcAccountingDataContext dc = new dcAccountingDataContext();
            alc.UserID = UserID;
            drpParentAst.DataSource = alc.SelectParentLocationByID(dc);
            drpParentAst.DataValueField = "ID";
            drpParentAst.DataTextField = "LocationName";
            drpParentAst.DataBind();
            drpParentAst.Items.Insert(0, new ListItem("--اختر--", "0"));
        }

        protected void ImgBtnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            ViewState.Add("LocID", Convert.ToInt32(ASPxTreeList1.FocusedNode.GetValue("ID")));
            FillFields(Convert.ToInt32(ASPxTreeList1.FocusedNode.GetValue("ID")));
        }

        protected void ImgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            AstLocations alc = new AstLocations();
            dcAccountingDataContext dc = new dcAccountingDataContext();
            alc.ID = Convert.ToInt32(ASPxTreeList1.FocusedNode.GetValue("ID"));
            int result = alc.DeleteAstLocation(dc);
            if (result > 0)
            {
                LabelOutputMsg.Visible = true;
                alertErrorDiv.Visible = false;
                alertConfirmDiv.Visible = true;
                LabelOutputMsg.Text = "تم حذف الحساب بنجاح";
            }
            else if (result == -2)
            {
                lblErrorMsg.Visible = true;
                alertErrorDiv.Visible = true;
                alertConfirmDiv.Visible = false;
                lblErrorMsg.Text = "لايمكن حذف الحساب لارتباطه بأصول";
            }
            BindTree();
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.AccAstLocations.GetHashCode());
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
    }
}