<%@ Page Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true"
    CodeBehind="Alarms.aspx.cs" Inherits="BusesWorkshop.Pages.Alarms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupPlaceHolder" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box text-right">
                <h4 class="page-title">
                    التنبيهات</h4>
            </div>
        </div>
    </div>
    
    
    
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <asp:GridView ID="gvAlarms" CssClass="table m-0 table-colored table-danger" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="AlarmId">
                    <Columns>
                        <asp:BoundField DataField="AlarmId" HeaderText="مسلسل" />
                        <asp:BoundField DataField="Notification" HeaderText="التنبيه" />
                        
                        
                        <asp:BoundField DataField="CompanyName" HeaderText="اسم الشركة" 
                            SortExpression="CompanyName" />
                        
                        
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
