<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemsPage.aspx.cs" Inherits="Assignment2.WebFormsUI.ItemsPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Manage Items</h2>

<%-- Input Form Area --%>
<div>
    <asp:Label runat="server" Text="Item ID:" AssociatedControlID="txtItemId"></asp:Label>
    <asp:TextBox ID="txtItemId" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
</div>
<div>
    <asp:Label runat="server" Text="Item Name:" AssociatedControlID="txtItemName"></asp:Label>
    <asp:TextBox ID="txtItemName" runat="server" MaxLength="200"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ErrorMessage="Item Name is required." ControlToValidate="txtItemName" ForeColor="Red" Display="Dynamic">*</asp:RequiredFieldValidator>
</div>
<div>
    <asp:Label runat="server" Text="Size:" AssociatedControlID="txtSize"></asp:Label>
    <asp:TextBox ID="txtSize" runat="server" MaxLength="50"></asp:TextBox>
</div>
<br />
<div>
    <asp:Button ID="btnAdd" runat="server" Text="Add New" OnClick="btnAdd_Click" />
    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this item?');" />
    <asp:Button ID="btnClear" runat="server" Text="Clear Form" OnClick="btnClear_Click" CausesValidation="false"/>
</div>
 <br />
<div>
     <asp:Label ID="lblStatus" runat="server" EnableViewState="false"></asp:Label> <%-- Disable ViewState for status messages --%>
</div>
<hr />

<%-- GridView Display Area --%>
<h3>Current Items</h3>
<asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" DataKeyNames="ItemID"
    OnSelectedIndexChanged="gvItems_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <%-- Command Field for Selection --%>
        <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Select" HeaderText="Action"/>
        <%-- Bound Fields for Data --%>
        <asp:BoundField DataField="ItemID" HeaderText="Item ID" ReadOnly="True" SortExpression="ItemID" />
        <asp:BoundField DataField="ItemName" HeaderText="Item Name" SortExpression="ItemName" />
        <asp:BoundField DataField="Size" HeaderText="Size" SortExpression="Size" />
    </Columns>
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#F5F7FB" />
    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
    <SortedDescendingCellStyle BackColor="#E9EBEF" />
    <SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>
</asp:Content>
