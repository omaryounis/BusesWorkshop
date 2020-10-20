<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="rpt_FollowUpCards.aspx.cs" Inherits="BusesWorkshop.Reports.rpt_FollowUpCards" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraReports.v18.1.Web, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
<script src="../plugins/bootstrap-datepicker/js/MyPlugins.js" 
    type="text/javascript"></script>

   
<script>
    

    $(function() {
        //	$.calendars.picker.setDefaults({renderer: $.calendars.picker.themeRollerRenderer}); // Requires jquery.calendars.picker.ext.js
    var calendar = $.calendars.instance('islamic');
    $('[id*=txt_FromDate]').calendarsPicker({ calendar: calendar });
    });


    $(function() {
        //	$.calendars.picker.setDefaults({renderer: $.calendars.picker.themeRollerRenderer}); // Requires jquery.calendars.picker.ext.js
        var calendar = $.calendars.instance('islamic');
        $('[id*=txt_ToDate]').calendarsPicker({ calendar: calendar });
    });
</script>
<div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    تقرير كارت المتابعة
                    
                    
                </h4>
            </div>
        </div>
    </div>
        <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <div class="form-horizontal">
                               
                
                
                
                 <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label24" runat="server" Text="من تاريخ"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="Print"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_FromDate"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <div class="input-group">
                                <asp:TextBox ID="txt_FromDate" CssClass="form-control " 
                                    runat="server" onkeydown="return false;"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                     
                            <!-- input-group -->
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                     <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label6" runat="server" Text="الى تاريخ"></asp:Label>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Print"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_ToDate"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <div class="input-group">
                                <asp:TextBox ID="txt_ToDate" CssClass="form-control " 
                                    runat="server" onkeydown="return false;"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                          
                            <!-- input-group -->
                        </div>
                    </div>
                
                
                
                
                
                
                
                
                
                
                
                
                
                 <div class="form-group col-md-6">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="Label1" runat="server" Text="رقم اللوحة"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Print"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_VehcleId"></asp:RequiredFieldValidator>
                    </label>
                    <div class="col-md-9">
                        <dx:ASPxComboBox ID="ddl_VehcleId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                            IncrementalFilteringMode="Contains"  ValueType="System.String"
                            dir="rtl" width="100%">
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                        </dx:ASPxComboBox>
                    </div>
                </div>
                
                
                <div class="form-group col-md-12 text-left">
                    <asp:Button ID="btnSearch" runat="server" Text="بحث" CssClass="btn btn-success" onclick="btnSearch_Click" 
                          ValidationGroup="Print"/>
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
        Report="<%# new BusesWorkshop.Reports.DevFollowUpCard() %>" 
        ReportName="BusesWorkshop.Reports.DevFollowUpCard">
    </dx:ReportViewer>
                
                
                
                
                </div>
            </div>
        </div>
        </div>
                
</asp:Content>