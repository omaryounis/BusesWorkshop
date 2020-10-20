<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="Rec.aspx.cs" Inherits="BusesWorkshop.Pages.Recommendations" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

    
    
    
    <asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">


    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    التوصيات</h4>
            </div>
        </div>
    </div>
    
    
    
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
            
            
                       <div class="row">


                            <label class="col-md-1 control-label">
                            <asp:Label ID="Label3" runat="server" Text="نوع الدعم"></asp:Label>
                            
                        </label>
                        <div class="col-md-1">
                            <asp:RadioButtonList ID="radio_SupportType" RepeatDirection="Vertical" runat="server" 
                                AutoPostBack="true"
                                  OnSelectedIndexChanged="radio_SupportType_SelectedIndexChanged"
                                >
                                <asp:ListItem Text="طلب صيانه" Value="0"></asp:ListItem>
                                <asp:ListItem Text="دعم فني" Value="1" ></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>


                        <label class="col-md-1 control-label">
                            <asp:Label ID="Label2" runat="server" Text="اسم العمل المطلوب"></asp:Label>
                            
                        </label>
                        <div class="col-md-3">
                             <dx:ASPxComboBox ID="ddl_WorkId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                            IncrementalFilteringMode="Contains" ValueType="System.String"
                             AutoPostBack="True" 
                                  OnSelectedIndexChanged="ddl_WorkId_SelectedIndexChanged"
                            dir="rtl">
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                        </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_WorkId"></asp:RequiredFieldValidator>
                        </div>

                       

                           <label class="col-md-1 control-label">
                                <asp:Label ID="Label1" runat="server" Text="التوصية"></asp:Label>
                           </label>
                            <div class="col-md-3 text-right" dir="rtl">

                                <dx:ASPxTextBox ID="txtRecDesc" runat="server" CssClass="form-control" 
                                            ValueType="System.String"
                                    dir="rtl">
                                </dx:ASPxTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Save"
                                    Display="Dynamic" ErrorMessage="*" ControlToValidate="txtRecDesc"></asp:RequiredFieldValidator>
                            </div>
                             <div class="col-md-2">
                                  <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="حفظ" 
                            ValidationGroup="Save" onclick="btnSave_Click" />
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
                <asp:GridView ID="gvRecommendations" CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="RecID">
                    <Columns>
                       <%-- <asp:BoundField DataField="Notes" HeaderText="الملاحظات" />--%>
                        <asp:BoundField DataField="RecID" HeaderText="مسلسل" Visible="False" />
                        <asp:BoundField DataField="ConfigDetailName" HeaderText="اسم العمل" 
                            SortExpression="ConfigDetailName" />
                        <asp:BoundField DataField="SupportType" HeaderText="نوع الدعم" 
                            SortExpression="SupportType" />
                        <asp:BoundField DataField="RecDesc" HeaderText="التوصيه"
                            SortExpression="RecDesc" />
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
