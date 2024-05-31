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
    public partial class EmployeeMenuForm : Form
    {
        public List<Employee> Employees;

        public DataGridViewRow SelectedRow;

        private static EmployeeMenuForm inst;

        public static EmployeeMenuForm GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new EmployeeMenuForm();
                return inst;
            }
        }

        public EmployeeMenuForm()
        {
            InitializeComponent();
            Employees = new List<Employee>();
        }

        private void EmployeeMenuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            MainMenuForm.GetForm.Show();
            MainMenuForm.GetForm.StartPosition = FormStartPosition.Manual;
            MainMenuForm.GetForm.Location = this.Location;
            this.Hide();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            EmployeeAddForm.GetForm.Show();
            EmployeeAddForm.GetForm.Location = this.Location;
            this.Hide();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            SelectedRow = EmployeeTable.SelectedRows[0];
            EmployeeEditForm.GetForm.Show();
            EmployeeEditForm.GetForm.Location = this.Location;
            this.Hide();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                Employee index = new Employee();
                foreach (var employee in Employees)
                {
                    if (employee.ID == Guid.Parse(EmployeeTable.SelectedRows[0].Cells[0].Value.ToString()))
                    {
                        index = employee;
                        break;
                    }
                        
                }
                Employees.Remove(index);
                EmployeeTable.Rows.RemoveAt(EmployeeTable.SelectedRows[0].Index);
            }
            catch
            {
                throw new Exception("Коллекция пуста.");
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < EmployeeTable.Rows.Count; i++)
            {
                EmployeeTable.Rows[i].Selected = false;
                if (EmployeeTable.Rows[i].Cells[1].Value.ToString().Contains(SearchTextBox.Text))
                {
                    EmployeeTable.CurrentCell = EmployeeTable.Rows[i].Cells[1];
                    EmployeeTable.Rows[i].Selected = true;
                    break;
                }
            }
        }

        private void EmployeeTable_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            object head = EmployeeTable.Rows[e.RowIndex].HeaderCell.Value;
            if (head == null || !head.Equals((e.RowIndex + 1).ToString()))
                EmployeeTable.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
        }

        private void EmployeeMenuForm_Activated(object sender, EventArgs e)
        {
            EmployeeTable.Rows.Clear();
            foreach (var emp in Employees)
            {
                EmployeeTable.Rows.Add(emp.ID, emp.FullName, emp.Position, emp.Salary.HourlyPayValue, 
                    emp.Salary.WorkingHours, emp.Salary.PremiumValue, emp.Salary.SalaryValue);
            }
        }
    }
}
