<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPages/Master.Master"  CodeBehind="Brands.aspx.cs" Inherits="BusesWorkshop.Pages.Definitions.Brands" %>


<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">

 <!-- Page-Title -->
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    ماركات السيارات</h4>
            </div>
        </div>
    </div>
    <!-- end page title end breadcrumb -->
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <div class="form-horizontal">
                
                
                
                  <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label2" runat="server" Text="اسم الماركة"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_BrandName"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox CssClass="form-control" ID="txt_BrandName" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                            <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label4" runat="server" Text="ملاحظات"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox CssClass="form-control" ID="txt_Notes" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                
                
                
                
                
                <div class="form-group col-md-12">
                        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="حفظ" 
                            ValidationGroup="SaveModels" onclick="btnSave_Click"/>
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
                <asp:GridView ID="gvBrands" CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="BrandId">
                    <Columns>
                        <asp:BoundField DataField="BrandId" HeaderText="مسلسل" Visible="False" />
                        <asp:BoundField DataField="BrandName" HeaderText="اسم الماركة" />
                       <%-- <asp:BoundField DataField="Notes" HeaderText="اسم الماركة" />--%>
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
                                <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="btn btn-default"  OnClick="lnk_Delete_Click" OnClientClick="return confirm('تأكيد الحذف ؟');" >
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