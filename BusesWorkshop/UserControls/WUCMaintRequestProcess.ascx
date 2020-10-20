<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCMaintRequestProcess.ascx.cs" Inherits="BusesWorkshop.UserControls.WUCMaintRequestProcess" %>
<script type="text/javascript">


    $(function () {
        //	$.calendars.picker.setDefaults({renderer: $.calendars.picker.themeRollerRenderer}); // Requires jquery.calendars.picker.ext.js
        var calendar = $.calendars.instance('islamic');
        $('[id*=txt_RequestDate]').calendarsPicker({ calendar: calendar });
    });

</script>
<!-- Page-Title -->
<div class="row">
    <div class="col-sm-12">
        <div class="page-title-box text-right">
            <h4 class="page-title">طلبات صيانة لم يتم عمل اجراء لها</h4>
        </div>
    </div>
</div>
<!-- end page title end breadcrumb -->
<div class="row">
    <div class="col-md-12">
        <div class="card-box">
            <asp:GridView ID="grd_Requests" runat="server"
                CssClass="table m-0 table-colored table-danger" DataKeyNames="مسلسل"
                OnSelectedIndexChanged="grd_Requests_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField SelectText="اختيار" ShowSelectButton="True" />
                </Columns>
                <SelectedRowStyle BackColor="#ffebe9" />
            </asp:GridView>


        </div>
    </div>
</div>
<!-- Page-Title -->
<div class="row">
    <div class="col-sm-12">
        <div class="page-title-box text-right">
            <h4 class="page-title">طلب صيانة مبانى</h4>
        </div>
    </div>
</div>
<!-- end page title end breadcrumb -->
<div class="row">
    <div class="col-md-12">
        <div class="card-box">


            <div class="form-group col-md-6">

                <label class="col-md-3 control-label">
                    <asp:Label ID="Label13" runat="server" Text="درجة اهمية الطلب"></asp:Label>

                </label>
                <div class="col-md-9 radio-st" dir="rtl">
                    <asp:RadioButton ID="rd_PriorUrgent" runat="server" Checked="true"
                        GroupName="job" Text="عاجل" Enabled="False" />
                    <asp:RadioButton ID="rd_PriorHigh" GroupName="job"
                        runat="server" Text="متوسط" Enabled="False" />
                    <asp:RadioButton ID="rd_PriorLow" GroupName="job"
                        runat="server" Text="منخفض" Enabled="False" />
                </div>
            </div>









            <div class="form-group col-md-6">
                <label class="col-md-3 control-label">
                    <asp:Label ID="Label17" runat="server" Text="اسم الشركة"></asp:Label>
                </label>
                <div class="col-md-9">
                    <dx:ASPxComboBox ID="ddl_companyId" dir="rtl" runat="server"
                        CssClass="form-control" DropDownStyle="DropDown"
                        IncrementalFilteringMode="Contains"
                        ValueType="System.String" OnSelectedIndexChanged="ddl_companyId_SelectedIndexChanged" AutoPostBack="true" Enabled="false">
                        <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                    </dx:ASPxComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="Save"
                        Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_companyId"></asp:RequiredFieldValidator>
                </div>
            </div>





           <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label14" runat="server" Text="الموقع الرئيسى"></asp:Label>
                        </label>
                        <div class="col-md-9 text-right" dir="rtl">

                            <dx:ASPxComboBox ID="ddl_ParentId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"
                                dir="rtl" AutoPostBack="True" Enabled="False" ReadOnly="True">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_ParentId"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label19" runat="server" Text="الدور"></asp:Label>
                        </label>
                        <div class="col-md-9">
                            <dx:ASPxComboBox ID="ddl_FloorId" dir="rtl" runat="server"
                                CssClass="form-control" Enabled="false" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains"
                                ValueType="System.String" AutoPostBack="true">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_FloorId"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="form-group col-md-6">
                        <label class="col-md-3 control-label">
                            <asp:Label ID="Label1" runat="server" Text="اسم الموقع"></asp:Label>
                        </label>
                        <div class="col-md-9 text-right" dir="rtl">

                            <dx:ASPxComboBox ID="ddl_LocationId" runat="server" CssClass="form-control" DropDownStyle="DropDown"
                                IncrementalFilteringMode="Contains" ValueType="System.String"
                                dir="rtl" Enabled="False">
                                <ClientSideEvents Init="OnInit" LostFocus="OnLostFocus" GotFocus="OnGotFocus" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Save"
                                Display="Dynamic" ErrorMessage="*" ControlToValidate="ddl_LocationId"></asp:RequiredFieldValidator>
                        </div>
                    </div>


