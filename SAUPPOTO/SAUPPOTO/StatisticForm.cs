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
    public partial class StatisticForm : Form
    {
        private static StatisticForm inst;

        public static StatisticForm GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new StatisticForm();
                return inst;
            }
        }

        public StatisticForm()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            MainMenuForm.GetForm.Show();
            MainMenuForm.GetForm.StartPosition = FormStartPosition.Manual;
            MainMenuForm.GetForm.Location = this.Location;
            this.Hide();
        }

        private void StatisticForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void StatisticForm_Load(object sender, EventArgs e)
        {
            EmpCountLabel.Text = EmployeeMenuForm.GetForm.Employees.Count.ToString();
            decimal sum = 0;
            foreach (var employee in EmployeeMenuForm.GetForm.Employees)
                sum += employee.Salary.SalaryValue;
            SumLabel.Text = sum.ToString();
        }
    }
}
