<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Buses.aspx.cs" Inherits="BusesWorkshop.Pages.Buses"
    MasterPageFile="~/MasterPages/Master.Master" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">

    <script language="javascript" type="text/javascript">
        function onlyNumbers(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        // Except only numbers and dot (.) for salary textbox
        function onlyDotsAndNumbers(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 46) {
                return true;
            }
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        var nullText = "اختر اسم المشرف";

        function OnLostFocus(s, e) {
            if (s.GetValue() != "" && s.GetValue() != null)
                return;

            var input = s.GetInputElement();
            input.style.color = "gray";
            input.value = nullText;
        }

        function OnGotFocus(s, e) {
            var input = s.GetInputElement();
            if (input.value == nullText) {
                input.value = "";
                input.style.color = "black";
            }
        }

        function OnInit(s, e) {
            OnLostFocus(s, e);
        }
    </script>

    <!-- Page-Title -->
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    بيانات الحافلات</h4>
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
                            <asp:Label ID="Label3" runat="server" Text="الشركة"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:DropDownList CssClass="form-control" ID="ddlCompanies" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>       
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label8" runat="server" Text="موديل"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:DropDownList ID="ddl_model" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label6" runat="server" Text="السائق"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:DropDownList CssClass="form-control" ID="ddlDrivers" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label1" runat="server" Text="رقم اللوحة"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="SaveBus"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txtPlateNumber"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox CssClass="form-control" ID="txtPlateNumber" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label2" runat="server" Text="اسم المالك"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="SaveBus"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txtOwner"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtOwner" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label4" runat="server" Text="الهيكل"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="SaveBus"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txtBodyDesc"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtBodyDesc" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 p-l-0 control-label" style="font-size: 12px; top: 0px; right: 0px;">
                            <asp:Label ID="Label5" runat="server" Text="تاريخ انتهاء الاستمارة"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="SaveBus"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txtBodyDesc"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <div class="input-group">
                                <asp:TextBox ID="txtRenewDate" CssClass="form-control datepicker" runat="server"
                                    ReadOnly="True"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label7" runat="server" Text="عدد الطلاب"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="SaveBus"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txtStudentsCount"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtStudentsCount" CssClass="form-control" runat="server" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
             
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label9" runat="server" Text="الرقم التسلسلي"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_Serial" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 p-l-0 control-label">
                            <asp:Label ID="Label12" runat="server" Text="تاريخ الفحص"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <div class="input-group">
                                <asp:TextBox ID="txt_PeriodicalInspectionDateExpiry" CssClass="form-control datepicker"
                                    runat="server" ReadOnly="True"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label11" runat="server" Text="رقم الاستمارة"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_DocumentNo" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label10" runat="server" Text="اسم المشرف"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:ASPxComboBox ID="ddl_SuperVisorId1" runat="server" CssClass="form-control" OnDataBound="ddl_SuperVisorId1_DataBound"
                                DropDownStyle="DropDown" IncrementalFilteringMode="Contains" 
                                 ValueType="System.String" dir="rtl">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label13" runat="server" Text="مرفقات السيارة"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:GridView ID="GrdAttAttment" CssClass="table m-0 table-bordered"
                                runat="server" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="GrdAttAttment_RowDataBound1"
                                OnRowDeleting="GrdAttAttment_RowDeleting">
                                <RowStyle HorizontalAlign="Right" />
                                <Columns>
                                    <asp:TemplateField HeaderText="اسم المرفق">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_AttachmentName" runat="server" Text='<%# Eval("AttachmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox CssClass="form-control" ID="txt_AttachmnentName" textmode="MultiLine"  runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ملاحظات">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Notes" runat="server" Text='<%# Eval("Notes") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt_Notes" textmode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:LinkButton ID="Lnk_Save" runat="server" Style="margin-right: 3px;" OnClick="LinkButton1_Click">ادخال</asp:LinkButton>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" DeleteText="حذف" />
                                </Columns>
                                <HeaderStyle HorizontalAlign="Right" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="form-group col-md-12">
                        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="حفظ" OnClick="btnSave_Click"
                            ValidationGroup="SaveBus" />
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
                <asp:GridView ID="gvBuses" CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="false" DataKeyNames="ID">
                    <Columns>
                        <asp:BoundField DataField="PlateNumber" HeaderText="رقم اللوحة" />
                        <asp:BoundField DataField="BodyDesc" HeaderText="الهيكل" />
                        <asp:BoundField DataField="StudentsCount" HeaderText="عدد الطلاب" />
                        <asp:BoundField DataField="RenewDate" HeaderText="تاريخ التجديد" DataFormatString=" {0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="OwnerName" HeaderText="المالك" />
                        <asp:TemplateField HeaderText="تعديل">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_Edit" runat="server" CssClass="btn btn-default" OnClick="lnk_Edit_Click">
                             <i class="fa fa-edit"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
