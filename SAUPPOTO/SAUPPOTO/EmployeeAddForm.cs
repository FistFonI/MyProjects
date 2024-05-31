using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAUPPOTO
{
    public partial class EmployeeAddForm : Form
    {
        private Employee Employee;

        private static EmployeeAddForm inst;
        
        public static EmployeeAddForm GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new EmployeeAddForm();
                return inst;
            }
        }

        public EmployeeAddForm()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            EmployeeMenuForm.GetForm.Show();
            EmployeeMenuForm.GetForm.Location = this.Location;
            this.Hide();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Employee.ID = Guid.NewGuid();
            Employee.FullName = FullNameTextBox.Text;
            Employee.Position = PositionTextBox.Text;
            Employee.Salary.HourlyPayValue = decimal.Parse(HourlyPaymentTextBox.Text);
            Employee.Salary.WorkingHours = int.Parse(WorkHoursTextBox.Text);
            Employee.Salary.PremiumValue = decimal.Parse(PremiumTextBox.Text);
            EmployeeMenuForm.GetForm.Employees.Add(Employee);
            EmployeeMenuForm.GetForm.Show();
            EmployeeMenuForm.GetForm.Location = this.Location;
            this.Hide();
        }

        private void EmployeeAddForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void HourlyPaymentTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal.Parse(HourlyPaymentTextBox.Text);
            }
            catch
            {
                HourlyPaymentTextBox.Text = "";
            }
            
        }

        private void WorkHoursTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal.Parse(WorkHoursTextBox.Text);
            }
            catch
            {
                WorkHoursTextBox.Text = "";
            }
        }

        private void PremiumTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal.Parse(PremiumTextBox.Text);
            }
            catch
            {
                PremiumTextBox.Text = "";
            }
        }

        private void EmployeeAddForm_Activated(object sender, EventArgs e)
        {
            FullNameTextBox.Text = "";
            PositionTextBox.Text = "";
            HourlyPaymentTextBox.Text = "";
            WorkHoursTextBox.Text = "";
            PremiumTextBox.Text = "";
            Employee = new Employee();
        }
    }
}
