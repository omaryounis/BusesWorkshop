
//using System;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//using System.Net;
//using System.Text;
//using System.IO;
//using System.Web.Script.Serialization;

//using System.Collections.Generic;
//using DevExpress.Web;
//using BusesWorkshop.DAL.Bus;

//namespace HR.UserControls.BusesWorkShop
//{
//    public partial class MaintRequest : System.Web.UI.UserControl
//    {
//        #region "Properties"
//        DataTable dt;
//        DataTable dtPictures;
//        RealEstateDataContext dcReal = new RealEstateDataContext();
//        WorkshopDataContext dcWorkShop = new WorkshopDataContext();
//        HrDataContext Hrctx = new HrDataContext();
//        private int EmpID
//        {
//            get
//            {
//                if (ViewState["EmpID"] != null)
//                {
//                    return ((int)ViewState["EmpID"]);
//                }
//                else
//                {
//                    return 0;
//                }
//            }
//            set
//            {
//                ViewState["EmpID"] = value;
//            }
//        }
//        private int? userId
//        {
//            get
//            {
//                if (Session["UserID"] != null)
//                {
//                    return Convert.ToInt32(Session["UserID"].ToString());
//                }
//                else
//                {
//                    return null;
//                }
//            }
//            set
//            {
//                Session["UserID"] = value;
//            }
//        }

//        private int BranchID
//        {
//            get
//            {
//                if (ViewState["BranchID"] != null)
//                {
//                    return ((int)ViewState["BranchID"]);
//                }
//                else
//                {
//                    return 0;
//                }
//            }
//            set
//            {

//                ViewState["BranchID"] = value;

//            }

//        }
//        #endregion

//        #region "Methods"
       
//        private void fillddlmainddlSectionAccJobs()
//        {
//            //Ado Code
//            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AccountingModel"].ConnectionString);
//            if (con.State == ConnectionState.Open) con.Close();
//            con.Open();
//            SqlCommand cmd = new SqlCommand();
//            cmd.Connection = con;
//            cmd.CommandText = "select * from [dbo].[Ast_Location ] where  ISNULL([ParentID],0)= 0 " ;
//            DataTable dt = new DataTable();
//            dt.Load(cmd.ExecuteReader());
//            con.Close();
//            ddlmainddlSectionAccJobs.DataSource = dt;
//            ddlmainddlSectionAccJobs.TextField = "LocationName";
//            ddlmainddlSectionAccJobs.ValueField = "ID";
//            ddlmainddlSectionAccJobs.DataBind();

//        }
       
//        public void FillParentMobiles(ASPxPageControl EmployeeTabPage)
//        {
//            if (EmployeeTabPage.ActiveTabIndex == 0)
//            {
//                ddl_MobileParentId.DataSource = dcWorkShop.Sp_SelectMainMobile(0);
//            }
//            else if (EmployeeTabPage.ActiveTabIndex == 1)
//            {
//                ddl_MobileParentId.DataSource = dcWorkShop.Sp_SelectMainMobile(1);

//            }

//          // ddl_MobileParentId.DataSource = dcWorkShop.Sp_SelectParentMobile();
//            ddl_MobileParentId.TextField = "MobileName";
//            ddl_MobileParentId.ValueField = "MobileId";
//            ddl_MobileParentId.DataBind();
//        }
//        //added by asmaa
//        private void FillBranch()
//        {
//            ddl_BranchId.DataSource =  //_unitOfWork.BranchesRepository.GetBranchies();
//            ddl_BranchId.TextField = "BranchName";
//            ddl_BranchId.ValueField = "ID";
           
//            ddl_BranchId.DataBind();
//            if (Session["BranchID"] != null)
//            {


//                ddl_BranchId.Value = Session["BranchID"].ToString();
//            }
//        }
//        private void FillSection(int branchID)
//        {
//            var d = _unitOfWork.SectionsRepository.getSectionsByBranch(Convert.ToInt16(Session["SectionID"]));//(branchID);//_unitOfWork.SectionsRepository.GetSections();
//            if (d.Count > 0)
//            {
//                ddlSection.DataSource = d;
//                ddlSection.TextField = "Name";
//                ddlSection.ValueField = "ID";
//                ddlSection.DataBind();
//                ddlSection.Value = Session["SectionID"].ToString();
//            }

//            ddl_SectionId.DataSource = dcReal.AssetLocationsWithLevelsSelect_Vws.Where(x => x.level == 2 && x.ParentID == CompanyID);
//            ddl_SectionId.TextField = "LocationName";
//            ddl_SectionId.ValueField = "ID";
//            ddl_SectionId.DataBind();

//        }
//        private void fillSectionAccJobs(int branchID, int sectionID)
//        {



