<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="Streets.aspx.cs" Inherits="BusesWorkshop.Pages.Streets" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

    
    
    
    <asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">


    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    اسماء الشوارع</h4>
            </div>
        </div>
    </div>
    
    
    
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
            
            
                         <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label2" runat="server" Text="اسم الشارع"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_StreetName"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox CssClass="form-control" ID="txt_StreetName" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                     <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label1" runat="server" Text="اسم المدينة"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:aspxcombobox ID="ddl_CityId" dir="rtl" runat="server" 
                                CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  
                                ValueType="System.String" AutoPostBack="True" 
                                onselectedindexchanged="ddl_CityId_SelectedIndexChanged">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:aspxcombobox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_CityId"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                    
                    
                      <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label144" runat="server" Text="اسم الحى"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:aspxcombobox ID="ddl_DistrictId" dir="rtl" runat="server" 
                                CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" 
                                ValueType="System.String" AutoPostBack="True" 
                                onselectedindexchanged="ddl_DistrictId_SelectedIndexChanged" >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:aspxcombobox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_DistrictId"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                    
                    
                    
                     <div class="form-group col-md-12">
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
                <asp:GridView ID="gvStreets" CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="DistrictId">
                    <Columns>
                       <%-- <asp:BoundField DataField="Notes" HeaderText="الملاحظات" />--%>
                        <asp:BoundField DataField="StreetId" HeaderText="مسلسل" Visible="False" />
                        <asp:BoundField DataField="StreetName" HeaderText="اسم الشارع" 
                            SortExpression="StreetName" />
                        <asp:BoundField DataField="DistrictName" HeaderText="اسم الحى" 
                            SortExpression="DistrictName" />
                        <asp:BoundField DataField="CityNAME" HeaderText="اسم المدينة" 
                            SortExpression="CityNAME" />
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
