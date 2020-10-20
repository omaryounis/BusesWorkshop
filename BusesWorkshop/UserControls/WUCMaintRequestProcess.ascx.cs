using BusesWorkshop.DAL.Bus;
using BusesWorkshop.DAL.Accounting;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusesWorkshop.DAL;

namespace BusesWorkshop.UserControls
{
    public partial class WUCMaintRequestProcess : System.Web.UI.UserControl
    {
        #region "Properties"
        RealEstateDataContext dcReal = new RealEstateDataContext();
        WorkshopDataContext dcWorkShop = new WorkshopDataContext();
        public ASPxPageControl reqPage = new ASPxPageControl();

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
        public void FillGridRequests()
        {
            int RequestType = 0;
            //ActiveTabIndex
            // if (reqPage.ActiveTabIndex==0)
            if (Convert.ToInt16(Session["reqPageTabIndex"]) == 0)
            {
                RequestType = 0;
            }
            //  else if (reqPage.ActiveTabIndex == 1)
            else if (Convert.ToInt16(Session["reqPageTabIndex"]) == 1)

            {
                RequestType = 1;
            }
           var q=  (
                from p in dcWorkShop.MaintRequests
              join d in dcWorkShop.MaintReqDetails on (int)p.MaintReqId equals (int)d.MaintReqId
              join m in dcWorkShop.Mobiles on (int)d.MobileId equals (int)m.MobileId
              where m.ServiceRequest == RequestType && p.IsClosed == false
                    //.ToString("dd/MM/yyyy")
                    select new { مسلسل = p.MaintReqId, التاريخ = p.RequestDate, الشركة = p.Company.CompName }).ToList();
            if (q!=null)
            {
                grd_Requests.DataSource = q; //dcWorkShop.MaintRequests

                grd_Requests.DataBind();
            }

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


            }
            FillGridRequests();
        }
        public void grd_Requests_SelectedIndexChanged(object sender, EventArgs e)
        {


            // var query =// (from p in dcWorkShop.MaintRequests
            // //join d in dcWorkShop.MaintReqDetails on (int)p.MaintReqId equals (int)d.MaintReqId
            // //join m in dcWorkShop.Mobiles on (int)d.MobileId equals (int)m.MobileId
            // //where m.ServiceRequest == RequestType
            // //  && p.MaintReqId == Convert.ToInt32(grd_Requests.SelectedRow.Cells[1].Text
            //    .Where(p => p.MaintReqId == Convert.ToInt32(grd_Requests.SelectedRow.Cells[1].Text)

            //  dcWorkShop.MaintRequests.Select(p=> new { p.CompanyId, p.PriorHigh, p.PriorLow, p.PriorUrgent, p.RequestDate, p.Notes, p.LocationId }).SingleOrDefault();


            var query = (from p in dcWorkShop.MaintRequests where p.MaintReqId == Convert.ToInt32(grd_Requests.SelectedRow.Cells[1].Text) select new { p.CompanyId, p.PriorHigh, p.PriorLow, p.PriorUrgent, p.RequestDate, p.Notes, p.LocationId }).SingleOrDefault();
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




            var queryDetail = from p in dcWorkShop.MaintReqDetails where p.MaintReqId == Convert.ToInt32(grd_Requests.SelectedRow.Cells[1].Text) select new {/* p.Mobile.MobileName,*/ p.PicDescription, p.ConfigDetail.ConfigDetailName };
            grd_WorksNeeded.DataSource = queryDetail;
            grd_WorksNeeded.DataBind();



            var queryPic = (from p in dcWorkShop.MaintReqPictures where p.MaintReqId == Convert.ToInt32(grd_Requests.SelectedRow.Cells[1].Text) select new { p.PicturePath, p.Description }).ToList();
            grd_Pictures.DataSource = queryPic;
            grd_Pictures.DataBind();


            GridMessagesFill();
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
    }
}