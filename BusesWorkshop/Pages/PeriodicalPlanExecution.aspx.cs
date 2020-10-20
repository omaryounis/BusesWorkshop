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
using DevExpress.Web;
using BusesWorkshop.DAL;

namespace BusesWorkshop.Pages
{
    public partial class PeriodicalPlanExecution : System.Web.UI.Page
    {
        #region "Properties"
        WorkshopDataContext dc = new WorkshopDataContext();
        DataTable dt;
        private int PeriodicalPlanExcutionID
        {
            get
            {
                if (ViewState["_PeriodicalPlanExcutionID"] != null)
                {
                    return Convert.ToInt32(ViewState["_PeriodicalPlanExcutionID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_PeriodicalPlanExcutionID", value);
            }
        }

        #endregion

        #region"Methods"
        private void FillGridPlans()
        {
            if (ddl_VehcleId.SelectedItem != null)
            {
                gvPlans.DataSource = from p in dc.PeriodicalPlanExecutions where p.VehcleId == int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()) select new { p.PeriodicalPlanExcutionId, p.Vehcle.PlateNo, Date = p.Date.Date.ToShortDateString(), p.BillNo, p.PeriodicalPlan.PlanName };
                gvPlans.DataBind();
            }
        }
        private void FillPlans()
        {
            if (ddl_VehcleId.SelectedItem != null)
            {
                ddl_maintPlanId.Items.Clear();
                ddl_maintPlanId.Text = "";
                ddl_maintPlanId.Value = null;
                ddl_maintPlanId.DataSource = from p in dc.PeriodicalPlanToVehcles where p.VehcleId == int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()) select new { p.PeriodicalPlan.maintPlanId, p.PeriodicalPlan.PlanName };

                ddl_maintPlanId.TextField = "PlanName";
                ddl_maintPlanId.ValueField = "maintPlanId";
                ddl_maintPlanId.DataBind();
            }
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

        private void FillGridSpareParts()
        {
            ASPxComboBox ddl_SpareMainId = grd_PeriodicalPlanExecDetail.FooterRow.FindControl("ddl_SpareMainId") as ASPxComboBox;
            ASPxComboBox ddl_SpareId = grd_PeriodicalPlanExecDetail.FooterRow.FindControl("ddl_SpareIdAdd") as ASPxComboBox;
            ClearGridTextBoxes();

            try
            {
                if (ddl_SpareMainId.SelectedItem.Value != null)
                {

                    var classId = (from p in dc.Vehcles where p.VehcleId == int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()) select new { p.Color, p.ClassId }).SingleOrDefault();
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
            TextBox txt_SparCost = grd_PeriodicalPlanExecDetail.FooterRow.FindControl("txt_SparCost") as TextBox;
            TextBox txt_SparCount = grd_PeriodicalPlanExecDetail.FooterRow.FindControl("txt_SparCount") as TextBox;
            TextBox txt_SparTotal = grd_PeriodicalPlanExecDetail.FooterRow.FindControl("txt_SparTotal") as TextBox;
            TextBox txt_LaborCost = grd_PeriodicalPlanExecDetail.FooterRow.FindControl("txt_LaborCost") as TextBox;
            TextBox txt_TotalCost = grd_PeriodicalPlanExecDetail.FooterRow.FindControl("txt_TotalCost") as TextBox;
            txt_SparCost.Text = string.Empty;
            txt_SparCount.Text = string.Empty;
            txt_SparTotal.Text = string.Empty;
            txt_LaborCost.Text = string.Empty;
            txt_TotalCost.Text = string.Empty;
        }
        private void CalSparTotal()
        {
            TextBox txt_SparCost = grd_PeriodicalPlanExecDetail.FooterRow.FindControl("txt_SparCost") as TextBox;
            TextBox txt_SparCount = grd_PeriodicalPlanExecDetail.FooterRow.FindControl("txt_SparCount") as TextBox;
            TextBox txt_SparTotal = grd_PeriodicalPlanExecDetail.FooterRow.FindControl("txt_SparTotal") as TextBox;
            TextBox txt_LaborCost = grd_PeriodicalPlanExecDetail.FooterRow.FindControl("txt_LaborCost") as TextBox;
            TextBox txt_TotalCost = grd_PeriodicalPlanExecDetail.FooterRow.FindControl("txt_TotalCost") as TextBox;

            txt_SparTotal.Text = (decimal.Parse(string.IsNullOrEmpty(txt_SparCost.Text) ? "0" : txt_SparCost.Text) * decimal.Parse(string.IsNullOrEmpty(txt_SparCount.Text) ? "0" : txt_SparCount.Text)).ToString();
            txt_TotalCost.Text = (decimal.Parse(string.IsNullOrEmpty(txt_SparTotal.Text) ? "0" : txt_SparTotal.Text) + decimal.Parse(string.IsNullOrEmpty(txt_LaborCost.Text) ? "0" : txt_LaborCost.Text)).ToString();
        }
        private void CalcTotals()
        {
            decimal SparTotals = 0;
            decimal labourCosts = 0;
            decimal totalcost = 0;


            for (int i = 0; i < grd_PeriodicalPlanExecDetail.Rows.Count; i++)
            {
                TextBox txt_SparTotalsItem = (TextBox)grd_PeriodicalPlanExecDetail.Rows[i].FindControl("txt_SparTotal");
                TextBox txt_LaborCostItem = (TextBox)grd_PeriodicalPlanExecDetail.Rows[i].FindControl("txt_LaborCost");
                TextBox txt_TotalCostItem = (TextBox)grd_PeriodicalPlanExecDetail.Rows[i].FindControl("txt_TotalCost");
                SparTotals += decimal.Parse(string.IsNullOrEmpty(txt_SparTotalsItem.Text) ? "0" : txt_SparTotalsItem.Text);
                labourCosts += decimal.Parse(string.IsNullOrEmpty(txt_LaborCostItem.Text) ? "0" : txt_LaborCostItem.Text);
                totalcost += decimal.Parse(string.IsNullOrEmpty(txt_TotalCostItem.Text) ? "0" : txt_TotalCostItem.Text);



                txt_SparSum.Text = SparTotals.ToString();
                txt_LaborSum.Text = labourCosts.ToString();
                txt_TotalSum.Text = totalcost.ToString();
            }
        }
        private void EmptyControls()
        {
            txt_NextID.Text = "";
            ddl_VehcleId.Value = null;
            ddl_VehcleId.Text = "";
            ddl_maintPlanId.Value = null;
            ddl_maintPlanId.Text = "";
            txt_Date.Text = "";
            txt_Notes.Text = "";
            txt_LaborSum.Text = "";
            txt_SparSum.Text = "";
            txt_TotalSum.Text = "";
            dt = (DataTable)ViewState["Spares"];
            dt.Rows.Clear();
            dt.Rows.Add(dt.NewRow());
            grd_PeriodicalPlanExecDetail.DataSource = dt;
            grd_PeriodicalPlanExecDetail.DataBind();
            grd_PeriodicalPlanExecDetail.Rows[0].Visible = false;
            ViewState["Spares"] = dt;
        }
        #endregion

        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            permissions(dc);
            Page.Title = "الاجراء الدوري";
            if (!IsPostBack)
            {
                FillVehcles();
                FillPlans();
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
                DataColumn dc10 = new DataColumn("maintPlanDetailId");
                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);
                dt.Columns.Add(dc3);
                dt.Columns.Add(dc4);
                dt.Columns.Add(dc5);
                dt.Columns.Add(dc6);
                dt.Columns.Add(dc7);
                dt.Columns.Add(dc8);
                dt.Columns.Add(dc9);
                dt.Columns.Add(dc10);
                if (dt.Rows.Count == 0) dt.Rows.Add(dt.NewRow());
                ViewState["Spares"] = dt;
                grd_PeriodicalPlanExecDetail.DataSource = ViewState["Spares"];
                grd_PeriodicalPlanExecDetail.DataBind();
                grd_PeriodicalPlanExecDetail.Rows[0].Visible = false;

                #endregion
            }
            var query = (from p in dc.GetBillId() select new { p.BillNO }).SingleOrDefault();
            txt_NextID.Text = query.BillNO;
            FillGridPlans();
        }
        protected void grd_SparParts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[0].Text;
                foreach (Button button in e.Row.Cells[9].Controls.OfType<Button>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('هل تريد الحذف " + item + "?')){ return false; };";
                    }
                }
            }
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


                if (ddl_maintPlanId.SelectedItem != null)
                {
                    ASPxComboBox ddl_maintPlanDetailId = (e.Row.FindControl("ddl_maintPlanDetailId") as ASPxComboBox);
                    ddl_maintPlanDetailId.DataSource = from p in dc.PeriodicPlanDetails where p.maintPlanId == int.Parse(ddl_maintPlanId.SelectedItem.Value.ToString()) select new { p.maintPlanDetailId, p.ServicesSetting.ServiceName };
                    //
                    ddl_maintPlanDetailId.TextField = "ServiceName";
                    ddl_maintPlanDetailId.ValueField = "maintPlanDetailId";
                    ddl_maintPlanDetailId.DataBind();

                    DataRowView drp = e.Row.DataItem as DataRowView;
                    ddl_maintPlanDetailId.Value = drp["maintPlanDetailId"].ToString();
                }


                try
                {
                    if (ddl_VehcleId.SelectedItem != null)
                    {

                        var classid = (from p in dc.Vehcles where p.VehcleId == int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()) select new { p.ClassId }).SingleOrDefault();
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
                // Find the product drop-down list, you can id (or cell number)
                var ddl_SpareMainId = e.Row.FindControl("ddl_SpareMainId") as ASPxComboBox;
                // var ddlProducts = e.Row.Cells[0].Controls[0]; // Finding by cell number and index
                if (null != ddl_SpareMainId)
                {
                    ddl_SpareMainId.DataSource = from p in dc.ConfigDetails where p.MasterId == 1 select new { p.ConfigDetailId, p.ConfigDetailName };
                    ddl_SpareMainId.TextField = "ConfigDetailName";
                    ddl_SpareMainId.ValueField = "ConfigDetailId";
                    ddl_SpareMainId.DataBind();
                }


                var ddl_EmployeeId = e.Row.FindControl("ddl_EmployeeId") as ASPxComboBox;
                // var ddlProducts = e.Row.Cells[0].Controls[0]; // Finding by cell number and index
                if (null != ddl_EmployeeId)
                {
                    ddl_EmployeeId.DataSource = from p in dc.Employees where p.IsTecnician.Equals(1) select new { p.EmployeeId, p.EmployeeName };
                    ddl_EmployeeId.TextField = "EmployeeName";
                    ddl_EmployeeId.ValueField = "EmployeeId";
                    ddl_EmployeeId.DataBind();
                }




                var ddl_maintPlanDetailId = e.Row.FindControl("ddl_maintPlanDetailId") as ASPxComboBox;
                // var ddlProducts = e.Row.Cells[0].Controls[0]; // Finding by cell number and index
                if (null != ddl_maintPlanDetailId)
                {
                    if (ddl_maintPlanId.SelectedItem != null)
                    {
                        ddl_maintPlanDetailId.DataSource = from p in dc.PeriodicPlanDetails where p.maintPlanId == int.Parse(ddl_maintPlanId.SelectedItem.Value.ToString()) select new { p.maintPlanDetailId, p.ServicesSetting.ServiceName };
                        //
                        ddl_maintPlanDetailId.TextField = "ServiceName";
                        ddl_maintPlanDetailId.ValueField = "maintPlanDetailId";
                        ddl_maintPlanDetailId.DataBind();
                    }
                }




            }
        }
        protected void ddl_SpareMainId_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGridSpareParts();

        }
        protected void ddl_SpareId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASPxComboBox ddl_SpareId = grd_PeriodicalPlanExecDetail.FooterRow.FindControl("ddl_SpareIdAdd") as ASPxComboBox;
            TextBox txt_SparCost = grd_PeriodicalPlanExecDetail.FooterRow.FindControl("txt_SparCost") as TextBox;
            TextBox txt_LaborCost = grd_PeriodicalPlanExecDetail.FooterRow.FindControl("txt_LaborCost") as TextBox;
            TextBox txt_Notes = grd_PeriodicalPlanExecDetail.FooterRow.FindControl("txt_Notes") as TextBox;
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

            if (ddl_SpareId.SelectedItem != null && ddl_VehcleId.SelectedItem != null)
            {
                var query = (from p in dc.vehclesConsumedSparesVws where p.SparId == int.Parse(ddl_SpareId.SelectedItem.Value.ToString()) && p.VehcleId == int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()) select new { p.Date }).ToList().LastOrDefault();
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
        protected void txt_LaborCost_TextChanged(object sender, EventArgs e)
        {
            CalSparTotal();
        }
        protected void txt_SparCost_TextChanged(object sender, EventArgs e)
        {
            CalSparTotal();
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ASPxComboBox ddl_SpareMainId = (ASPxComboBox)grd_PeriodicalPlanExecDetail.FooterRow.FindControl("ddl_SpareMainId");
            ASPxComboBox ddl_SpareId = (ASPxComboBox)grd_PeriodicalPlanExecDetail.FooterRow.FindControl("ddl_SpareIdAdd");
            ASPxComboBox ddl_maintPlanDetailId = (ASPxComboBox)grd_PeriodicalPlanExecDetail.FooterRow.FindControl("ddl_maintPlanDetailId");
            TextBox txt_SparCost = (TextBox)grd_PeriodicalPlanExecDetail.FooterRow.FindControl("txt_SparCost");
            TextBox txt_SparCount = (TextBox)grd_PeriodicalPlanExecDetail.FooterRow.FindControl("txt_SparCount");
            TextBox txt_SparTotal = (TextBox)grd_PeriodicalPlanExecDetail.FooterRow.FindControl("txt_SparTotal");
            TextBox txt_LaborCost = (TextBox)grd_PeriodicalPlanExecDetail.FooterRow.FindControl("txt_LaborCost");
            TextBox txt_TotalCost = (TextBox)grd_PeriodicalPlanExecDetail.FooterRow.FindControl("txt_TotalCost");
            TextBox txt_Notes = (TextBox)grd_PeriodicalPlanExecDetail.FooterRow.FindControl("txt_Notes");
            ASPxComboBox ddl_EmployeeId = (ASPxComboBox)grd_PeriodicalPlanExecDetail.FooterRow.FindControl("ddl_EmployeeId");
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
            if (ddl_maintPlanDetailId.SelectedItem == null)
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل اسم العمل";
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
            dr1[9] = ddl_maintPlanDetailId.SelectedItem.Value;
            dt.Rows.Add(dr1);

            grd_PeriodicalPlanExecDetail.DataSource = dt;
            grd_PeriodicalPlanExecDetail.DataBind();
            ViewState["Spares"] = dt;
            grd_PeriodicalPlanExecDetail.Rows[0].Visible = false;


            //Calculate Sum and display in Footer Row

            //decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>(txt_SparTotal.Text ));
            //grd_SparParts.FooterRow.Cells[1].Text = "Total";
            //grd_SparParts.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            //grd_SparParts.FooterRow.Cells[4].Text = total.ToString("N2");

            CalcTotals();
        }
        protected void btn_deleteFromGrid_Click(object sender, EventArgs e)
        {

        }
        protected void grd_SparParts_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }
        protected void grd_SparParts_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                grd_PeriodicalPlanExecDetail.DataSource = ViewState["Spares"];
                grd_PeriodicalPlanExecDetail.DataBind();
                grd_PeriodicalPlanExecDetail.Rows[0].Visible = false;
                CalcTotals();
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
            int result = 0;
            #region Validation
            if (ddl_VehcleId.SelectedItem == null)
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل رقم اللوحة";
                return;
            }

            if (ddl_maintPlanId.SelectedItem == null)
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل خطة الصيانة";
                return;
            }
            if (grd_PeriodicalPlanExecDetail.Rows.Count <= 1)
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل الاجراءات التي تمت في خطة الصيانة";
                return;

            }
            #endregion
            if (txt_Date.Text == "")
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل التاريخ";
                return;
            }



            #region save

            DAL.Bus.PeriodicalPlanExecution _PeriodicalPlanExecution = new BusesWorkshop.DAL.Bus.PeriodicalPlanExecution();
            _PeriodicalPlanExecution.BillNo = txt_NextID.Text;
            _PeriodicalPlanExecution.maintPlanId = int.Parse(ddl_maintPlanId.SelectedItem.Value.ToString());
            _PeriodicalPlanExecution.VehcleId = int.Parse(ddl_VehcleId.SelectedItem.Value.ToString());
            _PeriodicalPlanExecution.Date = DateTime.Parse(txt_Date.Text);
            _PeriodicalPlanExecution.Notes = txt_Notes.Text;
            _PeriodicalPlanExecution.CounterReading = int.Parse(txt_CurrentReading.Text);
            dc.PeriodicalPlanExecutions.InsertOnSubmit(_PeriodicalPlanExecution);
            dc.SubmitChanges();
            result = _PeriodicalPlanExecution.PeriodicalPlanExcutionId;



            #region SaveProcedures
            for (int i = 0; i < grd_PeriodicalPlanExecDetail.Rows.Count; i++)
            {
                ASPxComboBox ddl_SpareId = (ASPxComboBox)grd_PeriodicalPlanExecDetail.Rows[i].FindControl("ddl_SpareId");
                ASPxComboBox ddl_maintPlanDetailId = (ASPxComboBox)grd_PeriodicalPlanExecDetail.Rows[i].FindControl("ddl_maintPlanDetailId");
                TextBox txt_SparCost = (TextBox)grd_PeriodicalPlanExecDetail.Rows[i].FindControl("txt_SparCost");
                TextBox txt_SparCount = (TextBox)grd_PeriodicalPlanExecDetail.Rows[i].FindControl("txt_SparCount");
                TextBox txt_SparTotal = (TextBox)grd_PeriodicalPlanExecDetail.Rows[i].FindControl("txt_SparTotal");
                TextBox txt_LaborCost = (TextBox)grd_PeriodicalPlanExecDetail.Rows[i].FindControl("txt_LaborCost");
                TextBox txt_TotalCost = (TextBox)grd_PeriodicalPlanExecDetail.Rows[i].FindControl("txt_TotalCost");
                ASPxComboBox ddl_EmployeeId = (ASPxComboBox)grd_PeriodicalPlanExecDetail.Rows[i].FindControl("ddl_EmployeeId");




                if (ddl_maintPlanDetailId.SelectedItem != null)
                {
                    PeriodicalPlanExecDetail _PeriodicalPlanExecDetail = new PeriodicalPlanExecDetail();
                    _PeriodicalPlanExecDetail.PeriodicalPlanExcutionId = result;
                    _PeriodicalPlanExecDetail.SparId = int.Parse(ddl_SpareId.SelectedItem.Value.ToString());
                    _PeriodicalPlanExecDetail.maintPlanDetailId = int.Parse(ddl_maintPlanDetailId.SelectedItem.Value.ToString());
                    _PeriodicalPlanExecDetail.SparCost = decimal.Parse(txt_SparCost.Text);
                    _PeriodicalPlanExecDetail.SparCount = decimal.Parse(txt_SparCount.Text);
                    _PeriodicalPlanExecDetail.SparTotal = decimal.Parse(txt_SparTotal.Text);
                    _PeriodicalPlanExecDetail.LaborCost = decimal.Parse(txt_LaborCost.Text);
                    _PeriodicalPlanExecDetail.TotalCost = decimal.Parse(txt_TotalCost.Text);
                    _PeriodicalPlanExecDetail.EmployeeId = int.Parse(ddl_EmployeeId.SelectedItem.Value.ToString());

                    dc.PeriodicalPlanExecDetails.InsertOnSubmit(_PeriodicalPlanExecDetail);
                    dc.SubmitChanges();
                }
            }




            #endregion

            #endregion
            #region DELETE ALARMS
            dc.AlarmsPlanDelete(int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()), int.Parse(ddl_maintPlanId.SelectedItem.Value.ToString()), DateTime.Parse(txt_Date.Text));

            #endregion
            if (result > 0)
            {
                divMsg2.Attributes["class"] = "alert alert-success text-right";
                lblResult.Text = "تم الحفظ بنجاح";

            }

            EmptyControls();
        }
        protected void ddl_VehcleId_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = (DataTable)ViewState["Spares"];
            dt.Rows.Clear();
            dt.Rows.Add(dt.NewRow());
            grd_PeriodicalPlanExecDetail.DataSource = dt;
            grd_PeriodicalPlanExecDetail.DataBind();
            grd_PeriodicalPlanExecDetail.Rows[0].Visible = false;
            ViewState["Spares"] = dt;


            FillPlans();
            FillGridPlans();
        }
        protected void ddl_maintPlanId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_VehcleId.SelectedItem == null)
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل رقم اللوحة اولا";
                return;

            }

            dt = (DataTable)ViewState["Spares"];
            dt.Rows.Clear();
            dt.Rows.Add(dt.NewRow());
            grd_PeriodicalPlanExecDetail.DataSource = dt;
            grd_PeriodicalPlanExecDetail.DataBind();
            grd_PeriodicalPlanExecDetail.Rows[0].Visible = false;
            ViewState["Spares"] = dt;




        }
        protected void ddl_maintPlanDetailId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_VehcleId.SelectedItem == null)
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل رقم اللوحة اولا";
                return;

            }

            dt = (DataTable)ViewState["Spares"];
            dt.Rows.Clear();
            dt.Rows.Add(dt.NewRow());
            grd_PeriodicalPlanExecDetail.DataSource = dt;
            grd_PeriodicalPlanExecDetail.DataBind();
            grd_PeriodicalPlanExecDetail.Rows[0].Visible = false;
            ViewState["Spares"] = dt;

        }
        protected void ddl_VehcleId_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddl_VehcleId.SelectedItem != null)
            {
                dt = (DataTable)ViewState["Spares"];
                dt.Rows.Clear();
                dt.Rows.Add(dt.NewRow());
                grd_PeriodicalPlanExecDetail.DataSource = dt;
                grd_PeriodicalPlanExecDetail.DataBind();
                grd_PeriodicalPlanExecDetail.Rows[0].Visible = false;
                ViewState["Spares"] = dt;

                var query = (from p in dc.Vehcles where p.VehcleId == int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()) select new { p.CurrentReading }).SingleOrDefault();
                txt_CurrentReading.Text = query.CurrentReading.ToString();



                FillPlans();
            }
        }
        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;

            PeriodicalPlanExcutionID = Convert.ToInt32(gvPlans.DataKeys[row.RowIndex].Value);

            var query = (from p in dc.PeriodicalPlanExecutions where p.PeriodicalPlanExcutionId.Equals(PeriodicalPlanExcutionID) select new { p.BillNo, p.VehcleId, p.maintPlanId, p.Date, p.Notes }).SingleOrDefault();

            txt_NextID.Text = query.BillNo;
            ddl_VehcleId.Value = query.VehcleId.ToString();
            ddl_maintPlanId.Value = query.maintPlanId.ToString();
            txt_Date.Text = query.Date.ToString();
            txt_Notes.Text = query.Notes;


            #region FillPlanGrid
            var queryDet = from p in dc.PeriodicalPlanExecDetails where p.PeriodicalPlanExcutionId == PeriodicalPlanExcutionID select new { p.PeriodicalPlanExecDetailId, p.maintPlanDetailId, SpareMainId = p.SparePart.MainCategory, p.SparId, p.SparCost, p.SparCount, p.SparTotal, p.EmployeeId, p.LaborCost, p.TotalCost };
            dt = queryDet.CopyToDataTable();


            if (dt.Rows.Count > 0)
            {
                ViewState["Spares"] = dt;
                grd_PeriodicalPlanExecDetail.DataSource = ViewState["Spares"];
                grd_PeriodicalPlanExecDetail.DataBind();

            }
            else
            {
                dt = (DataTable)ViewState["Spares"];
                dt.Rows.Clear();
                dt.Rows.Add(dt.NewRow());
                grd_PeriodicalPlanExecDetail.DataSource = dt;
                grd_PeriodicalPlanExecDetail.DataBind();
                grd_PeriodicalPlanExecDetail.Rows[0].Visible = false;
                ViewState["Spares"] = dt;
            }
            #endregion
            CalcTotals();







        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.PeriodicalPlanExecution.GetHashCode());
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

            if (ddl_VehcleId.SelectedItem == null)
            {
                pnl_Alert.Attributes["class"] = "alert alert-danger text-right";
                lbl_SparAlert.Text = "عفو ادخال رقم اللوحة";
                return;
            }

            ASPxComboBox ddl_SpareMainId = grd_PeriodicalPlanExecDetail.FooterRow.FindControl("ddl_SpareMainId") as ASPxComboBox;
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
            var VehcleQuery = (from p in dc.Vehcles where p.VehcleId == int.Parse(ddl_VehcleId.SelectedItem.Value.ToString()) select new { p.ClassId }).SingleOrDefault();



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
                FillGridSpareParts();
            }


        }
        #endregion
    }

}
