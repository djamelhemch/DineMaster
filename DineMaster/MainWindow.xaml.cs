using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Diagnostics;

namespace DineMaster
{
    public class CategoryResponse
    {
        public List<Category> Categories { get; set; }
    }
    public class ItemResponse
    {
        public List<Item> Items { get; set; }
    }

    public class Category
    {
        public int Id { get; set; } // Add Id property
        public string category_name { get; set; }
        // Add any other properties you need
    }
    public class Item
    {
        public string name { get; set; }
        public string price { get; set; }
        public string cover { get; set; }
        // Add any other properties you need
    }

    public class Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public partial class MainWindow : Window
    {
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<Table> Tables { get; set; }
        public ObservableCollection<Item> OrderItems { get; set; }
        public decimal TotalAmount { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Categories = new ObservableCollection<Category>();
            Items = new ObservableCollection<Item>();
            Tables = new ObservableCollection<Table>
            {
                new Table { Id = 1, Name = "Table 1" },
                new Table { Id = 2, Name = "Table 2" },
                new Table { Id = 3, Name = "Table 3" },
                new Table { Id = 4, Name = "Table 4" },
                new Table { Id = 1, Name = "Table 5" },
                new Table { Id = 2, Name = "Table 6" },
                new Table { Id = 3, Name = "Table 7" },
                new Table { Id = 4, Name = "Table 8" }
                // Add more tables as needed
            };
            OrderItems = new ObservableCollection<Item>();

            DataContext = this; // Set the data context of the window to itself
            FetchCategories();
        }

        private async void FetchCategories()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync("http://127.0.0.1:5000/categories");
                var deserializedRes = JsonConvert.DeserializeObject<CategoryResponse>(response);

                foreach (var category in deserializedRes.Categories)
                {
                    Categories.Add(category);
                }
            }
        }

        private async void FetchItemsByCategory(int categoryId)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync($"http://127.0.0.1:5000/categories/{categoryId}/items");
                var deserializedRes = JsonConvert.DeserializeObject<ItemResponse>(response);

                Items.Clear(); // Clear previous items if you want to replace them

                foreach (var item in deserializedRes.Items)
                {
                    Items.Add(item);
                }
            }
        }

        private void CategoryButton(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Category category)
            {
                FetchItemsByCategory(category.Id); // Pass the selected category Id
            }
        }

        private void HomeButton(object sender, RoutedEventArgs e)
        {
            // Hide the tables grid and show the main content grid
            TablesGrid.Visibility = Visibility.Collapsed;
            MainContentGrid.Visibility = Visibility.Visible;
        }

        private void OrdersButton(object sender, RoutedEventArgs e)
        {
            // Navigate to Orders
        }

        private void SettingsButton(object sender, RoutedEventArgs e)
        {
            // Navigate to Settings
        }

        private void TablesButton(object sender, RoutedEventArgs e)
        {
            // Hide the main content grid and show the tables grid
            MainContentGrid.Visibility = Visibility.Collapsed;
            TablesGrid.Visibility = Visibility.Visible;
        }

        private void AddToOrderButton(object sender, RoutedEventArgs e)
        {
            if (sender is ListBoxItem listBoxItem && listBoxItem.DataContext is Item item)
            {
                OrderItems.Add(item);
                CalculateTotalAmount();
            }
        }

        private void RemoveFromOrderButton(object sender, RoutedEventArgs e)
        {
            if (OrderListBox.SelectedItem is Item selectedItem)
            {
                OrderItems.Remove(selectedItem);
                CalculateTotalAmount();
            }
        }

        private void FinalizeOrderButton(object sender, RoutedEventArgs e)
        {
            // Finalize the order
        }

        private void TableButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Table table)
            {
                //MessageBox.Show($"You have selected {table.Name}.");
                // Perform any additional actions here, such as storing the selected table
                // Close the tables view and return to the main content view
                TablesGrid.Visibility = Visibility.Collapsed;
                MainContentGrid.Visibility = Visibility.Visible;
            }
        }

        private void CalculateTotalAmount()
        {
            TotalAmount = 0;
            foreach (var item in OrderItems)
            {
                if (decimal.TryParse(item.price, out var price))
                {
                    TotalAmount += price;
                }
            }
            TotalTextBlock.Text = $"Total: {TotalAmount:C}";
        }
    }
}
