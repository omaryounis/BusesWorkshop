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
using BusesWorkshop.DAL.Accounting;

namespace BusesWorkshop.Pages.Definitions
{
    public partial class ConfigurationPage : System.Web.UI.Page
    {
        WorkshopDataContext dc = new WorkshopDataContext();
        RealEstateDataContext dcReal = new RealEstateDataContext();

        private int ConfigDetailId
        {
            get
            {
                if (ViewState["_ConfigDetailId"] != null)
                {
                    return Convert.ToInt32(ViewState["_ConfigDetailId"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_ConfigDetailId", value);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "الملفات العامة";
            permissions(dc);
            if (!IsPostBack)
            {
                
                FillMasterDll();
                FillGrdDetails();
                FillAssetMaster();
            }
            
        }
        private void FillAssetMaster()
        {

            var d = (from c in dcReal.CATs
                    join a in dcReal.Assets
                    on c.CAT_ID equals a.Cat_Id
                    select new
                    { CAT_Name = c.CAT_Name, CAT_ID = c.CAT_ID }).Distinct();
            ddl_AssetMasterId.DataSource = d; //dcReal.CATs.ToList();
            ddl_AssetMasterId.TextField = "CAT_Name";
            ddl_AssetMasterId.ValueField = "CAT_ID";
            ddl_AssetMasterId.DataBind();

        }
        private string getSubAssetName(int? AstID)
        {
            if (AstID != null)
                if(dcReal.AstNames.FirstOrDefault(x => x.ID == AstID)!=null)
                return dcReal.AstNames.FirstOrDefault(x => x.ID == AstID).Name;
            return null;
        }
        private string getMasterAssetName(int? ID)
        {
            if (ID != null)
                return dcReal.CATs.FirstOrDefault(x => x.CAT_ID == ID).CAT_Name;
            return null;
        }
        private string getServiceRequest(int? ID)
        {
            if (ID != null) {
                if (ID == 0)
                    return "طلب صيانه";
                else if (ID == 1)
                    return "دعم فنى  ";
                else if (ID == 2)
                    return "الكل";
            }
            return null;
        }
        private void FillGrdDetails()
        {
            if (ddl_MasterId.Value  != null )
            {
                try
                {
                    gvConfig.DataSource = from p in dc.ConfigDetails where p.MasterId == int.Parse(ddl_MasterId.SelectedItem.Value.ToString())

                                          select new {
                                              p.ConfigDetailId,p.ConfigMaster.MasterName, p.ConfigDetailName,
                                              MasterAssetName= getMasterAssetName(p.MasterAssetId),
                                              SubAssetName=getSubAssetName(p.Ast_Id),
                                              ServiceRequest=getServiceRequest(p.RecType)
                                          };
                    gvConfig.DataBind();
                }
                catch 
                {
                    gvConfig.DataSource = null;
                    gvConfig.DataBind();
                }
            }
        }

        private void FillMasterDll()
        {
            ddl_MasterId.DataSource = from p in dc.ConfigMasters orderby p.OrderNo ascending select new { p.MasterId, p.MasterName };
            ddl_MasterId.TextField = "MasterName";
            ddl_MasterId.ValueField = "MasterId";
            ddl_MasterId.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;
            var SubAssetID = Convert.ToInt16(ddl_SubAssetId.SelectedItem.Value.ToString());
            int MasterId = int.Parse(ddl_MasterId.SelectedItem.Value.ToString());
            int RecType= int.Parse(RequesServiceId.SelectedItem.Value.ToString());
            string ConfigDetailName = txt_ConfigDetailName.Text.ToString();
            #region Validation
            try 
            {
                  if (ddl_MasterId.SelectedItem.Value == null )
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "ادخل التصنيف ";
                return;
            
            }
            }
            catch{
            divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "ادخل التصنيف ";
                return;
            }
          
      
            #endregion
            
            if (ConfigDetailId  > 0)
            {
                #region update
                if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية التعديل";
                    return;
                }
                
                ConfigDetail _ConfigDetail = dc.ConfigDetails.Single(configdetails => configdetails.ConfigDetailId == ConfigDetailId);
                //////////////////////////////////////////////
                var configDetailRecs = dc.ConfigDetails
                    .Where(x => x.Ast_Id == SubAssetID && x.MasterId == 18
                    && (x.RecType == RecType || RecType == 2)
                    && x.ConfigDetailName == ConfigDetailName
                    && x.ConfigDetailId != _ConfigDetail.ConfigDetailId

                  ).FirstOrDefault();
                if (configDetailRecs != null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "إسم العمل المطلوب مكرر مسبقا لنفس المنقول";
                    return;
                }

                var configDetail = dc.ConfigDetails
                   .Where(x => x.MasterId == 18
                   && (x.RecType == 1 || RecType == 2)
                   && x.ConfigDetailName == ConfigDetailName
                   && x.ConfigDetailId != _ConfigDetail.ConfigDetailId
                 ).FirstOrDefault();
                if (configDetail != null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "إسم العمل المطلوب مكرر مسبقا لنفس في طلب الصيانه";
                    return;
                }

                ///////////////////////////////////////////////////////////
                if (RequesServiceId.SelectedItem != null)
                {
                    _ConfigDetail.RecType = int.Parse(RequesServiceId.SelectedItem.Value.ToString());
                }
                else
                {
                    _ConfigDetail.RecType = null;
                }
                if (ddl_AssetMasterId.SelectedItem != null)
                {
                    _ConfigDetail.MasterAssetId = Convert.ToInt16(ddl_AssetMasterId.SelectedItem.Value.ToString());
                }
                else
                {
                    _ConfigDetail.MasterAssetId = null;

                }

                if (ddl_SubAssetId.SelectedItem != null)
                {
                    _ConfigDetail.Ast_Id = Convert.ToInt16(ddl_SubAssetId.SelectedItem.Value.ToString());
                }
                else
                {
                    _ConfigDetail.Ast_Id = null;

                }
                _ConfigDetail.MasterId  = int.Parse(ddl_MasterId.SelectedItem.Value.ToString());
                _ConfigDetail.ConfigDetailName = txt_ConfigDetailName.Text;
                dc.SubmitChanges();
                result = _ConfigDetail.ConfigDetailId;
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
                ConfigDetail _ConfigDetail = new ConfigDetail();
                if (RequesServiceId.SelectedItem != null)
                {
                    _ConfigDetail.RecType = int.Parse(RequesServiceId.SelectedItem.Value.ToString());
                }
                else
                {
                    _ConfigDetail.RecType = null;
                }
                if (ddl_AssetMasterId.SelectedItem != null)
                {
                    _ConfigDetail.MasterAssetId = Convert.ToInt16(ddl_AssetMasterId.SelectedItem.Value.ToString());
                }
                else
                {
                    _ConfigDetail.MasterAssetId = null;

                }

                if (ddl_SubAssetId.SelectedItem != null)
                {
                    _ConfigDetail.Ast_Id = Convert.ToInt16(ddl_SubAssetId.SelectedItem.Value.ToString());
                }
                else
                {
                    _ConfigDetail.Ast_Id = null;

                }

                var configDetailRecs = dc.ConfigDetails
                    .Where(x => x.Ast_Id == SubAssetID && x.MasterId == 18
                    && (x.RecType == RecType || RecType == 2)
                    && x.ConfigDetailName == ConfigDetailName


                  ).FirstOrDefault();
                if (configDetailRecs != null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "إسم العمل المطلوب مكرر مسبقا لنفس المنقول";
                    return;
                }

                var configDetail = dc.ConfigDetails
                   .Where(x => x.MasterId == 18
                   && (x.RecType == 1 || RecType == 2)
                   && x.ConfigDetailName == ConfigDetailName
                 ).FirstOrDefault();
                if (configDetail != null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "إسم العمل المطلوب مكرر مسبقا لنفس في طلب الصيانه";
                    return;
                }

                _ConfigDetail.MasterId = int.Parse(ddl_MasterId.SelectedItem.Value.ToString());
                _ConfigDetail.ConfigDetailName = txt_ConfigDetailName.Text;
                dc.ConfigDetails.InsertOnSubmit(_ConfigDetail);
                dc.SubmitChanges();
                result = _ConfigDetail.ConfigDetailId;
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
            FillGrdDetails();
            EmptyControls();
            
        }

        private void EmptyControls()
        {
            ddl_MasterId.Value = null;
            ddl_MasterId.Text = string.Empty;
            txt_ConfigDetailName.Text = string.Empty;
            ConfigDetailId = 0;
            ddl_AssetMasterId.Value = null;
            ddl_AssetMasterId.Text = string.Empty;
            ddl_SubAssetId.Value = null;
            ddl_SubAssetId.Text = string.Empty;
            RequesServiceId.Value = null;
            RequesServiceId.Text = string.Empty;
        }

        protected void ddl_MasterId_SelectedIndexChanged(object sender, EventArgs e)
        {
             
            txt_ConfigDetailName.Text = string.Empty;
            FillGrdDetails();
        }

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;

            ConfigDetailId = Convert.ToInt32(gvConfig.DataKeys[row.RowIndex].Value);

            var query = (from p in dc.ConfigDetails  where p.ConfigDetailId .Equals(ConfigDetailId) select new { p.ConfigDetailName , p.MasterId,p.RecType,p.Ast_Id ,p.MasterAssetId}).SingleOrDefault();
            ddl_MasterId.Value  = query.MasterId .ToString();
           txt_ConfigDetailName.Text  = query.ConfigDetailName ;
            RequesServiceId.Value = query.RecType;
            if (RequesServiceId.Value.ToString() == "0")
            {
                RequesServiceId.Text = "طلب صيانه";
            }else if (RequesServiceId.Value.ToString() == "1")
            {
                RequesServiceId.Text = "دعم فنى  ";
            }
            else
            {
                RequesServiceId.Text = "الكل";
            }
            ddl_AssetMasterId.Value = query.MasterAssetId;
            if (ddl_AssetMasterId.Value != null)
            {
                var AssetMasterID = int.Parse(ddl_AssetMasterId.Value.ToString());
                ddl_AssetMasterId.Text = dcReal.CATs.FirstOrDefault(x => x.CAT_ID == AssetMasterID).CAT_Name;
            }
            ddl_SubAssetId.Value = query.Ast_Id;
            if (ddl_SubAssetId.Value != null)
            {
                var SubAssetID = int.Parse(ddl_SubAssetId.Value.ToString());
                ddl_SubAssetId.Text = dcReal.AstNames.FirstOrDefault(x => x.ID == SubAssetID).Name;
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
            var query = from p in dc.ConfigDetails  where p.ConfigDetailId.Equals(Convert.ToInt32(gvConfig.DataKeys[row.RowIndex].Value)) select p;
            try
            {
                foreach (var item in query)
                {
                    dc.ConfigDetails.DeleteOnSubmit(item);
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
                FillGrdDetails();
        }

        private void FillSubAssetMaster(int MasterID)
        {
            var Assets = dcReal.Assets.FirstOrDefault(x => x.Cat_Id == MasterID);
            if (Assets != null)
            {
                ddl_SubAssetId.DataSource = from n in dcReal.AstNames.Where(x => x.ID == Assets.AST_ID)
                                            select new
                                            {
                                                n.ID,
                                                n.Name
                                            };

                ddl_SubAssetId.TextField = "Name";
                ddl_SubAssetId.ValueField = "ID";
                ddl_SubAssetId.DataBind();
            }
            else
            {
                ddl_SubAssetId.Value = string.Empty;
                ddl_SubAssetId.DataSource = null;
                ddl_SubAssetId.DataBind();
            }

        }
        protected void ddl_AssetMasterId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_SubAssetId.Value = string.Empty;
            ddl_SubAssetId.DataSource = null;
            ddl_SubAssetId.DataBind();
            FillSubAssetMaster(int.Parse(ddl_AssetMasterId.SelectedItem.Value.ToString()));
        }

        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.ConfigurationPage.GetHashCode());
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

       
    }
}
