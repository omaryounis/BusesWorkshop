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
using System.Data.SqlClient;
using BusesWorkshop.VM;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Pages
{
    public partial class ServicesSettings : System.Web.UI.Page
    {
        private int ServiceID
        {
            get
            {
                if (ViewState["_ServiceID"] != null)
                {
                    return Convert.ToInt32(ViewState["_ServiceID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_ServiceID", value);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "الخدمات";
            WorkshopDataContext dc = new WorkshopDataContext();
            permissions(dc);
            if (!IsPostBack)
            {
                if (Session["UserID"] != null && Convert.ToInt32(Session["UserID"].ToString()) > 0)
                {
                    //fill_Services();
                    fill_Grid();
                }
                else
                {
                    Response.Redirect(@"..\Pages\Login.aspx");
                }

               
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;
           
                #region Code...

                WorkshopDataContext dc = new WorkshopDataContext();

                if (ServiceID > 0)
                {
                    // update
                    if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
                    {
                   divMsg2.Attributes["class"] = "alert alert-danger text-right";
                        lblResult.Text = "عفوا ليس لديك صلاحية التعديل";
                        return;
                    }
                    ServicesSetting tblService = dc.ServicesSettings.Single(srv => srv.ID == ServiceID);
                  //  tblService.DateV = Convert.ToInt32(txtDateV.Text.Trim());

                    tblService.ServiceName = txtServiceName.Text.Trim();

                    if (string.IsNullOrEmpty(txtDateV.Text.Trim()))
                    {
                        tblService.DateV = null;
                    }
                    else
                    {
                        tblService.DateV = Convert.ToInt32(txtDateV.Text);

                    }
                if (RequesServiceId.SelectedItem != null)
                {
                    tblService.RequestType = int.Parse(RequesServiceId.SelectedItem.Value.ToString());
                }
                else
                {
                    tblService.RequestType = null;
                }

                if (string.IsNullOrEmpty(txtKVPlus.Text.Trim()))
                    {
                        tblService.KVPlus = null;
                    }
                    else
                    {
                        tblService.KVPlus = Convert.ToInt32(txtKVPlus.Text);

                    }


                    if (string.IsNullOrEmpty(txtKVminus.Text.Trim()))
                    {
                        tblService.KVMinus = null;
                    }
                    else
                    {
                        tblService.KVMinus = Convert.ToInt32(txtKVminus.Text);

                    }


                    dc.SubmitChanges();
                    result = tblService.ID;
                }
                else
                {
                    // insert


                    if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
                    {
                        divMsg2.Attributes["class"] = "alert alert-danger text-right";
                        lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                        return;
                    }
                    ServicesSetting tblService = new ServicesSetting();

                    tblService.ServiceName = txtServiceName.Text.Trim();


                    if (string.IsNullOrEmpty(txtDateV.Text.Trim()))
                    {
                        tblService.DateV = null;
                    }
                    else
                    {
                        tblService.DateV = Convert.ToInt32(txtDateV.Text);

                    }
                if (RequesServiceId.SelectedItem != null)
                {
                    tblService.RequestType = int.Parse(RequesServiceId.SelectedItem.Value.ToString());
                }
                else
                {
                    tblService.RequestType = null;
                }

                if (string.IsNullOrEmpty(txtKVPlus.Text.Trim()))
                    {
                        tblService.KVPlus = null;
                    }
                    else
                    {
                        tblService.KVPlus = Convert.ToInt32(txtKVPlus.Text);

                    }


                    if (string.IsNullOrEmpty(txtKVminus.Text.Trim()))
                    {
                        tblService.KVMinus = null;
                    }
                    else
                    {
                        tblService.KVMinus = Convert.ToInt32(txtKVminus.Text);

                    }


                    if (string.IsNullOrEmpty(txtDateV.Text))
                    {
                        tblService.DateV = null;
                    }
                    else
                    {
                        tblService.DateV = int.Parse(string.IsNullOrEmpty(txtDateV.Text.Trim()) ? "0" : txtDateV.Text.Trim().ToString());
                    }

                    dc.ServicesSettings.InsertOnSubmit(tblService);
                    dc.SubmitChanges();

                    result = tblService.ID;

                }

                if (result > 0)
                {
                    divMsg2.Attributes["class"] = "alert alert-success text-right";
                    lblResult.Text = "تم الحفظ بنجاح";

                    // txtNotes.Text = txtKMcount.Text = txtCheckDate.Text = string.Empty;

                }
                else
                {
                    divMsg2.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "حدث خطأ أثنا الحفظ";

                }
            //lblResult.Text = "";
                empty_Controls();
                fill_Grid();

                #endregion
         

           
        }


        

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;


            ServiceID = Convert.ToInt32(gvServicesSetting.DataKeys[row.RowIndex].Value);
            WorkshopDataContext dc = new WorkshopDataContext();

            DataTable dt = new DataTable();
            var query = from service in dc.GetTable<ServicesSetting>().Where(srv => srv.ID == ServiceID) select service;
            dt = query.CopyToDataTable();
            if (dt.Rows.Count > 0)
            {
                txtDateV.Text = dt.Rows[0]["DateV"].ToString();
                txtKVminus.Text = dt.Rows[0]["KVMinus"].ToString();
                txtKVPlus.Text = dt.Rows[0]["KVPlus"].ToString();
                RequesServiceId.Value =dt.Rows[0]["RequestType"] .ToString();

                txtServiceName.Text = dt.Rows[0]["ServiceName"].ToString();

             }
        }

        void fill_Grid()
        {
            WorkshopDataContext dc = new WorkshopDataContext();

            DataTable dt = new DataTable();
            var query = from service in dc.GetTable<ServicesSetting>() select new ServiceSettingVM
            {
                RequestType =service.RequestType == 0 ? "طلب صيانه": service.RequestType == 1? "دعم فنى ":service.RequestType==2?"الكل":"",
                ServiceName=service.ServiceName,
                ID=service.ID,
                DateV=service.DateV.ToString(),
                KVMinus=service.KVMinus.ToString(),
            };
            dt = query.CopyToDataTable();
            
            if (dt.Rows.Count > 0)
            {
                gvServicesSetting.DataSource = dt;
                gvServicesSetting.DataBind();
            }
            else
            {
                gvServicesSetting.DataSource = null;
                gvServicesSetting.DataBind();
            }
        }

        void empty_Controls()
        {

            txtDateV.Text = txtKVminus.Text = txtKVPlus.Text = txtServiceName.Text =RequesServiceId.Text=string.Empty;
            ServiceID = 0;
        }

        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.ServicesSettings.GetHashCode());
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
                        ViewState["AllowDelete"] = 1;
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

        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            if (int.Parse(ViewState["AllowDelete"].ToString()) == 0 || ViewState["AllowDelete"] == null)
            {
               divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ليس لديك صلاحية الحذف";
                return;
            }
            WorkshopDataContext dc = new WorkshopDataContext();
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;


            ServicesSetting _ServicesSetting = dc.ServicesSettings.Single(x => x.ID == int.Parse(gvServicesSetting.DataKeys[row.RowIndex].Value.ToString()));

            //var query = from p in dc.ServicesSettings  where p.ID ==(Convert.ToInt32( gvServicesSetting.DataKeys[row.RowIndex].Value)) select p;
            try
            {
                //foreach (var item in query)
                //{
                dc.ServicesSettings.DeleteOnSubmit(_ServicesSetting);
                //}
                dc.SubmitChanges();
            }
            catch (SqlException ex)
            {

                if (ex.Errors[0].Number == 547)
                {

                    divMsg2.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفو السجل المراد حذفة مرتبط بمدخلات اخري";
                    return;
                }


            }


            fill_Grid();
        }

    }
}
