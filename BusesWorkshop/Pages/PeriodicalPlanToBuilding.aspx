<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="PeriodicalPlanToBuilding.aspx.cs" Inherits="BusesWorkshop.Pages.PeriodicalPlanToBuilding" %>


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
             $('[id*=txt_NextPlaneDate]').calendarsPicker({ calendar: calendar });
         });
</script>

<div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                     اسناد خطة صيانة الى مبنى</h4>
            </div>
        </div>
    </div>
                 
                    
                    
                      <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label2" runat="server" Text="اسم خطة الصيانة"></asp:Label>
                        </label>
                        <div class="col-md-9">
                          
                            
                            <dx:ASPxComboBox ID="ddl_PlanId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl" AutoPostBack="True" onselectedindexchanged="ddl_PlanId_SelectedIndexChanged"  
                                >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                           
 <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_PlanId">
                                </asp:RequiredFieldValidator>

 </div>
                    </div>
                    
                    
                    
                    
                    
                    

<div class="form-group col-md-6">    
        <label class="col-md-3 control-label">
            <asp:Label ID="Label3" runat="server" Text="تفعل كل / كم"></asp:Label>
        </label>
        <div class="col-md-9">
            <asp:TextBox ID="txt_EveryKm" Enabled="False" runat="server" CssClass="form-control" 
                onkeypress="return onlyNumbers(event);"></asp:TextBox>
        </div>
        </div>
      
      <div class="clearfix"></div>
      <hr style="margin-top: 5px;" />  
    
     <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label1" runat="server" Text="اسم المبنى"></asp:Label>
                        </label>
                        <div class="col-md-9">
                          
                            
                            <dx:ASPxComboBox ID="ddl_BuildingId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String"
                                dir="rtl" AutoPostBack="True" onselectedindexchanged="ddl_BuildingId_SelectedIndexChanged"  
                                >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                           
 <asp:RequiredFieldValidator ID="RequiredFieldValidator111" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_BuildingId"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                     <div class="form-group col-md-6">     
        <label class="col-md-3 control-label">
            <asp:Label ID="Label25" runat="server" Text="تاريخ دورة الصيانةالقادم"></asp:Label>
        </label>
        <div class="col-md-9">
            <div class="input-group">
                <asp:TextBox ID="txt_NextPlaneDate" CssClass="form-control" runat="server"
                    OnKeyPress="return false"></asp:TextBox>
                <span class="input-group-addon bg-success b-0" onkeypress="return false"><i class="mdi mdi-calendar text-white">
                </i></span>
            </div>
            <!-- input-group -->
        </div>
    </div>
       
       
            

    
    
    
            
                    
                    
              <div class="form-group col-md-12">
                        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="حفظ" 
                            ValidationGroup="SaveModels" onclick="btnSave_Click"/>
                    </div>
                    
                    
                    <div class="form-group col-md-12">
                        <div id="divMsg" runat="server">
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                        </div>
                    </div>       
                   <asp:GridView ID="gvPlanToBuilding" CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="PeriodicalPlanId">
                    <Columns>
                       <%-- <asp:BoundField DataField="Notes" HeaderText="اسم الماركة" />--%>
                        <asp:BoundField DataField="PeriodicalPlanId" HeaderText="مسلسل" 
                            Visible="False" />
                        <asp:BoundField DataField="BuildingName" HeaderText="اسم المبنى" />
                        <asp:BoundField DataField="PlanName" HeaderText="اسم خطة الصيانة" />
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
                                <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="btn btn-default" onclick="lnk_Delete_Click" OnClientClick="return confirm('تأكيد الحذف ؟');">
                             <i class="fa fa-trash-o"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
                    
</asp:Content>

