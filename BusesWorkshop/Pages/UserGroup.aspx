<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" EnableEventValidation="false"
 CodeBehind="UserGroup.aspx.cs" Inherits="BusesWorkshop.Pages.PagesPermissions.UserGroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
<div runat="server" id="DivAlert" title="Test Message" visible="false" class="alert alert-sm alert-border-left alert-danger light alert-dismissable">
        <button aria-hidden="true" data-dismiss="alert" class="close" type="button">
            <i class="fa fa-remove"></i>
        </button>
        <asp:Label ID="lblResult" runat="server" Text="البيانات المدخلة مضافة من قبل" ValidationGroup="save"></asp:Label>
     
    </div>

     <div class="col-sm-12 ">
        <div class="jumbotron">
        
         <div class="form-group col-sm-5">   
          <label class="col-sm-3 control-label padding_left_none padding_right_none"> 
            <asp:Label ID="lblUserName"  Text="اسم المستخدم" runat="server"></asp:Label>
          </label>  
          <div class="col-sm-8">   
            <asp:DropDownList ID="ddlUser" CssClass="selectpicker show-tick form-control" runat="server"></asp:DropDownList>
          </div>
          <div class="col-lg-1">
            <asp:RequiredFieldValidator ControlToValidate="ddlUser" CssClass="star_st" InitialValue="-1" ValidationGroup="save" ID="rfvUser" runat="server" Text="*"></asp:RequiredFieldValidator>
         </div>
         </div>  
        <div class="form-group col-sm-5">   
          <label class="col-sm-3 control-label padding_left_none padding_right_none">         
            <asp:Label ID="lblGroups" Text="اسم المجموعة" runat="server"></asp:Label>
          </label>  
          <div class="col-sm-8">           
             <asp:DropDownList ID="ddlGroup" CssClass="selectpicker show-tick form-control" runat="server" ></asp:DropDownList>
          </div>
          <div class="col-lg-1">          
            <asp:RequiredFieldValidator CssClass="star_st" ControlToValidate="ddlGroup" InitialValue="-1" ValidationGroup="save" ID="rfvGroup" runat="server" Text="*"></asp:RequiredFieldValidator>
         </div>
         </div>         
         
       <asp:ValidationSummary ID="vsGroup" CssClass="errorStyle" ValidationGroup="save" runat="server" />
        <div class="col-sm-2"> 
            <asp:Button runat="server" CssClass="btn btn-success" ValidationGroup="save" ID="btnSave" Text="حفظ" onclick="btnSave_Click" />
       </div>
       </div>
       </div>
    <div class="clearfix"></div>
     <div class="col-sm-12 ">
                <asp:GridView runat="server" 
                              AutoGenerateColumns="false" 
                              ID="gvUserGroup"
                              CssClass="table table-bordered table-striped table-hover tc-table table-primary footable default footable-loaded"
                              HeaderStyle-CssClass="table_header" 
                              PagerStyle-CssClass="btn_group_in">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                        <asp:BoundField DataField="User_ID"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                        <asp:BoundField DataField="Group_ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden"  />
                        <asp:BoundField DataField="UserName" HeaderText="اسم المستخدم" />
                        <asp:BoundField DataField="GroupName" HeaderText="اسم المجموعة" />
                        <asp:TemplateField HeaderText="تعديل">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lbtnUserGroupEdit_Click" CssClass="btn btn-success btn_icon" ><i class="fa fa-edit"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="حذف">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lbtnUserGroupDelete_Click" CssClass="btn btn-danger btn_icon" ><i class="fa fa-remove"></i></asp:LinkButton> 
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
    </div>        
</asp:Content>
