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
using System.Data.Entity;

namespace GroceryStore.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditProductTypeWindow.xaml
    /// </summary>
    public partial class EditProductTypeWindow : Window
    {
        private ProductType pType;

        public EditProductTypeWindow(ProductType pType)
        {
            InitializeComponent();
            this.pType = pType;
            AddStorageConditionsCB.ItemsSource = Enum.GetValues(typeof(Storages));
            DepartmentsCB.ItemsSource = Enum.GetValues(typeof(Departments));
        }
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddSortOfProductTB.Text = pType.Sort;
            AddPriceOfProductTB.Text = pType.Price.ToString();
            NameOfPruductTB.Text = pType.Name;
            ShelfLifeTB.Text = pType.ShelfLife.ToString();
            AddStorageConditionsCB.SelectedItem = pType.StorageConditions;
            DepartmentsCB.SelectedItem = pType.Department;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pType.Sort = AddSortOfProductTB.Text;
                pType.Price = decimal.Parse(AddPriceOfProductTB.Text);
                pType.Name = NameOfPruductTB.Text;
                pType.ShelfLife = int.Parse(ShelfLifeTB.Text);
                pType.StorageConditions = (Storages)AddStorageConditionsCB.SelectedIndex;
                pType.Department = (Departments)DepartmentsCB.SelectedIndex;
                using (StoreContext context = new StoreContext())
                {
                    context.Entry(pType).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе изменения данных продукта, в БД возникла следующая ошибка: {ex.Message}");
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
