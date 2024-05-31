using GroceryStore.Domain;
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

namespace GroceryStore.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public ProductType PType { get; set; }
        private List<Product> Products;

        public ProductWindow(ProductType pType)
        {
            InitializeComponent();
            PType = pType;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void BackBT_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Owner.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateProductsDataGrid();
        }

        private void AddBT_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow addProductWindow = new AddProductWindow(PType); ;
            addProductWindow.Owner = this;
            addProductWindow.ShowDialog();
            UpdateProductsDataGrid();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProductsDataGrid.SelectedItem == null)
                    MessageBox.Show($"Нужно выделить партию продукта, данные которого вы хотите изменить.");
                else
                {
                    EditProductWindow editProductWindow = new EditProductWindow(ProductsDataGrid.SelectedItem as Product);
                    editProductWindow.Owner = this;
                    editProductWindow.ShowDialog();
                    UpdateProductsDataGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При нажатии на кнопку изменения партии продукта, возникла следующая ошибка: {ex.Message}");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProductsDataGrid.SelectedItem == null)
                    MessageBox.Show($"Нужно выделить партию продукта, которую вы хотите удалить.");
                else
                {
                    using (StoreContext context = new StoreContext())
                    {
                        var prod = ProductsDataGrid.SelectedItem as Product;
                        var selectedProduct = context.Products
                            .Select(p => p)
                            .Where(p => p.ProdTypeId == PType.ID)
                            .Where(p => p.ID == prod.ID)
                            .FirstOrDefault();
                        context.Products.Remove(selectedProduct);
                        context.SaveChanges();
                    }
                    UpdateProductsDataGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При удалении партии продукта возникла следующая ошибка: {ex.Message}");
            }
        }

        private void SearchBT_Click(object sender, RoutedEventArgs e)
        {
            using (StoreContext context = new StoreContext())
            {
                var buyDate = new DateTime();
                var checkBuy = false;
                if (BuyDateDataPicker.SelectedDate != null)
                    buyDate = BuyDateDataPicker.SelectedDate.Value;
                else
                    checkBuy = true;

                var manDate = new DateTime();
                var checkManufacture = false;
                if (DateOfManufactureDataPicker.SelectedDate != null)
                    manDate = DateOfManufactureDataPicker.SelectedDate.Value;
                else
                    checkManufacture = true;

                var filteredProducts = context.Products
                    .Where(p => p.ProdTypeId == PType.ID)
                    .Where(p => checkBuy ? p.BuyDate.ToString().Contains("") : 
                        (p.BuyDate.Year == buyDate.Year && p.BuyDate.Month == buyDate.Month && p.BuyDate.Day == buyDate.Day))
                    .Where(p => p.Balance.ToString().Contains(BuyCountTB.Text))
                    .Where(p => p.Sold.ToString().Contains(SoldTB.Text))
                    .Where(p => checkManufacture ? p.DateOfManufacture.ToString().Contains("") : 
                        (p.DateOfManufacture.Year == manDate.Year && p.DateOfManufacture.Month == manDate.Month && p.DateOfManufacture.Day == manDate.Day))
                    .ToList();
                ProductsDataGrid.ItemsSource = filteredProducts;
            }
        }

        private void UpdateProductsDataGrid()
        {
            try
            {
                using (StoreContext context = new StoreContext())
                {
                    Products = context.Products
                        .Where(p => p.ProdTypeId == PType.ID)
                        .ToList();
                    ProductsDataGrid.ItemsSource = Products;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе обновления таблицы с партиями продукта, возникла следующая ошибка: {ex}");
            }
        }
    }
}
