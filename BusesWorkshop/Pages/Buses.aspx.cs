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

namespace BusesWorkshop.Pages
{
    public partial class Buses : System.Web.UI.Page
    {
        DataTable dt;
        private int BusID
        {
            get
            {
                if (ViewState["_BusID"] != null)
                {
                    return Convert.ToInt32(ViewState["_BusID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_BusID", value);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            WorkshopDataContext dc = new WorkshopDataContext();
            permissions(dc);
            if (!IsPostBack)
            {
                fill_Companies();
                fill_Drivers();
                fill_Bus();
                FillModels();
                FillSuperVisors();


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

        private void FillSuperVisors()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BusWorkshopConnectionString2"].ToString());
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT     ID ,Name FROM Employees", con);
            //ddl_SuperVisorId.DataSource = cmd.ExecuteReader();
            ddl_SuperVisorId1.DataSource = cmd.ExecuteReader();
            //ddl_SuperVisorId.DataTextField = "Name";
            //ddl_SuperVisorId.DataValueField = "ID";
            ddl_SuperVisorId1.TextField = "Name";
            ddl_SuperVisorId1.ValueField = "ID";
            //ddl_SuperVisorId.DataBind();
            ddl_SuperVisorId1.DataBind();
            //ddl_SuperVisorId.Items.Insert(0, new ListItem("اختر المشرف", ""));

            con.Close();
        }

        private void FillModels()
        {
            WorkshopDataContext dc = new WorkshopDataContext();
            ddl_model.DataSource = from p in dc.Types select new { p.Id, p.Name };
            ddl_model.DataTextField = "Name";
            ddl_model.DataValueField = "Id";
            ddl_model.DataBind();
            ddl_model.Items.Insert(0, new ListItem("اختر الموديل", "0"));
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {

            WorkshopDataContext dc = new WorkshopDataContext();
            int result = 0;

            if (ddl_model.SelectedValue == "0")
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "ادخل موديل السيارة";
                return;
            }

            if (BusID > 0)
            {
                // Update
                #region Validation
                var QueryValidation = (from p in dc.Buses where p.PlateNumber.Equals(txtPlateNumber.Text) && p.ID != BusID select new { p }).ToList();
                if (QueryValidation.Count > 0)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "رقم اللوحة مسجل من قبل لحافلة اخري";
                    return;
                }
                QueryValidation = (from p in dc.Buses where p.PlateNumber.Equals(txtBodyDesc.Text) && p.ID != BusID  select new { p }).ToList();
                if (QueryValidation.Count > 0)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "رقم الهيكل مسجل من قبل لحافلة اخري";
                    return;
                }
                if (ddl_model.SelectedValue == "0")
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "ادخل موديل السيارة";
                    return;
                }

                #endregion
                Buse tblBus = dc.Buses.Single(bus => bus.ID == BusID);
                tblBus.PlateNumber = txtPlateNumber.Text;
          
                tblBus.BodyDesc = txtBodyDesc.Text; ;
                tblBus.OwnerName = txtOwner.Text;
 
                tblBus.model = int.Parse(ddl_model.SelectedValue);
                tblBus.Serial = txt_Serial.Text;
                tblBus.DocumentNo = txt_DocumentNo.Text;
         
                tblBus.DocumentNo = txt_DocumentNo.Text;
                try
                {
                    string RenewDate = DateTime.ParseExact(txtRenewDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                        .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    tblBus.RenewDate = Convert.ToDateTime(RenewDate);
                }
                catch { tblBus.RenewDate = null; }
                try
                {
                    string PeriodicalInspectionDateExpiry = DateTime.ParseExact(txt_PeriodicalInspectionDateExpiry.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                           .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    tblBus.PeriodicalInspectionDateExpiry = DateTime.Parse(PeriodicalInspectionDateExpiry);
                }
                catch { tblBus.PeriodicalInspectionDateExpiry = null; }
                try { tblBus.StudentsCount = Convert.ToInt32(txtStudentsCount.Text); }
                catch { tblBus.StudentsCount = null; }
                try
                {
                    tblBus.SuperVisorId = int.Parse(ddl_SuperVisorId1.SelectedItem.Value.ToString());
                }
                catch
                {
                    tblBus.SuperVisorId = null;
                }
                tblBus.CompID = Convert.ToInt32(ddlCompanies.SelectedValue);
                tblBus.DriverID = Convert.ToInt32(ddlDrivers.SelectedValue);

                dc.SubmitChanges();



                result = tblBus.ID;
                #region Remove All Attachments
                var query = from c in dc.BusAttachments where c.VehcleId .Equals(result) select c;
                foreach (var deletedAttachment in query)
                {
                    dc.BusAttachments.DeleteOnSubmit(deletedAttachment);
                }
                dc.SubmitChanges();
                #endregion
                #region Insert Attachments

                for (int i = 0; i < GrdAttAttment.Rows.Count; i++)
                {
                    ASPxComboBox ddl_AttachmentName = (ASPxComboBox)GrdAttAttment.Rows[i].FindControl("ddl_AttachmentName");
                    Label lbl_Notes = (Label)GrdAttAttment.Rows[i].FindControl("lbl_Notes");

                    if (ddl_AttachmentName.SelectedItem  == null && lbl_Notes.Text == "") goto fo;
                    using (WorkshopDataContext ctx = new WorkshopDataContext())
                    {
                        BusAttachment _BusAttachment = new BusAttachment();
                        _BusAttachment.VehcleId  = result;
                        _BusAttachment.AttachmentName =int.Parse( ddl_AttachmentName.SelectedItem.Value.ToString());
                        _BusAttachment.Notes = lbl_Notes.Text;
                        ctx.BusAttachments.InsertOnSubmit(_BusAttachment);
                        ctx.SubmitChanges(); ;
                    }
                fo: ;
                }


                #endregion
            }
            else
            {
                // Insert

                #region Validation
                var QueryValidation = (from p in dc.Buses where p.PlateNumber.Equals(txtPlateNumber.Text) select new {p}).ToList();
                if (QueryValidation.Count > 0)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "رقم اللوحة مسجل من قبل";
                    return;
                }
                QueryValidation = (from p in dc.Buses where p.PlateNumber.Equals(txtBodyDesc.Text) select new { p }).ToList();
                if (QueryValidation.Count > 0)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "رقم الهيكل مسجل من قبل";
                    return;
                }
                if (ddl_model.SelectedValue == "0")
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "ادخل موديل السيارة";
                    return;
                }

                #endregion
                Buse tblsbus = new Buse();
                tblsbus.PlateNumber = txtPlateNumber.Text;
                tblsbus.BodyDesc = txtBodyDesc.Text; ;
                tblsbus.OwnerName = txtOwner.Text;

                tblsbus.model = int.Parse(ddl_model.SelectedValue);
                tblsbus.Serial = txt_Serial.Text;
                tblsbus.DocumentNo = txt_DocumentNo.Text;

                try
                {
                    string RenewDate = DateTime.ParseExact(txtRenewDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                        .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    tblsbus.RenewDate = Convert.ToDateTime(RenewDate);
                }
                catch { tblsbus.RenewDate = null; }
                try
                {
                    string PeriodicalInspectionDateExpiry = DateTime.ParseExact(txt_PeriodicalInspectionDateExpiry.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                           .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    tblsbus.PeriodicalInspectionDateExpiry = DateTime.Parse(PeriodicalInspectionDateExpiry);
                }
                catch { tblsbus.PeriodicalInspectionDateExpiry = null; }
                try { tblsbus.StudentsCount = Convert.ToInt32(txtStudentsCount.Text); }
                catch { tblsbus.StudentsCount = null; }
                try
                {
                    tblsbus.SuperVisorId = int.Parse(ddl_SuperVisorId1.SelectedItem.Value.ToString());
                }
                catch
                {
                    tblsbus.SuperVisorId = null;
                }


                tblsbus.CompID = Convert.ToInt32(ddlCompanies.SelectedValue);
                tblsbus.DriverID = Convert.ToInt32(ddlDrivers.SelectedValue);
                dc.Buses.InsertOnSubmit(tblsbus);
                dc.SubmitChanges();

                result = tblsbus.ID;

                #region Insert Attachments

                for (int i = 0; i < GrdAttAttment.Rows.Count; i++)
                {
                    ASPxComboBox ddl_AttachmentName = (ASPxComboBox)GrdAttAttment.Rows[i].FindControl("ddl_AttachmentName");
                    Label lbl_Notes = (Label)GrdAttAttment.Rows[i].FindControl("lbl_Notes");

                    if (ddl_AttachmentName.SelectedItem  == null && lbl_Notes.Text == "") goto fo;
                    using (WorkshopDataContext ctx = new WorkshopDataContext())
                    {
                        BusAttachment _BusAttachment = new BusAttachment();
                        _BusAttachment.VehcleId  = result;
                        _BusAttachment.AttachmentName = int.Parse(ddl_AttachmentName.SelectedItem.Value.ToString());
                        _BusAttachment.Notes = lbl_Notes.Text;
                        ctx.BusAttachments.InsertOnSubmit(_BusAttachment);
                        ctx.SubmitChanges(); ;
                    }
                fo: ;
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
                // divMsg. = "alert alert-danger text-right"; 
            }


            emptyControls();

            fill_Bus();

        }

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;


            BusID = Convert.ToInt32(gvBuses.DataKeys[row.RowIndex].Value);

            WorkshopDataContext dc = new WorkshopDataContext();

            DataTable dt = new DataTable();
            var query = from bus in dc.GetTable<Buse>().Where(bs => bs.ID == BusID) select bus;
            dt = query.CopyToDataTable();
            if (dt.Rows.Count > 0)
            {
                txtPlateNumber.Text = dt.Rows[0]["PlateNumber"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["RenewDate"].ToString()))
                {
                    txtRenewDate.Text = DateTime.Parse(dt.Rows[0]["RenewDate"].ToString()).ToShortDateString();
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["PeriodicalInspectionDateExpiry"].ToString()))
                {
                    txt_PeriodicalInspectionDateExpiry.Text = DateTime.Parse(dt.Rows[0]["PeriodicalInspectionDateExpiry"].ToString()).ToShortDateString();

                }

                txtBodyDesc.Text = dt.Rows[0]["BodyDesc"].ToString();
                txtOwner.Text = dt.Rows[0]["OwnerName"].ToString();
                ddlCompanies.SelectedValue = dt.Rows[0]["CompID"].ToString();
                ddlDrivers.SelectedValue = dt.Rows[0]["DriverID"].ToString();
                txtStudentsCount.Text = dt.Rows[0]["StudentsCount"].ToString();
                ddl_model.SelectedValue = dt.Rows[0]["model"].ToString();
                txt_Serial.Text = dt.Rows[0]["Serial"].ToString();
                txt_DocumentNo.Text = dt.Rows[0]["DocumentNo"].ToString();
                ddl_SuperVisorId1.Value = dt.Rows[0]["SuperVisorId"].ToString();

                #region fill grd_attachment with data

                var queryAtt = from p in dc.BusAttachments where p.VehcleId .Equals(BusID) select new { p.AttachmentName, p.Notes };
                dt = queryAtt.CopyToDataTable();
                ViewState["Attachments"] = dt;
                GrdAttAttment.DataSource = ViewState["Attachments"];
                GrdAttAttment.DataBind();


                #endregion
            }
        }


