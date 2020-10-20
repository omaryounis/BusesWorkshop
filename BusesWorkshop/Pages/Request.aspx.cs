//using BusesWorkshop.DAL.Bus;
using BusesWorkshop.DAL;
using BusesWorkshop.DAL.Accounting;
using BusesWorkshop.DAL.Bus;
using BusesWorkshop.Pages.PagesPermissions;
using BusesWorkshop.UserControls;
using BusesWorkshop.VM;
using DevExpress.Web;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusesWorkshop.Pages
{
    public partial class Request : System.Web.UI.Page
    {


        /// <summary>
        /// //sighn in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
                #region "prop"
        WorkshopDataContext dcWorkShop = new WorkshopDataContext();
        RealEstateDataContext dcReal = new RealEstateDataContext();

        #endregion
        #region "Methods"
        string getValOfrequestType(int? type)
        {
            string types;
            if (type == 0)
            {
                types = "طلب صيانة ";
            }
            else if (type == 1)
            {
                types = "طلب دعم ";

            }
            else
            {
                types = "صيانةظدعم";
            }
            return types;
        }
        private void permitions(WorkshopDataContext dc)
        {
            try
            {

                var Suserpermis =
                    from up in dcWorkShop.User_Phases
                    join p in dcWorkShop.Phases
                    on up.phases_Id equals p.phases_Id
                    where up.User_ID == int.Parse(Session["UserID"].ToString())
                    && p.phase_Step == 0
                    select p.phase_Step;
                if (Suserpermis.ToList().Count > 0)
                {

                    MaintRequestTabPage.TabPages[0].Visible = true;


                }           
                var Duserpermis =
                   from up in dcWorkShop.User_Phases
                   join p in dcWorkShop.Phases
                   on up.phases_Id equals p.phases_Id
                   where up.User_ID == int.Parse(Session["UserID"].ToString())
                   && p.phase_Step == 1
                   select p.phase_Step;
                if (Duserpermis.ToList().Count > 0)
                {

                    MaintRequestTabPage.TabPages[1].Visible = true;

                }
                var Puserpermis =
                   from up in dcWorkShop.User_Phases
                   join p in dcWorkShop.Phases
                   on up.phases_Id equals p.phases_Id
                   where up.User_ID == int.Parse(Session["UserID"].ToString())
                   && p.phase_Step == 2
                   select p.phase_Step;
                if (Puserpermis.ToList().Count > 0)
                {

                    MaintRequestTabPage.TabPages[2].Visible = true;

                }


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
        public List<RequestVM> GetRecData()
        {
            var d = (
                        from m in dcWorkShop.MaintRequests
                        join c in dcWorkShop.Companies on m.CompanyId equals c.ID
                        join uc in dcWorkShop.UserCompanies on c.ID equals uc.CompID

                        join u in dcWorkShop.Users on uc.UserID equals u.ID
                        join uP in dcWorkShop.User_Phases on u.ID equals uP.User_ID
                        join p in dcWorkShop.Phases on uP.phases_Id equals p.phases_Id
                        join rp in dcWorkShop.ReqPhases on m.MaintReqId equals rp.Req_Id

                        //phase_Step == 0 || p.phase_Step == null) && u.ID == Convert.ToInt16(Session["UserID"].ToString()) && (m.IsClosed == false || m.IsClosed == null) && m.IsAccepted == null//اعتماد

                        select new RequestVM
                        {
                            LeftId = m.MaintReqId,
                            RightId = ((m.CompanyId == c.ID) ? c.ID : 0)
                        ,
                            MaintReqId = m.MaintReqId,
                            RequestDate = m.RequestDate,
                            CompName = m.Company.CompName,
                            UserName = u.Name,
                            UserId = u.ID,
                            phase_Step = p.phase_Step,
                            IsClosed = m.IsClosed,
                            IsAccepted = m.IsAccepted,
                            IsRejected = m.IsRejected,
                            Phase_Order = p.Phase_Order,
                            requestType = p.requestType,
                            requestTypes = getValOfrequestType(p.requestType),//p. (p.requestType==0)?"طلب صيانة"? (p.requestType==1)?"طلب دعم فنى":"طلب دعم وصيانة",
                            time = m.RequestDate.TimeOfDay,
                            timeOfNow = DateTime.Now.TimeOfDay,
                            defTime = m.RequestDate.TimeOfDay.Hours - DateTime.Now.TimeOfDay.Hours,
                            defDay = m.RequestDate.Day - DateTime.Now.Day,
                            defdateTime = (m.RequestDate.Day - DateTime.Now.Day).ToString() + "days" + (m.RequestDate.TimeOfDay.Hours - DateTime.Now.TimeOfDay.Hours).ToString() + "Hours"
                            , None = null,
                            PhaseName=p.phases_Name

                        }).ToList();
                                    return d;
        }
        public void FillGridRequests()
        {

            /////////////////sighin
            ///


            ////////////////////////

            if (MaintRequestTabPage.ActiveTabIndex == 0 || MaintRequestTabPage.ActiveTabIndex == -1)
            {

                var d = GetRecData()
                    .Where(p => (p.phase_Step == 0 || p.phase_Step != null) && p.Phase_Order == 1 && p.UserId == Convert.ToInt16(Session["UserID"].ToString()) && (p.IsClosed == false || p.IsClosed == null) && p.IsAccepted == null).Distinct();//اعتماد)

                if (d != null)
                {
                    if (d.ToList().Count > 0)
                    {
                        if (dxgrd_Requests != null)
                        {

                            dxgrd_Requests.DataSource = d.Distinct();

                            dxgrd_Requests.DataBind();
                        }
                        Session["dxgrd_Requests"] = d.ToList().Distinct().CopyToDataTable();
                    }
                    else
                    {
                        Session["dxgrd_Requests"] = "";
                    }
                }

            }
            else if (MaintRequestTabPage.ActiveTabIndex == 1)
            {

                var dd = GetRecData().Where(p => p.Phase_Order == 2 && (p.IsAccepted == true || p.IsAccepted != null) && (p.IsClosed == false || p.IsClosed == null) && p.UserId == Convert.ToInt16(Session["UserID"].ToString())).Distinct();//.ToList().Distinct();//.AsQueryable();
                                                                                                                                                                                                                                              // DataTable dt = dd.CopyToDataTable();
                if (dd.Count() > 0)
                {
                    if (dxgrd_Requests != null)
                    {
                        dxgrd_Requests.DataSource = dd.AsEnumerable().Distinct().ToList();
                        dxgrd_Requests.DataBind();
                    }
                    Session["DistReq"] = dd.Distinct().CopyToDataTable();
                }
                else
                {

                    dxgrd_Requests.DataSource =null;

                    dxgrd_Requests.DataBind();
                    Session["DistReq"] = "";

                }
            }
        }
        public void fillGrdUsersRequest(int? req_Id)
        {
            var userRecData =
           from u in dcWorkShop.Users
               //  join m in dcWorkShop.MaintRequests on u.ID equals m.RequestedEmpId into j1
           join m in dcWorkShop.userRequests on u.ID equals m.UserId into j1
           join uc in dcWorkShop.UserCompanies on u.ID equals uc.UserID
           join uP in dcWorkShop.User_Phases on u.ID equals uP.User_ID
           join P in dcWorkShop.Phases on uP.phases_Id equals P.phases_Id
           where P.phase_Step == 1

           from j2 in j1.DefaultIfEmpty()
           group j2 by new { u.ID, u.Name } into g
           // from user in dcWorkShop.Users

           select new
           {

               user_Id = g.Key.ID,
               name = g.Key.Name,
               RecCount = g.Count(t => t.MaintRequestId != null && t.IsDeleted == null)

           };



            //  ViewState["userRecData"] = userRecData;

            var query =
    //in gj.DefaultIfEmpty()
    from a in dcWorkShop.MaintRequests

    join uc in dcWorkShop.UserCompanies on a.CompanyId equals uc.ID
    join u in dcWorkShop.Users on uc.UserID

     equals u.ID into US

    from d in
   US.DefaultIfEmpty()
    group a by a.RequestedEmpId into g //into gj

    //join user in dcWorkShop.Users on g.Key equals user.ID 
    //where u.ID==req_Id
    //from subpet in g.DefaultIfEmpty()

    select new
    {


        RecCount = g.Count()

    };
            dxUserRequests.DataSource = userRecData.ToList();
            dxUserRequests.DataBind();
        }

        #endregion
        #region "Events"

        protected void Page_Load(object sender, EventArgs e)
        {

            FillGridRequests();
            Page.Title = "مجموعات المستخدمين";
            // WorkshopDataContext dc = new WorkshopDataContext();
            permitions(dcWorkShop);
        }
        public void fillControls(int reqID)
        {
            var query = (from p in dcWorkShop.MaintRequests
                         join c in  dcWorkShop.Companies
                         on p.CompanyId equals c.ID
                         where p.MaintReqId == Convert.ToInt32(
reqID
)

                         select new { p.MaintReqId,c.CompName,p.Notes,p.CompanyId, p.PriorHigh, p.PriorLow, p.PriorUrgent, p.RequestDate, p.LocationId }).SingleOrDefault();
            lblRecId.Text = query.MaintReqId.ToString();
            lblDate.Text = query.RequestDate.Date.ToString();
            lblTime.Text = query.RequestDate.TimeOfDay.ToString();//query.RequestDate.TimeOfDay.TotalDays.ToString()+"Days"+ query.RequestDate.TimeOfDay.TotalDays.ToString()+"Hours";
            lblCompName.Text = query.CompName;
            lblNotes.Text = query.Notes;
            

            var queryParentLocation = (dcReal.Sp_SelectParentLocationById(int.Parse(query.LocationId.ToString()))).SingleOrDefault();
            var queryMainLocation = (dcReal.Sp_SelectParentLocationById(int.Parse(queryParentLocation.ParentID.ToString()))).SingleOrDefault();
            lblMainLoc.Text = dcReal.Ast_Locations.Select(l => new { l.LocationName, l.ID }).
                Where(l => l.ID == queryMainLocation.ParentID).FirstOrDefault().LocationName;
            lblLoc.Text= dcReal.Ast_Locations.Select(l => new { l.LocationName, l.ID }).
                Where(l => l.ID == query.LocationId).FirstOrDefault().LocationName;
            lblFloor.Text= dcReal.AssetLocationsWithLevelsSelect_Vws.Where(x => x.level == 3 && x.ParentID == queryMainLocation.ParentID.Value).FirstOrDefault().LocationName;

            var queryDetail = (from p in dcWorkShop.MaintReqDetails
                                   //join pic in dcWorkShop.MaintReqPictures
                                   //on p.MaintReqId equals pic.MaintReqId
                               where p.MaintReqId == Convert.ToInt32(Convert.ToInt32(
                                 reqID
                                 ))
                              select new { p.MobileId, p.PicDescription, p.ConfigDetail.ConfigDetailName }).ToList();

            var queryDetailWithMobileData =
                (from q in queryDetail
                 join a in dcReal.AstNames on q.MobileId equals a.ID into ps
                 from p in ps.DefaultIfEmpty()
                 select new
                 {
                     q.PicDescription,
                     q.ConfigDetailName,
                     MobileName=p.Name

                 });
                
            grd_WorksNeeded.DataSource = queryDetailWithMobileData;
            grd_WorksNeeded.DataBind();



            var queryPic = (from p in dcWorkShop.MaintReqPictures
                            where p.MaintReqId == Convert.ToInt32(Convert.ToInt32(
reqID))
                            select new { p.PicturePath, p.Description }).ToList();
            grd_Pictures.DataSource = queryPic;
            grd_Pictures.DataBind();
           
        }
        protected void grd_advanceRequest_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            var grid = (ASPxGridView)sender;

            var Usersource = from u in dcWorkShop.Users
                             select new { u.ID, u.Name };
            var advanceRequestId = e.KeyValue;
            //ASPxHyperLink btn_Details = (ASPxHyperLink)grid.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btn_Details");

           BusesWorkshop.DAL.Bus.MaintRequest _MaintRequest = dcWorkShop.MaintRequests.Single(x => x.MaintReqId == Convert.ToInt32(advanceRequestId));
            // ViewState["_MaintRequest"] = _MaintRequest;
            List<Phase> Phases = dcWorkShop.Phases.
     Where(p => p.phase_Step == 0).ToList();//اعتماد

            var d = (
from p in dcWorkShop.Phases
join uP in dcWorkShop.User_Phases on p.phases_Id equals uP.phases_Id
join u in dcWorkShop.Users on uP.User_ID equals u.ID
join m in dcWorkShop.MaintRequests on u.ID equals m.RequestedEmpId

where m.MaintReqId == Convert.ToInt16(advanceRequestId.ToString()) && p.phase_Step == 0 && m.IsClosed == false  //اعتماد
                                                                                                                //&&p.Phase_Order==1 

select new { Phase_Order = p.Phase_Order, phase_Step = p.phase_Step, phases_Id = p.phases_Id, MaintReqId = m.MaintReqId, RequestDate = m.RequestDate, CompName = m.Company.CompName, UserName = u.Name, UserId = u.ID }).ToList();
            if (d.Count != 0)
            {


                Phase phases = dcWorkShop.Phases.Single(x => x.phases_Id == Convert.ToInt32(d.FirstOrDefault().phases_Id));
            }


            //#region "SaveReqPhase"
            int? nextPhase = 0;
            List<ReqPhase> currentApprovedPhases = dcWorkShop.ReqPhases.Where(c => c.Req_Id == Convert.ToInt16(advanceRequestId.ToString())).ToList();


            if (e.CommandArgs.CommandName == "CloseRequest")
            {
                mReqId.Value = advanceRequestId.ToString();
                pCAddCNotes.ShowOnPageLoad = true;

                _MaintRequest.IsClosed = true;

                dcWorkShop.SubmitChanges();

            }
            else if (e.CommandArgs.CommandName == "Hold")
            {
                _MaintRequest.IsRejected = true;

                dcWorkShop.SubmitChanges();
                //var reqnotes = dcworkshop.maintrequests.where(r=>r.maintreqid ==convert.toint16( advancerequestid.tostring())).select(r => r.notes).firstordefault();

                mReqId.Value = advanceRequestId.ToString();
                // mrecnotes.value = reqnotes;
                pCAddCNotes.ShowOnPageLoad = true;

            }
            else if (e.CommandArgs.CommandName == "Forward")
            {
                //phases.IsActive = 1;
                //phases.phase_Step = d.FirstOrDefault().Phase_Order + 1;

                #region "SaveReqPhase"

                if (currentApprovedPhases.Count == 0)
                {
                    nextPhase = dcWorkShop.Phases.Where(x => x.Phase_Order == 1).FirstOrDefault().phases_Id;
                }
                else if (currentApprovedPhases.Count > 0)
                {
                    nextPhase = dcWorkShop.ReqPhases
                        .Where(RP => RP.Req_Id == Convert.ToInt32(advanceRequestId.ToString()))
                        .Select(rp => rp.Phase_Id).OrderByDescending(rp => rp.Value).FirstOrDefault();//.SingleOrDefault();//.OrderByDescending(rp => rp.Value).LastOrDefault();

                }


                ReqPhase ReqPhase = new ReqPhase();
                ReqPhase.Req_Id = Convert.ToInt32(advanceRequestId.ToString());
                ReqPhase.User_Id = int.Parse(Session["UserID"].ToString());
                ReqPhase.StartDate = DateTime.Now;
                //ReqPhase.EndDate = DateTime.Now;
                ReqPhase.Phase_Id = nextPhase;//Convert.ToInt32( ddl_Phases.SelectedItem.Value.ToString());
                dcWorkShop.ReqPhases.InsertOnSubmit(ReqPhase);
                dcWorkShop.SubmitChanges();
                _MaintRequest.IsAccepted = true;
                dcWorkShop.SubmitChanges();

                #endregion
            }
            else if (e.CommandArgs.CommandName == "Details")
            {

                // تفاصيل الطلب                    اسم الطل, الفرع, التاريخظ, اسم اليوزر ال مقدم الطلب

                recID.Value = advanceRequestId.ToString();
                fillControls(Convert.ToInt32(advanceRequestId.ToString()));
                //MaintRequestProcessing.recID = Convert.ToInt32(recID.Value);
                //MaintRequestProcessing.fillControls();
                PCDetail.ShowOnPageLoad = true;
            }

            if (e.CommandArgs.CommandName == "Go")
            {
                fillGrdUsersRequest(0);

                RequestID.Value = advanceRequestId.ToString();
                PayAccountPOPOUP.ShowOnPageLoad = true;
            }
            FillGridRequests();
        }


        protected void btnForward_Click(object sender, EventArgs e)
        {
            if (!(sender is ASPxButton button))
            {
                return;
            }

            if (!(button.NamingContainer is GridViewDataItemTemplateContainer container))
            {
                return;
            }
            int ID = int.Parse(container.KeyValue.ToString());
            ////////update user request
            var OlduserRequest = dcWorkShop.userRequests.Where(ur => ur.MaintRequestId == Convert.ToInt32(RequestID.Value)).ToList();
            foreach (userRequest userReq in OlduserRequest)
            {
                userReq.IsDeleted = 1;
                userReq.EndDate = DateTime.Now.Date;
                dcWorkShop.SubmitChanges();
            }
            ///////////insert userRequest
            userRequest userRequest = new userRequest();
            userRequest.StartDate = DateTime.Now.Date;

            userRequest.MaintRequestId = Convert.ToInt32(RequestID.Value);//Convert.ToInt16(Session["redid"].ToString());
            userRequest.UserId = ID;//Convert.ToInt32( ddl_AcccountName.SelectedItem.Value.ToString());
            dcWorkShop.userRequests.InsertOnSubmit(userRequest);
            dcWorkShop.SubmitChanges();
            PayAccountPOPOUP.ShowOnPageLoad = false;
            #region "SaveReqPhase"
            int? nextPhase = 0;
            List<ReqPhase> currentApprovedPhases = dcWorkShop.ReqPhases.Where(c => c.Req_Id == Convert.ToInt32(RequestID.Value)).ToList();

            if (currentApprovedPhases.Count == 0)
            {
                nextPhase = dcWorkShop.Phases.Where(x => x.Phase_Order == 1).FirstOrDefault().phases_Id;
            }

            else if (currentApprovedPhases.Count > 0)
            {
                nextPhase = dcWorkShop.ReqPhases
                    .Where(RP => RP.Req_Id == Convert.ToInt32(RequestID.Value))
                        .Select(rp => rp.Phase_Id).OrderByDescending(x => x.Value).FirstOrDefault();

            }

            ReqPhase ReqPhase = new ReqPhase();
            ReqPhase.User_Id = int.Parse(Session["UserID"].ToString());
            ReqPhase.StartDate = DateTime.Now;
            // ReqPhase.EndDate = DateTime.Now;
            ReqPhase.Phase_Id = nextPhase;//Convert.ToInt32( ddl_Phases.SelectedItem.Value.ToString());
            dcWorkShop.ReqPhases.InsertOnSubmit(ReqPhase);
            dcWorkShop.SubmitChanges();

            #endregion
            /////////////update user phase
            ////////update
            var OlduserPhase = dcWorkShop.User_Phases.Where(ur => ur.phases_Id == nextPhase).ToList();
            foreach (User_Phase UserPhases in OlduserPhase)
            {
                UserPhases.IsActive = 1;
                UserPhases.User_ID = ID;
                dcWorkShop.SubmitChanges();
            }
            ///////////insert
            User_Phase UserPhase = new User_Phase();
            UserPhase.User_ID = int.Parse(Session["UserID"].ToString());
            dcWorkShop.User_Phases.InsertOnSubmit(UserPhase);
            dcWorkShop.SubmitChanges();




        }


        protected void dxUserRequests_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            var userReq = dcWorkShop.userRequests.Where(x => x.IsDeleted == null).ToList();
            var grid = (ASPxGridView)sender;
            ASPxButton btnForward = new ASPxButton();
            foreach (var ur in userReq)
            {
                if (ur.UserId == Convert.ToInt32(e.KeyValue.ToString())
           &&
           (ur.MaintRequestId == null ? 0 : ur.MaintRequestId.Value) == Convert.ToInt32(RequestID.Value.ToString()))

                    if (e.DataColumn.FieldName == "None")
                    {
                        btnForward = (ASPxButton)grid.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btn_Foward");

                        btnForward.Visible = false;
                    }

                    else
                    {
                        if (e.DataColumn.FieldName == "None")
                        {
                            btnForward = (ASPxButton)grid.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btn_Foward");

                            btnForward.Visible = true;
                        }
                    }
            }

        }

        #endregion


        #region "Fields"

        #endregion
        #region "Method"

        #endregion
        #region "Event"

        protected void MaintRequestProcessingTabPage_ActiveTabChanged(object source, TabControlEventArgs e)
        {
            permitions(dcWorkShop);
            FillGridRequests();

        }
        protected void MaintRequestProcessingTabPage_TabClick(object source, TabControlCancelEventArgs e)
        {
            permitions(dcWorkShop);
            FillGridRequests();
        }

        #endregion

        protected void dxgrd_Requests_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            var grid = (ASPxGridView)sender;
            ASPxButton btnForward = new ASPxButton();
            btnForward = (ASPxButton)grid.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btn_Forward");
            ASPxButton btn_Close = (ASPxButton)grid.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btn_Close");
            ASPxButton btn_Hold = (ASPxButton)grid.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btn_Hold");
            ASPxButton btn_Details = (ASPxButton)grid.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btn_Details");
            ASPxButton btn_Aprove = (ASPxButton)grid.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btn_Aprove");

            if (MaintRequestTabPage.ActiveTabIndex == 0)

            {
                if (btnForward != null)
                {
                    btnForward.Visible = true;

                    btn_Aprove.Visible = false;
                    btn_Close.Visible = true;
                    btn_Hold.Visible = true;
                    btn_Details.Visible = true;
                }
            }
            else if (MaintRequestTabPage.ActiveTabIndex == 1)
            {
                if (btnForward != null)
                {
                    btn_Aprove.Visible = true;
                    btnForward.Visible = false;
                    btn_Close.Visible = false;
                    btn_Hold.Visible = false;
                    btn_Details.Visible = false;
                }
            }

        }

        protected void btnAddHoldReaso_Click(object sender, EventArgs e)
        {
            //if (!(sender is ASPxButton button))
            //{
            //    return;
            //}

            //if (!(button.NamingContainer is GridViewDataItemTemplateContainer container))
            //{
            //    return;
            //}
            //int ID = int.Parse(container.KeyValue.ToString());
        //    DAL.MaintRequest _MaintRequest = dcWorkShop.MaintRequests.Single(x => x.MaintReqId == Convert.ToInt32(mReqId.Value.ToString()));

            #region "Process"

            Process Process = new Process();
            Process.Req_Id = Convert.ToInt32(mReqId.Value.ToString());
            Process.Date = DateTime.Now;
            Process.User_Id = int.Parse(Session["UserID"].ToString());
            Process.Type = "Hold";//24;// -1;//hold
            dcWorkShop.Processes.InsertOnSubmit(Process);
            dcWorkShop.SubmitChanges();
         //   _MaintRequest.IsRejected = true;
            //_MaintRequest.Notes = txtNotes.Text;
           // dcWorkShop.SubmitChanges();

            #endregion
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlHold.SelectedValue == "0")
            {
                DataTable sighnInReq = new DataTable();

                var req = Session["dxgrd_Requests"];

                if (req.ToString() != "")
                {
                    sighnInReq = (DataTable)req;
                }
                //var dValue = from row in sighnInReq.AsEnumerable()
                //                where row.Field<bool>("IsRejected") == false
                //                select new
                //                {
                //                    MaintReqId = row.Field<int>("MaintReqId"),
                //                    RequestDate = row.Field<DateTime>("RequestDate"),
                //                    CompName = row.Field<DateTime>("CompName"),
                //                    UserName = row.Field<DateTime>("UserName"),
                //                    requestType = row.Field<DateTime>("requestType"),
                //                    time = row.Field<DateTime>("time"),
                //                    defdateTime = row.Field<DateTime>("defdateTime"),
                //                    IsRejected=row.Field<bool>("IsRejected")
                //                };
                var data = GetRecData().Where(r => r.Phase_Order == 1 && r.IsRejected == false && r.phase_Step == 0 && r.IsRejected == true && r.UserId == int.Parse(Session["UserID"].ToString())).Distinct();

                dxgrd_Requests.DataSource = data;
                dxgrd_Requests.DataBind();
            }
            else if (ddlHold.SelectedValue == "1")
            {

                dxgrd_Requests.DataSource = GetRecData().Where(r => r.Phase_Order == 1 && r.phase_Step == 0 && r.IsRejected == true && r.UserId == int.Parse(Session["UserID"].ToString())).Distinct();
                dxgrd_Requests.DataBind();
            }
            else if (ddlHold.SelectedValue == "2")
            {
                FillGridRequests();
            }

        }

        protected void btnCloseCommente_Click(object sender, EventArgs e)
        {
            #region "Process"

            Process Process = new Process();
            Process.Req_Id = Convert.ToInt32(rID.Value.ToString());
            Process.Date = DateTime.Now;
            Process.User_Id = int.Parse(Session["UserID"].ToString());
            Process.Type = "Close";
            Process.Notes = txtNotes.Text;
            dcWorkShop.Processes.InsertOnSubmit(Process);
            dcWorkShop.SubmitChanges();
            #endregion
        }
    }
}