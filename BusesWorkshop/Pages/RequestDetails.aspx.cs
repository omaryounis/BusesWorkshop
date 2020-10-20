using BusesWorkshop.DAL.Bus;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusesWorkshop.Pages
{
    public partial class RequestDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //#region "Fields"
            //RealEstateDataContext dcReal = new BusesWorkshop.DAL.RealEstateDataContext();
            //WorkshopDataContext dcWorkShop = new BusesWorkshop.DAL.WorkshopDataContext();

            //#endregion
            //#region "Methods"
            //void FillGrid()
            //{
            //    var query = (from p in dcWorkShop.MaintRequests where p.MaintReqId == Convert.ToInt32(grd_Requests.SelectedRow.Cells[1].Text) select new { p.CompanyId, p.PriorHigh, p.PriorLow, p.PriorUrgent, p.RequestDate, p.Notes, p.LocationId }).SingleOrDefault();


            //    txt_Notes.Text = query.Notes.ToString();
            //    DataTable dt = new DataTable();
            //    dt.Columns.Add("MainLocation");
            //    dt.Columns.Add("Location");
            //    dt.Columns.Add("Floor");

            //    ddl_LocationId.Value = query.LocationId.ToString();

            //    var queryParentLocation = (dcReal.Sp_SelectParentLocationById(int.Parse(query.LocationId.ToString()))).SingleOrDefault();
            //    var queryMainLocation = (dcReal.Sp_SelectParentLocationById(int.Parse(queryParentLocation.ParentID.ToString()))).SingleOrDefault();
            //    ddl_ParentId.Value = queryMainLocation.ParentID.ToString();
            //    var floor = dcReal.AssetLocationsWithLevelsSelect_Vws.Where(x => x.level == 3 && x.ParentID == SectionID);


            //    var queryDetail = from p in dcWorkShop.MaintReqDetails where p.MaintReqId == Convert.ToInt32(grd_Requests.SelectedRow.Cells[1].Text) select new { p.Mobile.MobileName, p.PicDescription, p.ConfigDetail.ConfigDetailName };
            //    grd_WorksNeeded.DataSource = queryDetail;
            //    grd_WorksNeeded.DataBind();


            //    var queryPic = (from p in dcWorkShop.MaintReqPictures where p.MaintReqId == Convert.ToInt32(grd_Requests.SelectedRow.Cells[1].Text) select new { p.PicturePath, p.Description }).ToList();
            //    grd_Pictures.DataSource = queryPic;
            //    grd_Pictures.DataBind();

            //}
            //#endregion
            #region "Events"
            if (!IsPostBack)
            {
                MaintRequestProcessing.recID = Convert.ToInt32(Request.QueryString["recID"]);
            }
            #endregion



        }
    }
}