<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MaintRequest.ascx.cs" Inherits="HR.UserControls.BusesWorkShop.MaintRequest" %>
<div class="module-body">
                          

 <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="module-head">طلب صيانة </h4>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <div class="form-group col-md-6">
                    <label class="col-md-4 control-label">
                        <asp:Label ID="Label13" runat="server" Text="درجة اهمية الطلب"></asp:Label>
                    </label>
                    <div class="col-md-8 radio-st" dir="rtl">
                        <asp:RadioButton ID="rd_PriorUrgent" runat="server" Checked="true" GroupName="job"
                            Text="عاجل" />
                        <asp:RadioButton ID="rd_PriorHigh" GroupName="job" runat="server" Text="متوسط" />
                        <asp:RadioButton ID="rd_PriorLow" GroupName="job" runat="server" Text="منخفض" />
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="form-group col-md-3">
                    <label class="col-md-12 control-label">
                        <asp:Label ID="Label17" runat="server" Text="اسم الفرع"></asp:Label>
                    </label>
                    <div class="col-md-12 ">
                        <dx:ASPxComboBox Enabled="false" ID="ddl_BranchId" dir="rtl" runat="server" CssClass="form-control-com"
                            DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" EnableIncrementalFiltering="True"
                            ValueType="System.String" AutoPostBack="true"
                            OnSelectedIndexChanged="ddl_BranchId_SelectedIndexChanged">
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_BranchId"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group col-md-3">
                    <label class="col-md-12 control-label">
                        <asp:Label ID="Label2" runat="server" Text="القسم"></asp:Label>
                    </label>
                    <div class="col-md-12 text-right" dir="rtl">
                        <dx:ASPxComboBox ID="ddlSection" runat="server"  Enabled="false" 
                            CssClass="form-control-com" DropDownStyle="DropDown"
                            IncrementalFilteringMode="StartsWith" EnableIncrementalFiltering="True" ValueType="System.String"
                            dir="rtl" AutoPostBack="True"
                            OnSelectedIndexChanged="ddl_DepartementId_SelectedIndexChanged">
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="ddlSection"></asp:RequiredFieldValidator>
                    </div>
                </div>


                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                <div class="form-group col-md-3">
                    <label class="col-md-12 control-label">
                        <asp:Label ID="Label10" runat="server" Text=" التصنيف الرئسي للقسم فى دليل المواقع"></asp:Label>
                    </label>
                    <div class="col-md-12 text-right" dir="rtl">
                        <dx:ASPxComboBox ID="ddlmainddlSectionAccJobs" runat="server"
                            CssClass="form-control-com" DropDownStyle="DropDown"
                            IncrementalFilteringMode="StartsWith" EnableIncrementalFiltering="True" ValueType="System.String"
                            dir="rtl" AutoPostBack="True" OnSelectedIndexChanged="ddlmainddlSectionAccJobs_SelectedIndexChanged"
                           >
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="ddlSectionAccJobs"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group col-md-3">
                    <label class="col-md-12 control-label">
                        <asp:Label ID="Label14" runat="server" Text="القسم فى دليل المواقع"></asp:Label>
                    </label>
                    <div class="col-md-12 text-right" dir="rtl">
                        <dx:ASPxComboBox ID="ddlSectionAccJobs" runat="server"
                            CssClass="form-control-com" DropDownStyle="DropDown"
                            IncrementalFilteringMode="StartsWith" EnableIncrementalFiltering="True" ValueType="System.String"
                            dir="rtl" AutoPostBack="True" OnSelectedIndexChanged="ddlSectionAccJobs_SelectedIndexChanged"
                           >
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="ddlSectionAccJobs"></asp:RequiredFieldValidator>
                    </div>
                </div>


                <div class="form-group col-md-3">
                    <label class="col-md-12 control-label">
                        <asp:Label ID="Label15644" runat="server" Text="الموقع"></asp:Label>
                    </label>
                    <div class="col-md-12 text-right" dir="rtl">
                        <dx:ASPxComboBox ID="ddl_FloorId" runat="server" CssClass="form-control-com"
                            DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" EnableIncrementalFiltering="True"
                            ValueType="System.String" dir="rtl" AutoPostBack="True"
                            OnSelectedIndexChanged="ddl_FloorId_SelectedIndexChanged">
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1564" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_FloorId"></asp:RequiredFieldValidator>
                    </div>
                </div>




                <div class="form-group col-md-3">
                    <label class="col-md-12 control-label">
                        <asp:Label ID="Label1" runat="server" Text="الدور"></asp:Label>
                    </label>
                    <div class="col-md-12 text-right" dir="rtl">
                        <dx:ASPxComboBox ID="ddl_LocationId" runat="server" CssClass="form-control-com" DropDownStyle="DropDown"
                            IncrementalFilteringMode="StartsWith" EnableIncrementalFiltering="True" ValueType="System.String"
                            dir="rtl">
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_LocationId"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
                <div class="form-group col-md-3">
                    <label class="col-md-12 control-label">
                        <asp:Label ID="Label24" runat="server" Text="تاريخ الطلب"></asp:Label>
                    </label>
                    <div class="col-md-12 ">
                        <asp:TextBox ID="txt_RequestDate" ClientId="txt_RequestDate" Height="33px" CssClass="form-control "
                            runat="server" onkeydown="return false;" Enabled="true" Font-Bold="true" Font-Size="14px"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_RequestDate"></asp:RequiredFieldValidator>
                    </div>
                    <!-- input-group -->
                </div>
            </div>
            <div class="form-group col-md-3">
                <label class="col-md-12 control-label">
                    <asp:Label ID="Label4" runat="server" Text="ملاحظات"></asp:Label>
                </label>
                <div class="col-md-12 ">
                    <asp:TextBox ID="txt_Notes" Style="width: 100% !important;" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <!-- Page-Title -->
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">اعمال الصيانة المطلوبة </h3>
        </div>
        <div class="panel-body">

            <div class="row">
                <div class="col-md-12">
                <div class="col-md-12">
                    <div class="card-box row">
                        <%--   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>--%>
                        <div class="form-group col-md-4">
                            <label class="col-md-12 control-label">
                                <asp:Label ID="Label3" runat="server" Text="اسم المنقول الرئيسى"></asp:Label>
                            </label>
                            <div class="col-md-12 text-right" dir="rtl">
                                <dx:ASPxComboBox ID="ddl_MobileParentId" AutoPostBack="true" runat="server" CssClass="form-control-com"
                                    DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" EnableIncrementalFiltering="True"
                                    ValueType="System.String" dir="rtl" OnSelectedIndexChanged="ddl_MobileParentId_SelectedIndexChanged">
                                    <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                                </dx:ASPxComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Save"
                                    Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_MobileParentId"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group col-md-4">
                            <label class="col-md-12 control-label">
                                <asp:Label ID="Label5" runat="server" Text="اسم المنقول "></asp:Label>
                            </label>
                            <div class="col-md-12 text-right" dir="rtl">
                                <dx:ASPxComboBox ID="ddl_MobileId" runat="server" CssClass="form-control-com" DropDownStyle="DropDown"
                                    IncrementalFilteringMode="StartsWith" EnableIncrementalFiltering="True" ValueType="System.String"
                                    dir="rtl">
                                    <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                                </dx:ASPxComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Save"
                                    Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_MobileId"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group col-md-4">
                            <label class="col-md-12 control-label">
                                <asp:Label ID="Label7" runat="server" Text="اسم العمل المطلوب "></asp:Label>
                            </label>
                            <div class="col-md-12 text-right" dir="rtl">
                                <dx:ASPxComboBox ID="ddl_WorkId" runat="server" CssClass="form-control-com" DropDownStyle="DropDown"
                                    IncrementalFilteringMode="StartsWith" EnableIncrementalFiltering="True" ValueType="System.String"
                                    dir="rtl">
                                    <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                                </dx:ASPxComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Save"
                                    Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_WorkId"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group col-md-12">
                            <label class="col-md-12 control-label">
                                <asp:Label ID="Label6" runat="server" Text="وصف العمل"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="SaveSpare"
                                    Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_Description"></asp:RequiredFieldValidator>
                            </label>
                            <div class="col-md-12 ">
                                <asp:TextBox ID="txt_Description" Style="width: 100% !important;" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group col-md-12">
                            <asp:LinkButton ID="btnAddWork" CssClass="btn btn-danger" runat="server" ValidationGroup="SaveModels" OnClick="btnAddWork_Click"> <i class="fa fa-plus-circle"></i> اضافة عمل</asp:LinkButton>
                        </div>
                        <div class="col-md-12">
                            <asp:GridView ID="grd_WorksNeeded" runat="server" HeaderStyle-CssClass="warning" CssClass="table table-bordered"
                                AutoGenerateColumns="False" OnRowDataBound="grd_WorksNeeded_RowDataBound" OnRowDeleting="grd_WorksNeeded_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="العمل المطلوب">
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt_RequiredJob" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <dx:ASPxComboBox ID="ddl_RequiredJob" runat="server" CssClass="form-control-com" Enabled="false"
                                                DropDownButton-Visible="False">
                                                <Border BorderStyle="None" />
                                                <DropDownButton Visible="False">
                                                </DropDownButton>
                                            </dx:ASPxComboBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="اسم المنقول">
                                        <ItemTemplate>
                                            <dx:ASPxComboBox ID="ddl_MobileId" runat="server" CssClass="form-control-com" DropDownButton-Visible="False"
                                                Enabled="false">
                                                <Border BorderStyle="None" />
                                                <DropDownButton Visible="False">
                                                </DropDownButton>
                                            </dx:ASPxComboBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="وصف العمل المطلوب">
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt_Description" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Description" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ButtonType="Button" DeleteText="حذف" ShowDeleteButton="True" />
                                </Columns>

