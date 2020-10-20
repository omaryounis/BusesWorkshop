using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Group = BusesWorkshop.DAL.Group;
using BusesWorkshop.DAL.Bus;
using BusesWorkshop.DAL;
using System.Data;
using DevExpress.Web;

namespace BusesWorkshop.Pages
{
    public partial class BuildingPeriocalExec : System.Web.UI.Page
    {
        #region "Properties"
        DataTable dt;
        WorkshopDataContext dc = new WorkshopDataContext();
        private int BuildingMaintID
        {
            get
            {
                if (ViewState["BuildingMaintID"] != null)
                {
                    return Convert.ToInt32(ViewState["BuildingMaintID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("BuildingMaintID", value);
            }
        }
        #endregion

        #region "Methods"
        private void ClearGridTextBoxes()
        {
            TextBox txt_MaterialCost = grd_Materials.FooterRow.FindControl("txt_MaterialCost") as TextBox;
            TextBox txt_MaterialCount = grd_Materials.FooterRow.FindControl("txt_MaterialCount") as TextBox;
            TextBox txt_MaterialTotal = grd_Materials.FooterRow.FindControl("txt_MaterialTotal") as TextBox;
            TextBox txt_LaborCost = grd_Materials.FooterRow.FindControl("txt_LaborCost") as TextBox;
            TextBox txt_TotalCost = grd_Materials.FooterRow.FindControl("txt_TotalCost") as TextBox;
            txt_MaterialCost.Text = string.Empty;
            txt_MaterialCount.Text = string.Empty;
            txt_MaterialTotal.Text = string.Empty;
            txt_LaborCost.Text = string.Empty;
            txt_TotalCost.Text = string.Empty;
        }
        private void FillPlans()
        {
            ddl_PlanId.DataSource = from p in dc.PeriodicalPlanToBuildings where p.BuildingId == int.Parse(ddl_BuildingId.SelectedItem.Value.ToString()) select new { p.BuildingPlan.PlanId, p.BuildingPlan.PlanName };
            ddl_PlanId.TextField = "PlanName";
            ddl_PlanId.ValueField = "PlanId";
            ddl_PlanId.DataBind();
        }
        private void FillBuilding()
        {
            ddl_BuildingId.DataSource = from p in dc.Buidings where p.Company.UserCompanies.Any(x => x.UserID == int.Parse(Session["UserId"].ToString())) select new { p.BuildingId, p.BuildingName };
            ddl_BuildingId.TextField = "BuildingName";
            ddl_BuildingId.ValueField = "BuildingId";
            ddl_BuildingId.DataBind();
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.BuildingPeriocalExec.GetHashCode());
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
        private void CalSparTotal()
        {
            TextBox txt_MaterialCost = grd_Materials.FooterRow.FindControl("txt_MaterialCost") as TextBox;
            TextBox txt_MaterialCount = grd_Materials.FooterRow.FindControl("txt_MaterialCount") as TextBox;
            TextBox txt_MaterialTotal = grd_Materials.FooterRow.FindControl("txt_MaterialTotal") as TextBox;
            TextBox txt_LaborCost = grd_Materials.FooterRow.FindControl("txt_LaborCost") as TextBox;
            TextBox txt_TotalCost = grd_Materials.FooterRow.FindControl("txt_TotalCost") as TextBox;

            txt_MaterialTotal.Text = (decimal.Parse(string.IsNullOrEmpty(txt_MaterialCost.Text) ? "0" : txt_MaterialCost.Text) * decimal.Parse(string.IsNullOrEmpty(txt_MaterialCount.Text) ? "0" : txt_MaterialCount.Text)).ToString();
            txt_TotalCost.Text = (decimal.Parse(string.IsNullOrEmpty(txt_MaterialTotal.Text) ? "0" : txt_MaterialTotal.Text) + decimal.Parse(string.IsNullOrEmpty(txt_LaborCost.Text) ? "0" : txt_LaborCost.Text)).ToString();
        }

        private void CalcTotals()
        {
            decimal MaterialTotals = 0;
            decimal labourCosts = 0;
            decimal totalcost = 0;


            for (int i = 0; i < grd_Materials.Rows.Count; i++)
            {
                TextBox txt_MaterialTotalsItem = (TextBox)grd_Materials.Rows[i].FindControl("txt_MaterialTotal");
                TextBox txt_LaborCostItem = (TextBox)grd_Materials.Rows[i].FindControl("txt_LaborCost");
                TextBox txt_TotalCostItem = (TextBox)grd_Materials.Rows[i].FindControl("txt_TotalCost");
                MaterialTotals += decimal.Parse(string.IsNullOrEmpty(txt_MaterialTotalsItem.Text) ? "0" : txt_MaterialTotalsItem.Text);
                labourCosts += decimal.Parse(string.IsNullOrEmpty(txt_LaborCostItem.Text) ? "0" : txt_LaborCostItem.Text);
                totalcost += decimal.Parse(string.IsNullOrEmpty(txt_TotalCostItem.Text) ? "0" : txt_TotalCostItem.Text);



                txt_SparSum.Text = MaterialTotals.ToString();
                txt_LaborSum.Text = labourCosts.ToString();
                txt_TotalSum.Text = totalcost.ToString();
            }
        }
        #endregion

        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            permissions(dc);
            Page.Title = "اجراء الصيانة الدورية للمبانى";
            if (!IsPostBack)
            {

                FillBuilding();

                #region Material Table
                dt = new DataTable();
                DataColumn dc1 = new DataColumn("MaterialMainId");
                DataColumn dc2 = new DataColumn("MaterialId");
                DataColumn dc3 = new DataColumn("WorkId");
                DataColumn dc4 = new DataColumn("MaterialCost");
                DataColumn dc5 = new DataColumn("MaterialCount");
                DataColumn dc6 = new DataColumn("MaterialTotal");
                DataColumn dc7 = new DataColumn("LabourCost");
                DataColumn dc8 = new DataColumn("TotalCost");

                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);
                dt.Columns.Add(dc3);
                dt.Columns.Add(dc4);
                dt.Columns.Add(dc5);
                dt.Columns.Add(dc6);
                dt.Columns.Add(dc7);
                dt.Columns.Add(dc8);
                if (dt.Rows.Count == 0) dt.Rows.Add(dt.NewRow());
                ViewState["Materials"] = dt;
                grd_Materials.DataSource = ViewState["Materials"];
                grd_Materials.DataBind();
                grd_Materials.Rows[0].Visible = false;

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
            #region Validation
            if (ddl_BuildingId.SelectedItem == null)
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل اسم المبنى";
                return;
            }
            if (ddl_PlanId.SelectedItem == null)
            {

                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل اسم الخطة";
                return;
            }
            #endregion
            int Result = 0;


            DAL.Bus.BuildPerExecPlan _BuildingPerPlanExc = new DAL.Bus.BuildPerExecPlan();
            _BuildingPerPlanExc.BuildingId = int.Parse(ddl_BuildingId.SelectedItem.Value.ToString());
            _BuildingPerPlanExc.PlanId = int.Parse(ddl_PlanId.SelectedItem.Value.ToString());
            _BuildingPerPlanExc.Date = DateTime.Parse(txt_Date.Text);
            _BuildingPerPlanExc.Notes = txtNotes.Text;
            dc.BuildPerExecPlans.InsertOnSubmit(_BuildingPerPlanExc);
            dc.SubmitChanges();
            Result = _BuildingPerPlanExc.PerioicalPlanId;



            #region Insert MATERIALS

            for (int i = 0; i < grd_Materials.Rows.Count; i++)
            {
                ASPxComboBox ddl_MaterialId = (ASPxComboBox)grd_Materials.Rows[i].FindControl("ddl_MaterialId");
                ASPxComboBox ddl_WorkId = (ASPxComboBox)grd_Materials.Rows[i].FindControl("ddl_WorkId");

                TextBox txt_MaterialCost = (TextBox)grd_Materials.Rows[i].FindControl("txt_MaterialCost");
                TextBox txt_MaterialCount = (TextBox)grd_Materials.Rows[i].FindControl("txt_MaterialCount");
                TextBox txt_MaterialTotal = (TextBox)grd_Materials.Rows[i].FindControl("txt_MaterialTotal");
                TextBox txt_LaborCost = (TextBox)grd_Materials.Rows[i].FindControl("txt_LaborCost");
                TextBox txt_TotalCost = (TextBox)grd_Materials.Rows[i].FindControl("txt_TotalCost");
                TextBox txt_Notes = (TextBox)grd_Materials.Rows[i].FindControl("txt_Notes");



                if (txt_MaterialCost.Text == "" && txt_TotalCost.Text == "") goto fo;
                using (WorkshopDataContext ctx = new WorkshopDataContext())
                {
                    BuildPerExecPlanDetail _BuildPerExecPlanDetail = new BuildPerExecPlanDetail();
                    _BuildPerExecPlanDetail.PerioicalPlanId = Result;
                    _BuildPerExecPlanDetail.MaterialId = int.Parse(ddl_MaterialId.SelectedItem.Value.ToString());
                    _BuildPerExecPlanDetail.WorkId = int.Parse(ddl_WorkId.SelectedItem.Value.ToString());
                    _BuildPerExecPlanDetail.MaterialCost = decimal.Parse(txt_MaterialCost.Text);
                    _BuildPerExecPlanDetail.MaterialCount = decimal.Parse(txt_MaterialTotal.Text);
                    _BuildPerExecPlanDetail.MaterialTotal = decimal.Parse(txt_MaterialTotal.Text);
                    _BuildPerExecPlanDetail.LabourCost = decimal.Parse(txt_MaterialCost.Text);
                    _BuildPerExecPlanDetail.TotalCost = decimal.Parse(txt_TotalCost.Text);
                    ctx.BuildPerExecPlanDetails.InsertOnSubmit(_BuildPerExecPlanDetail);
                    ctx.SubmitChanges();

                }
                fo:;
            }
            if (Result > 0)
            {
                divMsg2.Attributes["class"] = "alert alert-success text-right";
                lblResult.Text = "تم الحفظ بنجاح";



            }
            else
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "حدث خطأ أثنا الحفظ";

            }
            MyClasses.ClearControls(Page);
            dt = (DataTable)ViewState["Materials"];
            dt.Rows.Clear();
            dt.Rows.Add(dt.NewRow());
            grd_Materials.DataSource = dt;
            grd_Materials.DataBind();
            grd_Materials.Rows[0].Visible = false;
            ViewState["Materials"] = dt;
            #endregion



        }

