using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AddOrderForm.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        public AddOrderWindow()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var order = new RepairOrder();
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
                context.Orders.Add(order);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе добавления заказа в БД возникла следующая ошибка: {ex.Message}");
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DatePicker.DisplayDateEnd = DateTime.Now;
            DatePicker.SelectedDate = DateTime.Now;
            NotReadyCBI.IsSelected = true;
            NameTextBox.Focus();
        }
    }
}
