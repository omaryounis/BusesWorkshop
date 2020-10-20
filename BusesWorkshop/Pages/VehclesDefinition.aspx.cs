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
using System.Data.SqlClient;
using System.Globalization;
using DevExpress.Web;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Pages.Definitions
{
    public partial class VehclesDefinition : System.Web.UI.Page
    {

        DataTable dt;
        private int VehcleID
        {
            get
            {
                if (ViewState["_VehcleID"] != null)
                {
                    return Convert.ToInt32(ViewState["_VehcleID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_VehcleID", value);
            }
        }
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

        WorkshopDataContext dc = new WorkshopDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "بيانات السيارات";
            permissions(dc);

            try
            {
                if (Session["UserID"] != null || !string.IsNullOrEmpty(Session["UserID"].ToString()))
                {
                    UserID = Convert.ToInt32(Session["UserID"]);
                }
                else
                {
                    Response.Redirect("../pages/login.aspx");
                }
            }
            catch
            {
                Response.Redirect("../pages/login.aspx");

            }


            if (!IsPostBack)
            {
                FillBrands();
                FillLicenseType();
                FillVehcleTypes();
                FillFuel();
                FillCC();
                //FillMainSuperVisors();
                FillManfacturingCountries();
                FillCylinderNo();

                //FillDrivers();
                fill_Companies();
                FillColors();
                FillGrdVehcles();
                FillInsuranceCompanies();

                #region Attachment data table
                dt = new DataTable();
                DataColumn dc1 = new DataColumn("AttachmentName");
                DataColumn dc2 = new DataColumn("Notes");
                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);

                //DataRow dr1 = dt.NewRow();
                //dr1[0] = "مرفق";
                //dr1[1] = "ملاحظات";
                //dt.Rows.Add(dr1);
                if (dt.Rows.Count == 0) dt.Rows.Add(dt.NewRow());
                ViewState["Attachments"] = dt;
                GrdAttAttment.DataSource = ViewState["Attachments"];
                GrdAttAttment.DataBind();
                GrdAttAttment.Rows[0].Visible = false;

                #endregion
            }


        }

        private void FillInsuranceCompanies()
        {
            ddl_InsuranceCompanyId.DataSource = (from p in dc.ConfigDetails where p.MasterId == 19 select new { p.ConfigDetailId, p.ConfigDetailName }).ToList();
            ddl_InsuranceCompanyId.TextField = "ConfigDetailName";
            ddl_InsuranceCompanyId.ValueField = "ConfigDetailId";
            ddl_InsuranceCompanyId.DataBind();
        }

        private void FillColors()
        {
            ddl_Color.DataSource = from p in dc.ConfigDetails where p.MasterId == 10 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_Color.TextField = "ConfigDetailName";
            ddl_Color.ValueField = "ConfigDetailId";
            ddl_Color.DataBind();
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
        private void FillDrivers()
        {
            ddl_MainDriver.DataSource = from p in dc.Employees where p.IsDriver.Equals(1) && p.CompanyID == int.Parse(ddl_CompanyId.SelectedItem.Value.ToString()) select new { p.EmployeeName, p.EmployeeId };
            ddl_MainDriver.TextField = "EmployeeName";
            ddl_MainDriver.ValueField = "EmployeeId";
            ddl_MainDriver.DataBind();
        }

        private void FillGrdVehcles()
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
                gvVehcles.DataSource = from p in dc.Vehcles where p.Company.UserCompanies.Any(x => x.UserID.Equals(id)) && p.CompanyId == int.Parse(ddl_CompanyId.SelectedItem.Value.ToString()) select new { p.VehcleId, p.PlateNo, p.Class.ClassName, p.Class.Model.Brand.BrandName, p.Class.Model.ModelName };
                gvVehcles.DataBind();

            }

            //Tamer Code
            //gvVehcles.DataSource = from p in dc.Vehcles
            //                       join T in dc.UserCompanies 
            //                       on p.CompanyId equals T.CompID  

            //                       where 
            //                       T.UserID == 1
            //                      // && p.CompanyId == (int.Parse(ddl_CompanyId.SelectedItem.Value.ToString()))                                       
            //                       select new { p.VehcleId, p.PlateNo, p.Class.ClassName, p.Class.Model.Brand.BrandName, p.Class.Model.ModelName };
            //gvVehcles.DataBind();
            //}
        }

        private void FillCylinderNo()
        {
            ddl_CylenderNo.DataSource = from p in dc.ConfigDetails where p.MasterId == 7 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_CylenderNo.TextField = "ConfigDetailName";
            ddl_CylenderNo.ValueField = "ConfigDetailId";
            ddl_CylenderNo.DataBind();
        }

        private void FillManfacturingCountries()
        {
            ddl_ManufacturingCountry.DataSource = from p in dc.ConfigDetails where p.MasterId == 9 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_ManufacturingCountry.TextField = "ConfigDetailName";
            ddl_ManufacturingCountry.ValueField = "ConfigDetailId";
            ddl_ManufacturingCountry.DataBind();
        }
        private void FillMainSuperVisors()
        {

            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BusWorkshopConnectionString2"].ToString());
            //if (con.State == ConnectionState.Open) con.Close();
            //con.Open();
            //SqlCommand cmd = new SqlCommand("SuperVisorsSelectByBranch ", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@BranchName", ddl_CompanyId.SelectedItem.Text);
            ////ddl_SuperVisorId.DataSource = cmd.ExecuteReader();
            //ddl_SuperVisorId.DataSource = cmd.ExecuteReader();
            ////ddl_SuperVisorId.DataTextField = "Name";
            ////ddl_SuperVisorId.DataValueField = "ID";
            //ddl_SuperVisorId.TextField = "Name";
            //ddl_SuperVisorId.ValueField = "ID";
            ////ddl_SuperVisorId.DataBind();
            //ddl_SuperVisorId.DataBind();
            ////ddl_SuperVisorId.Items.Insert(0, new ListItem("اختر المشرف", ""));

            //con.Close();
        }
        private void FillCC()
        {
            ddl_CC.DataSource = from p in dc.ConfigDetails where p.MasterId == 6 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_CC.TextField = "ConfigDetailName";
            ddl_CC.ValueField = "ConfigDetailId";
            ddl_CC.DataBind();
        }

        private void FillFuel()
        {
            ddl_FueL.DataSource = from p in dc.ConfigDetails where p.MasterId == 5 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_FueL.TextField = "ConfigDetailName";
            ddl_FueL.ValueField = "ConfigDetailId";
            ddl_FueL.DataBind();
        }

        private void FillVehcleTypes()
        {
            ddl_VehcleType.DataSource = from p in dc.ConfigDetails where p.MasterId == 4 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_VehcleType.TextField = "ConfigDetailName";
            ddl_VehcleType.ValueField = "ConfigDetailId";
            ddl_VehcleType.DataBind();
        }

        private void FillLicenseType()
        {
            ddl_LicenseType.DataSource = from p in dc.ConfigDetails where p.MasterId == 8 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_LicenseType.TextField = "ConfigDetailName";
            ddl_LicenseType.ValueField = "ConfigDetailId";
            ddl_LicenseType.DataBind();
        }
        private void FillBrands()
        {
            ddl_ModelId.Items.Clear();
            ddl_ClassId.Items.Clear();
            ddl_BrandId.DataSource = from p in dc.Brands select new { p.BrandId, p.BrandName };
            ddl_BrandId.TextField = "BrandName";
            ddl_BrandId.ValueField = "BrandId";
            ddl_BrandId.DataBind();
        }

        protected void ddl_BrandId_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddl_ModelId.Items.Clear();
            ddl_ModelId.Text = string.Empty;
            ddl_ClassId.Items.Clear();
            ddl_ClassId.Text = string.Empty;
            if (ddl_BrandId.SelectedItem != null)
            {


                ddl_ModelId.DataSource = from p in dc.Models where p.BrandId == int.Parse(ddl_BrandId.SelectedItem.Value.ToString()) select new { p.ModelId, p.ModelName };
                ddl_ModelId.TextField = "ModelName";
                ddl_ModelId.ValueField = "ModelId";
                ddl_ModelId.DataBind();
            }

        }

        protected void ddl_ModelId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_ClassId.Items.Clear();
            if (ddl_ModelId.SelectedItem != null)
            {


                ddl_ClassId.DataSource = from p in dc.Classes where p.ModelId == int.Parse(ddl_ModelId.SelectedItem.Value.ToString()) select new { p.ClassId, p.ClassName };
                ddl_ClassId.TextField = "ClassName";
                ddl_ClassId.ValueField = "ClassId";
                ddl_ClassId.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;

            #region Validation
            try
            {
                if (ddl_ClassId.SelectedItem.Value == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "ادخل الطراز";
                    return;
                }

            }
            catch
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "ادخل الطراز";
                return;

            }
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


            #endregion
            if (VehcleID > 0)
            {

                #region make sure that license new data after old date
                var queryL = (from p in dc.Vehcles where p.VehcleId == VehcleID select new { p.LicenseExpiryDate, p.InsuranceExpiryDate }).SingleOrDefault();
                if (txt_LicenseExpiryDate.Text != string.Empty)
                {

                    if (queryL.LicenseExpiryDate > DateTime.Parse(txt_LicenseExpiryDate.Text))
                    {
                        divMsg.Attributes["class"] = "alert alert-danger text-right";
                        lblResult.Text = "عفوا لا يمكن ادخال تاريخ للاستمارة سايق للتاريخ الحالى";
                        return;
                    }
                }
                if (txt_InsuranceExpiryDate.Text != string.Empty)
                {

                    if (queryL.InsuranceExpiryDate > DateTime.Parse(txt_InsuranceExpiryDate.Text))
                    {
                        divMsg.Attributes["class"] = "alert alert-danger text-right";
                        lblResult.Text = "عفوا لا يمكن ادخال تاريخ للتأمين سايق للتاريخ الحالى";
                        return;
                    }
                }
                #endregion
                #region update
                if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية التعديل";
                    return;
                }
                Vehcle _Vehcle = dc.Vehcles.Single(vehcle => vehcle.VehcleId == VehcleID);
                _Vehcle.PlateNo = txt_PlateNo.Text;
                if (ddl_CompanyId.SelectedItem != null)
                {
                    _Vehcle.CompanyId = int.Parse(ddl_CompanyId.SelectedItem.Value.ToString());
                }
                else
                {
                    return;
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "يجب اختيار الشركة";
                }

                _Vehcle.ClassId = int.Parse(ddl_ClassId.SelectedItem.Value.ToString());

                try
                {
                    _Vehcle.LicenseType = int.Parse(ddl_LicenseType.SelectedItem.Value.ToString());
                }
                catch
                {
                    _Vehcle.LicenseType = null;
                }

                _Vehcle.LicenseNo = txt_LicenseNo.Text;
                try
                {
                    _Vehcle.LicenseExpiryDate = DateTime.Parse(txt_LicenseExpiryDate.Text);

                }
                catch { _Vehcle.LicenseExpiryDate = null; }
                try
                {
                    _Vehcle.VehcleType = int.Parse(ddl_VehcleType.SelectedItem.Value.ToString());
                }
                catch
                {
                    _Vehcle.VehcleType = null;
                }
                try
                {
                    _Vehcle.FueL = int.Parse(ddl_FueL.SelectedItem.Value.ToString());
                }
                catch
                {
                    _Vehcle.FueL = null;
                }

                _Vehcle.BodyNo = txt_BodyNo.Text;
                _Vehcle.MotorNo = txt_MotorNo.Text;
                try
                {
                    _Vehcle.CC = int.Parse(ddl_CC.SelectedItem.Value.ToString());
                }
                catch
                {
                    _Vehcle.CC = null;
                }

                try
                {
                    _Vehcle.MainDriver = int.Parse(ddl_MainDriver.SelectedItem.Value.ToString());
                }
                catch
                {
                    _Vehcle.MainDriver = null;
                }
                try
                {
                    _Vehcle.SuperVisorId = ddl_SuperVisorId.Text;
                }
                catch
                {
                    _Vehcle.SuperVisorId = null;

                }

                _Vehcle.CounterReadingStart = int.Parse(string.IsNullOrEmpty(txt_CounterReadingStart.Text) ? "0" : txt_CounterReadingStart.Text);
                //_Vehcle.AverageFuelConsumption = decimal.Parse(txt_AverageFuelConsumption.Text);
                _Vehcle.AverageFuelConsumption = Convert.ToDecimal(string.IsNullOrEmpty(txt_AverageFuelConsumption.Text) ? "0" : txt_AverageFuelConsumption.Text);
                try
                {
                    _Vehcle.Color = int.Parse(ddl_Color.SelectedItem.Value.ToString());
                }
                catch
                {
                    _Vehcle.Color = null;

                }
                try
                {
                    _Vehcle.CylenderNo = int.Parse(ddl_CylenderNo.SelectedItem.Value.ToString());
                }
                catch
                {
                    _Vehcle.CylenderNo = null;
                }
                if (txt_ManufactureYear.Text != string.Empty)
                {
                    _Vehcle.ManufactureYear = int.Parse(txt_ManufactureYear.Text);
                }
                else
                {
                    _Vehcle.ManufactureYear = null;

                }
                try
                {
                    _Vehcle.ManufacturingCountry = int.Parse(ddl_ManufacturingCountry.SelectedItem.Value.ToString());

                }
                catch
                {
                    _Vehcle.ManufacturingCountry = null;
                }
                try
                {
                    string licDate = txt_StartOperationDate.Text;
                    _Vehcle.StartOperationDate = Convert.ToDateTime(licDate);

                }
                catch { _Vehcle.StartOperationDate = null; }

                //_Vehcle.StartOperationDate = DateTime.Parse(txt_StartOperationDate.Text);
                try
                {
                    string inspectiondate = txt_InspectioDate.Text;
                    _Vehcle.InspectioDate = Convert.ToDateTime(inspectiondate);
                }
                catch { _Vehcle.InspectioDate = null; }
                if (ddl_InsuranceCompanyId.SelectedItem != null)
                {

                    _Vehcle.InsuranceCompanyId = int.Parse(ddl_InsuranceCompanyId.SelectedItem.Value.ToString());
                }
                else
                {
                    _Vehcle.InsuranceCompanyId = null;
                }
                try
                {
                    _Vehcle.InsuranceExpiryDate = DateTime.Parse(txt_InsuranceExpiryDate.Text);
                }
                catch
                {
                    _Vehcle.InsuranceExpiryDate = null;
                }

                //_Vehcle.InspectioDate = DateTime.Parse(txt_InspectioDate.Text);

                try
                {


                    string endoperationdate = txt_EndOperationDate.Text;
                    _Vehcle.EndOperationDate = DateTime.Parse(endoperationdate);
                }
                catch { _Vehcle.EndOperationDate = null; }


                //_Vehcle.EndOperationDate = DateTime.Parse(txt_EndOperationDate.Text);
                _Vehcle.Notes = txt_Notes.Text;
                _Vehcle.Active = chk_IsActive.Checked;
                try
                {
                    dc.SubmitChanges();
                }
                catch (SqlException ex)
                {

                    if (ex.Errors[0].Number == 2601)
                    {
                        divMsg.Attributes["class"] = "alert alert-danger text-right";
                        lblResult.Text = "عفو السجل المراد حذفة مرتبط بمدخلات اخري";
                        return;
                    }


                }
                result = _Vehcle.VehcleId;


                #region delete Document Alarm from alarms
                if (queryL.LicenseExpiryDate < DateTime.Parse(txt_LicenseExpiryDate.Text))
                {
                    var queryAl = from p in dc.Alarms where p.Name == txt_PlateNo.Text && p.Type == "Document" && p.counter == null && p.PlanId == null && p.Date == queryL.LicenseExpiryDate select p;
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
                            lblResult.Text = "عفو رقم اللوحة لايمكن تكرارة";
                            return;
                        }


                    }
                }
                if (queryL.InsuranceExpiryDate < DateTime.Parse(txt_LicenseExpiryDate.Text))
                {
                    var queryAlIns = from p in dc.Alarms where p.Name == txt_PlateNo.Text && p.Type == "Document" && p.counter == null && p.PlanId == null && p.Date == queryL.LicenseExpiryDate select p;
                }
                #endregion

                #endregion
                #region Remove All Attachments
                var query = from c in dc.BusAttachments where c.VehcleId.Equals(result) select c;
                foreach (var deletedAttachment in query)
                {
                    dc.BusAttachments.DeleteOnSubmit(deletedAttachment);
                }
                dc.SubmitChanges();
                #endregion
                #region Insert Attachments

                for (int i = 0; i < GrdAttAttment.Rows.Count; i++)
                {
                    ASPxComboBox ddl_AttachmnentName = (ASPxComboBox)GrdAttAttment.Rows[i].FindControl("ddl_AttachmnentName");
                    Label lbl_Notes = (Label)GrdAttAttment.Rows[i].FindControl("lbl_Notes");

                    if (ddl_AttachmnentName.Text == "" && lbl_Notes.Text == "") goto fo;
                    using (WorkshopDataContext ctx = new WorkshopDataContext())
                    {
                        BusAttachment _BusAttachment = new BusAttachment();
                        _BusAttachment.VehcleId = result;
                        _BusAttachment.AttachmentName = int.Parse(ddl_AttachmnentName.SelectedItem.Value.ToString());
                        _BusAttachment.Notes = lbl_Notes.Text;
                        ctx.BusAttachments.InsertOnSubmit(_BusAttachment);
                        ctx.SubmitChanges(); ;
                    }
                    fo:;
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


                #region StoppedCar


                #endregion
                Vehcle _Vehcle = new Vehcle();
                _Vehcle.PlateNo = txt_PlateNo.Text;
                if (ddl_CompanyId.SelectedItem != null)
                {
                    _Vehcle.CompanyId = int.Parse(ddl_CompanyId.SelectedItem.Value.ToString());
                }
                else
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "يجب اختيار الشركة";
                    return;

                }

                _Vehcle.ClassId = int.Parse(ddl_ClassId.SelectedItem.Value.ToString());

                try
                {
                    _Vehcle.LicenseType = int.Parse(ddl_LicenseType.SelectedItem.Value.ToString());
                }
                catch
                {
                    _Vehcle.LicenseType = null;
                }

                _Vehcle.LicenseNo = txt_LicenseNo.Text;
                try
                {
                    _Vehcle.LicenseExpiryDate = DateTime.Parse(txt_LicenseExpiryDate.Text);

                }
                catch { _Vehcle.LicenseExpiryDate = null; }
                try
                {
                    _Vehcle.VehcleType = int.Parse(ddl_VehcleType.SelectedItem.Value.ToString());
                }
                catch
                {
                    _Vehcle.VehcleType = null;
                }
                try
                {
                    _Vehcle.FueL = int.Parse(ddl_FueL.SelectedItem.Value.ToString());
                }
                catch
                {
                    _Vehcle.FueL = null;
                }

                _Vehcle.BodyNo = txt_BodyNo.Text;
                _Vehcle.MotorNo = txt_MotorNo.Text;
                try
                {
                    _Vehcle.CC = int.Parse(ddl_CC.SelectedItem.Value.ToString());
                }
                catch
                {
                    _Vehcle.CC = null;
                }

                try
                {
                    _Vehcle.MainDriver = int.Parse(ddl_MainDriver.SelectedItem.Value.ToString());
                }
                catch
                {
                    _Vehcle.MainDriver = null;
                }
                try
                {
                    _Vehcle.SuperVisorId = (ddl_SuperVisorId.Text);
                }
                catch
                {
                    _Vehcle.SuperVisorId = null;

                }

                _Vehcle.CounterReadingStart = int.Parse(string.IsNullOrEmpty(txt_CounterReadingStart.Text) ? "0" : txt_CounterReadingStart.Text);
                //_Vehcle.AverageFuelConsumption = decimal.Parse(txt_AverageFuelConsumption.Text);
                _Vehcle.AverageFuelConsumption = Convert.ToDecimal(string.IsNullOrEmpty(txt_AverageFuelConsumption.Text) ? "0" : txt_AverageFuelConsumption.Text);
                try
                {
                    _Vehcle.Color = int.Parse(ddl_Color.SelectedItem.Value.ToString());
                }
                catch
                {
                    _Vehcle.Color = null;

                }
                try
                {
                    _Vehcle.CylenderNo = int.Parse(ddl_CylenderNo.SelectedItem.Value.ToString());
                }
                catch
                {
                    _Vehcle.CylenderNo = null;
                }
                if (txt_ManufactureYear.Text != string.Empty)
                {
                    _Vehcle.ManufactureYear = int.Parse(txt_ManufactureYear.Text);
                }
                else
                {
                    _Vehcle.ManufactureYear = null;

                }
                try
                {
                    _Vehcle.ManufacturingCountry = int.Parse(ddl_ManufacturingCountry.SelectedItem.Value.ToString());

                }
                catch
                {
                    _Vehcle.ManufacturingCountry = null;
                }
                try
                {
                    string licDate = txt_StartOperationDate.Text;
                    _Vehcle.StartOperationDate = Convert.ToDateTime(licDate);

                }
                catch { _Vehcle.StartOperationDate = null; }

                //_Vehcle.StartOperationDate = DateTime.Parse(txt_StartOperationDate.Text);
                try
                {
                    string inspectiondate = txt_InspectioDate.Text;
                    _Vehcle.InspectioDate = Convert.ToDateTime(inspectiondate);
                }
                catch { _Vehcle.InspectioDate = null; }

                //_Vehcle.InspectioDate = DateTime.Parse(txt_InspectioDate.Text);
                if (ddl_InsuranceCompanyId.SelectedItem != null)
                {

                    _Vehcle.InsuranceCompanyId = int.Parse(ddl_InsuranceCompanyId.SelectedItem.Value.ToString());
                }
                else
                {
                    _Vehcle.InsuranceCompanyId = null;
                }
                try
                {
                    _Vehcle.InsuranceExpiryDate = DateTime.Parse(txt_InsuranceExpiryDate.Text);
                }
                catch
                {
                    _Vehcle.InsuranceExpiryDate = null;
                }


                try
                {


                    string endoperationdate = txt_EndOperationDate.Text;
                    _Vehcle.EndOperationDate = DateTime.Parse(endoperationdate);
                }
                catch { _Vehcle.EndOperationDate = null; }


                //_Vehcle.EndOperationDate = DateTime.Parse(txt_EndOperationDate.Text);
                _Vehcle.Notes = txt_Notes.Text;
                _Vehcle.Active = true;
                dc.Vehcles.InsertOnSubmit(_Vehcle);

                try
                {
                    dc.SubmitChanges();
                }
                catch (SqlException ex)
                {

                    if (ex.Errors[0].Number == 2601)
                    {
                        divMsg.Attributes["class"] = "alert alert-danger text-right";
                        lblResult.Text = "عفو رقم اللوحة لايمكن تكرارة";
                        return;
                    }


                }

                result = _Vehcle.VehcleId;
                #endregion
                #region Insert Attachments

                for (int i = 0; i < GrdAttAttment.Rows.Count; i++)
                {
                    ASPxComboBox ddl_AttachmnentName = (ASPxComboBox)GrdAttAttment.Rows[i].FindControl("ddl_AttachmnentName");
                    Label lbl_Notes = (Label)GrdAttAttment.Rows[i].FindControl("lbl_Notes");

                    if (ddl_AttachmnentName.Text == "" && lbl_Notes.Text == "") goto fo;
                    using (WorkshopDataContext ctx = new WorkshopDataContext())
                    {
                        BusAttachment _BusAttachment = new BusAttachment();
                        _BusAttachment.VehcleId = result;
                        _BusAttachment.AttachmentName = int.Parse(ddl_AttachmnentName.SelectedItem.Value.ToString());
                        _BusAttachment.Notes = lbl_Notes.Text;
                        ctx.BusAttachments.InsertOnSubmit(_BusAttachment);
                        ctx.SubmitChanges(); ;
                    }
                    fo:;
                }


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
            }
            FillGrdVehcles();
            EmptyControls();

        }

        private void EmptyControls()
        {
            txt_PlateNo.Text = string.Empty;
            ddl_BrandId.Value = null;
            ddl_BrandId.Text = string.Empty; ;
            ddl_ModelId.Value = null;
            ddl_ModelId.Text = string.Empty;
            ddl_ClassId.Value = null;
            ddl_ClassId.Text = string.Empty;
            ddl_LicenseType.Value = null;
            ddl_LicenseType.Text = string.Empty;
            txt_LicenseNo.Text = string.Empty;
            ddl_VehcleType.Value = null;
            ddl_VehcleType.Text = string.Empty;
            ddl_FueL.Value = null;
            ddl_FueL.Text = string.Empty;
            txt_BodyNo.Text = string.Empty;
            txt_MotorNo.Text = string.Empty;
            ddl_CC.Text = string.Empty;
            ddl_CC.Value = null;
            ddl_MainDriver.Text = string.Empty;
            ddl_MainDriver.Value = null;
            txt_CounterReadingStart.Text = string.Empty;
            txt_AverageFuelConsumption.Text = string.Empty;
            ddl_Color.Text = string.Empty;
            ddl_Color.Value = null;
            ddl_CylenderNo.Text = string.Empty;
            ddl_CylenderNo.Value = null;
            ddl_ManufacturingCountry.Value = null;
            txt_ManufactureYear.Text = string.Empty;
            txt_StartOperationDate.Text = string.Empty;
            txt_EndOperationDate.Text = string.Empty;
            txt_Notes.Text = string.Empty;
            ddl_SuperVisorId.Text = string.Empty;

            ddl_CompanyId.Text = string.Empty;
            ddl_CompanyId.Value = null;
            txt_InspectioDate.Text = string.Empty;
            txt_LicenseExpiryDate.Text = string.Empty;
            dt = (DataTable)ViewState["Attachments"];
            dt.Rows.Clear();
            dt.Rows.Add(dt.NewRow());
            GrdAttAttment.DataSource = dt;
            GrdAttAttment.DataBind();
            GrdAttAttment.Rows[0].Visible = false;
            ViewState["Attachments"] = dt;
            VehcleID = 0;
            chk_IsActive.Checked = false;
            MyClasses.ClearControls(Page);
        }

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            DateTime temp;
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;

            VehcleID = Convert.ToInt32(gvVehcles.DataKeys[row.RowIndex].Value);

            var query = (from p in dc.Vehcles where p.VehcleId.Equals(VehcleID) select new { p.VehcleId, p.PlateNo, p.Class.Model.Brand.BrandId, p.ClassId, p.LicenseType, p.FueL, p.BodyNo, p.MotorNo, p.CC, p.MainDriver, p.CounterReadingStart, p.AverageFuelConsumption, p.Color, p.CylenderNo, p.ManufactureYear, p.ManufacturingCountry, p.Class.Model.ModelId, p.VehcleType, p.StartOperationDate, p.LicenseNo, p.SuperVisorId, p.EndOperationDate, p.CompanyId, p.Notes, p.InspectioDate, p.LicenseExpiryDate, p.Active, p.InsuranceCompanyId, p.InsuranceExpiryDate }).SingleOrDefault();


            txt_PlateNo.Text = query.PlateNo;
            ddl_CompanyId.Value = query.CompanyId.ToString();
            ddl_BrandId.Value = query.BrandId.ToString();
            #region Fill brands
            ddl_ModelId.DataSource = from p in dc.Models where p.BrandId == int.Parse(ddl_BrandId.Value.ToString()) select new { p.ModelId, p.ModelName };
            ddl_ModelId.TextField = "ModelName";
            ddl_ModelId.ValueField = "ModelId";
            ddl_ModelId.DataBind();
            #endregion

            ddl_ModelId.Value = query.ModelId.ToString();
            #region Fill brands
            ddl_ClassId.DataSource = from p in dc.Classes where p.ModelId == int.Parse(ddl_ModelId.Value.ToString()) select new { p.ClassName, p.ClassId };
            ddl_ClassId.TextField = "ClassName";
            ddl_ClassId.ValueField = "ClassId";
            ddl_ClassId.DataBind();

            #endregion
            ddl_ClassId.Value = query.ClassId.ToString();
            ddl_LicenseType.Value = query.LicenseType.ToString();
            txt_LicenseNo.Text = query.LicenseNo;
            ddl_VehcleType.Value = query.VehcleType.ToString();
            ddl_FueL.Value = query.FueL.ToString();
            txt_BodyNo.Text = query.BodyNo;
            txt_MotorNo.Text = query.MotorNo;
            ddl_CC.Value = query.CC.ToString();
            ddl_MainDriver.Value = query.MainDriver.ToString();
            ddl_SuperVisorId.Text = query.SuperVisorId;
            txt_CounterReadingStart.Text = query.CounterReadingStart.ToString();
            txt_AverageFuelConsumption.Text = query.AverageFuelConsumption.ToString();
            ddl_Color.Text = query.Color.ToString();
            ddl_CylenderNo.Value = query.CylenderNo.ToString();
            txt_ManufactureYear.Text = query.ManufactureYear.ToString();
            ddl_ManufacturingCountry.Value = query.ManufacturingCountry.ToString();
            if (DateTime.TryParse(query.StartOperationDate.ToString(), out temp))
            {
                txt_StartOperationDate.Text = DateTime.Parse(query.StartOperationDate.ToString()).ToShortDateString();
            }
            else
            {
                txt_EndOperationDate.Text = string.Empty;
            }



            if (DateTime.TryParse(query.EndOperationDate.ToString(), out temp))
            {
                txt_EndOperationDate.Text = DateTime.Parse(query.EndOperationDate.ToString()).ToShortDateString();
            }
            else
            {
                txt_EndOperationDate.Text = string.Empty;
            }
            //
            txt_Notes.Text = query.Notes;
            if (DateTime.TryParse(query.LicenseExpiryDate.ToString(), out temp))
            {
                txt_LicenseExpiryDate.Text = DateTime.Parse(query.LicenseExpiryDate.ToString()).ToShortDateString();
            }
            else
            {
                txt_LicenseExpiryDate.Text = string.Empty;
            }
            if (DateTime.TryParse(query.InspectioDate.ToString(), out temp))
            {
                txt_InspectioDate.Text = DateTime.Parse(query.InspectioDate.ToString()).ToShortDateString();
            }
            else
            {
                txt_InspectioDate.Text = string.Empty;
            }

            ddl_InsuranceCompanyId.Value = query.InsuranceCompanyId.ToString();
            if (DateTime.TryParse(query.InsuranceExpiryDate.ToString(), out temp))
            {
                txt_InsuranceExpiryDate.Text = DateTime.Parse(query.InsuranceExpiryDate.ToString()).ToShortDateString();
            }
            else
            {
                txt_InsuranceExpiryDate.Text = string.Empty;
            }
            chk_IsActive.Checked = bool.Parse(query.Active.ToString());




            #region fill grd_attachment with data

            var queryAtt = from p in dc.BusAttachments where p.VehcleId.Equals(VehcleID) select new { p.AttachmentName, p.Notes };
            dt = queryAtt.CopyToDataTable();
            if (dt.Rows.Count > 0)
            {
                ViewState["Attachments"] = dt;
                GrdAttAttment.DataSource = ViewState["Attachments"];
                GrdAttAttment.DataBind();

            }
            else
            {
                dt = (DataTable)ViewState["Attachments"];
                dt.Rows.Clear();
                dt.Rows.Add(dt.NewRow());
                GrdAttAttment.DataSource = dt;
                GrdAttAttment.DataBind();
                GrdAttAttment.Rows[0].Visible = false;
                ViewState["Attachments"] = dt;
            }



            #endregion
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
            var query = from p in dc.Vehcles where p.VehcleId.Equals(Convert.ToInt32(gvVehcles.DataKeys[row.RowIndex].Value)) select p;
            try
            {
                foreach (var item in query)
                {
                    dc.Vehcles.DeleteOnSubmit(item);
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
            FillGrdVehcles();
        }

        protected void GrdAttAttment_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                ASPxComboBox ddl_AttachmnentName = (e.Row.FindControl("ddl_AttachmnentName") as ASPxComboBox);
                ddl_AttachmnentName.DataSource = from p in dc.ConfigDetails where p.MasterId == 11 select new { p.ConfigDetailId, p.ConfigDetailName };
                ddl_AttachmnentName.TextField = "ConfigDetailName";
                ddl_AttachmnentName.ValueField = "ConfigDetailId";
                ddl_AttachmnentName.DataBind();

                DataRowView dr = e.Row.DataItem as DataRowView;
                ddl_AttachmnentName.Value = dr["AttachmentName"].ToString();


            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[0].Text;
                foreach (Button button in e.Row.Cells[2].Controls.OfType<Button>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('هل تريد الحذف " + item + "?')){ return false; };";
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                // Find the product drop-down list, you can id (or cell number)
                var ddl_AttachmnentName = e.Row.FindControl("ddl_AttachmnentName") as ASPxComboBox;
                // var ddlProducts = e.Row.Cells[0].Controls[0]; // Finding by cell number and index
                if (null != ddl_AttachmnentName)
                {
                    ddl_AttachmnentName.DataSource = from p in dc.ConfigDetails where p.MasterId == 11 select new { p.ConfigDetailId, p.ConfigDetailName };
                    ddl_AttachmnentName.TextField = "ConfigDetailName";
                    ddl_AttachmnentName.ValueField = "ConfigDetailId";
                    ddl_AttachmnentName.DataBind();
                }
            }
        }

        protected void GrdAttAttment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = ViewState["Attachments"] as DataTable;
            dt.Rows[index].Delete();
            ViewState["Attachments"] = dt;
            GrdAttAttment.DataSource = ViewState["Attachments"];
            GrdAttAttment.DataBind();
            //GrdAttAttment.Rows[0].Visible = false;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ASPxComboBox ddl_AttachmnentName = (ASPxComboBox)GrdAttAttment.FooterRow.FindControl("ddl_AttachmnentName");
            TextBox txt_Notes = GrdAttAttment.FooterRow.FindControl("txt_Notes") as TextBox;
            dt = (DataTable)ViewState["Attachments"];
            DataRow dr1 = dt.NewRow();
            dr1[0] = ddl_AttachmnentName.SelectedItem.Value;
            dr1[1] = txt_Notes.Text;
            dt.Rows.Add(dr1);

            GrdAttAttment.DataSource = dt;
            GrdAttAttment.DataBind();
            ViewState["Attachments"] = dt;
            ASPxComboBox ddl_AttachmnentNameItem = (ASPxComboBox)GrdAttAttment.Rows[0].FindControl("ddl_AttachmnentName");
            if (ddl_AttachmnentNameItem.SelectedItem == null)
            {
                GrdAttAttment.Rows[0].Visible = false;
            }

        }

        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.VehclesDefinition.GetHashCode());
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
            if (ddl_CompanyId.SelectedItem != null)
            {
                FillDrivers();
                FillGrdVehcles();
            }
        }



    }
}
