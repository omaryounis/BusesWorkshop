using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;
using BusesWorkshop.DAL.Bus;
using BusesWorkshop.VM;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Pages
{
    public partial class Mobiles : System.Web.UI.Page
    {
        private int MobileID
        {
            get
            {
                if (ViewState["MobileID"] != null)
                {
                    return Convert.ToInt32(ViewState["MobileID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("MobileID", value);
            }
        }
        WorkshopDataContext dc=new WorkshopDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            permissions(dc);
            FillTreeList();
            Page.Title = "المنقولات";
            if (!IsPostBack)
            {
                FillMainMobiles();
            }
        }

        private void FillTreeList()
        {
            //var value = RequesServiceId.TextField;
            // //   == 0 ? "طلب صيانه" : دعم" فنى ";
            //MobileTL.DataSource = from p in dc.Mobiles select new { p.MobileId, p.MobileParentId, p.MobileName, p.ServiceRequest, MobileParentName = p.Mobile1.MobileName,
            //    value = p.Mobile1.ServiceRequest };


            //// var value = RequesServiceId.;
            //MobileTL.DataBind();

            List<MobilesVM> q = new List<MobilesVM>();
            var t = from c in dc.Mobiles
                    select new MobilesVM
                    {
                        // c.MobileId, c.MobileParentId, c.MobileName, , 
                        MobileId = c.MobileId,
                        MobileParentId = c.MobileParentId,

                        MobileParentName = c.Mobile1.MobileName,
                        ServiceRequest = c.Mobile1.ServiceRequest == 0 ? "طلب صيانه" : c.Mobile1.ServiceRequest == 1 ? "دعم فنى ":c.Mobile1.ServiceRequest==2 ? "الكل":""  ,
                    };

            MobileTL.DataSource = t.ToList();
            // var value = RequesServiceId.;
            MobileTL.DataBind();
        }

        private void FillMainMobiles()
        {
            ddl_MobileParentId.DataSource = from p in dc.Mobiles select new { p.MobileId, p.MobileName };
            ddl_MobileParentId.TextField = "MobileName";
            ddl_MobileParentId.ValueField = "MobileId";
            ddl_MobileParentId.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (MobileID > 0)
            {
                #region save
                Mobile _Mobile = dc.Mobiles.Single(x=> x.MobileId == MobileID );
                if (ddl_MobileParentId.SelectedItem != null)
                {
                    _Mobile.MobileParentId = int.Parse(ddl_MobileParentId.SelectedItem.Value.ToString());
                }
                else
                {
                    _Mobile.MobileParentId = null;
                }
                if (RequesServiceId.SelectedItem != null)
                {
                    _Mobile.ServiceRequest = int.Parse(RequesServiceId.SelectedItem.Value.ToString());
                }
                else
                {
                    _Mobile.ServiceRequest = null;
                }

                _Mobile.MobileName = txt_MobileName.Text;
                

            
                dc.SubmitChanges();
                result = _Mobile.MobileId;
                #endregion}
            }
            else
            {

                #region save
                Mobile _Mobile = new Mobile();
                if (ddl_MobileParentId.SelectedItem != null)
                {
                    _Mobile.MobileParentId = int.Parse(ddl_MobileParentId.SelectedItem.Value.ToString());
                }
                else
                {
                    _Mobile.MobileParentId = null;
                }

                if (RequesServiceId.SelectedItem != null)
                {
                   _Mobile.ServiceRequest = int.Parse(RequesServiceId.SelectedItem.Value.ToString());
                }
                else
                {
                    _Mobile.ServiceRequest = null;
                }
                _Mobile.MobileName = txt_MobileName.Text;
              
                dc.Mobiles.InsertOnSubmit(_Mobile);
                dc.SubmitChanges();
                result = _Mobile.MobileId;
                #endregion}
            }
            if (result > 0)
            {
                divMsg.Attributes["class"] = "alert alert-success text-right";
                lblResult.Text = "تم الحفظ بنجاح";

            }
            else
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "حدث خطأ أثنا الحفظ";
                // divMsg. = "alert alert-danger text-right"; 
            }
          
            MyClasses.ClearControls(Page);
            FillTreeList();
        }

        protected void ImgBtnUpdate_Click(object sender, ImageClickEventArgs e)
        {

          //  int result = 0;
            var query = (from p in dc.Mobiles where p.MobileId == int.Parse(MobileTL.FocusedNode.GetValue("MobileId").ToString()) select new { p.MobileId, p.MobileParentId, p.MobileName,p.ServiceRequest}).SingleOrDefault();
            ddl_MobileParentId.Value = query.MobileId.ToString();
          //  txt_MobileName.Text=query.MobileParentId.ToString();
            RequesServiceId.Value = query.ServiceRequest.ToString();
            txt_MobileName.Text = query.MobileName;
            MobileID = int.Parse(MobileTL.FocusedNode.GetValue("MobileId").ToString());
           
            //if (result > 0)
            //{
            //    divMsg.Attributes["class"] = "alert alert-success text-right";
            //    lblResult.Text = "تم التعديل بنجاح";

            //}
            //else
            //{
            //    divMsg.Attributes["class"] = "alert alert-danger text-right";
            //    lblResult.Text = "حدث خطأ أثنا التعديل";
            //    // divMsg. = "alert alert-danger text-right"; 
            //}
            FillTreeList();
        }

        protected void ImgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            if (MobileTL.FocusedNode.HasChildren)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا السجل مرتبط بسجلات اخري";
                return;
            }
            var Query = from p in dc.Mobiles where p.MobileId == int.Parse(MobileTL.FocusedNode.GetValue("MobileId").ToString()) select p;
            try
            {
                foreach (var item in Query)
                {
                    dc.Mobiles.DeleteOnSubmit(item);
                }
                dc.SubmitChanges();
            }

            catch (SqlException ex)
            {

                if (ex.Errors[0].Number == 547)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفو السجل المراد حذفة مرتبط بمدخلات اخري";
                    return;
                }


            }
            FillTreeList();
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.Mobiles.GetHashCode());
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

        protected void RequesServiceId_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
    }
}
