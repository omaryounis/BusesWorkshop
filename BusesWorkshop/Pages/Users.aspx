<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" EnableEventValidation="false"
CodeBehind="Users.aspx.cs" Inherits="BusesWorkshop.Pages.PagesPermissions.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    بيانات المستخدمين</h4>
            </div>
        </div>
    </div>
    
      <div class="pro_co">
     <div class="col-sm-12">
     <div class="col-sm-12 block_st"> 
<div class="form-group col-sm-5">
            <label class="col-sm-4 control-label">
             <asp:Label ID="LabelName" runat="server" Text="اسم الموظف"></asp:Label>
            </label>
            <div class="col-sm-8">  
            <asp:TextBox CssClass="form-control" ID="txtName" runat="server"></asp:TextBox>  
              <asp:RequiredFieldValidator ID="NameRequired" CssClass="star_st" runat="server" 
                ControlToValidate="txtName" ErrorMessage="يرجى إدخال اسم الموظف" 
                ValidationGroup="SaveUserGroup"></asp:RequiredFieldValidator>
            </div>
      </div>
<div class="form-group col-sm-5 hidden">
            <label for="inputEmail3" class="col-sm-4 control-label">
              <asp:Label ID="LabelBnach" runat="server" Text="الفرع"></asp:Label>
            </label>
            <div class="col-sm-8">  
               <asp:DropDownList CssClass="selectpicker show-tick form-control" ID="DropDownListBrnachID" runat="server" AppendDataBoundItems ="true">
    </asp:DropDownList>
    <%--<asp:RequiredFieldValidator ID="BranchRequired" CssClass="star_st" runat="server" 
                ControlToValidate="DropDownListBrnachID" ErrorMessage="* " 
                ValidationGroup="SaveUserGroup"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="SelectBranchValidator" runat="server" 
                ControlToValidate="DropDownListBrnachID" 
                ErrorMessage="يرجى إختيار الفرع المناسب" Operator="GreaterThan" 
                ValidationGroup="SaveUserGroup" ValueToCompare="0"></asp:CompareValidator>--%>
            </div>
      </div>
 <div class="form-group col-sm-5">
            <label for="inputEmail3" class="col-sm-4 control-label">
            <asp:Label ID="LabelUserName" runat="server" Text="اسم المستخدم"></asp:Label>
            </label>
            <div class="col-sm-8">  
           <asp:TextBox CssClass="form-control" ID="txtUserName" runat="server" ></asp:TextBox>    
                 <asp:RequiredFieldValidator ID="UserNameRequired" CssClass="star_st" runat="server" 
                ControlToValidate="txtUserName" ErrorMessage="يرجى إدخال اسم المستخدم" 
                ValidationGroup="SaveUserGroup"></asp:RequiredFieldValidator>
            </div>
      </div>   


  
<div class="form-group col-sm-5">
            <label for="inputEmail3" class="col-sm-4 control-label">
            <asp:Label ID="LabelPassword" runat="server" Text="كلمة المرور"></asp:Label>
            </label>
            <div class="col-sm-8">  
            <asp:TextBox CssClass="form-control" ID="txtPasword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="PasswordRequired" CssClass="star_st" runat="server" 
                ControlToValidate="txtPasword" ErrorMessage="يرجى كتابة كلمة المرور" 
                ValidationGroup="SaveUserGroup"></asp:RequiredFieldValidator>
            </div>
      </div>
<div class="clearfix"></div>
    <div class="form-group col-md-6">
                    
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label13" runat="server" Text=""></asp:Label>
                        </label>
                        <div class="col-md-9 radio-st" dir="rtl">
                        <asp:CheckBox ID="chk_IsActive" runat="server" Text="مفعل" />
                        </div>
        
                    </div>
                    
                    <div class="clearfix"></div>
 <div class="col-sm-12"> 

    <asp:Button ID="btnSave" runat="server" Text="حفظ" CssClass="btn btn-success pull-left" onclick="Button1_Click" 
                 ValidationGroup="SaveUserGroup" />

<div class="clearfix"></div>

 <div id="divMsg2" runat="server" >
    <asp:Label ID="lblResult" runat="server" ></asp:Label>
</div>
   </div> 
<div class="clearfix"></div>
    <%--<asp:Label ID="LabelIsActive" runat="server" Text="موظف نشط"></asp:Label>--%>

</div>
<div class="clearfix"></div>
<br />
<div class="clearfix"></div>
     <div class="col-sm-12 block_st"> 
    <asp:GridView ID="GridViewUser" runat="server" AutoGenerateColumns="False"  CssClass="table table-bordered table-striped table-hover tc-table table-primary footable default footable-loaded"
      HeaderStyle-CssClass="table_header" PagerStyle-CssClass="btn_group_in"  
    onrowcommand="GridViewUser_RowCommand" DataKeyNames="id">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="مسلسل" SortExpression="id" 
                Visible="False" />
            <asp:TemplateField HeaderText="اسم الموظف">
                <ItemTemplate>
                    <asp:Label ID="LabelName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="اسم المستخدم">
                <ItemTemplate>
                    <asp:Label ID="LabelUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="مفعل/غير مفعل">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Eval("IsActive") %>'  Enabled="False" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="تعديل">
                <ItemTemplate>
                    <asp:LinkButton ID="btnEdit" runat="server" 
                        CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-success btn_icon" CommandName="EditRow"><i class="fa fa-edit"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="حذف">
                <ItemTemplate>
                   <asp:LinkButton ID="btnDelete" runat="server" 
                        CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-danger btn_icon" 
                        CommandName = "DeleteRow" onclick="btnDelete_Click"><i class="fa fa-remove"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

<PagerStyle CssClass="btn_group_in"></PagerStyle>

<HeaderStyle CssClass="table_header"></HeaderStyle>
    </asp:GridView>
    </div>
    </div>
    <div class="clearfix"></div>
    </div>
</asp:Content>
