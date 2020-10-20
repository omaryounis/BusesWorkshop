<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpareParts.aspx.cs" MasterPageFile="~/MasterPages/Master.Master" Inherits="BusesWorkshop.Pages.SpareParts" %>
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
        var nullText = "----«Œ —----";

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
<div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    ﬁÿ⁄ «·€Ì«—</h4>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <div class="form-horizontal">
                
                <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label1" runat="server" Text="ﬁÿ⁄… «·€Ì«— «·—∆Ì”Ì…"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:ASPxComboBox ID="ddl_MainCategory" runat="server" CssClass="form-control"
                                DropDownStyle="DropDown" IncrementalFilteringMode="Contains" 
                                ValueType="System.String" dir="rtl">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_MainCategory"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                    
                    
                    
                  <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label9" runat="server" Text="«”„ ﬁÿ⁄… «·€Ì«—"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="SaveSpare"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_SpareName"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_SpareName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                
                      
                    
                    
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label5" runat="server" Text="«·„«—ﬂ…"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:ASPxComboBox ID="ddl_BrandId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl" AutoPostBack="True" 
                                onselectedindexchanged="ddl_BrandId_SelectedIndexChanged">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_BrandId"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                    
                    
                    
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label6" runat="server" Text="«·„ÊœÌ·"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:ASPxComboBox ID="ddl_ModelId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl" AutoPostBack="True" 
                                onselectedindexchanged="ddl_ModelId_SelectedIndexChanged">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_ModelId"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                    
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label7" runat="server" Text="«·ÿ—«“"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:ASPxComboBox ID="ddl_ClassId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl" AutoPostBack="True" 
                                onselectedindexchanged="ddl_ClassId_SelectedIndexChanged">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_ClassId"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                    
                    
                    
                   <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label2" runat="server" Text="”⁄— ﬁÿ⁄… «·€Ì«—"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="SaveSpare"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_SparePrice"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_SparePrice" runat="server" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                
                
                <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label4" runat="server" Text=" ﬂ·›… «·«ÌœÌ «·⁄«„·…"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="SaveSpare"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_LabourCost"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_LabourCost" runat="server" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
              
                
                <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label3" runat="server" Text="„·«ÕŸ« "></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_Notes" TextMode="MultiLine"  runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    <div class="col-md-12">
                        <div class="form-group col-md-12">
                            <asp:Button ID="btnSave" runat="server" Text="Õ›Ÿ" CssClass="btn btn-success" 
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
                <asp:GridView ID="grd_SpareParts" 
                CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="SpareId" 
                onselectedindexchanged="grd_SpareParts_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="SpareId" HeaderText="„”·”·" Visible="False" />
                        <asp:BoundField DataField="SpareName" HeaderText="«”„ ﬁÿ⁄… «·€Ì«—" />
                        <asp:BoundField DataField="BrandName" HeaderText="«·„«—ﬂ…" />
                        <asp:BoundField DataField="ModelName" HeaderText="«·„ÊœÌ·" />
                        <asp:BoundField DataField="ClassName" HeaderText="«·ÿ—«“" />
                        <asp:BoundField DataField="SparePrice" HeaderText="”⁄— «·ÊÕœ…" />
                        <asp:BoundField DataField="LabourCost" HeaderText=" ﬂ·›… «·«ÌœÏ «·⁄«„·…" />
                        <asp:TemplateField HeaderText=" ⁄œÌ·">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_Edit" runat="server" CssClass="btn btn-default" 
                                    onclick="lnk_Edit_Click">
                             <i class="fa fa-edit"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Õ–›">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="btn btn-default" 
                                    onclick="lnk_Edit_Click1" OnClientClick="return confirm(' √ﬂÌœ «·Õ–› ø');">
                             <i class="fa fa-trash-o"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
        </div>
    </div>



<div class="row">
        <div class="col-md-12">
            <div class="card-box">
            </div>
        </div>
    </div>
</asp:Content>
