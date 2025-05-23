using Assignment2.BLL.Services;
using Assignment2.DAL; // Maybe needed for Agent in dropdown
using System;
using System.Collections.Generic; // For List<Agent>
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment2.WebFormsUI
{
    public partial class FilterPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // --- Check Login Status ---
            if (Session["LoggedInUser"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            // --- End Check ---

            lblFilterStatus.Visible = false;
            if (!IsPostBack)
            {
                PopulateAgentFilterDropDown();
                // Initial panel visibility based on default radio button selection
                pnlAgentFilter.Visible = (rblFilterType.SelectedValue == "AgentHistory");
                rfvFilterAgent.Enabled = pnlAgentFilter.Visible; // Enable/disable validator
            }
        }

        // Populate Agent dropdown
        private void PopulateAgentFilterDropDown()
        {
            try
            {
                using (AgentService agentService = new AgentService())
                {
                    ddlFilterAgents.DataSource = agentService.GetAllAgents();
                    ddlFilterAgents.DataBind();
                    if (ddlFilterAgents.Items.FindByValue("") == null)
                        ddlFilterAgents.Items.Insert(0, new ListItem("-- Select Agent --", ""));
                }
            }
            catch (Exception ex)
            {
                ShowStatus($"Error loading agents: {ex.Message}", true);
                // Disable agent filter if loading fails
                ListItem agentItem = rblFilterType.Items.FindByValue("AgentHistory");
                if (agentItem != null) agentItem.Enabled = false;
                pnlAgentFilter.Visible = false;
                rfvFilterAgent.Enabled = false;
            }
        }

        // Handle RadioButtonList selection change
        protected void rblFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlAgentFilter.Visible = (rblFilterType.SelectedValue == "AgentHistory");
            rfvFilterAgent.Enabled = pnlAgentFilter.Visible; // Toggle validator
            gvFilterResults.DataSource = null; // Clear results grid
            gvFilterResults.DataBind();
        }

        // Apply Filter Button Click
        protected void btnApplyFilter_Click(object sender, EventArgs e)
        {
            gvFilterResults.DataSource = null; // Clear previous results

            try
            {
                string selectedFilter = rblFilterType.SelectedValue;

                if (selectedFilter == "BestSellers")
                {
                    using (ItemService itemService = new ItemService())
                    {
                        var results = itemService.GetBestSellingItems();
                        gvFilterResults.DataSource = results;
                    }
                }
                else if (selectedFilter == "AgentHistory")
                {
                    // Validate agent selection
                    if (string.IsNullOrEmpty(ddlFilterAgents.SelectedValue))
                    {
                        ShowStatus("Please select an agent for this filter.", true);
                        // Ensure validator is enabled if panel is visible
                        rfvFilterAgent.Enabled = pnlAgentFilter.Visible;
                        Page.Validate(); // Trigger validation display
                        return;
                    }
                    int agentId = Convert.ToInt32(ddlFilterAgents.SelectedValue);

                    using (OrderService orderService = new OrderService())
                    {
                        var results = orderService.GetOrderDetailsForAgent(agentId);
                        gvFilterResults.DataSource = results;
                        // Customize column formats for currency etc. if needed (can be done in GridView RowDataBound event)
                    }
                }
                // Add more filters here if needed

                gvFilterResults.DataBind(); // Bind the data

                if (gvFilterResults.Rows.Count == 0)
                {
                    ShowStatus("No results found for the selected criteria.", false); // Info, not error
                }

            }
            catch (Exception ex)
            {
                ShowStatus($"Error applying filter: {ex.Message}", true);
            }
        }

        private void ShowStatus(string message, bool isError)
        {
            lblFilterStatus.Text = message;
            lblFilterStatus.ForeColor = isError ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            lblFilterStatus.Visible = true;
        }
    }
}