<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="rpt_MaintRequests.aspx.cs" Inherits="BusesWorkshop.Reports.rpt_MaintRequests" %>

<%@ Register Assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">



    <div class="form-group col-md-4">
        <label class="col-md-3 control-label">
            <asp:Label ID="Label17" runat="server" Text="الطلبات المنتهية"></asp:Label>
        </label>
        <div class="col-md-9 radio-st" dir="rtl">
            <asp:CheckBox ID="chk_IsActive" runat="server" Text="طلبات مغلقة" />
            <asp:CheckBox ID="chk_IsInActive" runat="server" Text="طلبات غير مغلقة" />
        </div>
    </div>
    
    <div class="clearfix"></div>
    
    <div class="form-group col-md-4">
        <label class="col-md-3 control-label">
            <asp:Label ID="Label2" runat="server" Text="درجة الاهمية"></asp:Label>
        </label>
        <div class="col-md-9 radio-st" dir="rtl">
            <asp:CheckBox ID="chk_PriorUrgent" runat="server" Text="عاجل" />
            <asp:CheckBox ID="chk_PriorHigh" runat="server" Text="متوسط" />
            <asp:CheckBox ID="chk_PriorLow" runat="server" Text="منخفض" />
        </div>
    </div>
    <div class="clearfix"></div>
    
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
                            <asp:Label ID="Label1" runat="server" Text="اسم الموظف"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <dx:ASPxComboBox ID="ddl_RequestedEmpId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl" >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            
                        </div>
                    </div>
                    
                    
                    
                    
                    
                     <div class="form-group col-md-4">
                        <label class="col-md-4 control-label">
                            <asp:Label ID="Label5" runat="server" Text="عدد الايام للطلبات التي لم تنجز"></asp:Label>
                        </label>
                        <div class="col-md-8">
                           
                            <asp:TextBox ID="txt_DelayDays" runat="server" CssClass="form-control" 
                                onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                     <div class="form-group col-md-12 text-left">
                    <asp:Button ID="btnSearch" runat="server" Text="بحث" CssClass="btn btn-success" onclick="btnSearch_Click" 
                          ValidationGroup="Print"/>
                </div>
<%--                   <dx:ReportToolbar ID="ReportToolbar1" runat='server' ShowDefaultButtons='False' Width="100%" CssClass="report-st" 
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
   
    <dx:ReportViewer ID="ReportViewer1" runat="server"  CssClass="report-st-in" 
        Width="100%" Report="<%# new BusesWorkshop.Reports.Dev_MaintRequests() %>" 
        ReportName="BusesWorkshop.Reports.Dev_MaintRequests">
    </dx:ReportViewer>      --%>   
    
  <dx:ASPxWebDocumentViewer ID="rptMainRequest" runat="server"></dx:ASPxWebDocumentViewer>                            
</asp:Content>