//            //ddl_AccBranchs.DataSource = _acc.COMs.Where(X => X.ParentID != null)..Select(x => new { x.COMID, x.COMName });
//            //ddl_AccBranchs.DataBind();
//            /*
//           var SectionAccJobsByBranchAndSection =
//               from SectionAccJobs in ctx.SectionAccJobs //on coms.COMID equals SectionAccJobs.SectionAccJobID 
//                    join branch in ctx.Branchies on SectionAccJobs.BranchID equals branch.ID
//               join section in ctx.Sections on SectionAccJobs.SectionID equals section.ID
//               where (SectionAccJobs.SectionID == sectionID && SectionAccJobs.BranchID == branchID)
//               select (
//      new { AccBranchID = SectionAccJobs.AccBranchID }
//           ).AccBranchID;

//           IEnumerable<DataRow> query =
//      from coms in dcReal.COMs
//      join SectionAccJobs in ctx.SectionAccJobs on coms.COMID equals SectionAccJobs.SectionAccJobID
//      where SectionAccJobs.AccBranchID == Convert.ToInt32(SectionAccJobsByBranchAndSection)//(SectionAccJobs.SectionID == sectionID && SectionAccJobs.BranchID == branchID)

//           select (
//      new IEnumerable<DataRow> { COMID == coms.COMID, COMName== coms.COMName }
//           );*/
//            /*   select* from SectionAccJob
//       join[HR_Sales].dbo.Branchies on SectionAccJob.BranchID = Branchies.ID
//       --join[HR_Sales].dbo.Sections on SectionAccJob.SectionID = Sections.ID
//       where AccBranchID in (select AccBranchID from[HR_Sales].dbo.SectionAccJob
//       where Branchies.ID = 20--and Sections.ID = 89
//       )*/
//            //Ado.Net Code
//            SqlConnection conAccountingModel = new SqlConnection(ConfigurationManager.ConnectionStrings["AccountingModel"].ConnectionString);
//            if (conAccountingModel.State == ConnectionState.Open) conAccountingModel.Close();
//            conAccountingModel.Open();
//            SqlCommand cmd = new SqlCommand();
//            cmd.Connection = conAccountingModel;
//            var coms = " select   LocationName,ID LocID,COMID from com ";


//            coms += " right join Ast_Location on ast_location.Com_ID = com.COMID";
//            coms += " where ID = " + ddlmainddlSectionAccJobs.SelectedItem.Value;
//            cmd.CommandText = coms;

//            DataTable dt = new DataTable();
//            dt.Load(cmd.ExecuteReader());
//            conAccountingModel.Close();

//            SqlConnection coHRModelCF = new SqlConnection(ConfigurationManager.ConnectionStrings["HRModelCF"].ConnectionString);

//            if (coHRModelCF.State == ConnectionState.Open) coHRModelCF.Close();
//            coHRModelCF.Open();
//            SqlCommand cmdHRModelCF = new SqlCommand();
//            cmdHRModelCF.Connection = coHRModelCF;

//            string CommandText = " ";
//            CommandText = "select * ";// AccBranchID , Sections.ID SectionsID, Branchies.ID BranchiesID ";

//            //   CommandText += "from[AccountingTest].dbo.com coms ";
//            // CommandText += "join[HR_Sales].dbo.SectionAccJob on coms.COMID = SectionAccJob.SectionAccJobID ";
//            CommandText += "from SectionAccJob ";
//            CommandText += " join[HR_Sales].dbo.Branchies on SectionAccJob.BranchID = Branchies.ID ";
//            CommandText += " join[HR_Sales].dbo.Sections on SectionAccJob.SectionID = Sections.ID ";

//            CommandText += "  where AccBranchID in (select AccBranchID from[HR_Sales].dbo.SectionAccJob where Branchies.ID = " + branchID + " and Sections.ID = " + sectionID + " )";

//            cmdHRModelCF.CommandText = CommandText;

//            DataTable hrdt = new DataTable();
//            hrdt.Load(cmdHRModelCF.ExecuteReader());
//            coHRModelCF.Close();
//            DataTable dtResult = new DataTable();
//            var results = from table1 in dt.AsEnumerable()
//                          join table2 in hrdt.AsEnumerable() on (int)table1["COMID"] equals (int)table2["AccBranchID"]
//                          select new
//                          {
//                              LocID = (int)table1["LocID"],
//                              LocationName = (string)table1["LocationName"]
//                          };
//            dtResult = results.CopyToDataTable();
//            //from dt in dt.AsEnumerable()
//            //where dt.Field<DateTime>("OrderDate") > new DateTime(2001, 8, 1)
//            //select dt;

//            //    // Create a table from the query.
//            //    DataTable boundTable = query.CopyToDataTable<DataRow>();


//            //    dt.Rows.Cast<DataRow>().Join((hrdt.Rows.Cast<DataRow>(), COMID=> dt.Columns["COMID"], AccBranchID=>hrdt.Columns["AccBranchID"], dtResult.Rows.Cast<DataRow>());

