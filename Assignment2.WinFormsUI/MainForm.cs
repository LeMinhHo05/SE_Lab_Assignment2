using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2.WinFormsUI
{
    public partial class MainForm: Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void filterItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is FilterForm)
                {
                    openForm.BringToFront();
                    return;
                }
            }
            // Create and show new instance
            FilterForm filterForm = new FilterForm();
            // filterForm.MdiParent = this; // If using MDI
            filterForm.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is ItemForm)
                {
                    openForm.BringToFront(); // Bring existing form to front
                    return; // Exit if already open
                }
            }

            // If not open, create and show a new instance
            ItemForm itemForm = new ItemForm();
            // itemForm.MdiParent = this; // Optional: If MainForm is an MDI container
            itemForm.Show();
        }

        private void agentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is AgentForm)
                {
                    openForm.BringToFront();
                    return;
                }
            }

            // Create and show new instance
            AgentForm agentForm = new AgentForm();
            // agentForm.MdiParent = this; // If using MDI
            agentForm.Show();
        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is OrderForm)
                {
                    openForm.BringToFront();
                    return;
                }
            }
            // Create and show new instance
            OrderForm orderForm = new OrderForm();
            // orderForm.MdiParent = this; // If using MDI
            orderForm.Show();
        }

        private void viewExitingOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is ViewOrdersForm)
                {
                    openForm.BringToFront();
                    return;
                }
            }
            // Create and show new instance
            ViewOrdersForm viewOrders = new ViewOrdersForm();
            // viewOrders.MdiParent = this; // If using MDI
            viewOrders.Show();
        }
    }
}
