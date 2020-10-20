<%@ Page Language="C#" AutoEventWireup="true"   MasterPageFile="~/MasterPages/Master.Master" CodeBehind="VehclesDefinition.aspx.cs" Inherits="BusesWorkshop.Pages.Definitions.VehclesDefinition" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   

    <script src="../plugins/bootstrap-datepicker/js/MyPlugins.js" 
    type="text/javascript"></script>

   
<script>
    

    $(function() {
        //	$.calendars.picker.setDefaults({renderer: $.calendars.picker.themeRollerRenderer}); // Requires jquery.calendars.picker.ext.js
    var calendar = $.calendars.instance('islamic');
    $('[id*=txt_LicenseExpiryDate]').calendarsPicker({ calendar: calendar });
    });

    $(function() {
        //	$.calendars.picker.setDefaults({renderer: $.calendars.picker.themeRollerRenderer}); // Requires jquery.calendars.picker.ext.js
        var calendar = $.calendars.instance('islamic');
        $('[id*=txt_StartOperationDate]').calendarsPicker({ calendar: calendar });
    });

    $(function() {
        //	$.calendars.picker.setDefaults({renderer: $.calendars.picker.themeRollerRenderer}); // Requires jquery.calendars.picker.ext.js
        var calendar = $.calendars.instance('islamic');
        $('[id*=txt_EndOperationDate]').calendarsPicker({ calendar: calendar });
    });

    $(function() {
        //	$.calendars.picker.setDefaults({renderer: $.calendars.picker.themeRollerRenderer}); // Requires jquery.calendars.picker.ext.js
        var calendar = $.calendars.instance('islamic');
        $('[id*=txt_InspectioDate]').calendarsPicker({ calendar: calendar });
    });
    $(function() {
        //	$.calendars.picker.setDefaults({renderer: $.calendars.picker.themeRollerRenderer}); // Requires jquery.calendars.picker.ext.js
        var calendar = $.calendars.instance('islamic');
        $('[id*=txt_InsuranceExpiryDate]').calendarsPicker({ calendar: calendar });
    });