        void fill_Drivers()
        {
            WorkshopDataContext dc = new WorkshopDataContext();

            DataTable dt = new DataTable();
            var query = from drv in dc.GetTable<Driver>() select drv;
            dt = query.CopyToDataTable();
            if (dt.Rows.Count > 0)
            {
                ddlDrivers.DataSource = dt;
                ddlDrivers.DataTextField = "DriverName";
                ddlDrivers.DataValueField = "ID";
                ddlDrivers.DataBind();
            }
            else
            {
                ddlDrivers.DataSource = null;
                ddlDrivers.DataBind();
            }
        }

        void fill_Companies()
        {
            WorkshopDataContext dc = new WorkshopDataContext();

            DataTable dt = new DataTable();
            var query = from cmp in dc.GetTable<Company>() select cmp;
            dt = query.CopyToDataTable();
            if (dt.Rows.Count > 0)
            {
                ddlCompanies.DataSource = dt;
                ddlCompanies.DataTextField = "CompName";
                ddlCompanies.DataValueField = "ID";
                ddlCompanies.DataBind();
            }
            else
            {
                ddlCompanies.DataSource = null;
                ddlCompanies.DataBind();
            }
        }

        void fill_Bus()
        {
            WorkshopDataContext dc = new WorkshopDataContext();

            DataTable dt = new DataTable();
            var query = from bus in dc.GetTable<Buse>() select bus;
            dt = query.CopyToDataTable();
            if (dt.Rows.Count > 0)
            {
                gvBuses.DataSource = dt;
                gvBuses.DataBind();
            }
            else
            {
                gvBuses.DataSource = null;
                gvBuses.DataBind();
            }
        }

