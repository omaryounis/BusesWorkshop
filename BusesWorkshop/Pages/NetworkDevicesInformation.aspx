<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="NetworkDevicesInformation.aspx.cs" 
    Inherits="BusesWorkshop.Pages.NetworkDevicesInformation" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
    
<style type="text/css">
    .loader {
        position: fixed;
        left: 0px;
        top: 0px;
        width: 100%;
        height: 100%;
        z-index: 9999;
        background: url('../download/load.png') 50% 50% no-repeat rgb(249,249,249);
    }
</style>
    <script type="text/javascript">
        $(window).load(function () {
            HideProgressBar();
        });
    </script>
     <script type="text/javascript">
         function ShowProgressBar() {
             document.getElementById('dvProgressBar').style.visibility = 'visible';
             $(".gvNetworkDevicesInformation").hide();
             
         }

         function HideProgressBar() {
             document.getElementById('dvProgressBar').style.visibility = "hidden";
             $(".gvNetworkDevicesInformation").show();
         }
    </script>
     
   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">

    <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box text-right">
                        <h4 class="page-title">
                          مسح الشبكه</h4>
                    </div>
                </div>
        </div>
    <div class="row">
        <div class="col-md-12 block">
            <div class="card-box">
                <div class="form-group col-md-6">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="Label2" runat="server" Text="الفرع"></asp:Label>
                    </label>
                      <div class="col-md-9">
                           <dx:aspxcombobox ID="ddl_CompanyId" dir="rtl" runat="server" 
                                CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  
                                ValueType="System.String" AutoPostBack="True" 
                                onselectedindexchanged="ddl_CompanyId_SelectedIndexChanged">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:aspxcombobox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_CompanyId"></asp:RequiredFieldValidator>
                        </div>
                </div>

                    
                      <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label144" runat="server" Text="عنوان الشبكه"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:aspxcombobox ID="ddl_StaticIP" dir="rtl" runat="server" 
                                CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" 
                                ValueType="System.String">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:aspxcombobox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_StaticIP"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                 
                     <div class="form-group col-md-12">
                         <asp:Button ID="btnSearch" CssClass="btn btn-default" runat="server" Text="بحث" 
                            ValidationGroup="Save" onclick="btnSearch_Click"
                              OnClientClick="javascript:ShowProgressBar()" 
                             />

                        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="حفظ" 
                            ValidationGroup="Save" onclick="btnSave_Click" />
                    </div>
                    
                    
                    <div class="form-group col-md-12">
                        <div id="divMsg" runat="server">
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
            </div>
        </div>
    </div>
    <div class="row">
                   <div class="col-md-12">
            <div class="card-box">
                <asp:GridView  ID="gvNetworkDevicesInformation" CssClass="table m-0 table-colored table-danger gvNetworkDevicesInformation" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="IpAddress">
                    <Columns>
                      <asp:TemplateField>
                          <HeaderTemplate>
                                   المسلسل
                          </HeaderTemplate>
                          <ItemTemplate>
                              <%# Container.DataItemIndex + 1 %>
                          </ItemTemplate>
                      </asp:TemplateField>
                        
                        <asp:BoundField DataField="IpAddress" HeaderText="عنوان الجهاز علي الشبكه" 
                            SortExpression="IpAddress" />
                        
                        <asp:BoundField DataField="MacAddress" HeaderText=" MAC عنوان"
                            SortExpression="MacAddress" />

                        <asp:BoundField DataField="DeviceName" HeaderText="إسم الجهاز"
                            SortExpression="DeviceName" />

                        
                        <asp:BoundField DataField="BranchAccName" HeaderText="إسم الفرع في دليل المواقع" 
                            SortExpression="BranchAccName" />
                        
                        <asp:BoundField DataField="SectionName" HeaderText="القسم"
                            SortExpression="SectionName" />

                            <asp:BoundField DataField="FloorName" HeaderText="الدور"
                            SortExpression="FloorName" />

                              <asp:BoundField DataField="RoomName" HeaderText="الموقع الفرعي"
                            SortExpression="RoomName" />

                              <asp:BoundField DataField="AssetMasterName" HeaderText="إسم المنقول الرئيسي"
                            SortExpression="AssetMasterName" />

                              <asp:BoundField DataField="SubAssetMasterName" HeaderText="إسم المنقول "
                            SortExpression="SubAssetMasterName" />

                         <asp:TemplateField HeaderText="متصل" ItemStyle-Width="100" ItemStyle-HorizontalAlign = "Center">
                                <ItemTemplate>
                                    <asp:Image ImageUrl='<%# "~/Images/" + (Eval("Status")!=null?"A.png" : "P.png") %>' runat="server" Height = "25" Width = "25" />
                                </ItemTemplate>
                         </asp:TemplateField>

                       <asp:TemplateField HeaderText="الإتصال عن بعد بالجهاز">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_REMOTE_CONNECTION"
                                    
                                    runat="server" CssClass="btn btn-default" onclick="lnk_REMOTE_CONNECTION_Click" >
                             <i class="fa fa-download"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
            </div>
    
<br />
<asp:Button Text="تصدير ل إكسيل" runat="server" OnClick="btn_ExportExcel_Click" />
     
    <div id="dvProgressBar" style="float: left; visibility: hidden;">
        <img src="../download/load.gif" />
        <div class="textloading">
        ... جاري المسح على الشبكه للحصول على المعلومات 
            </div>
    </div> 
</asp:Content>



