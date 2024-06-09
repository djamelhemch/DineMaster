using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;

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
        public decimal price { get; set; } // Change to decimal for better currency handling
        public string cover { get; set; }
        // Add any other properties you need
    }

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<Item> OrderItems { get; set; } // Collection for Order Items

        private decimal totalAmount;
        public decimal TotalAmount
        {
            get { return totalAmount; }
            set
            {
                totalAmount = value;
                OnPropertyChanged(nameof(TotalAmount));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            Categories = new ObservableCollection<Category>();
            Items = new ObservableCollection<Item>();
            OrderItems = new ObservableCollection<Item>();

            OrderItems.CollectionChanged += (s, e) => UpdateTotalAmount(); // Update total when the collection changes

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
            // Navigate to Home
        }

        private void OrdersButton(object sender, RoutedEventArgs e)
        {
            // Navigate to Orders
        }

        private void SettingsButton(object sender, RoutedEventArgs e)
        {
            // Navigate to Settings
        }

        private void AddToOrderButton(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem listBoxItem && listBoxItem.DataContext is Item item)
            {
                // Add the item to the order
                OrderItems.Add(item);

                // Optionally, show a message to confirm the addition
               // MessageBox.Show($"{item.name} has been added to the order.");
            }
        }

        private void RemoveFromOrderButton(object sender, RoutedEventArgs e)
        {
            // Remove selected item from OrderListBox
            if (OrderListBox.SelectedItem is Item selectedItem)
            {
                OrderItems.Remove(selectedItem);
            }
        }

        private void FinalizeOrderButton(object sender, RoutedEventArgs e)
        {
            // Finalize the order
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            // Handle item selected event if needed
        }

        private void UpdateTotalAmount()
        {
            TotalAmount = OrderItems.Sum(item => item.price);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
