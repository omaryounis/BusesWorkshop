using BusesWorkshop.DAL.Bus;
using DevExpress.Web;
using DevExpress.Web.Data;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusesWorkshop.Pages
{
    [Serializable]
    public partial class DistributeRequestsToUsers : System.Web.UI.Page
    {
        #region "prop"
        WorkshopDataContext dcWorkShop = new WorkshopDataContext();

        #endregion
        
        #region "Methods"
        private List<userRequest> getUserRecData()
        {
            var q = from ur in dcWorkShop.userRequests
                    select new userRequest{
                    userRequestID= ur.MaintRequestId==null?0 : ur.MaintRequestId.Value,
                   UserId=     ur.UserId
                    };
            return q.ToList();
        }
        public void fillGrdUsersRequest(int? req_Id)
        {
                 var userRecData=
                from u in dcWorkShop.Users
              //  join m in dcWorkShop.MaintRequests on u.ID equals m.RequestedEmpId into j1
                join m in dcWorkShop.userRequests on u.ID equals m.UserId  into j1
                join uc in dcWorkShop.UserCompanies on u.ID equals uc.UserID
                join uP in dcWorkShop.User_Phases on u.ID equals uP.User_ID
                join P in dcWorkShop.Phases on uP.phases_Id equals P.phases_Id where P.phase_Step==1

                from j2 in j1.DefaultIfEmpty()
                group j2 by new { u.ID ,u.Name}  into g
               // from user in dcWorkShop.Users

                                      select new
                {

                    user_Id= g.Key.ID,
                 name=  g.Key.Name,
                    RecCount = g.Count(t=>t.MaintRequestId!=null&&t.IsDeleted==null)

                } ;



          //  ViewState["userRecData"] = userRecData;

            var query =
    //in gj.DefaultIfEmpty()
    from a in dcWorkShop.MaintRequests 

    join uc in dcWorkShop.UserCompanies on a.CompanyId equals uc.ID
    join u in dcWorkShop.Users    on uc.UserID
    
     equals u.ID  into US

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
        public void FillGridRequests()
        {

            var d = (

   from m in dcWorkShop.MaintRequests
   join c in dcWorkShop.Companies on m.CompanyId equals c.ID
   join uc in dcWorkShop.UserCompanies on c.ID equals uc.CompID

   join u in dcWorkShop.Users on uc.UserID equals u.ID
   join uP in dcWorkShop.User_Phases on u.ID equals uP.User_ID
   join p in dcWorkShop.Phases on uP.phases_Id equals p.phases_Id
   join rp in dcWorkShop.ReqPhases on m.MaintReqId equals rp.Req_Id
//p.phase_Step == 1(m.IsAccepted == true)
   where p.Phase_Order==2 && (m.IsClosed == false|| m.IsClosed==null)   && u.ID == Convert.ToInt16(Session["UserID"].ToString())//&& (p.phase_Step==0 &&p.IsActive==1)//توزيع

   select new { MaintReqId = m.MaintReqId, RequestDate = m.RequestDate, CompName = m.Company.CompName,UserName=u.Name,UserId=u.ID }).ToList();
            if (d.Count > 0)
            {
                if (dxgrd_Requests != null) { 
 dxgrd_Requests.DataSource = d.Distinct();
            dxgrd_Requests.DataBind();}
            Session["DistReq"] = d.Distinct().CopyToDataTable();
            }
            else
            {
                Session["DistReq"] = "";

            }

        }
        
        //public void fillUsers()
        //{
        //    var Usersource = from u in dcWorkShop.Users
        //                     select new { u.ID, u.Name };

        //    if (ddl_UserId.FindControl("listBox") is ASPxListBox branchesListBox)
        //    {
        //        branchesListBox.DataSource = Usersource.ToList();
        //        branchesListBox.TextField = "Name";
        //        branchesListBox.ValueField = "ID";
        //        //branchesListBox.AutoPostBack = AutoPostBack;
        //        branchesListBox.DataBind();
        //    }
        //}

        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
          
               
        }

        protected void grd_advanceRequest_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            fillGrdUsersRequest(0);

            // fillGrdUsersRequest(Convert.ToInt32( e.KeyValue.ToString()));

            var Usersource = from u in dcWorkShop.Users
                             select new { u.ID, u.Name };


            if (e.CommandArgs.CommandName == "Pay")
            {

                /*ddl_AcccountName.DataSource = Usersource;
                ddl_AcccountName.TextField = "Name";
                ddl_AcccountName.Value = "ID";

                    ddl_AcccountName.EnableCallbackMode = true;
                    ddl_AcccountName.CallbackPageSize = 10;
                    ddl_AcccountName.DataBind();*/
                



                var advanceRequestId = e.KeyValue;
                RequestID.Value = advanceRequestId.ToString();
                PayAccountPOPOUP.ShowOnPageLoad = true;
                
            }
        }

        public event EventHandler onSelectedIndexChanged;
        private void SelectedIndexChanged()
        {
            if (onSelectedIndexChanged != null)
            {
                onSelectedIndexChanged(this, EventArgs.Empty);
            }
        }
        #endregion
     #region "Events"

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               //   fillGrdUsersRequest(0);
                FillGridRequests();

            }
        }
        protected void btn_Forward_Init(object sender, EventArgs e)
        {
          
        }
        protected void grd_User_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            var grid = (ASPxGridView)sender;
            var Id = int.Parse(grid.GetMasterRowKeyValue().ToString());

        }


        protected void dxgrd_Requests_BeforePerformDataSelect(object sender, EventArgs e)
        {
            var dxUserRequests = new ASPxGridView();

            dxUserRequests = (ASPxGridView)sender;//dxgrd_Requests.FindDetailRowTemplateControl( 1,"dxUserRequests");
                                                  //  Session["RedId"] = Convert.ToInt16(dxUserRequests.GetMasterRowKeyValue().ToString());

         

         //   var d = (
         //from m in dcWorkShop.MaintRequests
         //join ur in dcWorkShop.userRequests on m.MaintReqId equals ur.MaintRequestId
         //join u in dcWorkShop.Users on m.RequestedEmpId equals u.ID
         //join uP in dcWorkShop.User_Phases on u.ID equals uP.User_ID
         //join p in dcWorkShop.Phases on uP.phases_Id equals p.phases_Id
         //where p.phase_Step == 1 && m.MaintReqId==Convert.ToInt32( dxUserRequests.GetMasterRowKeyValue().ToString())
         //select new { MaintReqId = m.MaintReqId, RequestDate = m.RequestDate, CompName = m.Company.CompName, UserName = u.Name,userName=ur.User.UserName, UserId = u.ID }).ToList();
         //   dxUserRequests.DataSource = d.ToList();
         //   ViewState["RequestData"] = d.ToList();
         //   var Usersource = from p in dcWorkShop.Users select new { p.ID, p.Name };
         //   if (dxUserRequests.FindControl("PopupControl") is ASPxListBox branchesListBox)
         //   {
         //       branchesListBox.DataSource = Usersource;
         //       branchesListBox.TextField = "Name";
         //       branchesListBox.ValueField = "ID";
         //       branchesListBox.DataBind();
         //   }
        }

        //protected void dxgrd_Requests_DataBinding(object sender, EventArgs e)
        //{
        //    if (sender is ASPxGridView grid)
        //    {
        //       // grid.DataSource = ViewState["RequestData"];
        //    }
        //}
        //    protected void btnCloseddl_Click(object sender, EventArgs e)
        //{
        //    ASPxListBox MultiDDLListBox = (ASPxListBox)ddl_UserId.FindControl("listBox");

        //    if (MultiDDLListBox is ASPxListBox vacsListBox)
        //    {
        //        foreach (ListEditItem item in vacsListBox.SelectedItems)
        //        {
        //            ddl_UserId.Text += ";" + item.Text;
        //        }
        //    }
        //    SelectedIndexChanged();
        //}
        userRequest userRequest;
        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    ASPxListBox MultiDDLListBox = (ASPxListBox)ddl_UserId.FindControl("listBox");
        //    if (MultiDDLListBox.SelectedItems != null)
        //        {
        //            foreach (var item2 in MultiDDLListBox.SelectedValues)
        //            {
        //                userRequest = new userRequest();
        //                userRequest.MaintRequestId = Convert.ToInt32(Session["RedId"].ToString());
        //                userRequest.UserId = (int.Parse(item2.ToString())); //هنا غلبت معاه 
        //                dcWorkShop.userRequests.InsertOnSubmit(userRequest);
        //                dcWorkShop.SubmitChanges();
        //            }
        //        return;
        //    }
            
        //}
        

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
           var OlduserRequest= dcWorkShop.userRequests.Where( ur=> ur.MaintRequestId== Convert.ToInt32(RequestID.Value)).ToList();
            foreach(userRequest userRequest in OlduserRequest)
            {
                userRequest.IsDeleted = 1;
                userRequest.EndDate = DateTime.Now.Date;
                dcWorkShop.SubmitChanges();
            }
            ///////////insert
            userRequest = new userRequest();
            userRequest.StartDate = DateTime.Now.Date;

            userRequest.MaintRequestId =Convert.ToInt32( RequestID.Value);//Convert.ToInt16(Session["redid"].ToString());
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
                        .Select(rp => rp.Phase_Id).OrderByDescending(x=>x.Value).FirstOrDefault();

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

        protected void ddl_UserId_Load(object sender, EventArgs e)
        {
                var ASPxDropDownEdit = new ASPxDropDownEdit();
                ASPxDropDownEdit = (ASPxDropDownEdit)sender;
                var Usersource = from p in dcWorkShop.Users select new { p.ID, p.Name };
                if (ASPxDropDownEdit.FindControl("listBox") is ASPxListBox branchesListBox)
                {
                    branchesListBox.DataSource = Usersource;
                    branchesListBox.TextField = "Name";
                    branchesListBox.ValueField = "ID";
                    branchesListBox.DataBind();
                }
        }

        protected void dxUserRequests_DataBinding(object sender, EventArgs e)
        {

            // dxUserRequests.DataSource=  ViewState["userRecData"];

        }

        protected void dxUserRequests_DataBound(object sender, EventArgs e)
        {
            
        }

        protected void dxUserRequests_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {

            //IQueryable<  userRequest> userReq = from ur in dcWorkShop.userRequests
            //                        select new userRequest
            //                        {
            //                            userRequestID = ur.MaintRequestId == null ? 0 : ur.MaintRequestId.Value,
            //                            UserId = ur.UserId
            //                        };
            var userReq = dcWorkShop.userRequests.Where(x=>x.IsDeleted==null).ToList();
            // getUserRecData();
            // dcWorkShop.userRequests.ToList().Where(x => x.MaintRequestId ==   int.Parse(dxUserRequests.GetMasterRowKeyValue().ToString()));
          

            var grid = (ASPxGridView)sender;

            ASPxButton btnForward = new ASPxButton();
            foreach (var ur in userReq)
            {
                if (ur.UserId == Convert.ToInt32(e.KeyValue.ToString())
           &&
           (ur.MaintRequestId == null ? 0 : ur.MaintRequestId.Value) == Convert.ToInt32(RequestID.Value.ToString()))

                    if (e.DataColumn.FieldName == "(None)")
                    {
                        btnForward = (ASPxButton)grid.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btn_Foward");

                        btnForward.Visible = false;
                    }

                    else
                    {
                        if (e.DataColumn.FieldName == "(None)")
                        {
                            btnForward = (ASPxButton)grid.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btn_Foward");

                            btnForward.Visible = true;
                        }
                    }
            }
          
        }
        #endregion

    }
}