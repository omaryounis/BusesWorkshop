<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PeriodicPlanToVehcle.aspx.cs"
    Inherits="BusesWorkshop.PeriodicPlanToVehcle" MasterPageFile="~/MasterPages/Master.Master" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                    اسناد خطة صيانة الى سيارة</h4>
            </div>
        </div>
    </div>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="form-group col-md-4">
        <label class="col-md-12 control-label">
            <asp:Label ID="Label1" runat="server" Text="اسم خطة الصيانة"></asp:Label>
        </label>
        <div class="col-md-9">
            <dx:ASPxComboBox ID="ddl_maintPlanId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                IncrementalFilteringMode="Contains"  ValueType="System.String"
                dir="rtl" AutoPostBack="True" 
                onselectedindexchanged="ddl_maintPlanId_SelectedIndexChanged" >
                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
            </dx:ASPxComboBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="SavePeriodicalPlan"
                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_maintPlanId"></asp:RequiredFieldValidator>
        </div>
    </div>

    
      <div class="form-group col-md-4">    
        <label class="col-md-12 control-label">
            <asp:Label ID="Label2" runat="server" Text="تفعل كل / كم"></asp:Label>
        </label>
        <div class="col-md-12">
            <asp:TextBox ID="txt_EveryKm" Enabled="false" runat="server" CssClass="form-control" 
                onkeypress="return onlyNumbers(event);"></asp:TextBox>
        </div>
      
      </div>   
           
     <div class="form-group col-md-4">  
        <label class="col-md-12 control-label">
            <asp:Label ID="Label3" runat="server" Text="تفعل كل / شهر"></asp:Label>
        </label>
        <div class="col-md-12">
            <asp:TextBox ID="txt_EveryWhile" runat="server" Enabled="false" CssClass="form-control" 
                onkeypress="return onlyNumbers(event);"></asp:TextBox>
        </div>
    </div>          
            

    </ContentTemplate>
    </asp:UpdatePanel>
    
    
            <div class="clearfix"></div>   
    
    <hr />
    
    <div class="form-group col-md-4">
        <label class="col-md-12 control-label">
            <asp:Label ID="Label23" runat="server" Text="رقم اللوحة"></asp:Label>
        </label>
        <div class="col-md-12">
            <dx:ASPxComboBox ID="ddl_VehcleId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                IncrementalFilteringMode="Contains"  ValueType="System.String"
                dir="rtl" AutoPostBack="True" 
                onselectedindexchanged="ddl_VehcleId_SelectedIndexChanged">
                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
            </dx:ASPxComboBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="SavePeriodicalPlan"
                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_VehcleId"></asp:RequiredFieldValidator>
        </div>
    </div>

      <div class="form-group col-md-4">  
        <label class="col-md-12 control-label">
            <asp:Label ID="Label7" runat="server" Text="قراءة عداد دورة الصيانة القادمة"></asp:Label>
        </label>
        <div class="col-md-12">
            <asp:TextBox ID="txt_NextPlanCounter" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
        </div>
    </div>
    
    
      <div class="form-group col-md-4">     
        <label class="col-md-12 control-label">
            <asp:Label ID="Label25" runat="server" Text="تاريخ دورة الصيانة القادم"></asp:Label>
        </label>
        <div class="col-md-12">
            <div class="input-group">
                <asp:TextBox ID="txt_NextPlaneDate" CssClass="form-control" runat="server"
                    OnKeyPress="return false"></asp:TextBox>
                <span class="input-group-addon bg-success b-0" onkeypress="return false"><i class="mdi mdi-calendar text-white">
                </i></span>
            </div>
            <!-- input-group -->
        </div>
    </div>

     
     
     <div class="clearfix"></div>   
    
    <hr />
     
    <div class="form-group col-md-12">
        <label class="col-md-2 control-label">
            <asp:Label ID="Label5" runat="server" Text="وصف خطة الصيانة"></asp:Label>
        </label>
        <div class="col-md-10">
            <asp:TextBox ID="txt_Notes" TextMode="MultiLine" runat="server" 
                CssClass="form-control"></asp:TextBox>
        </div>
    </div>
    
    
    
    
    
      <div class="form-group col-md-3" dir="rtl">
        <div class="checkbox checkbox-primary">  
           <asp:CheckBox ID="chk_IsActive" runat="server"/>
      
            <label for="chk_IsActive" runat="server">
                <asp:Label ID="Label4" runat="server" Text="ايقاف الخطة"></asp:Label>
            </label>
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
                    
                    
    
    
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <asp:GridView ID="gvPlanToVehcles" CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="PeriodicalPlanToVehcleId">
                    <Columns>
                       <%-- <asp:BoundField DataField="Notes" HeaderText="اسم الماركة" />--%>
                        <asp:BoundField DataField="PeriodicalPlanToVehcleId" HeaderText="مسلسل" 
                            Visible="False" />
                        <asp:BoundField DataField="PlateNo" HeaderText="رقم اللوحة" />
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
            </div>
        </div>
    </div>
</div>
</asp:Content>