</script>

                <!-- Page-Title -->
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    »Ì«‰«  «·”Ì«—« </h4>
            </div>
        </div>
    </div>
    <!-- end page title end breadcrumb -->
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
      
                
                
                  <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label23" runat="server" Text="«·‘—ﬂ…"></asp:Label>
                        </label>
                        <div class="col-md-12">
                           
                            <dx:ASPxComboBox ID="ddl_CompanyId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"   ValueType="System.String"
                                dir="rtl" onselectedindexchanged="ddl_CompanyId_SelectedIndexChanged" AutoPostBack="true">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_CompanyId"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                
                
                
                
                    <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label9" runat="server" Text="—ﬁ„ «··ÊÕ…"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="SaveSpare"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_PlateNo"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-12">
                            <asp:TextBox ID="txt_PlateNo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                  
                    
                    
                    <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label5" runat="server" Text="«·„«—ﬂ…"></asp:Label>
                        </label>
                        <div class="col-md-12">
                            <dx:ASPxComboBox ID="ddl_BrandId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"   ValueType="System.String"
                                dir="rtl" AutoPostBack="True" 
                                onselectedindexchanged="ddl_BrandId_SelectedIndexChanged">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_BrandId"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                    
                    
                    
                    <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label6" runat="server" Text="«·„ÊœÌ·"></asp:Label>
                        </label>
                        <div class="col-md-12">
                            <dx:ASPxComboBox ID="ddl_ModelId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"   ValueType="System.String"
                                dir="rtl" AutoPostBack="True" 
                                onselectedindexchanged="ddl_ModelId_SelectedIndexChanged" >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_ModelId"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                    
                    <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label7" runat="server" Text="«·ÿ—«“"></asp:Label>
                        </label>
                        <div class="col-md-12">
                            <dx:ASPxComboBox ID="ddl_ClassId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl" 
                                >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_ClassId"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                
                
                     <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label2" runat="server" Text="‰Ê⁄ «·”Ì«—…"></asp:Label>
                        </label>
                        <div class="col-md-12">
                            <dx:ASPxComboBox ID="ddl_VehcleType" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl" 
                                >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                           
                        </div>
                    </div>
         
                    
                    
                    
                    
         <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label4" runat="server" Text="«·—ﬁ„ «· ”·”·Ì"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="SaveSpare"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_LicenseNo"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-12">
                            <asp:TextBox ID="txt_LicenseNo" runat="server" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                          <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label24" runat="server" Text=" «—ÌŒ «‰ Â«¡ «·«” „«—…"></asp:Label>
                        </label>
                        <div class="col-md-12">
                            <div class="input-group">
                                <asp:TextBox ID="txt_LicenseExpiryDate" ClientId="txt_LicenseExpiryDate" CssClass="form-control " 
                                    runat="server" onkeydown="return false;"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
 
                                   
                                </i></span>
                               
                            </div>
                            <!-- input-group -->
                        </div>
                    </div>
                    
                    
                    
                    
                       <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label28" runat="server" Text="«”„ ‘—ﬂ… «· √„Ì‰"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="SaveSpare"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_LicenseNo"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-12">
                                <dx:ASPxComboBox ID="ddl_InsuranceCompanyId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_InsuranceCompanyId"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                     <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label29" runat="server" Text=" «—ÌŒ «‰ Â«¡ «· √„Ì‰"></asp:Label>
                        </label>
                        <div class="col-md-12">
                            <div class="input-group">
                                <asp:TextBox ID="txt_InsuranceExpiryDate" ClientId="txt_InsuranceExpiryDate" CssClass="form-control " 
                                    runat="server" onkeydown="return false;"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
 
                                   
                                </i></span>
                               
                            </div>
                            <!-- input-group -->
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label1" runat="server" Text="‰Ê⁄ «·—Œ’…"></asp:Label>
                        </label>
                        <div class="col-md-12">
                            <dx:ASPxComboBox ID="ddl_LicenseType" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl" 
                                >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                           
                        </div>
                    </div>
                    
                    
                    <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label3" runat="server" Text="‰Ê⁄ «·ÊﬁÊœ"></asp:Label>
                        </label>
                        <div class="col-md-12">
                            <dx:ASPxComboBox ID="ddl_FueL" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    
                    
                    <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label8" runat="server" Text="—ﬁ„ «·ÂÌﬂ·"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="SaveSpare"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_LicenseNo"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-12">
                            <asp:TextBox ID="txt_BodyNo" runat="server" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label10" runat="server" Text="—ﬁ„ «·„« Ê—"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="SaveSpare"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_LicenseNo"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-12">
                            <asp:TextBox ID="txt_MotorNo" runat="server" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label11" runat="server" Text="CC"></asp:Label>
                        </label>
                        <div class="col-md-12">
                            <dx:ASPxComboBox ID="ddl_CC" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    
                    
                    
                    <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label12" runat="server" Text="«·”«∆ﬁ «·«”«”Ì ··„—ﬂ»…"></asp:Label>
                        </label>
                        <div class="col-md-12">
                            <dx:ASPxComboBox ID="ddl_MainDriver" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            
                        </div>
                    </div>
                    
                    
                      <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label22" runat="server" Text="«”„ «·„‘—›"></asp:Label>
                        </label>
                        <div class="col-md-12">
                           
                            <asp:TextBox ID="ddl_SuperVisorId" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label13" runat="server" Text="ﬁ—«¡… «·⁄œ«œ ⁄‰œ »œ«Ì… «· ‘€Ì·"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_CounterReadingStart"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-12">
                            <asp:TextBox ID="txt_CounterReadingStart" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label14" runat="server" Text="„ Ê”ÿ «” Â·«ﬂ «·ÊﬁÊœ"></asp:Label>
                            
                        </label>
                        <div class="col-md-12">
                            <asp:TextBox ID="txt_AverageFuelConsumption" runat="server" CssClass="form-control"
                                onkeypress="return onlyDotsAndNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                             <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label15" runat="server" Text="·Ê‰ «·”Ì«—…"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="SaveSpare"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_LicenseNo"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-12">
                            
                               <dx:ASPxComboBox ID="ddl_Color" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    
                    
                    
                    <div class="clearfix"></div>
                    
                    
                    
                    <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label16" runat="server" Text="⁄œœ «”ÿÊ«‰«  «·„Õ—ﬂ"></asp:Label>
                        </label>
                        <div class="col-md-12">
                            <dx:ASPxComboBox ID="ddl_CylenderNo" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    
                    
                    
                    
                    <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label17" runat="server" Text="”‰… «·’‰⁄"></asp:Label>
                        </label>
                        <div class="col-md-12">
                            <asp:TextBox ID="txt_ManufactureYear" runat="server" MaxLength="4" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label18" runat="server" Text="»·œ «·’‰⁄"></asp:Label>
                        </label>
                        <div class="col-md-12">
                            <dx:ASPxComboBox ID="ddl_ManufacturingCountry" runat="server" CssClass="form-control"
                                DropDownStyle="DropDown" IncrementalFilteringMode="Contains" 
                                ValueType="System.String" dir="rtl">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    
                    
                    
                    
                  <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label19" runat="server" Text=" «—ÌŒ »œ√ «· ‘€Ì·"></asp:Label>
                        </label>
                        <div class="col-md-12">
                            <div class="input-group">
                                <asp:TextBox ID="txt_StartOperationDate" CssClass="form-control " 
                                    runat="server" onkeydown="return false;"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                            <!-- input-group -->
                        </div>
                    </div>
                    
                    
                    
                    <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label20" runat="server" Text=" «—ÌŒ «‰ Â«¡ «· ‘€Ì·"></asp:Label>
                        </label>
                        <div class="col-md-12">
                            <div class="input-group">
                                <asp:TextBox ID="txt_EndOperationDate" CssClass="form-control" 
                                    runat="server" onkeydown="return false;"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            
                               
                            
                            
                            
                            </div>
                            <!-- input-group -->
                        </div>
                    </div>
                    
                    
                     <div class="form-group col-md-3">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label25" runat="server" Text=" «—ÌŒ «·›Õ’"></asp:Label>
                        </label>
                        <div class="col-md-12">
                            <div class="input-group">
                                <asp:TextBox ID="txt_InspectioDate" CssClass="form-control" 
                                    runat="server" OnKeyPress="return false"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                            <!-- input-group -->
                        </div>
                    </div>
                    
                    
                    <div class="clearfix"></div>





                      <div class="form-group col-md-6" style="display:none;">
                    
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label27" runat="server" Text="„›⁄·…"></asp:Label>
                        </label>
                        <div class="col-md-9 radio-st" dir="rtl">
                        <asp:CheckBox ID="chk_IsActive" runat="server" Text="„›⁄·… / €Ì— „›⁄·…" />
                        </div>
        
                    </div>
                    
                    
                    
                    
                    
                    
                    <div class="form-group col-md-6">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label21" runat="server" Text="„·«ÕŸ« "></asp:Label>
                        </label>
                        <div class="col-md-12">
                            <asp:TextBox ID="txt_Notes" TextMode="MultiLine" runat="server" 
                                CssClass="form-control"
                                ></asp:TextBox>
                        </div>
                    </div>
                
                    
                    
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="form-group col-md-6">
                        <label class="col-md-12 control-label">
                            <asp:Label ID="Label26" runat="server" Text="„—›ﬁ«  «·”Ì«—…"></asp:Label>
                        </label>
                        <div class="col-md-12">
                            <asp:GridView ID="GrdAttAttment" CssClass="table m-0 table-bordered" runat="server"
                                AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="GrdAttAttment_RowDataBound1"
                                OnRowDeleting="GrdAttAttment_RowDeleting">
                                <RowStyle HorizontalAlign="Right" />
                                <Columns>
                               
                                    <asp:TemplateField HeaderText="«”„ «·„—›ﬁ">
                                        <ItemTemplate>
                                      
                                   <dx:ASPxComboBox ID="ddl_AttachmnentName" runat="server"  
                                        CssClass="form-control" enabled="false" 
                                        DropDownButton-Visible="False">
                                        <Border BorderStyle="None" />
                                    </dx:ASPxComboBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                                 <dx:ASPxComboBox ID="ddl_AttachmnentName"   runat="server" 
                                        CssClass="form-control" DropDownStyle="DropDown" IncrementalFilteringMode="Contains" 
                                 ValueType="System.String" dir="rtl" >
                                    </dx:ASPxComboBox>
                                                  
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="„·«ÕŸ« ">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Notes" runat="server" Text='<%# Eval("Notes") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt_Notes" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:LinkButton ID="Lnk_Save" runat="server" CssClass="btn btn-success" OnClick="LinkButton1_Click">«œŒ«·</asp:LinkButton>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-danger" ButtonType="Button" DeleteText="Õ–›" />
                                </Columns>
                                <HeaderStyle HorizontalAlign="Right" />
                            </asp:GridView>
                        </div>
                    </div>
             </ContentTemplate>
                    </asp:UpdatePanel>
                    
                
                    
                         
                    
                    
                                    
                <div class="form-group col-md-12">
                        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="Õ›Ÿ" 
                            ValidationGroup="SaveModels" onclick="btnSave_Click" />
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
                <asp:GridView ID="gvVehcles" CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="VehcleId" Visible="true">
                    <Columns>
                       <%-- <asp:BoundField DataField="Notes" HeaderText="«”„ «·„«—ﬂ…" />--%>
                         <asp:TemplateField>
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>
                        <asp:BoundField DataField="VehcleId" HeaderText="„”·”·" Visible="False" />
                        <asp:BoundField DataField="PlateNo" HeaderText="—ﬁ„ «··ÊÕ…" />
                        <asp:BoundField DataField="BrandName" HeaderText="«·„«—ﬂ…" 
                            SortExpression="BrandName" />
                        <asp:BoundField DataField="ModelName" HeaderText="«·„ÊœÌ·" 
                            SortExpression="ModelName" />
                        <asp:BoundField DataField="ClassName" HeaderText="«·ÿ—«“" 
                            SortExpression="ClassName" />
                        <asp:TemplateField HeaderText=" ⁄œÌ·">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_Edit" runat="server" CssClass="btn btn-default" onclick="lnk_Edit_Click" 
                                   >
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