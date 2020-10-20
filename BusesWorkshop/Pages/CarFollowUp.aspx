<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="CarFollowUp.aspx.cs" Inherits="BusesWorkshop.Pages.CarFollowUp" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script>
    $(function() {

    var calendar = $.calendars.instance('islamic');
    $('[id*=txt_Date]').calendarsPicker({ calendar: calendar });
});
    
    function ValidateMe() {
        if (Page_ClientValidate("vgOption")) {
            alert("valid");
        }
        Page_BlockSubmit = false;
        return false;
    }

</script>
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    ﬂ«—  „ «»⁄… Õ—ﬂ… «·”Ì«—…</h4>
            </div>
        </div>
    </div>
    
    
    
    
        <div class="row">
        <div class="col-md-12">
            <div class="card-box">
           
                
                
                
                
                
                
      <div class="form-group col-md-4">
                        <label class="col-md-4 p-l-0 control-label">
                            <asp:Label ID="Label3" runat="server" Text="—ﬁ„ «··ÊÕ…"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_VehcleId"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-8">
                            <dx:ASPxComboBox ID="ddl_VehcleId" dir="rtl" runat="server" 
                                CssClass="form-control"    DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  
                                ValueType="System.String" AutoPostBack="True" 
                                onselectedindexchanged="ddl_VehcleId_SelectedIndexChanged" >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                    
                    
                
                
                  <div class="form-group col-md-4">
                        <label class="col-md-4 p-l-0 control-label">
                            <asp:Label ID="Label1" runat="server" Text="«”„ «·”«∆ﬁ"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_DriverId"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-8">
                            <dx:ASPxComboBox ID="ddl_DriverId" dir="rtl" runat="server" 
                                CssClass="form-control"    DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" 
                                ValueType="System.String" >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                
                
                
                
                
                
                
   
                    
                    
                     <div class="form-group col-md-4">
                        <label class="col-md-4 p-l-0 control-label">
                            <asp:Label ID="Label2" runat="server" Text="ﬁ—«¡… «·⁄œ«œ «·”«Ìﬁ…"></asp:Label>
                        </label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txt_PreviousReading" runat="server" CssClass="form-control" 
                                Enabled="false" CausesValidation="True"></asp:TextBox>
                        </div>
                    </div>
                                
                
                    <div class="form-group col-md-4">
                        <label class="col-md-4 p-l-0 control-label">
                            <asp:Label ID="Label24" runat="server" Text="«· «—ÌŒ"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_Date"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-8">
                            <div class="input-group">
                                <asp:TextBox ID="txt_Date" CssClass="form-control" 
                                    runat="server" onkeydown="return false;"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                            <!-- input-group -->
                        </div>
                    </div>
                    
                    
                    
                    
                <div class="form-group col-md-4">
                    <label class="col-md-4 p-l-0 control-label">
                        <asp:Label ID="Label9" runat="server" Text="ﬁ—«¡… «·⁄œ«œ «·Õ«·Ì…"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_CounterReading"></asp:RequiredFieldValidator>
                    </label>
                    <div class="col-md-8">
                        <asp:TextBox ID="txt_CounterReading" runat="server" CssClass="form-control" AutoPostBack="True"
                            OnTextChanged="txt_CounterReading_TextChanged" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                    </div>
                </div>
                    
                    
                    
                    
                
                 
                
                
                
                
                
                
                
                
                
                
                
                  <div class="form-group col-md-4">
                        <label class="col-md-4 p-l-0 control-label">
                            <asp:Label ID="Label4" runat="server" Text="«·„”«›… «·„ﬁÿÊ⁄…"></asp:Label>
                            
                        </label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txt_Destination" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    
                
                
                
                
                
                
                
                
                
                <div class="form-group col-md-4">
                        <label class="col-md-4 p-l-0 control-label">
                            <asp:Label ID="Label5" runat="server" Text="Œÿ «·”Ì—"></asp:Label>
                            
                        </label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txt_Path" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                
                
                
                
                
                
                
                
                
                  <div class="form-group col-md-4">
                        <label class="col-md-4 p-l-0 control-label">
                            <asp:Label ID="Label6" runat="server" Text="«·„—«›ﬁÌ‰"></asp:Label>
                            
                        </label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txt_Accompanigns" runat="server" CssClass="form-control" 
                                TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                
                
                
                
                
                
                
                
                
                   <div class="form-group col-md-4">
                        <label class="col-md-4 p-l-0 control-label">
                            <asp:Label ID="Label7" runat="server" Text="„·«ÕŸ« "></asp:Label>
                            
                        </label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txt_Notes" runat="server" CssClass="form-control" 
                                TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
    
        </div>
                







