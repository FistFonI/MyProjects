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
    public partial class EmployeeEditForm : Form
    {
        private Employee Employee;

        private static EmployeeEditForm inst;

        public static EmployeeEditForm GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new EmployeeEditForm();
                return inst;
            }
        }

        public EmployeeEditForm()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            EmployeeMenuForm.GetForm.Show();
            EmployeeMenuForm.GetForm.Location = this.Location;
            this.Hide();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            Employee.ID = Guid.NewGuid();
            Employee.FullName = FullNameTextBox.Text;
            Employee.Position = PositionTextBox.Text;
            Employee.Salary.HourlyPayValue = decimal.Parse(HourlyPaymentTextBox.Text);
            Employee.Salary.WorkingHours = int.Parse(WorkHoursTextBox.Text);
            Employee.Salary.PremiumValue = decimal.Parse(PremiumTextBox.Text);
            EmployeeMenuForm.GetForm.Employees[EmployeeMenuForm.GetForm.SelectedRow.Index] = Employee;
            EmployeeMenuForm.GetForm.Show();
            EmployeeMenuForm.GetForm.Location = this.Location;
            this.Hide();
        }

        private void EmployeeEditForm_FormClosed(object sender, FormClosedEventArgs e)
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

        private void EmployeeEditForm_Activated(object sender, EventArgs e)
        {
            Employee = new Employee();
            EmployeeMenuForm.GetForm.Employees.Remove(Employee);
            var row = EmployeeMenuForm.GetForm.SelectedRow;
            var cells = row.Cells;
            FullNameTextBox.Text = cells[1].Value.ToString();
            PositionTextBox.Text = cells[2].Value.ToString();
            HourlyPaymentTextBox.Text = cells[3].Value.ToString();
            WorkHoursTextBox.Text = cells[4].Value.ToString();
            PremiumTextBox.Text = cells[5].Value.ToString();
        }
    }
}