//            //    //     dt.Rows.Cast<DataRow>().Join(hrdt.Rows.Cast<DataRow>(), a => a[0], b => b[0],
//            //(a, b) => { a[1] += "" + b[1]; a[2] += "" + b[2]; a[3] += "" + b[3]; return a; }).Count();

//            //    var query = dcReal.COMs   // your starting point - table in the "from" statement
//            // .Join(ctx.SectionAccJobs, // the source table of the inner join
//            //    coms => coms.COMID,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
//            //    sec => sec.SectionAccJobID // Select the foreign key (the second part of the "on" clause)
//            //   , (coms, sec) => new {coms= coms.COMID,sec= sec.SectionAccJobID }) // selection

//            //.Join(ctx.Branchies,
//            //          sec=> sec.BranchID,

//            //    branch => branch.ID, 
//            //    )
//            //    .Where(postAndMeta => postAndMeta.Post.ID == id);    // where statement

//            //  dt = query.CopyToDataTable<DataRow>();
//            ddlSectionAccJobs.DataSource = dtResult;
//            ddlSectionAccJobs.TextField = "LocationName";
//            ddlSectionAccJobs.ValueField = "LocID";
//            ddlSectionAccJobs.DataBind();
//        }
//        /*
//            //branch
//            private void FillCompanies()
//            {

//                //Linq Code
//                //ddl_BranchId.DataSource = dcAccounting.AssetLocationsWithLevelsSelect_Vws.Where(x => x.ParentID == null).Select(ass => new { ass.ID, ass.LocationName }).ToList(); 
//                //ddl_BranchId.TextField = "LocationName";
//                //ddl_BranchId.ValueField = "ID";
//                //ddl_BranchId.DataBind();


//                //Ado.Net Code

//                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AccountingModel"].ConnectionString);
//                if (con.State == ConnectionState.Open) con.Close();
//                con.Open();
//                SqlCommand cmd = new SqlCommand();
//                cmd.Connection = con;
//                cmd.CommandText = "select * from [dbo].[AssetLocationsWithLevelsSelect_Vw] where [ParentID] is null";
//                DataTable dt = new DataTable();
//                dt.Load(cmd.ExecuteReader());
//                con.Close();

//                ddl_BranchId.DataSource = dt;
//                ddl_BranchId.TextField = "LocationName";
//                ddl_BranchId.ValueField = "ID";
//                ddl_BranchId.DataBind();
//            } 
//            //section
//            private void FillDepartments()
//            {
//                if (ddl_BranchId.SelectedItem != null)
//                {
//                  //  ddl_DepartementId.Value = null;
//                    ddl_FloorId.Value = null;
//                    ddl_LocationId.Value = null;
//                    var branchId = int.Parse(ddl_BranchId.SelectedItem.Value.ToString());


//                    //Linq Code
//                    //ddl_DepartementId.DataSource = dcAccounting.AssetLocationsWithLevelsSelect_Vws.Where(x=>x.ParentID == branchId).Select(ass => new { ass.ID, ass.LocationName }).ToList();
//                    //ddl_DepartementId.TextField = "LocationName";
//                    //ddl_DepartementId.ValueField = "ID";
//                    //ddl_DepartementId.DataBind();



//                    //Ado Code
//                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AccountingModel"].ConnectionString);
//                    if (con.State == ConnectionState.Open) con.Close();
//                    con.Open();
//                    SqlCommand cmd = new SqlCommand();
//                    cmd.Connection = con;
//                    cmd.CommandText = "select * from [dbo].[AssetLocationsWithLevelsSelect_Vw] where [ParentID] = '" + branchId + "'";
//                    DataTable dt = new DataTable();
//                    dt.Load(cmd.ExecuteReader());
//                    con.Close();



//                    //ddl_DepartementId.DataSource = dt;
//                    //ddl_DepartementId.TextField = "LocationName";
//                    //ddl_DepartementId.ValueField = "ID";
//                    //ddl_DepartementId.DataBind();





//                }
//            }

//            */
//        private void FillFloors()
//        {
//            if (ddlSectionAccJobs.SelectedItem != null)
//            {
//                ddl_FloorId.Value = null;
//                ddl_LocationId.Value = null;
//                var DepartementId = int.Parse(ddlSectionAccJobs.SelectedItem.Value.ToString());





//                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AccountingModel"].ConnectionString);
//                if (con.State == ConnectionState.Open) con.Close();
//                con.Open();
//                SqlCommand cmd = new SqlCommand();
//                cmd.Connection = con;
//                cmd.CommandText = "select * from [dbo].[AssetLocationsWithLevelsSelect_Vw] where [ParentID] = '" + DepartementId + "'";
//                //cmd.CommandText = "select * from [dbo].[Ast_Location] where [Com_ID] = '" + DepartementId + "'";
//                DataTable dt = new DataTable();
//                dt.Load(cmd.ExecuteReader());
//                con.Close();





