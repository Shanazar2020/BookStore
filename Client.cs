using System;

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

        public IObservable<Book> books { get; set; }
        //public List<Book> newSelectedBooks { get; set; }

        public Client(int id=-1, string name="\0", string pass="\0")
        {
            this.id = id;
            this.name = name;
            this.password = pass;
        }
    }
}
