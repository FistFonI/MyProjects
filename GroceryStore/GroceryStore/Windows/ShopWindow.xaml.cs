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
    /// Логика взаимодействия для ShopWindow.xaml
    /// </summary>
    public partial class ShopWindow : Window
    {
        public Shop Shop { get; set; }
        private List<StoreDepartment> Departs;
        private List<ProductType> PTypes;
        private List<Storage> Storages;

        public ShopWindow(Shop shop)
        {
            InitializeComponent();
            Shop = shop;
            Title = shop.ToString();
            var list = new List<string>() { "" };
            var deps = Enum.GetValues(typeof(Departments)).Cast<Departments>().ToList();
            foreach (var e in deps)
                list.Add(e.ToString());
            DepartmentsPTypeCB.ItemsSource = list;
            DepartmentsPTypeCB.SelectedIndex = 0;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Owner.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateGrids();
        }

        private void BackFromSWBT_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddDepartButton_Click(object sender, RoutedEventArgs e)
        {
            AddDepartWindow addDepartWindow = new AddDepartWindow();
            addDepartWindow.Owner = this;
            addDepartWindow.ShowDialog();
            UpdateDepartDataGrid();
        }

        private void DeleteDepartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DepartDataGrid.SelectedItem == null)
                    MessageBox.Show($"Нужно выделить отдел, который вы хотите удалить.");
                else
                    using (StoreContext context = new StoreContext())
                    {
                        var depart = DepartDataGrid.SelectedItem as StoreDepartment;
                        var selectedDepart = context.StoreDepartments
                            .Select(d => d)
                            .Where(d => d.ShopID == Shop.ID)
                            .Where(d => d.ID == depart.ID)
                            .FirstOrDefault();
                        context.StoreDepartments.Remove(selectedDepart);
                        context.SaveChanges();
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При удалении отдела возникла следующая ошибка: {ex.Message}");
            }
            UpdateDepartDataGrid();
        }

        private void UpdateDepartDataGrid()
        {
            try
            {
                using (StoreContext context = new StoreContext())
                {
                    Departs = context.StoreDepartments
                        .Where(d => d.ShopID == Shop.ID)
                        .ToList();
                    DepartDataGrid.ItemsSource = Departs;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе обновления таблицы с отделами, возникла следующая ошибка: {ex.Message}");
            }
        }

        private void DepartRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            InitializeDepartWindow(row.Item as StoreDepartment);
        }

        private void ViewDepartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedDepart = DepartDataGrid.SelectedItem;
                if (selectedDepart == null)
                    MessageBox.Show($"Нужно выделить отдел, который вы хотите посмотреть.");
                else
                {
                    InitializeDepartWindow(selectedDepart as StoreDepartment);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При нажатии на кнопку просмотра отдела, возникла следующая ошибка: {ex.Message}");
            }
        }

        private void InitializeDepartWindow(StoreDepartment depart)
        {
            ProductTypeWindow pTypeWindow = new ProductTypeWindow(depart);
            pTypeWindow.Owner = this;
            pTypeWindow.Show();
            Hide();
            pTypeWindow.Left = Left;
            pTypeWindow.Top = Top;
        }

        private void AddPTypeButton_Click(object sender, RoutedEventArgs e)
        {
            AddProductTypeWindow addPTypeWindow = new AddProductTypeWindow();
            addPTypeWindow.Owner = this;
            addPTypeWindow.ShowDialog();
            UpdatePTypeDataGrid();
        }

        private void EditPTypeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PTypeDataGrid.SelectedItem == null)
                    MessageBox.Show($"Нужно выделить продукт, данные которого вы хотите изменить.");
                else
                {
                    EditProductTypeWindow editPTypeWindow = new EditProductTypeWindow(PTypeDataGrid.SelectedItem as ProductType);
                    editPTypeWindow.Owner = this;
                    editPTypeWindow.ShowDialog();
                    UpdatePTypeDataGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При нажатии на кнопку изменения продукта, возникла следующая ошибка: {ex.Message}");
            }
        }

        private void DeletePTypeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PTypeDataGrid.SelectedItem == null)
                    MessageBox.Show($"Нужно выделить продукт, который вы хотите удалить.");
                else
                {
                    using (StoreContext context = new StoreContext())
                    {
                        var pType = PTypeDataGrid.SelectedItem as ProductType;
                        var selectedPType = context.ProductTypes
                            .Select(pt => pt)
                            .Where(pt => pt.ShopID == Shop.ID)
                            .Where(pt => pt.ID == pType.ID)
                            .FirstOrDefault();
                        context.ProductTypes.Remove(selectedPType);
                        context.SaveChanges();
                    }
                    UpdatePTypeDataGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При удалении продукта возникла следующая ошибка: {ex.Message}");
            }
        }

        private void SearchPTypeButton_Click(object sender, RoutedEventArgs e)
        {
            using (StoreContext context = new StoreContext())
            {
                var filteredPTypes = context.ProductTypes
                    .Where(pt => pt.ShopID == Shop.ID)
                    .Where(pt => pt.Name.Contains(NamePTypeTB.Text))
                    .Where(pt => pt.Price.ToString().Contains(PricePTypeTB.Text))
                    .Where(pt => pt.Sort.ToString().Contains(SortPTypeTB.Text))
                    .Where(pt => pt.ShelfLife.ToString().Contains(ShelfLifePTypeTB.Text))
                    .Where(pt => StorageConditionsPTypeCB.SelectedIndex == 0 ? true : ((int)pt.StorageConditions == (StorageConditionsPTypeCB.SelectedIndex - 1)))
                    .Where(pt => DepartmentsPTypeCB.SelectedIndex == 0 ? true : ((int)pt.Department == (DepartmentsPTypeCB.SelectedIndex - 1)))
                    .ToList();
                PTypeDataGrid.ItemsSource = filteredPTypes;
            }
        }

        private void ViewPTypeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedPType = PTypeDataGrid.SelectedItem;
                if (selectedPType == null)
                    MessageBox.Show($"Нужно выделить продукт, который вы хотите посмотреть.");
                else
                {
                    InitializePTypeWindow(selectedPType as ProductType);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При нажатии на кнопку просмотра продукта, возникла следующая ошибка: {ex.Message}");
            }
        }

        private void PTypeRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            InitializePTypeWindow(row.Item as ProductType);
        }

        private void InitializePTypeWindow(ProductType pType)
        {
            ProductWindow productWindow = new ProductWindow(pType);
            productWindow.Owner = this;
            productWindow.Show();
            Hide();
            productWindow.Left = Left;
            productWindow.Top = Top;
        }

        private void UpdatePTypeDataGrid()
        {
            try
            {
                using (StoreContext context = new StoreContext())
                {
                    PTypes = context.ProductTypes
                        .Where(d => d.ShopID == Shop.ID)
                        .ToList();
                    PTypeDataGrid.ItemsSource = PTypes;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе обновления таблицы с продуктами, возникла следующая ошибка: {ex.Message}");
            }
        }

        private void AddStorageBT_Click(object sender, RoutedEventArgs e)
        {
            AddStorageWindow addStorageWindow = new AddStorageWindow();
            addStorageWindow.Owner = this;
            addStorageWindow.ShowDialog();
            UpdateStorageDataGrid();
        }

        private void EditStorageBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (StorageDataGrid.SelectedItem == null)
                    MessageBox.Show($"Нужно выделить склад, данные которого вы хотите изменить.");
                else
                {
                    EditStorageWindow editStorageWindow = new EditStorageWindow(StorageDataGrid.SelectedItem as Storage);
                    editStorageWindow.Owner = this;
                    editStorageWindow.ShowDialog();
                    UpdateStorageDataGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При нажатии на кнопку изменения склада, возникла следующая ошибка: {ex.Message}");
            }
        }

        private void DeleteStorageBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (StorageDataGrid.SelectedItem == null)
                    MessageBox.Show($"Нужно выделить склад, который вы хотите удалить.");
                else
                {
                    using (StoreContext context = new StoreContext())
                    {
                        var storage = StorageDataGrid.SelectedItem as Storage;
                        var selectedStorage = context.Storages
                            .Select(st => st)
                            .Where(st => st.ShopID == Shop.ID)
                            .Where(st => st.ID == storage.ID)
                            .FirstOrDefault();
                        context.Storages.Remove(selectedStorage);
                        context.SaveChanges();
                    }
                    UpdateStorageDataGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При удалении склада возникла следующая ошибка: {ex.Message}");
            }
        }

        private void ViewStorageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedStorage = StorageDataGrid.SelectedItem;
                if (selectedStorage == null)
                    MessageBox.Show($"Нужно выделить склад, который вы хотите посмотреть.");
                else
                {
                    InitializeStorageWindow(selectedStorage as Storage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При нажатии на кнопку просмотра склада, возникла следующая ошибка: {ex.Message}");
            }
        }

        private void StorageRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            InitializeStorageWindow(row.Item as Storage);
        }

        private void InitializeStorageWindow(Storage storage)
        {
            ProductTypeWindow pTypeWindow = new ProductTypeWindow(storage);
            pTypeWindow.Owner = this;
            pTypeWindow.Show();
            Hide();
            pTypeWindow.Left = Left;
            pTypeWindow.Top = Top;
        }

        private void UpdateStorageDataGrid()
        {
            try
            {
                using (StoreContext context = new StoreContext())
                {
                    Storages = context.Storages
                        .Where(st => st.ShopID == Shop.ID)
                        .ToList();
                    StorageDataGrid.ItemsSource = Storages;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе обновления таблицы со складами, возникла следующая ошибка: {ex.Message}");
            }
        }

        private void UpdateGrids()
        {
            UpdateDepartDataGrid();
            UpdatePTypeDataGrid();
            UpdateStorageDataGrid();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
                UpdateGrids();
        }
    }
}