//                ddl_FloorId.DataSource = dt;
//                ddl_FloorId.TextField = "LocationName";
//                ddl_FloorId.ValueField = "ID";
//                ddl_FloorId.DataBind();



//            }
//        }

//        private void FillSites()
//        {
//            if (ddlSectionAccJobs.SelectedItem != null)
//            {

//                var FloorId = int.Parse(ddl_FloorId.SelectedItem.Value.ToString());


//                //Linq
//                //ddl_LocationId.DataSource = dcAccounting.AssetLocationsWithLevelsSelect_Vws.Where(x => x.ParentID == FloorId).Select(ass => new { ass.ID, ass.LocationName }).ToList();
//                //ddl_LocationId.TextField = "LocationName";
//                //ddl_LocationId.ValueField = "ID";
//                //ddl_LocationId.DataBind();


//                //Ado 

//                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AccountingModel"].ConnectionString);
//                if (con.State == ConnectionState.Open) con.Close();
//                con.Open();
//                SqlCommand cmd = new SqlCommand();
//                cmd.Connection = con;
//                // cmd.CommandText = "select * from [dbo].[Ast_Location] where [Com_ID] = '" + DepartementId + "'";

//                cmd.CommandText = "select * from [dbo].[AssetLocationsWithLevelsSelect_Vw] where [ParentID] = '" + FloorId + "'";
//                DataTable dt = new DataTable();
//                dt.Load(cmd.ExecuteReader());
//                con.Close();


//                ddl_LocationId.DataSource = dt;
//                ddl_LocationId.TextField = "LocationName";
//                ddl_LocationId.ValueField = "ID";
//                ddl_LocationId.DataBind();

//            }
//        }


//        private void FillParentId()
//        {
//            //ddl_ParentId.DataSource = dcReal.Sp_SelectParentLocation().CopyToDataTable();
//            //ddl_ParentId.TextField = "LocationName";
//            //ddl_ParentId.ValueField = "ID";
//            //ddl_ParentId.DataBind();
//        }
//        #endregion

//        #region "Events"

//        protected void Page_Load(object sender, EventArgs e)
//        {

//            fillddlmainddlSectionAccJobs();
//            //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
//            //scriptManager.RegisterPostBackControl(this.ibtGenerateReport);
//            //permissions(dcWorkShop);

//            txt_RequestDate.Text = DateTime.Now.ToShortDateString();
//            Page.Title = "طلب صيانة مبانى";
//            if (!IsPostBack)
//            {
//                FillBranch();
//                FillSection(0);
//                if (Session["BranchID"] != null)
//                {
//                    BranchID = int.Parse(Session["BranchID"].ToString());
//                }
//                else
//                {
//                    Response.Redirect("Login.aspx");
//                }
//                //permitions(ctx);

//                #region required job Table
//                dt = new DataTable();
//                DataColumn dc1 = new DataColumn("RequiredJob");
//                DataColumn dc2 = new DataColumn("MobileId");
//                DataColumn dc3 = new DataColumn("Description");

//                dt.Columns.Add(dc1);
//                dt.Columns.Add(dc2);
//                dt.Columns.Add(dc3);

//                if (dt.Rows.Count == 0) dt.Rows.Add(dt.NewRow());
//                ViewState["RequiredJobs"] = dt;
//                grd_WorksNeeded.DataSource = ViewState["RequiredJobs"];
//                grd_WorksNeeded.DataBind();
//                grd_WorksNeeded.Rows[0].Visible = false;

//                #endregion

//                #region required job Table
//                dtPictures = new DataTable();
//                DataColumn Col1 = new DataColumn("PicturePath");
//                DataColumn Col2 = new DataColumn("Description");


//                dtPictures.Columns.Add(Col1);
//                dtPictures.Columns.Add(Col2);


//                if (dtPictures.Rows.Count == 0) dtPictures.Rows.Add(dtPictures.NewRow());
//                ViewState["Pictures"] = dtPictures;
//                grd_Pictures.DataSource = ViewState["Pictures"];
//                grd_Pictures.DataBind();
//                grd_Pictures.Rows[0].Visible = false;

//                #endregion
//                FillParentId();
//                ddl_WorkId.DataSource = dcWorkShop.Sp_WorksSelectAll();
//                ddl_WorkId.TextField = "ConfigDetailName";
//                ddl_WorkId.ValueField = "ConfigDetailId";
//                ddl_WorkId.DataBind();
//                //    FillCompanies();


//                // Add ImgArray
//                dtImgArray = new DataTable();
//                DataColumn ImgArrayCol1 = new DataColumn("ImgName", typeof(string));
//                DataColumn ImgArrayCol2 = new DataColumn("ImgByteArray", typeof(byte[]));
//                dtImgArray.Columns.Add(ImgArrayCol1);
//                dtImgArray.Columns.Add(ImgArrayCol2);
//                if (dtImgArray.Rows.Count == 0) dtImgArray.Rows.Add(dtImgArray.NewRow());
//                ViewState["imgArray"] = dtImgArray;
//            }
//            if (ViewState["EmpID"] == null)
//            {
//                EmpID = 1;
//            }
//        }

