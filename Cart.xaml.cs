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
        public Cart(Client client, string conn)
        {
            connectionString = conn;
            currentClient = client;
            bookIds = new List<int>();
            bookViews = new ObservableCollection<BookView>();
            loadSelections();
            loadBooks();
            InitializeComponent();


           
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

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("select * from Book where Id in @idList", connection);

                cmd.Parameters.AddWithValue("@idList", strIds);
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.HasRows && reader.Read())
                {
                    Book book = new Book(reader.GetInt32(0), reader.GetString(1), reader.GetFloat(2), reader.GetString(3));
                    currentClient.books.Add(book);
                    bookViews.Add(new BookView(book.Title, book.price));
                }

            }

            cartGrid.ItemsSource = bookViews;
        }
        private void goBackBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
