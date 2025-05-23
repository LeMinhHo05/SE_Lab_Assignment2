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
    public partial class OrderDetailReportForm: Form
    {
        private readonly OrderService _orderService;
        private readonly int _orderIdToLoad;

        public OrderDetailReportForm(int orderId)
        {
            InitializeComponent();
            _orderService = new OrderService();
            _orderIdToLoad = orderId; // Store the ID passed in
        }
        private void OrderDetailReportForm_Load(object sender, EventArgs e)
        {
            LoadOrderReportData();
        }

        private void LoadOrderReportData()
        {
            if (_orderIdToLoad <= 0)
            {
                MessageBox.Show("Invalid Order ID provided.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close(); // Close the form if ID is invalid
                return;
            }

            try
            {
                Order order = _orderService.GetOrderById(_orderIdToLoad);

                if (order == null)
                {
                    MessageBox.Show($"Order with ID {_orderIdToLoad} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Populate Header Labels
                lblOrderIdValue.Text = order.OrderID.ToString();
                lblOrderDateValue.Text = order.OrderDate.ToString("g"); // General date/time format
                lblAgentNameValue.Text = order.Agent?.AgentName ?? "N/A"; // Use null-conditional operator

                // Populate Details DataGridView
                // Select specific columns needed for the report view
                var detailsForReport = order.OrderDetails.Select(od => new {
                    ItemName = od.Item?.ItemName ?? "N/A", // Show Item Name
                    od.Quantity,
                    od.UnitAmount,
                    LineTotal = od.Quantity * od.UnitAmount
                }).ToList();

                dgvReportDetails.DataSource = null;
                dgvReportDetails.DataSource = detailsForReport;

                // Customize Columns
                if (dgvReportDetails.Columns["ItemName"] != null) dgvReportDetails.Columns["ItemName"].HeaderText = "Item Name";
                if (dgvReportDetails.Columns["Quantity"] != null) dgvReportDetails.Columns["Quantity"].HeaderText = "Quantity";
                if (dgvReportDetails.Columns["UnitAmount"] != null) { dgvReportDetails.Columns["UnitAmount"].HeaderText = "Unit Amount"; dgvReportDetails.Columns["UnitAmount"].DefaultCellStyle.Format = "C2"; }
                if (dgvReportDetails.Columns["LineTotal"] != null) { dgvReportDetails.Columns["LineTotal"].HeaderText = "Line Total"; dgvReportDetails.Columns["LineTotal"].DefaultCellStyle.Format = "C2"; }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order report data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnCloseReport_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void OrderDetailReportForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _orderService?.Dispose();
        }
    }
}
