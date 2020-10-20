using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusesWorkshop.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using BusesWorkshop.VM;
using System.Net;
using BusesWorkshop.DAL.Bus;
using System.IO;
using System.ComponentModel;
using BusesWorkshop.DAL.Accounting;

namespace BusesWorkshop.Pages
{
    public partial class NetworkDevicesInformation : System.Web.UI.Page
    {
        WorkshopDataContext dc = new WorkshopDataContext();
        RealEstateDataContext dcReal = new RealEstateDataContext();
        List<DeviceInfoVM> lst = new List<DeviceInfoVM>();
        private int NetworkDevicesInformationID
        {
            get
            {
                if (ViewState["_NetworkDevicesInformationID"] != null)
                {
                    return Convert.ToInt32(ViewState["_NetworkDevicesInformationID"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState.Add("_NetworkDevicesInformationID", value);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            permissions(dc);
            Page.Title = "تهيئة عنوان الشبكه للفروع";
           
            if (!IsPostBack)
            {
                FillCompanies();
            }
        }
        private void FillCompanies()
        {

            ddl_CompanyId.DataSource = from p in dc.Companies select new { p.ID, p.CompName };
            ddl_CompanyId.TextField = "CompName";
            ddl_CompanyId.ValueField = "ID";
            ddl_CompanyId.DataBind();
             
        }
        private void permissions(WorkshopDataContext dc)
        {
            try
            {
                DataTable dt = Common.GetUserPermission(dc, int.Parse(Session["UserID"].ToString()), 49);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["D"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["D"].ToString()))
                    {
                        Response.Redirect(@"..\Pages\Login.aspx", false);
                    }
                    if (dt.Rows[0]["D"].ToString() != string.Empty && !bool.Parse(dt.Rows[0]["D"].ToString()))
                    {
                        ViewState["AllowDelete"] =0;
                    }
                    else
                    {
                        ViewState["AllowDelete"] =1;
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                dc.Connection.Open();
                dc.Transaction = dc.Connection.BeginTransaction();
                #region validation
                if (ddl_CompanyId.SelectedItem == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ادخل اسم الفرع";
                    return;
                }

                if (ddl_StaticIP.SelectedItem == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ادخل اسم الشبكه";
                    return;
                }
                #endregion

                if (int.Parse(ViewState["AllowInsert"].ToString()) == 0 || ViewState["AllowInsert"] == null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "عفوا ليس لديك صلاحية الادخال";
                    return;
                }
                var HDID = int.Parse(ddl_StaticIP.SelectedItem.Value.ToString());
                List<DeviceInfoVM> DeviceInfoVMS = new List<DeviceInfoVM>();
                if(Session["IPS"] ==null)
                {
                    divMsg.Attributes["class"] = "alert alert-danger text-right";
                    lblResult.Text = "يجب مسح الشبكه أولا";
                    return;
                }

                DeviceInfoVMS = Session["IPS"] as List<DeviceInfoVM>;
                var oldData =   dc.NetworkDevicesInformations
                                .Where(x => x.HDID == HDID);
                //////////////////////////////////////////////////////////
                foreach (var item in DeviceInfoVMS)
                {
                    var StaticIP = int.Parse(ddl_StaticIP.SelectedItem.Value.ToString());
                    var OldDataComparing = oldData.Where(x => x.IpAddress == item.IpAddress && x.HDID == StaticIP).FirstOrDefault();
                    if (OldDataComparing != null)
                    {
                        OldDataComparing.IsActive = true;
                    }
                    else
                    {
                        BusesWorkshop.DAL.Bus.NetworkDevicesInformation obj = new BusesWorkshop.DAL.Bus.NetworkDevicesInformation();
                        obj.DeviceName = item.DeviceName;
                        obj.IpAddress = item.IpAddress;
                        obj.MacAddress = item.MacAddress;
                        obj.HDID = int.Parse(ddl_StaticIP.SelectedItem.Value.ToString());
                        obj.InsertDate = DateTime.Now;
                        obj.IsDeleted = false;
                        obj.IsActive = true;
                        obj.UserID = int.Parse(Session["UserID"].ToString());
                        dc.NetworkDevicesInformations.InsertOnSubmit(obj);
                    }
                    dc.SubmitChanges();
                }
                
                    divMsg.Attributes["class"] = "alert alert-success text-right";
                    lblResult.Text = "تم الحفظ بنجاح";
                    dc.Transaction.Commit();

            }
            catch(Exception ex)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "حدث خطأ أثنا الحفظ";
                dc.Transaction.Rollback();
            }
            finally
            {

                dc.Connection.Close();
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int result = 0;
            List<DeviceInfoVM> deviceInfoVMs = new List<DeviceInfoVM>();
            #region validation
            if (ddl_CompanyId.SelectedItem == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل اسم الفرع";
                return;
            }

            if (ddl_StaticIP.SelectedItem == null)
            {
                divMsg.Attributes["class"] = "alert alert-danger text-right";
                lblResult.Text = "عفوا ادخل اسم الشبكه";
                return;
            }
            #endregion
            lst = scan(ddl_StaticIP.Text);
            var HDID =int.Parse(ddl_StaticIP.SelectedItem.Value.ToString());
            var list = dc.NetworkDevicesInformations.Where(x => x.HDID == HDID).ToList();
            foreach(var item in list)
            {
                if(!lst.Any(x=>x.IpAddress == item.IpAddress))
                {
                    DeviceInfoVM deviceInfoVM = new DeviceInfoVM();
                    deviceInfoVM.DeviceName = item.DeviceName;
                    deviceInfoVM.MacAddress = item.MacAddress;
                    deviceInfoVM.IpAddress = item.IpAddress;
                    deviceInfoVM.NetworkDeviceInformationID = item.ID;
                    deviceInfoVM.Status = "OFF";
                    lst.Add(deviceInfoVM);
                }
               
            }
            foreach(var item in lst)
            {

                item.NetworkDeviceInformationID = dc.NetworkDevicesInformations.Where(x => x.HDID == HDID && x.IpAddress == item.IpAddress).Select(p => p.ID).FirstOrDefault();
                var linq1 = (from p in dc.DeviceWithIPAddresses
                             join c in dc.Companies
                             on p.CompanyId equals c.ID
                             join i in dc.NetworkDevicesInformations.Where(x=>x.ID == item.NetworkDeviceInformationID)
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
                         select new DeviceInfoVM
                         {
                            
                             CompName = l.CompName,
                             BranchAccName = a.LocationName,
                             SectionName = aa.LocationName,
                             RoomName = aaa.LocationName,
                             FloorName = aaaa.LocationName,
                             AssetMasterName = c.CAT_Name,
                             SubAssetMasterName = astName.Name,
                             IpAddress = l.IpAddress,
                             MacAddress = l.MacAddress,
                             DeviceName = item.DeviceName,    
                             Status = item.Status
                    }).FirstOrDefault();
                if (linq2 == null)
                {

                    deviceInfoVMs.Add(item);
                }
                else
                {

                    deviceInfoVMs.Add(linq2);
                }
            }
            FillGrid(deviceInfoVMs);
            Session["IPS"] = deviceInfoVMs;
        }
        protected void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Scan" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gvNetworkDevicesInformation.GridLines = GridLines.Both;
            gvNetworkDevicesInformation.HeaderStyle.Font.Bold = true;
            gvNetworkDevicesInformation.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
        public override void  VerifyRenderingInServerForm(Control control)
        {
            return;
        }
        protected void lnk_REMOTE_CONNECTION_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;
            var hostname = gvNetworkDevicesInformation.DataKeys[row.RowIndex].Value.ToString();

            string fileName = "Default.rdp";

            if ((fileName != ""))
            {
                // Get absolute path of the file
                string filePath = Server.MapPath("~/Download/" + fileName);

                // Update RDP file
                LinkButton linkButton = (LinkButton)sender;
                UpdateRDPFile(hostname, filePath);

                // Download RDP file
                DownLoadFile(filePath);
            }
            else
            {
                Response.Write("Please provide a file to download.");
            }
             
        }
        private void UpdateRDPFile(string IPAddress, string filePath)
        {
            StringBuilder newFile = new StringBuilder();
            string temp = "";
            File.WriteAllText(filePath, String.Empty);
            string[] Contents = { "full address:s:" };
            File.WriteAllLines(filePath, Contents);
            string[] fileToUpdate = File.ReadAllLines(filePath);
            foreach (string line in fileToUpdate)
            {
                if (line.Contains("full address:s:"))
                {
                    temp = temp.Insert(0, "full address:s:" + IPAddress);
                    newFile.Append(temp + "\r\n");
                    continue;
                }
                newFile.Append(line + "\r\n");
            }

            File.WriteAllText(filePath, newFile.ToString());
        }

        private void DownLoadFile(string filePath)
        {
            // get file object as FileInfo
            System.IO.FileInfo file = new System.IO.FileInfo(filePath);
            // -- if the file exists on the server
            if (file.Exists)
            {
                // set appropriate headers
                Response.Clear();
                Response.AddHeader("Content-Disposition", ("attachment; filename=" + file.Name));
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();
                // if file does not exist
            }
            else
            {
                Response.Write("This file does not exist.");
            }
        }

        private void FillGrid(List<DeviceInfoVM> deviceInfoVMs)
        {

            gvNetworkDevicesInformation.DataSource = deviceInfoVMs;
            gvNetworkDevicesInformation.DataBind();

        }
        protected void ddl_CompanyId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_CompanyId.SelectedItem != null)
            {
                ddl_StaticIP.SelectedItem = null;
                ddl_StaticIP.Items.Clear();
                ddl_StaticIP.DataSource = from p in dc.StaticIPAddressWithBranches where p.CompanyID == int.Parse(ddl_CompanyId.SelectedItem.Value.ToString()) select new { p.ID, p.StaticIP };
                ddl_StaticIP.TextField = "StaticIP";
                ddl_StaticIP.ValueField = "ID";
                ddl_StaticIP.DataBind();
            }
        }



        public List<DeviceInfoVM> scan(string subnet)
        {
            Ping myPing;
            PingReply reply;
            IPAddress addr;
            IPHostEntry host;
            List<DeviceInfoVM> listVAddr = new List<DeviceInfoVM>(); 
            for (int i = 1; i < 255; i++)
            {
                string subnetn = "." + i.ToString();
                myPing = new Ping();

                byte[] buffer = Encoding.ASCII.GetBytes(subnet + subnetn);
                PingOptions options = new PingOptions(247, true);
                AutoResetEvent reset = new AutoResetEvent(false);
                options.DontFragment = true;

                reply = myPing.Send(subnet + subnetn, 1000, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    try
                    {
                        addr = IPAddress.Parse(subnet + subnetn);
                        host = Dns.GetHostEntry(addr);

                        listVAddr.Add(new DeviceInfoVM {IpAddress=  subnet + subnetn , DeviceName = host.HostName ,MacAddress = GetMacAddress(subnet + subnetn) });
                    }
                    catch (SocketException ex)
                    {
                        if (ex.SocketErrorCode == SocketError.HostNotFound)
                        {
                            continue;
                        }
                    }
                }
            }
            return listVAddr;
        }

        public string GetMacAddress(string ipAddress)
        {
            string macAddress = string.Empty;
            System.Diagnostics.Process Process = new System.Diagnostics.Process();
            Process.StartInfo.FileName = "arp";
            Process.StartInfo.Arguments = "-a " + ipAddress;
            Process.StartInfo.UseShellExecute = false;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.StartInfo.CreateNoWindow = true;
            Process.Start();
            string strOutput = Process.StandardOutput.ReadToEnd();
            string[] substrings = strOutput.Split('-');
            if (substrings.Length >= 8)
            {
                macAddress = substrings[3].Substring(Math.Max(0, substrings[3].Length - 2))
                         + "-" + substrings[4] + "-" + substrings[5] + "-" + substrings[6]
                         + "-" + substrings[7] + "-"
                         + substrings[8].Substring(0, 2);
                return macAddress;
            }

            else
            {
                return "OWN Machine";
            }
        }

    }
}
