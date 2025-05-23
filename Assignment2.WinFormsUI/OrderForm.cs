using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assignment2.BLL.Services; 
using Assignment2.DAL;

namespace Assignment2.WinFormsUI
{
    public partial class OrderForm: Form
    {
        private readonly AgentService _agentService;
        private readonly ItemService _itemService;
        private readonly OrderService _orderService;
        private BindingList<OrderDetail> _currentOrderDetails;
        public OrderForm()
        {
            InitializeComponent();
            _agentService = new AgentService();
            _itemService = new ItemService();
            _orderService = new OrderService();
            _currentOrderDetails = new BindingList<OrderDetail>();
        }
        private void OrderForm_Load(object sender, EventArgs e)
        {
            LoadAgentComboBox();
            LoadItemComboBox();
            SetupDetailsDataGridView();
            ClearNewOrder(); // Start with a clean slate
        }

        // Populate Agent ComboBox
        private void LoadAgentComboBox()
        {
            try
            {
                List<Agent> agents = _agentService.GetAllAgents();
                cmbAgents.DataSource = agents;
                cmbAgents.DisplayMember = "AgentName"; // Show AgentName
                cmbAgents.ValueMember = "AgentID";    // Use AgentID as the value
                cmbAgents.SelectedIndex = -1; // No initial selection
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading agents: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Populate Item ComboBox
        private void LoadItemComboBox()
        {
            try
            {
                List<Item> items = _itemService.GetAllItems();
                cmbItems.DataSource = items;
                cmbItems.DisplayMember = "ItemName"; // Show ItemName
                cmbItems.ValueMember = "ItemID";    // Use ItemID as the value
                cmbItems.SelectedIndex = -1; // No initial selection
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Setup DataGridView for Order Details
        private void SetupDetailsDataGridView()
        {
            dgvOrderDetails.DataSource = _currentOrderDetails; // Bind to the list
            dgvOrderDetails.AutoGenerateColumns = false; // We'll define columns manually

            // Define columns (add more as needed)
            dgvOrderDetails.Columns.Clear(); // Clear existing columns if any
            dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemID", DataPropertyName = "ItemID", HeaderText = "Item ID" });
            dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", DataPropertyName = "Quantity", HeaderText = "Quantity" });
            dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "UnitAmount", DataPropertyName = "UnitAmount", HeaderText = "Unit Amount"}); // Currency format

            // Optional: Add a column to show Item Name (requires modification to how details are added/stored)
            // dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "ItemName", HeaderText = "Item Name" });

            // Make grid read-only, etc.
            dgvOrderDetails.ReadOnly = true;
            dgvOrderDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrderDetails.AllowUserToAddRows = false;
        }

        // Method to clear the form for a new order
        private void ClearNewOrder()
        {
            dtpOrderDate.Value = DateTime.Now; // Default to today
            cmbAgents.SelectedIndex = -1;      // Clear agent selection
            cmbItems.SelectedIndex = -1;       // Clear item selection
            numQuantity.Value = 1;             // Reset quantity
            txtUnitAmount.Clear();
            _currentOrderDetails.Clear();      // Clear the details list (this updates the grid)
            dgvOrderDetails.Refresh();         // Explicitly refresh grid
        }


        // Dispose services when form closes
        private void OrderForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _agentService?.Dispose();
            _itemService?.Dispose();
            _orderService?.Dispose();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lblAgent_Click(object sender, EventArgs e)
        {

        }

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            if (cmbItems.SelectedValue == null)
            {
                MessageBox.Show("Please select an item.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(cmbItems.SelectedValue.ToString(), out int itemId))
            {
                MessageBox.Show("Invalid item selected.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int quantity = (int)numQuantity.Value; // NumericUpDown ensures valid integer >= 1

            if (!decimal.TryParse(txtUnitAmount.Text, out decimal unitAmount) || unitAmount < 0)
            {
                MessageBox.Show("Please enter a valid, non-negative unit amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUnitAmount.Focus();
                txtUnitAmount.SelectAll();
                return;
            }
            // --- End Validation ---


            // Create a new OrderDetail object (NOT saved to DB yet)
            OrderDetail newDetail = new OrderDetail
            {
                ItemID = itemId,
                Quantity = quantity,
                UnitAmount = unitAmount
                // OrderID will be set when the main Order is saved
                // Optional: You could fetch and store ItemName here if needed for display in grid
            };

            // Add the detail to the temporary list (which updates the grid via BindingList)
            _currentOrderDetails.Add(newDetail);

            // Optional: Clear detail input fields after adding
            cmbItems.SelectedIndex = -1;
            numQuantity.Value = 1;
            txtUnitAmount.Clear();
            cmbItems.Focus();
        }

        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            if (cmbAgents.SelectedValue == null)
            {
                MessageBox.Show("Please select an agent.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(cmbAgents.SelectedValue.ToString(), out int agentId))
            {
                MessageBox.Show("Invalid agent selected.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DateTime orderDate = dtpOrderDate.Value;

            // Check if there are any details added
            if (_currentOrderDetails.Count == 0)
            {
                MessageBox.Show("Please add at least one detail line to the order.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // --- End Validation ---

            // Convert BindingList to List for the service method
            List<OrderDetail> detailsToSave = new List<OrderDetail>(_currentOrderDetails);

            try
            {
                // Call the service to add the order and its details
                bool success = _orderService.AddOrder(orderDate, agentId, detailsToSave);

                if (success)
                {
                    MessageBox.Show("Order saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearNewOrder(); // Clear form for the next order
                }
                else
                {
                    MessageBox.Show("Failed to save order. Please check input or contact support.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Don't clear the form on failure so user can retry/correct
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the order: {ex.Message}", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearOrder_Click(object sender, EventArgs e)
        {
            ClearNewOrder();
        }
    }
}
