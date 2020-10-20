using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusesWorkshop.DAL.Bus;
using System.Data.SqlClient;
using System.Data;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Pages
{
    public partial class Alarms : System.Web.UI.Page
    {
        WorkshopDataContext dc = new WorkshopDataContext();
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




            permissions(dc);
            Page.Title = "التنبيهات";
            FillGrid();
        }

        private void FillGrid()
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
            gvAlarms.DataSource = (dc.ShowNotifications(id)).ToList();
            gvAlarms.DataBind();
        }
        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
     

            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;
            var query = from p in dc.Alarms  where p.AlarmId.Equals(Convert.ToInt32(gvAlarms.DataKeys[row.RowIndex].Value)) select p;
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


            FillGrid();
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.Alarms.GetHashCode());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["D"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["D"].ToString()))
                    {
                        Response.Redirect(@"..\Pages\Login.aspx", false);
                    }
                    //    if (dt.Rows[0]["I"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["I"].ToString()))
                    //    {

                    //        btnSave.Enabled = false;
                    //        btn_AddReBillDetails.Enabled = false;
                    //        btn_CancelReBill.Enabled = false;

                    //    }
                    //    if (dt.Rows[0]["U"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["U"].ToString()))
                    //    {
                    //        btnSave.Enabled = false;
                    //        btn_AddReBillDetails.Enabled = false;
                    //        btn_CancelReBill.Enabled = false;
                    //    }
                    //    if (dt.Rows[0]["R"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["R"].ToString()))
                    //    {

                    //    }
                    //}
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
    }
}
