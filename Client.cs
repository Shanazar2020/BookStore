using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    public class Client
    {
        public string id { get; set; }
        public string name { get; set; }
        public string password { get; set; }

        public List<Book> books { get; set; }
        public List<Book> newSelectedBooks { get; set; }

        public Client(string id, string name, string pass)
        {
            this.id = id;
            this.name = name;
            this.password = pass;
        }
    }
}
