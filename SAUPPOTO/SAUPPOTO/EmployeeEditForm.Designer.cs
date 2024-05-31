
namespace SAUPPOTO
{
    partial class EmployeeEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeeEditForm));
            this.EmpEditBackGround = new System.Windows.Forms.Panel();
            this.EditButton = new System.Windows.Forms.Button();
            this.BackButton = new System.Windows.Forms.Button();
            this.PremiumTextBox = new System.Windows.Forms.TextBox();
            this.WorkingHours = new System.Windows.Forms.Label();
            this.WorkHoursTextBox = new System.Windows.Forms.TextBox();
            this.Premium = new System.Windows.Forms.Label();
            this.HourlyPaymentTextBox = new System.Windows.Forms.TextBox();
            this.HourlyPayment = new System.Windows.Forms.Label();
            this.PositionTextBox = new System.Windows.Forms.TextBox();
            this.Position = new System.Windows.Forms.Label();
            this.FullNameTextBox = new System.Windows.Forms.TextBox();
            this.FullName = new System.Windows.Forms.Label();
            this.EmpEditBackGround.SuspendLayout();
            this.SuspendLayout();
            // 
            // EmpEditBackGround
            // 
            this.EmpEditBackGround.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(180)))), ((int)(((byte)(140)))));
            this.EmpEditBackGround.Controls.Add(this.PremiumTextBox);
            this.EmpEditBackGround.Controls.Add(this.WorkingHours);
            this.EmpEditBackGround.Controls.Add(this.WorkHoursTextBox);
            this.EmpEditBackGround.Controls.Add(this.Premium);
            this.EmpEditBackGround.Controls.Add(this.HourlyPaymentTextBox);
            this.EmpEditBackGround.Controls.Add(this.HourlyPayment);
            this.EmpEditBackGround.Controls.Add(this.PositionTextBox);
            this.EmpEditBackGround.Controls.Add(this.Position);
            this.EmpEditBackGround.Controls.Add(this.FullNameTextBox);
            this.EmpEditBackGround.Controls.Add(this.FullName);
            this.EmpEditBackGround.Controls.Add(this.EditButton);
            this.EmpEditBackGround.Controls.Add(this.BackButton);
            this.EmpEditBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EmpEditBackGround.Location = new System.Drawing.Point(0, 0);
            this.EmpEditBackGround.Name = "EmpEditBackGround";
            this.EmpEditBackGround.Size = new System.Drawing.Size(800, 450);
            this.EmpEditBackGround.TabIndex = 0;
            // 
            // EditButton
            // 
            this.EditButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.EditButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EditButton.Font = new System.Drawing.Font("Malgun Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditButton.Location = new System.Drawing.Point(577, 373);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(211, 65);
            this.EditButton.TabIndex = 19;
            this.EditButton.Text = "Редактировать";
            this.EditButton.UseVisualStyleBackColor = false;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // BackButton
            // 
            this.BackButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BackButton.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackButton.Location = new System.Drawing.Point(12, 12);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(201, 27);
            this.BackButton.TabIndex = 2;
            this.BackButton.Text = "Возврат к списку сотрудников";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // PremiumTextBox
            // 
            this.PremiumTextBox.Location = new System.Drawing.Point(337, 316);
            this.PremiumTextBox.Name = "PremiumTextBox";
            this.PremiumTextBox.Size = new System.Drawing.Size(235, 20);
            this.PremiumTextBox.TabIndex = 31;
            this.PremiumTextBox.TextChanged += new System.EventHandler(this.PremiumTextBox_TextChanged);
            // 
            // WorkingHours
            // 
            this.WorkingHours.AutoSize = true;
            this.WorkingHours.Font = new System.Drawing.Font("Malgun Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkingHours.Location = new System.Drawing.Point(57, 254);
            this.WorkingHours.Name = "WorkingHours";
            this.WorkingHours.Size = new System.Drawing.Size(252, 30);
            this.WorkingHours.TabIndex = 30;
            this.WorkingHours.Text = "Кол-во рабочих часов:";
            // 
            // WorkHoursTextBox
            // 
            this.WorkHoursTextBox.Location = new System.Drawing.Point(337, 264);
            this.WorkHoursTextBox.Name = "WorkHoursTextBox";
            this.WorkHoursTextBox.Size = new System.Drawing.Size(235, 20);
            this.WorkHoursTextBox.TabIndex = 29;
            this.WorkHoursTextBox.TextChanged += new System.EventHandler(this.WorkHoursTextBox_TextChanged);
            // 
            // Premium
            // 
            this.Premium.AutoSize = true;
            this.Premium.Font = new System.Drawing.Font("Malgun Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Premium.Location = new System.Drawing.Point(58, 306);
            this.Premium.Name = "Premium";
            this.Premium.Size = new System.Drawing.Size(100, 30);
            this.Premium.TabIndex = 28;
            this.Premium.Text = "Премия:";
            // 
            // HourlyPaymentTextBox
            // 
            this.HourlyPaymentTextBox.Location = new System.Drawing.Point(337, 211);
            this.HourlyPaymentTextBox.Name = "HourlyPaymentTextBox";
            this.HourlyPaymentTextBox.Size = new System.Drawing.Size(235, 20);
            this.HourlyPaymentTextBox.TabIndex = 27;
            this.HourlyPaymentTextBox.TextChanged += new System.EventHandler(this.HourlyPaymentTextBox_TextChanged);
            // 
            // HourlyPayment
            // 
            this.HourlyPayment.AutoSize = true;
            this.HourlyPayment.Font = new System.Drawing.Font("Malgun Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HourlyPayment.Location = new System.Drawing.Point(57, 201);
            this.HourlyPayment.Name = "HourlyPayment";
            this.HourlyPayment.Size = new System.Drawing.Size(152, 30);
            this.HourlyPayment.TabIndex = 26;
            this.HourlyPayment.Text = "Оплата в час:";
            // 
            // PositionTextBox
            // 
            this.PositionTextBox.Location = new System.Drawing.Point(337, 150);
            this.PositionTextBox.Name = "PositionTextBox";
            this.PositionTextBox.Size = new System.Drawing.Size(235, 20);
            this.PositionTextBox.TabIndex = 25;
            // 
            // Position
            // 
            this.Position.AutoSize = true;
            this.Position.Font = new System.Drawing.Font("Malgun Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Position.Location = new System.Drawing.Point(57, 140);
            this.Position.Name = "Position";
            this.Position.Size = new System.Drawing.Size(136, 30);
            this.Position.TabIndex = 24;
            this.Position.Text = "Должность:";
            // 
            // FullNameTextBox
            // 
            this.FullNameTextBox.Location = new System.Drawing.Point(337, 96);
            this.FullNameTextBox.Name = "FullNameTextBox";
            this.FullNameTextBox.Size = new System.Drawing.Size(235, 20);
            this.FullNameTextBox.TabIndex = 23;
            // 
            // FullName
            // 
            this.FullName.AutoSize = true;
            this.FullName.Font = new System.Drawing.Font("Malgun Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FullName.Location = new System.Drawing.Point(57, 86);
            this.FullName.Name = "FullName";
            this.FullName.Size = new System.Drawing.Size(97, 30);
            this.FullName.TabIndex = 22;
            this.FullName.Text = "Ф. И. О.:";
            // 
            // EmployeeEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.EmpEditBackGround);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EmployeeEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SAUPPOTO";
            this.Activated += new System.EventHandler(this.EmployeeEditForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EmployeeEditForm_FormClosed);
            this.EmpEditBackGround.ResumeLayout(false);
            this.EmpEditBackGround.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel EmpEditBackGround;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.TextBox PremiumTextBox;
        private System.Windows.Forms.Label WorkingHours;
        private System.Windows.Forms.TextBox WorkHoursTextBox;
        private System.Windows.Forms.Label Premium;
        private System.Windows.Forms.TextBox HourlyPaymentTextBox;
        private System.Windows.Forms.Label HourlyPayment;
        private System.Windows.Forms.TextBox PositionTextBox;
        private System.Windows.Forms.Label Position;
        private System.Windows.Forms.TextBox FullNameTextBox;
        private System.Windows.Forms.Label FullName;
    }
}