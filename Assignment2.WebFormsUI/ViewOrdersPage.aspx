<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewOrdersPage.aspx.cs" Inherits="Assignment2.WebFormsUI.ViewOrdersPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>View Existing Orders</h2>

<asp:Label ID="lblViewOrdersStatus" runat="server" EnableViewState="false"></asp:Label>
<br />

<asp:GridView ID="gvOrdersList" runat="server" AutoGenerateColumns="False" DataKeyNames="OrderID"
    OnSelectedIndexChanged="gvOrdersList_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None" Width="80%">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="View Details" HeaderText="Action"/>
        <asp:BoundField DataField="OrderID" HeaderText="Order ID" ReadOnly="True" SortExpression="OrderID" />
        <asp:BoundField DataField="OrderDate" HeaderText="Order Date" ReadOnly="True" SortExpression="OrderDate" DataFormatString="{0:yyyy-MM-dd HH:mm}"/> <%-- Format Date --%>
        <asp:BoundField DataField="AgentName" HeaderText="Agent Name" ReadOnly="True" SortExpression="AgentName" />
    </Columns>
    <%-- Add Styles similar to other GridViews if desired --%>
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
</asp:GridView>
</asp:Content>
