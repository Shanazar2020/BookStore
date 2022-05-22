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
        public static SqlConnection connection = new SqlConnection("Data Source=LAPTOP-FQ9L2NHN;Initial Catalog=BookStore;Persist Security Info=True;" +
            "User ID=sa;Password=12pass34");
        public MainWindow()
        {
           // MessageBox.Show("In the main");
           
            try {
                connection.Open();
            }
            catch (SqlException ex) {
                MessageWindow mess = new BookStore.MessageWindow("Sorry for incon", "Internal Error");
            }
            
            //App.connection.Open();
            //loadBooks(App.connection);
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("In the login");
            if( checkLoginData(username.Text.ToString(), password.Password.ToString()))
            {   
                Store store = new Store();
                store.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("You are fucked up.");
            }

            
        }

        private SqlCommand buildUserCommand(string username, string pass)
        {
            string command = "Select * from Client where nameSurname=@usrname and loginPassword=@pass";
            SqlCommand oCmnd= new SqlCommand(command, connection);
            oCmnd.Parameters.AddWithValue("@usrname", username);
            oCmnd.Parameters.AddWithValue("@pass", pass);

            return oCmnd;
        }

        private bool checkLoginData(string username, string pass)
        {
            SqlCommand findUserCmnd = buildUserCommand(username, pass);
            SqlDataReader sqlDataReader = findUserCmnd.ExecuteReader();

            return (sqlDataReader.HasRows && sqlDataReader.Read());
        }

        private Client getClientObj(string username, string pass)
        {
            SqlCommand findUserCmnd = buildUserCommand(username, pass);
            SqlDataReader sqlDataReader = findUserCmnd.ExecuteReader();

            if (sqlDataReader.HasRows && sqlDataReader.Read())
            {
                string id = sqlDataReader.GetString(0);
                string username = sqlDataReader.GetString(1);
                string password = sqlDataReader.GetString(2);

                Client client = new Client(id, username, password);
                return client;
            }
            else
            {
                return null;
            }

        private void loadBooks(SqlConnection conn)
        {
            MessageBox.Show("in the load books");
            SqlCommand loadCommand = new SqlCommand("Select * from Book", conn);
            SqlDataReader reader = loadCommand.ExecuteReader();
            if (reader.HasRows && reader.Read())
            {
                MessageBox.Show(reader.GetString(0));
                MessageBox.Show(reader.GetString(1));
                MessageBox.Show(reader.GetString(2));
                MessageBox.Show(reader.GetString(3));
               
            }

         

        }
    }
}