        protected void ddl_MaterialMainId_SelectedIndexChanged1(object sender, EventArgs e)
        {
            ASPxComboBox ddl_MaterialMainId = grd_Materials.FooterRow.FindControl("ddl_MaterialMainId") as ASPxComboBox;
            ASPxComboBox ddl_MaterialId = grd_Materials.FooterRow.FindControl("ddl_MaterialId") as ASPxComboBox;
            ClearGridTextBoxes();


            if (ddl_MaterialMainId.SelectedItem != null)
            {

                ddl_MaterialId.Items.Clear();
                ddl_MaterialId.Text = string.Empty;
                ddl_MaterialId.DataSource = from p in dc.Materials
                                            where p.CategoryId == int.Parse(ddl_MaterialMainId.SelectedItem.Value.ToString())
                                            select new { p.MaterialId, p.MaterialName };
                ddl_MaterialId.TextField = "MaterialName";
                ddl_MaterialId.ValueField = "MaterialId";
                ddl_MaterialId.DataBind();
            }
        }

        protected void grd_Materials_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                ASPxComboBox ddl_MaterialMainId = (e.Row.FindControl("ddl_MaterialMainId") as ASPxComboBox);
                ddl_MaterialMainId.DataSource = from p in dc.ConfigDetails where p.MasterId == 17 select new { p.ConfigDetailId, p.ConfigDetailName };
                ddl_MaterialMainId.TextField = "ConfigDetailName";
                ddl_MaterialMainId.ValueField = "ConfigDetailId";
                ddl_MaterialMainId.DataBind();

