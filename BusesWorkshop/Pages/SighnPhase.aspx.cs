using BusesWorkshop.DAL;
using BusesWorkshop.DAL.Bus;
using BusesWorkshop.Pages.PagesPermissions;
using DevExpress.Web;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusesWorkshop
{
    public partial class SighnPhase : System.Web.UI.Page
    {
        #region "prop"
        WorkshopDataContext dcWorkShop = new WorkshopDataContext();

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
   where (p.phase_Step == 0 || p.phase_Step == null) && u.ID == Convert.ToInt16(Session["UserID"].ToString()) && (m.IsClosed == false || m.IsClosed == null) && m.IsAccepted == null//اعتماد

   select new
   {
       LeftId = m.MaintReqId,
       RightId = ((m.CompanyId == c.ID) ? c.ID : 0)
       ,
       MaintReqId = m.MaintReqId,
       RequestDate = m.RequestDate,
       CompName = m.Company.CompName,
       UserName = u.Name,
       UserId = u.ID
   }).ToList();
            if (d != null)
            {
                if (d.ToList().Count > 0)
                {
                    if (dxgrd_Requests != null)
                    {

                        dxgrd_Requests.DataSource = d.ToList().Distinct();
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

        #endregion
        #region "Events"

        protected void Page_Load(object sender, EventArgs e)
        {
            FillGridRequests();
            Page.Title = "مجموعات المستخدمين";
            // WorkshopDataContext dc = new WorkshopDataContext();
            // permitions(dc);
        }

        protected void grd_advanceRequest_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            var Usersource = from u in dcWorkShop.Users
                             select new { u.ID, u.Name };
            var advanceRequestId = e.KeyValue;
            MaintRequest _MaintRequest = dcWorkShop.MaintRequests.Single(x => x.MaintReqId == Convert.ToInt32(advanceRequestId));
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
            int?  nextPhase = 0;
            List<ReqPhase> currentApprovedPhases = dcWorkShop.ReqPhases.Where(c => c.Req_Id == Convert.ToInt16(advanceRequestId.ToString())).ToList();

          
            if (e.CommandArgs.CommandName == "CloseRequest")
            {
                _MaintRequest.IsClosed = true;

                dcWorkShop.SubmitChanges();

            }
            else if (e.CommandArgs.CommandName == "Hold")
            {

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
                        .Select(rp => rp.Phase_Id).OrderByDescending(rp=>rp.Value).FirstOrDefault();//.SingleOrDefault();//.OrderByDescending(rp => rp.Value).LastOrDefault();
                        
                }


                    ReqPhase ReqPhase = new ReqPhase();
                ReqPhase.Req_Id =Convert.ToInt32( advanceRequestId.ToString());
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




            }
            FillGridRequests();
        }
        //        protected void btn_Close_Click(object sender, EventArgs e)
        //{
        //    if (!(sender is ASPxButton button))
        //    {
        //        return;
        //    }

        //    if (!(button.NamingContainer is GridViewDataItemTemplateContainer container))
        //    {
        //        return;
        //    }
        //    int ID = int.Parse(container.KeyValue.ToString());
        //    DAL.MaintRequest _MaintRequest = dcWorkShop.MaintRequests.Single(x => x.MaintReqId == ID);
        //    _MaintRequest.IsClosed = true;
        //    dcWorkShop.SubmitChanges();

        //}
        #endregion

    } 
}