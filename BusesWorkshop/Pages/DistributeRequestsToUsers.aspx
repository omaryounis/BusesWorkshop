<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="DistributeRequestsToUsers.aspx.cs" Inherits="BusesWorkshop.Pages.DistributeRequestsToUsers" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
    <script type="text/javascript">
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <!-- Page-Title -->
  <%--  <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">توزيع الطلبات على المستخدمين</h4>
            </div>
        </div>
    </div>
       <div class="form-group col-md-12">
                        <div id="divMsg" runat="server">
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
    <div>
        <div class="row">
            <div class="col-sm-12">
                <dx:ASPxDropDownEdit Visible="true" 
                    ClientInstanceName="checkComboBox" ID="ddl_UserId" Width="285px" runat="server" AnimationType="None">
                    <DropDownWindowStyle BackColor="#EDEDED" />
                    <DropDownWindowTemplate>
                        <dx:ASPxListBox Width="100%" ID="listBox"
                            ClientInstanceName="checkListBox" SelectionMode="CheckColumn"
                            runat="server" Height="200"
                            EnableSelectAll="true">
                            <FilteringSettings ShowSearchUI="true" />
                            <Border BorderStyle="None" />
                            <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                            <Items>
                            </Items>
                            <ClientSideEvents SelectedIndexChanged="updateText" Init="updateText" />
                        </dx:ASPxListBox>
                    </DropDownWindowTemplate>
                </dx:ASPxDropDownEdit>
            </div>
        </div>
          <div class="row">
            <div class="col-sm-12">
                                <asp:Button ID="btnSave" OnClick="btnSave_Click" Text="Add" runat="server"></asp:Button>
                </div>
              </div>
        <table style="width: 100%">
            <tr>
                <td style="padding: 4px">
                    <dx:ASPxButton ID="btnCloseddl" OnClick="btnCloseddl_Click" AutoPostBack="False" runat="server" Text="Close" Style="float: right">
                        <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>--%>
    <dx:ASPxPopupControl ID="PayAccountPOPOUP" runat="server"  CloseAction="OuterMouseClick" LoadContentViaCallback="OnFirstShow"
                    PopupElementID="btn_Pay" PopupVerticalAlign="Below"
                    AllowResize="True" AutoUpdatePosition="True"
                    PopupHorizontalAlign="WindowCenter" AllowDragging="True"
                    ShowFooter="True" Width="500px" Height="500px" HeaderText=" اسم المستخدم المحول الية " ClientInstanceName="ClientPopupControl">
                    <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MaxWidth="700px" />
                    <HeaderStyle BackColor="black" Font-Size="Large" ForeColor="White" />
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PayAccountPOPOUPContent" runat="server">
                            <asp:HiddenField ID="ID" runat="server" />
                            <asp:HiddenField ID="RequestID" runat="server" />
                            <br />
                            <dx:ASPxGridView ID="dxUserRequests" OnHtmlDataCellPrepared="dxUserRequests_HtmlDataCellPrepared" RightToLeft="True" runat="server"  KeyFieldName="user_Id" OnBeforePerformDataSelect="dxgrd_Requests_BeforePerformDataSelect" OnDataBinding="dxUserRequests_DataBinding" OnDataBound="dxUserRequests_DataBound"
                    Width="100%" EnablePagingGestures="False">
                    <Columns>
                        <dx:GridViewDataColumn FieldName="user_Id" VisibleIndex="0" Visible="false" />
                        <dx:gridviewdatacolumn fieldname="name" visibleindex="1" />
                        <dx:GridViewDataColumn FieldName="RecCount" VisibleIndex="2" />
                              <dx:GridViewDataTextColumn  FieldName="(None)"  VisibleIndex="3" Width="10%" Caption="#" HeaderStyle-CssClass="align-center">
                                <DataItemTemplate>
                                    <dx:ASPxButton ID="btn_Foward"  runat="server" Text="تحويل" OnClick="btnForward_Click" Width ="25px">
                                    </dx:ASPxButton>
                                </DataItemTemplate>

                                <HeaderStyle CssClass="align-center"></HeaderStyle>

                                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                            </dx:GridViewDataTextColumn>

                        <%-- <dx:GridViewDataDropDownEditColumn VisibleIndex="3">
                            <DataItemTemplate>
                                <dx:ASPxDropDownEdit  
                                           
                                    
                                    ClientInstanceName="checkComboBox" ID="ddl_UserId" Width="285px" runat="server" AnimationType="None">
                                    <DropDownWindowStyle BackColor="#EDEDED" />
                                    <DropDownWindowTemplate>
                                        <dx:ASPxListBox Width="100%" ID="listBox" 
                                            ClientInstanceName="checkListBox" SelectionMode="CheckColumn"
                                            runat="server" Height="200"
                                            EnableSelectAll="true">
                                            <FilteringSettings ShowSearchUI="true" />
                                            <Border BorderStyle="None" />
                                            <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                            <Items>
                                            </Items>
                                            <ClientSideEvents SelectedIndexChanged="updateText" Init="updateText" />
                                        </dx:ASPxListBox>
                                    </DropDownWindowTemplate>
                                </dx:ASPxDropDownEdit>
                               <table style="width: 100%">
                                    <tr>
                                        <td style="padding: 4px">
                                            <dx:ASPxButton ID="btnCloseddl" OnClick="btnCloseddl_Click" AutoPostBack="False" runat="server" Text="Close" Style="float: right">
                                                <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </DataItemTemplate>
                        </dx:GridViewDataDropDownEditColumn>
                        <dx:GridViewDataButtonEditColumn>
                            <DataItemTemplate>
                                <asp:Button ID="btnSave" OnClick="btnSave_Click" Text="Add" runat="server"></asp:Button>

                            </DataItemTemplate>

                        </dx:GridViewDataButtonEditColumn>
                         --%>
                    </Columns>
                    <Settings ShowFooter="True" />
                    <SettingsPager EnableAdaptivity="true" />
                    <Styles Header-Wrap="True" />

                </dx:ASPxGridView>
                           
                            
                            <br />
                            <div class="clearfix"></div>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                    <FooterTemplate>
                    </FooterTemplate>
                </dx:ASPxPopupControl>
    <dx:ASPxGridView RightToLeft="True" ID="dxgrd_Requests" runat="server" KeyFieldName="MaintReqId" Width="100%"  OnDataBinding="dxUserRequests_DataBinding"
         OnRowCommand="grd_advanceRequest_RowCommand"
        >
        <SettingsBehavior AllowFocusedRow="true" />
        <SettingsAdaptivity AdaptivityMode="Off" AllowOnlyOneAdaptiveDetailExpanded="false" AdaptiveDetailColumnCount="7">
            <AdaptiveDetailLayoutProperties>
                <Styles>
                    <Disabled Font-Bold="true"></Disabled>

                </Styles>
            </AdaptiveDetailLayoutProperties>
        </SettingsAdaptivity>
        <Columns>
            <dx:GridViewDataColumn FieldName="MaintReqId" Caption="مسلسل" VisibleIndex="0" />
            <dx:GridViewDataColumn FieldName="RequestDate"  Caption="التاريخ" VisibleIndex="1" />
            <dx:GridViewDataColumn FieldName="CompName"  Caption="الشركة"  VisibleIndex="2"  />
            <dx:GridViewDataColumn FieldName="UserName"  Caption="المستخدم المحول الية"  VisibleIndex="2"  />
          <dx:GridViewDataTextColumn VisibleIndex="3" Width="10%" Caption="#" HeaderStyle-CssClass="align-center">
                                <DataItemTemplate>
                                    <dx:ASPxButton ID="btn_Aprove" runat="server" Text="تحويل" CommandName="Pay" Width="25px">
                                    </dx:ASPxButton>
                                </DataItemTemplate>

                                <HeaderStyle CssClass="align-center"></HeaderStyle>

                                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                            </dx:GridViewDataTextColumn>

          <%--<dx:GridViewDataButtonEditColumn Width="10%" Caption="تحويل" VisibleIndex="3" >
                            <DataItemTemplate>
                                <asp:Button  ID="btnForward" OnClick="btnForward_Click" runat="server" Text="تحويل" Width="25px">
                                </asp:Button>
                                <dx:ASPxPopupControl ID="PopupControl"  runat="server" CloseAction="OuterMouseClick" LoadContentViaCallback="OnFirstShow" 
                                    PopupElementID="btnForward" PopupVerticalAlign="Below" PopupHorizontalAlign="LeftSides" AllowDragging="True"
                                    ShowFooter="True" Width="310px" Height="160px" HeaderText="Updatable content" ClientInstanceName="ClientPopupControl">
                                    <ContentCollection>
                                        <dx:PopupControlContentControl ID="PopupControlContentControl" runat="server">
                                            <div style="vertical-align: middle">
                                                <dx:ASPxDropDownEdit OnLoad="ddl_UserId_Load"  ClientInstanceName="checkComboBox"  ID="ddl_UserId" Width="285px" runat="server" AnimationType="None">
                                                   
                                                </dx:ASPxDropDownEdit>
                                                
                                                 
                                            </div>
                                        </dx:PopupControlContentControl>
                                    </ContentCollection>
                                    <FooterTemplate>
                                        <div style="display: table; margin: 6px 6px 6px auto;">
                                            <dx:ASPxButton ID="UpdateButton"  runat="server" Text="Update Content" AutoPostBack="False"
                                                ClientSideEvents-Click="function(s, e) { ClientPopupControl.PerformCallback(); }" />
                                        </div>
                                    </FooterTemplate>
                                </dx:ASPxPopupControl>
                            </DataItemTemplate>

                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                        </dx:GridViewDataButtonEditColumn>--%>
        </Columns>
   
    </dx:ASPxGridView>

</asp:Content>
