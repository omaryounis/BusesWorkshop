<%@ Page Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="rptBusesRenew.aspx.cs" Inherits="BusesWorkshop.Reports.rptBusesRenew" Title="تقرير تجديد الاستمارات" %>


<%@ Register Assembly="DevExpress.XtraReports.v18.1.Web, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">


    
    <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box text-right">
                            <h4 class="page-title">تقرير بقرب تجديد الاستمارات و مواعيد انتهائها</h4>
                        </div>
                    </div>
                </div>

    <dx:ReportToolbar ID="ReportToolbar1" runat='server' ShowDefaultButtons='False'  Width="100%">
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

    <dx:ReportViewer CssClass="report-st-in" ID="ReportViewer1" runat="server" 
        Width="100%" PrintUsingAdobePlugIn="False" 
        Report="<%# new BusesWorkshop.Reports.Dev_rptBusesRenew() %>" 
        ReportName="BusesWorkshop.Reports.Dev_rptBusesRenew">
    </dx:ReportViewer>

</asp:Content>