        void emptyControls()
        {
            txtPlateNumber.Text = string.Empty;
            txtRenewDate.Text = string.Empty;
            txtBodyDesc.Text = string.Empty;
            txtOwner.Text = string.Empty;
            BusID = 0;
            dt = (DataTable)ViewState["Attachments"];
            dt.Rows.Clear();
            dt.Rows.Add(dt.NewRow());
            GrdAttAttment.DataSource = dt;
            GrdAttAttment.DataBind();
            ViewState["Attachments"] = dt;

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            TextBox txt_AttachmentName = (TextBox)GrdAttAttment.FooterRow.FindControl("txt_AttachmnentName");
            TextBox txt_Notes = GrdAttAttment.FooterRow.FindControl("txt_Notes") as TextBox;
            dt = (DataTable)ViewState["Attachments"];
            DataRow dr1 = dt.NewRow();
            dr1[0] = txt_AttachmentName.Text;
            dr1[1] = txt_Notes.Text;
            dt.Rows.Add(dr1);

            GrdAttAttment.DataSource = dt;
            GrdAttAttment.DataBind();
            ViewState["Attachments"] = dt;
            GrdAttAttment.Rows[0].Visible = false;
        }

        public static DateTime GetSpecificDateBackSlashFormate(string Date)
        {

            DateTime dt = DateTime.ParseExact(Date, "d/M/yyyy", CultureInfo.InvariantCulture);
            return dt;
        }

        protected void GrdAttAttment_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GrdAttAttment_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
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
        }

        protected void GrdAttAttment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = ViewState["Attachments"] as DataTable;
            dt.Rows[index].Delete();
            ViewState["Attachments"] = dt;
            GrdAttAttment.DataSource = ViewState["Attachments"];
            GrdAttAttment.DataBind();


        }

        protected void ddl_SuperVisorId1_DataBound(object sender, EventArgs e)
        {
            //(sender as ASPxComboBox).Items.Insert(0, new ListEditItem("*"));
            //if ((sender as ASPxComboBox).Value == null)
            //    (sender as ASPxComboBox).SelectedIndex = 0;
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
            }
            catch
            {
                Response.Redirect(@"..\Pages\Login.aspx");
            }
        }


    }
}
