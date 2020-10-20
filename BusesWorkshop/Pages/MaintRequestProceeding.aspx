<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="MaintRequestProceeding.aspx.cs" Inherits="BusesWorkshop.Pages.MaintRequestProceeding" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register src="../UserControls/WUCMaintRequestProcess.ascx" tagname="WUCMaintRequestProcess" tagprefix="uc1" %>
<%@ Register Src="~/UserControls/WUCMaintRequestProcess.ascx" TagPrefix="uc2" TagName="WUCMaintRequestProcess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
   <script src="../plugins/bootstrap-datepicker/js/MyPlugins.js" type="text/javascript"></script>
  <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
       
       <dx:ASPxPageControl ID="MaintRequestProcessingTabPage" Width="100%" runat="server" CssClass="dxtcFixed tabpage-ds" ActiveTabIndex="0" EnableHierarchyRecreation="True" RightToLeft="True" OnActiveTabChanged="MaintRequestProcessingTabPage_ActiveTabChanged" OnTabClick="MaintRequestProcessingTabPage_TabClick" >

    <TabPages>
        
 
  

            <dx:TabPage Text=" ÕíÇäÉ ØáÈ" Name="BasicData" >
                <ContentCollection>
                    <dx:ContentControl>
                    <uc2:WUCMaintRequestProcess runat="server" ID="WUCMaintRequestProcess" />

                    </dx:ContentControl>

                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="ØáÈ ÏÚã Ýäì    " Name="BasicData">
                <ContentCollection>
                     <dx:ContentControl>
                    <uc2:WUCMaintRequestProcess runat="server" ID="WUCSupportRequestProcess1" />

                    </dx:ContentControl>

                </ContentCollection>
            </dx:TabPage>
        </TabPages>
           </dx:ASPxPageControl>
  
  
</asp:Content>
