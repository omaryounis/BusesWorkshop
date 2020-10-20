using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using BusesWorkshop.DAL.Bus;
using System.Text;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Pages
{
    public partial class CarFollowUp : System.Web.UI.Page
    {
        #region "Properties" 
        WorkshopDataContext dc = new WorkshopDataContext();
        private int UserID
        {
            get
            {
                if (ViewState["_UserID"] != null)
                {
                    return Convert.ToInt32(ViewState["_UserID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_UserID", value);
            }
        }
        #endregion
        #region "Methods"
        private void FillCards()
        {
            if (ddl_EmployeeId.SelectedItem != null && ddl_VehcleId.SelectedItem != null)
            {
                ddl_FuelCardId.DataSource = (dc.FuelCardsSelectByFuelTypeAndSuperVisorID(int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()), int.Parse(ddl_EmployeeId.SelectedItem.Value.ToString()))).ToList();
                ddl_FuelCardId.TextField = "CardName";
                ddl_FuelCardId.ValueField = "FuelCardId";
                ddl_FuelCardId.DataBind();
            }

        }
        private void FillDrivers()
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
            ddl_DriverId.DataSource = from p in dc.Employees where p.IsDriver.Equals(true) && p.Company.UserCompanies.Any(x => x.UserID == id) select new { p.EmployeeId, p.EmployeeName };

            ddl_DriverId.TextField = "EmployeeName";
            ddl_DriverId.ValueField = "EmployeeId";
            ddl_DriverId.DataBind();
        }

        private void FillVehcles()
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
            ddl_VehcleId.DataSource = from p in dc.Vehcles where p.Company.UserCompanies.Any(x => x.UserID == id) && p.Active == true select new { p.VehcleId, p.PlateNo };
            ddl_VehcleId.TextField = "PlateNo";
            ddl_VehcleId.ValueField = "VehcleId";
            ddl_VehcleId.DataBind();
        }

        private void FillMainSuperVisors()
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

            ddl_EmployeeId.DataSource = from p in dc.Users where p.ID == id select new { p.ID, p.Name };
            ddl_EmployeeId.TextField = "Name";
            ddl_EmployeeId.ValueField = "ID";
            ddl_EmployeeId.DataBind();

        }
        private void CalcAverage()
        {
            if (txt_CounterReading.Text == "" || txt_lastLitres.Text == "") return;
            txt_Average.Text = ((decimal.Parse(string.IsNullOrEmpty(txt_CounterReading.Text) ? "0" : txt_CounterReading.Text) - decimal.Parse(string.IsNullOrEmpty(txt_LastSpplyCounter.Text) ? "0" : txt_LastSpplyCounter.Text)) / decimal.Parse(string.IsNullOrEmpty(txt_lastLitres.Text) ? "0" : txt_lastLitres.Text)).ToString();

        }

        private void DoSave()
        {
            if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                return;
            }


            #region Validation for operation
            if (DateTime.Parse(txt_Date.Text) > DateTime.Now)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا  لا يمكن ادخال تاريخ كارت المتابعة تالي لليوم الحالي";
                return;
            }
            if (int.Parse(txt_PreviousReading.Text) > int.Parse(txt_CounterReading.Text))
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا قراءة العداد الحالية لا يمكن ان تكون اقل من السابقة";
                return;
            }


            if (txt_Destination.Text == "")
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا المسافة المقطوعة فارغة";
                return;
            }


            if (ddl_DriverId.SelectedItem == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل اسم السائق";
                return;

            }
            if (ddl_VehcleId.SelectedItem == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل رقم اللوحة";
                return;

            }

            //string confirmValue = Request.Form["confirm_value"];
            //if (confirmValue == "Yes")
            //{
            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);

            //}
            //else
            //{
            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            //}

            #endregion
            #region Validation for Fuels
            if (ddl_FuelCardId.SelectedItem != null || ddl_EmployeeId.SelectedItem != null || txt_Count.Text != string.Empty || txt_Total.Text != string.Empty)
            {

                if (ddl_FuelCardId.SelectedItem == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ادخل اسم الكارت";
                    return;
                }
                if (ddl_EmployeeId.SelectedItem == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ادخل مسئول الخدمة";
                    return;
                }
                if (txt_Count.Text == string.Empty)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عدد الكروت المطلوبة";
                    return;
                }
                if (int.Parse(string.IsNullOrEmpty(txt_CardsAvailable.Text) ? "0" : txt_CardsAvailable.Text) < int.Parse(string.IsNullOrEmpty(txt_Count.Text) ? "0" : txt_Count.Text))
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا عدد الكروت المتاحة اقل من المطلوب";
                    return;

                }



            }

            #endregion

            #region save

            if (ddl_FuelCardId.SelectedItem == null || ddl_EmployeeId.SelectedItem == null || txt_Count.Text == string.Empty || txt_Total.Text == string.Empty)
            {

                dc.FollowUpInsert(int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()), int.Parse(ddl_DriverId.SelectedItem.Value.ToString()), DateTime.Parse(txt_Date.Text), int.Parse(txt_CounterReading.Text), int.Parse(txt_Destination.Text), txt_Path.Text, txt_Accompanigns.Text, txt_Notes.Text, null, null, null, null, null);

            }
            else
            {
                dc.FollowUpInsert(int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()), int.Parse(ddl_DriverId.SelectedItem.Value.ToString()), DateTime.Parse(txt_Date.Text), int.Parse(txt_CounterReading.Text), int.Parse(txt_Destination.Text), txt_Path.Text, txt_Accompanigns.Text, txt_Notes.Text, int.Parse(ddl_FuelCardId.SelectedItem.Value.ToString()), int.Parse(ddl_EmployeeId.SelectedItem.Value.ToString()), int.Parse(txt_Count.Text), decimal.Parse(txt_Value.Text), decimal.Parse(txt_Total.Text));

            }

            #endregion


            divMsg.Attributes["class"] = "alert alert-success text-right";
            lblResult.Text = "تم الحفظ بنجاح";

            MyClasses.ClearControls(Page);
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.CarFollowUp.GetHashCode());
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
            Page.Title = "كارت متابعة حركة السيارة";
            permissions(dc);
            if (!IsPostBack)
            {

                FillVehcles();
                FillDrivers();

                FillMainSuperVisors();
                txt_Date.Text = DateTime.Now.ToShortDateString();
            }
        }

        protected void ddl_VehcleId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_VehcleId.SelectedItem != null)
            {

                txt_CounterReading.Text = string.Empty;
                txt_Destination.Text = string.Empty;
                var query = (from p in dc.Vehcles where p.VehcleId == int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()) select new { p.CurrentReading, p.MainDriver, p.CounterReadingStart }).SingleOrDefault();
                if (query.CurrentReading == null || query.CurrentReading == 0)
                {

                    txt_PreviousReading.Text = query.CounterReadingStart.ToString();
                }
                else
                {

                    txt_PreviousReading.Text = query.CurrentReading.ToString();
                }

                ddl_DriverId.Value = query.MainDriver.ToString();


                var QuerylastInfo = (from p in dc.LastSupplyInfo(int.Parse(ddl_VehcleId.SelectedItem.Value.ToString())) select new { p.Date, p.LastReading, p.LitresNo }).LastOrDefault();
                try
                {
                    txt_lastLitres.Text = QuerylastInfo.LitresNo.ToString();
                }
                catch
                {
                    txt_lastLitres.Text = string.Empty;
                }
                try
                {
                    txt_LastSupplyDate.Text = QuerylastInfo.Date.ToShortDateString();
                }
                catch
                {

                    txt_LastSupplyDate.Text = string.Empty;
                }
                try
                {
                    txt_LastSpplyCounter.Text = QuerylastInfo.LastReading.ToString();
                }
                catch
                {
                    txt_LastSpplyCounter.Text = string.Empty;

                }


                CalcAverage();
            }
            FillCards();
        }

        protected void txt_CounterReading_TextChanged(object sender, EventArgs e)
        {
            if (txt_PreviousReading.Text == "") return;

            txt_Destination.Text = (int.Parse(string.IsNullOrEmpty(txt_CounterReading.Text) ? "0" : txt_CounterReading.Text) - int.Parse(string.IsNullOrEmpty(txt_PreviousReading.Text) ? "0" : txt_PreviousReading.Text)).ToString();


            CalcAverage();
        }

        protected void ddl_FuelCardId_SelectedIndexChanged(object sender, EventArgs e)
        {

            txt_CardsAvailable.Text = string.Empty;
            txt_Count.Text = string.Empty;
            txt_Value.Text = string.Empty;
            txt_Total.Text = string.Empty;
            if (ddl_EmployeeId.SelectedItem == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل اسم المشرف";
                return;
            }

            int empid = int.Parse(ddl_EmployeeId.SelectedItem.Value.ToString());
            int cardId = int.Parse(ddl_FuelCardId.SelectedItem.Value.ToString());
            var queryy = (from p in dc.CardsAvailableNo(empid, cardId) select new { p.Available }).SingleOrDefault();
            txt_CardsAvailable.Text = queryy.Available.ToString();


            if (ddl_FuelCardId.SelectedItem != null)
            {
                var query = (from p in dc.FuelCardsDefinitions where p.FuelCardId == int.Parse(ddl_FuelCardId.SelectedItem.Value.ToString()) select new { p.Price }).SingleOrDefault();
                txt_Value.Text = query.Price.ToString();
            }
        }

        protected void ddl_EmployeeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCards();
        }

        protected void txt_Count_TextChanged(object sender, EventArgs e)
        {
            txt_Total.Text = string.Empty;

            txt_Total.Text = (decimal.Parse(string.IsNullOrEmpty(txt_Value.Text) ? "0" : txt_Value.Text) * (decimal.Parse(string.IsNullOrEmpty(txt_Count.Text) ? "0" : txt_Count.Text))).ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            StringBuilder msg = new StringBuilder();
            msg.AppendFormat("سوف يتم حفظ كرت متابعة", Environment.NewLine);
            msg.Append(Environment.NewLine);
            msg.AppendLine("للسيارة رقم  ");
            msg.AppendLine(ddl_VehcleId.SelectedItem.Text);
            msg.Append(Environment.NewLine);
            msg.AppendLine("بتاريخ  ");
            msg.AppendLine(txt_Date.Text);
            msg.Append(Environment.NewLine);
            msg.AppendLine("وقراءة عداد  ");
            msg.AppendLine(txt_CounterReading.Text);
            msg.Append(Environment.NewLine);
            msg.AppendLine("عدد كروت و قود  ");
            msg.AppendLine(txt_Count.Text);
            msg.Append(Environment.NewLine);
            msg.AppendLine("!!!!برجاء التأكد من البيانات قبل الادخال");
            msg.Append(Environment.NewLine);
            msg.AppendLine(" ???هل تريد الاستمرار");
            pnlCheckMessage.Visible = true;

            msg.Replace(Environment.NewLine, "<br />").Replace("\r", "<br />").Replace("\n", "<br />");
            lblCheckMSG.Text = msg.ToString();
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {

            pnlCheckMessage.Visible = false;
            lblCheckMSG.Text = string.Empty;
            DoSave();

        }

        protected void btnCancelCheck_Click(object sender, EventArgs e)
        {
            pnlCheckMessage.Visible = false;
            lblCheckMSG.Text = string.Empty;
        }

        #endregion
    }
}
