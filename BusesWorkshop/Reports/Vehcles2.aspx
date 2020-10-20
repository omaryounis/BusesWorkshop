<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="Vehcles2.aspx.cs" Inherits="BusesWorkshop.Reports.Vehcles2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraReports.v18.1.Web, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
       

<script>
    $(function() {
      
    var calendar = $.calendars.instance('islamic');
    $('[id*=txt_EndOperationDateEnd]').calendarsPicker({ calendar: calendar });
});

$(function() {

    var calendar = $.calendars.instance('islamic');
    $('[id*=txt_EndOperationDateStart]').calendarsPicker({ calendar: calendar });
});


$(function() {

    var calendar = $.calendars.instance('islamic');
    $('[id*=txt_StartOperationDateEnd]').calendarsPicker({ calendar: calendar });
});

$(function() {

    var calendar = $.calendars.instance('islamic');
    $('[id*=txt_StartOperationDateStart]').calendarsPicker({ calendar: calendar });
});

</script>

    <div class="row">
        
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    تقرير بيانات السيارات 2</h4>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
            
            
            
            
            
            
            
            
              <div class="form-group col-md-4">
                    <label class="col-md-4 control-label">
                        <asp:Label ID="Label1" runat="server" Text="رقم اللوحة"></asp:Label>
                    </label>
                    <div class="col-md-8">
                        <dx:ASPxComboBox ID="ddl_VehcleId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                            IncrementalFilteringMode="Contains"  ValueType="System.String"
                            dir="rtl" >
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                        </dx:ASPxComboBox>
                    </div>
                </div>
                
                
                
                
                
                
                
            
               <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label23" runat="server" Text="الشركة"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <dx:ASPxComboBox ID="ddl_CompanyId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl" >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            
                        </div>
                    </div>
            
            
            
            
            
            
            
            
            
            
            
            
             <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label2" runat="server" Text="نوع الوقود"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <dx:ASPxComboBox ID="ddl_FuelId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl" >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            
                        </div>
                    </div>
            
            
            
            
            
            
            
            
            
            
            <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label22" runat="server" Text="رقم الماتور"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <asp:TextBox ID="txt_MotorNo" runat="server" CssClass="form-control" ></asp:TextBox>
                            
                            
                            
                            
                             <cc1:AutoCompleteExtender ServiceMethod="SearchMotors"
    MinimumPrefixLength="1"
    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
    TargetControlID="txt_MotorNo"
    ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false">
</cc1:AutoCompleteExtender>    
                        </div>
                    </div>
                    
                    
            
            
            
            
              <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label3" runat="server" Text="رقم الهيكل"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <asp:TextBox ID="txt_BodyNo" runat="server" CssClass="form-control" ></asp:TextBox>
                            
                            
                            
                            
                             <cc1:AutoCompleteExtender ServiceMethod="SearchBodies"
    MinimumPrefixLength="1"
    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
    TargetControlID="txt_BodyNo"
    ID="AutoCompleteExtender2" runat="server" FirstRowSelected = "false">
