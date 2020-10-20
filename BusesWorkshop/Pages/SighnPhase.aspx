<%@ Page Title="" Language="C#" MasterPageFile="../MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="SighnPhase.aspx.cs" Inherits="BusesWorkshop.SighnPhase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
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

                           <label ></label>
                            
                            <br />
                            <div class="clearfix"></div>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                    <FooterTemplate>
                    </FooterTemplate>
                </dx:ASPxPopupControl>
    <dx:ASPxGridView RightToLeft="True" ID="dxgrd_Requests" runat="server" KeyFieldName="MaintReqId" Width="100%"
        OnRowCommand="grd_advanceRequest_RowCommand">
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
            <dx:GridViewDataColumn FieldName="RequestDate" Caption="التاريخ" VisibleIndex="1" />
            <dx:GridViewDataColumn FieldName="CompName" Caption="الشركة" VisibleIndex="2" />
            <dx:GridViewDataColumn FieldName="UserName" Caption="المستخدم" VisibleIndex="2" />
            <dx:GridViewDataTextColumn VisibleIndex="3"  Caption="#" HeaderStyle-CssClass="align-center">
                <DataItemTemplate>

                    <dx:ASPxButton ID="btn_Close" runat="server" Text="اغلاق الطلب" CommandName="CloseRequest">
                    </dx:ASPxButton>
             

                    <dx:ASPxButton ID="btn_Hold" runat="server" Text=" Hold" CommandName="Hold" >
                    </dx:ASPxButton>
             

                    <dx:ASPxButton ID="btn_Forward" runat="server" Text=" رفع الطلب " CommandName="Forward">
                    </dx:ASPxButton>
                

                    <dx:ASPxButton ID="btn_Details" runat="server" Text="تفاصيل الطلب " CommandName="Details" >
                    </dx:ASPxButton>
                </DataItemTemplate>
                <HeaderStyle CssClass="align-center"></HeaderStyle>

                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
            </dx:GridViewDataTextColumn>


        </Columns>

    </dx:ASPxGridView>
</asp:Content>
