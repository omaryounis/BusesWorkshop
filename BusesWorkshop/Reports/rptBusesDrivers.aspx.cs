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
    public partial class rptBusesDrivers : System.Web.UI.Page
    {
        #region "Properties"
        WorkshopDataContext dc = new WorkshopDataContext();
        #endregion
        #region "Mehods" 
        void fill_Buses()
        {
            ddlBuses.DataSource = from p in dc.Vehcles select new { p.VehcleId, p.PlateNo };
            ddlBuses.TextField = "PlateNo";
            ddlBuses.ValueField = "VehcleId";
            ddlBuses.DataBind();


        }


        void BindReport(int? prmBusID)
        {
            Dev_rptBusesDrivers dev = new Dev_rptBusesDrivers(prmBusID);
        }

        #endregion
        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null && Convert.ToInt32(Session["UserID"].ToString()) > 0)
                {
                    BindReport(null);

                }
                else
                {
                    Response.Redirect(@"..\Pages\Login.aspx");
                }

                fill_Buses();
            }
        }

     
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            BindReport(Convert.ToInt32(ddlBuses.SelectedItem.Value));

        }

        #endregion

        #region reportViewer Code

        //private void BindReport(int? prmBusID)
        //{

        //    using (WorkshopDataContext dc = new WorkshopDataContext())
        //    {
        //        DataTable dt = new DataTable();
        //        dt = dc.usp_BusesDrivers(prmBusID).CopyToDataTable();

        //        string reportPath = Server.MapPath("../Reports/rptBusesDrivers.rdlc");
        //        GenerateReportDirect(rpvBusDriver, "DataSet1_usp_BusesDrivers", dt, reportPath);




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
