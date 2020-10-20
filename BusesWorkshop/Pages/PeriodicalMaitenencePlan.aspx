<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="PeriodicalMaitenencePlan.aspx.cs" Inherits="BusesWorkshop.Pages.PeriodicalMaitenencePlan" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <asp:ScriptManager runat="server">
    </asp:ScriptManager>
 <script src="../plugins/bootstrap-datepicker/js/MyPlugins.js" 
    type="text/javascript"></script>
<div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    Œÿ… «·’Ì«‰… «·œÊ—Ì…</h4>
            </div>
        </div>
    </div>
    
    
    

                    
    
    
    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label9" runat="server" Text="«”„ Œÿ… «·’Ì«‰…"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="SavePeriodicalPlan"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_PlanName"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_PlanName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                 
                    
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label2" runat="server" Text=" ›⁄· ﬂ· / ﬂ„"></asp:Label>
                           
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_EveryKm" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    

                    
                    
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label3" runat="server" Text=" ›⁄· ﬂ· / ‘Â—"></asp:Label>
                            
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_EveryWhilePerMonth" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                   
                       
                    
    <div class="form-group col-md-6">
        <label class="col-md-3 control-label">
            <asp:Label ID="Label5" runat="server" Text="Ê’› Œÿ… «·’Ì«‰…"></asp:Label>
        </label>
        <div class="col-md-9">
            <asp:TextBox ID="txt_PlanDescription" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
                    </div>
                    
                    
                    
                    <div class="form-group col-md-12">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label4" Visible="false"  runat="server"  Text="ﬁÿ⁄ «·€Ì«— «·„ÿ·Ê»… ·Œÿ… «·’Ì«‰…"></asp:Label>
                            
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox Visible="false"  ID="txt_RequiredSpareParts" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                 
              <div style="display:none;" >
              <div class="col-sm-12" >
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    ﬁÿ⁄ «·€Ì«— «·„ÿ·Ê»… ·Œÿ… «·’Ì«‰…</h4>
            </div>
        </div>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                    <div class="clearfix"></div>
                    <hr />
                    <div class="clearfix"></div>
        
                     <div class="form-group col-md-12">
                        <div id="divMsg2" runat="server">
                            <asp:Label ID="lblResult2" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                   
                    </ContentTemplate>
                    <Triggers>
                    <%--<asp:asyncpostbacktrigger controlid="btn_deleteFromGrid" eventname="Click" />--%>
                   
                    </Triggers>
                 
                    </asp:UpdatePanel>
              </div>   

                        
                        
                    
                    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    «·«⁄„«· «·„ÿ·Ê»… ›Ï Œÿ… «·’Ì«‰…</h4>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <div class="form-horizontal">
                
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                                     <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label1" runat="server" Text="«”„ «·⁄„·"></asp:Label>
                        </label>
                        <div class="col-md-9">
                          
                            
                            <dx:ASPxComboBox ID="ddl_RequiredJob" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" ValueType="System.String"
                                dir="rtl"  
                                >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="RequiredJobs"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_RequiredJob"></asp:RequiredFieldValidator>


                      </div>
                    </div>
                     <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label6" runat="server" Text="Ê’› «·⁄„·"></asp:Label>
                        </label>
                         
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_Description" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                        
                        </div>
                    </div>
                  <div class="col-md-12">
                        <div class="form-group col-md-12">
                            <asp:Button ID="btnSaveToGrid" runat="server" Text="«÷«›…" 
                                CssClass="btn btn-success" ValidationGroup="RequiredJobs" onclick="btnSaveToGrid_Click"
                                 />
                        </div>
                    </div>
                     <div class="form-group col-md-12">
                        <div id="DivalerGrid" runat="server">
                            <asp:Label ID="LBL_DivalerGrid" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                <asp:GridView ID="grd_WorksNeeded" runat="server" 
                        CssClass="table m-0 table-colored table-danger" 
                        AutoGenerateColumns="False" onrowdeleting="grd_WorksNeeded_RowDeleting" 
                                         onrowdatabound="grd_WorksNeeded_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="«·⁄„· «·„ÿ·Ê»">
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
                        <asp:TemplateField HeaderText="Ê’› «·⁄„· «·„ÿ·Ê»">
                            <FooterTemplate>
                                <asp:TextBox ID="txt_Description" runat="server" TextMode="MultiLine" ></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_Description" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Button" DeleteText="Õ–›" 
                            ShowDeleteButton="True" />
                    </Columns>
            </asp:GridView>
                    </ContentTemplate>
                    </asp:UpdatePanel>

                
                
                
                <div class="col-md-12" style="margin-top:50px;">
                        <div class="form-group col-md-12">
                            <asp:Button ID="btnSave" runat="server" Text="Õ›Ÿ" CssClass="btn btn-success" 
                                ValidationGroup="SavePeriodicalPlan" onclick="btnSave_Click"/>
                        </div>
                    </div>
                    
                    
                      <div class="form-group col-md-12">
                        <div id="divMsg" runat="server">
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    
                    
                </div>
            </div>
            
                    <asp:GridView ID="grd_SparParts" runat="server" AutoGenerateColumns="False" 
                             CssClass="table m-0 table-colored table-danger" 
                             onrowcommand="grd_SparParts_RowCommand" 
                             onrowdatabound="grd_SparParts_RowDataBound" 
                             onrowdeleting="grd_SparParts_RowDeleting" ShowFooter="True" 
                Visible="False">
                             <Columns>
                                 <asp:TemplateField HeaderText="«· ’‰Ì›">
                                     <ItemTemplate>
                                         <dx:ASPxComboBox ID="ddl_SpareMainId" runat="server" AutoPostBack="true" 
                                             CssClass="form-control" DropDownButton-Visible="False" enabled="false" 
                                             onselectedindexchanged="ddl_SpareMainId_SelectedIndexChanged">
                                             <Border BorderStyle="None" />
                                         </dx:ASPxComboBox>
                                     </ItemTemplate>
                                     <FooterTemplate>
                                         <dx:ASPxComboBox ID="ddl_SpareMainId" runat="server" AutoPostBack="true" 
                                             CssClass="form-control" dir="rtl" DropDownStyle="DropDown" 
                                              IncrementalFilteringMode="Contains" 
                                             onselectedindexchanged="ddl_SpareMainId_SelectedIndexChanged" 
                                             ValueType="System.String">
                                         </dx:ASPxComboBox>
                                     </FooterTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="ﬁÿ⁄ «·€Ì«—">
                                     <ItemTemplate>
                                         <dx:ASPxComboBox ID="ddl_SpareId" runat="server" AutoPostBack="true" 
                                             CssClass="form-control" DropDownButton-Visible="False" enabled="false">
                                             <Border BorderStyle="None" />
                                         </dx:ASPxComboBox>
                                     </ItemTemplate>
                                     <FooterTemplate>
                                         <dx:ASPxComboBox ID="ddl_SpareId" runat="server" CssClass="form-control" 
                                             dir="rtl" DropDownStyle="DropDown" 
                                             IncrementalFilteringMode="Contains" ValueType="System.String">
                                         </dx:ASPxComboBox>
                                     </FooterTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="⁄œœ">
                                     <ItemTemplate>
                                         <asp:TextBox ID="txt_SparCount" runat="server" AutoPostBack="True" 
                                             BackColor="White" BorderColor="White" BorderStyle="None" 
                                             CssClass="form-control" enabled="false" Text='<%# Eval("SparCount") %>'></asp:TextBox>
                                     </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:TextBox ID="txt_SparCount" runat="server" CssClass="form-control" 
                                             onkeypress="return onlyDotsAndNumbers(event);"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                             ControlToValidate="txt_SparCount" ErrorMessage="*" 
                                             ValidationGroup="spares"></asp:RequiredFieldValidator>
                                     </FooterTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="„·«ÕŸ« " Visible="False">
                                     <ItemTemplate>
                                         <asp:TextBox ID="txt_Notes" runat="server" BackColor="White" BorderStyle="None" 
                                             CssClass="form-control" enabled="false" Text='<%# Eval("Notes") %>' 
                                             TextMode="MultiLine"></asp:TextBox>
                                     </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:TextBox ID="txt_Notes" runat="server" CssClass="form-control" 
                                             TextMode="MultiLine"></asp:TextBox>
                                     </FooterTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="#">
                                     <ItemTemplate>
                                         <asp:Button ID="btn_deleteFromGrid" runat="server" ClientIDMode="Static" 
                                             CommandName="delete" onclick="btn_deleteFromGrid_Click" Text="Õ–›" />
                                     </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" 
                                             ValidationGroup="spares">«÷«›…</asp:LinkButton>
                                     </FooterTemplate>
                                 </asp:TemplateField>
                             </Columns>
                         </asp:GridView>
        
        </div>
    </div>
                
                
    
   
    
    
    

    
    
    
    
    
     <div class="row">
        <div class="col-md-12">
            <div class="card-box">
            
            
            
            <asp:GridView ID="gvPlans" CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="maintPlanId" 
                    onrowdatabound="gvPlans_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="maintPlanId" HeaderText="„”·”·" Visible="False" />
                        <asp:BoundField DataField="PlanName" HeaderText="«”„ Œÿ… «·’Ì«‰…" />
                       <%-- <asp:BoundField DataField="Notes" HeaderText="«”„ «·„«—ﬂ…" />--%>
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
                                <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="btn btn-default" onclick="lnk_Delete_Click" OnClientClick="return confirm(' √ﬂÌœ «·Õ–› ø');">
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