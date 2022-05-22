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
using System.Collections.Specialized;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for Store.xaml
    /// </summary>
    /// 
    
    public partial class Store : Window
    {
        public Client currentClient { get; set; }      
        
        List<Book> books = new List<Book>();
       public ObservableCollection<BookView> CollectionSource { get; set; }   

        string connectionString = ConfigurationManager.ConnectionStrings["BookStore.Properties.Settings.BookStoreConnectionString"].ConnectionString;
        public Store(Client client)
        {
            currentClient = client;
            CollectionSource = new ObservableCollection<BookView>();
            currentClient.books.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CollectionChangedMethod);
            InitializeComponent();
            loadBooks();
        }

        private bool existsInCart(int bookId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    
                    SqlCommand checkCommand = new SqlCommand( "select * from Selections where bookId = @bookID and clientId=@clientID", conn);
                    checkCommand.Parameters.AddWithValue("@bookID", bookId);
                    checkCommand.Parameters.AddWithValue("@clientID", currentClient.id);
                  
                    conn.Open();
                    SqlDataReader reader = checkCommand.ExecuteReader();

                    if( reader.HasRows && reader.Read())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return true;
            }
        }
        private void CollectionChangedMethod(object sender, NotifyCollectionChangedEventArgs e)
        {
            //different kind of changes that may have occurred in collection
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        int bookId = books[bookListGrid.SelectedIndex].Id;
                        SqlCommand insertCommand = new SqlCommand(String.Format("Insert into Selections values({0}, {1})",
                            bookId, currentClient.id), conn);
                        conn.Open();
                        insertCommand.ExecuteNonQuery();
                    }
                    //MessageWindow mes = new MessageWindow("Book added to cart", "Success");
                    MessageBox.Show("Book added to cart successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }
        private BitmapImage LoadImageFromFile(string filename)
        {
            return new BitmapImage(new Uri(filename));
        }
        private void go2Cart_Click(object sender, RoutedEventArgs e)
        {
            Cart cart = new Cart(currentClient, connectionString);
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
                        CollectionSource.Add(new BookView(title, price));
                        
                    }
                }

            }
            bookListGrid.ItemsSource = CollectionSource;
            //MessageBox.Show(Convert.ToString(bookListGrid.Columns.Count));
        }

        private void bookListGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = bookListGrid.SelectedIndex;
            if (index == -1) { return; }
            selectedImage.Source = LoadImageFromFile(books[index].imageURI);
            selectedPrice.Content = "Price: " + books[index].price + " TL";
            selectedTitle.Content = "Title: " + books[index].Title;
        }

        private void add2CartBtn_Click(object sender, RoutedEventArgs e)
        {
            int index = bookListGrid.SelectedIndex;
            if (!existsInCart(books[index].Id))
            {
                currentClient.books.Add(books[index]);
            }
            else
            {
                MessageBox.Show("Selected book is already in your cart!");
            }

            }
        }
}
