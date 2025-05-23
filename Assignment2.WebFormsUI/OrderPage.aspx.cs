using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Assignment2.BLL.Services;
using Assignment2.DAL;

namespace Assignment2.WebFormsUI
{
    [Serializable] // Required for storing in Session state
    public class TempOrderDetail
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; } // Store name for display
        public int Quantity { get; set; }
        public decimal UnitAmount { get; set; }
        // No OrderID needed here yet
    }
    public partial class OrderPage : System.Web.UI.Page
	{
        private List<TempOrderDetail> CurrentOrderDetails
        {
            get
            {
                if (Session["CurrentOrderDetails"] == null)
                {
                    Session["CurrentOrderDetails"] = new List<TempOrderDetail>();
                }
                return (List<TempOrderDetail>)Session["CurrentOrderDetails"];
            }
            set { Session["CurrentOrderDetails"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
		{
            if (Session["LoggedInUser"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            // --- End Check ---

            lblOrderStatus.Visible = false;
            if (!IsPostBack)
            {
                PopulateAgentDropDown();
                PopulateItemDropDown();
                txtOrderDate.Text = DateTime.Now.ToString("yyyy-MM-dd"); // Set default date
                BindDetailsGrid(); // Bind empty list initially
            }
        }

        // Populate Agent DropDown
        private void PopulateAgentDropDown()
        {
            try
            {
                using (AgentService agentService = new AgentService())
                {
                    ddlAgents.DataSource = agentService.GetAllAgents();
                    ddlAgents.DataBind(); // Binds based on DataTextField/ValueField set in markup
                                          // Re-add the default item after databinding if it got removed
                    if (ddlAgents.Items.FindByValue("") == null)
                        ddlAgents.Items.Insert(0, new ListItem("-- Select Agent --", ""));
                }
            }
            catch (Exception ex) { ShowStatusMessage($"Error loading agents: {ex.Message}", true); }
        }

        // Populate Item DropDown
        private void PopulateItemDropDown()
        {
            try
            {
                using (ItemService itemService = new ItemService())
                {
                    ddlItems.DataSource = itemService.GetAllItems();
                    ddlItems.DataBind();
                    if (ddlItems.Items.FindByValue("") == null)
                        ddlItems.Items.Insert(0, new ListItem("-- Select Item --", ""));
                }
            }
            catch (Exception ex) { ShowStatusMessage($"Error loading items: {ex.Message}", true); }
        }

        // Bind the temporary details list to the grid
        private void BindDetailsGrid()
        {
            gvOrderDetails.DataSource = this.CurrentOrderDetails;
            gvOrderDetails.DataBind();
        }

        // Show status messages
        private void ShowStatusMessage(string message, bool isError)
        {
            lblOrderStatus.Text = message;
            lblOrderStatus.ForeColor = isError ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            lblOrderStatus.Visible = true;
        }

        // Clear the entire form for a new order
        private void ClearFormForNewOrder()
        {
            txtOrderDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            ddlAgents.ClearSelection();
            ClearDetailInputFields();
            this.CurrentOrderDetails = null; // Clear the session list
            BindDetailsGrid(); // Rebind empty grid
        }

        // Clear only the detail input section
        private void ClearDetailInputFields()
        {
            ddlItems.ClearSelection();
            txtQuantity.Text = "1";
            txtUnitAmount.Text = string.Empty;
        }


        // Add Detail Button Click
        protected void btnAddDetail_Click(object sender, EventArgs e)
        {
            // Trigger validation for the detail section
            Page.Validate("AddDetailValidation");
            if (!Page.IsValid) return; // Stop if validation fails

            // Assume validation controls handle parsing checks (Type="Integer", Type="Currency")
            int itemId = Convert.ToInt32(ddlItems.SelectedValue);
            string itemName = ddlItems.SelectedItem.Text; // Get selected item name
            int quantity = Convert.ToInt32(txtQuantity.Text);
            decimal unitAmount = Convert.ToDecimal(txtUnitAmount.Text);

            // Add to the session list
            var currentDetails = this.CurrentOrderDetails; // Get list from session
            currentDetails.Add(new TempOrderDetail
            {
                ItemID = itemId,
                ItemName = itemName, // Store name for grid display
                Quantity = quantity,
                UnitAmount = unitAmount
            });
            this.CurrentOrderDetails = currentDetails; // Put updated list back in session

            BindDetailsGrid(); // Refresh the grid
            ClearDetailInputFields(); // Clear inputs for next detail
        }

        // Handle removing an item from the temporary grid/session list
        protected void gvOrderDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // The index of the row to delete corresponds to the index in the session list
            int indexToRemove = e.RowIndex;
            var currentDetails = this.CurrentOrderDetails;

            if (indexToRemove >= 0 && indexToRemove < currentDetails.Count)
            {
                currentDetails.RemoveAt(indexToRemove); // Remove from the list
            }

            this.CurrentOrderDetails = currentDetails; // Save updated list back to session
            BindDetailsGrid(); // Refresh grid
        }


        // Save Order Button Click
        protected void btnSaveOrder_Click(object sender, EventArgs e)
        {
            // Validate header fields explicitly (if not using validation groups properly)
            if (string.IsNullOrEmpty(ddlAgents.SelectedValue))
            {
                ShowStatusMessage("Please select an Agent.", true);
                return;
            }
            if (this.CurrentOrderDetails.Count == 0)
            {
                ShowStatusMessage("Cannot save order with no details. Please add items.", true);
                return;
            }

            // Assume date is valid due to TextMode="Date" or add server-side parse check
            DateTime orderDate = Convert.ToDateTime(txtOrderDate.Text);
            int agentId = Convert.ToInt32(ddlAgents.SelectedValue);

            // Convert TempOrderDetail list to the actual DAL OrderDetail list
            List<OrderDetail> detailsToSave = this.CurrentOrderDetails.Select(temp => new OrderDetail
            {
                ItemID = temp.ItemID,
                Quantity = temp.Quantity,
                UnitAmount = temp.UnitAmount
                // OrderID will be set by EF relationships when Order header is saved
            }).ToList();

            try
            {
                using (OrderService orderService = new OrderService())
                {
                    bool success = orderService.AddOrder(orderDate, agentId, detailsToSave);
                    if (success)
                    {
                        ShowStatusMessage("Order saved successfully!", false);
                        ClearFormForNewOrder(); // Clear everything for next order
                    }
                    else
                    {
                        ShowStatusMessage("Failed to save order. Please check inputs.", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowStatusMessage($"Error saving order: {ex.Message}", true);
            }
        }

        // Clear Order Button Click
        protected void btnClearOrder_Click(object sender, EventArgs e)
        {
            ClearFormForNewOrder();
            lblOrderStatus.Visible = false;
        }
	}
}