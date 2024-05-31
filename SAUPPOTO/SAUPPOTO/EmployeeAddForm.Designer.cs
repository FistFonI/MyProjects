
namespace SAUPPOTO
{
    partial class EmployeeAddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeeAddForm));
            this.EmpAddBackGround = new System.Windows.Forms.Panel();
            this.PremiumTextBox = new System.Windows.Forms.TextBox();
            this.WorkingHours = new System.Windows.Forms.Label();
            this.AddButton = new System.Windows.Forms.Button();
            this.WorkHoursTextBox = new System.Windows.Forms.TextBox();
            this.Premium = new System.Windows.Forms.Label();
            this.HourlyPaymentTextBox = new System.Windows.Forms.TextBox();
            this.HourlyPayment = new System.Windows.Forms.Label();
            this.PositionTextBox = new System.Windows.Forms.TextBox();
            this.Position = new System.Windows.Forms.Label();
            this.FullNameTextBox = new System.Windows.Forms.TextBox();
            this.FullName = new System.Windows.Forms.Label();
            this.BackButton = new System.Windows.Forms.Button();
            this.EmpAddBackGround.SuspendLayout();
            this.SuspendLayout();
            // 
            // EmpAddBackGround
            // 
            this.EmpAddBackGround.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(180)))), ((int)(((byte)(140)))));
            this.EmpAddBackGround.Controls.Add(this.PremiumTextBox);
            this.EmpAddBackGround.Controls.Add(this.WorkingHours);
            this.EmpAddBackGround.Controls.Add(this.AddButton);
            this.EmpAddBackGround.Controls.Add(this.WorkHoursTextBox);
            this.EmpAddBackGround.Controls.Add(this.Premium);
            this.EmpAddBackGround.Controls.Add(this.HourlyPaymentTextBox);
            this.EmpAddBackGround.Controls.Add(this.HourlyPayment);
            this.EmpAddBackGround.Controls.Add(this.PositionTextBox);
            this.EmpAddBackGround.Controls.Add(this.Position);
            this.EmpAddBackGround.Controls.Add(this.FullNameTextBox);
            this.EmpAddBackGround.Controls.Add(this.FullName);
            this.EmpAddBackGround.Controls.Add(this.BackButton);
            this.EmpAddBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EmpAddBackGround.Location = new System.Drawing.Point(0, 0);
            this.EmpAddBackGround.Name = "EmpAddBackGround";
            this.EmpAddBackGround.Size = new System.Drawing.Size(800, 450);
            this.EmpAddBackGround.TabIndex = 1;
            // 
            // PremiumTextBox
            // 
            this.PremiumTextBox.Location = new System.Drawing.Point(334, 315);
            this.PremiumTextBox.Name = "PremiumTextBox";
            this.PremiumTextBox.Size = new System.Drawing.Size(235, 20);
            this.PremiumTextBox.TabIndex = 21;
            this.PremiumTextBox.TextChanged += new System.EventHandler(this.PremiumTextBox_TextChanged);
            // 
            // WorkingHours
            // 
            this.WorkingHours.AutoSize = true;
            this.WorkingHours.Font = new System.Drawing.Font("Malgun Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkingHours.Location = new System.Drawing.Point(54, 253);
            this.WorkingHours.Name = "WorkingHours";
            this.WorkingHours.Size = new System.Drawing.Size(252, 30);
            this.WorkingHours.TabIndex = 20;
            this.WorkingHours.Text = "Кол-во рабочих часов:";
            // 
            // AddButton
            // 
            this.AddButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.AddButton.Font = new System.Drawing.Font("Malgun Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddButton.Location = new System.Drawing.Point(577, 373);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(211, 65);
            this.AddButton.TabIndex = 19;
            this.AddButton.Text = "Создать";
            this.AddButton.UseVisualStyleBackColor = false;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // WorkHoursTextBox
            // 
            this.WorkHoursTextBox.Location = new System.Drawing.Point(334, 263);
            this.WorkHoursTextBox.Name = "WorkHoursTextBox";
            this.WorkHoursTextBox.Size = new System.Drawing.Size(235, 20);
            this.WorkHoursTextBox.TabIndex = 18;
            this.WorkHoursTextBox.TextChanged += new System.EventHandler(this.WorkHoursTextBox_TextChanged);
            // 
            // Premium
            // 
            this.Premium.AutoSize = true;
            this.Premium.Font = new System.Drawing.Font("Malgun Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Premium.Location = new System.Drawing.Point(55, 305);
            this.Premium.Name = "Premium";
            this.Premium.Size = new System.Drawing.Size(100, 30);
            this.Premium.TabIndex = 17;
            this.Premium.Text = "Премия:";
            // 
            // HourlyPaymentTextBox
            // 
            this.HourlyPaymentTextBox.Location = new System.Drawing.Point(334, 210);
            this.HourlyPaymentTextBox.Name = "HourlyPaymentTextBox";
            this.HourlyPaymentTextBox.Size = new System.Drawing.Size(235, 20);
            this.HourlyPaymentTextBox.TabIndex = 16;
            this.HourlyPaymentTextBox.TextChanged += new System.EventHandler(this.HourlyPaymentTextBox_TextChanged);
            // 
            // HourlyPayment
            // 
            this.HourlyPayment.AutoSize = true;
            this.HourlyPayment.Font = new System.Drawing.Font("Malgun Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HourlyPayment.Location = new System.Drawing.Point(54, 200);
            this.HourlyPayment.Name = "HourlyPayment";
            this.HourlyPayment.Size = new System.Drawing.Size(152, 30);
            this.HourlyPayment.TabIndex = 15;
            this.HourlyPayment.Text = "Оплата в час:";
            // 
            // PositionTextBox
            // 
            this.PositionTextBox.Location = new System.Drawing.Point(334, 149);
            this.PositionTextBox.Name = "PositionTextBox";
            this.PositionTextBox.Size = new System.Drawing.Size(235, 20);
            this.PositionTextBox.TabIndex = 14;
            // 
            // Position
            // 
            this.Position.AutoSize = true;
            this.Position.Font = new System.Drawing.Font("Malgun Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Position.Location = new System.Drawing.Point(54, 139);
            this.Position.Name = "Position";
            this.Position.Size = new System.Drawing.Size(136, 30);
            this.Position.TabIndex = 13;
            this.Position.Text = "Должность:";
            // 
            // FullNameTextBox
            // 
            this.FullNameTextBox.Location = new System.Drawing.Point(334, 95);
            this.FullNameTextBox.Name = "FullNameTextBox";
            this.FullNameTextBox.Size = new System.Drawing.Size(235, 20);
            this.FullNameTextBox.TabIndex = 12;
            // 
            // FullName
            // 
            this.FullName.AutoSize = true;
            this.FullName.Font = new System.Drawing.Font("Malgun Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FullName.Location = new System.Drawing.Point(54, 85);
            this.FullName.Name = "FullName";
            this.FullName.Size = new System.Drawing.Size(97, 30);
            this.FullName.TabIndex = 11;
            this.FullName.Text = "Ф. И. О.:";
            // 
            // BackButton
            // 
            this.BackButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.BackButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BackButton.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackButton.Location = new System.Drawing.Point(12, 12);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(201, 27);
            this.BackButton.TabIndex = 2;
            this.BackButton.Text = "Возврат к списку сотрудников";
            this.BackButton.UseVisualStyleBackColor = false;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // EmployeeAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.EmpAddBackGround);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EmployeeAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SAUPPOTO";
            this.Activated += new System.EventHandler(this.EmployeeAddForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EmployeeAddForm_FormClosed);
            this.EmpAddBackGround.ResumeLayout(false);
            this.EmpAddBackGround.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel EmpAddBackGround;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.TextBox WorkHoursTextBox;
        private System.Windows.Forms.Label Premium;
        private System.Windows.Forms.TextBox HourlyPaymentTextBox;
        private System.Windows.Forms.Label HourlyPayment;
        private System.Windows.Forms.TextBox PositionTextBox;
        private System.Windows.Forms.Label Position;
        private System.Windows.Forms.TextBox FullNameTextBox;
        private System.Windows.Forms.Label FullName;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.TextBox PremiumTextBox;
        private System.Windows.Forms.Label WorkingHours;
    }
}