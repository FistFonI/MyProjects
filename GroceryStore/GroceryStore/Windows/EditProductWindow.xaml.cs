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
    /// Логика взаимодействия для EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        private Product product;

        public EditProductWindow(Product product)
        {
            InitializeComponent();
            this.product = product;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddSoldOfProductTB.Text = product.Sold.ToString();
            CountOfProductTB.Text = product.Balance.ToString();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                product.Sold = int.Parse(AddSoldOfProductTB.Text);               
                using (StoreContext context = new StoreContext())
                {
                    context.Entry(product).State = EntityState.Modified;
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
