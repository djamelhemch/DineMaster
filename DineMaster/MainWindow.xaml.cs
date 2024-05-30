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

namespace DineMaster
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddToOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuListBox.SelectedItem != null)
            {
                OrderListBox.Items.Add(MenuListBox.SelectedItem);
                UpdateTotal();
            }
        }

        private void RemoveFromOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrderListBox.SelectedItem != null)
            {
                OrderListBox.Items.Remove(OrderListBox.SelectedItem);
                UpdateTotal();
            }
        }

        private void FinalizeOrderButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Order finalized!");
            OrderListBox.Items.Clear();
            UpdateTotal();
        }

        private void OpenDrawerButton_Click(object sender, RoutedEventArgs e)
        {
            DrawerMenu.Visibility = Visibility.Visible;
        }

        private void CloseDrawerButton_Click(object sender, RoutedEventArgs e)
        {
            DrawerMenu.Visibility = Visibility.Collapsed;
        }

        private void SettingsTabButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings tab clicked!");
        }

        private void UpdateTotal()
        {
            double total = 0;
            foreach (var item in OrderListBox.Items)
            {
                // Example prices
                if (item.ToString() == "Pizza") total += 12.00;
                if (item.ToString() == "Burger") total += 8.00;
                if (item.ToString() == "Salad") total += 5.00;
                if (item.ToString() == "Pasta") total += 10.00;
                if (item.ToString() == "Steak") total += 20.00;
            }
            TotalTextBlock.Text = $"Total: ${total:0.00}";
        }
    }
}
