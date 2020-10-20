<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="ConfigurationPage.aspx.cs" Inherits="BusesWorkshop.Pages.Definitions.ConfigurationPage" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">

    <script src="../../plugins/bootstrap-datepicker/js/MyPlugins.js" type="text/javascript"></script>

<div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    الملفات العامة</h4>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 block">
            <div class="card-box">
                <div class="row">

                        <label class="col-md-2 control-label">
                            <asp:Label ID="Label1" runat="server" Text="التصنيف"></asp:Label>
                        </label>
                        <div class="col-md-4">
                            <dx:ASPxComboBox ID="ddl_MasterId" runat="server" CssClass="form-control"
                                DropDownStyle="DropDown" IncrementalFilteringMode="Contains" 
                                ValueType="System.String" dir="rtl" 
                                AutoPostBack="True" onselectedindexchanged="ddl_MasterId_SelectedIndexChanged">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_MasterId"></asp:RequiredFieldValidator>
                        </div>

                       <label class="col-md-2 control-label">
                            <asp:Label ID="Label9" runat="server" Text="الاسم"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="SaveSpare"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_ConfigDetailName"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-4">
                            <asp:TextBox ID="txt_ConfigDetailName" runat="server" CssClass="form-control"></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_ConfigDetailName"></asp:RequiredFieldValidator>
                        </div>
                  
                </div>
              <div class="clearfix"></div>
                <div  class="row">
                            <label class="col-md-2 control-label">
                                <asp:Label ID="lbl_serviceRequest" runat="server" Text="طلب الخدمه"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*Required" ControlToValidate="RequesServiceId"></asp:RequiredFieldValidator>
                            </label>
                            <div class="col-md-4">
                                    <dx:ASPxComboBox ID="RequesServiceId" dir="rtl" runat="server"  AutoPostBack="true"
                                    CssClass="form-control"  DropDownStyle="DropDown" 
                                    IncrementalFilteringMode="Contains"  ValueType="System.String">
                                    <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                                    <Items>
                                    <dx:ListEditItem Text="طلب صيانه" Value="0" />
                                    <dx:ListEditItem Text="دعم فنى  " Value="1" />
                                    <dx:ListEditItem Text="الكل" Value="2" />
                            </Items>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                            </dx:ASPxComboBox>
                         
                            </div>

                            <label class="col-md-2 control-label">
                                 <asp:Label ID="Label5" runat="server" Text="اسم المنقول الرئيسي "></asp:Label>
                            </label>
                            <div class="col-md-4 text-right" dir="rtl">

                                <dx:ASPxComboBox ID="ddl_AssetMasterId" AutoPostBack="true" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                    IncrementalFilteringMode="Contains" ValueType="System.String"
                                    OnSelectedIndexChanged="ddl_AssetMasterId_SelectedIndexChanged"
                                    dir="rtl" >
                                    <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                                </dx:ASPxComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Save"
                                    Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_AssetMasterId"></asp:RequiredFieldValidator>
                            </div>
                  
               </div>
                    <div class="row">
                            <label class="col-md-2 control-label">
                                 <asp:Label ID="Label2" runat="server" Text="اسم المنقول  "></asp:Label>
                            </label>
                            <div class="col-md-4 text-right" dir="rtl">

                                <dx:ASPxComboBox ID="ddl_SubAssetId" AutoPostBack="true" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                    IncrementalFilteringMode="Contains" ValueType="System.String"
                                    dir="rtl" >
                                    <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                                </dx:ASPxComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" ValidationGroup="Save"
                                    Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_SubAssetId"></asp:RequiredFieldValidator>
                            </div>

                        <div class="row">
                            <div class="col-md-3">
                                  <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="حفظ" 
                            ValidationGroup="SaveModels" onclick="btnSave_Click"/>
                            </div>
                        </div>
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
                <asp:GridView ID="gvConfig" CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="ConfigDetailId">
                    <Columns>
                        <asp:BoundField DataField="ConfigDetailId" HeaderText="مسلسل" Visible="False" />
                        <asp:BoundField DataField="MasterName" HeaderText="التصنيف" />
                        <asp:BoundField DataField="ConfigDetailName" HeaderText="القيمة" /> 
                        <asp:BoundField DataField="MasterAssetName" HeaderText="المنقول الرئيسي" /> 
                        <asp:BoundField DataField="SubAssetName" HeaderText="المنقول" /> 
                        <asp:BoundField DataField="ServiceRequest" HeaderText="طلب الخدمة" /> 

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
            </div>
        </div>
    </div>     
</asp:Content>