                DataRowView dr = e.Row.DataItem as DataRowView;
                ddl_MaterialMainId.Value = dr["MaterialMainId"].ToString();


                ASPxComboBox ddl_WorkId = (e.Row.FindControl("ddl_WorkId") as ASPxComboBox);
                ddl_WorkId.DataSource = from p in dc.ConfigDetails where p.MasterId == 18 select new { p.ConfigDetailId, p.ConfigDetailName };
                ddl_WorkId.TextField = "ConfigDetailName";
                ddl_WorkId.ValueField = "ConfigDetailId";
                ddl_WorkId.DataBind();

                DataRowView drWorkId = e.Row.DataItem as DataRowView;
                ddl_WorkId.Value = drWorkId["WorkId"].ToString();

                ASPxComboBox ddl_MaterialId = (e.Row.FindControl("ddl_MaterialId") as ASPxComboBox);
                ddl_MaterialId.DataSource = from p in dc.Materials select new { p.MaterialId, p.MaterialName };
                ddl_MaterialId.TextField = "MaterialName";
                ddl_MaterialId.ValueField = "MaterialId";
                ddl_MaterialId.DataBind();
                DataRowView dr1 = e.Row.DataItem as DataRowView;
                ddl_MaterialId.Value = dr1["MaterialId"].ToString();

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                // Find the product drop-down list, you can id (or cell number)
                var ddl_MaterialMainId = e.Row.FindControl("ddl_MaterialMainId") as ASPxComboBox;
                // var ddlProducts = e.Row.Cells[0].Controls[0]; // Finding by cell number and index
                if (null != ddl_MaterialMainId)
                {
                    ddl_MaterialMainId.DataSource = from p in dc.ConfigDetails where p.MasterId == 17 select new { p.ConfigDetailId, p.ConfigDetailName };
                    ddl_MaterialMainId.TextField = "ConfigDetailName";
                    ddl_MaterialMainId.ValueField = "ConfigDetailId";
                    ddl_MaterialMainId.DataBind();
                }


