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
using BusesWorkshop.DAL;

namespace BusesWorkshop.Pages
{
    public partial class FuelCardsDefinition : System.Web.UI.Page
    {
        private int FuelCardID
        {
            get
            {
                if (ViewState["_FuelCardID"] != null)
                {
                    return Convert.ToInt32(ViewState["_FuelCardID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_FuelCardID", value);
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
            Page.Title = "كروت الوقود";
            permissions(dc);
            if (!IsPostBack)
            {
                FillFuels();
            
            }
            FillGridCards();
        }

        private void FillFuels()
        {
            ddl_FuelId.DataSource = from p in dc.ConfigDetails where p.MasterId == 5 select new { p.ConfigDetailId, p.ConfigDetailName };
            ddl_FuelId.TextField = "ConfigDetailName";
            ddl_FuelId.ValueField = "ConfigDetailId";
            ddl_FuelId.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;
            #region validation 
            if (ddl_FuelId.SelectedItem == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل نوع الوقود";
                return;
            }
            
            #endregion

            if (FuelCardID > 0)
            {
                #region update 
                if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية التعديل";
                    return;
                }
                BusesWorkshop.DAL.Bus.FuelCardsDefinition _FuelCardsDefinition = dc.FuelCardsDefinitions.SingleOrDefault(x => x.FuelCardId == FuelCardID);
                _FuelCardsDefinition.FuelId = int.Parse(ddl_FuelId.SelectedItem.Value.ToString());
                _FuelCardsDefinition.CardName = txt_CardName.Text;
                _FuelCardsDefinition.Litres = int.Parse(txt_Litres.Text);
                _FuelCardsDefinition.Price = decimal.Parse(txt_Price.Text);
                _FuelCardsDefinition.Notes = txt_Notes.Text;
                dc.SubmitChanges();
                result = _FuelCardsDefinition.FuelCardId ; 
                #endregion
            }
            else
            {
                var query = (from p in dc.FuelCardsDefinitions where p.FuelId == int.Parse(ddl_FuelId.SelectedItem.Value.ToString()) && p.Litres == int.Parse(txt_Litres.Text) && p.Price == decimal.Parse(txt_Price.Text) select new { p.CardName }).ToList();
                if (query.Any())
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا الكارت مسجل مسبقا";
                    return;

                }
                #region save

                if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                    return;
                }
                BusesWorkshop.DAL.Bus.FuelCardsDefinition _FuelCardsDefinition = new BusesWorkshop.DAL.Bus.FuelCardsDefinition();
                _FuelCardsDefinition.FuelId = int.Parse(ddl_FuelId.SelectedItem.Value.ToString());
                _FuelCardsDefinition.CardName = txt_CardName.Text;
                _FuelCardsDefinition.Litres = int.Parse(txt_Litres.Text);
                _FuelCardsDefinition.Price = decimal.Parse(txt_Price.Text);
                _FuelCardsDefinition.Notes = txt_Notes.Text;
                dc.FuelCardsDefinitions.InsertOnSubmit(_FuelCardsDefinition);
                dc.SubmitChanges();
                result = _FuelCardsDefinition.FuelCardId; 

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

            FillGridCards();
            FuelCardID = 0;
            MyClasses.ClearControls(Page);
        }
        private void FillGridCards()
        {

            gvFuelCards.DataSource = from p in dc.FuelCardsDefinitions orderby p.FuelId, p.Litres ascending select new { p.FuelCardId, p.FuelId, FuelName = p.ConfigDetail.ConfigDetailName, p.CardName, p.Litres, p.Price };
            gvFuelCards.DataBind();
        }
        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;



            FuelCardID = Convert.ToInt32(gvFuelCards.DataKeys[row.RowIndex].Value);

            var query = (from p in dc.FuelCardsDefinitions  where p.FuelCardId .Equals(FuelCardID) select new { p.FuelId , p.CardName , p.Litres , p.Price , p.Notes  }).SingleOrDefault();

            ddl_FuelId.Value = query.FuelId.ToString();
            txt_CardName.Text = query.CardName;
            txt_Litres.Text = query.Litres.ToString();
            txt_Price.Text = query.Price.ToString();
            txt_Notes.Text = query.Notes;
            txt_LitrePrice.Text =(decimal.Parse(txt_Price.Text) / (decimal.Parse(txt_Litres.Text))).ToString() ;

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
            FuelCardID = int.Parse(gvFuelCards.DataKeys[row.RowIndex].Value.ToString());
            BusesWorkshop.DAL.Bus.FuelCardsDefinition _FuelCardsDefinition = dc.FuelCardsDefinitions.Single(x => x.FuelCardId == FuelCardID);

            try
            {
                dc.FuelCardsDefinitions.DeleteOnSubmit(_FuelCardsDefinition);
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
            FuelCardID = 0;
            FillGridCards();
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.FuelCardsDefinition.GetHashCode());
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

        protected void txt_LitrePrice_TextChanged(object sender, EventArgs e)
        {
            CalcCardPrice();
        }

        private void CalcCardPrice()
        {
            decimal litresCount = decimal.Parse(string.IsNullOrEmpty(txt_Litres.Text) ? "0" : txt_Litres.Text);
            decimal LitrePrice = decimal.Parse(string.IsNullOrEmpty(txt_LitrePrice.Text) ? "0" : txt_LitrePrice.Text);
            txt_Price.Text = (litresCount * LitrePrice).ToString();
        }

        protected void txt_Litres_TextChanged(object sender, EventArgs e)
        {
            CalcCardPrice();
        }
    }
}
