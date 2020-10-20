<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="StoppedCars.aspx.cs" Inherits="BusesWorkshop.Pages.StoppedCars" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
<script>
    $(function() {
        //	$.calendars.picker.setDefaults({renderer: $.calendars.picker.themeRollerRenderer}); // Requires jquery.calendars.picker.ext.js
    var calendar = $.calendars.instance('islamic');
    $('[id*=txt_ReoperateDate]').calendarsPicker({ calendar: calendar });
});
$(function() {
    //	$.calendars.picker.setDefaults({renderer: $.calendars.picker.themeRollerRenderer}); // Requires jquery.calendars.picker.ext.js
    var calendar = $.calendars.instance('islamic');
    $('[id*=txt_StopDate]').calendarsPicker({ calendar: calendar });
});
</script>

    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    السيارات المتوقفة
                </h4>
            </div>
        </div>
    </div>
    
    
    
    
    
    
    
   <div class="row">
        <div class="col-sm-12">
           <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label3" runat="server" Text="رقم اللوحة"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:ASPxComboBox ID="ddl_VehcleId" dir="rtl" runat="server" 
                                CssClass="form-control"    DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  
                                ValueType="System.String" AutoPostBack="True" onselectedindexchanged="ddl_VehcleId_SelectedIndexChanged"
                                >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"   ValidationGroup="SaveStop"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_VehcleId"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                    
                    
                      <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label24" runat="server" Text="تاريخ الايقاف"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <div class="input-group">
                                <asp:TextBox ID="txt_StopDate" CssClass="form-control" 
                                    runat="server" onkeydown="return false;"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="SaveStop"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_StopDate"></asp:RequiredFieldValidator>

                            </div>
                            <!-- input-group -->
                        </div>
                    </div>
                
                
                
                
                
                <div class="clearfix"></div>
                
                
                
                
                
                
                
                  <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label21" runat="server" Text="سبب الايقاف"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_StopReason" TextMode="MultiLine" runat="server" 
                                CssClass="form-control"
                                ></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="SaveStop"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_StopReason"></asp:RequiredFieldValidator>

                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                    
                
                
                
                
                
                
                  <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label1" runat="server" Text="تاريخ اعادة التشغيل"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <div class="input-group">
                                <asp:TextBox ID="txt_ReoperateDate" CssClass="form-control" 
                                    runat="server" onkeydown="return false;"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="SaveReoperate"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_ReoperateDate"></asp:RequiredFieldValidator>

                            </div>
                            <!-- input-group -->
                        </div>
                    </div>
                
                <div class="clearfix"></div>
                <hr />
                <div class="clearfix"></div>
                
                
                
                
                 <div class="col-md-12">
                        <div class="form-group col-md-12">
                            <asp:Button ID="btn_Reoperate" runat="server" Text="اعادة تشغيل" 
                                Enabled="false" CssClass="btn btn-success" ValidationGroup="SaveReoperate" onclick="btn_Reoperate_Click"
                                 />
                                 <asp:Button ID="btn_Stop" runat="server" Text="ايقاف" Enabled="false" 
                                CssClass="btn btn-danger" ValidationGroup="SaveStop" onclick="btn_Stop_Click"
                                />
                        </div>
                    </div>
                        <div class="form-group col-md-12">
                        <div id="divMsg" runat="server">
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                <hr />
                <div class="clearfix"></div>
                
               
                    
        
        
        
        
        
        
        
        
        </div>
    </div>
     <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <asp:GridView ID="grd_StopCars" CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="False" >
                    <Columns>
                        <asp:BoundField DataField="PlateNo" HeaderText="رقم اللوحة" Visible="False" />
                        <asp:BoundField DataField="CompName" HeaderText="اسم الشركة" />
                        <asp:BoundField DataField="StopReason" HeaderText="سبب الايقاف" />
                        <asp:BoundField DataField="StopDate" HeaderText="تاريخ الايقاف" 
                            DataFormatString="{0:MM-dd-yyyy}" />
                       <%-- <asp:BoundField DataField="Notes" HeaderText="الملاحظات" />--%>
                        <asp:BoundField DataField="ReoperateDate" HeaderText="تاريخ اعادة التشغيل" 
                            DataFormatString="{0:MM-dd-yyyy}" />
                        <asp:BoundField DataField="StopPeriod" HeaderText="مدة الايقاف" />
                        
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    
    </asp:Content>
    
    