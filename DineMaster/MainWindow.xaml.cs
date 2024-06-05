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

    public class Category
    {
        public string category_name { get; set; }
        // Add any other properties you need
    }

    public partial class MainWindow : Window
    {
        public ObservableCollection<Category> Categories { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Categories = new ObservableCollection<Category>();
            DataContext = this; // Set the data context of the window to itself
            FetchCategories();
        }

        private async void FetchCategories()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync("http://127.0.0.1:5000/categories");
                var deserializedRes = JsonConvert.DeserializeObject<CategoryResponse>(response);
     
                Console.WriteLine(deserializedRes);


                foreach (var category in deserializedRes.Categories)
                {
                    Categories.Add(category);
                }
            }

        }


/*        private async void FetchItemsByCategory()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync("http://127.0.0.1:5000/categories");
                var deserializedRes = JsonConvert.DeserializeObject<CategoryResponse>(response);

                Console.WriteLine(deserializedRes);


                foreach (var category in deserializedRes.Categories)
                {
                    Categories.Add(category);
                }
            }

        }*/

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Home
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Orders
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Settings
        }

        private void AddToOrderButton_Click(object sender, RoutedEventArgs e)
        {
            // Add selected item to OrderListBox
        }

        private void RemoveFromOrderButton_Click(object sender, RoutedEventArgs e)
        {
            // Remove selected item from OrderListBox
        }

        private void FinalizeOrderButton_Click(object sender, RoutedEventArgs e)
        {
            // Finalize the order
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
