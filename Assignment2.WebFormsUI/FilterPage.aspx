<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FilterPage.aspx.cs" Inherits="Assignment2.WebFormsUI.FilterPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Filter Reports</h2>

<asp:Label ID="lblFilterStatus" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
<br />


<fieldset style="margin-bottom: 15px; padding: 10px; border: 1px solid #ccc;">
    <legend>Select Filter Type</legend>
    <asp:RadioButtonList ID="rblFilterType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblFilterType_SelectedIndexChanged">
        <asp:ListItem Text="Best Selling Items (Top 10)" Value="BestSellers" Selected="True"></asp:ListItem>
        <asp:ListItem Text="Agent Order History" Value="AgentHistory"></asp:ListItem>
    </asp:RadioButtonList>
</fieldset>


<asp:Panel ID="pnlAgentFilter" runat="server" Visible="false" style="margin-bottom: 15px; padding: 10px; border: 1px solid #ccc;">
     <fieldset>
         <legend>Filter Criteria</legend>
         <div>
            <asp:Label runat="server" Text="Select Agent:" AssociatedControlID="ddlFilterAgents"></asp:Label>
            <asp:DropDownList ID="ddlFilterAgents" runat="server" DataTextField="AgentName" DataValueField="AgentID" AppendDataBoundItems="true" Width="250px">
                 <asp:ListItem Text="-- Select Agent --" Value=""></asp:ListItem>
            </asp:DropDownList>
             <asp:RequiredFieldValidator ID="rfvFilterAgent" runat="server" ControlToValidate="ddlFilterAgents" ErrorMessage="Agent is required for this filter." InitialValue="" ForeColor="Red" Display="Dynamic" Enabled="false">*</asp:RequiredFieldValidator> <%-- Initially Disabled --%>
         </div>
     </fieldset>
</asp:Panel>


<div>
    <asp:Button ID="btnApplyFilter" runat="server" Text="Apply Filter" OnClick="btnApplyFilter_Click" />
</div>
<hr />


<h3>Filter Results</h3>
 <asp:GridView ID="gvFilterResults" runat="server" AutoGenerateColumns="True" 
    CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%"
    EmptyDataText="No results found for the selected filter.">
      <AlternatingRowStyle BackColor="White" />
     
     <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
     <RowStyle BackColor="#EFF3FB" />
 </asp:GridView>
</asp:Content>
