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
using BusesWorkshop.DAL.Bus;
using System.Net.Mail;
using System.Data.SqlClient;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Pages
{

    public partial class Employees : System.Web.UI.Page
    {
        private int EmployeesID
        {
            get
            {
                if (ViewState["_EmployeesID"] != null)
                {
                    return Convert.ToInt32(ViewState["_EmployeesID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_EmployeesID", value);
            }
        }
        WorkshopDataContext dc = new WorkshopDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "الموظفين";
            permissions(dc);
            if (!IsPostBack)
            {
                fill_Companies();
                //FillVehcles();
                FillLicenseTypes();


                FillSpecialiazations();

            }
        }

        private void FillSpecialiazations()
        {
            ddl_SpecializationId.DataSource = from p in dc.ConfigDetails where p.MasterId == 12 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_SpecializationId.TextField = "ConfigDetailName";
            ddl_SpecializationId.ValueField = "ConfigDetailId";
            ddl_SpecializationId.DataBind();
        }

        private void FillLicenseTypes()
        {
            dll_LicenseType.DataSource = from p in dc.ConfigDetails where p.MasterId == 3 select new { p.ConfigDetailId, p.ConfigDetailName };
            dll_LicenseType.TextField = "ConfigDetailName";
            dll_LicenseType.ValueField = "ConfigDetailId";
            dll_LicenseType.DataBind();
        }

        private void FillVehcles()
        {
            if (ddl_CompanyId.SelectedItem != null)
            {
                ddl_LastVehcle.DataSource = from p in dc.Vehcles where p.CompanyId == int.Parse(ddl_CompanyId.SelectedItem.Value.ToString()) select new { p.VehcleId, p.PlateNo };
                ddl_LastVehcle.TextField = "PlateNo";
                ddl_LastVehcle.ValueField = "VehcleId";
                ddl_LastVehcle.DataBind();
            }
        }
        void fill_Companies()
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
            WorkshopDataContext dc = new WorkshopDataContext();

            ddl_CompanyId.DataSource = from p in dc.Companies where p.UserCompanies.Any(x => x.UserID == id) select new { p.ID, p.CompName };
            ddl_CompanyId.ValueField = "ID";
            ddl_CompanyId.TextField = "CompName";
            ddl_CompanyId.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;
            #region Validation
            try
            {
                if (ddl_CompanyId.SelectedItem.Value == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "ادخل اسم الشركة";
                    return;
                }
            }
            catch

            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "ادخل اسم الشركة";
                return;
            }
            if (rd_IsDriver.Checked == true)
            {
                if (txt_DrivingLicenseId.Text == "")
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا اذا كانت المهنة سائق عليك ادخال رقم رخصة القيادة";
                    return;
                }
                if (txt_DrivingLicenseExpiryDate.Text == "")
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا اذا كانت المهنة سائق عليك ادخال تاريخ انتهاء الرخصة";
                    return;
                }

                try
                {
                    if (dll_LicenseType.Value == null || dll_LicenseType.Value == "" || dll_LicenseType.SelectedItem.Value == null)
                    {
                        divMsg.Attributes["class"] = "alert alert-danger text-right";
                        lblResult.Text = "عفوا اذا كانت المهنة سائق عليك ادخال نوع الرخصة";
                        return;
                    }
                }
                catch
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا اذا كانت المهنة سائق عليك ادخال نوع الرخصة";
                    return;
                }

            }
            if (txt_Mail.Text != "")
            {
                try
                {
                    MailAddress mailAddress = new MailAddress(txt_Mail.Text);
                }
                catch
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا البريد الالكتروني غير صالح";
                    return;

                }
            }

            #endregion
            if (EmployeesID > 0)
            {
                #region UpDate
                if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية التعديل";
                    return;
                }
                var queryL = (from p in dc.Employees where p.EmployeeId == EmployeesID select new { p.DrivingLicenseExpiryDate }).SingleOrDefault();
                if (txt_DrivingLicenseExpiryDate.Text != string.Empty)
                {
                    if (queryL.DrivingLicenseExpiryDate > DateTime.Parse(txt_DrivingLicenseExpiryDate.Text))
                    {
                        divMsg.Attributes["class"] = "alert alert-danger text-right";
                        lblResult.Text = "عفوا لا يمكم تعديل تاريخ انتهاء الرخصة بتاريخ سابق لة ";
                        return;
                    }
                }
                #endregion
                Employee _Employees = dc.Employees.Single(employee => employee.EmployeeId == EmployeesID);
                _Employees.CompanyID = int.Parse(ddl_CompanyId.SelectedItem.Value.ToString());
                _Employees.IsDriver = (bool)rd_IsDriver.Checked;

                _Employees.IsTecnician = (bool)rd_IsTechnician.Checked;
                _Employees.EmployeeName = txt_EmployeeName.Text;
                try
                {
                    _Employees.LastVehcle = int.Parse(ddl_LastVehcle.SelectedItem.Value.ToString());
                }
                catch
                {
                    _Employees.LastVehcle = null;
                }
                _Employees.Salary = decimal.Parse(string.IsNullOrEmpty(txt_Salary.Text) ? "0" : txt_Salary.Text);
                _Employees.RatePerHour = decimal.Parse(string.IsNullOrEmpty(txt_RatePerHour.Text) ? "0" : txt_RatePerHour.Text);
                _Employees.DrivingLicenseId = txt_DrivingLicenseId.Text;
                try
                {
                    _Employees.LicenseType = int.Parse(dll_LicenseType.SelectedItem.Value.ToString());

                }
                catch
                {
                    _Employees.LicenseType = null;
                }
                DateTime temp;
                if (DateTime.TryParse(txt_DrivingLicenseExpiryDate.Text, out temp))
                {
                    _Employees.DrivingLicenseExpiryDate = DateTime.Parse(txt_DrivingLicenseExpiryDate.Text);
                }
                else
                {
                    _Employees.DrivingLicenseExpiryDate = null;
                }
                if (ddl_SpecializationId.SelectedItem != null)
                {
                    _Employees.SpecializationId = int.Parse(ddl_SpecializationId.SelectedItem.Value.ToString());
                }
                else
                {
                    _Employees.SpecializationId = null;

                }
                _Employees.AccomodationNo = txt_AccomodationNo.Text;
                _Employees.EmployeeAddress = txt_EmployeeAddress.Text;
                _Employees.Mobile = txt_Mobile.Text;
                _Employees.Tel = txt_Tel.Text;
                _Employees.Mail = txt_Mail.Text;
                _Employees.Notes = txt_Notes.Text;

                dc.SubmitChanges();
                result = _Employees.EmployeeId;

                #region delete Document Alarm from alarms
                if (queryL.DrivingLicenseExpiryDate < DateTime.Parse(txt_DrivingLicenseExpiryDate.Text))
                {
                    var queryAl = from p in dc.Alarms where p.Name == txt_EmployeeName.Text && p.Type == "License" && p.counter == null && p.PlanId == null && p.Date == queryL.DrivingLicenseExpiryDate select p;
                    try
                    {
                        foreach (var item in queryAl)
                        {
                            dc.Alarms.DeleteOnSubmit(item);
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
                }
                #endregion
            }
            else
            {
                #region save

                if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                    return;
                }
                Employee _Employees = new Employee();
                _Employees.IsDriver = (bool)rd_IsDriver.Checked;
                _Employees.IsTecnician = (bool)rd_IsTechnician.Checked;
                _Employees.CompanyID = int.Parse(ddl_CompanyId.SelectedItem.Value.ToString());
                _Employees.EmployeeName = txt_EmployeeName.Text;
                try
                {
                    _Employees.LastVehcle = int.Parse(ddl_LastVehcle.SelectedItem.Value.ToString());
                }
                catch
                {
                    _Employees.LastVehcle = null;
                }
                if (ddl_SpecializationId.SelectedItem != null)
                {
                    _Employees.SpecializationId = int.Parse(ddl_SpecializationId.SelectedItem.Value.ToString());
                }
                else
                {
                    _Employees.SpecializationId = null;

                }
                _Employees.Salary = decimal.Parse(string.IsNullOrEmpty(txt_Salary.Text) ? "0" : txt_Salary.Text);
                _Employees.RatePerHour = decimal.Parse(string.IsNullOrEmpty(txt_RatePerHour.Text) ? "0" : txt_RatePerHour.Text);
                _Employees.DrivingLicenseId = txt_DrivingLicenseId.Text;
                try
                {
                    _Employees.LicenseType = int.Parse(dll_LicenseType.SelectedItem.Value.ToString());

                }
                catch
                {
                    _Employees.LicenseType = null;
                }
                DateTime temp;
                if (DateTime.TryParse(txt_DrivingLicenseExpiryDate.Text, out temp))
                {
                    _Employees.DrivingLicenseExpiryDate = DateTime.Parse(txt_DrivingLicenseExpiryDate.Text);
                }
                else
                {
                    _Employees.DrivingLicenseExpiryDate = null;
                }
                _Employees.AccomodationNo = txt_AccomodationNo.Text;
                _Employees.EmployeeAddress = txt_EmployeeAddress.Text;
                _Employees.Mobile = txt_Mobile.Text;
                _Employees.Tel = txt_Tel.Text;
                _Employees.Mail = txt_Mail.Text;
                _Employees.Notes = txt_Notes.Text;
                dc.Employees.InsertOnSubmit(_Employees);
                dc.SubmitChanges();
                result = _Employees.EmployeeId;

                #endregion



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
            BindEmployeeGrid();
            EmptyControls();




        }

        private void EmptyControls()
        {
            rd_IsDriver.Checked = true;
            txt_EmployeeName.Text = string.Empty;
            ddl_CompanyId.Value = null;
            ddl_CompanyId.Text = string.Empty;
            ddl_LastVehcle.Value = null;
            ddl_LastVehcle.Text = string.Empty;
            txt_Salary.Text = "";
            txt_RatePerHour.Text = "";
            txt_AccomodationNo.Text = "";
            dll_LicenseType.Value = null;
            dll_LicenseType.Text = string.Empty;
            txt_DrivingLicenseExpiryDate.Text = "";
            txt_AccomodationNo.Text = "";
            txt_EmployeeAddress.Text = "";
            txt_Mobile.Text = "";
            txt_Tel.Text = "";
            txt_Mail.Text = "";
            txt_Notes.Text = "";
            EmployeesID = 0;
        }

        private void BindEmployeeGrid()
        {
            if (ddl_CompanyId.SelectedItem != null)
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
                grd_Employees.DataSource = dc.EmployeesSelectWithJob(id, int.Parse(ddl_CompanyId.SelectedItem.Value.ToString()));
                grd_Employees.DataBind();
            }

        }

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;

            EmployeesID = Convert.ToInt32(grd_Employees.DataKeys[row.RowIndex].Value);

            var query = (from p in dc.Employees where p.EmployeeId.Equals(EmployeesID) select new { p.IsDriver, p.IsTecnician, p.EmployeeName, p.CompanyID, p.LastVehcle, p.Salary, p.RatePerHour, p.DrivingLicenseId, p.LicenseType, p.DrivingLicenseExpiryDate, p.AccomodationNo, p.EmployeeAddress, p.Mobile, p.Tel, p.Mail, p.Notes, p.SpecializationId }).SingleOrDefault();
            rd_IsDriver.Checked = bool.Parse(query.IsDriver.ToString());
            rd_IsTechnician.Checked = bool.Parse(query.IsTecnician.ToString());
            txt_EmployeeName.Text = query.EmployeeName;
            if (query.CompanyID != null)
            {
                ddl_CompanyId.Value = query.CompanyID.ToString();
            }
            else
            {

                ddl_CompanyId.Value = null;
                ddl_CompanyId.Text = string.Empty;
            }
            if (query.SpecializationId != null)
            {
                ddl_SpecializationId.Value = query.SpecializationId.ToString();
            }
            else
            {
                ddl_SpecializationId.Value = null;
                ddl_SpecializationId.Text = "";

            }
            ddl_LastVehcle.Value = query.LastVehcle.ToString();
            txt_Salary.Text = query.Salary.ToString();
            txt_RatePerHour.Text = query.RatePerHour.ToString();
            txt_DrivingLicenseId.Text = query.DrivingLicenseId;
            dll_LicenseType.Value = query.LicenseType.ToString();
            DateTime temp;
            if (DateTime.TryParse(query.DrivingLicenseExpiryDate.ToString(), out temp))
            {
                txt_DrivingLicenseExpiryDate.Text = DateTime.Parse(query.DrivingLicenseExpiryDate.ToString()).ToShortDateString();
            }
            else
            {
                txt_DrivingLicenseExpiryDate.Text = string.Empty;
            }
            txt_AccomodationNo.Text = query.AccomodationNo;
            txt_EmployeeAddress.Text = query.EmployeeAddress;
            txt_Mobile.Text = query.Mobile;
            txt_Tel.Text = query.Tel;
            txt_Mail.Text = query.Mail;
            txt_Notes.Text = query.Notes;



            //txt_BrandName.Text = query.BrandName;
            //txt_Notes.Text = query.Notes;
        }

        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            if (int.Parse(ViewState["AllowDelete"].ToString()) == 0 || ViewState["AllowDelete"] == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ليس لديك صلاحية الحذف";
                return;
            }
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;
            var query = from p in dc.Employees where p.EmployeeId.Equals(Convert.ToInt32(grd_Employees.DataKeys[row.RowIndex].Value)) select p;
            try
            {
                foreach (var item in query)
                {
                    dc.Employees.DeleteOnSubmit(item);
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
            BindEmployeeGrid();
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.Employees.GetHashCode());
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

        protected void ddl_CompanyId_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillVehcles();
            BindEmployeeGrid();
        }
    }
}