//        protected void ddl_ParentId_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            //if (ddl_ParentId.SelectedItem != null)
//            //{
//            //    ddl_LocationId.DataSource = dcReal.Sp_SelectLocation(int.Parse(ddl_ParentId.SelectedItem.Value.ToString()));
//            //    ddl_LocationId.TextField = "LocationName";
//            //    ddl_LocationId.ValueField = "ID";
//            //    ddl_LocationId.DataBind();
//            //}
//        }

//        protected void ddl_MobileParentId_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            if (ddl_MobileParentId.SelectedItem != null)
//            {
//                ddl_MobileId.DataSource = dcWorkShop.Sp_SelectMobile(int.Parse(ddl_MobileParentId.SelectedItem.Value.ToString()));
//                ddl_MobileId.TextField = "MobileName";
//                ddl_MobileId.ValueField = "MobileId";
//                ddl_MobileId.DataBind();
//            }
//        }

//        protected void grd_WorksNeeded_RowDataBound(object sender, GridViewRowEventArgs e)
//        {
//            if (e.Row.RowType == DataControlRowType.DataRow)
//            {
//                ASPxComboBox ddl_RequiredJob = (e.Row.FindControl("ddl_RequiredJob") as ASPxComboBox);
//                ddl_RequiredJob.DataSource = dcWorkShop.Sp_WorksSelectAll();
//                ddl_RequiredJob.TextField = "ConfigDetailName";
//                ddl_RequiredJob.ValueField = "ConfigDetailId";
//                ddl_RequiredJob.DataBind();

//                DataRowView dr = e.Row.DataItem as DataRowView;
//                ddl_RequiredJob.Value = dr["RequiredJob"].ToString();




//                ASPxComboBox ddl_MobileId = (e.Row.FindControl("ddl_MobileId") as ASPxComboBox);
//                ddl_MobileId.DataSource = dcWorkShop.Sp_MobileSelectAll();
//                ddl_MobileId.TextField = "MobileName";
//                ddl_MobileId.ValueField = "MobileId";
//                ddl_MobileId.DataBind();

//                DataRowView dr1 = e.Row.DataItem as DataRowView;
//                ddl_MobileId.Value = dr1["MobileId"].ToString();

//            }
//        }

//        protected void btnAddWork_Click(object sender, EventArgs e)
//        {
//            #region validation
//            if (ddl_WorkId.SelectedItem == null)
//            {
//                divmsg3.Attributes["class"] = "alert alert-danger text-right";
//                lblMsg3.Text = "عفوا ادخل العمل";
//                return;

//            }
//            if (ddl_MobileId.SelectedItem == null)
//            {
//                divmsg3.Attributes["class"] = "alert alert-danger text-right";
//                lblMsg3.Text = "عفوا االمنقول المطلوب";
//                return;

//            }


//            #endregion
//            dt = (DataTable)ViewState["RequiredJobs"];
//            DataRow dr1 = dt.NewRow();
//            dr1[0] = ddl_WorkId.SelectedItem.Value;
//            dr1[1] = ddl_MobileId.SelectedItem.Value;
//            dr1[2] = txt_Description.Text;
//            dt.Rows.Add(dr1);

//            grd_WorksNeeded.DataSource = dt;
//            grd_WorksNeeded.DataBind();
//            ViewState["RequiredJobs"] = dt;
//            grd_WorksNeeded.Rows[0].Visible = false;
//            ddl_WorkId.Text = string.Empty;
//            ddl_WorkId.Value = null;
//            ddl_MobileId.Text = string.Empty;
//            ddl_MobileId.Value = null;
//            ddl_MobileParentId.Value = null;
//            ddl_MobileParentId.Text = string.Empty;
//            txt_Description.Text = string.Empty;
//        }

//        protected void grd_WorksNeeded_RowDeleting(object sender, GridViewDeleteEventArgs e)
//        {
//            int index = Convert.ToInt32(e.RowIndex);
//            DataTable dt = ViewState["RequiredJobs"] as DataTable;
//            dt.Rows[index].Delete();
//            ViewState["RequiredJobs"] = dt;
//            grd_WorksNeeded.DataSource = ViewState["RequiredJobs"];
//            grd_WorksNeeded.DataBind();
//            ASPxComboBox ddl_RequiredJobin = (ASPxComboBox)grd_WorksNeeded.Rows[0].FindControl("ddl_RequiredJob");
//            if (ddl_RequiredJobin.SelectedItem == null)
//            {
//                grd_WorksNeeded.Rows[0].Visible = false;
//            }
//        }

