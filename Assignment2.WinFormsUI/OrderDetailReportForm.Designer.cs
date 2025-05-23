namespace Assignment2.WinFormsUI
{
    partial class OrderDetailReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.lblOrderIdLabel = new System.Windows.Forms.Label();
            this.lblOrderIdValue = new System.Windows.Forms.Label();
            this.lblOrderDateLabel = new System.Windows.Forms.Label();
            this.lblOrderDateValue = new System.Windows.Forms.Label();
            this.lblAgentNameLabel = new System.Windows.Forms.Label();
            this.lblAgentNameValue = new System.Windows.Forms.Label();
            this.dgvReportDetails = new System.Windows.Forms.DataGridView();
            this.btnCloseReport = new System.Windows.Forms.Button();
            this.grpHeader.SuspendLayout();
            this.grpDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.lblAgentNameValue);
            this.grpHeader.Controls.Add(this.lblAgentNameLabel);
            this.grpHeader.Controls.Add(this.lblOrderDateValue);
            this.grpHeader.Controls.Add(this.lblOrderDateLabel);
            this.grpHeader.Controls.Add(this.lblOrderIdValue);
            this.grpHeader.Controls.Add(this.lblOrderIdLabel);
            this.grpHeader.Location = new System.Drawing.Point(12, 12);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(350, 236);
            this.grpHeader.TabIndex = 0;
            this.grpHeader.TabStop = false;
            this.grpHeader.Text = "Order Information";
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.dgvReportDetails);
            this.grpDetails.Location = new System.Drawing.Point(392, 12);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(396, 236);
            this.grpDetails.TabIndex = 1;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Order Details";
            // 
            // lblOrderIdLabel
            // 
            this.lblOrderIdLabel.AutoSize = true;
            this.lblOrderIdLabel.Location = new System.Drawing.Point(7, 34);
            this.lblOrderIdLabel.Name = "lblOrderIdLabel";
            this.lblOrderIdLabel.Size = new System.Drawing.Size(50, 13);
            this.lblOrderIdLabel.TabIndex = 0;
            this.lblOrderIdLabel.Text = "Order ID:";
            // 
            // lblOrderIdValue
            // 
            this.lblOrderIdValue.AutoSize = true;
            this.lblOrderIdValue.Location = new System.Drawing.Point(60, 34);
            this.lblOrderIdValue.Name = "lblOrderIdValue";
            this.lblOrderIdValue.Size = new System.Drawing.Size(16, 13);
            this.lblOrderIdValue.TabIndex = 1;
            this.lblOrderIdValue.Text = "...";
            // 
            // lblOrderDateLabel
            // 
            this.lblOrderDateLabel.AutoSize = true;
            this.lblOrderDateLabel.Location = new System.Drawing.Point(7, 102);
            this.lblOrderDateLabel.Name = "lblOrderDateLabel";
            this.lblOrderDateLabel.Size = new System.Drawing.Size(62, 13);
            this.lblOrderDateLabel.TabIndex = 2;
            this.lblOrderDateLabel.Text = "Order Date:";
            // 
            // lblOrderDateValue
            // 
            this.lblOrderDateValue.AutoSize = true;
            this.lblOrderDateValue.Location = new System.Drawing.Point(72, 102);
            this.lblOrderDateValue.Name = "lblOrderDateValue";
            this.lblOrderDateValue.Size = new System.Drawing.Size(16, 13);
            this.lblOrderDateValue.TabIndex = 3;
            this.lblOrderDateValue.Text = "...";
            // 
            // lblAgentNameLabel
            // 
            this.lblAgentNameLabel.AutoSize = true;
            this.lblAgentNameLabel.Location = new System.Drawing.Point(7, 171);
            this.lblAgentNameLabel.Name = "lblAgentNameLabel";
            this.lblAgentNameLabel.Size = new System.Drawing.Size(69, 13);
            this.lblAgentNameLabel.TabIndex = 4;
            this.lblAgentNameLabel.Text = "Agent Name:";
            // 
            // lblAgentNameValue
            // 
            this.lblAgentNameValue.AutoSize = true;
            this.lblAgentNameValue.Location = new System.Drawing.Point(82, 171);
            this.lblAgentNameValue.Name = "lblAgentNameValue";
            this.lblAgentNameValue.Size = new System.Drawing.Size(16, 13);
            this.lblAgentNameValue.TabIndex = 5;
            this.lblAgentNameValue.Text = "...";
            // 
            // dgvReportDetails
            // 
            this.dgvReportDetails.AllowUserToAddRows = false;
            this.dgvReportDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReportDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReportDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportDetails.Location = new System.Drawing.Point(3, 16);
            this.dgvReportDetails.Name = "dgvReportDetails";
            this.dgvReportDetails.ReadOnly = true;
            this.dgvReportDetails.Size = new System.Drawing.Size(390, 217);
            this.dgvReportDetails.TabIndex = 0;
            this.dgvReportDetails.Click += new System.EventHandler(this.OrderDetailReportForm_Load);
            // 
            // btnCloseReport
            // 
            this.btnCloseReport.Location = new System.Drawing.Point(343, 316);
            this.btnCloseReport.Name = "btnCloseReport";
            this.btnCloseReport.Size = new System.Drawing.Size(75, 23);
            this.btnCloseReport.TabIndex = 1;
            this.btnCloseReport.Text = "Close";
            this.btnCloseReport.UseVisualStyleBackColor = true;
            this.btnCloseReport.Click += new System.EventHandler(this.btnCloseReport_Click);
            // 
            // OrderDetailReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCloseReport);
            this.Controls.Add(this.grpDetails);
            this.Controls.Add(this.grpHeader);
            this.Name = "OrderDetailReportForm";
            this.Text = "Order Detail Report";
            this.grpHeader.ResumeLayout(false);
            this.grpHeader.PerformLayout();
            this.grpDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpHeader;
        private System.Windows.Forms.Label lblAgentNameValue;
        private System.Windows.Forms.Label lblAgentNameLabel;
        private System.Windows.Forms.Label lblOrderDateValue;
        private System.Windows.Forms.Label lblOrderDateLabel;
        private System.Windows.Forms.Label lblOrderIdValue;
        private System.Windows.Forms.Label lblOrderIdLabel;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.DataGridView dgvReportDetails;
        private System.Windows.Forms.Button btnCloseReport;
    }
}