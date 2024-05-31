
namespace ClientApp
{
    partial class ServersListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServersListForm));
            this.ServersGrid = new System.Windows.Forms.DataGridView();
            this.ColumnIp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOnline = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ServersGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ServersGrid
            // 
            this.ServersGrid.AllowUserToAddRows = false;
            this.ServersGrid.AllowUserToDeleteRows = false;
            this.ServersGrid.BackgroundColor = System.Drawing.Color.White;
            this.ServersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ServersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnIp,
            this.ColumnOnline});
            this.ServersGrid.Location = new System.Drawing.Point(14, 59);
            this.ServersGrid.MultiSelect = false;
            this.ServersGrid.Name = "ServersGrid";
            this.ServersGrid.ReadOnly = true;
            this.ServersGrid.RowHeadersVisible = false;
            this.ServersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ServersGrid.Size = new System.Drawing.Size(653, 339);
            this.ServersGrid.TabIndex = 0;
            // 
            // ColumnIp
            // 
            this.ColumnIp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnIp.HeaderText = "IP сервера";
            this.ColumnIp.Name = "ColumnIp";
            this.ColumnIp.ReadOnly = true;
            // 
            // ColumnOnline
            // 
            this.ColumnOnline.HeaderText = "Онлайн";
            this.ColumnOnline.Name = "ColumnOnline";
            this.ColumnOnline.ReadOnly = true;
            this.ColumnOnline.Width = 150;
            // 
            // ConnectButton
            // 
            this.ConnectButton.BackColor = System.Drawing.Color.White;
            this.ConnectButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ConnectButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ConnectButton.Location = new System.Drawing.Point(527, 406);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(140, 29);
            this.ConnectButton.TabIndex = 4;
            this.ConnectButton.Text = "Подключиться";
            this.ConnectButton.UseVisualStyleBackColor = false;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.BackColor = System.Drawing.Color.White;
            this.RefreshButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RefreshButton.Location = new System.Drawing.Point(14, 406);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(140, 29);
            this.RefreshButton.TabIndex = 3;
            this.RefreshButton.Text = "Обновить список";
            this.RefreshButton.UseVisualStyleBackColor = false;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "Список серверов";
            // 
            // ServersListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(681, 440);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.ServersGrid);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ServersListForm";
            this.Text = "ВКонт";
            this.Activated += new System.EventHandler(this.ServersListForm_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.ServersGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ServersGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIp;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOnline;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Label label1;
    }
}