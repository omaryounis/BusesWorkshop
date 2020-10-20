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
using BusesWorkshop.DAL;
using BusesWorkshop.DAL.Bus;
using DevExpress.Web;
namespace BusesWorkshop.Pages
{
    public partial class BusCheck : System.Web.UI.Page
    {
        #region "Properties"
        DataTable dt;
        WorkshopDataContext dc = new WorkshopDataContext();
        private int BusServiceCheckID
        {
            get
            {
                if (ViewState["_BusServiceCheckID"] != null)
                {
                    return Convert.ToInt32(ViewState["_BusServiceCheckID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_BusServiceCheckID", value);
            }
        }
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
        void fill_Services()
        {
            WorkshopDataContext dc = new WorkshopDataContext();

            DataTable dt = new DataTable();
            var query = from srv in dc.GetTable<ServicesSetting>() select srv;
            dt = query.CopyToDataTable();
            if (dt.Rows.Count > 0)
            {
                ddlServices.DataSource = dt;
                ddlServices.DataTextField = "ServiceName";
                ddlServices.DataValueField = "ID";
                ddlServices.DataBind();
            }
            else
            {
                ddlServices.DataSource = null;
                ddlServices.DataBind();
            }
        }
        void fill_Buses()
        {
            WorkshopDataContext dc = new WorkshopDataContext();

            DataTable dt = new DataTable();
            var query = from bus in dc.GetTable<Buse>()
                        join
                     drv in dc.GetTable<Driver>() on bus.DriverID equals drv.ID
                        select new { bus.ID, bus.PlateNumber, drv.DriverName, fullName = string.Format("حافلة رقم {0} - السائق - {1}", bus.PlateNumber, drv.DriverName) };




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
        string Check_Service(int prmServiceID, int prmBusID, int? prmKMcount, DateTime prmCurrentCheckDate)
        {
            int result = 0;
            string msg = string.Empty;

            ClsBusServicesCheck cls = new ClsBusServicesCheck();
            result = cls.Check_Service(prmServiceID, prmBusID, prmKMcount, prmCurrentCheckDate);
            string txt = " , هل تريد الاستمرار؟";
            if (result > 0)
            {
                msg = "OK";
            }
            else if (result == -1)
            {
                msg = "التاريخ الحالي أقل من أخر تاريخ آخر فحص" + txt;

            }
            else if (result == -2)
            {
                msg = "عدد الشهور المنقضية أقل من الافتراضي" + txt;

            }
            else if (result == -3)
            {
                msg = "عدد الكيلومترات الحالة أقل من المسجلة افتراضيا" + txt;

            }
            else if (result == -4)
            {
                msg = "الفرق بين الكيلومترات الحالية و آخر فحص أقل من الافتراضي" + txt;

            }
            else if (result == -5)
            {
                msg = "عدد الكيلومترات المدخلة أقل من الحد الأدنى الافتراضي" + txt;
            }
            else if (result == -6)
            {
                msg = "عدد الكيلو مترات المدخلة أكبر من الحد الأقصى الافتراضي" + txt;
            }
            else if (result == -7)
            {
                msg = "عدد الكيلو مترات المدخلة أقل من الافتراضي" + txt;
            }
            return msg;

            #region Old Code
            //int res = 0;
            //int getResult = 0;
            //ClsBusServicesCheck cls = new ClsBusServicesCheck();
            //DataTable dt = new DataTable();

            //dt = cls.Get_lastCheck(prmServiceID,  prmBusID);
            //if (dt!=null)
            //{


            //    if (dt.Rows.Count > 0)
            //    {
            //        getResult = cls.Check_Service(prmServiceID, prmBusID, prmKMcount, prmCurrentCheckDate);
            //        if (getResult < 0)
            //        {
            //            res = getResult;

            //        }
            //        else
            //        {
            //            res = 1; 
            //        }
            //    }

            //}
            //else
            //{

            //    res = 1;
            //}




            //return res;

            #endregion

        }
        void doInsert()
        {

            #region Insert , Update Code

            int result = 0;
            if (string.IsNullOrEmpty(txtKMcount.Text.Trim()))
            {
                txtKMcount.Text = "0";
            }

            if (BusServiceCheckID > 0)
            {
                // update
            }
            else
            {
                // insert
                WorkshopDataContext dc = new WorkshopDataContext();
                BusesService busService = new BusesService();

                busService.BusID = Convert.ToInt32(ddl_Vehcles.SelectedValue);
                busService.ServiceID = Convert.ToInt32(ddlServices.SelectedValue);


                busService.CheckDate = Convert.ToDateTime(txtCheckDate.Text);

                if (!string.IsNullOrEmpty(txtKMcount.Text.Trim()))
                {
                    busService.CheckKiloM = Convert.ToInt32(txtKMcount.Text);

                }

                busService.Notes = txtNotes.Text;

                busService.UserID = UserID;

                busService.Defects = txt_Defects.Text;
                busService.Prodedures = txt_Prodedures.Text;
                dc.BusesServices.InsertOnSubmit(busService);
                dc.SubmitChanges();
                result = busService.ID;
                #region Insert Attachments

                for (int i = 0; i < grd_SparParts.Rows.Count; i++)
                {
                    ASPxComboBox ddl_SpareId = (ASPxComboBox)grd_SparParts.Rows[i].FindControl("ddl_SpareId");
                    TextBox txt_SparCost = (TextBox)grd_SparParts.Rows[i].FindControl("txt_SparCost");
                    TextBox txt_SparCount = (TextBox)grd_SparParts.Rows[i].FindControl("txt_SparCount");
                    TextBox txt_SparTotal = (TextBox)grd_SparParts.Rows[i].FindControl("txt_SparTotal");
                    TextBox txt_LaborCost = (TextBox)grd_SparParts.Rows[i].FindControl("txt_LaborCost");
                    TextBox txt_TotalCost = (TextBox)grd_SparParts.Rows[i].FindControl("txt_TotalCost");
                    TextBox txt_Notes = (TextBox)grd_SparParts.Rows[i].FindControl("txt_Notes");
                    ASPxComboBox ddl_EmployeeId = (ASPxComboBox)grd_SparParts.Rows[i].FindControl("ddl_EmployeeId");


                    if (txt_SparCost.Text == "" && txt_TotalCost.Text == "") goto fo;
                    using (WorkshopDataContext ctx = new WorkshopDataContext())
                    {
                        BusesServicesSpar _BusesServicesSpar = new BusesServicesSpar();
                        _BusesServicesSpar.BusesServicesId = result;
                        _BusesServicesSpar.SpareId = int.Parse(ddl_SpareId.Value.ToString());
                        _BusesServicesSpar.SparCost = decimal.Parse(string.IsNullOrEmpty(txt_SparCost.Text) ? "0" : txt_SparCost.Text);
                        _BusesServicesSpar.SparCount = decimal.Parse(string.IsNullOrEmpty(txt_SparCount.Text) ? "0" : txt_SparCount.Text);
                        _BusesServicesSpar.SparTotal = decimal.Parse(string.IsNullOrEmpty(txt_SparTotal.Text) ? "0" : txt_SparTotal.Text);
                        _BusesServicesSpar.LaborCost = decimal.Parse(string.IsNullOrEmpty(txt_LaborCost.Text) ? "0" : txt_LaborCost.Text);
                        _BusesServicesSpar.TotalCost = decimal.Parse(string.IsNullOrEmpty(txt_LaborCost.Text) ? "0" : txt_TotalCost.Text);
                        _BusesServicesSpar.Notes = txt_Notes.Text;
                        _BusesServicesSpar.EmployeeId = int.Parse(ddl_EmployeeId.Value.ToString());
                        ctx.BusesServicesSpars.InsertOnSubmit(_BusesServicesSpar);
                        ctx.SubmitChanges(); ;
                    }
                    fo:;
                }


                #endregion
            }

            if (result > 0)
            {
                divMsg2.Attributes["class"] = "alert alert-success text-right";
                lblResult.Text = "تم الحفظ بنجاح";

                txtNotes.Text = txtKMcount.Text = txtCheckDate.Text = string.Empty;

            }
            else
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "حدث خطأ أثنا الحفظ";

            }


            txtCheckDate.Text = string.Empty;
            txtKMcount.Text = string.Empty;
            txt_Defects.Text = string.Empty;
            txt_Prodedures.Text = string.Empty;
            txtNotes.Text = string.Empty;
            txt_SparSum.Text = string.Empty;
            txt_LaborSum.Text = string.Empty;
            txt_TotalSum.Text = string.Empty;
            dt = (DataTable)ViewState["Spares"];
            dt.Rows.Clear();
            dt.Rows.Add(dt.NewRow());
            grd_SparParts.DataSource = dt;
            grd_SparParts.DataBind();
            grd_SparParts.Rows[0].Visible = false;
            ViewState["Spares"] = dt;
            #endregion

        }
        private void FillGridSparPart()
        {
            ASPxComboBox ddl_SpareMainId = grd_SparParts.FooterRow.FindControl("ddl_SpareMainId") as ASPxComboBox;
            ASPxComboBox ddl_SpareId = grd_SparParts.FooterRow.FindControl("ddl_SpareIdAdd") as ASPxComboBox;
            ClearGridTextBoxes();

            try
            {
                if (ddl_SpareMainId.SelectedItem.Value != null)
                {
                    var classId = (from p in dc.Vehcles where p.VehcleId == int.Parse(ddl_Vehcles.SelectedItem.Value.ToString()) select new { p.Color, p.ClassId }).SingleOrDefault();
                    ddl_SpareId.Items.Clear();
                    ddl_SpareId.Text = string.Empty;
                    ddl_SpareId.DataSource = from p in dc.SpareParts
                                             where p.MainCategory == int.Parse(ddl_SpareMainId.SelectedItem.Value.ToString()) &&
                                                   p.ClassId.Equals(classId.ClassId)
                                             select new { p.SpareId, p.SpareName };
                    ddl_SpareId.TextField = "SpareName";
                    ddl_SpareId.ValueField = "SpareId";
                    ddl_SpareId.DataBind();
                }
            }
            catch { }
        }
        private void ClearGridTextBoxes()
        {
            TextBox txt_SparCost = grd_SparParts.FooterRow.FindControl("txt_SparCost") as TextBox;
            TextBox txt_SparCount = grd_SparParts.FooterRow.FindControl("txt_SparCount") as TextBox;
            TextBox txt_SparTotal = grd_SparParts.FooterRow.FindControl("txt_SparTotal") as TextBox;
            TextBox txt_LaborCost = grd_SparParts.FooterRow.FindControl("txt_LaborCost") as TextBox;
            TextBox txt_TotalCost = grd_SparParts.FooterRow.FindControl("txt_TotalCost") as TextBox;
            txt_SparCost.Text = string.Empty;
            txt_SparCount.Text = string.Empty;
            txt_SparTotal.Text = string.Empty;
            txt_LaborCost.Text = string.Empty;
            txt_TotalCost.Text = string.Empty;
        }
        private void CalSparTotal()
        {
            TextBox txt_SparCost = grd_SparParts.FooterRow.FindControl("txt_SparCost") as TextBox;
            TextBox txt_SparCount = grd_SparParts.FooterRow.FindControl("txt_SparCount") as TextBox;
            TextBox txt_SparTotal = grd_SparParts.FooterRow.FindControl("txt_SparTotal") as TextBox;
            TextBox txt_LaborCost = grd_SparParts.FooterRow.FindControl("txt_LaborCost") as TextBox;
            TextBox txt_TotalCost = grd_SparParts.FooterRow.FindControl("txt_TotalCost") as TextBox;

            txt_SparTotal.Text = (decimal.Parse(string.IsNullOrEmpty(txt_SparCost.Text) ? "0" : txt_SparCost.Text) * decimal.Parse(string.IsNullOrEmpty(txt_SparCount.Text) ? "0" : txt_SparCount.Text)).ToString();
            txt_TotalCost.Text = (decimal.Parse(string.IsNullOrEmpty(txt_SparTotal.Text) ? "0" : txt_SparTotal.Text) + decimal.Parse(string.IsNullOrEmpty(txt_LaborCost.Text) ? "0" : txt_LaborCost.Text)).ToString();
        }
        private void CalcTotals()
        {
            decimal SparTotals = 0;
            decimal labourCosts = 0;
            decimal totalcost = 0;


            for (int i = 0; i < grd_SparParts.Rows.Count; i++)
            {
                TextBox txt_SparTotalsItem = (TextBox)grd_SparParts.Rows[i].FindControl("txt_SparTotal");
                TextBox txt_LaborCostItem = (TextBox)grd_SparParts.Rows[i].FindControl("txt_LaborCost");
                TextBox txt_TotalCostItem = (TextBox)grd_SparParts.Rows[i].FindControl("txt_TotalCost");
                SparTotals += decimal.Parse(string.IsNullOrEmpty(txt_SparTotalsItem.Text) ? "0" : txt_SparTotalsItem.Text);
                labourCosts += decimal.Parse(string.IsNullOrEmpty(txt_LaborCostItem.Text) ? "0" : txt_LaborCostItem.Text);
                totalcost += decimal.Parse(string.IsNullOrEmpty(txt_TotalCostItem.Text) ? "0" : txt_TotalCostItem.Text);



                txt_SparSum.Text = SparTotals.ToString();
                txt_LaborSum.Text = labourCosts.ToString();
                txt_TotalSum.Text = totalcost.ToString();
            }
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.BusCheck.GetHashCode());
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
            Page.Title = "الصيانة الطارئة للسيارات";
            permissions(dc);

            if (!IsPostBack)
            {
                if (Session["UserID"] != null && Convert.ToInt32(Session["UserID"].ToString()) > 0)
                {
                    UserID = Convert.ToInt32(Session["UserID"].ToString());
                }
                else
                {
                    Response.Redirect(@"..\Pages\Login.aspx");
                }

                fill_Services();
                fill_Buses();
                int? id = null;
                if (Session["UserID"] != null)
                {
                    id = int.Parse(Session["UserID"].ToString());
                }
                else
                {
                    Response.Redirect("Login.aspx", false);
                }
                ddl_Vehcles.DataSource = from p in dc.Vehcles where p.Company.UserCompanies.Any(x => x.UserID == id) && p.Active == true select new { p.VehcleId, p.PlateNo };
                ddl_Vehcles.DataTextField = "PlateNo";
                ddl_Vehcles.DataValueField = "VehcleId";
                ddl_Vehcles.DataBind();


                #region SparParts Table
                dt = new DataTable();
                DataColumn dc1 = new DataColumn("SpareMainId");
                DataColumn dc2 = new DataColumn("SpareId");
                DataColumn dc3 = new DataColumn("SparCost");
                DataColumn dc4 = new DataColumn("SparCount");
                DataColumn dc5 = new DataColumn("SparTotal");
                DataColumn dc6 = new DataColumn("LaborCost");
                DataColumn dc7 = new DataColumn("TotalCost");
                DataColumn dc8 = new DataColumn("Notes");
                DataColumn dc9 = new DataColumn("EmployeeId");
                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);
                dt.Columns.Add(dc3);
                dt.Columns.Add(dc4);
                dt.Columns.Add(dc5);
                dt.Columns.Add(dc6);
                dt.Columns.Add(dc7);
                dt.Columns.Add(dc8);
                dt.Columns.Add(dc9);
                if (dt.Rows.Count == 0) dt.Rows.Add(dt.NewRow());
                ViewState["Spares"] = dt;
                grd_SparParts.DataSource = ViewState["Spares"];
                grd_SparParts.DataBind();
                grd_SparParts.Rows[0].Visible = false;

                #endregion

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                return;
            }
            int? km = null; ;
            if (!string.IsNullOrEmpty(txtKMcount.Text))
            {
                km = Convert.ToInt32(txtKMcount.Text);
            }
            else
            {
                km = 0;
            }
            string msg = Check_Service(Convert.ToInt32(ddlServices.SelectedValue), Convert.ToInt32(ddl_Vehcles.SelectedValue), km, Convert.ToDateTime(txtCheckDate.Text));


            if (msg == "OK")
            {

                doInsert();

                txtKMcount.Text = "0";
                txtCheckDate.Text = string.Empty;
            }
            else
            {
                pnlCheckMessage.Visible = true;
                lblCheckMSG.Text = msg;
            }
        }
        protected void btnContinue_Click(object sender, EventArgs e)
        {
            doInsert();
            pnlCheckMessage.Visible = false;
            lblCheckMSG.Text = string.Empty;

        }
        protected void btnCancelCheck_Click(object sender, EventArgs e)
        {
            pnlCheckMessage.Visible = false;
            lblCheckMSG.Text = string.Empty;
        }
        protected void grd_SparParts_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }
        protected void ddl_SpareMainId_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGridSparPart();

        }
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        protected void grd_SparParts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                ASPxComboBox ddl_SpareMainId = (e.Row.FindControl("ddl_SpareMainId") as ASPxComboBox);
                ddl_SpareMainId.DataSource = from p in dc.ConfigDetails where p.MasterId == 1 select new { p.ConfigDetailId, p.ConfigDetailName };
                ddl_SpareMainId.TextField = "ConfigDetailName";
                ddl_SpareMainId.ValueField = "ConfigDetailId";
                ddl_SpareMainId.DataBind();

                DataRowView dr = e.Row.DataItem as DataRowView;
                ddl_SpareMainId.Value = dr["SpareMainId"].ToString();


                ASPxComboBox ddl_EmployeeIdF = (e.Row.FindControl("ddl_EmployeeId") as ASPxComboBox);
                ddl_EmployeeIdF.DataSource = from p in dc.Employees where p.IsTecnician.Equals(1) select new { p.EmployeeId, p.EmployeeName };
                ddl_EmployeeIdF.TextField = "EmployeeName";
                ddl_EmployeeIdF.ValueField = "EmployeeId";
                ddl_EmployeeIdF.DataBind();

                DataRowView drF = e.Row.DataItem as DataRowView;
                ddl_EmployeeIdF.Value = drF["EmployeeId"].ToString();



                try
                {
                    if (ddl_Vehcles.SelectedItem.Value != null)
                    {
                        var classid = (from p in dc.Vehcles where p.VehcleId == int.Parse(ddl_Vehcles.SelectedItem.Value.ToString()) select new { p.ClassId }).SingleOrDefault();
                        ASPxComboBox ddl_SpareId = (e.Row.FindControl("ddl_SpareId") as ASPxComboBox);
                        ddl_SpareId.DataSource = from p in dc.SpareParts select new { p.SpareId, p.SpareName };
                        ddl_SpareId.TextField = "SpareName";
                        ddl_SpareId.ValueField = "SpareId";
                        ddl_SpareId.DataBind();
                        DataRowView dr1 = e.Row.DataItem as DataRowView;
                        ddl_SpareId.Value = dr1["SpareId"].ToString();
                    }
                }
                catch
                {

                }



            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {

                var ddl_SpareMainId = e.Row.FindControl("ddl_SpareMainId") as ASPxComboBox;

                if (null != ddl_SpareMainId)
                {
                    ddl_SpareMainId.DataSource = from p in dc.ConfigDetails where p.MasterId == 1 select new { p.ConfigDetailId, p.ConfigDetailName };
                    ddl_SpareMainId.TextField = "ConfigDetailName";
                    ddl_SpareMainId.ValueField = "ConfigDetailId";
                    ddl_SpareMainId.DataBind();
                }


                var ddl_EmployeeId = e.Row.FindControl("ddl_EmployeeId") as ASPxComboBox;

                if (null != ddl_EmployeeId)
                {
                    ddl_EmployeeId.DataSource = from p in dc.Employees where p.IsTecnician.Equals(1) select new { p.EmployeeId, p.EmployeeName };
                    ddl_EmployeeId.TextField = "EmployeeName";
                    ddl_EmployeeId.ValueField = "EmployeeId";
                    ddl_EmployeeId.DataBind();
                }
            }
        }
        protected void ddl_SpareId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASPxComboBox ddl_SpareId = grd_SparParts.FooterRow.FindControl("ddl_SpareIdAdd") as ASPxComboBox;
            TextBox txt_SparCost = grd_SparParts.FooterRow.FindControl("txt_SparCost") as TextBox;
            TextBox txt_LaborCost = grd_SparParts.FooterRow.FindControl("txt_LaborCost") as TextBox;
            TextBox txt_Notes = grd_SparParts.FooterRow.FindControl("txt_Notes") as TextBox;
            ClearGridTextBoxes();
            try
            {
                if (ddl_SpareId.SelectedItem.Value != null)
                {
                    txt_SparCost.Text = "";
                    txt_LaborCost.Text = "";
                    var query = (from p in dc.SpareParts where p.SpareId == int.Parse(ddl_SpareId.SelectedItem.Value.ToString()) select new { p.SparePrice, p.LabourCost }).SingleOrDefault();
                    txt_SparCost.Text = query.SparePrice.ToString();
                    txt_LaborCost.Text = query.LabourCost.ToString();
                    CalSparTotal();
                }

            }
            catch
            {

            }

            if (ddl_SpareId.SelectedItem != null && ddl_Vehcles.SelectedItem != null)
            {
                var query = (from p in dc.vehclesConsumedSparesVws where p.SparId == int.Parse(ddl_SpareId.SelectedItem.Value.ToString()) && p.VehcleId == int.Parse(ddl_Vehcles.SelectedItem.Value.ToString()) select new { p.Date }).ToList().LastOrDefault();
                if (query != null)
                {
                    txt_LastSparPay.Text = DateTime.Parse(query.Date.ToString()).ToShortDateString();
                }
                else
                {
                    txt_LastSparPay.Text = string.Empty;
                }
            }

        }
        protected void txt_SparCount_TextChanged(object sender, EventArgs e)
        {
            CalSparTotal();


        }
        protected void txt_SparCost_TextChanged(object sender, EventArgs e)
        {
            CalSparTotal();
        }
        protected void txt_LaborCost_TextChanged(object sender, EventArgs e)
        {
            CalSparTotal();
        }
        protected void ddl_Vehcles_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = (DataTable)ViewState["Spares"];
            dt.Rows.Clear();
            dt.Rows.Add(dt.NewRow());
            grd_SparParts.DataSource = dt;
            grd_SparParts.DataBind();
            grd_SparParts.Rows[0].Visible = false;
            ViewState["Spares"] = dt;
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ASPxComboBox ddl_SpareMainId = (ASPxComboBox)grd_SparParts.FooterRow.FindControl("ddl_SpareMainId");
            ASPxComboBox ddl_SpareId = (ASPxComboBox)grd_SparParts.FooterRow.FindControl("ddl_SpareIdAdd");
            TextBox txt_SparCost = (TextBox)grd_SparParts.FooterRow.FindControl("txt_SparCost");
            TextBox txt_SparCount = (TextBox)grd_SparParts.FooterRow.FindControl("txt_SparCount");
            TextBox txt_SparTotal = (TextBox)grd_SparParts.FooterRow.FindControl("txt_SparTotal");
            TextBox txt_LaborCost = (TextBox)grd_SparParts.FooterRow.FindControl("txt_LaborCost");
            TextBox txt_TotalCost = (TextBox)grd_SparParts.FooterRow.FindControl("txt_TotalCost");
            TextBox txt_Notes = (TextBox)grd_SparParts.FooterRow.FindControl("txt_Notes");
            ASPxComboBox ddl_EmployeeId = (ASPxComboBox)grd_SparParts.FooterRow.FindControl("ddl_EmployeeId");
            #region validation
            try
            {
                if (ddl_SpareMainId.SelectedItem.Value == null)
                {
                    divMsg2.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "ادخل الصنف الرئيسي";
                    return;

                }
            }
            catch
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "ادخل الصنف الرئيسي";
                return;
            }
            try
            {
                if (ddl_SpareId.SelectedItem.Value == null)
                {
                    divMsg2.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "ادخل الصنف الفرعي";
                    return;

                }
            }
            catch
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "ادخل الصنف الفرعي";
                return;
            }
            try
            {
                if (ddl_EmployeeId.SelectedItem.Value == null)
                {
                    divMsg2.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "ادخل اسم الفنى";
                    return;

                }
            }
            catch
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "ادخل اسم الفنى";
                return;
            }
            if (decimal.Parse(string.IsNullOrEmpty(txt_TotalCost.Text) ? "0" : txt_TotalCost.Text) <= 0)
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا لا يمكن ادخل اجمالي التكلفة بصفر";
                return;
            }

            #endregion


            dt = (DataTable)ViewState["Spares"];
            DataRow dr1 = dt.NewRow();
            dr1[0] = ddl_SpareMainId.SelectedItem.Value;
            dr1[1] = ddl_SpareId.SelectedItem.Value;
            dr1[2] = txt_SparCost.Text;
            dr1[3] = txt_SparCount.Text;
            dr1[4] = txt_SparTotal.Text;
            dr1[5] = txt_LaborCost.Text;
            dr1[6] = txt_TotalCost.Text;
            dr1[7] = txt_Notes.Text;
            dr1[8] = ddl_EmployeeId.SelectedItem.Value;
            dt.Rows.Add(dr1);

            grd_SparParts.DataSource = dt;
            grd_SparParts.DataBind();
            ViewState["Spares"] = dt;
            grd_SparParts.Rows[0].Visible = false;


            CalcTotals();
        }

        protected void grd_SparParts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btn_deleteFromGrid_Click(object sender, EventArgs e)
        {

        }

        protected void grd_SparParts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {

                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                int index = gvr.RowIndex;

                DataTable dt = ViewState["Spares"] as DataTable;
                dt.Rows[index].Delete();
                ViewState["Spares"] = dt;
                grd_SparParts.DataSource = ViewState["Spares"];
                grd_SparParts.DataBind();
                grd_SparParts.Rows[0].Visible = false;
                CalcTotals();
            }

        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            pnl_AddSpare.Visible = true;
        }
        protected void btn_CancelSpare_Click(object sender, EventArgs e)
        {
            pnl_AddSpare.Visible = false;
        }
        protected void btn_ContinueSpare_Click(object sender, EventArgs e)
        {
            if (ddl_Vehcles.SelectedItem == null)
            {
                pnl_Alert.Attributes["class"] = "alert alert-danger text-right";
                lbl_SparAlert.Text = "عفو ادخال رقم اللوحة";
                return;
            }
            ASPxComboBox ddl_SpareMainId = grd_SparParts.FooterRow.FindControl("ddl_SpareMainId") as ASPxComboBox;
            if (ddl_SpareMainId.SelectedItem == null)
            {
                pnl_Alert.Attributes["class"] = "alert alert-danger text-right";
                lbl_SparAlert.Text = "عفو ادخال التصنيف الرئيسيى";
                return;
            }
            if (txt_SpareName.Text == string.Empty)
            {
                pnl_Alert.Attributes["class"] = "alert alert-danger text-right";
                lbl_SparAlert.Text = "عفو ادخال اسم قطعة الغيار";
                return;
            }
            if (txt_SparePrice.Text == string.Empty)
            {
                pnl_Alert.Attributes["class"] = "alert alert-danger text-right";
                lbl_SparAlert.Text = "عفو ادخال سعر قطعة الغيار";
                return;
            }
            if (txt_LabourCost.Text == string.Empty)
            {
                pnl_Alert.Attributes["class"] = "alert alert-danger text-right";
                lbl_SparAlert.Text = "عفو ادخال تكلفة الايدى العاملة";
                return;
            }
            var VehcleQuery = (from p in dc.Vehcles where p.VehcleId == int.Parse(ddl_Vehcles.SelectedItem.Value.ToString()) select new { p.ClassId }).SingleOrDefault();



            SparePart _SpareParts = new SparePart();
            _SpareParts.SpareName = txt_SpareName.Text;
            _SpareParts.MainCategory = int.Parse(ddl_SpareMainId.SelectedItem.Value.ToString());
            _SpareParts.ClassId = VehcleQuery.ClassId;
            _SpareParts.SparePrice = decimal.Parse(txt_SparePrice.Text);
            _SpareParts.LabourCost = decimal.Parse(txt_LabourCost.Text);
            _SpareParts.Notes = txt_Notes.Text;

            dc.SpareParts.InsertOnSubmit(_SpareParts);
            dc.SubmitChanges();


            if (_SpareParts.SpareId > 0)
            {
                pnl_AddSpare.Visible = false;
                txt_SpareName.Text = string.Empty;
                txt_SparePrice.Text = string.Empty;
                txt_LabourCost.Text = string.Empty;
                txt_Notes.Text = string.Empty;
                FillGridSparPart();

            }


        }
        #endregion
    }

}
