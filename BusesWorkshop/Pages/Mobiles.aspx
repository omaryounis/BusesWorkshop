<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="Mobiles.aspx.cs" Inherits="BusesWorkshop.Pages.Mobiles" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <!-- Page-Title -->
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    المنقولات</h4>
            </div>
        </div>
    </div>
    <!-- end page title end breadcrumb -->
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                
               <div  class="form-group col-md-6" >
                <label class="col-md-3 control-label">
                            <asp:Label ID="lbl_serviceRequest" runat="server" Text="طلب الخدمه"></asp:Label>

                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="SaveModels"
                         Display="Dynamic" ErrorMessage="*Required" ControlToValidate="RequesServiceId"></asp:RequiredFieldValidator>
                </label>
                         <div class="col-md-9">
                            <dx:ASPxComboBox ID="RequesServiceId" dir="rtl" runat="server" 
                                CssClass="form-control"  DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String" OnSelectedIndexChanged="RequesServiceId_SelectedIndexChanged">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                                <Items>
                                    <dx:ListEditItem Text="طلب صيانه" Value="0" />
                                   <dx:ListEditItem Text="دعم فنى  " Value="1" />
                                    <dx:ListEditItem Text="الكل" Value="2" />

                                </Items>
                                   <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                            </dx:ASPxComboBox>
                         
                        </div>
                  
               </div> 
                
                             
                
                
                
                
                
                 <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label3" runat="server" Text="اسم المنقول الرئيسي"></asp:Label>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="SaveModels"
                         Display="Dynamic" ErrorMessage="*Required" ControlToValidate="ddl_MobileParentId"></asp:RequiredFieldValidator>
                            
                        </label>
                        <div class="col-md-9">
                            <dx:ASPxComboBox ID="ddl_MobileParentId" dir="rtl" runat="server" 
                                CssClass="form-control"  DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                                  <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                            </dx:ASPxComboBox>
                            
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                    
                    
                
                
                   <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label2" runat="server" Text="المنقول الفرعى اسم "></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*Required" ControlToValidate="txt_MobileName"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox CssClass="form-control" ID="txt_MobileName" runat="server"></asp:TextBox>
                        </div>
                      
                    </div>
                    
                    
                    
                    
                    
                    
                    
                    <div class="form-group col-md-12">
                        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="اضافه" 
                            ValidationGroup="SaveModels" onclick="btnSave_Click"  />
                    </div>
                    
                    
                    
                    
                    
                    
                    <div class="form-group col-md-12">
                        <div id="divMsg" runat="server">
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                    
                    
                
                </div>
            </div>
        </div>
 <div class="row">
        <div class="col-md-12">
            <div class="card-box" dir="rtl">
            
            
            
                <dx:ASPxTreeList ID="MobileTL" runat="server" AutoGenerateColumns="False" 
                    ParentFieldName="MobileParentId" KeyFieldName="MobileId" Width="100%">
                    <SettingsBehavior AllowFocusedNode="True" />
                    <Columns>
                        <dx:TreeListTextColumn Name="MobileName" FieldName="MobileName" 
                            VisibleIndex="0" Caption="ÇÓã ÇáãäÞæá">
                            <HeaderStyle HorizontalAlign="Right" />
                        </dx:TreeListTextColumn>
                           <dx:TreeListTextColumn Name="ServiceRequest" FieldName="ServiceRequest" 
                            VisibleIndex="2" Caption="طلب الخدمه ">
                            <HeaderStyle HorizontalAlign="Right" />
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn Name="MobileParentName" FieldName ="MobileParentName" 
                            VisibleIndex="1" Caption="ÇÓã ÇáãäÞæá ÇáÑÆíÓí">
                            <HeaderStyle HorizontalAlign="Right" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn Name="MobileId"  VisibleIndex="2" Visible="False">
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn Name="MobileParentId"  VisibleIndex="1" Visible="False">
                        </dx:TreeListTextColumn>
                          <dx:TreeListTextColumn Name="تعديل">
                    <DataCellTemplate>
                        <asp:ImageButton ID="ImgBtnUpdate" runat="server" CommandName="Modify" AlternateText="ÊÚÏíá"
                            ValidationGroup="rep" onclick="ImgBtnUpdate_Click"   ImageUrl="~/Images/images.jpg" Width="40px" Height="40px" />
                    </DataCellTemplate>
                              <HeaderStyle HorizontalAlign="Right" />
                </dx:TreeListTextColumn>
                <dx:TreeListTextColumn Name="ÍÐÝ " meta:resourceKey="TreeListTextColumnResource13" 
                            Visible="False">
                    <DataCellTemplate>
                        <asp:ImageButton ID="ImgBtnDelete" runat="server" CommandName="Remove" AlternateText="ÍÐÝ"
                             ValidationGroup="rep" onclick="ImgBtnDelete_Click" />
                    </DataCellTemplate>
                    <HeaderStyle HorizontalAlign="Right" />
                </dx:TreeListTextColumn>
                    </Columns>
                </dx:ASPxTreeList>
            
            
            
            
            
            
            
            
            </div>
            </div>
            </div>
            
                
                





</asp:Content>