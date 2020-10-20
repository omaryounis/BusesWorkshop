<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="Material.aspx.cs" Inherits="BusesWorkshop.Pages.Material" %>


<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    المبانى</h4>
            </div>
        </div>
    </div>
    
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
            
            
            
            
            
                 <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label1" runat="server" Text="التصنيف"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:ASPxComboBox ID="ddl_MainCategory" runat="server" CssClass="form-control"
                                DropDownStyle="DropDown" IncrementalFilteringMode="Contains" 
                                 ValueType="System.String" dir="rtl" 
                                AutoPostBack="True" 
                                onselectedindexchanged="ddl_MainCategory_SelectedIndexChanged">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_MainCategory"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label9" runat="server" Text="اسم الخامة"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="SaveSpare"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_MaterialName"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_MaterialName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
            
            
            
            
            
            
            
            
            
            
               <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label2" runat="server" Text="سعر الخامة"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="SaveSpare"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_MaterialPrice"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_MaterialPrice" runat="server" CssClass="form-control" 
                                onkeypress="return onlyDotsAndNumbers(event);"></asp:TextBox>
                        </div>
</div>







  <div class="col-md-12">
                        <div class="form-group col-md-12">
                            <asp:Button ID="btnSave" runat="server" Text="حفظ" CssClass="btn btn-success" 
                                ValidationGroup="SaveSpare" onclick="btnSave_Click" 
                                 />
                        </div>
                    </div>
                    
                    
                     <div class="form-group col-md-12">
                        <div id="divMsg2" runat="server">
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                
            </div>
            </div>
            </div>
               <asp:GridView ID="grd_Material" 
                CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="MaterialId" 
                >
                    <Columns>
                        <asp:BoundField DataField="MaterialId" HeaderText="مسلسل" Visible="False" />
                        <asp:BoundField DataField="MaterialName" HeaderText="اسم الخامة" />
                        <asp:BoundField DataField="MaterialPrice" HeaderText="سعر الوحدة" />
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
                                <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="btn btn-default" 
                                    OnClientClick="return confirm('تأكيد الحذف ؟');" 
                                    onclick="lnk_Delete_Click">
                             <i class="fa fa-trash-o"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Content>
