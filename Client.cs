using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookStore
{
    public class Client
    {
        public int id { get; set; }
        public string name { get; set; }
        public string password { get; set; }

        public ObservableCollection<Book> books { get; set; }
        

        public Client(int id=-1, string name="\0", string pass="\0")
        {
            this.id = id;
            this.name = name;
            this.password = pass;
            books = new ObservableCollection<Book>();
        
        }
        

    }
}
