<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="rpt_Vecles1.aspx.cs" Inherits="BusesWorkshop.Reports.Dev_Vecles1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraReports.v18.1.Web, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"
EnablePageMethods = "true">
</asp:ScriptManager>
<script>
    $(function() {
      
    var calendar = $.calendars.instance('islamic');
    $('[id*=txt_LicenseExpiryDateStart]').calendarsPicker({ calendar: calendar });
});

$(function() {

    var calendar = $.calendars.instance('islamic');
    $('[id*=txt_LicenseExpiryDateEnd]').calendarsPicker({ calendar: calendar });
});


$(function() {

    var calendar = $.calendars.instance('islamic');
    $('[id*=txt_InspectioDateStart]').calendarsPicker({ calendar: calendar });
});


$(function() {

    var calendar = $.calendars.instance('islamic');
    $('[id*=txt_InspectioDateEnd]').calendarsPicker({ calendar: calendar });
});
</script>

<div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    تقرير بيانات المركبة 1
                    
                    
                </h4>
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
                            <asp:Label ID="Label5" runat="server" Text="الماركة"></asp:Label>
                        </label>
                        <div class="col-md-8">
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
                    
                    
                    
                    
                    <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label6" runat="server" Text="الموديل"></asp:Label>
                        </label>
                        <div class="col-md-8">
                            <dx:ASPxComboBox ID="ddl_ModelId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl" AutoPostBack="True" 
                                onselectedindexchanged="ddl_ModelId_SelectedIndexChanged" >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_ModelId"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                    
                    <div class="clearfix"></div>
                    
                    <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label7" runat="server" Text="الطراز"></asp:Label>
                        </label>
                        <div class="col-md-8">
                            <dx:ASPxComboBox ID="ddl_ClassId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl"
                                >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_ClassId"></asp:RequiredFieldValidator>
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
                            <asp:Label ID="Label12" runat="server" Text="السائق"></asp:Label>
                        </label>
                        <div class="col-md-8">
                            <dx:ASPxComboBox ID="ddl_MainDriver" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            
                        </div>
                    </div>
                    
                    
                    <div class="clearfix"></div>
                    
                    
                    
                     <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label22" runat="server" Text="اسم المشرف"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <asp:TextBox ID="txt_SuperVisorId" runat="server" CssClass="form-control" ></asp:TextBox>
                            
                            
                            
                            
                             <cc1:AutoCompleteExtender ServiceMethod="SearchPSuperVisors"
    MinimumPrefixLength="1"
    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
    TargetControlID="txt_SuperVisorId"
    ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false">
</cc1:AutoCompleteExtender>    
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                    
                    <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label4" runat="server" Text="نوع الرخصة"></asp:Label>
                        </label>
                        <div class="col-md-8">
                            <dx:ASPxComboBox ID="ddl_LicenseType" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl" 
                                >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                           
                        </div>
                    </div>
                    
  
  
  
  
       <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label8" runat="server" Text="رقم الرخصة"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <asp:TextBox ID="txt_LicenseNo" runat="server" CssClass="form-control" ></asp:TextBox>
                            
                            
                            
                            
                             <cc1:AutoCompleteExtender ServiceMethod="SearchLicenses"
    MinimumPrefixLength="1"
    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
    TargetControlID="txt_LicenseNo"
    ID="AutoCompleteExtender2" runat="server" FirstRowSelected = "false">
</cc1:AutoCompleteExtender>    
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                        <div class="form-group col-md-4">
                                            <label class="col-md-4 p-l-0 control-label">     
                                                 <asp:Label ID="Label9" runat="server" Text="تاريخ انتهاء الرخصة من"></asp:Label>                                                                                                               
                                                      
                                            </label>    
                                            <div class="col-md-8">  
                                            <div class="input-group">
                                            <asp:TextBox ID="txt_LicenseExpiryDateStart" CssClass="form-control" runat="server"></asp:TextBox>     
                                            <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white" ></i></span>
                                            </div>
                                                                                                                     
                                            </div>
                                      </div>
                                      
                                      
                                      
                                     
                                     
                                     
                                     
                                     
                                     
                                      <div class="form-group col-md-4">
                                            <label class="col-md-4 p-l-0 control-label">     
                                                 <asp:Label ID="Label10" runat="server" Text="تاريخ انتهاء الرخصة الى"></asp:Label>                                                                                                               
                                                      
                                            </label>    
                                            <div class="col-md-8">  
                                            <div class="input-group">
                                            <asp:TextBox ID="txt_LicenseExpiryDateEnd" CssClass="form-control" runat="server"></asp:TextBox>     
                                            <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white" ></i></span>
                                            </div>
                                                                                                                     
                                            </div>
                                      </div> 
                                      
                                      
                                      <div class="clearfix"></div> 
                                      
                                      
                                      
                                      
                                                                            
                                      
                               <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label25" runat="server" Text="تاريخ الفحص من"></asp:Label>
                        </label>
                        <div class="col-md-8">
                            <div class="input-group">
                                <asp:TextBox ID="txt_InspectioDateStart" CssClass="form-control" 
                                    runat="server" OnKeyPress="return false"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                            <!-- input-group -->
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                     <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label11" runat="server" Text="تاريخ الفحص الي"></asp:Label>
                        </label>
                        <div class="col-md-8">
                            <div class="input-group">
                                <asp:TextBox ID="txt_InspectioDateEnd" CssClass="form-control" 
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
                            <asp:Label ID="Label2" runat="server" Text="من قراءة عداد حالية"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <asp:TextBox ID="txt_CurrentReadingStart" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                       <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label3" runat="server" Text="الى قراءة عداد حالية"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <asp:TextBox ID="txt_CurrentReadingEnd" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                                      
                                      
                                      
                                      
                                      
                                      
                                      
                                  
                    
                    <div class="clearfix"></div>
                    
                    
                    <div class="form-group col-md-12 text-left">
                    <asp:Button ID="btnSearch" runat="server" Text="بحث" CssClass="btn btn-success" onclick="btnSearch_Click" 
                          />
                </div>
                   
                    </div> 
                    
                                <div class="clearfix"></div>      

                                      
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
        Report="<%# new BusesWorkshop.Reports.VehclesInfo() %>" 
        ReportName="BusesWorkshop.Reports.VehclesInfo">
    </dx:ReportViewer>
                
                
                
                
                </div>
            </div>
        </div>
        </div>
        
        
        
</asp:Content>