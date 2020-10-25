<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="Request.aspx.cs" Inherits="BusesWorkshop.Pages.Request" %>

<%@ Register Src="~/UserControls/MaintRequestProcessing.ascx" TagPrefix="uc1" TagName="MaintRequestProcessing" %>


<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <dx:ASPxPageControl ID="MaintRequestTabPage" Width="100%" runat="server" CssClass="dxtcFixed tabpage-ds" ActiveTabIndex="0" EnableHierarchyRecreation="True" RightToLeft="True"
        OnActiveTabChanged="MaintRequestProcessingTabPage_ActiveTabChanged" OnTabClick="MaintRequestProcessingTabPage_TabClick">

        <TabPages>




            <dx:TabPage Visible="false" Text=" اعتماد طلب" Name="BasicData">
                <ContentCollection>
                    <dx:ContentControl>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">

                                    <div class="form-group col-md-6">
                                        <label class="col-md-3 control-label">
                                            <asp:Label ID="lbl_HoldReq" runat="server" Text="الطلبات المعلقة"></asp:Label>

                                        </label>
                                        <div class="col-md-6 text-right" dir="rtl">
                                            <asp:DropDownList ID="ddlHold" runat="server">
                                                <asp:ListItem Text="New" Value="0">New</asp:ListItem>
                                                <asp:ListItem Text="Hold" Value="1">Hold</asp:ListItem>
                                                <asp:ListItem Text="All" Value="2">All</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <asp:Button ID="btnSearch" runat="server" Text="بحث" CssClass="btn btn-success pull-left" OnClick="btnSearch_Click"
                                        ValidationGroup="Save" />
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
                    </dx:ContentControl>

                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text=" توزيع الطلب " Visible="false" Name="BasicData">
                <ContentCollection>
                    <dx:ContentControl>
                    </dx:ContentControl>

                </ContentCollection>
            </dx:TabPage>

            <dx:TabPage Text=" اجراء " Visible="false" Name="BasicData">
                <ContentCollection>
                    <dx:ContentControl>
                    </dx:ContentControl>

                </ContentCollection>
            </dx:TabPage>

        </TabPages>
    </dx:ASPxPageControl>

    <dx:ASPxGridView RightToLeft="True" ID="dxgrd_Requests" runat="server" KeyFieldName="LeftId" Width="100%"
        OnRowCommand="grd_advanceRequest_RowCommand" OnHtmlDataCellPrepared="dxgrd_Requests_HtmlDataCellPrepared">
        <SettingsBehavior AllowFocusedRow="true" />
        <SettingsAdaptivity AdaptivityMode="Off" AllowOnlyOneAdaptiveDetailExpanded="false" AdaptiveDetailColumnCount="7">
            <AdaptiveDetailLayoutProperties>
                <Styles>
                    <Disabled Font-Bold="true"></Disabled>

                </Styles>
            </AdaptiveDetailLayoutProperties>
        </SettingsAdaptivity>
        <Toolbars>
            <dx:GridViewToolbar EnableAdaptivity="true" Enabled="true">
                <Items>
                    <dx:GridViewToolbarItem Command="ExportToPdf" />
                    <dx:GridViewToolbarItem Command="ExportToXls" />
                    <dx:GridViewToolbarItem Command="ExportToXlsx" />
                </Items>
            </dx:GridViewToolbar>
        </Toolbars>
        <Columns>
            <dx:GridViewDataColumn FieldName="LeftId" Caption="مسلسل" VisibleIndex="0" />
            <dx:GridViewDataColumn FieldName="CompName" Caption="الشركة" VisibleIndex="2" />
            <dx:GridViewDataColumn FieldName="UserName" Caption="المستخدم" VisibleIndex="3" />
            <dx:GridViewDataColumn FieldName="requestTypes" Caption="نوع الطلب" VisibleIndex="3" />
            <dx:GridViewDataColumn FieldName="RequestDate" Caption="التاريخ" VisibleIndex="1" />
            <dx:GridViewDataColumn FieldName="defdateTime" Caption="الوقت المستغرق" VisibleIndex="3" />
            <dx:GridViewDataColumn FieldName="PhaseName" Caption=" إسم المرحله " VisibleIndex="3" />
            <dx:GridViewDataTextColumn VisibleIndex="4" Caption="#" HeaderStyle-CssClass="align-center" FieldName="None">
                <DataItemTemplate>
                    <dx:ASPxButton ID="btn_Close" runat="server" Text="اغلاق الطلب" CommandName="CloseRequest">
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btn_Hold" runat="server" Text=" Hold" CommandName="Hold">
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btn_Forward" runat="server" Text=" رفع الطلب " CommandName="Forward">
                    </dx:ASPxButton>

                    <dx:ASPxButton ID="btn_Aprove" runat="server" Text="تحويل" CommandName="Go" Width="25px"
                        
                        >
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btn_Details" runat="server" Text="تفاصيل" CommandName="Details" Width="25px">
                    </dx:ASPxButton>

                </DataItemTemplate>
                <HeaderStyle CssClass="align-center"></HeaderStyle>

                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
            </dx:GridViewDataTextColumn>


        </Columns>

        <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" />

        <SettingsSearchPanel Visible="False"></SettingsSearchPanel>
    </dx:ASPxGridView>

    <dx:ASPxPopupControl ID="PayAccountPOPOUP" runat="server" CloseAction="OuterMouseClick" LoadContentViaCallback="OnFirstShow"
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
                <dx:ASPxGridView ID="dxUserRequests" OnHtmlDataCellPrepared="dxUserRequests_HtmlDataCellPrepared" RightToLeft="True" runat="server" KeyFieldName="user_Id"
                    Width="100%" EnablePagingGestures="False">
                    <Columns>
                        <dx:GridViewDataColumn FieldName="user_Id" VisibleIndex="0" Visible="false" />
                        <dx:GridViewDataColumn FieldName="name" VisibleIndex="1" />
                        <%--<dx:GridViewDataColumn FieldName="RecCount" VisibleIndex="2" />--%>
                        <dx:GridViewDataTextColumn FieldName="None" VisibleIndex="3" Width="10%" Caption="#" HeaderStyle-CssClass="align-center">
                            <DataItemTemplate>
                                <dx:ASPxButton ID="btn_Foward" runat="server" Text="تحويل" OnClick="btnForward_Click" Width="25px">
                                </dx:ASPxButton>
                            </DataItemTemplate>

                            <HeaderStyle CssClass="align-center"></HeaderStyle>

                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                        </dx:GridViewDataTextColumn>


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
    <dx:ASPxPopupControl ID="pCAddCNotes" runat="server" CloseAction="OuterMouseClick" LoadContentViaCallback="OnFirstShow"
        PopupElementID="btn_Pay" PopupVerticalAlign="Below"
        AllowResize="True" AutoUpdatePosition="True"
        PopupHorizontalAlign="WindowCenter" AllowDragging="True"
        ShowFooter="True" Width="500px" Height="500px" HeaderText="اذكر سبب التعليق" ClientInstanceName="ClientPopupControl">
        <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MaxWidth="700px" />
        <HeaderStyle BackColor="black" Font-Size="Large" ForeColor="White" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <asp:HiddenField ID="mReqId" runat="server" />
                <asp:HiddenField ID="mRecNotes" runat="server" />
                <br />
                <asp:Button OnClick="btnAddHoldReaso_Click" CssClass="btn btn-success pull-left" Text="تعليق" runat="server" ID="btnAddHoldReaso" />
                <asp:TextBox ID="txtNotes" runat="server"></asp:TextBox>

                <br />
                <div class="clearfix"></div>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <FooterTemplate>
        </FooterTemplate>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="pcAddCloseomment" runat="server" CloseAction="OuterMouseClick" LoadContentViaCallback="OnFirstShow"
        PopupElementID="btn_Pay" PopupVerticalAlign="Below"
        AllowResize="True" AutoUpdatePosition="True"
        PopupHorizontalAlign="WindowCenter" AllowDragging="True"
        ShowFooter="True" Width="500px" Height="500px" HeaderText="اذكر سبب التعليق" ClientInstanceName="ClientPopupControl">
        <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MaxWidth="700px" />
        <HeaderStyle BackColor="black" Font-Size="Large" ForeColor="White" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                <asp:HiddenField ID="rID" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" />
                <br />
                <asp:Button OnClick="btnCloseCommente_Click" CssClass="btn btn-success pull-left" Text="تعليق" runat="server" ID="btnCloseCommente" />
                <asp:TextBox ID="txtCloseComment" runat="server"></asp:TextBox>

                <br />
                <div class="clearfix"></div>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <FooterTemplate>
        </FooterTemplate>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="PCDetail" runat="server" CloseAction="OuterMouseClick" LoadContentViaCallback="OnFirstShow"
        PopupElementID="btn_Pay" PopupVerticalAlign="Below"
        AllowResize="True" AutoUpdatePosition="True"
        PopupHorizontalAlign="WindowCenter" AllowDragging="True"
        ShowFooter="True" Width="500px" Height="500px" HeaderText=" تفاصيل الطلب " ClientInstanceName="ClientPopupControl">
        <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MinWidth="1000px" />
        <HeaderStyle BackColor="black" Font-Size="Large" ForeColor="White" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server" Width="80%">
                <asp:HiddenField ID="recID" runat="server" />
                <div class="row" runat="server" id="tiltle">
                    <div class="col-sm-1"></div>

                    <div class="col-sm-5">
                        <div class="page-title-box text-right">
                            <h4 class="page-title">الطلب</h4>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-4">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label4" runat="server" Text="رقم الطلب"></asp:Label>
                        </label>
                        <div class="col-md-9 text-right">
                            <asp:Label ID="lblRecId" runat="server" Text=""></asp:Label>

                        </div>

                    </div>

                    <div class="form-group col-md-4">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label7" runat="server" Text=" التاريخ"></asp:Label>
                        </label>
                        <div class="col-md-9 text-right">
                            <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>

                        </div>

                    </div>


                    <div class="form-group col-md-4">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="lblT" runat="server" Text="الوقت"></asp:Label>
                        </label>
                        <div class="col-md-9 text-right">
                            <asp:Label ID="lblTime" runat="server" Text=""></asp:Label>

                        </div>

                    </div>
                </div>
