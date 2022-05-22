using System;
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
using System.Data.SqlClient;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        string connectionString = ConfigurationManager.ConnectionStrings["BookStore.Properties.Settings.BookStoreConnectionString"].ConnectionString;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            using(SqlConnection connection = new SqlConnection(connectionString)){
                connection.Open();

                if( true || checkLoginData(username.Text.ToString(), password.Password.ToString(), connection))


                {   
                    Client client = getClientObj(username.Text.ToString(), password.Password.ToString(), connection);
                    if (true || client.name == username.Text.ToString() )
                    {
                       Store store = new Store(client);
                        store.Show();
                        this.Close();
                    }
                    else { 
                        MessageWindow mess = new MessageWindow("Internal Problem arosed. Please try later!", "Internal Error");
                        
                    }
                }
                else
                {
                    MessageBox.Show("Name or password was incorrect. Please check and try again! Or register for a new user", "No user found", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                username.Clear();
                password.Clear();

            }

            
        }

        private SqlCommand buildUserCommand(string username, string pass, SqlConnection connection)
        {
            string command = String.Format( "Select * from Client where nameSurname='{0}' and loginPassword='{1}'", username, pass);
            SqlCommand oCmnd= new SqlCommand(command, connection);
            
            return oCmnd;
        }

        private bool checkLoginData(string username, string pass, SqlConnection connection)
        {
            SqlCommand findUserCmnd = buildUserCommand(username, pass, connection);
            using (SqlDataReader sqlDataReader = findUserCmnd.ExecuteReader()) {  

                return (sqlDataReader.HasRows && sqlDataReader.Read());
            }
        }

        private Client getClientObj(string username, string pass, SqlConnection connection)
        {
            SqlCommand findUserCmnd = buildUserCommand(username, pass, connection);
            using ( SqlDataReader sqlDataReader = findUserCmnd.ExecuteReader()) { 

                if (sqlDataReader.HasRows && sqlDataReader.Read())
                {
                    int id = sqlDataReader.GetInt32(0);
               
                    Client client = new Client(id, username, pass);
                    return client;
                }
            
                return new Client();
            }
        }
        //private void loadBooks(SqlConnection conn)
        private int insertNewUser(string username, string password, SqlConnection conn)
        {
            try {
                SqlCommand insertUserCmnd = new SqlCommand(String.Format("Insert into Client values ('{0}', {1});SELECT CAST(scope_identity() AS int)"
                                                                         ,username, password),
                                                           conn);

                return (Int32)insertUserCmnd.ExecuteScalar();
             }
            catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString());
                return -1;
            }


        } 
        private void register_Click(object sender, RoutedEventArgs e)
        {
            if ( password.Password != null && 
                 password.Password.ToString().All(char.IsDigit)) // check if password consists of only digits
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand sqlCommand = new SqlCommand(String.Format("Select * from Client where nameSurname='{0}'", username.Text), connection);

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                            if (sqlDataReader.HasRows && sqlDataReader.Read())
                            {
                                MessageBox.Show("User with provided username already exists.\nPlease try different username.", "Invalid username.", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                sqlDataReader.Close();
                                int newUserId = insertNewUser(username.Text, password.Password.ToString(), connection);
                                if (newUserId != -1)
                                {
                                    Client client = new Client(newUserId, username.Text, password.Password.ToString());
                                    Store store = new Store(client);
                                    store.Show();
                                    this.Close();

                                }
                                else
                                {
                                    MessageBox.Show("Could not register.\nPlease try again later!", "Internal error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }

                    } catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Password must consist of digits only.\nPlease try again with correct format.", "Invalid password.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            password.Clear();
        }

        

    }
}