//        protected void SavePicture_Click(object sender, EventArgs e)
//        {
//            string path = "";
//            if (FU_Pic.HasFile == false)
//            {
//                divmsg3.Visible = true;
//                divmsg3.Attributes["class"] = "alert alert-danger text-right";
//                lblMsg3.Text = "عفوا ادخل الصورة";
//                return;

//            }
//            else if (FU_Pic.HasFile)
//            {
//                divmsg3.Visible = false;
//                lblMsg3.Text = "";
//                Guid GUIDNo = Guid.NewGuid();
//                string uploadFileName = FU_Pic.FileName.ToString();
//                string strtemp = GUIDNo + uploadFileName;

//                FU_Pic.SaveAs(Server.MapPath("~/Images/maintRequestPictures/" + strtemp));
//                path = "~/Images/maintRequestPictures/" + strtemp;

//                dtImgArray = (DataTable)ViewState["imgArray"];
//                DataRow dr = dtImgArray.NewRow();
//                dr[0] = strtemp;
//                dr[1] = FU_Pic.FileBytes;

//                dtImgArray.Rows.Add(dr);

//                ViewState["imgArray"] = dtImgArray;
//            }

//            dtPictures = (DataTable)ViewState["Pictures"];
//            DataRow dr1 = dtPictures.NewRow();
//            dr1[0] = path;
//            dr1[1] = txt_PicDescription.Text;

//            dtPictures.Rows.Add(dr1);

//            grd_Pictures.DataSource = dtPictures;
//            grd_Pictures.DataBind();
//            ViewState["Pictures"] = dtPictures;
//            grd_Pictures.Rows[0].Visible = false;
//            txt_PicDescription.Text = "";

//        }

//        protected void grd_Pictures_RowDeleting(object sender, GridViewDeleteEventArgs e)
//        {

//            int index = Convert.ToInt32(e.RowIndex);
//            DataTable dtPictures = ViewState["Pictures"] as DataTable;


//            string field = dtPictures.Rows[index]["PicturePath"].ToString();
//            if ((System.IO.File.Exists(Server.MapPath(field))))
//            {
//                System.IO.File.Delete(Server.MapPath(field));
//            }


//            dtPictures.Rows[index].Delete();
//            ViewState["Pictures"] = dtPictures;
//            grd_Pictures.DataSource = ViewState["Pictures"];
//            grd_Pictures.DataBind();


//            Image IMG = (Image)grd_WorksNeeded.Rows[0].FindControl("Image1");
//            try
//            {
//                if (IMG.ImageUrl == null)
//                {
//                    grd_Pictures.Rows[0].Visible = false;
//                }
//            }
//            catch
//            {
//                grd_Pictures.Rows[0].Visible = false;
//            }

//        }

//        protected void Save_Click(object sender, EventArgs e)
//        {

//            dt = (DataTable)ViewState["RequiredJobs"];
//            #region Validation
//            if (dt.Rows.Count <= 1)
//            {
//                divMsg2.Attributes["class"] = "alert alert-danger text-right";
//                lblResult2.Text = "عفوا لا يد من ادخال اعمال الصيانة المطلوبة";
//                return;

//            }
//            #endregion

//            #region Old Code
//            //int Maintreqid = 0;

//            //#region save MainReq

//            ////Ado
//            //var branchIdinLocations = int.Parse(ddl_BranchId.SelectedItem.Value.ToString());
//            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AccountingModel"].ConnectionString);
//            //if (con.State == ConnectionState.Open) con.Close();
//            //con.Open();
//            //SqlCommand cmd = new SqlCommand();
//            //cmd.Connection = con;
//            //cmd.CommandText = "select Com_ID from [dbo].[AssetLocationsWithLevelsSelect_Vw] where ID = '" + branchIdinLocations + "'";
//            //DataTable dtCom = new DataTable();
//            //dtCom.Load(cmd.ExecuteReader());
//            //con.Close();





//            //MaintRequest _MaintRequest = new MaintRequest();
//            //_MaintRequest.PriorUrgent = rd_PriorUrgent.Checked;
//            //_MaintRequest.PriorHigh = rd_PriorHigh.Checked;
//            //_MaintRequest.PriorLow = rd_PriorLow.Checked;


//            ////_MaintRequest.CompanyId = dcAccounting.AssetLocationsWithLevelsSelect_Vws.Single(x => x.ID == branchIdinLocations).Com_ID;

//            //_MaintRequest.CompanyId = branchIdinLocations;


//            //_MaintRequest.RequestedEmpId = EmpID;
//            //_MaintRequest.RequestDate = DateTime.Parse(txt_RequestDate.Text);
//            //_MaintRequest.Notes = txt_Notes.Text;
//            //_MaintRequest.LocationId = int.Parse(ddl_LocationId.SelectedItem.Value.ToString());
//            //dcWorkShop.MaintRequests.InsertOnSubmit(_MaintRequest);
//            //dcWorkShop.SubmitChanges();

