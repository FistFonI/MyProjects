using System;
using GroceryStore.Domain;
using GroceryStore.Infrastructure;
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

namespace GroceryStore.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddStorageWindow.xaml
    /// </summary>
    public partial class AddStorageWindow : Window
    {
        public AddStorageWindow()
        {
            InitializeComponent();
            TypeOfStorageCB.ItemsSource = Enum.GetValues(typeof(Storages));
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

                    if (shop.Storages.Where(st => st.Type == (Storages)TypeOfStorageCB.SelectedIndex).Count() == 0)
                    {
                        var storage = new Storage((Storages)TypeOfStorageCB.SelectedIndex, int.Parse(SizeOfStorageTB.Text), shop);
                        context.Storages.Add(storage);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Магазин может содержать только один склад одного типа.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе добавления склада магазина в БД возникла следующая ошибка: {ex.Message}");
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TypeOfStorageCB.Focus();
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
