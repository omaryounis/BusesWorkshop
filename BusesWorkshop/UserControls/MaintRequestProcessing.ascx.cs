using BusesWorkshop.DAL.Bus;
using BusesWorkshop.DAL.Accounting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusesWorkshop.DAL;

namespace BusesWorkshop.UserControls
{
    public partial class MaintRequestProcessing1 : System.Web.UI.UserControl
    {
        #region "Properties"
        RealEstateDataContext dcReal = new RealEstateDataContext();
        WorkshopDataContext dcWorkShop = new WorkshopDataContext();
        public int recID{get;set;}
        #endregion
        #region "Methods"
        private void FillLocation()
        {
            ddl_LocationId.DataSource = dcReal.Sp_SelectLocation(null);
            ddl_LocationId.TextField = "LocationName";
            ddl_LocationId.ValueField = "ID";
            ddl_LocationId.DataBind();
        }

        private void FillParentId()
        {

        }
        private void FillGridRequests()
        {
            grd_Requests.Visible = true;
            DataTable sighnInReq = new DataTable();
            var req = Session["dxgrd_Requests"];

            if (req.ToString() != "")
            {
                sighnInReq = (DataTable)req;
            }
            if(recID>0)
            {
                var dValue = from row in sighnInReq.AsEnumerable()
                             where row.Field<int>("MaintReqId") == recID
                             select new
                             { MaintReqId = row.Field<int>("MaintReqId") };

                grd_Requests.DataSource = dValue;// sighnInReq.Select("MaintReqId = " + recID).CopyToDataTable();// //dcWorkShop.MaintRequests.Where(p => p.IsClosed == false).ToList().Select(p => new { مسلسل = p.MaintReqId, التاريخ = p.RequestDate.ToString("dd/MM/yyyy"), الشركة = p.Company.CompName }).ToList();
                ProcessTitle.Visible = false;
                Process.Visible = false;
            }
            else
            {
                grd_Requests.DataSource=Session["dxgrd_Requests"];
            }
            grd_Requests.DataBind();
        }
        private void FillCompanies()
        {
            ddl_companyId.DataSource = dcWorkShop.Sp_CompaniesSelectAll(int.Parse(Session["UserId"].ToString()));
            ddl_companyId.TextField = "CompName";
            ddl_companyId.ValueField = "ID";
            ddl_companyId.DataBind();
        }
        private void FillSections(int comID)
        {

            ddl_ParentId.DataSource = dcReal.AssetLocationsWithLevelsSelect_Vws.Where(x => x.level == 2 && x.ParentID == comID);
            ddl_ParentId.TextField = "LocationName";
            ddl_ParentId.ValueField = "ID";
            ddl_ParentId.DataBind();
        }
        private void FillFloors(int SectionID)
        {
            ddl_FloorId.DataSource = dcReal.AssetLocationsWithLevelsSelect_Vws.Where(x => x.level == 3 && x.ParentID == SectionID);
            ddl_FloorId.TextField = "LocationName";
            ddl_FloorId.ValueField = "ID";
            ddl_FloorId.DataBind();
        }
        private void GridMessagesFill()
        {
            grd_Messages.DataSource = from p in dcWorkShop.MaintReqComments where p.MaintReqId == Convert.ToInt32(grd_Requests.SelectedRow.Cells[1].Text) select new { p.Name, p.Message };
            grd_Messages.DataBind();
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.MaintRequestProceeding.GetHashCode());
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
        #region"Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            permissions(dcWorkShop);
            Page.Title = "طلب صيانة مبانى";
            if (!IsPostBack)
            {
                FillCompanies();
                // FillParentId();
                FillLocation();

                if (recID > 0)
                {
                    fillControls();

                }
                else
                {
                    FillGridRequests();
                }
            }
           
        }
     public void   fillControls()
        {
            var query = (from p in dcWorkShop.MaintRequests where p.MaintReqId == Convert.ToInt32(
               recID// Request.QueryString["recID"]//grd_Requests.SelectedRow.Cells[1].Text
                         ) select new { p.CompanyId, p.PriorHigh, p.PriorLow, p.PriorUrgent, p.RequestDate, p.Notes, p.LocationId }).SingleOrDefault();
            rd_PriorHigh.Checked = bool.Parse(query.PriorHigh.ToString());
            rd_PriorLow.Checked = bool.Parse(query.PriorLow.ToString());
            rd_PriorUrgent.Checked = bool.Parse(query.PriorUrgent.ToString());
            ddl_companyId.Value = query.CompanyId.ToString();
            txt_RequestDate.Text = query.RequestDate.ToShortDateString();
            txt_Notes.Text = query.Notes.ToString();
            ddl_LocationId.Value = query.LocationId.ToString();
            ddl_companyId_SelectedIndexChanged(null, null);


            var queryParentLocation = (dcReal.Sp_SelectParentLocationById(int.Parse(query.LocationId.ToString()))).SingleOrDefault();
            var queryMainLocation = (dcReal.Sp_SelectParentLocationById(int.Parse(queryParentLocation.ParentID.ToString()))).SingleOrDefault();
            ddl_ParentId.Value = queryMainLocation.ParentID.ToString();

            FillFloors(queryMainLocation.ParentID.Value);
            ddl_FloorId.Value = queryParentLocation.ParentID.ToString();
            //var queryWORKS = from p in dcWorkShop.MaintReqDetails where p.MaintReqId == Convert.ToInt32(grd_Requests.SelectedRow.Cells[1].Text) select new {p.MobileId , p.}




            var queryDetail = from p in dcWorkShop.MaintReqDetails where p.MaintReqId == Convert.ToInt32(Convert.ToInt32(
               recID// Request.QueryString["recID"]//grd_Requests.SelectedRow.Cells[1].Text
                         )) select new { /*p.Mobile.MobileName,*/ p.PicDescription, p.ConfigDetail.ConfigDetailName };
            grd_WorksNeeded.DataSource = queryDetail;
            grd_WorksNeeded.DataBind();



            var queryPic = (from p in dcWorkShop.MaintReqPictures where p.MaintReqId == Convert.ToInt32(Convert.ToInt32(
               recID// Request.QueryString["recID"]//grd_Requests.SelectedRow.Cells[1].Text
                         )) select new { p.PicturePath, p.Description }).ToList();
            grd_Pictures.DataSource = queryPic;
            grd_Pictures.DataBind();
            PageTitle.Visible = false;
            GRidRec.Visible = false;
            tiltle.Visible = false;
            ProcessTitle.Visible = false;
            Process.Visible = false;
            //  GridMessagesFill();
        }
        protected void grd_Requests_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillControls();
        }
        protected void grd_WorksNeeded_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void Save_Click(object sender, EventArgs e)
        {
            if (grd_Requests.SelectedRow == null)
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult2.Text = "عفوا اختر الطلب المراد اضافة رد علية";
                return;
            }
            BusesWorkshop.DAL.Bus.MaintRequest _MaintRequest = dcWorkShop.MaintRequests.Single(x => x.MaintReqId == int.Parse(grd_Requests.SelectedRow.Cells[1].Text));
            _MaintRequest.IsClosed = true;
            dcWorkShop.SubmitChanges();
            divMsg2.Attributes["class"] = "alert alert-success text-right";
            lblResult2.Text = "تم اغلاق الطلب بنجاح";


            grd_Pictures.DataSource = null;
            grd_Pictures.DataBind();

            grd_WorksNeeded.DataSource = null;
            grd_WorksNeeded.DataBind();

            grd_Messages.DataSource = null;
            grd_Messages.DataBind();
            MyClasses.ClearControls(Page);
            FillGridRequests();

        }
        protected void btn_AddMessage_Click(object sender, EventArgs e)
        {
            if (grd_Requests.SelectedRow == null)
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult2.Text = "عفوا اختر الطلب المراد اضافة رد علية";
                return;

            }



            BusesWorkshop.DAL.Bus.MaintRequest _MaintRequest = dcWorkShop.MaintRequests.Single(x => x.MaintReqId == int.Parse(grd_Requests.SelectedRow.Cells[1].Text));
            _MaintRequest.IsRejected = rd_IsRejected.Checked;
            _MaintRequest.IsAccepted = rd_IsAccepted.Checked;
            dcWorkShop.SubmitChanges();




            MaintReqComment _MaintReqComment = new MaintReqComment();
            _MaintReqComment.MaintReqId = int.Parse(grd_Requests.SelectedRow.Cells[1].Text);
            var UserName = (from p in dcWorkShop.Users where p.ID == int.Parse(Session["UserId"].ToString()) select new { p.Name }).SingleOrDefault();

            _MaintReqComment.Name = UserName.Name;
            _MaintReqComment.Message = txt_Message.Text;
            dcWorkShop.MaintReqComments.InsertOnSubmit(_MaintReqComment);
            dcWorkShop.SubmitChanges();

            GridMessagesFill();

            MyClasses.ClearControls(Page);
        }
        protected void ddl_companyId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_companyId.SelectedItem != null)
            {
                FillSections(int.Parse(ddl_companyId.Value.ToString()));
            }

        }
        #endregion

        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                    new object[] { sender as UpdatePanel });
            
        }
    }
}