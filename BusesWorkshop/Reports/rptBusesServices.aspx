<%@ Page Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="rptBusesServices.aspx.cs" Inherits="BusesWorkshop.Reports.rptBusesServices" Title="تقرير تفصيلي للخدمات" %>



<%@ Register Assembly="DevExpress.XtraReports.v18.1.Web, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">


    
    <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box text-right">
                            <h4 class="page-title">تقرير الفحص التفصيلي للحافلات</h4>
                        </div>
                    </div>
                </div>
    
<div class="row">
                                <div class="col-md-12">
                                  <div class="card-box">
                                  <div class="form-horizontal">
                                    
                                        <div class="form-group col-md-6">
                                            <label class="col-md-3 control-label">
                                                <asp:Label ID="Label3" runat="server" Text="الحافلة"></asp:Label>
                                            </label>    
                                            <div class="col-md-9">
                                                <asp:DropDownList class="form-control" ID="ddlBuses" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                         <div class="form-group col-md-6">
                                         
                                             <asp:Button ID="btnSearch" runat="server" Text="عرض" CssClass="btn btn-success"
                                                 onclick="btnSearch_Click" />
                                         
                                         </div>
                         </div>
                                   </div>
                                   </div> 
                                   
                                   
    <dx:ReportToolbar ID="ReportToolbar1" runat='server' ShowDefaultButtons='False'  Width="100%" 
                                    ReportViewer="<%# ReportViewer1 %>" >
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
                                    Report="<%# new BusesWorkshop.Reports.Dev_rptBusesServices() %>" 
                                    ReportName="BusesWorkshop.Reports.Dev_rptBusesServices" >
    </dx:ReportViewer>
    
    <%--<rsweb:ReportViewer CssClass="table-report" ID="rpvBusesServices" runat="server" Font-Names="Verdana" 
        Font-Size="10pt" Width="100%">
        <LocalReport ReportPath="Reports\rptBusesServices.rdlc" >
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                    Name="DataSet1_usp_BusesServices_Select" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    
    
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="GetData" TypeName="BusesWorkshop.DataSet1TableAdapters.">
    </asp:ObjectDataSource>--%>
    
    
</div>
</asp:Content>
