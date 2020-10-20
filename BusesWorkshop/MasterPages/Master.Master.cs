using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BusesWorkshop.DAL.Bus;
using BusesWorkshop.Pages;

namespace BusesWorkshop.Master
{
    public partial class Master : System.Web.UI.MasterPage
    {
        WorkshopDataContext dc = new WorkshopDataContext();
        DataTable sighnInReq = new DataTable();
        SighnPhase SighnPhase = new SighnPhase();
        DistributeRequestsToUsers DistributeRequestsToUsers = new DistributeRequestsToUsers();
        DataTable DistributeReq = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            int? id = null;
            if (Session["UserID"] != null)
            {
                id = int.Parse(Session["UserID"].ToString());
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
            /////////////////
          //  RediredctPermissions();
            DataTable dt = new DataTable();
            dt = dc.ShowLicenseNotifications(id).CopyToDataTable();//dc.ShowNotifications(id).CopyToDataTable();
            rpt.DataSource = dt;
            rpt.DataBind();
            //signIn
            SighnPhase.FillGridRequests();
            var req = Session["dxgrd_Requests"];
            if (req.ToString() != "")
            {
                sighnInReq = (DataTable)req;
            }

            //distribute
            
            DistributeRequestsToUsers.FillGridRequests();
            var dReq = Session["DistReq"];
            if (dReq.ToString() != "")
            {
                DistributeReq = (DataTable)dReq;
            }

            lbl_NotIcon.Text = dt.Rows.Count.ToString();
            if (dt.Rows.Count <= 0)
            {
                reportLink.Visible = false;
            }
            /////////////////
            DataTable Plandt = new DataTable();
            Plandt = dc.ShowPlanNotifications(id).CopyToDataTable();//dc.ShowNotifications(id).CopyToDataTable();
            rptPlan.DataSource = Plandt;
            rptPlan.DataBind();

            lbl_PlanIcon.Text = Plandt.Rows.Count.ToString();
            if (Plandt.Rows.Count <= 0)
            {
                PlanNotLink.Visible = false;
            }
           // FillPlanGrid();
            ////////////////////
            // var Request = dc.ShowMaintainanceNotifications(id).Join(dc.MaintRequests,x=>x.Notification,x=>x.Notes,(x,y)=>new { x.AlarmId, y.IsAccepted,y.IsClosed,y.IsRejected }).Where(x => x.IsAccepted == null && x.IsClosed == null && x.IsRejected == null).Count();//(Convert.ToInt32(Session["UserID"].ToString())).Count();
            //.Where(x => x.IsAccepted == null && x.IsClosed == null && x.IsRejected == null).Count(); ;//dc.MaintRequests.Where(x => x.IsAccepted == null && x.IsClosed == null && x.IsRejected == null).Count();
            var q = (
               from p in dc.MaintRequests
               join d in dc.MaintReqDetails on (int)p.MaintReqId equals (int)d.MaintReqId
               join m in dc.Mobiles on (int)d.MobileId equals (int)m.MobileId
               where  p.IsClosed == false
                select new { مسلسل = p.MaintReqId, التاريخ = p.RequestDate, الشركة = p.Company.CompName }).ToList();

            /*lbl_Request.Text = q.ToList().Count().ToString(); //Request.ToString();
            if (q.ToList().Count() <= 0)
            {
                reportLink2.Visible = false;
            }*/
            var data = (
               from m in dc.MaintRequests
               join u in dc.Users on m.RequestedEmpId equals u.ID
               join uc in dc.UserCompanies on u.ID equals uc.UserID
               join ur in dc.userRequests on uc.UserID equals ur.UserId
               join up in dc.User_Phases on ur.UserId equals up.phases_Id


               where m.IsClosed == false && u.ID ==Convert.ToInt16( Session["UserID"].ToString())
               
               select  new { مسلسل = m.MaintReqId, التاريخ = m.RequestDate, الشركة = m.Company.CompName }).Distinct().ToList();
            Request request = new Request();
            var recdata = request.GetRecData().Where(
                
                p =>
                ((p.Phase_Order == 1&& p.phase_Step == 0 )
                
                ||(p.Phase_Order == 2&& p.phase_Step ==1 ))
              &&  (p.IsClosed == false || p.IsClosed == null)

               
                && p.UserId == Convert.ToInt16(Session["UserID"].ToString())
                ).ToList().Distinct();
            lblUserRequest.Text = recdata.Count().ToString();
                ;



            lbl_UserName.Text =Session["UserName"].ToString() + ": اسم المستخدم"   ;
        }
      
