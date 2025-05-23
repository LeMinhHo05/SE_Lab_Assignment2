using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assignment2.BLL.Services; 
using Assignment2.DAL;

namespace Assignment2.WinFormsUI
{
    public partial class AgentForm: Form
    {
        private AgentService _agentService;
        public AgentForm()
        {
            InitializeComponent();
            _agentService = new AgentService();
        }
        private void LoadAgentsGrid()
        {
            try
            {
                List<Agent> agents = _agentService.GetAllAgents();
                dgvAgents.DataSource = null; // Ensure refresh
                dgvAgents.DataSource = agents;

                // Optional: Customize Columns
                dgvAgents.Columns["AgentID"].HeaderText = "Agent ID";
                dgvAgents.Columns["AgentName"].HeaderText = "Agent Name";
                // Hide related data columns if they exist (e.g., Orders)
                if (dgvAgents.Columns["Orders"] != null) dgvAgents.Columns["Orders"].Visible = false;

                dgvAgents.ClearSelection();
                ClearFormFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading agents: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to clear input fields
        private void ClearFormFields()
        {
            txtAgentId.Clear();
            txtAgentName.Clear();
            txtAddress.Clear();
            dgvAgents.ClearSelection();
            txtAgentName.Focus();
        }

        // Form Load
        private void AgentForm_Load(object sender, EventArgs e)
        {
            LoadAgentsGrid();
        }

        // Grid Selection Changed
        private void dgvAgents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAgents.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvAgents.SelectedRows[0];
                txtAgentId.Text = selectedRow.Cells["AgentID"].Value?.ToString() ?? string.Empty;
                txtAgentName.Text = selectedRow.Cells["AgentName"].Value?.ToString() ?? string.Empty;
                txtAddress.Text = selectedRow.Cells["Address"].Value?.ToString() ?? string.Empty;
            }
        }

        // Dispose service on close
        private void AgentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _agentService?.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string agentName = txtAgentName.Text.Trim();
            string address = txtAddress.Text.Trim();

            if (string.IsNullOrWhiteSpace(agentName))
            {
                MessageBox.Show("Agent Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = _agentService.AddAgent(agentName, address);
                if (success)
                {
                    MessageBox.Show("Agent added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAgentsGrid();
                }
                else
                {
                    MessageBox.Show("Failed to add agent.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding agent: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvAgents.SelectedRows.Count == 0 || string.IsNullOrWhiteSpace(txtAgentId.Text))
            {
                MessageBox.Show("Please select an agent to update.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtAgentId.Text, out int agentId))
            {
                MessageBox.Show("Invalid Agent ID.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string agentName = txtAgentName.Text.Trim();
            string address = txtAddress.Text.Trim();

            if (string.IsNullOrWhiteSpace(agentName))
            {
                MessageBox.Show("Agent Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = _agentService.UpdateAgent(agentId, agentName, address);
                if (success)
                {
                    MessageBox.Show("Agent updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAgentsGrid();
                }
                else
                {
                    MessageBox.Show("Failed to update agent.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating agent: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAgents.SelectedRows.Count == 0 || string.IsNullOrWhiteSpace(txtAgentId.Text))
            {
                MessageBox.Show("Please select an agent to delete.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtAgentId.Text, out int agentId))
            {
                MessageBox.Show("Invalid Agent ID.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirmation = MessageBox.Show($"Are you sure you want to delete Agent ID: {agentId}? This may fail if the agent has existing orders.",
                                                      "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmation == DialogResult.Yes)
            {
                try
                {
                    bool success = _agentService.DeleteAgent(agentId);
                    if (success)
                    {
                        MessageBox.Show("Agent deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAgentsGrid();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete agent. It might have associated orders or already be deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting agent: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFormFields();
        }
    }
}
