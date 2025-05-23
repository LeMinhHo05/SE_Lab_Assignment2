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
    public partial class FilterForm: Form
    {
        private readonly ItemService _itemService;
        private readonly AgentService _agentService;
        private readonly OrderService _orderService;
        public FilterForm()
        {
            InitializeComponent();
            _itemService = new ItemService();
            _agentService = new AgentService();
            _orderService = new OrderService();
        }
        private void FilterForm_Load(object sender, EventArgs e)
        {
            LoadAgentFilterComboBox();
            // Set initial state based on default radio button
            ToggleAgentSelector(rbAgentHistory.Checked);
        }
        private void LoadAgentFilterComboBox()
        {
            try
            {
                List<Agent> agents = _agentService.GetAllAgents();
                cmbFilterAgents.DataSource = agents;
                cmbFilterAgents.DisplayMember = "AgentName";
                cmbFilterAgents.ValueMember = "AgentID";
                cmbFilterAgents.SelectedIndex = -1; // No initial selection
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading agents for filter: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Disable agent-related filtering if loading fails
                rbAgentHistory.Enabled = false;
                lblSelectAgent.Visible = false;
                cmbFilterAgents.Visible = false;
            }
        }

        // Show/Hide Agent selector based on selected filter type
        private void ToggleAgentSelector(bool showAgentSelector)
        {
            lblSelectAgent.Visible = showAgentSelector;
            cmbFilterAgents.Visible = showAgentSelector;
            if (!showAgentSelector)
            {
                cmbFilterAgents.SelectedIndex = -1; // Clear selection when hidden
            }
            dgvFilterResults.DataSource = null; // Clear results when filter type changes
        }

        // Event handler for when RadioButton selection changes
        private void rbFilterType_CheckedChanged(object sender, EventArgs e)
        {
            // Only act if the radio button that triggered the event is now checked
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                ToggleAgentSelector(rb == rbAgentHistory); // Show agent selector only if Agent History is checked
            }
        }


        // Apply Filter Button Click Handler
        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            dgvFilterResults.DataSource = null; // Clear previous results

            try
            {
                if (rbBestSellers.Checked)
                {
                    // Filter 1: Best Selling Items
                    var results = _itemService.GetBestSellingItems(); // Uses default top 10
                    dgvFilterResults.DataSource = results;
                    // Optional: Customize column headers for anonymous type
                    if (dgvFilterResults.Columns["ItemID"] != null) dgvFilterResults.Columns["ItemID"].HeaderText = "Item ID";
                    if (dgvFilterResults.Columns["ItemName"] != null) dgvFilterResults.Columns["ItemName"].HeaderText = "Item Name";
                    if (dgvFilterResults.Columns["TotalQuantity"] != null) dgvFilterResults.Columns["TotalQuantity"].HeaderText = "Total Quantity Sold";

                }
                else if (rbAgentHistory.Checked)
                {
                    // Filter 2: Agent Order History
                    if (cmbFilterAgents.SelectedValue == null)
                    {
                        MessageBox.Show("Please select an agent.", "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (!int.TryParse(cmbFilterAgents.SelectedValue.ToString(), out int agentId))
                    {
                        MessageBox.Show("Invalid agent selected.", "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var results = _orderService.GetOrderDetailsForAgent(agentId);
                    dgvFilterResults.DataSource = results;
                    // Optional: Customize column headers for anonymous type
                    if (dgvFilterResults.Columns["OrderDate"] != null) dgvFilterResults.Columns["OrderDate"].HeaderText = "Order Date";
                    if (dgvFilterResults.Columns["ItemName"] != null) dgvFilterResults.Columns["ItemName"].HeaderText = "Item Name";
                    if (dgvFilterResults.Columns["Quantity"] != null) dgvFilterResults.Columns["Quantity"].HeaderText = "Quantity";
                    if (dgvFilterResults.Columns["UnitAmount"] != null) { dgvFilterResults.Columns["UnitAmount"].HeaderText = "Unit Amount"; dgvFilterResults.Columns["UnitAmount"].DefaultCellStyle.Format = "C2"; }
                    if (dgvFilterResults.Columns["LineTotal"] != null) { dgvFilterResults.Columns["LineTotal"].HeaderText = "Line Total"; dgvFilterResults.Columns["LineTotal"].DefaultCellStyle.Format = "C2"; }
                }
                // Add more filter types here if needed
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying filter: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ensure grid columns resize appropriately after setting datasource
                dgvFilterResults.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
        }


        // Dispose services on close
        private void FilterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _itemService?.Dispose();
            _agentService?.Dispose();
            _orderService?.Dispose();
        }

        private void rbBestSellers_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbAgentHistory_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
