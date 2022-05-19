﻿using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string connectionString =
                ConfigurationManager.ConnectionStrings["BookStore.Properties.Settings.BookStoreConnectionString"].ConnectionString;
            System.Console.WriteLine(connectionString);
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            Store store = new Store();
            store.Show();
        }
    }
}
