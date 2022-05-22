using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    public class BookView
    {

        public string Title { get; set; }
        public double Price { get; set; }

        public BookView(string t, double p)
        {
            Title = t;
            Price = p;
        }
    }
}