<%--                <div class="row" runat="server" id="Div1">
                    <div class="col-sm-1"></div>
                    <div class="col-sm-5">
                        <div class="page-title-box text-right">
                            <h4 class="page-title">تفاصيل الطلب  </h4>
                        </div>
                    </div>
                </div>--%>
                <div class="row">

                    <div class="form-group col-md-3">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label17" runat="server" Text="الشركة"></asp:Label>
                        </label>
                        <div class="col-md-9 text-right">
                            <asp:Label ID="lblCompName" runat="server" Text=""></asp:Label>

                        </div>

                    </div>

                    <div class="form-group col-md-3">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label3" runat="server" Text="الموقع الرئيسي"></asp:Label>
                        </label>
                        <div class="col-md-9 text-right">
                            <asp:Label ID="lblMainLoc" runat="server" Text=""></asp:Label>

                        </div>

                    </div>


                    <div class="form-group col-md-3">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label1" runat="server" Text="الموقع"></asp:Label>
                        </label>
                        <div class="col-md-9 text-right">
                            <asp:Label ID="lblLoc" runat="server" Text=""></asp:Label>

                        </div>

                    </div>
                    <div class="form-group col-md-3">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label5" runat="server" Text="الدور"></asp:Label>
                        </label>
                        <div class="col-md-9 text-right">
                            <asp:Label ID="lblFloor" runat="server" Text=""></asp:Label>

                        </div>

                    </div>
                </div>
               <%-- <div class="row" runat="server" id="Div2">
                    <div class="col-sm-1"></div>

                    <div class="col-sm-5">
                        <div class="page-title-box text-right ">
                            <h4 class="page-title">ملاحظات  </h4>
                        </div>
                    </div>
                </div>--%>
                <div class="row">
                    <label class="col-md-2 control-label">
                        <asp:Label ID="Label2" runat="server" Text="الملاحظات"></asp:Label>
                    </label>
                    <div class="col-md-10 text-right">
                        <asp:Label ID="lblNotes" runat="server" Text=""></asp:Label>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">

                        <asp:GridView ID="grd_WorksNeeded" runat="server"
                            CssClass="table m-0 table-colored table-danger"
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="MobileName" HeaderText="اسم المنقول"
                                    SortExpression="MobileName" />
                                <asp:BoundField DataField="ConfigDetailName" HeaderText="اسم العمل المطلوب"
                                    SortExpression="ConfigDetailName" />
                                <asp:BoundField DataField="PicDescription" HeaderText="الوصف"
                                    SortExpression="PicDescription" />


                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="grd_Pictures" runat="server" Visible="true"
                            CssClass="table m-0 table-colored table-danger"
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="PicturePath" HeaderText="مسار الصورة"
                                    SortExpression="PicturePath" Visible="False" />
                                <asp:BoundField DataField="Description" HeaderText="التعليق"
                                    SortExpression="Description" />
                                <asp:TemplateField HeaderText="الصورة">
                                    <ItemTemplate>
                                        <%--<asp:Image Width="350px" Height="350px" ID="Image1" runat="server" ImageUrl='<%# Eval("PicturePath") %>' />--%>
                                        <dx:ASPxHyperLink runat="server" Text="عرض الصورة" NavigateUrl='<%# Eval("PicturePath") %>' Target="_blank"></dx:ASPxHyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <%--<uc1:MaintRequestProcessing runat="server" ID="MaintRequestProcessing" />--%>

                <div class="clearfix"></div>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <FooterTemplate>
        </FooterTemplate>
    </dx:ASPxPopupControl>

</asp:Content>
