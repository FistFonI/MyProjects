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
using GroceryStore.Domain;
using GroceryStore.Infrastructure;

namespace GroceryStore.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddShopWindow.xaml
    /// </summary>
    public partial class AddShopWindow : Window
    {
        public AddShopWindow()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var shop = new Shop(NameShopTB.Text, AddressShopTB.Text);
                using (StoreContext context = new StoreContext())
                {
                    context.Shops.Add(shop);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе добавления магазина в БД возникла следующая ошибка: {ex.Message}");
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NameShopTB.Focus();
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
