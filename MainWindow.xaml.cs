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
        string connectionString = "Data Source=LAPTOP-FQ9L2NHN;Initial Catalog=BookStore;Persist Security Info=True;" +
            "User ID=sa;Password=12pass34";
        public MainWindow()
        {
            MessageBox.Show("In the main" + connectionString);
           

            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            using(SqlConnection connection = new SqlConnection(connectionString)){
                connection.Open();

                if( checkLoginData(username.Text.ToString(), password.Password.ToString(), connection))
                {   
                    Client client = getClientObj(username.Text.ToString(), password.Password.ToString(), connection);
                    if (client.name == username.Text.ToString() )
                    {
                       Store store = new Store();
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
            
            MessageBox.Show("Sql command created: "+oCmnd.CommandText);
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

    }
}
