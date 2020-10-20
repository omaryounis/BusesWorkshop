<%@ Page Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true"
    CodeBehind="UserCompanies.aspx.cs" Inherits="BusesWorkshop.Pages.UserCompanies"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    صلاحيات المستخدمين</h4>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <div class="form-horizontal">
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label1" runat="server" Text="المستخدم"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:DropDownList ID="ddlUsers" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <br />
                    <div class="clearfix"></div>
                    <div class="form-group col-md-12">
                        <label class="col-md-1 control-label">
                            <asp:Label ID="Label2" runat="server" Text="الشركات"></asp:Label>
                        </label>
                        <div class="col-md-11">
                    <asp:CheckBoxList ID="chkCompanies" CssClass="CheckBoxList-edit" runat="server" RepeatDirection="Vertical" RepeatColumns="4">
                    </asp:CheckBoxList>
                      </div> 
                    </div>
                    <div class="clearfix"></div>
                    <hr />
                    <div class="clearfix"></div>
                    <div >
                    </div>
                    <div class="form-group col-md-12">
                        <asp:Button ID="btnSave" runat="server" Text="حفظ" OnClick="btnSave_Click" CssClass="btn btn-success" />
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
    <asp:GridView ID="gvUsers" runat="server" CssClass="table m-0 table-colored table-danger"
    DataKeyNames="UserID" AutoGenerateColumns="false"> <Columns> <asp:BoundField DataField="UserID"
    Visible="false" /> <asp:BoundField DataField="UserName" HeaderText="اسم المستخدم"
    /> <asp:TemplateField> <ItemTemplate> <asp:LinkButton ID="lnk_Edit" runat="server"
    CssClass="btn btn-default" OnClick="lnk_Edit_Click"> <i class="fa fa-edit"></i>
    </asp:LinkButton> </ItemTemplate> </asp:TemplateField> </Columns> </asp:GridView>
</asp:Content>
