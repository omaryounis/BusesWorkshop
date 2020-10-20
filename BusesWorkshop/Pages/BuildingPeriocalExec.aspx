<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="BuildingPeriocalExec.aspx.cs" Inherits="BusesWorkshop.Pages.BuildingPeriocalExec" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>




<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
 <script>
$(function() {

    var calendar = $.calendars.instance('islamic');
    $('[id*=txt_Date]').calendarsPicker({ calendar: calendar });
    });

    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
  <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    اجراء الصيانة الدورية للمبانى</h4>
            </div>
        </div>
    </div>

    
   

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
                           
 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_BuildingId"></asp:RequiredFieldValidator>
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
                           
 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="SaveModels"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_PlanId"></asp:RequiredFieldValidator>
                      </div>
                    </div>       
                    
                 
                 
                 
                 
                 
                  <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label5" runat="server" Text="التاريخ"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="SaveBus"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_Date"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <div class="input-group">
                                <asp:TextBox ID="txt_Date" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                            <!-- input-group -->
                        </div>
                    </div>   





 <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label97" runat="server" Text="ملاحظات"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
                    <asp:GridView ID="grd_Materials" runat="server" 
                        CssClass="table m-0 table-colored table-danger" 
                        AutoGenerateColumns="False" ShowFooter="True" 
                     
                        onrowdatabound="grd_Materials_RowDataBound" 
                            onrowcommand="grd_Materials_RowCommand">
                        <Columns>
                        
                        
                        
                         <asp:TemplateField HeaderText="اسم العمل">
                                <ItemTemplate>
                                    <dx:ASPxComboBox ID="ddl_WorkId" runat="server" AutoPostBack="true" 
                                        CssClass="form-control" enabled="false" 
                                         DropDownButton-Visible="False" 
                                        >
                                        <Border BorderStyle="None" />
                                    </dx:ASPxComboBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <dx:ASPxComboBox ID="ddl_WorkId" AutoPostBack="true"  runat="server" 
                                        CssClass="form-control" DropDownStyle="DropDown" IncrementalFilteringMode="Contains" 
                                 ValueType="System.String" dir="rtl">
                                    </dx:ASPxComboBox>
                                    
                                </FooterTemplate>
                            </asp:TemplateField>
                            
                            
                            
                            
                            
                        <asp:TemplateField HeaderText="التصنيف">
                                <ItemTemplate>
                                    <dx:ASPxComboBox ID="ddl_MaterialMainId" runat="server" AutoPostBack="true" 
                                        CssClass="form-control" enabled="false" 
                                         DropDownButton-Visible="False" 
                                        onselectedindexchanged="ddl_MaterialMainId_SelectedIndexChanged1">
                                        <Border BorderStyle="None" />
                                    </dx:ASPxComboBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <dx:ASPxComboBox ID="ddl_MaterialMainId" AutoPostBack="true"  runat="server" 
                                        CssClass="form-control" DropDownStyle="DropDown" IncrementalFilteringMode="Contains" 
                                  ValueType="System.String" dir="rtl" onselectedindexchanged="ddl_MaterialMainId_SelectedIndexChanged1"
                                       >
                                    </dx:ASPxComboBox>
                                    
                                </FooterTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="الخامات">
                                <ItemTemplate>
                                    <dx:ASPxComboBox ID="ddl_MaterialId" runat="server" AutoPostBack="true" 
                                        CssClass="form-control" enabled="false"  DropDownButton-Visible="False">
                                   <Border BorderStyle="None" />
                                    </dx:ASPxComboBox>
                                </ItemTemplate>
                                <FooterTemplate>
   <%--                                 <dx:ASPxComboBox ID="ddl_SpareIdAdd" AutoPostBack="true"  runat="server" 
                                        CssClass="form-control" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" 
                                EnableIncrementalFiltering="True" ValueType="System.String" dir="rtl"
                                        onselectedindexchanged="ddl_SpareId_SelectedIndexChanged">
                                    </dx:ASPxComboBox>--%>
                                    <dx:ASPxComboBox ID="ddl_MaterialId" runat="server" AutoPostBack="true"  runat="server" 
                                    CssClass="form-control" DropDownStyle="DropDown" IncrementalFilteringMode="Contains"
                                     ValueType="System.String" dir="rtl" 
                                        onselectedindexchanged="ddl_MaterialId_SelectedIndexChanged">
                                    </dx:ASPxComboBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="سعر">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_MaterialCost" runat="server" AutoPostBack="True" 
                                        CssClass="form-control" enabled="false" 
                                        Text='<%# Eval("MaterialCost") %>' BackColor="White" BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_MaterialCost" runat="server" CssClass="form-control" 
                                        AutoPostBack="true" ontextchanged="txt_SparCost_TextChanged"> </asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="عدد">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_MaterialCount" runat="server" AutoPostBack="True" enabled="false" 
                                        CssClass="form-control" 
                                        Text='<%# Eval("MaterialCount") %>' BackColor="White" BorderColor="White" 
                                        BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_MaterialCount" runat="server" AutoPostBack="true" CssClass="form-control" ontextchanged="txt_MaterialCount_TextChanged" 
                                        ></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="الاجمالى">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_MaterialTotal" runat="server" CssClass="form-control"  
                                        Enabled="False" Text='<%# Eval("MaterialTotal") %>' BackColor="White" 
                                        BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_MaterialTotal" enabled="false"  runat="server"  
                                        CssClass="form-control" ontextchanged="txt_MaterialTotal_TextChanged"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="الايدى العاملة">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_LaborCost" enabled="false"  runat="server" AutoPostBack="True" 
                                        CssClass="form-control" ontextchanged="txt_LaborCost_TextChanged"  
                                        Text='<%# Eval("LabourCost") %>' BackColor="White" BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_LaborCost" runat="server" CssClass="form-control" 
                                        AutoPostBack="true" ontextchanged="txt_LaborCost_TextChanged"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                          <asp:TemplateField HeaderText="الاجمالى الكلى">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_TotalCost" runat="server" CssClass="form-control"  enabled="false" 
                                       Text='<%# Eval("TotalCost") %>' BackColor="White" BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_TotalCost" runat="server"  CssClass="form-control" Enabled="false"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                        
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:Button ID="btn_deleteFromGrid" runat="server" 
                                        OnClientClick="return confirm('تأكيد الحذف ؟');" Text="حذف" CommandName="delete" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">اضافة</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    </ContentTemplate>
    </asp:UpdatePanel>
                    <div class="clearfix"></div>
                    <hr />
                    <div class="clearfix"></div>
                    
                    <div class="form-group col-md-8">
                        <label class="col-md-5 control-label">
                            <asp:Label ID="Label3" runat="server" Text="اجمالى قطع الغيار"></asp:Label>
                        </label>
                        <div class="col-md-3">
                            <asp:TextBox ID="txt_SparSum" Enabled="false" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="clearfix"></div>
                    
                    <div class="form-group col-md-8">
                        <label class="col-md-5 control-label">
                            <asp:Label ID="Label4" runat="server" Text="اجمالى الايدى العاملة"></asp:Label>
                        </label>
                        <div class="col-md-3">
                            <asp:TextBox ID="txt_LaborSum" Enabled="false"  runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="clearfix"></div>
                    
                    <div class="form-group col-md-8">
                        <label class="col-md-5 control-label">
                            <asp:Label ID="Label6" runat="server" Text="اجمالى قطع الغيار و الايدى العاملة "></asp:Label>
                        </label>
                        <div class="col-md-3">
                            <asp:TextBox ID="txt_TotalSum" Enabled="false" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
               <%--     </ContentTemplate>
                 
                    </asp:UpdatePanel>--%>
                    
                    
                    
                    <div class="col-md-12">
                        <div class="form-group col-md-12">
                            <asp:Button ID="btnSave" runat="server" Text="حفظ" CssClass="btn btn-success" ValidationGroup="SaveBus"
                                OnClick="btnSave_Click" />
                        </div>
                    </div>
                    <div class="form-group col-md-12">
                        <div id="divMsg2" runat="server">
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
  
   

                    
</asp:Content>

