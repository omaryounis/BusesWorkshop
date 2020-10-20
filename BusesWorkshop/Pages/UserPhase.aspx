<%@ Page Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="UserPhase.aspx.cs" Inherits="BusesWorkshop.Pages.UserPhase" Title="اسناد مراحل للمستخدمين" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <%--<asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none">--%>
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">اسناد مراحل للمستخدمين </h4>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Scripts/jquery-{version}.js"));
        var textSeparator = ";";
        function updateText() {
            var selectedItems = checkListBox.GetSelectedItems();
            checkComboBox.SetText(getSelectedItemsText(selectedItems));
        }
        function synchronizeListBoxValues(dropDown, args) {
            checkListBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = getValuesByTexts(texts);
            checkListBox.SelectValues(values);
            updateText(); // for remove non-existing texts
        }
        function getSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function getValuesByTexts(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = checkListBox.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }
    </script>
    <div class="row">
        <div class="col-md-12 block">
            <div class="card-box"> 

                <div class="form-group col-md-6" style="width:30%">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="lbl_Phase" runat="server" Text="المراحل "></asp:Label>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_Phases"></asp:RequiredFieldValidator>--%>

                    </label>
                    <div class="col-md-6 text-right" dir="rtl" style="width:80%">
                        <dx:ASPxComboBox ID="ddl_Phases" dir="rtl" runat="server"
                            CssClass="form-control" DropDownStyle="DropDown"
                            IncrementalFilteringMode="Contains" ValueType="System.String">
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </dx:ASPxComboBox>

                    </div>

                </div>




                <div class="form-group col-md-6"  style="width:30%">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="lbl_User" runat="server" Text="المستخدمين"></asp:Label>
                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Save"
                         Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_UserId"></asp:RequiredFieldValidator>--%>
                    </label>





                    <div class="col-md-9 text-right" dir="rtl">
                        <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="ddl_UserId" Width="285px" runat="server" AnimationType="None">
                            <DropDownWindowStyle BackColor="#EDEDED" />
                            <DropDownWindowTemplate>
                                <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="checkListBox" SelectionMode="CheckColumn"
                                    runat="server" Height="200"
                                    EnableSelectAll="true">
                                    <FilteringSettings ShowSearchUI="true" />
                                    <Border BorderStyle="None" />
                                    <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                    <Items>
                                    </Items>
                                    <ClientSideEvents SelectedIndexChanged="updateText" Init="updateText" />
                                </dx:ASPxListBox>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="padding: 4px">
                                            <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" Style="float: right">
                                                <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </DropDownWindowTemplate>
                            <ClientSideEvents TextChanged="synchronizeListBoxValues" DropDown="synchronizeListBoxValues" />
                        </dx:ASPxDropDownEdit>
                    </div>
                </div>
                <div class="form-group col-md-6"  style="width:30%">

                    <label class="col-md-3 control-label">
                        <asp:Label ID="lbl_isactive" runat="server" Text="الحاله "></asp:Label>


                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="rdbtn_IsActive"></asp:RequiredFieldValidator>
                    </label>

                    <%--   <div class="col-md-9">--%>
                    <div class="col-md-9 radio-st" dir="rtl">
                        <dx:ASPxRadioButtonList ID="rdbtn_IsActive" runat="server">
                            <Items>

                                <dx:ListEditItem Text="مفعل" Value="1" />
                                <dx:ListEditItem Text="غير مفعل" Value="0" />

                            </Items>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </dx:ASPxRadioButtonList>

                    </div>
                    
                 
                </div>
                   <div class="form-group col-md-3 col-sm-12 col-xs-12 text-center" style="width:auto;">
                        <label class="col-md-12 control-label col-sm-12 col-xs-12">
                          

                        </label>
                      
                      
                        <asp:Button ID="btnSave" runat="server" Text="حفظ" CssClass="btn btn-success pull-center" OnClick="BtnSave_Click"
                            ValidationGroup="Save" />
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <br />
                        <div class="clearfix">
                            <br />
                            <br />
                            <div id="divMsg2" runat="server">
                                <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
            </div>

        </div>






     
        <div class="form-group col-md-12">
            <div id="divMsg" runat="server">
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </div>
        </div>



        <div class="row">
            <div class="col-md-12">
                <div class="card-box" dir="rtl">
                    <dx:ASPxGridView ID="grd_Phases" runat="server" KeyFieldName="phases_Id" Width="100%"      HorizontalAlign="right">

                        <Columns>
                            <dx:GridViewDataColumn FieldName="phases_Id" Visible="false"  HeaderStyle-HorizontalAlign="Center"/>
                            <dx:GridViewDataColumn FieldName="phases_Name" Caption="اسم المرحله"   HeaderStyle-HorizontalAlign="Center"/>
                            <dx:GridViewDataColumn FieldName="Users_ID" Caption="عدد المستخدمين"   HeaderStyle-HorizontalAlign="Center"  Width="30%" />
                          <%--  <dx:GridViewDataColumn Name="ID" Caption="تعديل"   HeaderStyle-HorizontalAlign="Center" Width="20%">
                                <DataItemTemplate >

                                    <asp:LinkButton ID="lnk_Edit" OnClick="BtnEditClick" 
                                        runat="server" CssClass="btn btn-default">
                             <i class="fa fa-edit"></i>
                                    </asp:LinkButton>

                                </DataItemTemplate>

                            </dx:GridViewDataColumn>--%>
                        </Columns>
                        <Templates>
                            <DetailRow>
                                معرف المرحله:
                <dx:ASPxLabel runat="server" Text='<%# Eval("phases_Id") %>' Font-Bold="true" />
                                اسم  المرحله:
               <dx:ASPxLabel runat="server" Text='<%# Eval("phases_Name") %>' Font-Bold="true" />

                                <br />
                                <br />
                                <dx:ASPxGridView ID="grd_detailGridUsers" runat="server" KeyFieldName="phases_Id,User_Id"
                                    Width="100%" EnablePagingGestures="False" OnBeforePerformDataSelect="grd_Phases_BeforePerformDataSelect"  OnDataBinding="grd_Phases_DataBinding1" >
                                    <Columns>
                                        <dx:GridViewDataColumn Visible="false" FieldName="phases_Id" />
                                        <dx:GridViewDataColumn Visible="false" FieldName="User_ID" Name="User_ID" />
                                        <dx:GridViewDataColumn FieldName="Name" Name="Name" VisibleIndex="1" />

                                        <dx:GridViewDataColumn Caption="مفعل/غير مفعل " FieldName="IsActive" VisibleIndex="2">
                                            <DataItemTemplate>
                                                <dx:ASPxCheckBox ID="cb" runat="server" Checked='<%# Eval("IsActive") %>' Enabled="False" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>

                                     <dx:GridViewDataColumn VisibleIndex="4" Caption="Delete">
                                            <DataItemTemplate>

                                                <asp:LinkButton ID="lnk_Delete" runat="server" OnClick="BtnDeleteClick" CssClass="btn btn-default" OnClientClick="return confirm('تأكيد الحذف ؟');">
                                     <i class="fa fa-trash-o"></i>
                                                </asp:LinkButton>
                                            </DataItemTemplate>

                                        </dx:GridViewDataColumn>


                                    </Columns>


                                </dx:ASPxGridView>


                            </DetailRow>
                        </Templates>

                        <SettingsDetail ShowDetailRow="true" />
                    </dx:ASPxGridView>


                 

                </div>
            </div>
        </div>










    </div>


</asp:Content>