        private void FillPlanGrid()
        {
            int? id = null;
            if (Session["UserID"] != null)
            {
                id = int.Parse(Session["UserID"].ToString());
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
            
            GridView gvPlanAlarms =(GridView)rptPlan.FindControl("gvPlanAlarms");
            gvPlanAlarms.DataSource = (dc.ShowPlanNotifications(id)).ToList();
            gvPlanAlarms.DataBind();
        }
        protected void lnk_Delete_Click(object sender, EventArgs e)
        {


            LinkButton lnk = (LinkButton)sender;
            HiddenField HiddenField = lnk.NamingContainer.FindControl("alaramId") as HiddenField;

            GridViewRow row = (GridViewRow)lnk.NamingContainer;

            var query = from p in dc.Alarms where p.AlarmId.Equals(HiddenField) select p;
            try
            {
                foreach (var item in query)
                {
                    dc.Alarms.DeleteOnSubmit(item);
                }
                dc.SubmitChanges();
            }
            catch
            {
            }


            FillPlanGrid();
        }

        protected void lbRead_Click(object sender, EventArgs e)
        {

            //HtmlAnchor AAlarmId = new planNotUl.Controls
            LinkButton lnk = (LinkButton)sender;
            foreach (HtmlAnchor AAlarmId in planNotUl.Controls)
            {
                if(AAlarmId.ID == "AAlarmId")
                {
                    string id = AAlarmId.InnerText;
                }
              
            }

            //var query = from p in dc.Alarms where p.AlarmId.Equals(hdcontrol)//.Controls.Contains(AAlarmId)) select p;
            //try
            //{
            //    foreach (var item in query)
            //    {
            //        dc.Alarms.DeleteOnSubmit(item);
            //    }
            //    dc.SubmitChanges();
            //}
            //catch
            //{
            //}



        }

        protected void rptPlan_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            //if (e.Item.DataItem != null)
            //{
            //    var row = (Alarm)e.Item.DataItem;
            //    if (row.IsReaded==null)
            //    {
            //        var li = (HtmlGenericControl)e.Item.FindControl("notificationLI");
            //        li.Attributes.Add("class", "new-notification");
            //    }
            //}
        }

        protected void rptPlan_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName=="read")
                {
                var query = from p in dc.Alarms where p.AlarmId.Equals(1) select p;
                try
                {
                    foreach (var item in query)
                    {
                        dc.Alarms.DeleteOnSubmit(item);
                    }
                    dc.SubmitChanges();
                }
                catch
                {
                }


                FillPlanGrid();


            }
        }
       
        public void RediredctPermissions()
        {
           
            //signIn
            SighnPhase.FillGridRequests();
            var d = Session["dxgrd_Requests"];
            if (d.ToString()!="")
            {
sighnInReq = (DataTable)d;
            }
            
            //distribute
            DistributeRequestsToUsers.FillGridRequests();
            var dReq = Session["DistReq"];
            if(dReq.ToString()!="")
            {
 DistributeReq = (DataTable)dReq;
            }
           
            if (sighnInReq.Rows.Count > 0)
            {
                //Response.Redirect("SighnPhase.aspx");
                aNewReq.HRef = "../Pages/SighnPhase.aspx";
            }
            else if(DistributeReq.Rows.Count>0)
            {
                //esponse.Redirect("DistributeRequestsToUsers.aspx");
                aNewReq.HRef = "../Pages/DistributeRequestsToUsers.aspx";

            }
        }
        //public void clkBtn_Click(object sender, EventArgs e)
        //{



        //    if (Session["UserID"] != null)
        //    {
        //        int userID = int.Parse(Session["UserID"].ToString());
        //        var list = dc.ShowPlanNotifications().Where(x => x.UserID == userID).ToList();
        //        if (list.Count > 0)
        //        {
        //            ctx.UsersNotifications.Where(x => x.UserID == userID).ToList().ForEach(x => x.IsSeen = true);
        //            ctx.SubmitChanges();
        //        }

        //    }

        //    notificationDDL.Attributes.Add("class", "nav-notfcation dropdown open");

        //}
        //protected void lnkLogOut_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("../Pages/Login.aspx");
        //}
    }
}
