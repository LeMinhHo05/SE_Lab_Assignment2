using Assignment2.BLL.Services;
using Assignment2.DAL;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment2.WebFormsUI
{
    public partial class ItemsPage : System.Web.UI.Page
    {
        // Use a single service instance per request, managed by using statement
        // private readonly ItemService _itemService = new ItemService(); // Don't do this in WebForms usually

        protected void Page_Load(object sender, EventArgs e)
        {
            // --- IMPORTANT: Check Login Status ---
            if (Session["LoggedInUser"] == null)
            {
                Response.Redirect("LoginPage.aspx"); // Redirect to login if not logged in
            }
            // --- End Check ---

            lblStatus.Visible = false; // Hide status label on load/postback
            if (!IsPostBack)
            {
                BindItemGrid();
                ClearForm(); // Ensure form is clear on initial load
            }
        }

        // Bind data to the GridView
        private void BindItemGrid()
        {
            try
            {
                using (ItemService itemService = new ItemService())
                {
                    List<Item> items = itemService.GetAllItems();
                    gvItems.DataSource = items;
                    gvItems.DataBind();
                }
            }
            catch (Exception ex)
            {
                ShowStatusMessage($"Error loading items: {ex.Message}", true);
            }
        }

        // Clear input fields
        private void ClearForm()
        {
            txtItemId.Text = string.Empty;
            txtItemName.Text = string.Empty;
            txtSize.Text = string.Empty;
            gvItems.SelectedIndex = -1; // Deselect grid row
            btnAdd.Enabled = true; // Enable Add button when clearing
            btnUpdate.Enabled = false; // Disable Update/Delete until row selected
            btnDelete.Enabled = false;
        }

        // Show status messages
        private void ShowStatusMessage(string message, bool isError)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = isError ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            lblStatus.Visible = true;
        }

        // Handle GridView row selection
        protected void gvItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Get the selected ItemID from DataKeys
                int itemId = Convert.ToInt32(gvItems.SelectedDataKey.Value);

                using (ItemService itemService = new ItemService())
                {
                    Item selectedItem = itemService.GetItemById(itemId);
                    if (selectedItem != null)
                    {
                        txtItemId.Text = selectedItem.ItemID.ToString();
                        txtItemName.Text = selectedItem.ItemName;
                        txtSize.Text = selectedItem.Size;

                        // Enable/Disable buttons based on selection
                        btnAdd.Enabled = false; // Disable Add when editing
                        btnUpdate.Enabled = true;
                        btnDelete.Enabled = true;
                    }
                    else
                    {
                        ShowStatusMessage("Selected item not found.", true);
                        ClearForm();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowStatusMessage($"Error loading selected item: {ex.Message}", true);
                ClearForm();
            }
        }

        // Add Button Click
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // Ensure validation controls passed client-side checks (if any)
            if (!Page.IsValid) return;

            string itemName = txtItemName.Text.Trim();
            string size = txtSize.Text.Trim();

            try
            {
                using (ItemService itemService = new ItemService())
                {
                    bool success = itemService.AddItem(itemName, size);
                    if (success)
                    {
                        ShowStatusMessage("Item added successfully.", false);
                        ClearForm();
                        BindItemGrid(); // Refresh grid
                    }
                    else
                    {
                        ShowStatusMessage("Failed to add item.", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowStatusMessage($"Error adding item: {ex.Message}", true);
            }
        }

        // Update Button Click
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid || string.IsNullOrEmpty(txtItemId.Text)) return; // Ensure item is selected

            if (!int.TryParse(txtItemId.Text, out int itemId))
            {
                ShowStatusMessage("Invalid Item ID for update.", true);
                return;
            }

            string itemName = txtItemName.Text.Trim();
            string size = txtSize.Text.Trim();

            try
            {
                using (ItemService itemService = new ItemService())
                {
                    bool success = itemService.UpdateItem(itemId, itemName, size);
                    if (success)
                    {
                        ShowStatusMessage("Item updated successfully.", false);
                        ClearForm();
                        BindItemGrid(); // Refresh grid
                    }
                    else
                    {
                        ShowStatusMessage("Failed to update item (it might have been deleted).", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowStatusMessage($"Error updating item: {ex.Message}", true);
            }
        }

        // Delete Button Click
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItemId.Text)) // Ensure item is selected
            {
                ShowStatusMessage("Please select an item to delete.", true);
                return;
            }

            if (!int.TryParse(txtItemId.Text, out int itemId))
            {
                ShowStatusMessage("Invalid Item ID for deletion.", true);
                return;
            }

            // Client-side confirm('Are you sure?') is handled by OnClientClick property on button

            try
            {
                using (ItemService itemService = new ItemService())
                {
                    bool success = itemService.DeleteItem(itemId);
                    if (success)
                    {
                        ShowStatusMessage("Item deleted successfully.", false);
                        ClearForm();
                        BindItemGrid(); // Refresh grid
                    }
                    else
                    {
                        ShowStatusMessage("Failed to delete item (it might be in use or already deleted).", true);
                    }
                }
            }
            catch (Exception ex) // Catch FK constraint errors etc.
            {
                ShowStatusMessage($"Error deleting item: {ex.Message}", true);
            }
        }

        // Clear Button Click
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            lblStatus.Visible = false; // Also hide status on clear
        }
    }
}