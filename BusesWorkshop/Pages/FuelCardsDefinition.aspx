<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="FuelCardsDefinition.aspx.cs" Inherits="BusesWorkshop.Pages.FuelCardsDefinition" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    كروت الوقود</h4>
            </div>
        </div>
    </div>



  <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <div class="form-horizontal">
                
                
                
                
                 <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label3" runat="server" Text="نوع الوقود"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_FuelId"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <dx:ASPxComboBox ID="ddl_FuelId" dir="rtl" runat="server" 
                                CssClass="form-control"  
                                 DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" ValueType="System.String">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            
                        </div>
                    </div>
                
                
                
                
                     <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label1" runat="server" Text="اسم الكارت"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_CardName"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox CssClass="form-control" ID="txt_CardName" runat="server"></asp:TextBox>
                        </div>
                    </div>
                
                
                
                
                
                
                
                       <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label2" runat="server" Text="عدد اللترات"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_Litres"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox CssClass="form-control" ID="txt_Litres" runat="server" 
                                onkeypress="return onlyDotsAndNumbers(event);" AutoPostBack="True" 
                                ontextchanged="txt_Litres_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                
                
                
                
                
                <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label6" runat="server" Text="قيمة اللتر"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_Price"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox CssClass="form-control" ID="txt_LitrePrice" runat="server" 
                                onkeypress="return onlyDotsAndNumbers(event);" AutoPostBack="True" 
                                ontextchanged="txt_LitrePrice_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    
                
                
                
                  <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label4" runat="server" Text="قيمة الكارت"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_Price"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox CssClass="form-control" ID="txt_Price" runat="server" 
                                onkeypress="return onlyDotsAndNumbers(event);" Enabled="False"></asp:TextBox>
                        </div>
                    </div>
                
                
                
                
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label5" runat="server" Text="ملاحظات"></asp:Label>
                           
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox CssClass="form-control" ID="txt_Notes" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                
                
                
                
                
                
                
                <div class="form-group col-md-12">
                        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="حفظ" 
                            ValidationGroup="Save" onclick="btnSave_Click"/>
                    </div>
                    
                    
                    <div class="form-group col-md-12">
                        <div id="divMsg" runat="server">
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                </div>
            </div>
        </div>
  </div>
                
<div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <asp:GridView ID="gvFuelCards" CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="FuelCardId">
                    <Columns>
                        <asp:BoundField DataField="FuelCardId" HeaderText="مسلسل" Visible="False" />
                        <asp:BoundField DataField="FuelName" HeaderText="نوع الوقود" />
                        <asp:BoundField DataField="CardName" HeaderText="اسم الكارت" />
                        <asp:BoundField DataField="Litres" HeaderText="عدد اللترات" />
                        <asp:BoundField DataField="Price" HeaderText="السعر" />
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

