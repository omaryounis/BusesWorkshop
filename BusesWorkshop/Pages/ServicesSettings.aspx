<%@ Page Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="ServicesSettings.aspx.cs" Inherits="BusesWorkshop.Pages.ServicesSettings" Title="تهيئة الخدمات" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

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
    </script>
    <%-- <script> 
         $('#btnSave_Click').text("Click on button to hide div after 1 sec.");

         function GFG_Fun() {
             $("#divMsg2").delay(100).fadeOut(500);
           //  $('#GFG_DOWN').text("Div hides after 1 second.");
         }
        </script>  --%>

    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">الخدمات</h4>
            </div>
        </div>
    </div>



    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
              <div  class="form-group col-md-6" >
                <label class="col-md-3 control-label">
                            <asp:Label ID="lbl_serviceRequest" runat="server" Text="طلب الخدمه"></asp:Label>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*Required" ControlToValidate="RequesServiceId"></asp:RequiredFieldValidator>
                        </label>
                         <div class="col-md-9">
                            <dx:ASPxComboBox ID="RequesServiceId" dir="rtl" runat="server" 
                                CssClass="form-control"  DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"  ValueType="System.String" >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                                <Items>
                                    <dx:ListEditItem Text="طلب صيانه" Value="0" />
                                   <dx:ListEditItem Text="دعم فنى  " Value="1" />
                                   <dx:ListEditItem Text="الكل   " Value="2" />


                                </Items>
                            </dx:ASPxComboBox>
                         
                        </div>
                    

                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label3" runat="server" Text="الخدمة"></asp:Label>

                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtServiceName" CssClass="form-control" runat="server" ValidationGroup="Save"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*Required" ControlToValidate="txtServiceName"></asp:RequiredFieldValidator>

                        </div>
                    </div>

                    
                    <div class="clearfix"></div>


                    <div class="form-group col-md-6" style="display: none;">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label2" runat="server" Text="المدة الزمنية بالأشهر"></asp:Label>

                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtDateV" CssClass="form-control" runat="server" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>


                    <div class="clearfix"></div>


                    <div class="form-group col-md-6" style="display: none;">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label7" runat="server" Text="كيلومترات أقل من"></asp:Label>

                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtKVminus" CssClass="form-control" runat="server" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>


                    <div class="form-group col-md-6" style="display: none;">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label1" runat="server" Text="كيلومترات أكبر من"></asp:Label>

                        </label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtKVPlus" CssClass="form-control" runat="server" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group col-md-12">
                        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="حفظ"
                            OnClick="btnSave_Click" ValidationGroup="Save" />
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


    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <asp:GridView ID="gvServicesSetting" CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="ID">
                    <Columns>
                        <asp:BoundField DataField="ServiceName" HeaderText="الخدمة" />
                        <asp:BoundField DataField="DateV" HeaderText="المدة الزمنية" Visible="False" />
                        <asp:BoundField DataField="KVminus" HeaderText="كيلومترات أقل من"
                            Visible="False" />

                       
                          <asp:BoundField DataField="KVplus" HeaderText="كيلومترات أكبر من"
                            Visible="False" />
                         <asp:BoundField DataField="RequestType" HeaderText="طلب الخدمه"
                            Visible="true" />
                        <asp:TemplateField HeaderText="تعديل">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_Edit"
                                    runat="server" CssClass="btn btn-default" OnClick="lnk_Edit_Click">
                             <i class="fa fa-edit"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="حذف">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="btn btn-default" OnClick="lnk_Delete_Click" OnClientClick="return confirm('تأكيد الحذف ؟');">
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