//            //Maintreqid = _MaintRequest.MaintReqId;
//            //#endregion

//            //#region save Works

//            //if (dt.Rows.Count > 1)
//            //{

//            //    for (int i = 0; i < dt.Rows.Count; i++)
//            //    {
//            //        if (dt.Rows[i]["RequiredJob"].ToString() != string.Empty)
//            //        {
//            //            MaintReqDetail _MaintReqDetail = new MaintReqDetail();
//            //            _MaintReqDetail.MaintReqId = Maintreqid;
//            //            _MaintReqDetail.MobileId = int.Parse(dt.Rows[i]["MobileId"].ToString());
//            //            _MaintReqDetail.WorkId = int.Parse(dt.Rows[i]["RequiredJob"].ToString());
//            //            if (dt.Rows[i]["Description"] != null)
//            //            {
//            //                _MaintReqDetail.PicDescription = dt.Rows[i]["Description"].ToString();
//            //            }
//            //            else
//            //            {
//            //                _MaintReqDetail.PicDescription = null;
//            //            }
//            //            dcWorkShop.MaintReqDetails.InsertOnSubmit(_MaintReqDetail);
//            //            dcWorkShop.SubmitChanges();
//            //        }
//            //    }




//            //}

//            //#endregion
//            //dtPictures = (DataTable)ViewState["Pictures"];
//            //#region SaveImages
//            //if (dtPictures.Rows.Count > 1)
//            //{
//            //    for (int i = 0; i < dtPictures.Rows.Count; i++)
//            //    {
//            //        if (dtPictures.Rows[i]["PicturePath"].ToString() != string.Empty)
//            //        {
//            //            MaintReqPicture _MaintReqPicture = new MaintReqPicture();
//            //            _MaintReqPicture.MaintReqId = Maintreqid;
//            //            _MaintReqPicture.PicturePath = dtPictures.Rows[i]["PicturePath"].ToString();
//            //            if (dtPictures.Rows[i]["Description"] != null)
//            //            {
//            //                _MaintReqPicture.Description = dtPictures.Rows[i]["Description"].ToString();
//            //            }
//            //            else
//            //            {
//            //                _MaintReqPicture.Description = null;
//            //            }
//            //            dcWorkShop.MaintReqPictures.InsertOnSubmit(_MaintReqPicture);
//            //            dcWorkShop.SubmitChanges();
//            //        }



//            //    }

//            //}

//            //#endregion

//            #endregion

//            #region New Code API
//            MaintRequestVM maintRequestVM = new MaintRequestVM();
//            //EmpId
//            maintRequestVM.EmpId = EmpID;
//            //maintRequest
//            maintRequestDataVM _maintRequestDataVM = new maintRequestDataVM();

//            _maintRequestDataVM.PriorUrgent = rd_PriorUrgent.Checked;
//            _maintRequestDataVM.PriorHigh = rd_PriorHigh.Checked;
//            _maintRequestDataVM.PriorLow = rd_PriorLow.Checked;
//            _maintRequestDataVM.CompanyId = int.Parse(ddl_BranchId.SelectedItem.Value.ToString());
//            _maintRequestDataVM.RequestedEmpId = EmpID;
//            _maintRequestDataVM.RequestDate = DateTime.Parse(txt_RequestDate.Text);
//            _maintRequestDataVM.Notes = txt_Notes.Text;
//            _maintRequestDataVM.LocationId = int.Parse(ddl_LocationId.SelectedItem.Value.ToString());

//            maintRequestVM.maintRequest = _maintRequestDataVM;
//            //MaintReqDetail
//            if (dt.Rows.Count > 1)
//            {

//                for (int i = 0; i < dt.Rows.Count; i++)
//                {
//                    if (dt.Rows[i]["RequiredJob"].ToString() != string.Empty)
//                    {
//                        MaintReqDetailVM _MaintReqDetail = new MaintReqDetailVM();
//                      //  _MaintReqDetail.MaintReqId=; //= 0;edited by asmaa asmaa
//                        _MaintReqDetail.MobileId = int.Parse(dt.Rows[i]["MobileId"].ToString());
//                        _MaintReqDetail.WorkId = int.Parse(dt.Rows[i]["RequiredJob"].ToString());
//                        if (dt.Rows[i]["Description"] != null)
//                        {
//                            _MaintReqDetail.PicDescription = dt.Rows[i]["Description"].ToString();
//                        }
//                        else
//                        {
//                            _MaintReqDetail.PicDescription = null;
//                        }
//                        maintRequestVM.maintReqDetailList.Add(_MaintReqDetail);
//                    }
//                }
//            }

