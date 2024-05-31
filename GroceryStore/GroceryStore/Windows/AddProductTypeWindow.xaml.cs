using GroceryStore.Domain;
using GroceryStore.Infrastructure;
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
    /// Логика взаимодействия для AddProductTypeWindow.xaml
    /// </summary>
    public partial class AddProductTypeWindow : Window
    {
        private StoreDepartment department;
        private Storage storage;
        
        public AddProductTypeWindow(StoreDepartment department)
        {
            InitializeComponent();
            this.department = department;
            StorageConditionsCB.ItemsSource = Enum.GetValues(typeof(Storages));
            DepartmentsCB.ItemsSource = Enum.GetValues(typeof(Departments));
            DepartmentsCB.SelectedItem = department.Name;
        }

        public AddProductTypeWindow(Storage storage)
        {
            InitializeComponent();
            this.storage = storage;
            StorageConditionsCB.ItemsSource = Enum.GetValues(typeof(Storages));
            DepartmentsCB.ItemsSource = Enum.GetValues(typeof(Departments));
            StorageConditionsCB.SelectedItem = storage.Type;
        }

        public AddProductTypeWindow()
        {
            InitializeComponent();
            StorageConditionsCB.ItemsSource = Enum.GetValues(typeof(Storages));
            DepartmentsCB.ItemsSource = Enum.GetValues(typeof(Departments));
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
                    Shop ownerShop;
                    if (department == null && storage == null)
                        ownerShop = (Owner as ShopWindow).Shop;
                    else
                        ownerShop = (Owner.Owner as ShopWindow).Shop;

                    var shop = context.Shops
                        .Select(sh => sh)
                        .Where(sh => sh.ID == ownerShop.ID)
                        .FirstOrDefault();
                    var productType = new ProductType(NameTB.Text, decimal.Parse(PriceTB.Text), int.Parse(ShelfLifeTB.Text), 
                        (Departments)DepartmentsCB.SelectedIndex, (Storages)StorageConditionsCB.SelectedIndex, SortTB.Text, shop);
                    context.ProductTypes.Add(productType);
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе добавления продукта в БД возникла следующая ошибка: {ex.Message}");
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NameTB.Focus();
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
