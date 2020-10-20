<%@ Page Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true"
    CodeBehind="rptReminder.aspx.cs" Inherits="BusesWorkshop.Reports.rptReminder"
    Title="تقرير الصيانة الدورية" %>



<%@ Register Assembly="DevExpress.XtraReports.v18.1.Web, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
                    <!-- Page-Title -->
                <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box text-right">
                            <h4 class="page-title">تذكير بالصيانة الدورية</h4>
                        </div>
                    </div>
                </div>
    <div class="row">
                    <div class="col-sm-12">
                    
                  
                        <dx:ReportToolbar ID="ReportToolbar1" runat='server' Width="100%" CssClass="report-st" ShowDefaultButtons='False' 
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
                        <dx:ReportViewer CssClass="report-st-in" ID="ReportViewer1" runat="server" Width="100%" 
                            PrintUsingAdobePlugIn="False" 
                            Report="<%# new BusesWorkshop.Reports.Dev_rptReminder() %>" 
                            ReportName="BusesWorkshop.Reports.Dev_rptReminder">
                        </dx:ReportViewer>
            
                    
        <%--<rsweb:ReportViewer ID="rpvReminder" runat="server" Font-Names="Verdana" 
            Font-Size="10pt" Width="100%">
            <LocalReport ReportPath="Reports\rptReminder.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1_usp_Services_Reminder" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        </div>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
        TypeName="BusesWorkshop.DataSet1TableAdapters.usp_Services_ReminderTableAdapter">
        
        
    </asp:ObjectDataSource>--%>
</div>
    </div>
</asp:Content>