</cc1:AutoCompleteExtender>    
                        </div>
                    </div>
            
            
            
            
            
            
            
              <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label4" runat="server" Text="CC"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <dx:ASPxComboBox ID="ddl_CC" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl" >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                     <div class="clearfix"></div>
                    
                     <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label9" runat="server" Text="اللون"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <dx:ASPxComboBox ID="ddl_ColorId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"   ValueType="System.String"
                                dir="rtl" >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            
                        </div>
                    </div>
                
                    
                     <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label10" runat="server" Text="عدد الاسطوانات"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <dx:ASPxComboBox ID="ddl_CylinderId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl" >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            
                        </div>
                    </div>
                    
                    
                    
                    
                    
                      <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label16" runat="server" Text="بلد الصنع"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <dx:ASPxComboBox ID="ddl_ManufacturingCountryId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl" >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            
                        </div>
                    </div>
                    
                    
                    
                    <div class="clearfix"></div>
                    
                    
                         <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label5" runat="server" Text="من قراءة عداد بداية التشغيل"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <asp:TextBox ID="txt_CounterReadingStartSart" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                       <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label6" runat="server" Text="الى قراءة عداد بداية التشغيل"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <asp:TextBox ID="txt_CounterReadingStartEnd" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    <div class="clearfix"></div>
                    
                    
                    
                     <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label7" runat="server" Text="من متوسط استهلاك وقود"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <asp:TextBox ID="txt_AverageFuelConsumptionStart" runat="server" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                       <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label8" runat="server" Text="الى متوسط استهلاك وقود"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <asp:TextBox ID="txt_AverageFuelConsumptionEnd" runat="server" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    
                   
                    
                    
                    <div class="clearfix"></div>
                    
                    
                    
                    
                    
                     <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label11" runat="server" Text="من سنة صنع"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <asp:TextBox ID="txt_ManufactureYearStart" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                       <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label12" runat="server" Text="الى سنة صنع"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <asp:TextBox ID="txt_ManufactureYearEnd" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    <div class="clearfix" > </div>
                    
                    
                     <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label25" runat="server" Text="تاريخ بدأ التشغيل من"></asp:Label>
                        </label>
                        <div class="col-md-8">
                            <div class="input-group">
                                <asp:TextBox ID="txt_StartOperationDateStart" CssClass="form-control" 
                                    runat="server" OnKeyPress="return false"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                            <!-- input-group -->
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                     <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label13" runat="server" Text="تاريخ بدأ التشغيل الى"></asp:Label>
                        </label>
                        <div class="col-md-8">
                            <div class="input-group">
                                <asp:TextBox ID="txt_StartOperationDateEnd" CssClass="form-control" 
                                    runat="server" OnKeyPress="return false"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                            <!-- input-group -->
                        </div>
                    </div>        
                    
                    
                    
                    
                    
                    <div class="clearfix"></div>
                    
                    
                    
                    
                     <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label14" runat="server" Text="تاريخ انتهاء التشغيل من"></asp:Label>
                        </label>
                        <div class="col-md-8">
                            <div class="input-group">
                                <asp:TextBox ID="txt_EndOperationDateStart" CssClass="form-control" 
                                    runat="server" OnKeyPress="return false"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                            <!-- input-group -->
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                     <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label15" runat="server" Text="تاريخ انتهاء التشغيل الى"></asp:Label>
                        </label>
                        <div class="col-md-8">
                            <div class="input-group">
                                <asp:TextBox ID="txt_EndOperationDateEnd" CssClass="form-control" 
                                    runat="server" OnKeyPress="return false"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                            <!-- input-group -->
                        </div>
                    </div>    
                    
                    
                    
                    <div class="clearfix"></div>
                      <div class="form-group col-md-6">
                    
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label17" runat="server" Text=""></asp:Label>
                        </label>
                        <div class="col-md-9 radio-st" dir="rtl">
                        <asp:CheckBox ID="chk_IsActive" runat="server" Text="مفعل" />
                        <asp:CheckBox ID="chk_IsInActive" runat="server" Text="غير مفعل" />
                        </div>
        
                    </div>
                     <div class="clearfix"></div>
                    
                    
                    <div class="form-group col-md-12 text-left">
                    <asp:Button ID="btnSearch" runat="server" Text="بحث" CssClass="btn btn-success" onclick="btnSearch_Click" 
                          />
                </div>
                
                
                
                
                    
            </div>

                                      
                    <dx:ReportToolbar ID="ReportToolbar1" runat='server' ShowDefaultButtons='False' Width="100%" CssClass="report-st" 
        ReportViewer="<%# ReportViewer1 %>">
        <Items>
            <dx:ReportToolbarButton ItemKind='Search' />
            <dx:ReportToolbarSeparator />
            <dx:ReportToolbarButton ItemKind='PrintReport' />
            <dx:ReportToolbarButton ItemKind='PrintPage' />
            <dx:ReportToolbarSeparator />
            <dx:ReportToolbarButton Enabled='False' ItemKind='FirstPage' />
            <dx:ReportToolbarButton Enabled='False' ItemKind='PreviousPage' />
            <dx:ReportToolbarLabel ItemKind='PageLabel' />
            <dx:ReportToolbarComboBox ItemKind='PageNumber' Width='65px'>
            </dx:ReportToolbarComboBox>
            <dx:ReportToolbarLabel ItemKind='OfLabel' />
            <dx:ReportToolbarTextBox IsReadOnly='True' ItemKind='PageCount' />
            <dx:ReportToolbarButton ItemKind='NextPage' />
            <dx:ReportToolbarButton ItemKind='LastPage' />
            <dx:ReportToolbarSeparator />
            <dx:ReportToolbarButton ItemKind='SaveToDisk' />
            <dx:ReportToolbarButton ItemKind='SaveToWindow' />
            <dx:ReportToolbarComboBox ItemKind='SaveFormat' Width='70px'>
                <Elements>
                    <dx:ListElement Value='pdf' />
                    <dx:ListElement Value='xls' />
                    <dx:ListElement Value='xlsx' />
                    <dx:ListElement Value='rtf' />
                    <dx:ListElement Value='mht' />
                    <dx:ListElement Value='html' />
                    <dx:ListElement Value='txt' />
                    <dx:ListElement Value='csv' />
                    <dx:ListElement Value='png' />
                </Elements>
            </dx:ReportToolbarComboBox>
        </Items>
        <Styles>
            <LabelStyle>
                <Margins MarginLeft='3px' MarginRight='3px' />
            </LabelStyle>
        </Styles>
    </dx:ReportToolbar>
   
    <dx:ReportViewer ID="ReportViewer1" runat="server"  CssClass="report-st-in" Width="100%" 
        Report="<%# new BusesWorkshop.Reports.Dev_Vehcles2() %>" 
        ReportName="BusesWorkshop.Reports.Dev_Vehcles2">
    </dx:ReportViewer>
                
                
                
                
            </div>
        </div>
    </div>
    
</asp:Content>
