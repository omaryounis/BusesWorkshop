<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="Groups.aspx.cs" Inherits="BusesWorkshop.Pages.PagesPermissions.Groups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <div class="col-sm-12 ">
    <div runat="server" id="DivAlert" title="Test Message" visible="false" class="alert alert-sm alert-border-left alert-danger light alert-dismissable">
        <button aria-hidden="true" data-dismiss="alert" class="close" type="button">
            <i class="fa fa-remove"></i>
        </button>
        <asp:Label ID="lblResult" runat="server" Visible="False" Text="åÐå ÇáãÌãæÚÉ ãÖÇÝÉ ãä ÞÈá" ValidationGroup="save"></asp:Label>
        <%--          <asp:Label ID="Label1" runat="server" Text="" Visible="False"></asp:Label>
--%>
    </div>
    </div>
    <%-- <asp:Label ID="lblGroups" Text="ÅÓã ÇáãÌãæÚÉ" runat="server"></asp:Label>--%>
    <div class="col-sm-12 ">
        <div class="jumbotron">
            <div class="form-group col-sm-6">
                <label for="inputGroups" class="col-sm-3 control-label padding_left_none padding_right_none">
                    <asp:Label ID="lblGroups" runat="server" Text="ÅÓã ÇáãÌãæÚÉ"></asp:Label>
                </label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtGroup" CssClass="form-control" runat="server" ValidationGroup="save"></asp:TextBox>
                </div>
                <%-- <div ><asp:TextBox ID="txtGroup"  runat="server"></asp:TextBox></div>--%>
                <div class="col-sm-1">
                    <asp:RequiredFieldValidator ControlToValidate="txtGroup" ValidationGroup="save" ID="rfGroup"
                        runat="server" Text="*" ErrorMessage="*" CssClass="star_st"></asp:RequiredFieldValidator>
                </div>
            </div>
            <%--<asp:ValidationSummary ID="vsGroup" ValidationGroup="save" runat="server" />--%>
            
            <div class="form-group col-sm-2">
                <asp:Button ID="btnSave" CssClass="btn btn-success pull-left" runat="server" Text="ÍÝÙ"
                    ValidationGroup="save" OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
    <div class="clearfix">
    </div>
    <%-- <div>
        <asp:Button runat="server" ValidationGroup="save" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
    </div>--%>
    <div class="col-sm-12 ">
        <asp:GridView runat="server" CssClass="table table-bordered table-striped table-hover tc-table table-primary footable default footable-loaded"
            AutoGenerateColumns="False" ID="gvGroup" HeaderStyle-CssClass="table_header"
            PagerStyle-CssClass="btn_group_in">
            <Columns>
                <asp:BoundField DataField="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                <asp:BoundField DataField="Name" HeaderText="ÇáÇÓã" />
                <asp:TemplateField HeaderText="ÊÚÏíá" >
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-success btn_icon" 
                            OnClick="lbtnGroupEdit_Click"><i class="fa fa-edit"></i></asp:LinkButton>
                    </ItemTemplate>
                     </asp:TemplateField>
                <asp:TemplateField HeaderText="ÍÐÝ" >
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-danger btn_icon" 
                            OnClick="lbtnGroupDelete_Click"><i class="fa fa-remove"></i></asp:LinkButton>
                    </ItemTemplate>
                     </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="btn_group_in"></PagerStyle>
            <HeaderStyle CssClass="table_header"></HeaderStyle>
        </asp:GridView>
    </div>
</asp:Content>
