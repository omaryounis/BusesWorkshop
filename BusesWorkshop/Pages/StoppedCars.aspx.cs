//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using BusesWorkshop.DAL;
//using BusesWorkshop.DAL.Bus;

//namespace BusesWorkshop.Pages
//{
//    public partial class StoppedCars : System.Web.UI.Page
//    {
//        #region "Properties" 
//        WorkshopDataContext dc=new WorkshopDataContext();
//        #endregion

//        #region "Methods"
//        private void FillVehcles()
//        {
//            int? id = null;
//            if (Session["UserID"] != null)
//            {
//                id = int.Parse(Session["UserID"].ToString());
//            }
//            else
//            {
//                Response.Redirect("Login.aspx", false);
//            }
//            ddl_VehcleId.DataSource = from p in dc.Vehcles where p.Company.UserCompanies.Any(x => x.UserID == id) select new { p.VehcleId, p.PlateNo };
//            ddl_VehcleId.TextField = "PlateNo";
//            ddl_VehcleId.ValueField = "VehcleId";
//            ddl_VehcleId.DataBind();
//        }
//        void FillGrid()
//        {
//            grd_StopCars.DataSource = (dc.StoppedCarsSelectById(int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()))).ToList();
//            grd_StopCars.DataBind();
//        }
//        #endregion

//        #region "Events" 
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            Page.Title = "السيارات المتوقفة";
//            if (!IsPostBack)
//            {
//                FillVehcles();
//            }
//        }
     
//        protected void ddl_VehcleId_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            FillGrid();
//            if (ddl_VehcleId.SelectedItem != null)
//            {
//                var query = (from p in dc.Vehcles where p.VehcleId == int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()) select new { p.Active }).SingleOrDefault();
//                if (query.Active == true)
//                {
//                    btn_Reoperate.Enabled = false;
//                    btn_Stop.Enabled = true;



//                    txt_StopDate.Text = "";
//                    txt_StopDate.Enabled = true;
//                    txt_StopReason.Text = "";
//                    txt_StopReason.Enabled = true;
//                    txt_ReoperateDate.Text = "";
//                    txt_ReoperateDate.Enabled = false ;
//                }
//                else
//                {
//                    btn_Reoperate.Enabled = true;
//                    btn_Stop.Enabled = false;



//                    var queryStop = (from p in dc.StoppedCars where p.VaehcleId == int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()) && p.ReoperateDate == null && p.StopDate !=null select new { p.StopDate, p.ReoperateDate, p.StopReason }).ToList().LastOrDefault();
//                    if (queryStop != null)
//                    {
//                        txt_StopDate.Text = queryStop.StopDate.ToShortDateString();
//                        txt_StopDate.Enabled = false;
//                        txt_StopReason.Text = queryStop.StopReason;
//                        txt_StopReason.Enabled = false;
//                        txt_ReoperateDate.Text = "";
//                        txt_ReoperateDate.Enabled = true;
//                    }
//                    else
//                    {
//                        txt_StopDate.Text = "";
//                        txt_StopDate.Enabled = true;
//                        txt_StopReason.Text = "";
//                        txt_StopReason.Enabled = true;
//                        txt_ReoperateDate.Text = "";
                       
//                    }
//                }
//            }
//        }

//        protected void btn_Stop_Click(object sender, EventArgs e)
//        {
//            #region validation
//            if (ddl_VehcleId.SelectedItem == null)
//            {
//                divMsg.Attributes["class"] = "alert alert-danger text-right";
//                lblResult.Text = "عفوا ادخل رقم السيارة";
//                return;
//            }

//            #endregion

//            #region StopCar
//            Vehcle _Vehcle = dc.Vehcles.Single(x => x.VehcleId == int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()));
//            _Vehcle.Active = false;
//            dc.SubmitChanges();



//            StoppedCar _StoppedCar = new StoppedCar();
//            _StoppedCar.VaehcleId = int.Parse(ddl_VehcleId.SelectedItem.Value.ToString());
//            _StoppedCar.StopDate = DateTime.Parse(txt_StopDate.Text);
//            _StoppedCar.StopReason = txt_StopReason.Text;
//            dc.StoppedCars.InsertOnSubmit(_StoppedCar);
//            dc.SubmitChanges();
//            FillGrid();
//            MyClasses.ClearControls(Page);

//            divMsg.Attributes["class"] = "alert alert-success text-right";
//            lblResult.Text = "تم ايقاف السيارة";
//            btn_Reoperate.Enabled = false;
//            btn_Stop.Enabled = false;
//            #endregion

//        }

//        protected void btn_Reoperate_Click(object sender, EventArgs e)
//        {
//            #region validation
//            if (ddl_VehcleId.SelectedItem == null)
//            {
//                divMsg.Attributes["class"] = "alert alert-danger text-right";
//                lblResult.Text = "عفوا ادخل رقم السيارة";
//                return;
//            }

//            if (DateTime.Parse(txt_StopDate.Text) > DateTime.Parse(txt_ReoperateDate.Text))
//            {
//                divMsg.Attributes["class"] = "alert alert-danger text-right";
//                lblResult.Text = "عفوا تاريخ اعادة التشغيل لا يمكن ان يكون قبل تاريخ الايقاف";
//                return; 
//            }
//            #endregion

//            #region ReoperateCar
//            Vehcle _Vehcle = dc.Vehcles.Single(x => x.VehcleId == int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()));
//            _Vehcle.Active = true;
//            dc.SubmitChanges();


//            StoppedCar _StoppedCar = dc.StoppedCars.Single(x => x.VaehcleId == int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()) && x.StopDate != null && x.ReoperateDate == null);
//            _StoppedCar.ReoperateDate = DateTime.Parse(txt_ReoperateDate.Text);
//            dc.SubmitChanges();
//            FillGrid();
//            MyClasses.ClearControls(Page);

//            divMsg.Attributes["class"] = "alert alert-success text-right";
//            lblResult.Text = "تم اعادة السيارة الى التشغيل";
//            btn_Reoperate.Enabled = false;
//            btn_Stop.Enabled = false;
//            #endregion
//        }
//   #endregion
//    }
//}
