<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="GroupPermission.aspx.cs" Inherits="BusesWorkshop.Pages.PagesPermissions.GroupPermission" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="col-sm-12 ">
        <div class="jumbotron">
            <div class="form-group col-sm-6">
                <label class="col-sm-3 control-label padding_left_none padding_right_none">
                    <asp:Label ID="lblGroups" Text="اسم المجموعة" runat="server"></asp:Label>
                </label>
                <div class="col-sm-8">
                    <asp:DropDownList ID="ddlGroup" runat="server" CssClass="selectpicker show-tick form-control"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-1">
                    <asp:RequiredFieldValidator ControlToValidate="ddlGroup" InitialValue="-1" ValidationGroup="save"
                        ID="rfGroup" runat="server" Text="*" ErrorMessage="rfGroup"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-sm-1">
                <asp:Button runat="server" ValidationGroup="save" ID="btnSave" CssClass="btn btn-success pull-left"
                    Text="حفظ" OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
    <div class="clearfix">
    </div>
    <div class="col-sm-12">
        <asp:GridView runat="server" AutoGenerateColumns="false" ID="gvGroup" CssClass="table table-bordered table-striped table-hover tc-table table-primary footable default footable-loaded"
            HeaderStyle-CssClass="table_header" PagerStyle-CssClass="btn_group_in" OnRowDataBound="gvGroup_RowDataBound">
            <Columns>
                <asp:BoundField DataField="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"
                    FooterStyle-CssClass="hidden" />
                <asp:BoundField DataField="Group_ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"
                    FooterStyle-CssClass="hidden" />
                <asp:BoundField DataField="Page_ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"
                    FooterStyle-CssClass="hidden" />
                <asp:BoundField DataField="PageName" HeaderText="اسم الصفحة" />
                <asp:TemplateField HeaderText="عرض">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkDisplay" runat="server" Checked='<%# Eval("Display") %>' Enabled='<%# check(Eval("Display1")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="اضافه">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkInsert" runat="server" Checked='<%#Bind("InsertA") %>' Enabled='<%# check(Eval("InsertA1")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="تعديل">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkUpdate" runat="server" Checked='<%#Bind("UpdateA") %>' Enabled='<%# check(Eval("UpdateA1")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="حذف">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkDelete" runat="server" Checked='<%#Bind("DeleteA") %>' Enabled='<%# check(Eval("DeleteA1")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="col-sm-12">
    </div>
    <asp:LinkButton ID="hdMessage" runat="server"></asp:LinkButton>
    <asp:Panel ID="pnMessage" runat="server">
        <table style="background-color: #f5f6f7; border: solid 1px #999; text-align: center;
            width: 320px; color: #000; height: 130px; margin: auto; clear: both; float: none;">
            <tr>
                <td colspan="2" valign="middle" style="height: 50px; text-align: center;">
                    <asp:Label ID="lblMessage" runat="server" Text="تم الادخال بنجاح"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 50px; text-align: center;">
                    <asp:Button style="width:auto !important" ID="btnCont" CssClass="btn btn-success" runat="server" Text="الاستمرار" PostBackUrl="~/Pages/GroupPermission.aspx" />
                
                    <asp:Button style="width:auto !important" ID="btnReturn" CssClass="btn btn-primary" runat="server" Text="الرجوع للقائمه"
                        PostBackUrl="~/Pages/Main.aspx" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ModalPopupExtender PopupControlID="pnMessage" TargetControlID="hdMessage" ID="mpeMessage"
        runat="server">
    </asp:ModalPopupExtender>
</asp:Content>
