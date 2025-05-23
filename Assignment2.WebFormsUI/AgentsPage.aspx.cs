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
	public partial class AgentsPage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (Session["LoggedInUser"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            // --- End Check ---

            lblAgentStatus.Visible = false;
            if (!IsPostBack)
            {
                BindAgentGrid();
                ClearAgentForm();
            }
        }

        private void BindAgentGrid()
        {
            try
            {
                using (AgentService agentService = new AgentService())
                {
                    List<Agent> agents = agentService.GetAllAgents();
                    gvAgents.DataSource = agents;
                    gvAgents.DataBind();
                }
            }
            catch (Exception ex)
            {
                ShowAgentStatusMessage($"Error loading agents: {ex.Message}", true);
            }
        }

        private void ClearAgentForm()
        {
            txtAgentId.Text = string.Empty;
            txtAgentName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            gvAgents.SelectedIndex = -1;
            btnAddAgent.Enabled = true;
            btnUpdateAgent.Enabled = false;
            btnDeleteAgent.Enabled = false;
            txtAgentName.Focus(); // Focus on name field
        }

        private void ShowAgentStatusMessage(string message, bool isError)
        {
            lblAgentStatus.Text = message;
            lblAgentStatus.ForeColor = isError ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            lblAgentStatus.Visible = true;
        }

        protected void gvAgents_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int agentId = Convert.ToInt32(gvAgents.SelectedDataKey.Value);
                using (AgentService agentService = new AgentService())
                {
                    Agent selectedAgent = agentService.GetAgentById(agentId);
                    if (selectedAgent != null)
                    {
                        txtAgentId.Text = selectedAgent.AgentID.ToString();
                        txtAgentName.Text = selectedAgent.AgentName;
                        txtAddress.Text = selectedAgent.Address;

                        btnAddAgent.Enabled = false;
                        btnUpdateAgent.Enabled = true;
                        btnDeleteAgent.Enabled = true;
                    }
                    else
                    {
                        ShowAgentStatusMessage("Selected agent not found.", true);
                        ClearAgentForm();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowAgentStatusMessage($"Error loading selected agent: {ex.Message}", true);
                ClearAgentForm();
            }
        }

        protected void btnAddAgent_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string agentName = txtAgentName.Text.Trim();
            string address = txtAddress.Text.Trim();

            try
            {
                using (AgentService agentService = new AgentService())
                {
                    bool success = agentService.AddAgent(agentName, address);
                    if (success)
                    {
                        ShowAgentStatusMessage("Agent added successfully.", false);
                        ClearAgentForm();
                        BindAgentGrid();
                    }
                    else
                    {
                        ShowAgentStatusMessage("Failed to add agent.", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowAgentStatusMessage($"Error adding agent: {ex.Message}", true);
            }
        }

        protected void btnUpdateAgent_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid || string.IsNullOrEmpty(txtAgentId.Text)) return;

            if (!int.TryParse(txtAgentId.Text, out int agentId))
            {
                ShowAgentStatusMessage("Invalid Agent ID for update.", true);
                return;
            }

            string agentName = txtAgentName.Text.Trim();
            string address = txtAddress.Text.Trim();

            try
            {
                using (AgentService agentService = new AgentService())
                {
                    bool success = agentService.UpdateAgent(agentId, agentName, address);
                    if (success)
                    {
                        ShowAgentStatusMessage("Agent updated successfully.", false);
                        ClearAgentForm();
                        BindAgentGrid();
                    }
                    else
                    {
                        ShowAgentStatusMessage("Failed to update agent.", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowAgentStatusMessage($"Error updating agent: {ex.Message}", true);
            }
        }

        protected void btnDeleteAgent_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAgentId.Text))
            {
                ShowAgentStatusMessage("Please select an agent to delete.", true);
                return;
            }

            if (!int.TryParse(txtAgentId.Text, out int agentId))
            {
                ShowAgentStatusMessage("Invalid Agent ID for deletion.", true);
                return;
            }

            // Client-side confirm handled by OnClientClick

            try
            {
                using (AgentService agentService = new AgentService())
                {
                    bool success = agentService.DeleteAgent(agentId);
                    if (success)
                    {
                        ShowAgentStatusMessage("Agent deleted successfully.", false);
                        ClearAgentForm();
                        BindAgentGrid();
                    }
                    else
                    {
                        ShowAgentStatusMessage("Failed to delete agent (agent might have orders or already be deleted).", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowAgentStatusMessage($"Error deleting agent: {ex.Message}", true);
            }
        }

        protected void btnClearAgent_Click(object sender, EventArgs e)
        {
            ClearAgentForm();
            lblAgentStatus.Visible = false;
        }
    }
}
