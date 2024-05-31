using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Workshop
{
    /// <summary>
    /// Логика взаимодействия для ChangeOrderWindow.xaml
    /// </summary>
    public partial class ChangeOrderWindow : Window
    {
        private RepairOrder order = new RepairOrder();

        public ChangeOrderWindow(RepairOrder order)
        {
            InitializeComponent();
            this.order = order;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DatePicker.DisplayDateEnd = DateTime.Now;
            NameTextBox.Text = order.Name;
            DatePicker.SelectedDate = order.Date;
            ReadinessCB.Text = order.Readiness;
            DescriptionTextBox.Text = order.Description;
            NameTextBox.Focus();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                order.Name = NameTextBox.Text;
                order.Date = DatePicker.SelectedDate.Value;
                order.Readiness = ReadinessCB.Text;
                order.Description = DescriptionTextBox.Text;
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show($"Поле ввода марки изделия не должно быть пустым.");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Поле ввода даты приёма не должно быть пустым.");
            }
            try
            {
                RepairOrderContext context = new RepairOrderContext();
                IEnumerable<RepairOrder> orders = context.Orders
                    .Select(a => a)
                    .AsEnumerable()
                    .Select(a =>
                    {
                        a.Name = NameTextBox.Text;
                        a.Date = order.Date;
                        a.Readiness = order.Readiness;
                        a.Description = a.Description;
                        return a;
                    });
                    context.Entry(order).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе изменения заказа в БД возникла следующая ошибка: {ex.Message}");
            }
            Close();
        }
    }
}
