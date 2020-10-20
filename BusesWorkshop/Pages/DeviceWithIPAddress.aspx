<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" 
    CodeBehind="DeviceWithIPAddress.aspx.cs" 
    Inherits="BusesWorkshop.Pages.DeviceWithIPAddress" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
    <style>
    label{
        font-size:10px !important;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box text-right">
                        <h4 class="page-title">
                          ربط العنوان بالمنقول</h4>
                    </div>
                </div>
        </div>

       <div class="card-box block">
            <div class="row" ><!-- style="width:50%;margin:0 auto !important;margin-bottom:10px !important;" !-->
             <div class="col-md-4" > 
              </div>
                    <label class="col-md-1 control-label">
                        <asp:Label ID="Label17" runat="server" Text="اسم الفرع"></asp:Label>
                    </label>
                    <div class="col-md-3" > <!--style="width:80%"-->
                        <dx:aspxcombobox ID="ddl_companyId" dir="rtl" runat="server" 
                            CssClass="form-control" DropDownStyle="DropDown"
                            IncrementalFilteringMode="Contains" 
                            ValueType="System.String"  
                            OnSelectedIndexChanged="ddl_companyid_selectedindexchanged"
                                AutoPostBack="true" 
                            >
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                        </dx:aspxcombobox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_companyId">

                        </asp:RequiredFieldValidator>
                    </div>
               </div>

           <br />
           <div class="row" > <!--style="width:40%;float:right;"-->
                <label class="col-md-1 control-label">
                    <asp:Label ID="Label1" runat="server" Text="إسم الفرع في دليل المواقع"></asp:Label>
                </label>
                <div class="col-md-3" ><!--style="width:65%"-->
                    <dx:aspxcombobox ID="ddl_BranchIdInAcc" dir="rtl" runat="server" 
                        CssClass="form-control" DropDownStyle="DropDown"
                        IncrementalFilteringMode="Contains" 
                        ValueType="System.String" OnSelectedIndexChanged="ddl_BranchIdInAcc_SelectedIndexChanged" AutoPostBack="true" >
                        <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                    </dx:aspxcombobox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Save"
                        Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_BranchIdInAcc"></asp:RequiredFieldValidator>
                </div>
                <label class="col-md-1 control-label" >
                            <asp:Label ID="Label18" runat="server" Text="القسم"></asp:Label>
                        </label>
                <div class="col-md-3" > <!--style="width:80%"-->
                <dx:aspxcombobox ID="ddl_SectionId" dir="rtl" runat="server" 
                    CssClass="form-control" DropDownStyle="DropDown"
                    IncrementalFilteringMode="Contains" 
                    ValueType="System.String" OnSelectedIndexChanged="ddl_SectionId_SelectedIndexChanged" AutoPostBack="true" >
                    <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                </dx:aspxcombobox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="Save"
                    Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_SectionId"></asp:RequiredFieldValidator>
            </div>
                <label class="col-md-1 control-label" >
                            <asp:Label ID="Label19" runat="server" Text="الدور"></asp:Label>
                        </label>
                <div class="col-md-3"> <!-- style="width:80%"-->
                            <dx:aspxcombobox ID="ddl_FloorId" dir="rtl" runat="server" 
                                CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" 
                                ValueType="System.String" OnSelectedIndexChanged="ddl_FloorId_SelectedIndexChanged" AutoPostBack="true" >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:aspxcombobox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_FloorId"></asp:RequiredFieldValidator>
                        </div>
         </div>   
  
           <br />
    <div class="row" > <!--style="width:30%;float:right;"-->
          <label class="col-md-1 control-label"> <!-- style="padding-left:80px;"-->
                            <asp:Label ID="Label20" runat="server" Text="الموقع الفرعي"></asp:Label>
                        </label>
          <div class="col-md-3"> <!-- style="width:65%;"-->
                            <dx:aspxcombobox ID="ddl_RoomId"  AutoPostBack="true"   
                                OnSelectedIndexChanged="ddl_RoomId_SelectedIndexChanged" dir="rtl" runat="server" 
                                CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" 
                                ValueType="System.String">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:aspxcombobox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_RoomId"></asp:RequiredFieldValidator>
                        </div>
        
          <label class="col-md-1 control-label">
                            <asp:Label ID="Label3" runat="server" Text="اسم المنقول الرئيسى"></asp:Label>
                        </label>
          <div class="col-md-3 text-right" dir="rtl" ><!--style="width:58%;"-->
                           
                            <dx:ASPxComboBox ID="ddl_AssetMasterId"  AutoPostBack="true" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl" 
                                OnSelectedIndexChanged="ddl_AssetMasterId_SelectedIndexChanged"  >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_AssetMasterId"></asp:RequiredFieldValidator>
                        </div>
        
          <label class="col-md-1 control-label">
                                        <asp:Label ID="Label5" runat="server" Text="اسم المنقول "></asp:Label>
                                    </label>
          <div class="col-md-3 text-right" dir="rtl"><!-- style="width:68%;"-->
                           
                                        <dx:ASPxComboBox ID="ddl_SubAssetId"   AutoPostBack="true" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                            IncrementalFilteringMode="Contains" ValueType="System.String"
                                            dir="rtl"
                                            >
                                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                                        </dx:ASPxComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Save"
                                            Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_SubAssetId"></asp:RequiredFieldValidator>
                                    </div>
                 
        </div>

    <br />
     <div class="row" > <!---style="width:30%;float:right;"-->
            </div>
            
  <br />
       
           </div>

         <div class="card-box block">
        <div class="row">
            <label class="col-md-2 control-label" > <!--style="padding-top: 15px !important;"-->
                <asp:Label ID="Label144" runat="server" Text="عنوان الشبكه الثابت"></asp:Label>
            </label>
            <div class="col-md-4"> <!-- style="width:20%;"-->
                <dx:aspxcombobox ID="ddl_StaticIP" dir="rtl" runat="server" 
                    CssClass="form-control" DropDownStyle="DropDown"
                    IncrementalFilteringMode="Contains" 
                      OnSelectedIndexChanged="ddl_StaticIP_selectedindexchanged"
                        AutoPostBack="true" 
                    ValueType="System.String">
                    <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                </dx:aspxcombobox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Save"
                    Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_StaticIP"></asp:RequiredFieldValidator>
            </div>

              <label class="col-md-2 control-label" ><!--style="padding-top: 15px !important;"-->
                <asp:Label ID="Label2" runat="server" Text="عنوان الشبكه"></asp:Label>
            </label>
            <div class="col-md-4" ><!--style="width:20%;"-->
                <dx:aspxcombobox ID="ddl_IPAddress" dir="rtl" runat="server" 
                    CssClass="form-control" DropDownStyle="DropDown"
                    IncrementalFilteringMode="Contains" 
                    ValueType="System.String">
                    <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                </dx:aspxcombobox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Save"
                    Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_IPAddress"></asp:RequiredFieldValidator>
            </div>

       
            
           
                   <label class="col-md-2 control-label" > <!--style="padding-top: 15px !important;"-->
                        <asp:Label ID="Label4" runat="server" Text="عنوان الماك"></asp:Label>
                       
                    </label>
                    <div class="col-md-4" > <!--style="width:20%;"-->
                        <asp:TextBox CssClass="form-control"  ID="txt_MacAddress" runat="server"></asp:TextBox>
                    </div>
            
            
 
    
        <div class="col-md-12" >  <!--style="width:auto;padding:0px;margin-bottom:10px;"-->
           
   
                    <div class="form-group col-md-12"><!--style="width:auto;padding:0px;margin:0px;margin-top:-10px;"-->
                        <asp:Button ID="Save" 
                            CssClass="btn btn-success" runat="server" Text="حفظ" 
                            ValidationGroup="SaveModels" Width="150px" onclick="btnSave_Click" />
                    </div> 
                      <div class="form-group col-md-12"> <!--style="width:auto;padding:0px;margin:0px;"-->
                        <div id="divMsg" runat="server">
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
        </div>
         </div> </div>
              <asp:GridView ID="IPAddressWithDeviceGrid" CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="ID">
                    <Columns> 
                        <asp:BoundField DataField="ID" HeaderText="مسلسل" Visible="False" />
                        <asp:BoundField DataField="CompName" HeaderText="اسم الفرع" 
                            SortExpression="CompName" />

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

                              <asp:BoundField DataField="IpAddress" HeaderText="عنوان الجهاز علي الشبكه "
                            SortExpression="IPAddress" />

                              <asp:BoundField DataField="MacAddress" HeaderText="عنوان الماك "
                            SortExpression="MacAddress" />


                        <asp:TemplateField HeaderText="تعديل">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_Edit" runat="server" CssClass="btn btn-default" onclick="lnk_Edit_Click" 
                                     >
                             <i class="fa fa-edit"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="حذف">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="btn btn-default" onclick="lnk_Delete_Click" OnClientClick="return confirm('تأكيد الحذف ؟');" >
                             <i class="fa fa-trash-o"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
 
</asp:Content>
