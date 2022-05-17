using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    internal class Client
    {
        public string id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }

        public List<Book> books { get; set; }
        public List<Book> newSelectedBooks { get; set; }


    }
}
