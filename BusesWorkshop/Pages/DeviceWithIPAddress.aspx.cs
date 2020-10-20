using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
//using BusesWorkshop.DAL.Bus;
using BusesWorkshop.DAL.Accounting;
using BusesWorkshop.DAL.Bus;
using BusesWorkshop.DAL;
using System.Data.SqlClient;

namespace BusesWorkshop.Pages
{
    public partial class DeviceWithIPAddress : System.Web.UI.Page
    {
        #region "Properties" 
        
        RealEstateDataContext dcReal = new  RealEstateDataContext();
     WorkshopDataContext dcWorkShop = new WorkshopDataContext();
        private int DeviceWithIPAddressID
        {
            get
            {
                if (ViewState["DeviceWithIPAddressID"] != null)
                {
                    return ((int)ViewState["DeviceWithIPAddressID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["DeviceWithIPAddressID"] = value;
            }
        }
        #endregion
        #region "Methods"

        private void FillAssetMaster()
        {

            var d = (from c in dcReal.CATs
                    join a in dcReal.Assets
                    on c.CAT_ID equals a.Cat_Id
                    where a.LocationID == Convert.ToInt16(ddl_RoomId.SelectedItem.Value.ToString())
                    select new
                    { CAT_Name = c.CAT_Name, CAT_ID = c.CAT_ID }).Distinct();
            ddl_AssetMasterId.DataSource = d; //dcReal.CATs.ToList();
            ddl_AssetMasterId.TextField = "CAT_Name";
            ddl_AssetMasterId.ValueField = "CAT_ID";
            ddl_AssetMasterId.DataBind();

        }
        private void FillSubAssetMaster(int MasterID) {
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

        private void FillStaticIP(int BranchID)
        {

            ddl_StaticIP.DataSource = dcWorkShop.StaticIPAddressWithBranches.Where(x => x.CompanyID == BranchID).Select(x=>new
            {
                x.ID,
                x.StaticIP
            });
            ddl_StaticIP.TextField = "StaticIP";
            ddl_StaticIP.ValueField = "ID";
            ddl_StaticIP.DataBind();
            

        }
        private void FillCompanies()
        {
            ddl_companyId.DataSource = dcWorkShop.Companies.ToList();
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

        private void FillBranchIdInAcc(int? ComID)
        {
            ddl_BranchIdInAcc.DataSource
                = dcReal.Ast_Locations.
                          Where(ast_l => ast_l.Com_ID == ComID && ast_l.ParentID == null)
                          .Select(x => new { LocID= x.ID, LocationName= x.LocationName }).ToList();

            ddl_BranchIdInAcc.TextField = "LocationName";
            ddl_BranchIdInAcc.ValueField = "LocID";
            ddl_BranchIdInAcc.DataBind();
        }

        private void FillSections(int? BranchIdInAcc)
        {
            ddl_SectionId.DataSource = dcReal.Ast_Locations.Where(x =>  x.ParentID == BranchIdInAcc)
                .Select(x=> new { x.ID ,x.LocationName });
            ddl_SectionId.TextField = "LocationName";
            ddl_SectionId.ValueField = "ID";
            ddl_SectionId.DataBind();
        }
        private void FillFloors(int SectionID)
        {
            ddl_FloorId.DataSource = dcReal.Ast_Locations.Where(x =>  x.ParentID == SectionID);
            ddl_FloorId.TextField = "LocationName";
            ddl_FloorId.ValueField = "ID";
            ddl_FloorId.DataBind();
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
            //try
            //{
            //    DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), Common.PagesEnum.DeviceWithIPAddress.GetHashCode());
            //    if (dt.Rows.Count > 0)
            //    {
            //        if (dt.Rows[0]["D"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["D"].ToString()))
            //        {
            //            Response.Redirect(@"..\Pages\Login.aspx", false);
            //        }
            //        if (dt.Rows[0]["R"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["R"].ToString()))
            //        {
            //            ViewState["AllowDelete"] = 0;
            //        }
            //        else
            //        {
            //            ViewState["AllowDelete"] = 0;
            //        }

            //        if (dt.Rows[0]["U"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["U"].ToString()))
            //        {
            //            ViewState["AllowUpDate"] = 0;
            //        }
            //        else
            //        {
            //            ViewState["AllowUpDate"] = 1;
            //        }
            //        if (dt.Rows[0]["I"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["I"].ToString()))
            //        {
            //            ViewState["AllowInsert"] = 0;
            //        }
            //        else
            //        {
            //            ViewState["AllowInsert"] = 1;
            //        }
            //    }
            //    else
            //    {
            //        Response.Redirect(@"..\Pages\Login.aspx");
            //    }
            //}
            //catch
            //{
            //    Response.Redirect(@"..\Pages\Login.aspx");
           // }
        }
        #endregion
        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            
            permissions(dcWorkShop);
            Page.Title = " ربط العنوان بالمنقول";
            if (!IsPostBack)
            {

                txt_MacAddress.Text = null;
                FillGrid();
                #endregion
                FillCompanies();
            }
             
        }
        protected void ddl_StaticIP_selectedindexchanged(object sender, EventArgs e)
        {
            var StaticIPAddress =int.Parse(ddl_StaticIP.SelectedItem.Value.ToString());
            ddl_IPAddress.DataSource = dcWorkShop.NetworkDevicesInformations.Where(x => x.HDID == StaticIPAddress).Select(x=>new { x.ID ,x.IpAddress});
            ddl_IPAddress.TextField = "IpAddress";
            ddl_IPAddress.ValueField = "ID";
            ddl_IPAddress.DataBind();
        }
         
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                dcWorkShop.Connection.Open();
                dcWorkShop.Transaction = dcWorkShop.Connection.BeginTransaction();
                #region validation
                if (ddl_companyId.SelectedItem == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ادخل اسم الفرع";
                    return;
                }
                if (ddl_BranchIdInAcc.SelectedItem == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ادخل إسم الفرع في دليل المواقع";
                    return;
                }

                if (ddl_SectionId.SelectedItem == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ادخل اسم القسم";
                    return;
                }

                if (ddl_FloorId.SelectedItem == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ادخل اسم الدور";
                    return;
                }

                if (ddl_RoomId.SelectedItem == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ادخل اسم الموقع الفرعي";
                    return;
                }
                if (ddl_AssetMasterId.SelectedItem == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ادخل اسم المنقول الرئيسي";
                    return;
                }
                if (ddl_SubAssetId.SelectedItem == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ادخل اسم المنقول ";
                    return;
                }

                if (ddl_StaticIP.SelectedItem == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ادخل عنوان الشبكه الثابت";
                    return;
                }
                if (ddl_IPAddress.SelectedItem == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ادخل عنوان الشبكه";
                    return;
                }
                #endregion

                //if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
                //{
                //    divMsg.Attributes["class"] = "alert alert-danger text-right";
                //    lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                //    return;
                //}
                var SelectedNetworkDeviceInfoID = int.Parse(ddl_IPAddress.SelectedItem.Value.ToString());

                if (DeviceWithIPAddressID > 0)
                {
                    #region Update
                    //if (int.Parse(ViewState["AllowUpDate"].ToString()) == 0 || ViewState["AllowUpDate"] == null)
                    //{
                    //    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    //    lblResult.Text = "عفوا ليس لديك صلاحية التعديل";
                    //    return;
                    //}
                    BusesWorkshop.DAL.Bus.DeviceWithIPAddress _DeviceWithIPAddress   =
                    dcWorkShop.DeviceWithIPAddresses.Single(x => x.ID == DeviceWithIPAddressID);
                    _DeviceWithIPAddress.CompanyId = int.Parse(ddl_companyId.SelectedItem.Value.ToString());
                    _DeviceWithIPAddress.BranchIdInAcc = int.Parse(ddl_BranchIdInAcc.SelectedItem.Value.ToString());
                    _DeviceWithIPAddress.SectionId = int.Parse(ddl_SectionId.SelectedItem.Value.ToString());
                    _DeviceWithIPAddress.FloorId = int.Parse(ddl_FloorId.SelectedItem.Value.ToString());
                    _DeviceWithIPAddress.RoomId = int.Parse(ddl_RoomId.SelectedItem.Value.ToString());
                    _DeviceWithIPAddress.AssetMasterId = int.Parse(ddl_AssetMasterId.SelectedItem.Value.ToString());
                    _DeviceWithIPAddress.SubAssetId = int.Parse(ddl_SubAssetId.SelectedItem.Value.ToString());
                    _DeviceWithIPAddress.NetworkDeviceInformationId = int.Parse(ddl_IPAddress.SelectedItem.Value.ToString());
                    BusesWorkshop.DAL.Bus.NetworkDevicesInformation obj =
                     dcWorkShop.NetworkDevicesInformations.Single(x => x.ID == SelectedNetworkDeviceInfoID);
                    obj.MacAddress = txt_MacAddress.Text.ToString();
                    dcWorkShop.SubmitChanges();
                    result = _DeviceWithIPAddress.ID;
                    #endregion

                }
                else
                {
                    #region save
                    //if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
                    //{
                    //    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    //    lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                    //    return;
                    //}
                    BusesWorkshop.DAL.Bus.DeviceWithIPAddress _DeviceWithIPAddress = new DAL.Bus.DeviceWithIPAddress();
                    _DeviceWithIPAddress.CompanyId = int.Parse(ddl_companyId.SelectedItem.Value.ToString());
                    _DeviceWithIPAddress.BranchIdInAcc = int.Parse(ddl_BranchIdInAcc.SelectedItem.Value.ToString());
                    _DeviceWithIPAddress.SectionId = int.Parse(ddl_SectionId.SelectedItem.Value.ToString());
                    _DeviceWithIPAddress.FloorId = int.Parse(ddl_FloorId.SelectedItem.Value.ToString());
                    _DeviceWithIPAddress.RoomId = int.Parse(ddl_RoomId.SelectedItem.Value.ToString());
                    _DeviceWithIPAddress.AssetMasterId = int.Parse(ddl_AssetMasterId.SelectedItem.Value.ToString());
                    _DeviceWithIPAddress.SubAssetId = int.Parse(ddl_SubAssetId.SelectedItem.Value.ToString());
                    _DeviceWithIPAddress.NetworkDeviceInformationId = int.Parse(ddl_IPAddress.SelectedItem.Value.ToString());
                    _DeviceWithIPAddress.InsertDate = DateTime.Now;
                    _DeviceWithIPAddress.UserId = int.Parse(Session["UserID"].ToString());

                    BusesWorkshop.DAL.Bus.NetworkDevicesInformation obj =
                    dcWorkShop.NetworkDevicesInformations.Single(x => x.ID == SelectedNetworkDeviceInfoID);
                    obj.MacAddress = txt_MacAddress.Text.ToString();
                    dcWorkShop.DeviceWithIPAddresses.InsertOnSubmit(_DeviceWithIPAddress);

                    dcWorkShop.SubmitChanges();
                    result = _DeviceWithIPAddress.ID;
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
                FillGrid();
                MyClasses.ClearControls(Page);
                dcWorkShop.Transaction.Commit();

            }
            catch (Exception)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "حدث خطأ أثنا الحفظ";
                dcWorkShop.Transaction.Rollback();
            }
            finally
            {

                dcWorkShop.Connection.Close();
            }
        }
        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        private void FillGrid()
        {
            #region test
            //var Query1 = from a in dcReal.Ast_Locations.ToList().Where(x => x.ParentID == null)
            //             join l in linq1
            //             on a.Com_ID equals l.SchoolID
            //             select new
            //             {
            //                 CompName = l.CompName,
            //                 BranchAccName = a.LocationName
            //             };
            //var Query2 =
            //           from aa in dcReal.Ast_Locations.ToList()
            //           join l in linq1
            //           on aa.ID equals l.SectionId
            //           select new
            //           {
            //               SectionName = aa.LocationName
            //           };
            //var Query3 =
            //          from aaa in dcReal.Ast_Locations.ToList()
            //          join l in linq1
            //          on aaa.ID equals l.RoomId
            //          join asset in dcReal.Assets.ToList()
            //          on l.RoomId equals asset.LocationID
            //          join c in dcReal.CATs.ToList()
            //          on asset.Cat_Id equals c.CAT_ID
            //          join astName in dcReal.AstNames
            //          on asset.AST_ID equals astName.ID
            //          select new
            //          {
            //              RoomName = aaa.LocationName,
            //              AssetMasterName = c.CAT_Name,
            //              SubAssetMasterName = astName.Name
            //          };
            //var Query4 =
            //          from aaaa in dcReal.Ast_Locations.ToList()
            //          join l in linq1
            //          on aaaa.ID equals l.FloorId

            //          select new
            //          {
            //              FloorNAme = aaaa.LocationName
            //          };
            #endregion
            var linq1 = (from p in dcWorkShop.DeviceWithIPAddresses
                         join c in dcWorkShop.Companies
                         on p.CompanyId equals c.ID
                         join i in dcWorkShop.NetworkDevicesInformations
                         on p.NetworkDeviceInformationId equals i.ID
                         select new
                         {
                             p.ID,
                             p.CompanyId,
                             p.BranchIdInAcc,
                             p.SectionId,
                             p.RoomId,
                             p.FloorId,
                             p.SubAssetId,
                             p.AssetMasterId,
                             c.SchoolID,
                             c.CompName,
                             i.IpAddress,
                             i.MacAddress
                         }).ToList();


            var linq2 = (from a in dcReal.Ast_Locations.ToList().Where(x => x.ParentID == null)
                         join l in linq1
                         on a.Com_ID equals l.SchoolID

                         join aa in dcReal.Ast_Locations.ToList()
                         on l.SectionId equals aa.ID

                         join aaa in dcReal.Ast_Locations.ToList()
                         on l.RoomId equals aaa.ID

                         join asset in dcReal.Assets.ToList()
                         on l.RoomId equals asset.LocationID
                         join c in dcReal.CATs.ToList()
                         on asset.Cat_Id equals c.CAT_ID

                         join aaaa in dcReal.Ast_Locations.ToList()
                         on l.FloorId equals aaaa.ID
                         join astName in dcReal.AstNames
                         on asset.AST_ID equals astName.ID
                         select new
                         {
                             ID=l.ID,
                             CompName = l.CompName,
                             BranchAccName = a.LocationName,
                             SectionName = aa.LocationName,
                             RoomName = aaa.LocationName,
                             FloorNAme = aaaa.LocationName,
                             AssetMasterName = c.CAT_Name,
                             SubAssetMasterName = astName.Name,
                             IPAddress = l.IpAddress,
                             MacAddress = l.MacAddress
                         }).ToList();
            IPAddressWithDeviceGrid.DataSource = linq2.ToList();
            IPAddressWithDeviceGrid.DataBind();

        }
        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;

            DeviceWithIPAddressID = Convert.ToInt32(IPAddressWithDeviceGrid.DataKeys[row.RowIndex].Value);

            var query = (from p in dcWorkShop.DeviceWithIPAddresses where p.ID.Equals(DeviceWithIPAddressID) select new {
                
                p
            
            }).SingleOrDefault();
            var NetworkDeviceInformations = dcWorkShop.NetworkDevicesInformations.Where(x => x.ID == query.p.NetworkDeviceInformationId).Select(x =>new { x.MacAddress , x.StaticIPAddressWithBranch.StaticIP ,x.IpAddress } ).FirstOrDefault();
            ddl_companyId.Value = query.p.CompanyId.ToString();
            ddl_companyid_selectedindexchanged(null, null);
            ddl_BranchIdInAcc.Value = query.p.BranchIdInAcc.ToString();
            ddl_BranchIdInAcc_SelectedIndexChanged(null, null);
            ddl_SectionId.Value = query.p.SectionId.ToString();
            ddl_SectionId_SelectedIndexChanged(null, null);
            ddl_FloorId.Value = query.p.FloorId.ToString();
            ddl_FloorId_SelectedIndexChanged(null, null);
            ddl_RoomId.Value = query.p.RoomId.ToString();
            ddl_RoomId_SelectedIndexChanged(null, null);
            ddl_AssetMasterId.Value = query.p.AssetMasterId.ToString();
            ddl_AssetMasterId_SelectedIndexChanged(null, null);
            ddl_SubAssetId.Value = query.p.SubAssetId.ToString();
            ddl_StaticIP.Value = NetworkDeviceInformations.StaticIP.ToString();
            ddl_StaticIP_selectedindexchanged(null, null);
            ddl_IPAddress.Value = NetworkDeviceInformations.IpAddress.ToString();
            txt_MacAddress.Text = NetworkDeviceInformations.MacAddress.ToString();
        }

        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            //if (int.Parse(ViewState["AllowDelete"].ToString()) == 0 || ViewState["AllowDelete"] == null)
            //{
            //    divMsg.Attributes["class"] = "alert alert-danger text-right";
            //    lblResult.Text = "عفوا ليس لديك صلاحية الحذف";
            //    return;
            //}
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;
            var query = from p in dcWorkShop.DeviceWithIPAddresses where p.ID.Equals(Convert.ToInt32(IPAddressWithDeviceGrid.DataKeys[row.RowIndex].Value)) select p;
            try
            {
                foreach (var item in query)
                {
                    dcWorkShop.DeviceWithIPAddresses.DeleteOnSubmit(item);
                }
                dcWorkShop.SubmitChanges();
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
            FillGrid();
        }
        protected void ddl_companyid_selectedindexchanged(object sender, EventArgs e)
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

            ddl_StaticIP.Value = string.Empty;
            ddl_StaticIP.DataSource = null;
            ddl_StaticIP.DataBind();


            ddl_IPAddress.Value = string.Empty;
            ddl_IPAddress.DataSource = null;
            ddl_IPAddress.DataBind();
            if (ddl_companyId.SelectedItem.Value != null)
            {

                int CompID = int.Parse(ddl_companyId.SelectedItem.Value.ToString());
                int? schoolid = dcWorkShop.Companies.Where(
                                                            c => c.ID == CompID
                                                            ).Select(c => c.SchoolID).FirstOrDefault();
                int? com_id = dcReal.Ast_Locations.Where(c => c.Com_ID == CompID).Select(c => c.Com_ID).FirstOrDefault();
                FillBranchIdInAcc(schoolid);
                FillStaticIP(CompID);
            }

        }
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

        protected void ddl_FloorId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_RoomId.Value = string.Empty;
            ddl_RoomId.DataSource = null;
            ddl_RoomId.DataBind();

            ddl_AssetMasterId.Value = string.Empty;
            ddl_AssetMasterId.DataSource = null;
            ddl_AssetMasterId.DataBind();

            ddl_SubAssetId.Value = string.Empty;
            ddl_SubAssetId.DataSource = null;
            ddl_SubAssetId.DataBind();

            FillRoom(int.Parse(ddl_FloorId.SelectedItem.Value.ToString()));
        }

         
        protected void ddl_RoomId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_AssetMasterId.Value = string.Empty;
            ddl_AssetMasterId.DataSource = null;
            ddl_AssetMasterId.DataBind();
            FillAssetMaster();
        }
        protected void ddl_AssetMasterId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_SubAssetId.Value = string.Empty;
            ddl_SubAssetId.DataSource = null;
            ddl_SubAssetId.DataBind();
            FillSubAssetMaster(int.Parse(ddl_AssetMasterId.SelectedItem.Value.ToString()));
        }
         
    }
}