//            //SaveImages
//            dtPictures = (DataTable)ViewState["Pictures"];
//            if (dtPictures.Rows.Count > 1)
//            {
//                for (int i = 0; i < dtPictures.Rows.Count; i++)
//                {
//                    if (dtPictures.Rows[i]["PicturePath"].ToString() != string.Empty)
//                    {
//                        MaintReqPictureVM _MaintReqPicture = new MaintReqPictureVM();
//                        _MaintReqPicture.MaintReqId = 0;
//                        _MaintReqPicture.PicturePath = dtPictures.Rows[i]["PicturePath"].ToString();
//                        if (dtPictures.Rows[i]["Description"] != null)
//                        {
//                            _MaintReqPicture.Description = dtPictures.Rows[i]["Description"].ToString();
//                        }
//                        else
//                        {
//                            _MaintReqPicture.Description = null;
//                        }
//                        maintRequestVM.maintReqPictureList.Add(_MaintReqPicture);
//                    }
//                }

//                //Add ImgArray
//                dtImgArray = (DataTable)ViewState["imgArray"];
//                if (dtImgArray.Rows.Count > 1)
//                {
//                    for (int i = 1; i < dtImgArray.Rows.Count; i++)
//                    {
//                        maintRequestVM.imgByteArray.Add(dtImgArray.Rows[i]["ImgName"].ToString(), (byte[])dtImgArray.Rows[i]["ImgByteArray"]);
//                    }
//                }
//            }
//            var ServicePath = _unitOfWork.ConfigRepository.GetConfigValueByKey("BusesApiPath");

//            try
//            {
//                string url = $"{ServicePath}WebApi/MaintRequest/SaveMaintRequest";

//                // Create a request using a URL that can receive a post.   
//                WebRequest request = WebRequest.Create(url);
//                // Set the Method property of the request to POST.  
//                request.Method = "POST";

//                // Create POST data and convert it to a byte array.  
//                string postData = new JavaScriptSerializer().Serialize(maintRequestVM);
//                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

//                // Set the ContentType property of the WebRequest.  
//                request.ContentType = "application/json";
//                // Set the ContentLength property of the WebRequest.  
//                request.ContentLength = byteArray.Length;

//                // Get the request stream.  
//                Stream dataStream = request.GetRequestStream();
//                // Write the data to the request stream.  
//                dataStream.Write(byteArray, 0, byteArray.Length);
//                // Close the Stream object.  
//                dataStream.Close();

//                // Get the response.  
//                WebResponse response = request.GetResponse();

//                // Get the stream containing content returned by the server.  
//                // The using block ensures the stream is automatically closed.
//                using (dataStream = response.GetResponseStream())
//                {
//                    // Open the stream using a StreamReader for easy access.  
//                    StreamReader reader = new StreamReader(dataStream);
//                    // Read the content.  
//                    string responseFromServer = reader.ReadToEnd().ToString();
//                    if (responseFromServer == '"' + "Success" + '"')
//                    {
//                        divMsg2.Attributes["class"] = "alert alert-success text-right";
//                        lblResult2.Text = "تم الحفظ بنجاح";
//                        Employee emp = ctx.Employees.Where(x => x.ID == EmpID).SingleOrDefault();
//                        bool notified = _hrBusiness.Notifications.InsertNotification(userId.Value, 3, null, null, null, emp.BranchID, emp.SectionID, null, null);


//                        CommonHelper.ClearControls(Page);

//                        dtPictures = (DataTable)ViewState["Pictures"];
//                        dtPictures.Rows.Clear();
//                        dtPictures.Rows.Add(dtPictures.NewRow());
//                        grd_Pictures.DataSource = dtPictures;
//                        grd_Pictures.DataBind();
//                        grd_Pictures.Rows[0].Visible = false;
//                        ViewState["Pictures"] = dtPictures;




//                        dt = (DataTable)ViewState["RequiredJobs"];
//                        dt.Rows.Clear();
//                        dt.Rows.Add(dt.NewRow());
//                        grd_WorksNeeded.DataSource = dt;
//                        grd_WorksNeeded.DataBind();
//                        grd_WorksNeeded.Rows[0].Visible = false;
//                        ViewState["RequiredJobs"] = dt;
//                    }
//                }

//                // Close the response.  
//                response.Close();
//            }
//            catch (Exception ex)
//            {
//                divMsg2.Attributes["class"] = "alert alert-danger text-right";
//                lblResult2.Text = "حدث خطأ اثناء الحفظ";
//            }


//            #endregion

//        }



//        protected void ddl_BranchId_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            // FillDepartments();
//            FillSection(Convert.ToInt16(ddl_BranchId.SelectedItem.Value));//


//        }
//        protected void ddlSectionAccJobs_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            FillFloors();
//        }
//        protected void ddlmainddlSectionAccJobs_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            fillSectionAccJobs(Convert.ToInt16(ddl_BranchId.SelectedItem.Value), Convert.ToInt16(ddlSection.SelectedItem.Value));
//        }
//        protected void ddl_DepartementId_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            //FillFloors();

//        }
//        protected void ddl_FloorId_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            FillSites();
//        }


//        #endregion
//    }
//}