<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Master.Master" CodeBehind="MaintRequestTechnical.aspx.cs" Inherits="BusesWorkshop.Pages.MaintRequestTechnical" %>




<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%--asmaa--%>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
    <link href="../assets/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
     
   <%-- <script src="../plugins/bootstrap-datepicker/js/MyPlugins.js"
        type="text/javascript"></script>--%>
     
    <script src="../assets/js/bootstrap-datetimepicker.min.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


    <script type="text/javascript">

        //$(function () {
        //   //  var calendar = $.calendars.instance('islamic');
        //  //  $('[id*=txt_RequestDate]').calendarsPicker({ calendar: calendar });
        //    $('[id*=txt_RequestDate]').datepicker({
        //        format: 'mm/dd/yyyy'
        //    });
        //});
        $(function () {
            $('[id*=txt_RequestDate]').datetimepicker({
           
            });

        });
       
    </script>
        
    
    <script type="text/javascript">
        // <![CDATA[
        var textSeparator = ";";
        function OnListBoxSelectionChanged(listBox, args) {
            if (args.index == -1)
                args.isSelected ? listBox.SelectAll() : listBox.UnselectAll();
            UpdateSelectAllItemState();
            UpdateText();
            var selectedItems = checkListBox.GetSelectedItems();
            for (i = 0; i < selectedItems.length; i++) {
                console.log(selectedItems[i].value)
            }
        }
        function UpdateSelectAllItemState() {
            IsAllSelected() ? checkListBox.SelectIndices([-1]) : checkListBox.UnselectIndices([-1]);
        }
        function IsAllSelected() {
            var selectedDataItemCount = checkListBox.GetItemCount() - (checkListBox.GetItem(0).selected ? 0 : 1);
            return checkListBox.GetSelectedItems().length == selectedDataItemCount;
        }
        function UpdateText() {
            var selectedItems = checkListBox.GetSelectedItems();
            checkComboBox.SetText(GetSelectedItemsText(selectedItems));
        }
        function SynchronizeListBoxValues(dropDown, args) {
            checkListBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = GetValuesByTexts(texts);
            checkListBox.SelectValues(values);
            UpdateSelectAllItemState();
            UpdateText(); // for remove non-existing texts
        }
        function GetSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                if (items[i].index != -1)
                    texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function GetValuesByTexts(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = checkListBox.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }
        // ]]>
    </script>

    <div class="row">
        <dx:ASPxPageControl ID="MaintRequestTabPage" Width="100%" runat="server" CssClass="dxtcFixed tabpage-ds" ActiveTabIndex="0" EnableHierarchyRecreation="True" RightToLeft="True">

        <TabPages>
            <dx:TabPage Text=" طلب الدعم الفني" Name="BasicData">
                <ContentCollection>
                    <dx:ContentControl>
                          <div class="row">
        <div class="col-md-12">
            <div class="card-box block raaduis">
               
                <div class="row" >

                     <label class="col-md-1 control-label">
                        <asp:Label ID="Label24" runat="server" Text="تاريخ الطلب"></asp:Label>
                    </label>
                      <div class="col-md-3">
                        <div class="input-group controls input-append date form_datetime"
                            data-date="1979-09-16T05:25:07Z" data-date-format="dd MM yyyy - HH:ii p">
                            <asp:TextBox ID="txt_RequestDate" ClientId="txt_RequestDate" CssClass="form-control "
                                runat="server" onkeydown="return false;"></asp:TextBox>
                            <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white"></i></span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_RequestDate"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    
                      
                     <label class="col-md-1 control-label">
                        <asp:Label ID="Label17" runat="server" Text="اسم الفرع"></asp:Label>
                    </label>
                     <div class="col-md-3" >
                         <dx:ASPxComboBox ID="ddl_companyId" dir="rtl" runat="server" 
                            CssClass="form-control" DropDownStyle="DropDown"
                            IncrementalFilteringMode="Contains" 
                            ValueType="System.String"  
                            OnSelectedIndexChanged="ddl_companyId_SelectedIndexChanged"
                            AutoPostBack="true" 
                            >
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                        </dx:ASPxComboBox>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_companyId"></asp:RequiredFieldValidator>
                    </div>
                    
                <label class="col-md-1 control-label">
                    <asp:Label ID="Label31" runat="server" Text="القسم الرئيسي"></asp:Label>
                </label>
                <div class="col-md-3" ><!--style="width:65%"-->
                    <dx:aspxcombobox ID="ddl_BranchIdInAcc" dir="rtl" runat="server" 
                        CssClass="form-control" DropDownStyle="DropDown"
                        IncrementalFilteringMode="Contains" 
                        ValueType="System.String" OnSelectedIndexChanged="ddl_BranchIdInAcc_SelectedIndexChanged" AutoPostBack="true" >
                        <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                    </dx:aspxcombobox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ValidationGroup="Save"
                        Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_BranchIdInAcc"></asp:RequiredFieldValidator>
                </div>

                      
                   

                </div>

              
 

                
           <div class="row" >
                <label class="col-md-1 control-label" >
                            <asp:Label ID="Label32" runat="server" Text="القسم"></asp:Label>
                        </label>
                <div class="col-md-3" > <!--style="width:80%"-->
                <dx:aspxcombobox ID="ddl_SectionId" dir="rtl" runat="server" 
                    CssClass="form-control" DropDownStyle="DropDown"
                    IncrementalFilteringMode="Contains" 
                    ValueType="System.String" OnSelectedIndexChanged="ddl_SectionId_SelectedIndexChanged" AutoPostBack="true" >
                    <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                </dx:aspxcombobox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ValidationGroup="Save"
                    Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_SectionId"></asp:RequiredFieldValidator>
            </div>
                <label class="col-md-1 control-label" >
                            <asp:Label ID="Label33" runat="server" Text="الدور"></asp:Label>
                        </label>
                <div class="col-md-3"> <!-- style="width:80%"-->
                            <dx:aspxcombobox ID="ddl_FloorId" dir="rtl" runat="server" 
                                CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" 
                                ValueType="System.String" OnSelectedIndexChanged="ddl_FloorId_SelectedIndexChanged" AutoPostBack="true" >
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:aspxcombobox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_FloorId"></asp:RequiredFieldValidator>
                        </div>
                <label class="col-md-1 control-label">
                        <asp:Label ID="Label20" runat="server" Text="الموقع الفرعي"></asp:Label>
                    </label>
                    <div class="col-md-3">
                        <dx:ASPxComboBox ID="ddl_RoomId" AutoPostBack="true" OnSelectedIndexChanged="ddl_RoomId_SelectedIndexChanged" dir="rtl" runat="server"
                            CssClass="form-control" DropDownStyle="DropDown"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.String">
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_RoomId"></asp:RequiredFieldValidator>
                    </div>
         </div> 
               

                <div class="row">
                     <label class="col-md-1 control-label" style="padding-left:45px !important;">
                        <asp:Label ID="Label4" runat="server" Text="ملاحظات"></asp:Label>

                    </label>
                    <div class="col-md-11">
                        <asp:TextBox ID="txt_Notes" runat="server"   CssClass="form-control" ></asp:TextBox>
                    </div>
                </div>

               <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" OnUnload="UpdatePanel_Unload">
                    <ContentTemplate>
                    </ContentTemplate>
                </asp:UpdatePanel>--%>


            </div>
        </div>
    </div>
   <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">اعمال الصيانة المطلوبة</h4>
            </div>
        </div>
    </div>
                          <ContentTemplate>
                        <div class="block raaduis">

                        <div class="row">
                                <div class="col-md-4">
                                    <asp:RadioButtonList ID="radio_IsAsset" RepeatDirection="Horizontal" runat="server" 
                                         OnSelectedIndexChanged="radio_IsAsset_SelectedIndexChanged"
                                        AutoPostBack="true"
                                        >
                                        <asp:ListItem Text="منقول" Value="0" ></asp:ListItem>
                                        <asp:ListItem Text="غير منقول" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div   visible="false" ID="AssetRow" runat="server" >
                            <label class="col-md-1 control-label">
                                <asp:Label ID="Label3" runat="server" Text="اسم المنقول الرئيسى"></asp:Label>
                            </label>
                            <div class="col-md-3 text-right" dir="rtl">

                                <dx:ASPxComboBox ID="ddl_AssetMasterId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                    IncrementalFilteringMode="Contains" ValueType="System.String"
                                    dir="rtl"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddl_AssetMasterId_SelectedIndexChanged">
                                    <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                                </dx:ASPxComboBox>
                                </div>
                             <label class="col-md-1 control-label">
                                <asp:Label ID="Label5" runat="server" Text="اسم المنقول "></asp:Label>
                            </label>
                            <div class="col-md-3 text-right" dir="rtl">

                                <dx:ASPxComboBox ID="ddl_SubAssetId" AutoPostBack="true" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                    IncrementalFilteringMode="Contains" ValueType="System.String"
                                    OnSelectedIndexChanged="ddl_SubAssetMasterId_SelectedIndexChanged"
                                    dir="rtl"
                                    >
                                    <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                                </dx:ASPxComboBox>
                            </div>

                         </div>      
                        </div> 
                        <div class="row">
                            <label class="col-md-1 control-label">
                                <asp:Label ID="Label7" runat="server" Text="اسم العمل المطلوب "></asp:Label>
                            </label>
                            <div class="col-md-3 text-right" dir="rtl">
                                <dx:ASPxComboBox ID="ddl_WorkId" AutoPostBack="true" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                    IncrementalFilteringMode="Contains" ValueType="System.String"
                                    dir="rtl" OnSelectedIndexChanged="ddl_WorkId_SelectedIndexChanged">
                                    <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                                </dx:ASPxComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Save"
                                    Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_WorkId"></asp:RequiredFieldValidator>
                            </div>
                            
                        <div  ID="RecDiv" runat="server"   visible="false">
                            <label class="col-md-1 control-label">
                                <asp:Label ID="Label2" runat="server" Text="التوصيات"></asp:Label>
                            </label>
                            <div class="col-md-3">
                          <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="ASPxDropDownEdit1" Width="100%" runat="server" AnimationType="None">
                                        <DropDownWindowStyle BackColor="#EDEDED" />
                                        <DropDownWindowTemplate>
                                            <dx:ASPxListBox Width="100%" ID="ddl_Rec"  ClientInstanceName="checkListBox" SelectionMode="CheckColumn"
                                                runat="server" Height="200" EnableSelectAll="true"
                                                 OnSelectedIndexChanged="ddl_Rec_SelectedIndexChanged"
                                                AutoPostBack="true">
                                                <FilteringSettings ShowSearchUI="true"/>
                                                <Border BorderStyle="None" />
                                                <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                               
                                                <ClientSideEvents SelectedIndexChanged="OnListBoxSelectionChanged"  />
                                            </dx:ASPxListBox>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="padding: 4px">
                                                        <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" style="float: right">
                                                            <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </DropDownWindowTemplate>
                                        <ClientSideEvents TextChanged="SynchronizeListBoxValues" DropDown="SynchronizeListBoxValues" />
                                    </dx:ASPxDropDownEdit>
                            </div>
                                   
                            </div>
                        </div>

                     


                     <div class="clearfix"></div>
                        <div class="row">
                            <label class="col-md-1 control-label">
                                <asp:Label ID="Label6" runat="server" Text="وصف العمل"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="SaveSpare"
                                    Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_Description"></asp:RequiredFieldValidator>
                            </label>
                            <div class="col-md-9">
                                <asp:TextBox ID="txt_Description" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                             <div class="col-md-2">
                                 <asp:Button Enabled="false" ID="btnAddWork" CssClass="btn btn-success" runat="server" Text="اضافة عمل"
                                    ValidationGroup="SaveModels" Width="150px" OnClick="btnAddWork_Click" />
                             </div>
                        </div>
                       

                        <asp:GridView ID="grd_WorksNeeded" runat="server"
                            CssClass="table m-0 table-colored table-danger"
                            AutoGenerateColumns="False"
                          OnRowDeleting="grd_WorksNeeded_RowDeleting"
                            >
                            <Columns> 
                                <asp:BoundField DataField="RequiredWork" HeaderText="العمل المطلوب" 
                                    SortExpression="RequiredWork" />
                                <asp:BoundField DataField="SubAssetName" HeaderText="المنقول" 
                                    SortExpression="SubAssetName" />
                                <asp:BoundField DataField="Description" HeaderText="وصف المنقول" 
                                    SortExpression="Description" />
                                <asp:BoundField DataField="AssetOrNot" HeaderText="منقول؟" 
                                    SortExpression="AssetOrNot" />
                                <asp:BoundField DataField="Recommendation" HeaderText="التوصيات" 
                                    SortExpression="Recommendation" />
                             
                                <asp:CommandField ButtonType="Button" DeleteText="حذف"
                                    ShowDeleteButton="True" />
                            </Columns>
                        </asp:GridView>

                        <div class="form-group col-md-12">
                            <div id="divmsg3" runat="server">
                                <asp:Label ID="lblMsg3" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

 </div>
                        
 

    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">الصور</h4>
            </div>
        </div>
    </div> 
     <div class="block raaduis">


                <div class="form-group col-md-6" style="width:auto;">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="Label8" runat="server" Text="اختر صورة"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="SaveSpare"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_Description"></asp:RequiredFieldValidator>
                    </label>
                    <div class="col-md-9" style="width:auto;padding-top:10px;">

                        <asp:FileUpload ID="FU_Pic" runat="server"  />
                    </div>
                </div>


                <div class="form-group col-md-6">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="Label9" runat="server" Text="التعليق"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="SaveSpare"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_PicDescription"></asp:RequiredFieldValidator>
                    </label>
                    <div class="col-md-9">
                        <asp:TextBox ID="txt_PicDescription" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>




                <div class="form-group col-md-12" style="width:auto;float:left;">
                    <asp:Button ID="SavePicture" CssClass="btn btn-success" runat="server" Text="اضافة صورة"
                        ValidationGroup="SaveModels" Width="150px" OnClick="SavePicture_Click" />
                </div>






                <asp:GridView ID="grd_Pictures" runat="server"
                    CssClass="table m-0 table-colored table-danger"
                    AutoGenerateColumns="False" OnRowDeleting="grd_Pictures_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="PicturePath" HeaderText="مسار الصورة"
                            SortExpression="PicturePath" Visible="False" />
                        <asp:BoundField DataField="Description" HeaderText="التعليق"
                            SortExpression="Description" />
                        <asp:TemplateField HeaderText="الصورة">
                            <ItemTemplate>
                                <asp:Image Width="120px" Height="120px" ID="Image1" runat="server" ImageUrl='<%# Eval("PicturePath") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Button" DeleteText="حذف"
                            ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>


    </div>

                    </ContentTemplate>
 
                      <%--  <asp:UpdatePanel ID="UpdatePanel2" runat="server"  OnUnload="UpdatePanel_Unload">
                  
                </asp:UpdatePanel>--%>
                    
    <div class="form-group col-md-12"style="width:auto;float:left;">
        <asp:Button ID="Save"
            CssClass="btn btn-success" runat="server" Text="حفظ"
            ValidationGroup="SaveModels" Width="150px" OnClick="Save_Click" />
    </div>

    <div class="form-group col-md-12" >
        <div id="divMsg2" runat="server">
            <asp:Label ID="lblResult2" runat="server" Text=""></asp:Label>
        </div>
    </div>





                    </dx:ContentControl>

                </ContentCollection>
            </dx:TabPage>




        </TabPages>
    </dx:ASPxPageControl>
    </div>
    




</asp:Content>
