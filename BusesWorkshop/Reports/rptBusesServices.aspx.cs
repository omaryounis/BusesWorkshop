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
    public partial class rptBusesServices : System.Web.UI.Page
    {
        #region "Methods"
        void fill_Buses()
        {
            WorkshopDataContext dc = new WorkshopDataContext();

            DataTable dt = new DataTable();
            var query = from bus in dc.GetTable<Buse>()
                        join
                     drv in dc.GetTable<Driver>() on bus.DriverID equals drv.ID
                        select new { bus.ID, bus.PlateNumber, drv.DriverName, fullName = string.Format("حافلة رقم {0}", bus.PlateNumber) };




            dt = query.CopyToDataTable();
            if (dt.Rows.Count > 0)
            {
                ddlBuses.DataSource = dt;
                ddlBuses.DataTextField = "fullName";
                ddlBuses.DataValueField = "ID";
                ddlBuses.DataBind();
            }
            else
            {
                ddlBuses.DataSource = null;
                ddlBuses.DataBind();
            }
        }

        void BindReport(int? prmBusID)
        {

            Dev_rptBusesServices dev = new Dev_rptBusesServices(prmBusID);
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

            BindReport(Convert.ToInt32(ddlBuses.SelectedValue));

        }
        #endregion
        #region ReportViewer old code

        //public static void GenerateReportDirect(ReportViewer reportViewer, string Datasource, DataTable Dt, string Reportpath) //List<ReportParameter> ReportList
        //{

        //    reportViewer.LocalReport.ReportPath = Reportpath;
        //    ReportDataSource repds = new ReportDataSource(Datasource, Dt);

        //    reportViewer.LocalReport.DataSources.Clear();
        //    reportViewer.LocalReport.DataSources.Add(repds);

        //    reportViewer.LocalReport.Refresh();
        //}



        //private void BindReport(int? prmBusID)
        //{

        //    using (WorkshopDataContext dc = new WorkshopDataContext())
        //    {
        //        DataTable dt = new DataTable();
        //        dt = dc.usp_BusesServices_Select(prmBusID).CopyToDataTable();

        //        string reportPath = Server.MapPath("../Reports/rptBusesServices.rdlc");
        //        GenerateReportDirect(rpvBusesServices, "DataSet1_usp_BusesServices_Select", dt, reportPath);

        //    }
        //}

        #endregion
    }
}
