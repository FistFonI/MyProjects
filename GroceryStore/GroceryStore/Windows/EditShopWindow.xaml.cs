using GroceryStore.Domain;
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
    /// Логика взаимодействия для EditShopWindow.xaml
    /// </summary>
    public partial class EditShopWindow : Window
    {
        private Shop shop;

        public EditShopWindow(Shop shop)
        {
            InitializeComponent();
            this.shop = shop;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NameShopTB.Text = shop.Name;
            AddressShopTB.Text = shop.Address;
            NameShopTB.Focus();
            NameShopTB.Select(NameShopTB.Text.Length, 0);
            AddressShopTB.Select(AddressShopTB.Text.Length, 0);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                shop.Name = NameShopTB.Text;
                shop.Address = AddressShopTB.Text;
                using (StoreContext context = new StoreContext())
                {
                    context.Entry(shop).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе изменения данных магазина, в БД возникла следующая ошибка: {ex.Message}");
            }
            Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
