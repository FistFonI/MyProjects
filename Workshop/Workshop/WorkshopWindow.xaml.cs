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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Workshop
{
    /// <summary>
    /// Логика взаимодействия для WorkshopWindow.xaml
    /// </summary>
    public partial class WorkshopWindow : Window
    {
        private List<RepairOrder> Orders;
        private List<RepairOrder> TodayOrders;

        public WorkshopWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DatePicker.DisplayDateEnd = DateTime.Now;
            UpdateGrids();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrderWindow addOrderWindow = new AddOrderWindow();
            addOrderWindow.Owner = this;
            addOrderWindow.ShowDialog();
            UpdateGrids();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AllOrdersGrid.SelectedItem == null)
                    MessageBox.Show($"Нужно выделить заказ, который вы хотите изменить.");
                else
                {
                    ChangeOrderWindow changeOrderWindow = new ChangeOrderWindow(AllOrdersGrid.SelectedItem as RepairOrder);
                    changeOrderWindow.Owner = this;
                    changeOrderWindow.ShowDialog();
                    UpdateGrids();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При нажатии на кнопку изменения заказа, возникла следующая ошибка: {ex.Message}");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AllOrdersGrid.SelectedItem == null)
                    MessageBox.Show($"Нужно выделить заказ, который вы хотите удалить.");
                else
                    DeleteFromDB(AllOrdersGrid.SelectedItem as RepairOrder);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При удалении заказа возникла следующая ошибка: {ex.Message}");
            }
            UpdateGrids();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            RepairOrderContext context = new RepairOrderContext();
            var date = new DateTime();
            var check = false;
            if (DatePicker.SelectedDate != null)
                date = DatePicker.SelectedDate.Value;
            else
                check = true;

            var orders = context.Orders
                .Where(a => a.Name.Contains(NameTextBox.Text))
                .Where(a => check ? a.Date.ToString().Contains("") : (a.Date.Year == date.Year && a.Date.Month == date.Month && a.Date.Day == date.Day))
                .Where(a => AllOrdersCB.Text == "" ? a.Readiness.Contains("") : a.Readiness == AllOrdersCB.Text)
                .ToList();
            AllOrdersGrid.ItemsSource = orders;
        }

        private void StatisticsTI_GotFocus(object sender, RoutedEventArgs e)
        {
            RepairOrderContext context = new RepairOrderContext();

            AllOrdersStatTB.Text = context.Orders.OrdersCount();
            AllReadyOrdersStatTB.Text = context.Orders.OrdersCount("Отремонтирован");
            AllCanceledOrdersStatTB.Text = context.Orders.OrdersCount("Отменён");

            var year = 0;
            try 
            {
                year = int.Parse(ResultYearsCB.Text);
            }
            catch
            {
                year = DateTime.Now.Year;
            }

            QuarterOrdersStatTB.Text = context.Orders.QuarterOrdersCount(year, QuarterCB.SelectedIndex);
            QuarterReadyOrdersStatTB.Text = context.Orders.QuarterOrdersCount(year, QuarterCB.SelectedIndex, "Отремонтирован");
            QuarterCanceledOrdersStatTB.Text = context.Orders.QuarterOrdersCount(year, QuarterCB.SelectedIndex, "Отменён");

            var quarterStatChange = int.Parse(QuarterOrdersStatTB.Text) - 
                int.Parse(context.Orders.QuarterOrdersCount(year, QuarterCB.SelectedIndex, "", 1));
            QuarterOrdersStatChangeTB.Text = quarterStatChange.ToString();
            if (quarterStatChange >= 0)
                QuarterOrdersStatChangeTB.Foreground = Brushes.Green;
            else
                QuarterOrdersStatChangeTB.Foreground = Brushes.Red;

            var quarterReadyStatChange = int.Parse(QuarterReadyOrdersStatTB.Text) - 
                int.Parse(context.Orders.QuarterOrdersCount(year, QuarterCB.SelectedIndex, "Отремонтирован", 1));
            QuarterReadyOrdersStatChangeTB.Text = quarterReadyStatChange.ToString();
            if (quarterReadyStatChange >= 0)
                QuarterReadyOrdersStatChangeTB.Foreground = Brushes.Green;
            else
                QuarterReadyOrdersStatChangeTB.Foreground = Brushes.Red;

            var quarterCanceledStatChange = int.Parse(QuarterCanceledOrdersStatTB.Text) - 
                int.Parse(context.Orders.QuarterOrdersCount(year, QuarterCB.SelectedIndex, "Отменён", 1));
            QuarterCanceledOrdersStatChangeTB.Text = quarterCanceledStatChange.ToString();
            if (quarterCanceledStatChange >= 0)
                QuarterCanceledOrdersStatChangeTB.Foreground = Brushes.Red;
            else
                QuarterCanceledOrdersStatChangeTB.Foreground = Brushes.Green;
        }

        private void StatisticsTI_Loaded(object sender, RoutedEventArgs e)
        {
            var month = DateTime.Now.Month;
            if (1 <= month && month <= 3)
                QuarterCB.SelectedIndex = 0;
            else if (4 <= month && month <= 6)
                QuarterCB.SelectedIndex = 1;
            else if (7 <= month && month <= 9)
                QuarterCB.SelectedIndex = 2;
            else if (10 <= month && month <= 12)
                QuarterCB.SelectedIndex = 3;
        }

        private void SearchTodayButton_Click(object sender, RoutedEventArgs e)
        {
            RepairOrderContext context = new RepairOrderContext();
            var today = DateTime.Now;

            var orders = context.Orders
                .Where(a => a.Name.Contains(NameTodayTextBox.Text))
                .Where(a => a.Date.Year == today.Year && a.Date.Month == today.Month && a.Date.Day == today.Day)
                .Where(a => a.Readiness == "Не отремонтирован" || a.Readiness == "Ремонтируется")
                .ToList();
            TodayOrdersGrid.ItemsSource = orders;
        }

        private void ChangeTodayButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TodayOrdersGrid.SelectedItem == null)
                    MessageBox.Show($"Нужно выделить заказ, который вы хотите изменить.");
                else
                {
                    ChangeOrderWindow changeOrderWindow = new ChangeOrderWindow(TodayOrdersGrid.SelectedItem as RepairOrder);
                    changeOrderWindow.Owner = this;
                    changeOrderWindow.ShowDialog();
                    UpdateGrids();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При нажатии на кнопку изменения заказа, возникла следующая ошибка: {ex.Message}");
            }
        }

        private void DeleteTodayButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TodayOrdersGrid.SelectedItem == null)
                    MessageBox.Show($"Нужно выделить заказ, который вы хотите удалить.");
                else
                    DeleteFromDB(TodayOrdersGrid.SelectedItem as RepairOrder);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При удалении заказа возникла следующая ошибка: {ex.Message}");
            }
            UpdateGrids();
        }

        private void UpdateGrids()
        {
            RepairOrderContext context = new RepairOrderContext();
            try
            {
                Orders = context.Orders
                    .Select(a => a)
                    .ToList();
                AllOrdersGrid.ItemsSource = Orders;
                OrdersCountTB.Text = Orders.Count().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе обновления таблицы со всеми заказами, возникла следующая ошибка: {ex}");
            }
            try
            {
                var today = DateTime.Now;
                TodayOrders = Orders
                    .Where(a => a.Date.Year == today.Year && a.Date.Month == today.Month && a.Date.Day == today.Day)
                    .Select(a => a)
                    .ToList();
                TodayOrdersGrid.ItemsSource = TodayOrders;
                TodayOrdersCountTB.Text = TodayOrders.Count().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе обновления таблицы с заказами на сегодня, возникла следующая ошибка: {ex}");
            }
            try
            {
                var years = context.Orders
                    .Select(a => a.Date.Year)
                    .Distinct()
                    .ToList();
                ResultYearsCB.ItemsSource = years;
                ResultYearsCB.SelectedItem = DateTime.Now.Year;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе обновления статистики, возникла следующая ошибка: {ex}");
            }
        }

        private void DeleteFromDB(RepairOrder order)
        {
            try
            {
                RepairOrderContext context = new RepairOrderContext();
                var ord = context.Orders
                    .Select(a => a)
                    .Where(a => a.ID == order.ID)
                    .FirstOrDefault();
                context.Orders.Remove(ord);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При удалении заказа из базы данных возникла следующая ошибка: {ex.Message}");
            }
        }
    }
}
