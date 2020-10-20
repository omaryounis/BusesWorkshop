<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="FuelCardsCustody.aspx.cs" Inherits="BusesWorkshop.Pages.FuelCardsCustody" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    عهدة كروت الوقود</h4>
            </div>
        </div>
    </div>

<script>
    $(function() {
        //	$.calendars.picker.setDefaults({renderer: $.calendars.picker.themeRollerRenderer}); // Requires jquery.calendars.picker.ext.js
    var calendar = $.calendars.instance('islamic');
    $('[id*=txt_Date]').calendarsPicker({ calendar: calendar });
    $('[id*=Txt_FromDate]').calendarsPicker({ calendar: calendar });
    $('[id*=Txt_ToDate]').calendarsPicker({ calendar: calendar });
    
    
    
    
    
    
    
    
    
    
});
    
    
    
    
    
    
    
    
    
    
</script>



    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                
                
                
                
                  <div class="form-group col-md-3 col-md-3 col-sm-12 col-xs-12 ">
                        <label class="col-md-12 control-label col-sm-12 col-xs-12">
                            <asp:Label ID="Label3" runat="server" Text="اسم مسئول الخدمات"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_EmployeeId"></asp:RequiredFieldValidator>
                        </label>
                        <div class=" col-md-12 col-sm-12 col-xs-12"> 
                            <dx:ASPxComboBox ID="ddl_EmployeeId" dir="rtl" runat="server" 
                                CssClass="form-control"  
                                 DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" ValueType="System.String">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                          <div class="form-group col-md-3 col-md-3 col-sm-12 col-xs-12 ">
                        <label class="col-md-12 control-label col-sm-12 col-xs-12">
                            <asp:Label ID="Label1" runat="server" Text="اسم الكارت"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_FuelCardId"></asp:RequiredFieldValidator>
                        </label>
                        <div class=" col-md-12 col-sm-12 col-xs-12"> 
                            <dx:ASPxComboBox ID="ddl_FuelCardId" dir="rtl" runat="server" 
                                CssClass="form-control"  
                                 DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" 
                                ValueType="System.String" AutoPostBack="True" 
                                onselectedindexchanged="ddl_FuelCardId_SelectedIndexChanged">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                        </div>
                    </div>
                    
                    
                    
                
                    
                    
                    
                    
                    
                    
                                        
       <div class="form-group col-md-3 col-md-3 col-sm-12 col-xs-12 ">
                        <label class="col-md-12 control-label col-sm-12 col-xs-12">
                            <asp:Label ID="Label24" runat="server" Text="التاريخ"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_Date"></asp:RequiredFieldValidator>
                        </label>
                        <div class=" col-md-12 col-sm-12 col-xs-12"> 
                            <div class="input-group">
                                <asp:TextBox ID="txt_Date" CssClass="form-control" 
                                    runat="server" onkeydown="return false;"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                            <!-- input-group -->
                        </div>
                    </div>
                
                
                
                
                
                <div class="form-group col-md-3 col-md-3 col-sm-12 col-xs-12 ">
                        <label class="col-md-12 control-label col-sm-12 col-xs-12">
                            <asp:Label ID="Label2" runat="server" Text="العدد"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_Count"></asp:RequiredFieldValidator>
                        </label>
                        <div class=" col-md-12 col-sm-12 col-xs-12"> 
                            <asp:TextBox CssClass="form-control" ID="txt_Count" runat="server" 
                                onkeypress="return onlyDotsAndNumbers(event);" AutoPostBack="True" 
                                ontextchanged="txt_Count_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                
                
                
                
                
                
                
       
                
                
                
                <div class="form-group col-md-3 col-md-3 col-sm-12 col-xs-12 ">
                        <label class="col-md-12 control-label col-sm-12 col-xs-12">
                            <asp:Label ID="Label5" runat="server" Text="القيمة"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_Value"></asp:RequiredFieldValidator>
                        </label>
                        <div class=" col-md-12 col-sm-12 col-xs-12"> 
                            <asp:TextBox CssClass="form-control" ID="txt_Value" runat="server" 
                                onkeypress="return onlyNumbers(event);" AutoPostBack="True" 
                                ontextchanged="txt_Value_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                
                
                
                
                
                
                
                
                
                
                
                <div class="form-group col-md-3 col-md-3 col-sm-12 col-xs-12 ">
                        <label class="col-md-12 control-label col-sm-12 col-xs-12">
                            <asp:Label ID="Label6" runat="server" Text="الاجمالي"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_Total"></asp:RequiredFieldValidator>
                        </label>
                        <div class=" col-md-12 col-sm-12 col-xs-12"> 
                            <asp:TextBox CssClass="form-control" ID="txt_Total" runat="server" 
                                onkeypress="return onlyNumbers(event);" Enabled="False"></asp:TextBox>
                        </div>
                    </div>
                
                
                
                
                
                
                <div class="form-group col-md-3 col-md-3 col-sm-12 col-xs-12 ">
                        <label class="col-md-12 control-label col-sm-12 col-xs-12">
                            <asp:Label ID="Label7" runat="server" Text="ملاحظات"></asp:Label>
                           
                        </label>
                        <div class=" col-md-12 col-sm-12 col-xs-12"> 
                            <asp:TextBox CssClass="form-control" ID="txt_Notes" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>

                
                 <div class="form-group col-md-3 col-sm-12 col-xs-12 text-left">
                  <label class="col-md-12 control-label col-sm-12 col-xs-12"> <br /><br /></label>
                        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="حفظ" 
                            ValidationGroup="Save" onclick="btnSave_Click" />
                    </div>
                
                
                <div class="clearfix"></div>
                
                
                
                
                    
                    
                    <div class="form-group col-md-12">
                        <div id="divMsg" runat="server">
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                        <div class="row">
        <div class="col-md-12">
            <div class="card-box">
            
            
            
            
            
            
            
            
            <div class="col-lg-11">
               <div class="row">
               
            
                <div class="form-group col-md-3">
                    <label class="col-md-12 control-label col-sm-12 col-xs-12">
                        <asp:Label ID="Label4" runat="server" Text="اسم مسئول الخدمات"></asp:Label>
                    </label>
                    <div class=" col-md-12 col-sm-12 col-xs-12"> 
                    
                        <dx:ASPxComboBox ID="ddl_EmployeeNameSearch" dir="rtl" runat="server" CssClass="form-control" DropDownStyle="DropDown" IncrementalFilteringMode="Contains" ValueType="System.String">
                                                    
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                        </dx:ASPxComboBox><asp:RequiredFieldValidator ID="ddl_EmployeeNameSearchee" runat="server" ValidationGroup="search"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_EmployeeNameSearch"></asp:RequiredFieldValidator>
                    </div>
                </div>
             
                
                <div class="form-group col-md-3">
                        <label class="col-md-12 control-label col-sm-12 col-xs-12">
                            <asp:Label ID="Label8" runat="server" Text="اسم الكارت"></asp:Label>
                            
                        </label>
                        <div class=" col-md-12 col-sm-12 col-xs-12"> 
                            <dx:ASPxComboBox ID="ddl_FuelCardIdSearch" dir="rtl" runat="server" 
                                CssClass="form-control"  
                                DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" 
                                ValueType="System.String" AutoPostBack="True">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                        </div>
                    </div>
        
            
               <div class="form-group col-md-3">
                        <label class="col-md-12 control-label col-sm-12 col-xs-12">
                            <asp:Label ID="Label99" runat="server" Text="من تاريخ"></asp:Label>
                            
                        </label>
                        <div class=" col-md-12 col-sm-12 col-xs-12"> 
                            <div class="input-group">
                                <asp:TextBox ID="Txt_FromDate" CssClass="form-control" 
                                    runat="server" onkeydown="return false;"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                            <!-- input-group -->
                        </div>
                    </div>
  
            
                        
               <div class="form-group col-md-3">
                        <label class="col-md-12 control-label col-sm-12 col-xs-12">
                            <asp:Label ID="Label9" runat="server" Text="الى تاريخ"></asp:Label>
                          
                        </label>
                        <div class=" col-md-12 col-sm-12 col-xs-12"> 
                            <div class="input-group">
                                <asp:TextBox ID="Txt_ToDate" CssClass="form-control" 
                                    runat="server" onkeydown="return false;"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                            <!-- input-group -->
                        </div>
                    </div>
            
               </div>
            </div>
            
            <div class="col-lg-1 form-group">  
            
               <div class="row">
                        <label class="col-md-12 control-label col-sm-12 col-xs-12"> <br /><br /></label>
                   
                     <asp:Button ID="btn_Search" CssClass="btn btn-success" ValidationGroup="search" runat="server" Text="بحث" onclick="btn_Search_Click" />
                     
               </div>
            </div>            
            
                <div class="clearfix"></div>
      <br />
                    <div class="clearfix"></div>
                    
        
            <div class="col-lg-12">            
                    
                <asp:GridView ID="grd_Custodies" runat="server" 
                    CssClass="table m-0 table-colored table-danger" AutoGenerateColumns="False" 
                    DataKeyNames="CustodyID" AllowPaging="True" PagerStyle-CssClass="pagination"
                    onpageindexchanging="grd_Custodies_PageIndexChanging">
                    
                    <Columns>
                      <asp:TemplateField>
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>
                        <asp:BoundField DataField="CustodyID" HeaderText="مسلسل" ReadOnly="True" 
                            SortExpression="CustodyID" Visible="False" />
                        <asp:BoundField DataField="EmployeeName" HeaderText="اسم الموظف" 
                            SortExpression="EmployeeName" />
                        <asp:BoundField DataField="FuelCardName" HeaderText="كارت الوقود" 
                            SortExpression="FuelCardName" />
                        <asp:BoundField DataField="Date" HeaderText="التاريخ" 
                            DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Count" HeaderText="العدد" />
                        <asp:BoundField DataField="Value" HeaderText="السعر" />
                        <asp:BoundField DataField="Total" HeaderText="الاجمالى" />
                        <asp:TemplateField HeaderText="تعديل">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_Edit" runat="server" CssClass="btn btn-default" 
                                    onclick="lnk_Edit_Click">
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
    </div>
                
                </div>
      
        </div>
    </div>
                
</asp:Content>

