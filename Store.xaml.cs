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
using System.Threading;
namespace BookStore
{
    /// <summary>
    /// Interaction logic for Store.xaml
    /// </summary>
    public partial class Store : Window
    {
        public Store()
        {
            InitializeComponent();
        }

        private void go2Cart_Click(object sender, RoutedEventArgs e)
        {
            Cart cart = new Cart();
            cart.Show();
            this.Close(); // close current window
        }

        private void logOutBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow message = new MessageWindow("Thank you for visiting us!", "Logging out");
            //message.Show();
            MainWindow main = new MainWindow();

            this.Close();
            
            main.Show();
            
        }
    }
}
