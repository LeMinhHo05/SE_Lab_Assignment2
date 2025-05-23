using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Assignment2.BLL.Services;
using Assignment2.DAL;

namespace Assignment2.WebFormsUI
{
	public partial class OrderDetailReportPage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (Session["LoggedInUser"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            // --- End Check ---

            if (!IsPostBack)
            {
                // Get OrderID from QueryString
                if (Request.QueryString["OrderID"] == null || !int.TryParse(Request.QueryString["OrderID"], out int orderId))
                {
                    ShowError("Invalid or missing Order ID.");
                    return;
                }

                LoadReport(orderId);
            }
        }

        private void LoadReport(int orderId)
        {
            try
            {
                using (OrderService orderService = new OrderService())
                {
                    Order order = orderService.GetOrderById(orderId); // This fetches details too

                    if (order == null)
                    {
                        ShowError($"Order with ID {orderId} not found.");
                        return;
                    }

                    // Populate Header
                    lblOrderIdValue.Text = order.OrderID.ToString();
                    lblOrderDateValue.Text = order.OrderDate.ToString("g");
                    lblAgentNameValue.Text = order.Agent?.AgentName ?? "N/A";

                    // Populate Details Grid
                    var detailsForReport = order.OrderDetails.Select(od => new {
                        ItemName = od.Item?.ItemName ?? "N/A",
                        od.Quantity,
                        od.UnitAmount
                        // LineTotal will be calculated in the GridView TemplateField
                    }).ToList();

                    gvReportDetails.DataSource = detailsForReport;
                    gvReportDetails.DataBind();

                    reportContent.Visible = true; // Show content if loaded successfully
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error loading order report: {ex.Message}");
            }
        }

        private void ShowError(string message)
        {
            lblReportError.Text = message;
            lblReportError.Visible = true;
            reportContent.Visible = false; // Hide the main content on error
        }
    }
}
