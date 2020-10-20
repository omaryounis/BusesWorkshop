<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusCheck.aspx.cs" Inherits="BusesWorkshop.Pages.BusCheck"
    MasterPageFile="~/MasterPages/Master.Master" EnableEventValidation="false" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">

    <script language="javascript" type="text/javascript">
        function onlyNumbers(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        // Except only numbers and dot (.) for salary textbox
        function onlyDotsAndNumbers(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 46) {
                return true;
            }
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
      
    $(function() {

    var calendar = $.calendars.instance('islamic');
    $('[id*=txtCheckDate]').calendarsPicker({ calendar: calendar });
    });

    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- Page-Title -->
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    «·’Ì«‰… «·ÿ«—∆…</h4>
            </div>
        </div>
    </div>
    <!-- end page title end breadcrumb -->
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <div class="form-horizontal">
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label3" runat="server" Text="«·Õ«›·…"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:DropDownList class="form-control" ID="ddl_Vehcles" runat="server" 
                                AutoPostBack="True" onselectedindexchanged="ddl_Vehcles_SelectedIndexChanged">
                            </asp:DropDownList>
                            
                             <asp:DropDownList Visible="false"  class="form-control" ID="ddlBuses" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label6" runat="server" Text="«·Œœ„…"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:DropDownList ID="ddlServices" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label5" runat="server" Text="«· «—ÌŒ"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="SaveBus"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txtCheckDate"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <div class="input-group">
                                <asp:TextBox ID="txtCheckDate" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white">
                                </i></span>
                            </div>
                            <!-- input-group -->
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label7" runat="server" Text="⁄œœ «·ﬂÌ·Ê„ —« "></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtKMcount" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>



                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label8" runat="server" Text="Ê’› «·«⁄ÿ«·"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_Defects" runat="server"  TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label9" runat="server" Text="«·«Ã—«¡« "></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_Prodedures"  TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label97" runat="server" Text="„·«ÕŸ« "></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix" style="margin-bottom:10px;"></div>
                  <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                       <asp:GridView ID="grd_SparParts" runat="server" 
                        CssClass="table m-0 table-colored table-danger" 
                        AutoGenerateColumns="False" ShowFooter="True" 
                        onrowcreated="grd_SparParts_RowCreated" 
                        onrowdatabound="grd_SparParts_RowDataBound" 
                        onrowdeleting="grd_SparParts_RowDeleting" 
                            onrowcommand="grd_SparParts_RowCommand">
                        <Columns>
                        <asp:TemplateField HeaderText="«· ’‰Ì›">
                                <ItemTemplate>
                                    <dx:ASPxComboBox ID="ddl_SpareMainId" runat="server" AutoPostBack="true" 
                                        CssClass="form-control" enabled="false" 
                                        onselectedindexchanged="ddl_SpareMainId_SelectedIndexChanged" DropDownButton-Visible="False">
                                        <Border BorderStyle="None" />
                                    </dx:ASPxComboBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <dx:ASPxComboBox ID="ddl_SpareMainId" AutoPostBack="true"  runat="server" 
                                        CssClass="form-control" DropDownStyle="DropDown" IncrementalFilteringMode="Contains" 
                                  ValueType="System.String" dir="rtl"
                                        onselectedindexchanged="ddl_SpareMainId_SelectedIndexChanged">
                                    </dx:ASPxComboBox>
                                    
                                </FooterTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="ﬁÿ⁄ «·€Ì«—">
                                <ItemTemplate>
                                    <dx:ASPxComboBox ID="ddl_SpareId" runat="server" AutoPostBack="true" 
                                        CssClass="form-control" enabled="false" 
                                        onselectedindexchanged="ddl_SpareId_SelectedIndexChanged" DropDownButton-Visible="False">
                                   <Border BorderStyle="None" />
                                    </dx:ASPxComboBox>
                                </ItemTemplate>
                                <FooterTemplate>
   <%--                                 <dx:ASPxComboBox ID="ddl_SpareIdAdd" AutoPostBack="true"  runat="server" 
                                        CssClass="form-control" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" 
                                EnableIncrementalFiltering="True" ValueType="System.String" dir="rtl"
                                        onselectedindexchanged="ddl_SpareId_SelectedIndexChanged">
                                    </dx:ASPxComboBox>--%>
                                    <dx:ASPxComboBox ID="ddl_SpareIdAdd" runat="server" AutoPostBack="true"  runat="server" 
                                    CssClass="form-control" DropDownStyle="DropDown" IncrementalFilteringMode="Contains"
                                      ValueType="System.String" dir="rtl"
                                        onselectedindexchanged="ddl_SpareId_SelectedIndexChanged">
                                    </dx:ASPxComboBox>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-default" 
                                        onclick="LinkButton2_Click"> <span aria-hidden="true" class="glyphicon glyphicon-plus-sign"></span></asp:LinkButton>
                                 </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="”⁄—">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_SparCost" runat="server" AutoPostBack="True" 
                                        CssClass="form-control" enabled="false"  ontextchanged="txt_SparCost_TextChanged" 
                                        Text='<%# Eval("SparCost") %>' BackColor="White" BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_SparCost" runat="server" CssClass="form-control" 
                                        AutoPostBack="true" ontextchanged="txt_SparCost_TextChanged"> </asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="⁄œœ">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_SparCount" runat="server" AutoPostBack="True" enabled="false" 
                                        CssClass="form-control" ontextchanged="txt_SparCount_TextChanged" 
                                        Text='<%# Eval("SparCount") %>' BackColor="White" BorderColor="White" 
                                        BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_SparCount" runat="server"  CssClass="form-control" 
                                        AutoPostBack="True" ontextchanged="txt_SparCount_TextChanged"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="«·«Ã„«·Ï">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_SparTotal" runat="server" CssClass="form-control"  
                                        Enabled="False" Text='<%# Eval("SparTotal") %>' BackColor="White" 
                                        BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_SparTotal" enabled="false"  runat="server"  CssClass="form-control"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="«”„ «·›‰Ï">
                                <FooterTemplate>
                                    <dx:ASPxComboBox ID="ddl_EmployeeId" runat="server"
                                        CssClass="form-control" dir="rtl" DropDownStyle="DropDown" 
                                         IncrementalFilteringMode="Contains" 
                                        ValueType="System.String">
                                    </dx:ASPxComboBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <dx:ASPxComboBox ID="ddl_EmployeeId" runat="server" AutoPostBack="True" 
                                        CssClass="form-control" DropDownButton-Visible="False" enabled="False" 
                                        onselectedindexchanged="ddl_SpareMainId_SelectedIndexChanged"  ValueType="System.String">
                                        <Border BorderStyle="None" />
                                        <DropDownButton Visible="False">
                                        </DropDownButton>
                                    </dx:ASPxComboBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="«·«ÌœÏ «·⁄«„·…">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_LaborCost" enabled="false"  runat="server" AutoPostBack="True" 
                                        CssClass="form-control" ontextchanged="txt_LaborCost_TextChanged"  
                                        Text='<%# Eval("LaborCost") %>' BackColor="White" BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_LaborCost" runat="server" CssClass="form-control" 
                                        AutoPostBack="true" ontextchanged="txt_LaborCost_TextChanged"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="«·«Ã„«·Ï «·ﬂ·Ï">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_TotalCost" runat="server" CssClass="form-control"  enabled="false" 
                                       Text='<%# Eval("TotalCost") %>' BackColor="White" BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_TotalCost" runat="server"  CssClass="form-control" Enabled="false"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="„·«ÕŸ« " Visible="False">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_Notes" runat="server" TextMode="MultiLine" 
                                        CssClass="form-control" enabled="false"  
                                        Text='<%# Eval("Notes") %>' BackColor="White" BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_Notes" runat="server" TextMode="MultiLine"  CssClass="form-control"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:Button ID="btn_deleteFromGrid" runat="server" 
                                        onclick="btn_deleteFromGrid_Click" OnClientClick="return confirm(' √ﬂÌœ «·Õ–› ø');" Text="Õ–›" CommandName="delete" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">«÷«›…</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="clearfix"></div>
                    <hr />
                    <div class="clearfix"></div>
                    
                    <div class="form-group col-md-6">
                        <label class="col-md-5 control-label">
                            <asp:Label ID="Label2" runat="server" Text="«Ã„«·Ï ﬁÿ⁄ «·€Ì«—"></asp:Label>
                        </label>
                        <div class="col-md-3">
                            <asp:TextBox ID="txt_SparSum" Enabled="false" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    
                     <div class="form-group col-md-6">
                        <label class="col-md-5 control-label">
                            <asp:Label ID="Label10" runat="server" Text=" «—ÌŒ «Œ— ’—› ·ﬁÿ⁄… «·€Ì«—"></asp:Label>
                        </label>
                        <div class="col-md-3">
                            <asp:TextBox ID="txt_LastSparPay" Enabled="false" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                    <div class="clearfix"></div>
                    
                    <div class="form-group col-md-6">
                        <label class="col-md-5 control-label">
                            <asp:Label ID="Label1" runat="server" Text="«Ã„«·Ï «·«ÌœÏ «·⁄«„·…"></asp:Label>
                        </label>
                        <div class="col-md-3">
                            <asp:TextBox ID="txt_LaborSum" Enabled="false"  runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="clearfix"></div>
                    
                    <div class="form-group col-md-6">
                        <label class="col-md-5 control-label">
                            <asp:Label ID="Label4" runat="server" Text="«Ã„«·Ï ﬁÿ⁄ «·€Ì«— Ê «·«ÌœÏ «·⁄«„·… "></asp:Label>
                        </label>
                        <div class="col-md-3">
                            <asp:TextBox ID="txt_TotalSum" Enabled="false" runat="server" CssClass="form-control" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
               <%--     </ContentTemplate>
                 
                    </asp:UpdatePanel>--%>
                    
                    
                    
                    <div class="col-md-12">
                        <div class="form-group col-md-12">
                            <asp:Button ID="btnSave" runat="server" Text="Õ›Ÿ" CssClass="btn btn-success" ValidationGroup="SaveBus"
                                OnClick="btnSave_Click" />
                        </div>
                    </div>
                    <div class="form-group col-md-12">
                        <div id="divMsg2" runat="server">
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    
                    
                    
                    
                 
                    
                    
                    
                </div>
        </div>
    </div>
    </div>
    <%-- <div>
         
          <asp:GridView ID="gvCheck" runat="server" AutoGenerateColumns ="false" DataKeyNames="ID">
        <Columns>
         
         <asp:BoundField DataField="" HeaderText="" />
         <asp:BoundField DataField="" HeaderText="" />
         <asp:BoundField DataField="" HeaderText="" />
         
                <asp:TemplateField HeaderText=" ⁄œÌ·">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_Edit" 
                            runat="server" Text=" ⁄œÌ·" OnClick="lnk_Edit_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
        </Columns>
        
        </asp:GridView>
    
    </div>
    --%>
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
    
    
    
    
    
     <asp:Panel ID="pnl_AddSpare" runat="server" Visible="false">
        <div class="over-page">
        </div>
        <div class="modal-dialog" role="document">
            <div class="modal-content modal-content-st">
                <div class="modal-header text-right">
                    <h3>
                        «÷«›… ﬁÿ⁄… €Ì«—</h3>
                </div>
                <div class="modal-body text-center">
                    <strong>
                        <asp:Label ID="lbl_AddSpare" runat="server" Text=""></asp:Label></strong>
                             <div class="form-group col-md-3">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label11" runat="server" Text="«”„ ﬁÿ⁄… «·€Ì«—"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="SaveSpare"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_SpareName"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_SpareName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                        <div class="form-group col-md-3">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label12" runat="server" Text="”⁄— ﬁÿ⁄… «·€Ì«—"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="SaveSpare"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_SparePrice"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_SparePrice" runat="server" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
                
                
                <div class="form-group col-md-3">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label13" runat="server" Text=" ﬂ·›… «·«ÌœÌ «·⁄«„·…"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="SaveSpare"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_LabourCost"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_LabourCost" runat="server" CssClass="form-control" onkeypress="return onlyDotsAndNumbers(event);"></asp:TextBox>
                        </div>
                    </div>
              
                
                <div class="form-group col-md-3">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label14" runat="server" Text="„·«ÕŸ« "></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txt_Notes" TextMode="MultiLine"  runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="modal-footer text-left" style="">
                    <asp:Button ID="btn_CancelSpare" CssClass="btn btn-danger" runat="server" Text="≈·€«¡"
                          Width="100px" onclick="btn_CancelSpare_Click"/>
                    <asp:Button ID="btn_ContinueSpare" CssClass="btn btn-success" Width="100px" runat="server" Text="«” „—«—"
                      ValidationGroup="SaveSpare" onclick="btn_ContinueSpare_Click"/>
                </div>
                   <div class="form-group col-md-12">
                        <div id="pnl_Alert" runat="server">
                            <asp:Label ID="lbl_SparAlert" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
