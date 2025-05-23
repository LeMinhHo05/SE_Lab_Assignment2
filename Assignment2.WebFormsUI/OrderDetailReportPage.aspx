<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderDetailReportPage.aspx.cs" Inherits="Assignment2.WebFormsUI.OrderDetailReportPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Order Detail Report</h2>

<asp:Label ID="lblReportError" runat="server" ForeColor="Red" Visible="false"></asp:Label>

<%-- Only show details if no error --%>
<div id="reportContent" runat="server">
    <fieldset style="margin-bottom: 15px; padding: 10px; border: 1px solid #ccc;">
        <legend>Order Information</legend>
         <p><strong>Order ID:</strong> <asp:Label ID="lblOrderIdValue" runat="server"></asp:Label></p>
         <p><strong>Order Date:</strong> <asp:Label ID="lblOrderDateValue" runat="server"></asp:Label></p>
         <p><strong>Agent Name:</strong> <asp:Label ID="lblAgentNameValue" runat="server"></asp:Label></p>
    </fieldset>

     <fieldset style="margin-bottom: 15px; padding: 10px; border: 1px solid #ccc;">
        <legend>Order Details</legend>
         <asp:GridView ID="gvReportDetails" runat="server" AutoGenerateColumns="False"
            CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
             <AlternatingRowStyle BackColor="White" />
             <Columns>
                 <%-- Define columns matching the anonymous type from BLL/DAL --%>
                 <asp:BoundField DataField="ItemName" HeaderText="Item Name" ReadOnly="True" />
                 <asp:BoundField DataField="Quantity" HeaderText="Quantity" ReadOnly="True" />
                 <asp:BoundField DataField="UnitAmount" HeaderText="Unit Amount" ReadOnly="True" DataFormatString="{0:C2}" />
                 <%-- Calculate LineTotal if not done in BLL/DAL or use TemplateField --%>
                  <asp:TemplateField HeaderText="Line Total">
                     <ItemTemplate>
                         <%# (Convert.ToInt32(Eval("Quantity")) * Convert.ToDecimal(Eval("UnitAmount"))).ToString("C2") %>
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Right" />
                 </asp:TemplateField>
             </Columns>
             <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
             <RowStyle BackColor="#EFF3FB" />
         </asp:GridView>
     </fieldset>
     <br />
    <asp:HyperLink ID="hlBackToOrders" runat="server" NavigateUrl="~/ViewOrdersPage.aspx">Back to Orders List</asp:HyperLink>
</div>
</asp:Content>
