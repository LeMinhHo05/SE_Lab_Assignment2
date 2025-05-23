<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderPage.aspx.cs" Inherits="Assignment2.WebFormsUI.OrderPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create New Order</h2>

<%-- Order Header Section --%>
<fieldset style="margin-bottom: 15px; padding: 10px; border: 1px solid #ccc;">
    <legend>Order Header</legend>
    <div>
        <asp:Label runat="server" Text="Order Date:" AssociatedControlID="txtOrderDate"></asp:Label>
        <asp:TextBox ID="txtOrderDate" runat="server" TextMode="Date"></asp:TextBox> <%-- HTML5 Date input --%>
        <asp:RequiredFieldValidator ID="rfvOrderDate" runat="server" ControlToValidate="txtOrderDate" ErrorMessage="Order Date is required." ForeColor="Red" Display="Dynamic">*</asp:RequiredFieldValidator>
    </div>
    <br />
    <div>
        <asp:Label runat="server" Text="Select Agent:" AssociatedControlID="ddlAgents"></asp:Label>
        <asp:DropDownList ID="ddlAgents" runat="server" DataTextField="AgentName" DataValueField="AgentID" AppendDataBoundItems="true">
            <asp:ListItem Text="-- Select Agent --" Value=""></asp:ListItem> <%-- Default empty item --%>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvAgent" runat="server" ControlToValidate="ddlAgents" ErrorMessage="Agent is required." InitialValue="" ForeColor="Red" Display="Dynamic">*</asp:RequiredFieldValidator>
    </div>
</fieldset>

<%-- Add Order Detail Section --%>
<fieldset style="margin-bottom: 15px; padding: 10px; border: 1px solid #ccc;">
    <legend>Add Order Detail</legend>
    <div>
        <asp:Label runat="server" Text="Select Item:" AssociatedControlID="ddlItems"></asp:Label>
        <asp:DropDownList ID="ddlItems" runat="server" DataTextField="ItemName" DataValueField="ItemID" AppendDataBoundItems="true" Width="250px">
             <asp:ListItem Text="-- Select Item --" Value=""></asp:ListItem>
        </asp:DropDownList>
         <asp:RequiredFieldValidator ID="rfvItemRequired" runat="server" ControlToValidate="ddlItems" ErrorMessage="Item selection is required to add detail." InitialValue="" ForeColor="Red" Display="Dynamic" ValidationGroup="AddDetailValidation">*</asp:RequiredFieldValidator>
    </div>
    <br />
     <div>
        <asp:Label runat="server" Text="Quantity:" AssociatedControlID="txtQuantity"></asp:Label>
        <asp:TextBox ID="txtQuantity" runat="server" TextMode="Number" Width="80px" Text="1"></asp:TextBox>
         <asp:RequiredFieldValidator ID="rfvQuantityRequired" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Quantity is required." ForeColor="Red" Display="Dynamic" ValidationGroup="AddDetailValidation">*</asp:RequiredFieldValidator>
         <asp:RangeValidator ID="rvQuantity" runat="server" ControlToValidate="txtQuantity" MinimumValue="1" MaximumValue="10000" Type="Integer" ErrorMessage="Quantity must be between 1 and 10000." ForeColor="Red" Display="Dynamic" ValidationGroup="AddDetailValidation">*</asp:RangeValidator>
    </div>
     <br />
     <div>
        <asp:Label runat="server" Text="Unit Amount:" AssociatedControlID="txtUnitAmount"></asp:Label>
        <asp:TextBox ID="txtUnitAmount" runat="server" Width="100px"></asp:TextBox>
         <asp:RequiredFieldValidator ID="rfvAmountRequired" runat="server" ControlToValidate="txtUnitAmount" ErrorMessage="Unit Amount is required." ForeColor="Red" Display="Dynamic" ValidationGroup="AddDetailValidation">*</asp:RequiredFieldValidator>
         <asp:CompareValidator ID="cvAmount" runat="server" ControlToValidate="txtUnitAmount" Operator="DataTypeCheck" Type="Currency" ErrorMessage="Invalid amount format." ForeColor="Red" Display="Dynamic" ValidationGroup="AddDetailValidation">*</asp:CompareValidator>
         <asp:RangeValidator ID="rvAmount" runat="server" ControlToValidate="txtUnitAmount" MinimumValue="0" MaximumValue="1000000" Type="Currency" ErrorMessage="Amount must be non-negative." ForeColor="Red" Display="Dynamic" ValidationGroup="AddDetailValidation">*</asp:RangeValidator>
    </div>
     <br />
    <div>
        <asp:Button ID="btnAddDetail" runat="server" Text="Add Detail To Order" OnClick="btnAddDetail_Click" ValidationGroup="AddDetailValidation"/>
    </div>
</fieldset>

<%-- Current Order Details Grid --%>
<h3>Order Details (Current Order)</h3>
<asp:GridView ID="gvOrderDetails" runat="server" AutoGenerateColumns="False"
    CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%"
    EmptyDataText="No details added to the current order yet."
    OnRowDeleting="gvOrderDetails_RowDeleting">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <%-- Need TemplateFields to display potentially non-bound data like ItemName and add delete button --%>
        <asp:BoundField DataField="ItemID" HeaderText="Item ID" ReadOnly="True" />
        <asp:TemplateField HeaderText="Item Name">
            <ItemTemplate>
                <%-- We'll populate this in code-behind or store name in session object --%>
                <asp:Label ID="lblGridItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:BoundField DataField="Quantity" HeaderText="Quantity" ReadOnly="True" />
         <asp:BoundField DataField="UnitAmount" HeaderText="Unit Amount" ReadOnly="True" DataFormatString="{0:C2}" />
         <asp:TemplateField HeaderText="Line Total">
             <ItemTemplate>
                 <%# (Convert.ToInt32(Eval("Quantity")) * Convert.ToDecimal(Eval("UnitAmount"))).ToString("C2") %>
             </ItemTemplate>
         </asp:TemplateField>
        <%-- Delete Button Column --%>
        <asp:CommandField ShowDeleteButton="True" ButtonType="Button" DeleteText="Remove" HeaderText="Action"/>
    </Columns>
    <%-- Add Styles if desired --%>
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <RowStyle BackColor="#EFF3FB" />
</asp:GridView>
<hr />

<%-- Action Buttons --%>
<div>
     <asp:Button ID="btnSaveOrder" runat="server" Text="Save Complete Order" OnClick="btnSaveOrder_Click" />
     <asp:Button ID="btnClearOrder" runat="server" Text="Clear / New Order" OnClick="btnClearOrder_Click" CausesValidation="false"/>
</div>
<br />
<div>
    <asp:Label ID="lblOrderStatus" runat="server" EnableViewState="false"></asp:Label>
</div>
</asp:Content>
