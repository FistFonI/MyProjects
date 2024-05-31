
namespace SAUPPOTO
{
    partial class EmployeeMenuForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeeMenuForm));
            this.EmpMenuBackGround = new System.Windows.Forms.Panel();
            this.EmployeeTable = new System.Windows.Forms.DataGridView();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.EditButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.EmpList = new System.Windows.Forms.Label();
            this.BackButton = new System.Windows.Forms.Button();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HourlyPayment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkingHours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PremiumValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Salary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpMenuBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EmployeeTable)).BeginInit();
            this.SuspendLayout();
            // 
            // EmpMenuBackGround
            // 
            this.EmpMenuBackGround.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(180)))), ((int)(((byte)(140)))));
            this.EmpMenuBackGround.Controls.Add(this.EmployeeTable);
            this.EmpMenuBackGround.Controls.Add(this.DeleteButton);
            this.EmpMenuBackGround.Controls.Add(this.EditButton);
            this.EmpMenuBackGround.Controls.Add(this.AddButton);
            this.EmpMenuBackGround.Controls.Add(this.SearchTextBox);
            this.EmpMenuBackGround.Controls.Add(this.SearchButton);
            this.EmpMenuBackGround.Controls.Add(this.EmpList);
            this.EmpMenuBackGround.Controls.Add(this.BackButton);
            this.EmpMenuBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EmpMenuBackGround.Location = new System.Drawing.Point(0, 0);
            this.EmpMenuBackGround.Name = "EmpMenuBackGround";
            this.EmpMenuBackGround.Size = new System.Drawing.Size(904, 625);
            this.EmpMenuBackGround.TabIndex = 1;
            // 
            // EmployeeTable
            // 
            this.EmployeeTable.AllowUserToAddRows = false;
            this.EmployeeTable.AllowUserToDeleteRows = false;
            this.EmployeeTable.AllowUserToResizeRows = false;
            this.EmployeeTable.BackgroundColor = System.Drawing.SystemColors.Control;
            this.EmployeeTable.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.EmployeeTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EmployeeTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.FullName,
            this.Position,
            this.HourlyPayment,
            this.WorkingHours,
            this.PremiumValue,
            this.Salary});
            this.EmployeeTable.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.EmployeeTable.Location = new System.Drawing.Point(12, 69);
            this.EmployeeTable.MultiSelect = false;
            this.EmployeeTable.Name = "EmployeeTable";
            this.EmployeeTable.ReadOnly = true;
            this.EmployeeTable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.EmployeeTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.EmployeeTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.EmployeeTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.EmployeeTable.Size = new System.Drawing.Size(880, 498);
            this.EmployeeTable.TabIndex = 2;
            this.EmployeeTable.TabStop = false;
            this.EmployeeTable.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.EmployeeTable_RowPrePaint);
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.DeleteButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteButton.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteButton.Location = new System.Drawing.Point(384, 573);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(180, 40);
            this.DeleteButton.TabIndex = 11;
            this.DeleteButton.Text = "Удалить сотрудника";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // EditButton
            // 
            this.EditButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.EditButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EditButton.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditButton.Location = new System.Drawing.Point(198, 573);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(180, 40);
            this.EditButton.TabIndex = 10;
            this.EditButton.Text = "Редактировать сотрудника";
            this.EditButton.UseVisualStyleBackColor = false;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.AddButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddButton.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddButton.Location = new System.Drawing.Point(12, 572);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(180, 40);
            this.AddButton.TabIndex = 9;
            this.AddButton.Text = "Добавить нового сотрудника";
            this.AddButton.UseVisualStyleBackColor = false;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Location = new System.Drawing.Point(570, 583);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(219, 20);
            this.SearchTextBox.TabIndex = 8;
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.SearchButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SearchButton.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchButton.Location = new System.Drawing.Point(802, 583);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(90, 21);
            this.SearchButton.TabIndex = 7;
            this.SearchButton.Text = "Искать";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // EmpList
            // 
            this.EmpList.AutoSize = true;
            this.EmpList.Font = new System.Drawing.Font("Century", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EmpList.Location = new System.Drawing.Point(266, 9);
            this.EmpList.Name = "EmpList";
            this.EmpList.Size = new System.Drawing.Size(398, 44);
            this.EmpList.TabIndex = 6;
            this.EmpList.Text = "Список сотрудников";
            // 
            // BackButton
            // 
            this.BackButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.BackButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BackButton.Font = new System.Drawing.Font("Malgun Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackButton.Location = new System.Drawing.Point(12, 12);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(171, 29);
            this.BackButton.TabIndex = 5;
            this.BackButton.Text = "Возврат в главное меню";
            this.BackButton.UseVisualStyleBackColor = false;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // ID
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ID.DefaultCellStyle = dataGridViewCellStyle1;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ID.Width = 230;
            // 
            // FullName
            // 
            this.FullName.HeaderText = "ФИО";
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            this.FullName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FullName.Width = 200;
            // 
            // Position
            // 
            this.Position.HeaderText = "Должность";
            this.Position.Name = "Position";
            this.Position.ReadOnly = true;
            this.Position.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // HourlyPayment
            // 
            this.HourlyPayment.HeaderText = "Почасовая оплата труда";
            this.HourlyPayment.Name = "HourlyPayment";
            this.HourlyPayment.ReadOnly = true;
            this.HourlyPayment.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.HourlyPayment.Width = 70;
            // 
            // WorkingHours
            // 
            this.WorkingHours.HeaderText = "Кол-во рабочих часов";
            this.WorkingHours.Name = "WorkingHours";
            this.WorkingHours.ReadOnly = true;
            this.WorkingHours.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.WorkingHours.Width = 50;
            // 
            // PremiumValue
            // 
            this.PremiumValue.HeaderText = "Размер премии";
            this.PremiumValue.Name = "PremiumValue";
            this.PremiumValue.ReadOnly = true;
            this.PremiumValue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PremiumValue.Width = 70;
            // 
            // Salary
            // 
            this.Salary.HeaderText = "Зарплата";
            this.Salary.Name = "Salary";
            this.Salary.ReadOnly = true;
            this.Salary.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Salary.Width = 120;
            // 
            // EmployeeMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 625);
            this.Controls.Add(this.EmpMenuBackGround);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EmployeeMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SAUPPOTO";
            this.Activated += new System.EventHandler(this.EmployeeMenuForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EmployeeMenuForm_FormClosed);
            this.EmpMenuBackGround.ResumeLayout(false);
            this.EmpMenuBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EmployeeTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel EmpMenuBackGround;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.TextBox SearchTextBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Label EmpList;
        private System.Windows.Forms.DataGridView EmployeeTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Position;
        private System.Windows.Forms.DataGridViewTextBoxColumn HourlyPayment;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkingHours;
        private System.Windows.Forms.DataGridViewTextBoxColumn PremiumValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Salary;
    }
}