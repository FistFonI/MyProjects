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
    /// Логика взаимодействия для ProductTypeWindow.xaml
    /// </summary>
    public partial class ProductTypeWindow : Window
    {
        public StoreDepartment Depart { get; set; }
        public Storage Storage { get; set; }
        private List<ProductType> PTypes;

        public ProductTypeWindow(StoreDepartment depart)
        {
            InitializeComponent();
            Depart = depart;
            Storage = null;
        }

        public ProductTypeWindow(Storage storage)
        {
            InitializeComponent();
            Storage = storage;
            Depart = null;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void AddPTypeButton_Click(object sender, RoutedEventArgs e)
        {
            AddProductTypeWindow addPTypeWindow;
            if (Depart == null)
                addPTypeWindow = new AddProductTypeWindow(Storage);
            else
                addPTypeWindow = new AddProductTypeWindow(Depart);
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
                            .Where(pt => pt.ShopID == (Depart == null ? Storage.ShopID : Depart.ShopID))
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
                List<ProductType> filteredPTypes;
                if (Depart == null)
                {
                    filteredPTypes = context.ProductTypes
                        .Where(pt => pt.ShopID == Storage.ShopID)
                        .Where(pt => pt.Name.Contains(NamePTypeTB.Text))
                        .Where(pt => pt.Price.ToString().Contains(PricePTypeTB.Text))
                        .Where(pt => pt.Sort.ToString().Contains(SortPTypeTB.Text))
                        .Where(pt => pt.ShelfLife.ToString().Contains(ShelfLifePTypeTB.Text))
                        .Where(pt => StorageConditionsPTypeCB.SelectedIndex == 0 ? true : ((int)pt.StorageConditions == (StorageConditionsPTypeCB.SelectedIndex - 1)))
                        .Where(pt => DepartmentsPTypeCB.SelectedIndex == 0 ? true : ((int)pt.Department == (DepartmentsPTypeCB.SelectedIndex - 1)))
                        .ToList();
                }
                else
                {
                    filteredPTypes = context.ProductTypes
                        .Where(pt => pt.ShopID == Depart.ShopID)
                        .Where(pt => pt.Name.Contains(NamePTypeTB.Text))
                        .Where(pt => pt.Price.ToString().Contains(PricePTypeTB.Text))
                        .Where(pt => pt.Sort.ToString().Contains(SortPTypeTB.Text))
                        .Where(pt => pt.ShelfLife.ToString().Contains(ShelfLifePTypeTB.Text))
                        .Where(pt => StorageConditionsPTypeCB.SelectedIndex == 0 ? true : ((int)pt.StorageConditions == (StorageConditionsPTypeCB.SelectedIndex - 1)))
                        .Where(pt => DepartmentsPTypeCB.SelectedIndex == 0 ? true : ((int)pt.Department == (DepartmentsPTypeCB.SelectedIndex - 1)))
                        .ToList();
                }
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
                    if (Depart == null)
                    {
                        PTypes = context.ProductTypes
                            .Where(pt => pt.ShopID == Storage.ShopID)
                            .Where(pt => pt.StorageConditions == Storage.Type)
                            .ToList();
                    }
                    else
                    {
                        PTypes = context.ProductTypes
                            .Where(pt => pt.ShopID == Depart.ShopID)
                            .Where(pt => pt.Department == Depart.Name)
                            .ToList();
                    }
                    PTypeDataGrid.ItemsSource = PTypes;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе обновления таблицы с продуктами, возникла следующая ошибка: {ex.Message}");
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
            var list1 = new List<string>() { "" };
            var deps = Enum.GetValues(typeof(Departments)).Cast<Departments>().ToList();
            foreach (var d in deps)
                list1.Add(d.ToString());
            DepartmentsPTypeCB.ItemsSource = list1;
            DepartmentsPTypeCB.SelectedIndex = 0;
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
                UpdatePTypeDataGrid();
        }
    }
}
