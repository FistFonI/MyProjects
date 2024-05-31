using GroceryStore.Domain;
using GroceryStore.Windows;
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

namespace GroceryStore
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Shop> Shops;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddShopButton_Click(object sender, RoutedEventArgs e)
        {
            AddShopWindow addShopWindow = new AddShopWindow();
            addShopWindow.Owner = this;
            addShopWindow.ShowDialog();
            UpdateShopDataGrid();
        }

        private void ViewShopButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedShop = ShopDataGrid.SelectedItem;
                if (selectedShop == null)
                    MessageBox.Show($"Нужно выделить магазин, который вы хотите посмотреть.");
                else
                {
                    InitializeShopWindow(selectedShop as Shop);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При нажатии на кнопку просмотра магазина, возникла следующая ошибка: {ex.Message}");
            }
        }

        private void EditShopButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ShopDataGrid.SelectedItem == null)
                    MessageBox.Show($"Нужно выделить магазин, данные которого вы хотите изменить.");
                else
                {
                    EditShopWindow editShopWindow = new EditShopWindow(ShopDataGrid.SelectedItem as Shop);
                    editShopWindow.Owner = this;
                    editShopWindow.ShowDialog();
                    UpdateShopDataGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При нажатии на кнопку изменения магазина, возникла следующая ошибка: {ex.Message}");
            }
        }

        private void DeleteShopButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ShopDataGrid.SelectedItem == null)
                    MessageBox.Show($"Нужно выделить магазин, который вы хотите удалить.");
                else
                    using (StoreContext context = new StoreContext())
                    {
                        var shop = ShopDataGrid.SelectedItem as Shop;
                        var selectedShop = context.Shops
                            .Select(sh => sh)
                            .Where(sh => sh.ID == shop.ID)
                            .FirstOrDefault();
                        context.Shops.Remove(selectedShop);
                        context.SaveChanges();
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При удалении магазина возникла следующая ошибка: {ex.Message}");
            }
            UpdateShopDataGrid();
        }

        private void SearchShopButton_Click(object sender, RoutedEventArgs e)
        {
            using (StoreContext context = new StoreContext()) 
            {
                var filteredShops = context.Shops
                    .Where(sh => sh.Name.Contains(NameShopTB.Text))
                    .Where(sh => sh.Address.Contains(AdressShopTB.Text))
                    .ToList();
                ShopDataGrid.ItemsSource = filteredShops;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateShopDataGrid();
        }

        private void UpdateShopDataGrid()
        {
            try
            {
                using (StoreContext context = new StoreContext())
                {
                    Shops = context.Shops
                        .Select(sh => sh)
                        .ToList();
                    ShopDataGrid.ItemsSource = Shops;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе обновления таблицы с магазинами, возникла следующая ошибка: {ex.Message}");
            }
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            InitializeShopWindow(row.Item as Shop);
        }

        private void InitializeShopWindow(Shop shop)
        {
            ShopWindow shopWindow = new ShopWindow(shop);
            shopWindow.Owner = this;
            shopWindow.Show();
            Hide();
            shopWindow.Left = Left;
            shopWindow.Top = Top;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
                UpdateShopDataGrid();
        }

        private void EndDayButton_Click(object sender, RoutedEventArgs e)
        {
            using (StoreContext context = new StoreContext())
            {
                var newOrder = new AutoOrder(context);
                MessageBox.Show($"Заказы были сформированы, для просмотра вернитесь к таблице партий продуктов.");
            }
        }
    }
}
