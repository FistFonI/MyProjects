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
    public partial class MainMenuForm : Form
    {
        private static MainMenuForm inst;

        public static MainMenuForm GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new MainMenuForm();
                return inst;
            }
        }
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void EmpMenuButton_Click(object sender, EventArgs e)
        {
            EmployeeMenuForm.GetForm.Show();
            EmployeeMenuForm.GetForm.Location = this.Location;
            this.Hide();
        }

        private void StatButton_Click(object sender, EventArgs e)
        {
            StatisticForm.GetForm.Show();
            StatisticForm.GetForm.Location = this.Location;
            this.Hide();
        }

        private void MainMenuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
