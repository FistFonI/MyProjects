using System;
using GroceryStore.Domain;
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
using GroceryStore.Infrastructure;

namespace GroceryStore.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditStorageWindow.xaml
    /// </summary>
    public partial class EditStorageWindow : Window
    {
        private Storage storage;
        
        public EditStorageWindow(Storage storage)
        {
            InitializeComponent();
            this.storage = storage;
            TypeOfStorageCB.ItemsSource = Enum.GetValues(typeof(Storages));
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TypeOfStorageCB.SelectedItem = storage.Type;
            SizeOfStorageTB.Text = storage.Size.ToString();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                storage.Type = (Storages)TypeOfStorageCB.SelectedIndex;
                storage.Size = int.Parse(SizeOfStorageTB.Text);
                using (StoreContext context = new StoreContext())
                {
                    context.Entry(storage).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В ходе изменения данных склада магазина, в БД возникла следующая ошибка: {ex.Message}");
            }
            Close();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
