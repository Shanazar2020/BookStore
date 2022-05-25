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
using System.Data.SqlClient;
using System.Collections.ObjectModel;
namespace BookStore
{
    /// <summary>
    /// Interaction logic for Cart.xaml
    /// </summary>
    ///
    public partial class Cart : Window
    {
        public Client currentClient { get; set; }
        public List<int> bookIds { get; set; }
        public string connectionString { get; set; }

        public ObservableCollection<BookView> bookViews { get; set; }

        double currentPrice = 0;
        public Cart(Client client, string conn)
        {
            connectionString = conn;
            currentClient = client;
            
            currentClient.books = new ObservableCollection<Book>();
            bookIds = new List<int>();
            bookViews = new ObservableCollection<BookView>();
            loadSelections();
            InitializeComponent();
            loadBooks();

            customerName.Content = currentClient.name;
            
           
        }


        private void updatePrice()
        {
            currentCost.Content = String.Format("Current cost: {0}", currentPrice);
        }
        private void loadSelections()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("select * from Selections where clientId=@id", connection);
                cmd.Parameters.AddWithValue("@id", currentClient.id);
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.HasRows && reader.Read())
                {
                    bookIds.Add(reader.GetInt32(1));
                }

            }
        }

        private void loadBooks()
        {

            string strIds = String.Format( "({0})", string.Join(",", bookIds));

                MessageBox.Show(strIds);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(String.Format("select * from Book where Id in {0}", strIds), connection);
               // MessageBox.Show(cmd.CommandText);
               // return; 
                //cmd.Parameters.AddWithValue("@idList", strIds);
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.HasRows && reader.Read())
                {
                    int id = Convert.ToInt32(reader["Id"]);
                    string title = Convert.ToString(reader["title"]);
                    float price = Convert.ToSingle(reader["price"]);
                    string imgUri = Convert.ToString(reader["img"]);
                    
                    Book book = new Book(id, title, price, imgUri);
                    currentClient.books.Add(book);
                    bookViews.Add(new BookView(book.Title, book.price));

                    currentPrice += price;
                }

            }

            cartGrid.ItemsSource = bookViews;
        }
        private void goBackBtn_Click(object sender, RoutedEventArgs e)
        {
            Store store = new Store(currentClient);
            store.Show();
            this.Close();
        }

        private void buyAllbtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("All books in your cart have been purchased.", "Thank you for purchasing.", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void removeSelBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
