<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="BuildingPlans.aspx.cs" Inherits="BusesWorkshop.BuildingPlans" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    

 <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    خطة الصيانة الدورية للمبانى</h4>
            </div>
        </div>
    </div>
    
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
            
               
                
                
                
                
                
            
             <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label9" runat="server" Text="اسم خطة الصيانة"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="SavePeriodicalPlan"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_PlanName"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_PlanName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                    
                    
                    
                    
                    
            
            
              <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label3" runat="server" Text="تفعل كل / شهر"></asp:Label>
                            
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_EveryWhilePerMonth" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
            
            
            
            
            
            
            
            
            
            
             <div class="form-group col-md-6">
        <label class="col-md-3 control-label">
            <asp:Label ID="Label5" runat="server" Text="وصف خطة الصيانة"></asp:Label>
        </label>
        <div class="col-md-9">
            <asp:TextBox ID="txt_PlanDescription" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
            

            
            </div>
        </div>
    </div>
            


        <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                                     <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label1" runat="server" Text="اسم العمل"></asp:Label>
                        </label>
                        <div class="col-md-9">
                          
                            
                            <dx:ASPxComboBox ID="ddl_RequiredJob" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" ValueType="System.String"
                                dir="rtl"  
                                >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                           

                      </div>
                    </div>
                     <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label6" runat="server" Text="وصف العمل"></asp:Label>
                        </label>
                         
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_Description" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                        
                        </div>
                    </div>
                  <div class="col-md-12">
                        <div class="form-group col-md-12">
                                 <asp:Button ID="btn_Add" runat="server" Text="اضافة" CssClass="btn btn-success" 
                                     ValidationGroup="AddGrid" onclick="btn_Add_Click"/>
                        </div>
                    </div>
                     <div class="form-group col-md-12">
                        <div id="DivalerGrid" runat="server">
                            <asp:Label ID="LBL_DivalerGrid" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                <asp:GridView ID="grd_WorksNeeded" runat="server" 
                        CssClass="table m-0 table-colored table-danger" 
                        AutoGenerateColumns="False" ondatabound="grd_WorksNeeded_DataBound" 
                                         onrowdatabound="grd_WorksNeeded_RowDataBound" 
                                         onrowdeleting="grd_WorksNeeded_RowDeleting" >
                    <Columns>
                        <asp:TemplateField HeaderText="العمل المطلوب">
                            <FooterTemplate>
                                <asp:TextBox ID="txt_RequiredJob" runat="server" 
                                    ></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                    
                                    <dx:ASPxComboBox ID="ddl_RequiredJob" runat="server"  
                                        CssClass="form-control" enabled="false" 
                                        DropDownButton-Visible="False">
    <Border BorderStyle="None" />
                                        <DropDownButton Visible="False">
                                        </DropDownButton>
</dx:ASPxComboBox>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="وصف العمل المطلوب">
                            <FooterTemplate>
                                <asp:TextBox ID="txt_Description" runat="server" TextMode="MultiLine" ></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_Description" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Button" DeleteText="حذف" 
                            ShowDeleteButton="True" />
                    </Columns>
            </asp:GridView>
                    </ContentTemplate>
                    </asp:UpdatePanel>

                
                
                
                <div class="col-md-12" style="margin-top:50px;">
                        <div class="form-group col-md-12">
                            <asp:Button ID="btnSave" runat="server" Text="حفظ" CssClass="btn btn-success" 
                                ValidationGroup="SavePeriodicalPlan" onclick="btnSave_Click" />
                        </div>
                    </div>
                    
                    
                      <div class="form-group col-md-12">
                        <div id="divMsg" runat="server">
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    
                    
                </div>
            </div>
            
                
                

</div>
</div>
   <div class="row">
        <div class="col-md-12">
            <div class="card-box">
            
            
            
            
            </div>
            
            
            
            <asp:GridView ID="gvPlans" CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="PlanId" 
                    >
                    <Columns>
                        <asp:BoundField DataField="PlanId" HeaderText="مسلسل" Visible="False" />
                        <asp:BoundField DataField="PlanName" HeaderText="اسم خطة الصيانة" />
                       <%-- <asp:BoundField DataField="Notes" HeaderText="اسم الماركة" />--%>
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
                                <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="btn btn-default"  
                                    OnClientClick="return confirm('تأكيد الحذف ؟');" onclick="lnk_Delete_Click">
                             <i class="fa fa-trash-o"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                
                
                
            
            </div>
            </div>
    
    
    
    
    
  
</asp:Content>