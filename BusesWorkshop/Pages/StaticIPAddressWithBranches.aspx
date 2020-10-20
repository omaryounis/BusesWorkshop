<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="StaticIPAddressWithBranches.aspx.cs" Inherits="BusesWorkshop.Pages.StaticIPAddressWithBranches" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

    
    
    
    <asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">


    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                  تهيئة عنوان الشبكه للفروع</h4>
            </div>
        </div>
    </div>
    
    
    
    <div class="row">
        <div class="col-md-12 block">
            <div class="card-box">


                <div class="form-group col-md-6">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="Label2" runat="server" Text="عنوان الشبكه"></asp:Label>
                       
                    </label>
                    <div class="col-md-2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_StaticIP3"></asp:RequiredFieldValidator>

                        <asp:TextBox CssClass="form-control" MaxLength="3" ID="txt_StaticIP3" runat="server"></asp:TextBox>
                    
                    
                        <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" 
                            ControlToValidate="txt_StaticIP3" ErrorMessage="يجب أن يكون رقم صحيح" />
                    </div>
                    <div class="col-md-2">

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_StaticIP2"></asp:RequiredFieldValidator>

                        <asp:TextBox CssClass="form-control" MaxLength="3" ID="txt_StaticIP2" runat="server"></asp:TextBox>
                    
                        
                        <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" 
                            ControlToValidate="txt_StaticIP2" ErrorMessage="يجب أن يكون رقم صحيح" />
                    </div>
                    
                    <div class="col-md-2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_StaticIP1"></asp:RequiredFieldValidator>
                        
                        <asp:TextBox CssClass="form-control" MaxLength="3" ID="txt_StaticIP1" runat="server"></asp:TextBox>
                    
                        <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" 
                            ControlToValidate="txt_StaticIP1" ErrorMessage="يجب أن يكون رقم صحيح" />

                    </div>
                </div>






                <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label1" runat="server" Text="الفرع"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:aspxcombobox ID="ddl_CompanyID" dir="rtl" runat="server"
                                CssClass="form-control" DropDownStyle="DropDown"
                                ValueType="System.String" 
                                >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:aspxcombobox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_CompanyID"></asp:RequiredFieldValidator>
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
                <asp:GridView ID="gvStaticIPAddressWithBranches" CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="ID">
                    <Columns>
                       <%-- <asp:BoundField DataField="Notes" HeaderText="الملاحظات" />--%>
                        <asp:BoundField DataField="ID" HeaderText="مسلسل" Visible="False" />
                        <asp:BoundField DataField="CompName" HeaderText="اسم الفرع" 
                            SortExpression="CompName" />
                        <asp:BoundField DataField="StaticIP" HeaderText="عنوان الشبكه" 
                            SortExpression="StaticIP" />
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
