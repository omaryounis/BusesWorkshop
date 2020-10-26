using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusesWorkshop.DAL.Bus;
using System.Data;
using BusesWorkshop.DAL.Accounting;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Reports
{
    public partial class rpt_MaintRequests : System.Web.UI.Page
    {
        #region "Properties"
        WorkshopDataContext dc = new WorkshopDataContext();
        #endregion
        #region "Methods"
        private void FillEmployees()
        {
            ddl_RequestedEmpId.DataSource = from p in dc.Users select new { p.ID, p.Name };
            ddl_RequestedEmpId.TextField = "Name";
            ddl_RequestedEmpId.ValueField = "ID";
            ddl_RequestedEmpId.DataBind();
        }

        private void FillCompanies()
        {
            ddl_CompanyId.DataSource = from p in dc.Companies select new { p.ID, p.CompName };
            ddl_CompanyId.TextField = "CompName";
            ddl_CompanyId.ValueField = "ID";
            ddl_CompanyId.DataBind();
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.rpt_MaintRequests.GetHashCode());
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
        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillCompanies();
                FillEmployees();
            }
        }

      
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bool? IsClosed = null;
            bool? IsNotClosed = null;
            int? CompanyId = null;
            int? RequestedEmpId = null;
            int? DelayDays = null;
            bool? PriorUrgent = null;
            bool? PriorHigh = null;
            bool? PriorLow = null;

            if (chk_IsActive.Checked == true)
            {
                IsClosed = chk_IsActive.Checked;
            }
            else
            {
                IsClosed = null;
            }

            if (chk_IsInActive.Checked == true)
            {
                IsNotClosed = chk_IsInActive.Checked;
            }
            else
            {
                IsNotClosed = null;
            }
            if (ddl_CompanyId.SelectedItem != null)
            {
                CompanyId = int.Parse(ddl_CompanyId.SelectedItem.Value.ToString());
            }
            else
            {
                CompanyId = null;
            }
            if (ddl_RequestedEmpId.SelectedItem != null)
            {
                RequestedEmpId = int.Parse(ddl_RequestedEmpId.SelectedItem.Value.ToString());
            }
            else
            {
                RequestedEmpId = null;
            }
            if (txt_DelayDays.Text != string.Empty)
            {
                DelayDays = int.Parse(txt_DelayDays.Text);
            }
            else
            {
                DelayDays = null;
            }
            if (chk_PriorUrgent.Checked == true)
            {
                PriorUrgent = chk_PriorUrgent.Checked;
            }
            else
            {
                PriorUrgent = null;
            }
            if (chk_PriorHigh.Checked == true)
            {
                PriorHigh = chk_PriorHigh.Checked;
            }
            else
            { 
                PriorHigh = null;
            }
            if (chk_PriorLow.Checked == true)
            {
                PriorLow = chk_PriorLow.Checked;
            }
            else
            {
                PriorLow = null;
            }
            WorkshopDataContext db = new WorkshopDataContext();
            RealEstateDataContext real = new RealEstateDataContext();

            var data = db.MaintRequests.ToList()
                .Select(x => new Dev_MaintRequestsVM
                {
                    IsClosed = x.IsClosed,
                    CompanyId = x.CompanyId,
                    RequestedEmpId = x.RequestedEmpId,
                    MaintReqId = x.MaintReqId,
                    RequestDate = x.RequestDate,
                    CompName = x.Company.CompName,
                    Priorty = x.PriorHigh == true ? "عاجل" : x.PriorUrgent == true ? "متوسط" : "منخفض",
                    UserName = db.Users.FirstOrDefault(c => c.ID == x.RequestedEmpId).Name,
                    DaysNumber = x.RequestDate != null ? Convert.ToInt32((DateTime.Now - x.RequestDate).TotalDays) + 1 : 0,
                    LocationName = real.Ast_Locations.FirstOrDefault(c => c.ID == x.LocationId).LocationName,
                }).Where(x =>
                   (IsClosed == null || x.IsClosed == IsClosed) &&
                   (IsNotClosed == null || x.IsClosed == false) &&
                   (PriorHigh == null || x.PriorHigh == PriorHigh) &&
                   (PriorLow == null || x.PriorLow == PriorLow) &&
                   (PriorUrgent == null || x.PriorUrgent == PriorUrgent) &&
                   (CompanyId == null || x.CompanyId == CompanyId) &&
                   (RequestedEmpId == null || x.RequestedEmpId == RequestedEmpId) &&
                   (DelayDays == null || x.DaysNumber == DelayDays)
                )
            .ToList();

            var report = new Dev_MaintRequests { DataSource = data };
            rptMainRequest.OpenReport(report);


            //Dev_MaintRequests rpt = new Dev_MaintRequests(IsClosed , IsNotClosed , CompanyId , RequestedEmpId , DelayDays , PriorUrgent , PriorHigh , PriorLow );


        }
        #endregion
    }
}
