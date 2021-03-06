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
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow(string message, string caption="", int sleepTime = 2000)
        {
            
            InitializeComponent();
            messageBoxCaption.Content = caption;
            messageLbl.Content = message;

            this.Show();
            Thread.Sleep(sleepTime);
            this.Close();

        }
    }
}
