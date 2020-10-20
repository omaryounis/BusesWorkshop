
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master"  CodeBehind="PeriodicalPlanExecution.aspx.cs" Inherits="BusesWorkshop.Pages.PeriodicalPlanExecution" %>


<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
    
    
    
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<script>
    $(function() {
        //	$.calendars.picker.setDefaults({renderer: $.calendars.picker.themeRollerRenderer}); // Requires jquery.calendars.picker.ext.js
    var calendar = $.calendars.instance('islamic');
    $('[id*=txt_Date]').calendarsPicker({ calendar: calendar });
    });
</script>
    
 <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    الاجراء الدوري</h4>
            </div>
        </div>
    </div>
    
<div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <div class="form-horizontal">
                
                
                
                
                
                 <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label9" runat="server" Text="رقم الفحص الدوري"></asp:Label>
                            
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_NextID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label5" runat="server" Text="قراءة العداد الحالية"></asp:Label>
                            
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_CurrentReading" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                
                
                      <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label3" runat="server" Text="رقم اللوحة"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:ASPxComboBox ID="ddl_VehcleId" dir="rtl" runat="server" 
                                CssClass="form-control"    DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  
                                ValueType="System.String" AutoPostBack="True" 
                                onselectedindexchanged="ddl_VehcleId_SelectedIndexChanged1">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_VehcleId"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                          <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label1" runat="server" Text="خطة الصيانة"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:ASPxComboBox ID="ddl_maintPlanId" dir="rtl" runat="server" 
                                CssClass="form-control" 
                                 DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  
                                ValueType="System.String" AutoPostBack="True" 
                                onselectedindexchanged="ddl_maintPlanId_SelectedIndexChanged">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_maintPlanId"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                
                
                
                
                
                
                
                
                
                
                
                
                
                 <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label24" runat="server" Text="التاريخ"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <div class="input-group">
                                <asp:TextBox ID="txt_Date" CssClass="form-control" 
                                    runat="server" onkeydown="return false;"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_Date"></asp:RequiredFieldValidator>

                            </div>
                            <!-- input-group -->
                        </div>
                    </div>
                
                
                
                
                
                
                
                
                
                  <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label21" runat="server" Text="ملاحظات"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_Notes" TextMode="MultiLine" runat="server" 
                                CssClass="form-control"
                                ></asp:TextBox>
                        </div>
                    </div>
                    
                    
                
                
                
                
                
                
                
                
                
                
                
                </div>
                </div>
                </div>
                </div>