<HeaderStyle CssClass="warning"></HeaderStyle>
                            </asp:GridView>
                        </div>
                        <div class="form-group col-md-12">
                            <div id="divmsg3" runat="server">
                                <asp:Label ID="lblMsg3" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <%--         </ContentTemplate>
                </asp:UpdatePanel>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- end page title end breadcrumb -->
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">الصور </h3>
        </div>
        <div class="panel-body">

            <div class="row">
                <div class="col-md-12">
                    <div class="card-box row">
                        <div class="form-group col-md-4">
                            <label class="col-md-12 control-label">
                                <asp:Label ID="Label8" runat="server" Text="اختر صورة"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="SaveSpare"
                                    Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_Description"></asp:RequiredFieldValidator>
                            </label>
                            <div class="col-md-12 ">
                                <asp:FileUpload ID="FU_Pic" runat="server" accept=".gif, .png,.jpeg,.jpg" />
                            </div>
                        </div>
                        <div class="form-group col-md-4">
                            <label class="col-md-12 control-label">
                                <asp:Label ID="Label9" runat="server" Text="التعليق"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="SaveSpare"
                                    Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_PicDescription"></asp:RequiredFieldValidator>
                            </label>
                            <div class="col-md-12 ">
                                <asp:TextBox ID="txt_PicDescription" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group col-md-12">
                            <asp:LinkButton ID="SavePicture" CssClass="btn btn-danger" runat="server"
                                ValidationGroup="SaveModels" OnClick="SavePicture_Click"><i class="fa fa-plus-circle"></i> اضافة صورة</asp:LinkButton>
                        </div>
                        <div class="col-md-12">
                            <asp:GridView ID="grd_Pictures" HeaderStyle-CssClass="warning" runat="server" CssClass="table table-bordered"
                                AutoGenerateColumns="False" OnRowDeleting="grd_Pictures_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="PicturePath" HeaderText="مسار الصورة" SortExpression="PicturePath"
                                        Visible="False" />
                                    <asp:BoundField DataField="Description" HeaderText="التعليق" SortExpression="Description" />
                                    <asp:TemplateField HeaderText="الصورة">
                                        <ItemTemplate>
                                            <asp:Image Width="120px" Height="120px" ID="Image1" runat="server" ImageUrl='<%# Eval("PicturePath") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ButtonType="Button" DeleteText="حذف" ShowDeleteButton="True" />
                                </Columns>

<HeaderStyle CssClass="warning"></HeaderStyle>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="form-group col-md-12 text-left">
        <asp:Button ID="Save" CssClass="btn" runat="server" Text="حفظ" ValidationGroup="SaveModels"
            Width="150px" OnClick="Save_Click" />
    </div>
    <div class="form-group col-md-12">
        <div id="divMsg2" runat="server">
            <asp:Label ID="lblResult2" runat="server" Text=""></asp:Label>
        </div>
    </div>
                               
                    </div>

    <div class="form-group col-md-6">
                    <label class="col-md-3 control-label">
                        <asp:Label ID="lbl_Phase" runat="server" Text="المراحل "></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_Phases"></asp:RequiredFieldValidator>

                    </label>
                    <div class="col-md-6 text-right" dir="rtl">
                        <dx:ASPxComboBox ID="ddl_Phases" dir="rtl" runat="server"
                            CssClass="form-control" DropDownStyle="DropDown"
                            IncrementalFilteringMode="Contains" ValueType="System.String">
                            <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </dx:ASPxComboBox>

                    </div>

                </div>
