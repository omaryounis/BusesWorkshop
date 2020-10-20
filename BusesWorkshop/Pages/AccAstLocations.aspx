<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="AccAstLocations.aspx.cs" Inherits="BusesWorkshop.Pages.AccAstLocations" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>





<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <div id="alertConfirmDiv" class="alert alert-success alert-dismissible" runat="server"
        visible="false">
        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
            <span aria-hidden="true">&times;</span></button>
        <asp:Label ID="LabelOutputMsg" runat="server"></asp:Label>
    </div>
    <div class="clearfix">
    </div>
    <div id="alertErrorDiv" class="alert alert-danger alert-dismissible" runat="server"
        visible="false">
        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
            <span aria-hidden="true">&times;</span></button>
        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
    </div>
    <div class="clearfix">
    </div>
     <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    المواقع</h4>
            </div>
        </div>
    </div>
      <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                
    <div class="form-group col-lg-6" style="margin-top:15px;">
        <label class="col-sm-3 control-label" style="padding-left: 2px; padding-right: 2px;">
            <asp:Label ID="Label4" runat="server" Text="Company"></asp:Label></label>
        <div class="col-sm-8">
            <asp:DropDownList ID="drpCompanies" CssClass="selectpicker show-tick form-control"
                runat="server">
            </asp:DropDownList>
        </div>
        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="drpCompanies"
            ValidationGroup="Check" Operator="GreaterThan" CssClass="red_icon" ValueToCompare="0"
            ErrorMessage="*" Text="*"></asp:CompareValidator>
    </div>
    <div class="form-group col-lg-6" style="margin-top:15px;">
        <label class="col-sm-3 control-label" style="padding-left: 2px; padding-right: 2px;">
            <asp:Label ID="lblJobName" runat="server" Text="الموقع الرئيسي"></asp:Label></label>
        <div class="col-sm-8">
            <asp:DropDownList ID="drpParentAst" CssClass="selectpicker show-tick form-control"
                ValidationGroup="Check" runat="server">
            </asp:DropDownList>
        </div>
    </div>
    <div class="clearfix">
    </div>
    <div class="form-group col-lg-6">
        <label class="col-sm-3 control-label" style="padding-left: 2px; padding-right: 2px;">
            <asp:Label ID="lblSubLocation" runat="server" Text="اسم الموقع"></asp:Label></label>
        <div class="col-sm-8">
            <asp:TextBox ID="txtLocationName" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtLocationName"
            ValidationGroup="Check" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
    </div>
    <div class="col-sm-6">
        <div>
            <asp:RadioButtonList ID="radioListLocType" RepeatDirection="Horizontal" runat="server" >
                <asp:ListItem Text="فرعي" ></asp:ListItem>
                <asp:ListItem Text="رئيسي"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </div>
    <div class="clearfix">
    </div>
    <div class="col-sm-12 text-left" style="margin-bottom: 5px;">
        <asp:Button ID="btnSave" runat="server" CssClass="btn primary_st" Text="حفظ"
            ValidationGroup="Check" OnClick="btnSave_Click" />
        <%--        <asp:Button ID="BtnShowTree" runat="server" CssClass="btn primary_st" ValidationGroup="checker"
            Text="عرض" OnClick="BtnShowTree_Click" meta:resourcekey="BtnReportResource1" />
        <asp:Button ID="BtnReport" runat="server" CssClass="btn primary_st" ValidationGroup="checker"
            Text="عرض التقرير" OnClick="BtnReport_Click" meta:resourcekey="BtnReportResource1" />--%>
    </div>
      </div>
    </div>
    </div>
    <div class="clearfix">
    </div>
     <div class="row">
        <div class="col-md-12">
            <div class="card-box">
    <div dir="rtl">
        <dx:ASPxTreeList ID="ASPxTreeList1" runat="server" AutoGenerateColumns="False" 
            meta:resourcekey="ASPxTreeList1Resource1" Width="100%">
            <SettingsBehavior AllowFocusedNode="True" />
            <Columns>
                <dx:TreeListTextColumn FieldName="ID" Name="CoaID" Visible="False" Width="100%">
                    <DataCellTemplate>
                        <asp:Label ID="lblLocID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                    </DataCellTemplate>
                </dx:TreeListTextColumn>
                <dx:TreeListTextColumn FieldName="الموقع">
                    <DataCellTemplate>
                        <asp:Label ID="lblLocDescription" runat="server" Text='<%# Eval("LocationName") %>'
                            meta:resourceKey="LabelCoa_IDResource1"></asp:Label>
                    </DataCellTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False">
                    </CellStyle>
                </dx:TreeListTextColumn>
                <dx:TreeListTextColumn FieldName="ParentID" Name="Parent_ID" Visible="false" VisibleIndex="3">
                    <DataCellTemplate>
                        <asp:Label ID="lblParent_ID" runat="server" Text='<%# Eval("ParentID") %>' meta:resourceKey="LabelCOADescResource1"></asp:Label>
                    </DataCellTemplate>
                </dx:TreeListTextColumn>
                <dx:TreeListTextColumn FieldName="الموقع الرئيسي" Name="ParentName">
                    <DataCellTemplate>
                        <asp:Label ID="lblParentName" runat="server" Text='<%# Eval("ParentName") %>' meta:resourceKey="LabelCOA_TypeResource1"></asp:Label>
                    </DataCellTemplate>
                </dx:TreeListTextColumn>
                <dx:TreeListTextColumn FieldName="الوحدة الإدارية" Name="ComName">
                    <DataCellTemplate>
                        <asp:Label ID="lblParentName" runat="server" Text='<%# Eval("ComName") %>' meta:resourceKey="LabelCOA_TypeResource1"></asp:Label>
                    </DataCellTemplate>
                </dx:TreeListTextColumn>
                <dx:TreeListTextColumn Name="تعديل">
                    <DataCellTemplate>
                        <asp:ImageButton ID="ImgBtnUpdate" runat="server" CommandName="Modify" AlternateText="تعديل"
                            OnClick="ImgBtnUpdate_Click" ValidationGroup="rep" />
                    </DataCellTemplate>
                </dx:TreeListTextColumn>
                <dx:TreeListTextColumn Name="حذف " 
                    meta:resourceKey="TreeListTextColumnResource13" Visible="False">
                    <DataCellTemplate>
                        <asp:ImageButton ID="ImgBtnDelete" runat="server" CommandName="Remove" AlternateText="حذف"
                            OnClick="ImgBtnDelete_Click" ValidationGroup="rep" />
                    </DataCellTemplate>
                </dx:TreeListTextColumn>
            </Columns>
        </dx:ASPxTreeList>
    </div>
    </div>
    </div>
    </div>
    
    
  
   
    
    
</asp:Content>