<div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <div class="form-horizontal">














    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    
    <ContentTemplate>
    
        <asp:GridView ID="grd_PeriodicalPlanExecDetail" runat="server" 
                        CssClass="table m-0 table-colored table-danger" 
                        AutoGenerateColumns="False" ShowFooter="True" 
                        onrowcreated="grd_SparParts_RowCreated" 
                        onrowdatabound="grd_SparParts_RowDataBound" 
                        onrowdeleting="grd_SparParts_RowDeleting" 
                            onrowcommand="grd_SparParts_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="العمل">
                                <ItemTemplate>
                                    <dx:ASPxComboBox ID="ddl_maintPlanDetailId" runat="server" 
                                        CssClass="form-control" DropDownButton-Visible="False" enabled="false" 
                                        >
                                        <Border BorderStyle="None" />
                                        <DropDownButton Visible="False">
                                        </DropDownButton>
                                    </dx:ASPxComboBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <dx:ASPxComboBox ID="ddl_maintPlanDetailId" runat="server" 
                                        CssClass="form-control" dir="rtl" DropDownStyle="DropDown" 
                                          IncrementalFilteringMode="Contains" 
                                        
                                        ValueType="System.String">
                                    </dx:ASPxComboBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField HeaderText="التصنيف">
                                <ItemTemplate>
                                    <dx:ASPxComboBox ID="ddl_SpareMainId" runat="server" AutoPostBack="true" 
                                        CssClass="form-control" enabled="false" 
                                        onselectedindexchanged="ddl_SpareMainId_SelectedIndexChanged" DropDownButton-Visible="False">
                                        <Border BorderStyle="None" />
                                    </dx:ASPxComboBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <dx:ASPxComboBox ID="ddl_SpareMainId" AutoPostBack="true"  runat="server" 
                                        CssClass="form-control" DropDownStyle="DropDown" IncrementalFilteringMode="Contains" 
                                 ValueType="System.String" dir="rtl"
                                        onselectedindexchanged="ddl_SpareMainId_SelectedIndexChanged">
                                    </dx:ASPxComboBox>
                                    
                                </FooterTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="قطع الغيار">
                                <ItemTemplate>
                                    <dx:ASPxComboBox ID="ddl_SpareId" runat="server" AutoPostBack="true" 
                                        CssClass="form-control" enabled="false" 
                                        onselectedindexchanged="ddl_SpareId_SelectedIndexChanged" DropDownButton-Visible="False">
                                   <Border BorderStyle="None" />
                                    </dx:ASPxComboBox>
                                </ItemTemplate>
                                <FooterTemplate>
   <%--                                 <dx:ASPxComboBox ID="ddl_SpareIdAdd" AutoPostBack="true"  runat="server" 
                                        CssClass="form-control" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" 
                                EnableIncrementalFiltering="True" ValueType="System.String" dir="rtl"
                                        onselectedindexchanged="ddl_SpareId_SelectedIndexChanged">
                                    </dx:ASPxComboBox>--%>
                                    <dx:ASPxComboBox ID="ddl_SpareIdAdd" runat="server" AutoPostBack="true"  runat="server" 
                                    CssClass="form-control" DropDownStyle="DropDown" IncrementalFilteringMode="Contains"
                                      ValueType="System.String" dir="rtl"
                                        onselectedindexchanged="ddl_SpareId_SelectedIndexChanged">
                                    </dx:ASPxComboBox>
                                     <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-default" 
                                        onclick="LinkButton2_Click" CausesValidation="False"> <span aria-hidden="true" class="glyphicon glyphicon-plus-sign"></span></asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="سعر">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_SparCost" runat="server" AutoPostBack="True" 
                                        CssClass="form-control" enabled="false"  ontextchanged="txt_SparCost_TextChanged" 
                                        Text='<%# Eval("SparCost") %>' BackColor="White" BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_SparCost" runat="server" CssClass="form-control" 
                                        AutoPostBack="true" ontextchanged="txt_SparCost_TextChanged"> </asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="عدد">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_SparCount" runat="server" AutoPostBack="True" enabled="false" 
                                        CssClass="form-control" ontextchanged="txt_SparCount_TextChanged" 
                                        Text='<%# Eval("SparCount") %>' BackColor="White" BorderColor="White" 
                                        BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_SparCount" runat="server"  CssClass="form-control" 
                                        AutoPostBack="True" ontextchanged="txt_SparCount_TextChanged"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="الاجمالى">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_SparTotal" runat="server" CssClass="form-control"  
                                        Enabled="False" Text='<%# Eval("SparTotal") %>' BackColor="White" 
                                        BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_SparTotal" enabled="false"  runat="server"  CssClass="form-control"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="اسم الفنى">
                                <FooterTemplate>
                                    <dx:ASPxComboBox ID="ddl_EmployeeId" runat="server"
                                        CssClass="form-control" dir="rtl" DropDownStyle="DropDown" 
                                         IncrementalFilteringMode="Contains" 
                                        ValueType="System.String">
                                    </dx:ASPxComboBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <dx:ASPxComboBox ID="ddl_EmployeeId" runat="server" AutoPostBack="True" 
                                        CssClass="form-control" DropDownButton-Visible="False" enabled="False" 
                                        onselectedindexchanged="ddl_SpareMainId_SelectedIndexChanged"  ValueType="System.String">
                                        <Border BorderStyle="None" />
                                        <DropDownButton Visible="False">
                                        </DropDownButton>
                                    </dx:ASPxComboBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="الايدى العاملة">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_LaborCost" enabled="false"  runat="server" AutoPostBack="True" 
                                        CssClass="form-control" ontextchanged="txt_LaborCost_TextChanged"  
                                        Text='<%# Eval("LaborCost") %>' BackColor="White" BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_LaborCost" runat="server" CssClass="form-control" 
                                        AutoPostBack="true" ontextchanged="txt_LaborCost_TextChanged"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="الاجمالى الكلى">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_TotalCost" runat="server" CssClass="form-control"  enabled="false" 
                                       Text='<%# Eval("TotalCost") %>' BackColor="White" BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_TotalCost" runat="server"  CssClass="form-control" Enabled="false"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ملاحظات" Visible="False">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_Notes" runat="server" TextMode="MultiLine" 
                                        CssClass="form-control" enabled="false"  
                                         BackColor="White" BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_Notes" runat="server" TextMode="MultiLine"  CssClass="form-control"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:Button ID="btn_deleteFromGrid" runat="server" 
                                        onclick="btn_deleteFromGrid_Click" Text="حذف" CommandName="delete" OnClientClick="return confirm('تأكيد الحذف ؟');"/>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">اضافة</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    
                    
                     <div class="clearfix"></div>
                     <br />
                    <div class="clearfix"></div>
                    
                    <div class="form-group col-md-6">
                        <label class="col-md-5 control-label">
                            <asp:Label ID="Label2" runat="server" Text="اجمالى قطع الغيار"></asp:Label>
                        </label>
                        <div class="col-md-3">
                            <asp:TextBox ID="txt_SparSum" Enabled="false" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    
                       <div class="form-group col-md-6">
                        <label class="col-md-5 control-label">
                            <asp:Label ID="Label10" runat="server" Text="تاريخ اخر صرف لقطعة الغيار"></asp:Label>
                        </label>
                        <div class="col-md-3">
                            <asp:TextBox ID="txt_LastSparPay" Enabled="false" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    
                    <div class="form-group col-md-6">
                        <label class="col-md-5 control-label">
                            <asp:Label ID="Label222" runat="server" Text="اجمالى الايدى العاملة"></asp:Label>
                        <hr />
                        </label>
                        <div class="col-md-3">
                            <asp:TextBox ID="txt_LaborSum" Enabled="false"  runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="clearfix"></div>
                    
                    <div class="form-group col-md-6">
                        <label class="col-md-5 control-label">
                            <asp:Label ID="Label4" runat="server" Text="اجمالى قطع الغيار و الايدى العاملة "></asp:Label>
                        </label>
                        <div class="col-md-3">
                            <asp:TextBox ID="txt_TotalSum" Enabled="false" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                      <asp:Panel ID="pnl_AddSpare" runat="server" Visible="false">
        <div class="over-page">
        </div>
        <div class="modal-dialog" role="document">
            <div class="modal-content modal-content-st">
                <div class="modal-header text-right">
                    <h3>
                        اضافة قطعة غيار</h3>
                </div>
                <div class="modal-body text-center">
                    <strong>
                        <asp:Label ID="lbl_AddSpare" runat="server" Text=""></asp:Label></strong>
                             <div class="form-group col-md-3">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label11" runat="server" Text="اسم قطعة الغيار"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="SaveSpare"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_SpareName"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_SpareName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                        <div class="form-group col-md-3">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label12" runat="server" Text="سعر قطعة الغيار"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="SaveSpare"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_SparePrice"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_SparePrice" runat="server" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                
                
                <div class="form-group col-md-3">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label13" runat="server" Text="تكلفة الايدي العاملة"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="SaveSpare"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_LabourCost"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_LabourCost" runat="server" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
              
                
                <div class="form-group col-md-3">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label14" runat="server" Text="ملاحظات"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="TextBox1" TextMode="MultiLine"  runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="modal-footer text-left" style="">
                    <asp:Button ID="btn_CancelSpare" CssClass="btn btn-danger" runat="server" Text="إلغاء"
                          Width="100px" onclick="btn_CancelSpare_Click"/>
                    <asp:Button ID="btn_ContinueSpare" CssClass="btn btn-success" Width="100px" runat="server" Text="استمرار"
                      ValidationGroup="SaveSpare" onclick="btn_ContinueSpare_Click"/>
                </div>
                   <div class="form-group col-md-12">
                        <div id="pnl_Alert" runat="server">
                            <asp:Label ID="lbl_SparAlert" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
            </div>
        </div>
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>













                    <div class="col-md-12">
                        <div class="form-group col-md-12">
                            <asp:Button ID="btnSave" runat="server" Text="حفظ" CssClass="btn btn-success" ValidationGroup="Save"
                                OnClick="btnSave_Click" />
                        </div>
                    </div>
                    <div class="form-group col-md-12">
                        <div id="divMsg2" runat="server">
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
                <div class="form-horizontal">
                
                       <asp:GridView ID="gvPlans" CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="PeriodicalPlanExcutionId">
                    <Columns>
                        <asp:BoundField DataField="PeriodicalPlanExcutionId" HeaderText="مسلسل" 
                            Visible="False" />
                        <asp:BoundField DataField="PlateNo" HeaderText="رقم اللوحة" />
                        <asp:BoundField DataField="PlanName" HeaderText="اسم خطة الصيانة" />
                       <%-- <asp:BoundField DataField="Notes" HeaderText="اسم الماركة" />--%>
                        <asp:BoundField DataField="Date" HeaderText="التاريخ" />
                        <asp:TemplateField HeaderText="تعديل" Visible="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_Edit" runat="server" CssClass="btn btn-default" onclick="lnk_Edit_Click" 
                                   >
                             <i class="fa fa-edit"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="حذف" Visible="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="btn btn-default" 
                                    >
                             <i class="fa fa-trash-o"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                
                
                </div>
                </div>
                </div>
                </div>
                
                
                
                

  
</asp:Content>