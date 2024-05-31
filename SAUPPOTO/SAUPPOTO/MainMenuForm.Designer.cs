
namespace SAUPPOTO
{
    partial class MainMenuForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenuForm));
            this.MenuBackGround = new System.Windows.Forms.Panel();
            this.StatButton = new System.Windows.Forms.Button();
            this.EmpMenuButton = new System.Windows.Forms.Button();
            this.MenuLabel = new System.Windows.Forms.Label();
            this.MenuBackGround.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuBackGround
            // 
            this.MenuBackGround.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(180)))), ((int)(((byte)(140)))));
            this.MenuBackGround.Controls.Add(this.StatButton);
            this.MenuBackGround.Controls.Add(this.EmpMenuButton);
            this.MenuBackGround.Controls.Add(this.MenuLabel);
            this.MenuBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MenuBackGround.Location = new System.Drawing.Point(0, 0);
            this.MenuBackGround.Name = "MenuBackGround";
            this.MenuBackGround.Size = new System.Drawing.Size(800, 450);
            this.MenuBackGround.TabIndex = 1;
            // 
            // StatButton
            // 
            this.StatButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.StatButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StatButton.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatButton.Location = new System.Drawing.Point(231, 246);
            this.StatButton.Name = "StatButton";
            this.StatButton.Size = new System.Drawing.Size(341, 78);
            this.StatButton.TabIndex = 2;
            this.StatButton.Text = "Перейти к статистике";
            this.StatButton.UseVisualStyleBackColor = false;
            this.StatButton.Click += new System.EventHandler(this.StatButton_Click);
            // 
            // EmpMenuButton
            // 
            this.EmpMenuButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.EmpMenuButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EmpMenuButton.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmpMenuButton.Location = new System.Drawing.Point(231, 122);
            this.EmpMenuButton.Name = "EmpMenuButton";
            this.EmpMenuButton.Size = new System.Drawing.Size(341, 78);
            this.EmpMenuButton.TabIndex = 1;
            this.EmpMenuButton.Text = "Перейти к меню сотрудников";
            this.EmpMenuButton.UseVisualStyleBackColor = false;
            this.EmpMenuButton.Click += new System.EventHandler(this.EmpMenuButton_Click);
            // 
            // MenuLabel
            // 
            this.MenuLabel.AutoSize = true;
            this.MenuLabel.Font = new System.Drawing.Font("Century", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MenuLabel.Location = new System.Drawing.Point(263, 30);
            this.MenuLabel.Name = "MenuLabel";
            this.MenuLabel.Size = new System.Drawing.Size(286, 44);
            this.MenuLabel.TabIndex = 0;
            this.MenuLabel.Text = "Главное меню";
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MenuBackGround);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SAUPPOTO";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainMenuForm_FormClosed);
            this.MenuBackGround.ResumeLayout(false);
            this.MenuBackGround.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MenuBackGround;
        private System.Windows.Forms.Button StatButton;
        private System.Windows.Forms.Button EmpMenuButton;
        private System.Windows.Forms.Label MenuLabel;
    }
}

