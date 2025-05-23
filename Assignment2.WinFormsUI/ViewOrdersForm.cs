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

namespace Assignment2.WinFormsUI
{
    public partial class ViewOrdersForm: Form
    {
        private readonly OrderService _orderService;
        public ViewOrdersForm()
        {
            InitializeComponent();
            _orderService = new OrderService();
        }
        private void ViewOrdersForm_Load(object sender, EventArgs e)
        {
            LoadOrdersList();
        }

        private void LoadOrdersList()
        {
            try
            {
                var ordersInfo = _orderService.GetAllOrdersBasicInfo();
                dgvOrdersList.DataSource = null;
                dgvOrdersList.DataSource = ordersInfo;

                // Customize columns if needed (names match anonymous type properties)
                if (dgvOrdersList.Columns["OrderID"] != null) dgvOrdersList.Columns["OrderID"].HeaderText = "Order ID";
                if (dgvOrdersList.Columns["OrderDate"] != null) dgvOrdersList.Columns["OrderDate"].HeaderText = "Order Date";
                if (dgvOrdersList.Columns["AgentName"] != null) dgvOrdersList.Columns["AgentName"].HeaderText = "Agent Name";

                dgvOrdersList.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
        private void btnViewDetails_Click_1(object sender, EventArgs e)
        {
            if (dgvOrdersList.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order from the list to view its details.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Get the OrderID from the selected row (assuming OrderID is the first column or named "OrderID")
                int selectedOrderId = Convert.ToInt32(dgvOrdersList.SelectedRows[0].Cells["OrderID"].Value);

                // Create and show the detail report form, passing the OrderID
                OrderDetailReportForm reportForm = new OrderDetailReportForm(selectedOrderId);
                reportForm.ShowDialog(); // Show as a dialog to focus user attention

                // Optional: Refresh list after closing dialog in case something changed (e.g., deletion)
                // LoadOrdersList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving order details or opening report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefreshOrders_Click_1(object sender, EventArgs e)
        {
            LoadOrdersList();
        }
        private void ViewOrdersForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _orderService?.Dispose();
        }
    }
}
