<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgentsPage.aspx.cs" Inherits="Assignment2.WebFormsUI.AgentsPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Manage Agents</h2>

<%-- Input Form Area --%>
<div>
    <asp:Label runat="server" Text="Agent ID:" AssociatedControlID="txtAgentId"></asp:Label>
    <asp:TextBox ID="txtAgentId" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
</div>
<div>
    <asp:Label runat="server" Text="Agent Name:" AssociatedControlID="txtAgentName"></asp:Label>
    <asp:TextBox ID="txtAgentName" runat="server" MaxLength="150"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvAgentName" runat="server" ErrorMessage="Agent Name is required." ControlToValidate="txtAgentName" ForeColor="Red" Display="Dynamic">*</asp:RequiredFieldValidator>
</div>
<div>
    <asp:Label runat="server" Text="Address:" AssociatedControlID="txtAddress"></asp:Label>
    <asp:TextBox ID="txtAddress" runat="server" MaxLength="255" TextMode="MultiLine" Rows="3" Width="300px"></asp:TextBox> <%-- Use MultiLine for address --%>
</div>
<br />
<div>
    <asp:Button ID="btnAddAgent" runat="server" Text="Add New Agent" OnClick="btnAddAgent_Click" />
    <asp:Button ID="btnUpdateAgent" runat="server" Text="Update Agent" OnClick="btnUpdateAgent_Click" />
    <asp:Button ID="btnDeleteAgent" runat="server" Text="Delete Agent" OnClick="btnDeleteAgent_Click" OnClientClick="return confirm('Are you sure you want to delete this agent? This may fail if they have orders.');" />
    <asp:Button ID="btnClearAgent" runat="server" Text="Clear Form" OnClick="btnClearAgent_Click" CausesValidation="false"/>
</div>
 <br />
<div>
     <asp:Label ID="lblAgentStatus" runat="server" EnableViewState="false"></asp:Label>
</div>
<hr />

<%-- GridView Display Area --%>
<h3>Current Agents</h3>
<asp:GridView ID="gvAgents" runat="server" AutoGenerateColumns="False" DataKeyNames="AgentID"
    OnSelectedIndexChanged="gvAgents_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Select" HeaderText="Action"/>
        <asp:BoundField DataField="AgentID" HeaderText="Agent ID" ReadOnly="True" SortExpression="AgentID" />
        <asp:BoundField DataField="AgentName" HeaderText="Agent Name" SortExpression="AgentName" />
        <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
    </Columns>
    <%-- Add Styles similar to ItemsPage GridView if desired --%>
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
</asp:GridView>
</asp:Content>
