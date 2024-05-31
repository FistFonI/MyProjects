
namespace SAUPPOTO
{
    partial class StatisticForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticForm));
            this.StatBackGround = new System.Windows.Forms.Panel();
            this.BackButton = new System.Windows.Forms.Button();
            this.SumLabel = new System.Windows.Forms.Label();
            this.EmpCountLabel = new System.Windows.Forms.Label();
            this.MoneyCountLabel = new System.Windows.Forms.Label();
            this.EmpCount = new System.Windows.Forms.Label();
            this.MenuLabel = new System.Windows.Forms.Label();
            this.StatBackGround.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatBackGround
            // 
            this.StatBackGround.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(180)))), ((int)(((byte)(140)))));
            this.StatBackGround.Controls.Add(this.BackButton);
            this.StatBackGround.Controls.Add(this.SumLabel);
            this.StatBackGround.Controls.Add(this.EmpCountLabel);
            this.StatBackGround.Controls.Add(this.MoneyCountLabel);
            this.StatBackGround.Controls.Add(this.EmpCount);
            this.StatBackGround.Controls.Add(this.MenuLabel);
            this.StatBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatBackGround.Location = new System.Drawing.Point(0, 0);
            this.StatBackGround.Name = "StatBackGround";
            this.StatBackGround.Size = new System.Drawing.Size(800, 450);
            this.StatBackGround.TabIndex = 2;
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
            // SumLabel
            // 
            this.SumLabel.AutoSize = true;
            this.SumLabel.BackColor = System.Drawing.Color.LightGray;
            this.SumLabel.Font = new System.Drawing.Font("Century", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SumLabel.Location = new System.Drawing.Point(409, 262);
            this.SumLabel.Name = "SumLabel";
            this.SumLabel.Size = new System.Drawing.Size(0, 25);
            this.SumLabel.TabIndex = 4;
            // 
            // EmpCountLabel
            // 
            this.EmpCountLabel.AutoSize = true;
            this.EmpCountLabel.BackColor = System.Drawing.Color.LightGray;
            this.EmpCountLabel.Font = new System.Drawing.Font("Century", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EmpCountLabel.Location = new System.Drawing.Point(409, 189);
            this.EmpCountLabel.Name = "EmpCountLabel";
            this.EmpCountLabel.Size = new System.Drawing.Size(0, 25);
            this.EmpCountLabel.TabIndex = 3;
            // 
            // MoneyCountLabel
            // 
            this.MoneyCountLabel.AutoSize = true;
            this.MoneyCountLabel.Font = new System.Drawing.Font("Century", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MoneyCountLabel.Location = new System.Drawing.Point(75, 262);
            this.MoneyCountLabel.Name = "MoneyCountLabel";
            this.MoneyCountLabel.Size = new System.Drawing.Size(246, 25);
            this.MoneyCountLabel.TabIndex = 2;
            this.MoneyCountLabel.Text = "Общее сумма выплат";
            // 
            // EmpCount
            // 
            this.EmpCount.AutoSize = true;
            this.EmpCount.Font = new System.Drawing.Font("Century", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EmpCount.Location = new System.Drawing.Point(75, 189);
            this.EmpCount.Name = "EmpCount";
            this.EmpCount.Size = new System.Drawing.Size(282, 25);
            this.EmpCount.TabIndex = 1;
            this.EmpCount.Text = "Количество сотрудников";
            // 
            // MenuLabel
            // 
            this.MenuLabel.AutoSize = true;
            this.MenuLabel.Font = new System.Drawing.Font("Century", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MenuLabel.Location = new System.Drawing.Point(245, 12);
            this.MenuLabel.Name = "MenuLabel";
            this.MenuLabel.Size = new System.Drawing.Size(302, 57);
            this.MenuLabel.TabIndex = 0;
            this.MenuLabel.Text = "Статистика";
            // 
            // StatisticForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.StatBackGround);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StatisticForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SAUPPOTO";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StatisticForm_FormClosed);
            this.Load += new System.EventHandler(this.StatisticForm_Load);
            this.StatBackGround.ResumeLayout(false);
            this.StatBackGround.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel StatBackGround;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Label SumLabel;
        private System.Windows.Forms.Label EmpCountLabel;
        private System.Windows.Forms.Label MoneyCountLabel;
        private System.Windows.Forms.Label EmpCount;
        private System.Windows.Forms.Label MenuLabel;
    }
}