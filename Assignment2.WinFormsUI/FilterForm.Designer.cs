namespace Assignment2.WinFormsUI
{
    partial class FilterForm
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
            this.grpFilterType = new System.Windows.Forms.GroupBox();
            this.rbAgentHistory = new System.Windows.Forms.RadioButton();
            this.rbBestSellers = new System.Windows.Forms.RadioButton();
            this.grpFilterCriteria = new System.Windows.Forms.GroupBox();
            this.cmbFilterAgents = new System.Windows.Forms.ComboBox();
            this.lblSelectAgent = new System.Windows.Forms.Label();
            this.dgvFilterResults = new System.Windows.Forms.DataGridView();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            this.grpFilterType.SuspendLayout();
            this.grpFilterCriteria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilterResults)).BeginInit();
            this.SuspendLayout();
            // 
            // grpFilterType
            // 
            this.grpFilterType.Controls.Add(this.rbAgentHistory);
            this.grpFilterType.Controls.Add(this.rbBestSellers);
            this.grpFilterType.Location = new System.Drawing.Point(12, 28);
            this.grpFilterType.Name = "grpFilterType";
            this.grpFilterType.Size = new System.Drawing.Size(279, 165);
            this.grpFilterType.TabIndex = 0;
            this.grpFilterType.TabStop = false;
            this.grpFilterType.Text = "Select Filter Type";
            // 
            // rbAgentHistory
            // 
            this.rbAgentHistory.AutoSize = true;
            this.rbAgentHistory.Location = new System.Drawing.Point(6, 103);
            this.rbAgentHistory.Name = "rbAgentHistory";
            this.rbAgentHistory.Size = new System.Drawing.Size(117, 17);
            this.rbAgentHistory.TabIndex = 1;
            this.rbAgentHistory.TabStop = true;
            this.rbAgentHistory.Text = "Agent Order History";
            this.rbAgentHistory.UseVisualStyleBackColor = true;
            this.rbAgentHistory.CheckedChanged += new System.EventHandler(this.rbAgentHistory_CheckedChanged);
            this.rbAgentHistory.Click += new System.EventHandler(this.rbAgentHistory_CheckedChanged);
            // 
            // rbBestSellers
            // 
            this.rbBestSellers.AutoSize = true;
            this.rbBestSellers.Location = new System.Drawing.Point(6, 40);
            this.rbBestSellers.Name = "rbBestSellers";
            this.rbBestSellers.Size = new System.Drawing.Size(151, 17);
            this.rbBestSellers.TabIndex = 0;
            this.rbBestSellers.TabStop = true;
            this.rbBestSellers.Text = "Best Selling Items (Top 10)";
            this.rbBestSellers.UseVisualStyleBackColor = true;
            this.rbBestSellers.CheckedChanged += new System.EventHandler(this.rbBestSellers_CheckedChanged);
            // 
            // grpFilterCriteria
            // 
            this.grpFilterCriteria.Controls.Add(this.cmbFilterAgents);
            this.grpFilterCriteria.Controls.Add(this.lblSelectAgent);
            this.grpFilterCriteria.Location = new System.Drawing.Point(12, 211);
            this.grpFilterCriteria.Name = "grpFilterCriteria";
            this.grpFilterCriteria.Size = new System.Drawing.Size(279, 204);
            this.grpFilterCriteria.TabIndex = 1;
            this.grpFilterCriteria.TabStop = false;
            this.grpFilterCriteria.Text = "Filter Criteria";
            // 
            // cmbFilterAgents
            // 
            this.cmbFilterAgents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterAgents.FormattingEnabled = true;
            this.cmbFilterAgents.Location = new System.Drawing.Point(83, 34);
            this.cmbFilterAgents.Name = "cmbFilterAgents";
            this.cmbFilterAgents.Size = new System.Drawing.Size(121, 21);
            this.cmbFilterAgents.TabIndex = 1;
            this.cmbFilterAgents.Visible = false;
            this.cmbFilterAgents.Click += new System.EventHandler(this.FilterForm_Load);
            // 
            // lblSelectAgent
            // 
            this.lblSelectAgent.AutoSize = true;
            this.lblSelectAgent.Location = new System.Drawing.Point(6, 34);
            this.lblSelectAgent.Name = "lblSelectAgent";
            this.lblSelectAgent.Size = new System.Drawing.Size(71, 13);
            this.lblSelectAgent.TabIndex = 0;
            this.lblSelectAgent.Text = "Select Agent:";
            this.lblSelectAgent.Visible = false;
            this.lblSelectAgent.Click += new System.EventHandler(this.FilterForm_Load);
            // 
            // dgvFilterResults
            // 
            this.dgvFilterResults.AllowUserToAddRows = false;
            this.dgvFilterResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFilterResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFilterResults.Location = new System.Drawing.Point(449, 52);
            this.dgvFilterResults.Name = "dgvFilterResults";
            this.dgvFilterResults.ReadOnly = true;
            this.dgvFilterResults.Size = new System.Drawing.Size(240, 150);
            this.dgvFilterResults.TabIndex = 2;
            this.dgvFilterResults.Click += new System.EventHandler(this.FilterForm_Load);
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.Location = new System.Drawing.Point(524, 243);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(75, 23);
            this.btnApplyFilter.TabIndex = 3;
            this.btnApplyFilter.Text = "Apply Filter";
            this.btnApplyFilter.UseVisualStyleBackColor = true;
            this.btnApplyFilter.Click += new System.EventHandler(this.btnApplyFilter_Click);
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnApplyFilter);
            this.Controls.Add(this.dgvFilterResults);
            this.Controls.Add(this.grpFilterCriteria);
            this.Controls.Add(this.grpFilterType);
            this.Name = "FilterForm";
            this.Text = "Filter Reports";
            this.grpFilterType.ResumeLayout(false);
            this.grpFilterType.PerformLayout();
            this.grpFilterCriteria.ResumeLayout(false);
            this.grpFilterCriteria.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilterResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFilterType;
        private System.Windows.Forms.RadioButton rbAgentHistory;
        private System.Windows.Forms.RadioButton rbBestSellers;
        private System.Windows.Forms.GroupBox grpFilterCriteria;
        private System.Windows.Forms.ComboBox cmbFilterAgents;
        private System.Windows.Forms.Label lblSelectAgent;
        private System.Windows.Forms.DataGridView dgvFilterResults;
        private System.Windows.Forms.Button btnApplyFilter;
    }
}