<div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    ﬂ«—  „ «»⁄… «·ÊﬁÊœ</h4>
            </div>
        </div>
    </div>
    
    
    
    
        <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                
                
                
                
                
                
                
                
                <div class="form-group col-md-3">
                        <label class="col-md-6 p-l-0 control-label">
                            <asp:Label ID="Label15" runat="server" Text="ﬁ—«¡… ⁄œ«œ «Œ—  „ÊÌ‰"></asp:Label>
                            
                        </label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txt_LastSpplyCounter" runat="server" CssClass="form-control" 
                              enabled="false" ></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    
                    <div class="form-group col-md-3">
                        <label class="col-md-6 p-l-0 p-r-0 control-label">
                            <asp:Label ID="Label17" runat="server" Text=" «—ÌŒ «Œ—  „ÊÌ‰"></asp:Label>
                            
                        </label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txt_LastSupplyDate" runat="server" CssClass="form-control" 
                               enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                      <div class="form-group col-md-3">
                        <label class="col-md-6 p-l-0 p-r-0 control-label">
                            <asp:Label ID="Label19" runat="server" Text="⁄œœ · —«  «Œ—  „ÊÌ‰"></asp:Label>
                            
                        </label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txt_lastLitres" runat="server" CssClass="form-control" 
                               enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                     <div class="form-group col-md-3">
                        <label class="col-md-6 p-l-0 p-r-0 control-label">
                            <asp:Label ID="Label18" runat="server" Text="„ Ê”ÿ «·«” Â·«ﬂ"></asp:Label>
                            
                        </label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txt_Average" runat="server" CssClass="form-control" 
                               enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="clearfix"></div>
                    <hr />
                    <div class="clearfix"></div>
                    
                    
                    
                    
                   
                    
                    
                    
                    
                    
                    
                    
                   <asp:UpdatePanel ID="UpdatePanel1" runat="server" updatemode="conditional">
                    <ContentTemplate>
                                        
                    
                    
                    
                    
                     <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label10" runat="server" Text="«”„ „”∆Ê· «·Œœ„…"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="SaveFuel"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_EmployeeId"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <dx:ASPxComboBox ID="ddl_EmployeeId" dir="rtl" runat="server" 
                                CssClass="form-control"    DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  
                                ValueType="System.String" AutoPostBack="True" 
                                onselectedindexchanged="ddl_EmployeeId_SelectedIndexChanged"  >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                  <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label8" runat="server" Text="«”„ ﬂ«—  «·ÊﬁÊœ"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="SaveFuel"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_FuelCardId"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <dx:ASPxComboBox ID="ddl_FuelCardId" dir="rtl" runat="server" 
                                CssClass="form-control"    DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" 
                                ValueType="System.String" AutoPostBack="True" 
                                onselectedindexchanged="ddl_FuelCardId_SelectedIndexChanged" >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            
                        </div>
                    </div>
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                <div class="clearfix"></div>
                          
                
                
                
                
                
                
                
                
                
                
                
      
                
                 <div class="form-group col-md-3">
                        <label class="col-md-6 p-l-0 control-label">
                            <asp:Label ID="Label11" runat="server" Text="⁄œœ «·ﬂ—Ê  «·„ÿ·Ê»…"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="SaveFuel"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_Count"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txt_Count" runat="server" CssClass="form-control" 
                                AutoPostBack="True" ontextchanged="txt_Count_TextChanged" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                
                
                          
                
                
                
                       <div class="form-group col-md-3">
                        <label class="col-md-6 p-l-0 p-r-0 control-label">
                            <asp:Label ID="Label14" runat="server" Text="⁄œœ «·ﬂ—Ê  «·„ «Õ…"></asp:Label>
                            
                        </label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txt_CardsAvailable" runat="server" CssClass="form-control" 
                                Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                
                
                
                
                
                
                
                
                
                <div class="form-group col-md-3">
                        <label class="col-md-6 p-l-0 p-r-0 control-label">
                            <asp:Label ID="Label12" runat="server" Text="ﬁÌ„… «·ﬂ«— "></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="SaveFuel"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_Value"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txt_Value" runat="server" CssClass="form-control" 
                                Enabled="False"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                
                
                
                  <div class="form-group col-md-3">
                        <label class="col-md-6 p-l-0 p-r-0 control-label">
                            <asp:Label ID="Label13" runat="server" Text="«·«Ã„«·Ï"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="SaveFuel"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_Total"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txt_Total" runat="server" CssClass="form-control" 
                                Enabled="False"></asp:TextBox>
                        </div>
                    </div>
                    
                    
               </ContentTemplate>
                    <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_EmployeeId" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_FuelCardId" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>
                    

                    
                    
                    
                    
                    
                    
                    
                    
                    
                 <div class="form-group col-md-12">
                        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="Õ›Ÿ" 
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
                
                  <asp:Panel ID="pnlCheckMessage" runat="server" Visible="false">
        <div class="over-page">
        </div>
        <div class="modal-dialog" role="document">
            <div class="modal-content modal-content-st">
                <div class="modal-header text-right">
                    <h3>
                         ‰»ÌÂ</h3>
                </div>
                <div class="modal-body text-center">
                    <strong>
                        <asp:Label ID="lblCheckMSG" runat="server" Text=""></asp:Label></strong>
                </div>
                <div class="modal-footer text-left">
                    <asp:Button ID="btnCancelCheck" CssClass="btn btn-danger" runat="server" Text="≈·€«¡"
                        OnClick="btnCancelCheck_Click" />
                    <asp:Button ID="btnContinue" CssClass="btn btn-success" runat="server" Text="«” „—«—"
                        OnClick="btnContinue_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>


