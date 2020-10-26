using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using BusesWorkshop.DAL.Accounting;
using BusesWorkshop.DAL.Bus;
using BusesWorkshop.DAL;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Globalization;
using DevExpress.Utils.Extensions;
using System.Threading;

namespace BusesWorkshop.Pages
{
    public partial class MaintRequestTechnical : System.Web.UI.Page
    {
        #region "Properties" 
        DataTable dt;
        DataTable DtDisplay;
        DataTable dtPictures;
        
        RealEstateDataContext dcReal = new  RealEstateDataContext();
     WorkshopDataContext dcWorkShop = new WorkshopDataContext();
        private int EmpID
        {
            get
            {
                if (ViewState["EmpID"] != null)
                {
                    return ((int)ViewState["EmpID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["EmpID"] = value;
            }
        }
        #endregion
        #region "Methods"

        protected void ddl_BranchIdInAcc_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_SectionId.Value = string.Empty;
            ddl_SectionId.DataSource = null;
            ddl_SectionId.DataBind();

            ddl_FloorId.Value = string.Empty;
            ddl_FloorId.DataSource = null;
            ddl_FloorId.DataBind();

            ddl_RoomId.Value = string.Empty;
            ddl_RoomId.DataSource = null;
            ddl_RoomId.DataBind();

            ddl_AssetMasterId.Value = string.Empty;
            ddl_AssetMasterId.DataSource = null;
            ddl_AssetMasterId.DataBind();


            ddl_SubAssetId.Value = string.Empty;
            ddl_SubAssetId.DataSource = null;
            ddl_SubAssetId.DataBind();
            FillSections(int.Parse(ddl_BranchIdInAcc.SelectedItem.Value.ToString()));
        }


        private void Fillddl_WorkId(string RadioIsAssetValue) {
            if (RadioIsAssetValue == "0")
            {
                var AssetMasterId = Convert.ToInt16(ddl_AssetMasterId.SelectedItem.Value.ToString());
                var SubAssetId = Convert.ToInt16(ddl_SubAssetId.SelectedItem.Value.ToString());
                ddl_WorkId.DataSource = dcWorkShop.ConfigDetails.Where(x=>x.MasterId == 18
                                                                        && (x.RecType == 1 ||  x.RecType ==2)
                                                                        &&  x.MasterAssetId == AssetMasterId
                                                                       
                                                                        && x.Ast_Id == SubAssetId
                ).Select(x => new { x.ConfigDetailId, x.ConfigDetailName }).ToList();
            }
            else if (RadioIsAssetValue == "1")
            {
                ddl_WorkId.DataSource = dcWorkShop.ConfigDetails.Where(x => x.MasterId == 18 && x.RecType == 1 || x.RecType == 2
                ).Select(x => new { x.ConfigDetailId, x.ConfigDetailName }).ToList();
            }
            ddl_WorkId.TextField = "ConfigDetailName";
            ddl_WorkId.ValueField = "ConfigDetailId";
            ddl_WorkId.DataBind();
        }
        
       
        private void FillCompanies()
        {
            var userID = int.Parse(Session["UserID"].ToString());
            ddl_companyId.DataSource = (from c in dcWorkShop.Companies
                                        join uc in dcWorkShop.UserCompanies
                                        on c.ID equals uc.CompID
                                        where uc.UserID == userID
                                        select new
                                        {
                                            c.CompName,
                                            c.ID
                                        }).Distinct();
            ddl_companyId.TextField = "CompName";
            ddl_companyId.ValueField = "ID";
            ddl_companyId.DataBind();

            ddl_BranchIdInAcc.Value = string.Empty;
            ddl_BranchIdInAcc.DataSource = null;
            ddl_BranchIdInAcc.DataBind();

            ddl_AssetMasterId.Value = string.Empty;
            ddl_AssetMasterId.DataSource = null;
            ddl_AssetMasterId.DataBind();

            ddl_SubAssetId.Value = string.Empty;
            ddl_SubAssetId.DataSource = null;
            ddl_SubAssetId.DataBind();

            ddl_SectionId.Value = string.Empty;
            ddl_SectionId.DataSource = null;
            ddl_SectionId.DataBind();

            ddl_RoomId.Value = string.Empty;
            ddl_RoomId.DataSource = null;
            ddl_RoomId.DataBind();

            ddl_FloorId.Value = string.Empty;
            ddl_FloorId.DataSource = null;
            ddl_FloorId.DataBind();
        }
        
        private void FillSections(int? BranchIdInAcc)
        {
            ddl_SectionId.DataSource = dcReal.Ast_Locations.Where(x => x.ParentID == BranchIdInAcc)
                .Select(x => new { x.ID, x.LocationName });
            ddl_SectionId.TextField = "LocationName";
            ddl_SectionId.ValueField = "ID";
            ddl_SectionId.DataBind();
        }

        private void FillRoom(int FloorID)
        {
            ddl_RoomId.DataSource = dcReal.Ast_Locations.Where(x => x.ParentID == FloorID);
            ddl_RoomId.TextField = "LocationName";
            ddl_RoomId.ValueField = "ID";
            ddl_RoomId.DataBind();
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.MaintRequestTechnical.GetHashCode());
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
            
            permissions(dcWorkShop);
            Page.Title = "طلب الدعم الفني";
            if (!IsPostBack)
            {
               
                #region required job Table

                radio_IsAsset.SelectedIndex =-1;
                dt = new DataTable();
                DataColumn dc1 = new DataColumn("RequiredJob");
                DataColumn dc2 = new DataColumn("MobileId");
                DataColumn dc3 = new DataColumn("Description"); 
                DataColumn dc4 = new DataColumn("IsAsset");
                DataColumn dc5 = new DataColumn("RecommendationId"); 
                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);
                dt.Columns.Add(dc3);
                dt.Columns.Add(dc4);
                dt.Columns.Add(dc5); 

                if (dt.Rows.Count == 0) dt.Rows.Add(dt.NewRow());
                ViewState["RequiredJobs"] = dt;
                ////////////////////////////////////////////////////////////
                ///
                DtDisplay = new DataTable();
                DataColumn col1 = new DataColumn("RequiredWork");
                DataColumn col2 = new DataColumn("SubAssetName");
                DataColumn col3 = new DataColumn("Description");
                DataColumn col4 = new DataColumn("AssetOrNot");
                DataColumn col5 = new DataColumn("Recommendation");
                DataColumn col6 = new DataColumn("ddl_WorkId");
                DataColumn col7 = new DataColumn("ddl_SubAssetId");
                DtDisplay.Columns.Add(col1);
                DtDisplay.Columns.Add(col2);
                DtDisplay.Columns.Add(col3);
                DtDisplay.Columns.Add(col4);
                DtDisplay.Columns.Add(col5);
                DtDisplay.Columns.Add(col6);
                DtDisplay.Columns.Add(col7);
                if (DtDisplay.Rows.Count == 0) DtDisplay.Rows.Add(DtDisplay.NewRow());
                ViewState["WorkDetails"] = DtDisplay;
                /////////////////////////////////////////////////////////////////

                grd_WorksNeeded.DataSource = ViewState["WorkDetails"];
                grd_WorksNeeded.DataBind();
                grd_WorksNeeded.Rows[0].Visible = false;

                #endregion

                #region required job Table
                dtPictures = new DataTable();
                DataColumn Col1 = new DataColumn("PicturePath");
                DataColumn Col2 = new DataColumn("Description");


                dtPictures.Columns.Add(Col1);
                dtPictures.Columns.Add(Col2);


                if (dtPictures.Rows.Count == 0) dtPictures.Rows.Add(dtPictures.NewRow());
                ViewState["Pictures"] = dtPictures;
                grd_Pictures.DataSource = ViewState["Pictures"];
                grd_Pictures.DataBind();
                grd_Pictures.Rows[0].Visible = false;

                #endregion
                FillCompanies();
            }
            if (ViewState["EmpID"] == null)
            {
                EmpID = 1;
            }

        }
        protected void ddl_AssetMasterId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_AssetMasterId.SelectedItem != null)
            {

                ddl_SubAssetId.Value = string.Empty;
                ddl_SubAssetId.DataSource = null;
                ddl_SubAssetId.DataBind();
                FillSubAssetMaster(int.Parse(ddl_AssetMasterId.SelectedItem.Value.ToString()));
            }
        }

        protected void radio_IsAsset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radio_IsAsset.SelectedValue == "0")
                AssetRow.Visible = true;
            else { 
                AssetRow.Visible = false;
                ddl_AssetMasterId.SelectedItem = null;
                ddl_SubAssetId.SelectedItem = null;

            }

            if (ddl_AssetMasterId.SelectedItem!=null && radio_IsAsset.SelectedIndex==1)
            {
                divmsg3.Attributes["class"] = "alert alert-danger text-right";
                lblMsg3.Text = "عفوا يوجد منقول";
                return;
            }
            if (ddl_SubAssetId.SelectedItem != null && radio_IsAsset.SelectedIndex == 1)
            {
                divmsg3.Attributes["class"] = "alert alert-danger text-right";
                lblMsg3.Text = "عفوا يوجد منقول";
                return;
            }
            
            ddl_WorkId.Value = string.Empty;
            ddl_WorkId.DataSource = null;
            ddl_WorkId.DataBind();
            if(radio_IsAsset.SelectedItem.Value.ToString()=="1")
             Fillddl_WorkId(radio_IsAsset.SelectedItem.Value.ToString());
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
        protected void ddl_SubAssetMasterId_SelectedIndexChanged(object sender, EventArgs e)
        {

            Fillddl_WorkId(radio_IsAsset.SelectedItem.Value.ToString());
        }
        protected void btnAddWork_Click(object sender, EventArgs e)
        {
            #region validation
            if (ddl_WorkId.SelectedItem == null)
            {
                divmsg3.Attributes["class"] = "alert alert-danger text-right";
                lblMsg3.Text = "عفوا ادخل العمل";
                return;

            }
            if (ddl_SubAssetId.SelectedItem == null && radio_IsAsset.SelectedItem.Value== "0")
            {
                divmsg3.Attributes["class"] = "alert alert-danger text-right";
                lblMsg3.Text = "عفوا االمنقول المطلوب";
                return;

            }

            if (ddl_AssetMasterId.SelectedItem == null && radio_IsAsset.SelectedItem.Value == "0")
            {
                divmsg3.Attributes["class"] = "alert alert-danger text-right";
               
                lblMsg3.Text = "عفوا االمنقول المطلوب";
                return;

            }

            if ((ddl_AssetMasterId.SelectedItem != null ||
                ddl_SubAssetId.SelectedItem != null )
                && radio_IsAsset.SelectedItem.Value == "1")
            {
                divmsg3.Attributes["class"] = "alert alert-danger text-right";
                lblMsg3.Text = "عفوا لا يمكن الربط بمنقول";
                return;

            }

            Dictionary<int, string> ddlRecDic = GetRecData();

            #endregion
            dt = (DataTable)ViewState["RequiredJobs"];
            DataRow dr1 = dt.NewRow();
            dr1[0] = ddl_WorkId.SelectedItem.Value;
            dr1[1] = ddl_SubAssetId.SelectedItem !=null?ddl_SubAssetId.SelectedItem.Value:null;
            dr1[2] = txt_Description.Text;
            dr1[3] = radio_IsAsset.SelectedItem.Value;
            dr1[4] = ddlRecDic != null ? string.Join(",", ddlRecDic.Select(x => x.Key).ToArray()) : null ;  
            dt.Rows.Add(dr1);

            ViewState["RequiredJobs"] = dt;
            /////////////////////////////////////////////////////////
            DtDisplay = (DataTable)ViewState["WorkDetails"];
            DataRow dr = DtDisplay.NewRow();
            int WorkID = int.Parse(ddl_WorkId.SelectedItem.Value.ToString());
            int? SubAssetID = ddl_SubAssetId.SelectedItem != null ?
                int.Parse(ddl_SubAssetId.SelectedItem.Value.ToString()) 
                : 0;
            dr[0] =dcWorkShop.ConfigDetails.Where(x=>x.ConfigDetailId == WorkID).FirstOrDefault().ConfigDetailName;
            dr[1] = dcReal.AstNames.FirstOrDefault(x => x.ID == SubAssetID) != null?
                    dcReal.AstNames.FirstOrDefault(x=>x.ID == SubAssetID).Name:null;
            dr[2] = txt_Description.Text;
            dr[3] = radio_IsAsset.SelectedItem.Value.ToString()=="0"?"منقول":"غير منقول";
            dr[4] = ddlRecDic!=null? string.Join(",", ddlRecDic.Select(x => x.Value).ToArray()):null;
            dr[5] = WorkID;
            dr[6] = SubAssetID==0?null:SubAssetID;
            DtDisplay.Rows.Add(dr);
            grd_WorksNeeded.DataSource = DtDisplay;
            grd_WorksNeeded.DataBind();
            ViewState["WorkDetails"] = DtDisplay;
            grd_WorksNeeded.Rows[0].Visible = false;
            ///////////////////////////////////////////////
            ddl_WorkId.Text = string.Empty;
            ddl_WorkId.Value = null;
            ddl_WorkId.SelectedIndex = -1;
            ddl_WorkId.Items.Clear();
            ddl_SubAssetId.Text = string.Empty;
            ddl_SubAssetId.Value = null;
            ddl_SubAssetId.SelectedIndex = -1;
            ddl_SubAssetId.Items.Clear();
            ddl_AssetMasterId.Value = null;
            ddl_AssetMasterId.Text = string.Empty;
            ddl_AssetMasterId.SelectedIndex = -1;
            ddl_AssetMasterId.Items.Clear();
            txt_Description.Text = string.Empty;
            ASPxListBox LisBox = ASPxDropDownEdit1.FindControl("ddl_Rec") as ASPxListBox;
            ASPxDropDownEdit1.Text=null;
            LisBox.DataSource = null;
            LisBox.ValueField = null;
            LisBox.TextField = null;
            LisBox.SelectedIndex = -1;
            LisBox.Items.Clear();
            LisBox.DataBind();
            radio_IsAsset.SelectedIndex = -1;

        }

       

        protected void grd_WorksNeeded_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = ViewState["RequiredJobs"] as DataTable;
            dt.Rows[index].Delete();
            ViewState["RequiredJobs"] = dt;


            DataTable WorkDetailsDt = ViewState["WorkDetails"] as DataTable;
            WorkDetailsDt.Rows[index].Delete();
            ViewState["WorkDetails"] = WorkDetailsDt;


            grd_WorksNeeded.DataSource = ViewState["WorkDetails"];
            grd_WorksNeeded.DataBind();

        }
        protected void SavePicture_Click(object sender, EventArgs e)
        {
            string path = "";
            if (FU_Pic.HasFile == false)
            {
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "alert alert-danger text-right";
                lblMsg3.Text = "عفوا ادخل الصورة";
                return;

            }
            else if (FU_Pic.HasFile)
            {
                Guid GUIDNo = Guid.NewGuid();
                string uploadFileName = FU_Pic.FileName.ToString();
                string strtemp = GUIDNo + uploadFileName;
                FU_Pic.SaveAs(Server.MapPath("~/Images/maintRequestPictures/" + strtemp));
                path = "~/Images/maintRequestPictures/" + strtemp;

                divmsg3.Visible = false;
                lblMsg3.Text = string.Empty;
            }



            dtPictures = (DataTable)ViewState["Pictures"];
            DataRow dr1 = dtPictures.NewRow();
            dr1[0] = path;
            dr1[1] = txt_PicDescription.Text;

            dtPictures.Rows.Add(dr1);

            grd_Pictures.DataSource = dtPictures;
            grd_Pictures.DataBind();
            ViewState["Pictures"] = dtPictures;
            grd_Pictures.Rows[0].Visible = false;
            txt_PicDescription.Text = "";

        }
        protected void grd_Pictures_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            int index = Convert.ToInt32(e.RowIndex);
            DataTable dtPictures = ViewState["Pictures"] as DataTable;


            string field = dtPictures.Rows[index]["PicturePath"].ToString();
            if ((System.IO.File.Exists(Server.MapPath(field))))
            {
                System.IO.File.Delete(Server.MapPath(field));
            }


            dtPictures.Rows[index].Delete();
            ViewState["Pictures"] = dtPictures;
            grd_Pictures.DataSource = ViewState["Pictures"];
            grd_Pictures.DataBind();


            Image IMG = (Image)grd_WorksNeeded.Rows[0].FindControl("Image1");
            try
            {
                if (IMG.ImageUrl == null)
                {
                    grd_Pictures.Rows[0].Visible = false;
                }
            }
            catch
            {
                grd_Pictures.Rows[0].Visible = false;
            }

        }
        protected void Save_Click(object sender, EventArgs e)
        {
            dt = (DataTable)ViewState["RequiredJobs"];
            #region Validation
            if (dt.Rows.Count <= 1)
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult2.Text = "عفوا لا  بد من ادخال اعمال الصيانة المطلوبة";
                return;
            }

            if (String.IsNullOrEmpty(txt_RequestDate.Text))
            {
                divMsg2.Attributes["class"] = "alert alert-danger text-right";
                lblResult2.Text = "عفوا لا  بد من ادخال تاريخ الطلب";
                return;
            }
            #endregion
            int EmpId = int.Parse(Session["UserID"].ToString());
            int Maintreqid = 0;
            #region save MainReq
           BusesWorkshop.DAL.Bus.MaintRequest _MaintRequest = new BusesWorkshop.DAL.Bus.MaintRequest();

            _MaintRequest.CompanyId = int.Parse(ddl_companyId.SelectedItem.Value.ToString());
            _MaintRequest.LocationId = int.Parse(ddl_RoomId.SelectedItem.Value.ToString());
            _MaintRequest.RequestedEmpId = EmpId;
            _MaintRequest.RequestType = 1;
           
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            _MaintRequest.RequestDate = DateTime.Parse(txt_RequestDate.Text);
            _MaintRequest.Notes = txt_Notes.Text;
            dcWorkShop.MaintRequests.InsertOnSubmit(_MaintRequest);
            dcWorkShop.SubmitChanges();
            Maintreqid = _MaintRequest.MaintReqId;
            #endregion
            #region save Works

            if (dt.Rows.Count > 1)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["RequiredJob"].ToString() != string.Empty)
                    {
                        MaintReqDetail _MaintReqDetail = new MaintReqDetail();
                        _MaintReqDetail.MaintReqId = Maintreqid;
                        if(dt.Rows[i]["MobileId"]!=DBNull.Value)
                        _MaintReqDetail.MobileId = int.Parse(dt.Rows[i]["MobileId"].ToString());
                        _MaintReqDetail.WorkId = int.Parse(dt.Rows[i]["RequiredJob"].ToString());
                        _MaintReqDetail.IsAsset = dt.Rows[i]["IsAsset"].ToString()=="0"?true:false;
                        _MaintReqDetail.RecommendationId = dt.Rows[i]["RecommendationId"]!=null? dt.Rows[i]["RecommendationId"].ToString():null;
                        if (dt.Rows[i]["Description"] != null)
                        {
                            _MaintReqDetail.PicDescription = dt.Rows[i]["Description"].ToString();
                        }
                        else

                        {
                            _MaintReqDetail.PicDescription = null;
                        }
                        dcWorkShop.MaintReqDetails.InsertOnSubmit(_MaintReqDetail);
                        dcWorkShop.SubmitChanges();
                    }
                }




            }

            #endregion
            dtPictures = (DataTable)ViewState["Pictures"];
            #region SaveImages
            if (dtPictures.Rows.Count > 1)
            {
                for (int i = 0; i < dtPictures.Rows.Count; i++)
                {
                    if (dtPictures.Rows[i]["PicturePath"].ToString() != string.Empty)
                    {
                        MaintReqPicture _MaintReqPicture = new MaintReqPicture();
                        _MaintReqPicture.MaintReqId = Maintreqid;
                        _MaintReqPicture.PicturePath = dtPictures.Rows[i]["PicturePath"].ToString();
                        if (dtPictures.Rows[i]["Description"] != null)
                        {
                            _MaintReqPicture.Description = dtPictures.Rows[i]["Description"].ToString();
                        }
                        else
                        {
                            _MaintReqPicture.Description = null;
                        }
                        dcWorkShop.MaintReqPictures.InsertOnSubmit(_MaintReqPicture);
                        dcWorkShop.SubmitChanges();
                    }



                }

            }

            #endregion



            #region "SaveReqPhase"
            List<ReqPhase> currentApprovedPhases = dcWorkShop.ReqPhases.Where(c => c.Req_Id == Maintreqid).ToList();
            int? nextPhase = 0;

            if (currentApprovedPhases.Count == 0)
            {
                nextPhase = dcWorkShop.Phases.Where(x => x.Phase_Order == 1).FirstOrDefault().phases_Id;
            }


            else if (currentApprovedPhases.Count > 0)
            {
                var RequestPhase = dcWorkShop.ReqPhases.Where(RP => RP.Req_Id == Convert.ToInt32(Maintreqid)).LastOrDefault();
                if (RequestPhase != null)
                {
                    var NextPhaseOrder = RequestPhase.Phase.Phase_Order + 1;
                    var nextPhaseObj = dcWorkShop.Phases.Where(x => x.Phase_Order == NextPhaseOrder).FirstOrDefault();
                    if (nextPhaseObj != null)
                        nextPhase = nextPhaseObj.phases_Id;
                    else
                    {
                        nextPhase = dcWorkShop.ReqPhases
                            .Where(x => x.Req_Id == Convert.ToInt32(Maintreqid))
                            .OrderByDescending(x => x.ReqPhaseID).Select(rp => rp.Phase_Id).FirstOrDefault();
                    }
                }


            }
            ReqPhase ReqPhase = new ReqPhase();
            ReqPhase.Req_Id = Convert.ToInt32(Maintreqid);

            ReqPhase.User_Id = int.Parse(Session["UserID"].ToString());
            ReqPhase.StartDate = DateTime.Now;
            ReqPhase.EndDate = DateTime.Now;
            ReqPhase.Phase_Id = nextPhase;//Convert.ToInt32( ddl_Phases.SelectedItem.Value.ToString());
            dcWorkShop.ReqPhases.InsertOnSubmit(ReqPhase);
            dcWorkShop.SubmitChanges();

            #endregion


            divMsg2.Attributes["class"] = "alert alert-success text-right";
            lblResult2.Text = "تم الحفظ بنجاح";
            DAL.MyClasses.ClearControls(Page);
            dtPictures = (DataTable)ViewState["Pictures"];
            dtPictures.Rows.Clear();
            dtPictures.Rows.Add(dtPictures.NewRow());
            grd_Pictures.DataSource = dtPictures;
            grd_Pictures.DataBind();
            grd_Pictures.Rows[0].Visible = false;
            ViewState["Pictures"] = dtPictures;
            dt = (DataTable)ViewState["RequiredJobs"];
            dt.Rows.Clear();
            dt.Rows.Add(dt.NewRow());

            DtDisplay = (DataTable)ViewState["WorkDetails"];
            DtDisplay.Rows.Clear();
            DtDisplay.Rows.Add(DtDisplay.NewRow());

            grd_WorksNeeded.DataSource = DtDisplay;
            grd_WorksNeeded.DataBind();
            grd_WorksNeeded.Rows[0].Visible = false;
            ViewState["WorkDetails"] = DtDisplay;
            ViewState["RequiredJobs"] = dt;
        }

       
        protected void ddl_companyId_SelectedIndexChanged(object sender, EventArgs e)
        { 
            ddl_BranchIdInAcc.Value = string.Empty;
            ddl_BranchIdInAcc.DataSource = null;
            ddl_BranchIdInAcc.DataBind();

            ddl_SectionId.Value = string.Empty;
            ddl_SectionId.DataSource = null;
            ddl_SectionId.DataBind();


            ddl_FloorId.Value = string.Empty;
            ddl_FloorId.DataSource = null;
            ddl_FloorId.DataBind();

            ddl_RoomId.Value = string.Empty;
            ddl_RoomId.DataSource = null;
            ddl_RoomId.DataBind();


            ddl_AssetMasterId.Value = string.Empty;
            ddl_AssetMasterId.DataSource = null;
            ddl_AssetMasterId.DataBind();

            ddl_SubAssetId.Value = string.Empty;
            ddl_SubAssetId.DataSource = null;
            ddl_SubAssetId.DataBind();

            if (ddl_companyId.SelectedItem.Value != null)
            {

                int CompID = int.Parse(ddl_companyId.SelectedItem.Value.ToString());
                int? schoolid = dcWorkShop.Companies.Where(
                                                            c => c.ID == CompID
                                                            ).Select(c => c.SchoolID).FirstOrDefault();
                int? com_id = dcReal.Ast_Locations.Where(c => c.Com_ID == CompID).Select(c => c.Com_ID).FirstOrDefault();
                FillBranchIdInAcc(schoolid); 
            }
        }

        private void FillBranchIdInAcc(int? ComID)
        {
            ddl_BranchIdInAcc.DataSource
                = dcReal.Ast_Locations.
                          Where(ast_l => ast_l.Com_ID == ComID && ast_l.ParentID == null)
                          .Select(x => new { LocID = x.ID, LocationName = x.LocationName }).ToList();

            ddl_BranchIdInAcc.TextField = "LocationName";
            ddl_BranchIdInAcc.ValueField = "LocID";
            ddl_BranchIdInAcc.DataBind();
        }

        protected void ddl_SectionId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_FloorId.Value = string.Empty;
            ddl_FloorId.DataSource = null;
            ddl_FloorId.DataBind();

            ddl_RoomId.Value = string.Empty;
            ddl_RoomId.DataSource = null;
            ddl_RoomId.DataBind();

            FillFloors(int.Parse(ddl_SectionId.SelectedItem.Value.ToString()));
        }


        private void FillFloors(int SectionID)
        {
            ddl_FloorId.DataSource = dcReal.Ast_Locations.Where(x => x.ParentID == SectionID);
            ddl_FloorId.TextField = "LocationName";
            ddl_FloorId.ValueField = "ID";
            ddl_FloorId.DataBind();
        }


        protected void ddl_FloorId_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillRoom(int.Parse(ddl_FloorId.SelectedItem.Value.ToString()));
        }


      
        protected void ddl_WorkId_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillRecs(ddl_WorkId.SelectedItem.Value.ToString());
            
            btnAddWork.Enabled = true;

        }
        public Dictionary<int, string> GetRecData()
        {
            ASPxListBox lb = ASPxDropDownEdit1.FindControl("ddl_Rec") as ASPxListBox;
            Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
            for (var i = 0; i < lb.SelectedItems.Count; i++)
            {
                keyValuePairs.Add(int.Parse(lb.SelectedValues[i].ToString()), lb.SelectedItems[i].ToString());
            }
            return keyValuePairs;
        }

        protected void ddl_Rec_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASPxListBox LisBox = ASPxDropDownEdit1.FindControl("ddl_Rec") as ASPxListBox;
            List<int> dllworkIDList = new List<int>();
            List<string> dllworkIDStrList = new List<string>();
            foreach (var item in LisBox.SelectedValues)
            {
                dllworkIDList.Add(int.Parse(item.ToString()));
            }
            foreach (var item in LisBox.SelectedItems)
            {
                dllworkIDStrList.Add(item.ToString());
            }
        }
        private void FillRecs(string WorkId)
        {
            int ConfigDetailID = int.Parse(WorkId);
            var d = dcWorkShop.Recommendations.
                Where(x => x.ConfigDetailId == ConfigDetailID).
                Select(x => new { x.RecID, x.RecDesc }).ToList();
            ASPxListBox LisBox = ASPxDropDownEdit1.FindControl("ddl_Rec") as ASPxListBox;
            if (d.Count != 0)
            {

                LisBox.DataSource = d;
                LisBox.ValueField = "RecID";
                LisBox.TextField = "RecDesc";
                LisBox.DataBind();
                RecDiv.Visible = true;
            }
            else
            {

                LisBox.DataSource = null;
                LisBox.ValueField = null;
                LisBox.TextField = null;
                LisBox.DataBind();
                RecDiv.Visible = false;
            }
        }


        #endregion 
        protected void ddl_RoomId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_AssetMasterId.Value = string.Empty;
            ddl_AssetMasterId.DataSource = null;
            ddl_AssetMasterId.DataBind();
            FillAssetMaster();
        }

        private void FillAssetMaster()
        {

            var d = from c in dcReal.CATs
                    join a in dcReal.Assets
                    on c.CAT_ID equals a.Cat_Id
                    where a.LocationID == Convert.ToInt16(ddl_RoomId.SelectedItem.Value.ToString())
                    select new
                    { CAT_Name = c.CAT_Name, CAT_ID = c.CAT_ID };
            ddl_AssetMasterId.DataSource = d;
            ddl_AssetMasterId.TextField = "CAT_Name";
            ddl_AssetMasterId.ValueField = "CAT_ID";
            ddl_AssetMasterId.DataBind();

        }


        
    }
}