                var ddl_WorkId = e.Row.FindControl("ddl_WorkId") as ASPxComboBox;
                if (null != ddl_WorkId && ddl_PlanId.SelectedItem != null)
                {
                    ddl_WorkId.DataSource = from p in dc.BuildingPlanDetails where p.PlanId == int.Parse(ddl_PlanId.SelectedItem.Value.ToString()) select new { p.WorkId, p.ConfigDetail.ConfigDetailName };
                    ddl_WorkId.TextField = "ConfigDetailName";
                    ddl_WorkId.ValueField = "WorkId";
                    ddl_WorkId.DataBind();
                }


            }
        }

        protected void ddl_MaterialId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASPxComboBox ddl_MaterialId = grd_Materials.FooterRow.FindControl("ddl_MaterialId") as ASPxComboBox;
            TextBox txt_MaterialCost = grd_Materials.FooterRow.FindControl("txt_MaterialCost") as TextBox;
            TextBox txt_LaborCost = grd_Materials.FooterRow.FindControl("txt_LaborCost") as TextBox;
            TextBox txt_Notes = grd_Materials.FooterRow.FindControl("txt_Notes") as TextBox;
            ClearGridTextBoxes();
            try
            {
                if (ddl_MaterialId.SelectedItem.Value != null)
                {
                    txt_MaterialCost.Text = "";
                    txt_LaborCost.Text = "";
                    var query = (from p in dc.Materials where p.MaterialId == int.Parse(ddl_MaterialId.SelectedItem.Value.ToString()) select new { p.MaterialPrice }).SingleOrDefault();
                    txt_MaterialCost.Text = query.MaterialPrice.ToString();

                    CalSparTotal();
                }

            }
            catch
            {

            }
        }

        protected void txt_SparCost_TextChanged(object sender, EventArgs e)
        {
            CalSparTotal();
        }

        protected void txt_MaterialCount_TextChanged(object sender, EventArgs e)
        {
            CalSparTotal();
        }

        protected void txt_MaterialTotal_TextChanged(object sender, EventArgs e)
        {
            CalSparTotal();
        }

        protected void txt_LaborCost_TextChanged(object sender, EventArgs e)
        {
            CalSparTotal();
        }

        protected void ddl_BuildingId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_BuildingId.SelectedItem != null)
            {
                FillPlans();
            }
            dt = (DataTable)ViewState["Materials"];
            dt.Rows.Clear();
            dt.Rows.Add(dt.NewRow());
            grd_Materials.DataSource = dt;
            grd_Materials.DataBind();
            grd_Materials.Rows[0].Visible = false;
            ViewState["Materials"] = dt;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ASPxComboBox ddl_MaterialMainId = (ASPxComboBox)grd_Materials.FooterRow.FindControl("ddl_MaterialMainId");
            ASPxComboBox ddl_MaterialId = (ASPxComboBox)grd_Materials.FooterRow.FindControl("ddl_MaterialId");
            ASPxComboBox ddl_WorkId = (ASPxComboBox)grd_Materials.FooterRow.FindControl("ddl_WorkId");
            TextBox txt_MaterialCost = (TextBox)grd_Materials.FooterRow.FindControl("txt_MaterialCost");
            TextBox txt_MaterialCount = (TextBox)grd_Materials.FooterRow.FindControl("txt_MaterialCount");
            TextBox txt_MaterialTotal = (TextBox)grd_Materials.FooterRow.FindControl("txt_MaterialTotal");
            TextBox txt_LaborCost = (TextBox)grd_Materials.FooterRow.FindControl("txt_LaborCost");
            TextBox txt_TotalCost = (TextBox)grd_Materials.FooterRow.FindControl("txt_TotalCost");
            TextBox txt_Notes = (TextBox)grd_Materials.FooterRow.FindControl("txt_Notes");

            #region validation

            if (ddl_MaterialMainId.SelectedItem == null)
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "ادخل الصنف الرئيسي";
                return;

            }



            if (ddl_MaterialId.SelectedItem == null)
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "ادخل الصنف الفرعي";
                return;

            }




            if (decimal.Parse(string.IsNullOrEmpty(txt_TotalCost.Text) ? "0" : txt_TotalCost.Text) <= 0)
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا لا يمكن ادخل اجمالي التكلفة بصفر";
                return;
            }

            #endregion


            dt = (DataTable)ViewState["Materials"];
            DataRow dr1 = dt.NewRow();
            dr1[0] = ddl_MaterialMainId.SelectedItem.Value;
            dr1[1] = ddl_MaterialId.SelectedItem.Value;
            dr1[2] = ddl_WorkId.SelectedItem.Value;
            dr1[3] = txt_MaterialCost.Text;
            dr1[4] = txt_MaterialCount.Text;
            dr1[5] = txt_MaterialTotal.Text;
            dr1[6] = txt_LaborCost.Text;
            dr1[7] = txt_TotalCost.Text;


            dt.Rows.Add(dr1);

            grd_Materials.DataSource = dt;
            grd_Materials.DataBind();
            ViewState["Materials"] = dt;
            grd_Materials.Rows[0].Visible = false;



            CalcTotals();
        }

        protected void grd_Materials_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int index = gvr.RowIndex;
                DataTable dt = ViewState["Materials"] as DataTable;
                dt.Rows[index].Delete();
                ViewState["Materials"] = dt;
                grd_Materials.DataSource = ViewState["Materials"];
                grd_Materials.DataBind();
                grd_Materials.Rows[0].Visible = false;
                CalcTotals();
            }
        }

        protected void ddl_PlanId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_BuildingId.SelectedItem == null)
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل رقم اللوحة اولا";
                return;

            }

            dt = (DataTable)ViewState["Materials"];
            dt.Rows.Clear();
            dt.Rows.Add(dt.NewRow());
            grd_Materials.DataSource = dt;
            grd_Materials.DataBind();
            grd_Materials.Rows[0].Visible = false;
            ViewState["Materials"] = dt;
        }
        #endregion
    }
}
