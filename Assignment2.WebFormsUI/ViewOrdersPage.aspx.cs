using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Assignment2.BLL.Services;

namespace Assignment2.WebFormsUI
{
	public partial class ViewOrdersPage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (Session["LoggedInUser"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            // --- End Check ---

            lblViewOrdersStatus.Visible = false;
            if (!IsPostBack)
            {
                BindOrdersGrid();
            }
        }

        private void BindOrdersGrid()
        {
            try
            {
                using (OrderService orderService = new OrderService())
                {
                    // Use the BLL method that gets basic info
                    var ordersInfo = orderService.GetAllOrdersBasicInfo();
                    gvOrdersList.DataSource = ordersInfo;
                    gvOrdersList.DataBind();
                }
            }
            catch (Exception ex)
            {
                ShowStatus($"Error loading orders: {ex.Message}", true);
            }
        }

        protected void gvOrdersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Get the selected OrderID
                int selectedOrderId = Convert.ToInt32(gvOrdersList.SelectedDataKey.Value);

                // Redirect to the detail report page, passing the OrderID in QueryString
                Response.Redirect($"OrderDetailReportPage.aspx?OrderID={selectedOrderId}");
            }
            catch (Exception ex)
            {
                ShowStatus($"Error selecting order: {ex.Message}", true);
            }
        }

        private void ShowStatus(string message, bool isError)
        {
            lblViewOrdersStatus.Text = message;
            lblViewOrdersStatus.ForeColor = isError ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            lblViewOrdersStatus.Visible = true;
        }
    }
}