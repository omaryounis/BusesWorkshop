<%@ Page Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="PhasesSetting.aspx.cs" Inherits="BusesWorkshop.Pages.PhasesSetting" Title="تهيئة المراحل" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">  
</asp:ScriptManager>  
        <%--<style type="text/css">  
        .Background  
        {  
            background-color: Black;  
            filter: alpha(opacity=90);  
            opacity: 0.8;  
        }  
        .Popup  
        {  
            background-color: #FFFFFF;  
            border-width: 3px;  
            border-style: solid;  
            border-color: black;  
            padding-top: 10px;  
            padding-left: 10px;  
            width: 400px;  
            height: 350px;  
        }  
        .lbl  
        {  
            font-size:16px;  
            font-style:italic;  
            font-weight:bold;  
        }  
    </style>--%>  
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">تهيئه المراحل</h4>
            </div>
        </div>
    </div>
     

    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <%-- <div class="pro_co">
        <div class="col-sm-12">--%>
            
             <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                        <asp:Label ID="lbl_phaseName" runat="server" Text="اسم المرحله"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*Required" ControlToValidate="txtPhaseName"></asp:RequiredFieldValidator>

                    </label>
                    <div class="col-md-6 text-right" dir="rtl">

                        <asp:TextBox CssClass="form-control" ID="txtPhaseName" runat="server" ValidateRequestMode="Enabled">

                        </asp:TextBox>

                    </div>
                </div>
             <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                        <asp:Label ID="lbl_Order" runat="server" Text=" محدد الترتيب"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="cb_order"></asp:RequiredFieldValidator>
                           <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" 
                            ControlToValidate="cb_order" ErrorMessage="محدد الترتيب يجب أن يكن رقم صحيح" />
                            </label>
                     <div class="col-md-9 text-right" dir="rtl">
                         
                        <asp:TextBox CssClass="form-control" ID="cb_order" runat="server" ValidateRequestMode="Enabled">

                        </asp:TextBox>
                        <%--  <asp:TextBox CssClass="form-control" ID="cb_order" runat="server" ValidateRequestMode="Enabled">

                        </asp:TextBox>--%>
              <%--          <dx:ASPxComboBox ID="cb_order" dir="rtl" runat="server"
                            CssClass="form-control" DropDownStyle="DropDown"
                            IncrementalFilteringMode="Contains" ValueType="System.String">
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            <Items>
                                <dx:ListEditItem Text="" Value="0" />
                                <dx:ListEditItem Text=" الاولى " Value="1" />
                                <dx:ListEditItem Text="الثانيه  " Value="2" />
                                <dx:ListEditItem Text="الثالثه  " Value="3" />


                            </Items>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </dx:ASPxComboBox>
                  --%>
                    </div>

                </div>
                  <div class="clearfix"></div>

             
                     <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                       
                                <asp:Label ID="lbl_serviceRequest" runat="server" Text="طلب الخدمه"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Save"
                                    Display="Dynamic" ErrorMessage="*" ControlToValidate="cb_RequestType"></asp:RequiredFieldValidator>
                            </label>

                          <div class="col-md-9 text-right" dir="rtl">
                                <dx:ASPxComboBox ID="cb_RequestType" dir="rtl" runat="server"
                                    CssClass="form-control" DropDownStyle="DropDown"
                                    IncrementalFilteringMode="Contains" ValueType="System.String">
                                    <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                                    <Items>
                                        <dx:ListEditItem Text="طلب صيانه" Value="0" />
                                        <dx:ListEditItem Text="دعم فنى  " Value="1" />
                                        <dx:ListEditItem Text="الكل" Value="2" />

                                    </Items>
                                    <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                </dx:ASPxComboBox>
                            </div>
                        </div>

                     <div class="form-group col-md-6">
                    
                        <label class="col-md-3 control-label">
                                <asp:Label ID="lbl_isactive" runat="server" Text="الحاله "></asp:Label>

                               
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Save"
                              Display="Dynamic" ErrorMessage="*" ControlToValidate="rdbtn_IsActive"></asp:RequiredFieldValidator>
                            </label>

                         <%--   <div class="col-md-9">--%>
                                    <div class="col-md-9 radio-st" dir="rtl">
                                <dx:ASPxRadioButtonList ID="rdbtn_IsActive" runat="server" Width="178px">
                                    <Items>
                                        <dx:ListEditItem Text="مفعل" Value="0" Selected="true" />
                                        <dx:ListEditItem Text="غير مفعل" Value="1" />

                                    </Items>
                                    <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                </dx:ASPxRadioButtonList>
                         <%--   </div>--%>
                                </div>
                         </div>

                            <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                                <asp:Label ID="lbl_phases" runat="server" Text="المراحل " ></asp:Label>

                               
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Save"
                              Display="Dynamic" ErrorMessage="*" ControlToValidate="rd_Step"></asp:RequiredFieldValidator>
                            </label>

                         <%--   <div class="col-md-9">--%>
                                  <div class="col-md-9 text-right   radio-st" dir="rtl"  >
                                <dx:ASPxRadioButtonList ID="rd_Step" runat="server" Width="178px">
                                    <Items>
                                        <dx:ListEditItem Text="مراحل اعتماد" Value="0" />
                                        <dx:ListEditItem Text="مراحل توزيع" Value="1" />
                                        <dx:ListEditItem Text="مراحل اجراء" Value="2" />

                                    </Items>
                                    <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                </dx:ASPxRadioButtonList>
                         <%--   </div>--%>
                              
                    
                        </div>
                        <div class="form-group col-md-3 col-sm-12 col-xs-12 text-center">
                            <label class="col-md-12 control-label col-sm-12 col-xs-12">
                                <br />
                                <br />
                            </label>
                           
                        <asp:Button ID="btnSave" runat="server" Text="حفظ" CssClass="btn btn-success pull-center" OnClick="BtnSave_Click"
                                ValidationGroup="Save" /> 
                            <br />
                            <div class="clearfix">
                                <br />
                                <br />
                                <div id="divMsg2" runat="server">
                                    <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
      
  
