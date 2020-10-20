<%@ Page Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="rptBusesDrivers.aspx.cs" Inherits="BusesWorkshop.Reports.rptBusesDrivers" Title="تقرير السائقين" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraReports.v18.1.Web, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">


    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <div class="form-horizontal">
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label3" runat="server" Text="الحافلة"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:ASPxComboBox ID="ddlBuses" runat="server" class="form-control">
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <asp:Button ID="btnSearch" runat="server" Text="عرض" CssClass="btn btn-success" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    
        <dx:ReportToolbar ID="ReportToolbar1" runat='server' ShowDefaultButtons='False'  Width="100%" 
                                    CssClass="report-st" ReportViewer="<%# ReportViewer1 %>">
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
                                    Report="<%# new BusesWorkshop.Reports.Dev_rptBusesDrivers() %>" 
                                    ReportName="BusesWorkshop.Reports.Dev_rptBusesDrivers" >
        </dx:ReportViewer>
    <%--<rsweb:ReportViewer ID="rpvBusDriver" runat="server" Font-Names="Verdana" 
        Font-Size="10pt" Height="1000px" Width="1000px">
        <LocalReport ReportPath="Reports\rptBusesDrivers.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                    Name="DataSet1_usp_BusesDrivers" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="GetData" 
        TypeName="BusesWorkshop.DataSet1TableAdapters.usp_BusesDriversTableAdapter">
    </asp:ObjectDataSource>--%>
     </div>
                                   
</asp:Content>
