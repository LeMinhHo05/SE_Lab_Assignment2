using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assignment2.BLL.Services; // For ItemService
using Assignment2.DAL;          // For Item entity

namespace Assignment2.WinFormsUI
{
    public partial class ItemForm: Form
    {
        private ItemService _itemService;
        public ItemForm()
        {
            InitializeComponent();
            _itemService = new ItemService();
        }
        private void LoadItemsGrid()
        {
            try
            {
                List<Item> items = _itemService.GetAllItems();
                // Set DataSource. Null it first to ensure refresh.
                dgvItems.DataSource = null;
                dgvItems.DataSource = items;

                // Optional: Customize column headers or hide columns
                dgvItems.Columns["ItemID"].HeaderText = "Item ID";
                dgvItems.Columns["ItemName"].HeaderText = "Item Name";
                // Hide columns if they exist and you don't want to show them
                // if (dgvItems.Columns["OrderDetails"] != null) dgvItems.Columns["OrderDetails"].Visible = false;

                // Clear selection after loading
                dgvItems.ClearSelection();
                ClearFormFields(); // Clear text boxes as well
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to clear input fields
        private void ClearFormFields()
        {
            txtItemId.Clear();
            txtItemName.Clear();
            txtSize.Clear();
            dgvItems.ClearSelection(); // Also clear grid selection
            txtItemName.Focus(); // Set focus to the first input field
        }

        // Form Load event handler
        private void ItemForm_Load(object sender, EventArgs e)
        {
            LoadItemsGrid(); // Load data when the form opens
        }

        // Handle selecting a row in the DataGridView
        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dgvItems.SelectedRows[0];

                // Extract data (handle potential nulls)
                string itemId = selectedRow.Cells["ItemID"].Value?.ToString() ?? string.Empty;
                string itemName = selectedRow.Cells["ItemName"].Value?.ToString() ?? string.Empty;
                string size = selectedRow.Cells["Size"].Value?.ToString() ?? string.Empty;

                // Populate the text boxes
                txtItemId.Text = itemId;
                txtItemName.Text = itemName;
                txtSize.Text = size;
            }
            else
            {
                // Optional: Clear fields if selection is cleared, handled by LoadItemsGrid/ClearFormFields usually
                // ClearFormFields();
            }
        }

        // Dispose the service when the form closes
        private void ItemForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _itemService?.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string itemName = txtItemName.Text.Trim();
            string size = txtSize.Text.Trim(); // Trim whitespace

            if (string.IsNullOrWhiteSpace(itemName))
            {
                MessageBox.Show("Item Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = _itemService.AddItem(itemName, size);
                if (success)
                {
                    MessageBox.Show("Item added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadItemsGrid(); // Refresh grid
                }
                else
                {
                    MessageBox.Show("Failed to add item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 0 || string.IsNullOrWhiteSpace(txtItemId.Text))
            {
                MessageBox.Show("Please select an item from the list to update.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtItemId.Text, out int itemId))
            {
                MessageBox.Show("Invalid Item ID selected.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string itemName = txtItemName.Text.Trim();
            string size = txtSize.Text.Trim();

            if (string.IsNullOrWhiteSpace(itemName))
            {
                MessageBox.Show("Item Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = _itemService.UpdateItem(itemId, itemName, size);
                if (success)
                {
                    MessageBox.Show("Item updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadItemsGrid(); // Refresh grid
                }
                else
                {
                    MessageBox.Show("Failed to update item. It might have been deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 0 || string.IsNullOrWhiteSpace(txtItemId.Text))
            {
                MessageBox.Show("Please select an item from the list to delete.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtItemId.Text, out int itemId))
            {
                MessageBox.Show("Invalid Item ID selected.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirmation dialog
            DialogResult confirmation = MessageBox.Show($"Are you sure you want to delete Item ID: {itemId}?",
                                                      "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmation == DialogResult.Yes)
            {
                try
                {
                    bool success = _itemService.DeleteItem(itemId);
                    if (success)
                    {
                        MessageBox.Show("Item deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadItemsGrid(); // Refresh grid
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete item. It might be in use in an order or already deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Catch potential FK constraint errors if not handled in BLL/DAL
                    MessageBox.Show($"Error deleting item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFormFields();
        }
    }
}
