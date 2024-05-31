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
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private ProductType type;

        public AddProductWindow(ProductType type)
        {
            InitializeComponent();
            this.type = type;
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
                    var productCount = context.Products
                        .Where(p => p.ProductType.ShopID == type.ShopID)
                        .Where(p => p.ProductType.StorageConditions == type.StorageConditions)
                        .Count();
                    int count = 0;
                    if (productCount > 0)
                        count = context.Products
                            .Where(p => p.ProductType.ShopID == type.ShopID)
                            .Where(p => p.ProductType.StorageConditions == type.StorageConditions)
                            .Select(p => p.Count)
                            .Sum() + int.Parse(CountOfProductTB.Text);
                    var storage = context.Storages
                        .Where(st => st.ShopID == type.ShopID)
                        .Where(st => st.Type == type.StorageConditions)
                        .FirstOrDefault();
                    if (storage == null)
                        MessageBox.Show("Отсутствует хранилище под указанный вид хранения товара.");
                    if (count > storage.Size)
                        throw new ArgumentException("Добавление указанного количества продуктов на склад невозможно, так как склад переполнится.");
                    var pType = context.ProductTypes
                        .Select(pt => pt)
                        .Where(pt => pt.ID == type.ID)
                        .FirstOrDefault();
                    var product = new Product((DateTime)DataOfBuyDP.SelectedDate, int.Parse(CountOfProductTB.Text),
                       (DateTime)DataOfmanufacturingDP.SelectedDate, pType);
                    context.Products.Add(product);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе добавления партии товара в БД возникла следующая ошибка: {ex.Message}");
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataOfBuyDP.Focus();
            DataOfmanufacturingDP.DisplayDateEnd = DateTime.Now;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void DataOfmanufacturingDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataOfmanufacturingDP.SelectedDate != null)
            {
                DataOfBuyDP.SelectedDate = DataOfmanufacturingDP.SelectedDate;
                DataOfBuyDP.DisplayDateStart = DataOfmanufacturingDP.SelectedDate;
                DataOfBuyDP.DisplayDateEnd = ((DateTime)DataOfmanufacturingDP.SelectedDate).AddDays(type.ShelfLife);
            }
        }
    }
}
