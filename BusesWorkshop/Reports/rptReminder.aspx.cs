using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using BusesWorkshop.DAL.Bus;

namespace BusesWorkshop.Reports
{
    public partial class rptReminder : System.Web.UI.Page
    {
        #region "Methods"
        void BindReport(int? serviceID)
        {

            Dev_rptReminder dev = new Dev_rptReminder(serviceID);

        }
        #endregion

        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null && Convert.ToInt32(Session["UserID"].ToString()) > 0)
                {
                    BindReport(2);
                    
                }
                else
                {
                    Response.Redirect(@"..\Pages\Login.aspx");
                }

            }
        }



        #endregion

        #region ReportViewer Code

        //private void BindReport()
        //{

        //    using (WorkshopDataContext dc = new WorkshopDataContext())
        //    {
        //        DataTable dt = new DataTable();
        //        dt = dc.usp_Services_Reminder(2).CopyToDataTable();

        //        string reportPath = Server.MapPath("../Reports/rptReminder.rdlc");
        //        GenerateReportDirect(rpvReminder, "DataSet1_usp_Services_Reminder", dt, reportPath);




        //    }
        //}

        //public static void GenerateReportDirect(ReportViewer reportViewer, string Datasource, DataTable Dt, string Reportpath) //List<ReportParameter> ReportList
        //{

        //    reportViewer.LocalReport.ReportPath = Reportpath;
        //    ReportDataSource repds = new ReportDataSource(Datasource, Dt);

        //    reportViewer.LocalReport.DataSources.Clear();
        //    reportViewer.LocalReport.DataSources.Add(repds);

        //    reportViewer.LocalReport.Refresh();
        //}

        #endregion
    }
}
