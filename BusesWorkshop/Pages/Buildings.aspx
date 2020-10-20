<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="Buildings.aspx.cs" Inherits="BusesWorkshop.Pages.Buildings" %>




<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>





<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    المبانى</h4>
            </div>
        </div>
    </div>
    
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
            
            
            
            
            
                <div class="form-group col-md-6">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="Label2" runat="server" Text="اسم المبنى"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_BuildingName"></asp:RequiredFieldValidator>
                    </label>
                    <div class="col-md-9">
                        <asp:TextBox CssClass="form-control" ID="txt_BuildingName" runat="server"></asp:TextBox>
                    </div>
                </div>
                
                
                
                
                
                
                
                
                 <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label17" runat="server" Text="اسم الشركة"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:aspxcombobox ID="ddl_companyId" dir="rtl" runat="server" 
                                CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" 
                                ValueType="System.String" AutoPostBack="True" 
                                onselectedindexchanged="ddl_companyId_SelectedIndexChanged" >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:aspxcombobox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_companyId"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                  <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label1" runat="server" Text="اسم المدينة"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:aspxcombobox ID="ddl_CityId" dir="rtl" runat="server" 
                                CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" 
                                ValueType="System.String" AutoPostBack="True" 
                                onselectedindexchanged="ddl_CityId_SelectedIndexChanged">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:aspxcombobox>
                   
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                      <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label3" runat="server" Text="اسم الحى"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:aspxcombobox ID="ddl_DistrictId" dir="rtl" runat="server" 
                                CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" 
                                ValueType="System.String" AutoPostBack="True" 
                                onselectedindexchanged="ddl_DistrictId_SelectedIndexChanged" >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:aspxcombobox>
                          
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                    
                    
                      <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label4" runat="server" Text="اسم الشارع"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:aspxcombobox ID="ddl_StreetId" dir="rtl" runat="server" 
                                CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" 
                                ValueType="System.String">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:aspxcombobox>
                           
                        </div>
                    </div>
                
                
                
                
                
                
                
                <div class="form-group col-md-6">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="Label5" runat="server" Text="رقم القطعة"></asp:Label>
                        
                    </label>
                    <div class="col-md-9">
                        <asp:TextBox CssClass="form-control" ID="txt_PieceNo" runat="server"></asp:TextBox>
                    </div>
                </div>
                
                
                
                
                
                
                   <div class="form-group col-md-6">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="Label10" runat="server" Text="رقم رخصة البناء"></asp:Label>
                        
                    </label>
                    <div class="col-md-9">
                        <asp:TextBox CssClass="form-control" ID="txt_BuildingLicinseNo" runat="server"></asp:TextBox>
                    </div>
                </div>
                
                
                
                
                
                
                
                
                
                 <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label11" runat="server" Text="الاستخدام"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:aspxcombobox ID="ddl_UsageId" dir="rtl" runat="server" 
                                CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" 
                                ValueType="System.String"/>
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:aspxcombobox>
                   
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                     <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label14" runat="server" Text="نوع المبنى"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:aspxcombobox ID="ddl_BuildingTypeId" dir="rtl" runat="server" 
                                CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" 
                                ValueType="System.String">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:aspxcombobox>
                   
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                       <div class="form-group col-md-6">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="Label12" runat="server" Text="المساحة"></asp:Label>
                        
                    </label>
                    <div class="col-md-9">
                        <asp:TextBox CssClass="form-control" ID="txt_Area" runat="server" onkeypress="return onlyDotsAndNumbers(event);"></asp:TextBox>
                    </div>
                </div>
                
                
                
                
                
                    
                    
                      <div class="form-group col-md-6">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="Label13" runat="server" Text="عدد الطوابق"></asp:Label>
                        
                    </label>
                    <div class="col-md-9">
                        <asp:TextBox CssClass="form-control" ID="txt_FloorNo" runat="server" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                    </div>
                </div>
                
                
                
                
                
                
                
                
                
                
                
                  <div class="form-group col-md-6">
                    
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label27" runat="server" Text=""></asp:Label>
                        </label>
                        <div class="col-md-9 radio-st" dir="rtl">
                        <asp:CheckBox ID="chk_Water" runat="server" Text="تراخيص ماء" />
                         <asp:CheckBox ID="chk_Electricity" runat="server" Text="تراخيص كهرياء" />
                         <asp:CheckBox ID="chk_Gas" runat="server" Text="تراخيص غاز" />
                        </div>
        
                    </div>
                    
                    
                   
                    
                    
                    
                    
                    
                    
                      <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label15" runat="server" Text="نوع الملكية"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:aspxcombobox ID="ddl_OwnerShipId" dir="rtl" runat="server" 
                                CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"
                                ValueType="System.String">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:aspxcombobox>
                   
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                    
                 <div class="form-group col-md-12">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="Label6" runat="server" Text="الحدود الشمالية"></asp:Label>
                        
                    </label>
                    <div class="col-md-9">
                        <asp:TextBox CssClass="form-control" ID="txt_NorthBorder" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                
                
                
                
                     <div class="form-group col-md-12">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="Label7" runat="server" Text="الحدود الشرقية"></asp:Label>
                        
                    </label>
                    <div class="col-md-9">
                        <asp:TextBox CssClass="form-control" ID="txt_EasternBorder" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                
                
                
                
                
                
                  <div class="form-group col-md-12">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="Label8" runat="server" Text="الحدود الجنوبية"></asp:Label>
                        
                    </label>
                    <div class="col-md-9">
                        <asp:TextBox CssClass="form-control" ID="txt_SouthBorder" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                
                
                
                
                
                
                 <div class="form-group col-md-12">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="Label9" runat="server" Text="الحدود الغربية"></asp:Label>
                        
                    </label>
                    <div class="col-md-9">
                        <asp:TextBox CssClass="form-control" ID="txt_WesternBorder" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                
                
                
                
                
                
                
                
                
              
                
                    
                    
                            <div class="form-group col-md-12">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="Label16" runat="server" Text="ملاحظات"></asp:Label>
                        
                    </label>
                    <div class="col-md-9">
                        <asp:TextBox CssClass="form-control" TextMode="MultiLine" ID="txt_Notes" runat="server" ></asp:TextBox>
                    </div>
                </div>
                
                
                
                
                
                
                
                   <div class="form-group col-md-12">
                        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="حفظ" 
                            ValidationGroup="Save" onclick="btnSave_Click" />
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
            <div class="card-box">
                <asp:GridView ID="gvBuildings" CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="BuildingId">
                    <Columns>
                       <%-- <asp:BoundField DataField="Notes" HeaderText="الملاحظات" />--%>
                        <asp:BoundField DataField="DistrictId" HeaderText="مسلسل" Visible="False" />
                        <asp:BoundField DataField="BuildingName" HeaderText="اسم المبنى" 
                            SortExpression="BuildingName" />
                        <asp:TemplateField HeaderText="تعديل">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_Edit" runat="server" CssClass="btn btn-default" onclick="lnk_Edit_Click" 
                                     >
                             <i class="fa fa-edit"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="حذف">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="btn btn-default" onclick="lnk_Delete_Click" OnClientClick="return confirm('تأكيد الحذف ؟');" >
                             <i class="fa fa-trash-o"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

