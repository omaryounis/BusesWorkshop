<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="SearchFollowCars.aspx.cs" Inherits="BusesWorkshop.Pages.SearchFollowCars" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">

    <script src="../plugins/bootstrap-datepicker/js/MyPlugins.js" 
    type="text/javascript"></script>

   
<script>
    

    $(function() {
        //	$.calendars.picker.setDefaults({renderer: $.calendars.picker.themeRollerRenderer}); // Requires jquery.calendars.picker.ext.js
    var calendar = $.calendars.instance('islamic');
    $('[id*=txt_FromDate]').calendarsPicker({ calendar: calendar });
    });


    $(function() {
        //	$.calendars.picker.setDefaults({renderer: $.calendars.picker.themeRollerRenderer}); // Requires jquery.calendars.picker.ext.js
        var calendar = $.calendars.instance('islamic');
        $('[id*=txt_ToDate]').calendarsPicker({ calendar: calendar });
    });
</script>




<div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    بحث كروت المتابعة
                </h4>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <div class="form-horizontal">
                
                
                
                
                
                                 <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label24" runat="server" Text="من تاريخ"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="Print"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_FromDate"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <div class="input-group">
                                <asp:TextBox ID="txt_FromDate" CssClass="form-control " 
                                    runat="server" onkeydown="return false;"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                     
                            <!-- input-group -->
                        </div>
                    </div>
                    
                    
                    
                    
                    
<%--                    
                     <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label6" runat="server" Text="الى تاريخ"></asp:Label>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Print"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_ToDate"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <div class="input-group">
                                <asp:TextBox ID="txt_ToDate" CssClass="form-control " 
                                    runat="server" onkeydown="return false;"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                          
                            <!-- input-group -->
                        </div>
                    </div>
                --%>
                
                
                
                
                
                
                
                
                
                
                
                
                 <div class="form-group col-md-6">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="Label1" runat="server" Text="رقم اللوحة"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Print"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_VehcleId"></asp:RequiredFieldValidator>
                    </label>
                    <div class="col-md-9">
                        <dx:ASPxComboBox ID="ddl_VehcleId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                            IncrementalFilteringMode="Contains"  ValueType="System.String"
                            dir="rtl" width="100%">
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                        </dx:ASPxComboBox>
                    </div>
                </div>
                
                
                <div class="form-group col-md-12 text-left">
                    <asp:Button ID="btnSearch" runat="server" Text="بحث" CssClass="btn btn-success" onclick="btnSearch_Click" 
                          ValidationGroup="Print"/>
                </div>
                
                
                  <div class="form-group col-md-12">
                        <div id="divMsg" runat="server">
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                
                
                  <div class="col-lg-12">            
                    
                <asp:GridView ID="grd_FollowCards" runat="server" 
                    CssClass="table m-0 table-colored table-danger" AutoGenerateColumns="False" 
                    DataKeyNames="FollowUpCardId" AllowPaging="True" 
                          PagerStyle-CssClass="pagination" 
                          onpageindexchanging="grd_FollowCards_PageIndexChanging" onrowdatabound="grd_FollowCards_RowDataBound"
                    >
                    
                    <Columns>
                      <asp:TemplateField>
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>
                        <asp:BoundField DataField="FollowUpCardId" HeaderText="مسلسل" ReadOnly="True" 
                            SortExpression="FollowUpCardId" Visible="False" />
                        <asp:BoundField DataField="PlateNo" HeaderText="رقم اللوحة" 
                            SortExpression="PlateNo" />
                        <asp:BoundField DataField="DriverName" HeaderText="اسم السائق" 
                            SortExpression="DriverName" />
                        <asp:BoundField DataField="Date" HeaderText="التاريخ" 
                            DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="CounterReading" HeaderText="قراءة العداد" />
                        <asp:BoundField DataField="distence" HeaderText="المسافة المقطوعة" />
                        <asp:BoundField DataField="FuelName" HeaderText="نوع الوقود" />
                        <asp:BoundField DataField="LitresConsumed" HeaderText="كمية الوقود" />
                        <asp:BoundField DataField="cost" HeaderText="التكلفة" />
                        <asp:BoundField DataField="CompName" HeaderText="اسم الشركة" />
                      
                        
                        
                     
                        
                        
                        
                        <asp:TemplateField HeaderText="حذف">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_Delete" Visible="true" runat="server" CssClass="btn btn-default" onclick="lnk_Delete_Click" OnClientClick="return confirm('تأكيد الحذف ؟');" >
                             <i class="fa fa-trash-o"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            
            </div>
            
                
                
                
                
                
                
                
                </div>
            </div>
        </div>
    </div>
</asp:Content>
