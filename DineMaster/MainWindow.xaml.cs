﻿using System.Text;
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