</div>
    <br />
    <br />
    <div class="col-sm-12 block_st">
        <asp:GridView ID="GridViewPhase" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover tc-table table-primary footable default footable-loaded"
            HeaderStyle-CssClass="table_header" PagerStyle-CssClass="btn_group_in"
            DataKeyNames="phases_Id">
            <Columns>
                <asp:BoundField DataField="phases_Id" HeaderText="مسلسل" SortExpression="phases_Id"
                    Visible="False" />
                <asp:TemplateField HeaderText="اسم المرحله">
                    <ItemTemplate>
                        <asp:Label ID="lbl_phaseName" runat="server" Text='<%# Eval("phases_Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="محدد الترتيب ">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Order" runat="server" Text='<%# Eval("Phase_Order") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" الحاله ">
                    <ItemTemplate>
                        <asp:Label ID="lbl_RequestType" runat="server" Text='<%# Eval("requestType") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText=" المراحل ">
                    <ItemTemplate>
                        <asp:Label ID="lbl_step" runat="server" Text='<%# Eval("phase_Step") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="مفعل/غير مفعل">
                    <ItemTemplate>
                        <asp:Label ID="lbl_isActive" runat="server" Text='<%# Eval("IsActive") %>'></asp:Label>
                    </ItemTemplate>

                </asp:TemplateField>
                <asp:TemplateField HeaderText="تعديل">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_Edit" OnClick="BtnEditClick"
                            runat="server" CssClass="btn btn-default">
                             <i class="fa fa-edit"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <%--<asp:TemplateField HeaderText="اضافه مستخدم" >
                    <ItemTemplate>

                         <form id="form1" runat="server">  
<asp:ScriptManager ID="ScriptManager1" runat="server">  
</asp:ScriptManager>  
<asp:Button ID="btnAdduser" runat="server" Text="اضافه " />  
<cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="Button1"  
    CancelControlID="BtnCancel" BackgroundCssClass="Background">  
</cc1:ModalPopupExtender>  
<asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none">  
    <iframe style=" width: 350px; height: 300px;" id="irm1" src="User_Phase.aspx" runat="server"></iframe>  
   <br/>  
    <asp:Button ID="BtnClose" runat="server" Text="Close" />  
</asp:Panel>  
    </form>  
                      <%--  <asp:LinkButton ID="lnk_AddUser" OnClick="BtnAddUser"
                            runat="server" CssClass="btn btn-default">
                            <i class="fa fa-adn"></i>
                        </asp:LinkButton>--%>

<%--                    </ItemTemplate>


                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="حذف" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_Delete" runat="server" OnClick="BtnDeleteClick" CssClass="btn btn-default" OnClientClick="return confirm('تأكيد الحذف ؟');">
                             <i class="fa fa-trash-o"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

            <PagerStyle CssClass="btn_group_in"></PagerStyle>

            <HeaderStyle CssClass="table_header"></HeaderStyle>
        </asp:GridView>
    </div>
</asp:Content>
