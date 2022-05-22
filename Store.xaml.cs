using System;
using System.Collections;
using System.Collections.ObjectModel;
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
using System.Data.SqlClient;
using System.Configuration;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for Store.xaml
    /// </summary>
    /// 
    
    public partial class Store : Window
    {
        public class ViewBook
        {
            public string Title { get; set; }  
            public float Price { get; set; }

            public ViewBook(string t, float p)
            {
                Title = t;
                Price = p;
            }
        }
        List<Book> books = new List<Book>();
       public ObservableCollection<ViewBook> CollectionSource { get; set; }   

        string connectionString = ConfigurationManager.ConnectionStrings["BookStore.Properties.Settings.BookStoreConnectionString"].ConnectionString;
        public Store(Client client)
        {

            CollectionSource = new ObservableCollection<ViewBook>();
            InitializeComponent();
            loadBooks();
        }

        private BitmapImage LoadImageFromFile(string filename)
        {
            return new BitmapImage(new Uri(filename));
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

        private void loadBooks()
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("Select * from Book", connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["Id"]);
                        string title = Convert.ToString(reader["title"]);
                        float price = Convert.ToSingle(reader["price"]);
                        string imgUri = Convert.ToString(reader["img"]);
                       
                        Book book = new Book(id, title, price, imgUri);
                        books.Add(book);
                        //var b = ;
                        CollectionSource.Add(new ViewBook(title, price));
                        
                    }
                }

            }
            bookListGrid.ItemsSource = CollectionSource;
            //MessageBox.Show(Convert.ToString(bookListGrid.Columns.Count));
        }

        private void bookListGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = bookListGrid.SelectedIndex;
            selectedImage.Source = LoadImageFromFile(books[index].imageURI);
            selectedPrice.Content = "Price: " + books[index].price + " TL";
            selectedTitle.Content = "Title: " + books[index].Title;
        }
    }
}
