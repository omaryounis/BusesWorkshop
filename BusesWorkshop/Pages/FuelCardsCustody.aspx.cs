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
using BusesWorkshop.DAL;
namespace BusesWorkshop.Pages
{
    public partial class FuelCardsCustody : System.Web.UI.Page
    {
        #region "Properties"
        WorkshopDataContext dc = new WorkshopDataContext();
        private int CustodyID
        {
            get
            {
                if (ViewState["_CustodyID"] != null)
                {
                    return Convert.ToInt32(ViewState["_CustodyID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_CustodyID", value);
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
        private void FillCards()
        {
            var query = from p in dc.FuelCardsDefinitions select new { p.FuelCardId, p.CardName };

            ddl_FuelCardId.DataSource = query;
            ddl_FuelCardId.TextField = "CardName";
            ddl_FuelCardId.ValueField = "FuelCardId";
            ddl_FuelCardId.DataBind();


            ddl_FuelCardIdSearch.DataSource = query;
            ddl_FuelCardIdSearch.TextField = "CardName";
            ddl_FuelCardIdSearch.ValueField = "FuelCardId";
            ddl_FuelCardIdSearch.DataBind();
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

            var Query = from p in dc.Users where p.ID == id select new { p.ID, p.Name };
            ddl_EmployeeId.DataSource = Query;
            ddl_EmployeeId.TextField = "Name";
            ddl_EmployeeId.ValueField = "ID";
            ddl_EmployeeId.DataBind();

            ddl_EmployeeId.Value = id.ToString();

            ddl_EmployeeNameSearch.DataSource = Query;
            ddl_EmployeeNameSearch.TextField = "Name";
            ddl_EmployeeNameSearch.ValueField = "ID";
            ddl_EmployeeNameSearch.DataBind();
            ddl_EmployeeNameSearch.Value = id.ToString(); DataBind();
        }
        private void Calc()
        {
            txt_Total.Text = (decimal.Parse(string.IsNullOrEmpty(txt_Count.Text) ? "0" : txt_Count.Text) * decimal.Parse(string.IsNullOrEmpty(txt_Value.Text) ? "0" : txt_Value.Text)).ToString();
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.FuelCardsCustody.GetHashCode());
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
        private void FillCustodyGrid()
        {
            int? EmployeeId = null;
            int? FuelCardId = null;
            DateTime? FromDate = null;
            DateTime? ToDate = null;


            if (ddl_EmployeeNameSearch.SelectedItem != null)
            {
                EmployeeId = int.Parse(ddl_EmployeeNameSearch.SelectedItem.Value.ToString());
            }

            if (ddl_FuelCardIdSearch.SelectedItem != null)
            {
                FuelCardId = int.Parse(ddl_FuelCardIdSearch.SelectedItem.Value.ToString());
            }

            if (Txt_FromDate.Text != "")
            {
                FromDate = DateTime.Parse(Txt_FromDate.Text);
            }

            if (Txt_ToDate.Text != "")
            {
                ToDate = DateTime.Parse(Txt_ToDate.Text);
            }
            //notfound in busesWoerkShopDB

            //var Query = dc.FuelCardsCustodySearch_Sp(EmployeeId, FuelCardId, FromDate, ToDate).ToList();
            //grd_Custodies.DataSource = Query;
            //grd_Custodies.DataBind();
        }

        #endregion

        #region  "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "عهدة كروت الوقود";
            permissions(dc);
            if (!IsPostBack)
            {
                FillMainSuperVisors();
                FillCards();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                return;
            }
            int result = 0;

            #region validation
            if (ddl_EmployeeId.SelectedItem == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل اسم مسئول الخدمة";
                return;
            }

            if (ddl_FuelCardId.SelectedItem == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل اسم كارت الوقود";
                return;
            }

            #endregion
            if (CustodyID == 0)
            {
                #region save
                DAL.Bus.FuelCardsCustody _FuelCardsCustody = new BusesWorkshop.DAL.Bus.FuelCardsCustody();

                _FuelCardsCustody.FuelCardId = int.Parse(ddl_FuelCardId.SelectedItem.Value.ToString());
                _FuelCardsCustody.EmployeeId = int.Parse(ddl_EmployeeId.SelectedItem.Value.ToString());
                _FuelCardsCustody.Date = DateTime.Parse(txt_Date.Text);
                _FuelCardsCustody.Count = int.Parse(txt_Count.Text);
                _FuelCardsCustody.Value = decimal.Parse(txt_Value.Text);
                _FuelCardsCustody.Total = decimal.Parse(txt_Total.Text);
                _FuelCardsCustody.Notes = txt_Notes.Text;
                dc.FuelCardsCustodies.InsertOnSubmit(_FuelCardsCustody);
                dc.SubmitChanges();
                result = _FuelCardsCustody.CustodyID;
                #endregion



                if (result > 0)
                {
                    divMsg.Attributes["class"] = "alert alert-success text-right";
                    lblResult.Text = "تم الحفظ بنجاح";
                    FillCustodyGrid();
                    CustodyID = 0;
                }
                else
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "حدث خطأ أثنا الحفظ";

                }
            }
            else
            {
                if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية التعديل";
                    return;
                }


                //الحصول على رصيد الكارت



                var FuelEmpQ = (from p in dc.FuelCardsCustodies where p.CustodyID == CustodyID select new { p.EmployeeId, p.FuelCardId, p.Count }).Single();



                var Quantities = (from p in dc.FuelCardsCustodies where p.EmployeeId == FuelEmpQ.EmployeeId && FuelEmpQ.FuelCardId == FuelEmpQ.FuelCardId select new { p.Count, p.CountOut }).ToList();


                int QIN = Quantities.Sum(x => x.Count) ?? 0;
                int QOUT = Quantities.Sum(x => x.CountOut) ?? 0;

                int Balance = QIN - QOUT;
                //التاكد ان الرصيد كافى
                if (Balance < FuelEmpQ.Count)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا تم سحب كروب من هذه العهدة ولا يمكن التعديل عليها ";
                    return;

                }




                DAL.Bus.FuelCardsCustody _FuelCardsCustody = dc.FuelCardsCustodies.Single(x => x.CustodyID == CustodyID);
                _FuelCardsCustody.FuelCardId = int.Parse(ddl_FuelCardId.SelectedItem.Value.ToString());
                _FuelCardsCustody.EmployeeId = int.Parse(ddl_EmployeeId.SelectedItem.Value.ToString());
                _FuelCardsCustody.Date = DateTime.Parse(txt_Date.Text);
                _FuelCardsCustody.Count = int.Parse(txt_Count.Text);
                _FuelCardsCustody.Value = decimal.Parse(txt_Value.Text);
                _FuelCardsCustody.Total = decimal.Parse(txt_Total.Text);
                _FuelCardsCustody.Notes = txt_Notes.Text;
                dc.SubmitChanges();
                result = _FuelCardsCustody.CustodyID;

                if (result > 0)
                {
                    divMsg.Attributes["class"] = "alert alert-success text-right";
                    lblResult.Text = "تم التعديل بنجاح";
                    FillCustodyGrid();
                    CustodyID = 0;

                }
                else
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "حدث خطأ أثنا التعديل";

                }
            }




