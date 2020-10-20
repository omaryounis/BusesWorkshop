<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master"
    CodeBehind="Employees.aspx.cs" Inherits="BusesWorkshop.Pages.Employees" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <!-- Page-Title -->
<script>
    $(function() {
        //	$.calendars.picker.setDefaults({renderer: $.calendars.picker.themeRollerRenderer}); // Requires jquery.calendars.picker.ext.js
    var calendar = $.calendars.instance('islamic');
    $('[id*=txt_DrivingLicenseExpiryDate]').calendarsPicker({ calendar: calendar });
    });
</script>
    <script src="../../plugins/bootstrap-datepicker/js/MyPlugins.js" type="text/javascript"></script>

    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    «·„ÊŸ›Ì‰</h4>
            </div>
        </div>
    </div>
    <!-- end page title end breadcrumb -->
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <div class="form-horizontal">
                  
                        
                        
                        
                         <div class="form-group col-md-6">
                    
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label13" runat="server" Text="«·„Â‰…"></asp:Label>
                           
                        </label>
                        <div class="col-md-9 radio-st" dir="rtl">
                            <asp:RadioButton ID="rd_IsDriver" runat="server" Checked="true" GroupName="job"   Text="”«∆ﬁ"/>
                    <asp:RadioButton ID="rd_IsTechnician"  GroupName="job"
                        runat="server" Text="›‰Ï"/>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                      <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label14" runat="server" Text="«· Œ’’"></asp:Label>
                        </label>
                        <div class="col-md-9 text-right" dir="rtl">
                           
                            <dx:ASPxComboBox ID="ddl_SpecializationId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" ValueType="System.String"
                                dir="rtl"  >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_SpecializationId"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    <div class="form-group col-md-6">
                    
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label2" runat="server" Text="«”„ «·„ÊŸ›"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_EmployeeName"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox CssClass="form-control" ID="txt_EmployeeName" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    
                                <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label23" runat="server" Text="«·‘—ﬂ…"></asp:Label>
                        </label>
                        <div class="col-md-9 text-right" dir="rtl">
                           
                            <dx:ASPxComboBox ID="ddl_CompanyId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" ValueType="System.String"
                                dir="rtl" AutoPostBack="true" 
                                onselectedindexchanged="ddl_CompanyId_SelectedIndexChanged"  >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_CompanyId"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                                   <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label12" runat="server" Text="«Œ— Õ«›·… ⁄„· ⁄·ÌÂ«"></asp:Label>
                        </label>
                        <div class="col-md-9" dir="rtl">
                            <dx:ASPxComboBox ID="ddl_LastVehcle"  runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                 
                    </div>
                    </div>
            
       
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label4" runat="server" Text="«·—« » «·‘Â—Ì"></asp:Label>
                            
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_Salary" runat="server" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label5" runat="server" Text="«·«Ã— »«·”«⁄…"></asp:Label>
                           
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_RatePerHour" runat="server" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label6" runat="server" Text="—ﬁ„ «·—Œ’…"></asp:Label>
                           
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_DrivingLicenseId" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label8" runat="server" Text="‰Ê⁄ «·—Œ’…"></asp:Label>
                        </label>
                        <div class="col-md-9" dir="rtl">
                            <dx:ASPxComboBox ID="dll_LicenseType"  runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" ValueType="System.String"
                                dir="rtl">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                           
                        </div>
                    </div>
                     <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label24" runat="server" Text=" «—ÌŒ «‰ Â«¡ «·—Œ’…"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <div class="input-group">
                                <asp:TextBox ID="txt_DrivingLicenseExpiryDate" CssClass="form-control" 
                                    runat="server" onkeydown="return false;"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                            <!-- input-group -->
                        </div>
                    </div>
                     <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label3" runat="server" Text="—ﬁ„ «·«ﬁ«„…"></asp:Label>
                          
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_AccomodationNo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                            <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label1" runat="server" Text="«·⁄‰Ê«‰"></asp:Label>
                           
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox CssClass="form-control" ID="txt_EmployeeAddress" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label9" runat="server" Text="—ﬁ„ «·ÃÊ«·"></asp:Label>
                            
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_Mobile" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label10" runat="server" Text="—ﬁ„ «·Â« › «·«—÷Ì"></asp:Label>
                          
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_Tel" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label11" runat="server" Text="«·»—Ìœ «·«·ﬂ —Ê‰Ì"></asp:Label>
                           
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_Mail" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="form-group col-md-6">
        <label class="col-md-3 control-label">
            <asp:Label ID="Label7" runat="server" Text="„·«ÕŸ« "></asp:Label>
        </label>
        <div class="col-md-9">
            <asp:TextBox CssClass="form-control" ID="txt_Notes" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
                </div>
            </div>
        </div>
    </div>
    
    <div class="form-group col-md-12">
        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="Õ›Ÿ" 
            ValidationGroup="Save" onclick="btnSave_Click"
           />
    </div>
    <div class="form-group col-md-12">
        <div id="divMsg" runat="server">
            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <asp:GridView ID="grd_Employees" runat="server" CssClass="table m-0 table-colored table-danger" AutoGenerateColumns="False" DataKeyNames="EmployeeId">
                    <Columns>
                      <asp:TemplateField>
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>
                        <asp:BoundField DataField="EmployeeId" HeaderText="„”·”·" ReadOnly="True" 
                            SortExpression="EmployeeId" Visible="False" />
                        <asp:BoundField DataField="EmployeeName" HeaderText="«”„ «·„ÊŸ›" 
                            SortExpression="EmployeeName" />
                        <asp:BoundField DataField="Job" HeaderText="«·„Â‰…" SortExpression="Job" />
                        <asp:TemplateField HeaderText=" ⁄œÌ·">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_Edit" runat="server" CssClass="btn btn-default" 
                                    onclick="lnk_Edit_Click">
                             <i class="fa fa-edit"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Õ–›">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="btn btn-default" onclick="lnk_Delete_Click" OnClientClick="return confirm(' √ﬂÌœ «·Õ–› ø');" >
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
