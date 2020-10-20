<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPages/Master.Master" CodeBehind="rpt_RequiredPlans.aspx.cs" Inherits="BusesWorkshop.Reports.rpt_RequiredPlans" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraReports.v18.1.Web, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    تقرير بالسيارات المطلوب عمل صيانة دورية لها
                    
                    
                </h4>
            </div>
        </div>
    </div>
        <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <div class="form-horizontal">
                
                
                
                
                   <div class="form-group col-md-4">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="Label1" runat="server" Text="اسم السائق"></asp:Label>
                    </label>
                    <div class="col-md-7">
                        <dx:ASPxComboBox ID="ddl_EmployeeId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                            IncrementalFilteringMode="Contains"  ValueType="System.String"
                            dir="rtl">
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                        </dx:ASPxComboBox>
                    </div>
                </div>
                
                
                
                
                <div class="form-group col-md-4">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="Label2" runat="server" Text="رقم اللوحة"></asp:Label>
                    </label>
                    <div class="col-md-7">
                        <dx:ASPxComboBox ID="ddl_VehcleId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                            IncrementalFilteringMode="Contains"  ValueType="System.String"
                            dir="rtl">
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                        </dx:ASPxComboBox>
                    </div>
                </div>
                
                
                
                
                 <div class="form-group col-md-4">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="Label3" runat="server" Text="اسم الشركة"></asp:Label>
                    </label>
                    <div class="col-md-7">
                        <dx:ASPxComboBox ID="ddl_CompanyId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                            IncrementalFilteringMode="Contains" style="width:80% !important;float:right"  ValueType="System.String"
                            dir="rtl">
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                        </dx:ASPxComboBox>
                    </div>
                   <div class="form-group col-md-2 text-right">
                    <asp:Button ID="btnSearch" runat="server" Text="بحث" CssClass="btn btn-success" 
                           onclick="btnSearch_Click"/>
                </div>
                </div>
                
                
                
                </div>
                </div>
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
        Report="<%# new BusesWorkshop.Reports.Dev_RequiredPlanVehcles() %>" 
        ReportName="BusesWorkshop.Reports.Dev_RequiredPlanVehcles">
    </dx:ReportViewer>


</asp:Content>