<%--                </ContentTemplate>
            </asp:UpdatePanel>--%>












            <div class="form-group col-md-6">
                <label class="col-md-3 control-label">
                    <asp:Label ID="Label24" runat="server" Text="تاريخ الطلب"></asp:Label>
                </label>
                <div class="col-md-9">
                    <div class="input-group">
                        <asp:TextBox ID="txt_RequestDate" ClientId="txt_RequestDate" CssClass="form-control "
                            runat="server" onkeydown="return false;" Enabled="False"></asp:TextBox>
                        <span class="input-group-addon bg-success b-0"><i class="mdi mdi-calendar text-white"></i></span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Save"
                            Display="Dynamic" ErrorMessage="*" ControlToValidate="txt_RequestDate"></asp:RequiredFieldValidator>
                    </div>
                    <!-- input-group -->
                </div>
            </div>










            <div class="form-group col-md-6">
                <label class="col-md-3 control-label">
                    <asp:Label ID="Label4" runat="server" Text="ملاحظات"></asp:Label>

                </label>
                <div class="col-md-9">
                    <asp:TextBox ID="txt_Notes" runat="server" TextMode="MultiLine"
                        CssClass="form-control" Enabled="False"></asp:TextBox>
                </div>
            </div>





























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
<div class="row">
    <div class="col-md-12">
        <div class="card-box">









            <asp:GridView ID="grd_WorksNeeded" runat="server"
                CssClass="table m-0 table-colored table-danger"
                AutoGenerateColumns="False" OnRowDataBound="grd_WorksNeeded_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="MobileName" HeaderText="اسم المنقول"
                        SortExpression="MobileName" />
                    <asp:BoundField DataField="ConfigDetailName" HeaderText="اسم العمل المطلوب"
                        SortExpression="ConfigDetailName" />
                    <asp:BoundField DataField="PicDescription" HeaderText="الوصف"
                        SortExpression="PicDescription" />
                </Columns>
            </asp:GridView>
            <div class="form-group col-md-12">
                <div id="divmsg3" runat="server">
                    <asp:Label ID="lblMsg3" runat="server" Text=""></asp:Label>
                </div>
            </div>








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
<div class="row">
    <div class="col-md-12">
        <div class="card-box">

            <asp:GridView ID="grd_Pictures" runat="server"
                CssClass="table m-0 table-colored table-danger"
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="PicturePath" HeaderText="مسار الصورة"
                        SortExpression="PicturePath" Visible="False" />
                    <asp:BoundField DataField="Description" HeaderText="التعليق"
                        SortExpression="Description" />
                    <asp:TemplateField HeaderText="الصورة">
                        <ItemTemplate>
                            <asp:Image Width="350px" Height="350px" ID="Image1" runat="server" ImageUrl='<%# Eval("PicturePath") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>







        </div>
    </div>
</div>
<div class="row">
    <div class="col-sm-12">
        <div class="page-title-box text-right">
            <h4 class="page-title">الاجراء</h4>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        <div class="card-box">



            <div class="form-group col-md-6">

                <label class="col-md-3 control-label">
                    <asp:Label ID="Label2" runat="server" Text="الاجراء"></asp:Label>

                </label>
                <div class="col-md-9 radio-st" dir="rtl">
                    <asp:CheckBox ID="rd_IsAccepted" runat="server" Text="اعتماد" />
                    <asp:CheckBox ID="rd_IsRejected"
                        runat="server" Text="رفض" />
                </div>
            </div>
            <div class="clearfix"></div>
            <hr />


            <div class="form-group col-md-12">
                <label class="col-md-3 control-label">
                    <asp:Label ID="Label3" runat="server" Text="الرسالة"></asp:Label>

                </label>
                <div class="col-md-12">
                    <asp:TextBox ID="txt_Message" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>

                </div>
            </div>





            <div class="form-group col-md-12">
                <asp:Button ID="btn_AddMessage" CssClass="btn btn-success" runat="server" Text="حفظ رد"
                    ValidationGroup="SaveModels" Width="150px"
                    OnClick="btn_AddMessage_Click" />
            </div>







            <div class="row">
                <div class="col-md-12">
                    <div class="card-box">

                        <asp:GridView ID="grd_Messages" runat="server"
                            CssClass="table m-0 table-colored table-danger"
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Name" HeaderText="الاسم"
                                    SortExpression="Name" Visible="true">
                                    <ItemStyle Width="25%"></ItemStyle>

                                </asp:BoundField>
                                <asp:BoundField DataField="Message" HeaderText="الرد"
                                    SortExpression="Message" Visible="true">
                                    <ItemStyle Width="85%"></ItemStyle>

                                </asp:BoundField>

                            </Columns>
                        </asp:GridView>







                    </div>
                </div>
            </div>


        </div>
    </div>
</div>
<div class="form-group col-md-12">
    <asp:Button ID="Save" CssClass="btn btn-success" runat="server" Text="اغلاق"
        ValidationGroup="SaveModels" Width="150px" OnClick="Save_Click" />
</div>
<div class="form-group col-md-12">
    <div class="form-group col-md-12">
        <div id="divMsg2" runat="server">
            <asp:Label ID="lblResult2" runat="server" Text=""></asp:Label>
        </div>
    </div>
</div>