            MyClasses.ClearControls(Page);


        }

        protected void txt_Count_TextChanged(object sender, EventArgs e)
        {
            Calc();
        }

        protected void txt_Value_TextChanged(object sender, EventArgs e)
        {
            Calc();
        }

        protected void ddl_FuelCardId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_FuelCardId.SelectedItem != null)
            {
                var query = (from p in dc.FuelCardsDefinitions where p.FuelCardId == int.Parse(ddl_FuelCardId.SelectedItem.Value.ToString()) select new { p.Price }).SingleOrDefault();
                txt_Value.Text = query.Price.ToString();

            }
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



            //الحصول على رصيد الكارت

            int CustodyId = Convert.ToInt32(grd_Custodies.DataKeys[row.RowIndex].Value);

            var FuelEmpQ = (from p in dc.FuelCardsCustodies where p.CustodyID == CustodyId select new { p.EmployeeId, p.FuelCardId, p.Count }).Single();



            var Quantities = (from p in dc.FuelCardsCustodies where p.EmployeeId == FuelEmpQ.EmployeeId && FuelEmpQ.FuelCardId == FuelEmpQ.FuelCardId select new { p.Count, p.CountOut }).ToList();


            int QIN = Quantities.Sum(x => x.Count) ?? 0;
            int QOUT = Quantities.Sum(x => x.CountOut) ?? 0;

            int Balance = QIN - QOUT;
            //التاكد ان الرصيد كافى
            if (Balance < FuelEmpQ.Count)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا تم سحب كروب من هذه العهدة ولا يمكن التعديل عليها ";
                return;

            }












            var query = from p in dc.FuelCardsCustodies where p.CustodyID.Equals(CustodyId) select p;
            try
            {
                foreach (var item in query)
                {
                    dc.FuelCardsCustodies.DeleteOnSubmit(item);
                }

                dc.SubmitChanges();

                FillCustodyGrid();
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


        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;

            CustodyID = Convert.ToInt32(grd_Custodies.DataKeys[row.RowIndex].Value);

            var query = (from p in dc.FuelCardsCustodies where p.CustodyID.Equals(CustodyID) select new { p.EmployeeId, p.FuelCardId, p.Date, p.Count, p.Value, p.Total, p.Notes }).SingleOrDefault();


            ddl_EmployeeId.Value = query.EmployeeId.ToString();
            ddl_FuelCardId.Value = query.FuelCardId.ToString();
            txt_Date.Text = query.Date.ToShortDateString();
            txt_Count.Text = query.Count.Value.ToString();
            txt_Value.Text = query.Value.Value.ToString();
            txt_Total.Text = query.Total.Value.ToString();
            txt_Notes.Text = query.Notes;
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            FillCustodyGrid();
        }

        protected void grd_Custodies_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FillCustodyGrid();
            grd_Custodies.PageIndex = e.NewPageIndex;
            grd_Custodies.DataBind();
        }
        #endregion

    }
}
