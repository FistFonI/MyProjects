using GroceryStore.Domain;
using GroceryStore.Infrastructure;
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

namespace GroceryStore.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddDepartWindow.xaml
    /// </summary>
    public partial class AddDepartWindow : Window
    {
        public AddDepartWindow()
        {
            InitializeComponent();
            DepartCB.ItemsSource = Enum.GetValues(typeof(Departments));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StoreContext context = new StoreContext())
                {
                    var ownerShop = (Owner as ShopWindow).Shop;
                    var shop = context.Shops
                        .Select(sh => sh)
                        .Where(sh => sh.ID == ownerShop.ID)
                        .FirstOrDefault();
                    if (shop.StoreDepartments.Where(dep => dep.Name == (Departments)DepartCB.SelectedIndex).Count() == 0)
                    {
                        var depart = new StoreDepartment((Departments)DepartCB.SelectedIndex, shop);
                        context.StoreDepartments.Add(depart);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Магазин может содержать только один отдел одного типа.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе добавления отдела магазина в БД возникла следующая ошибка: {ex.Message}");
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DepartCB.Focus();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
