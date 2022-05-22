using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookStore
{
    public class Book
    {
        
        public int Id { get; set; }
        public string Title { get; set; }  
        public double price { get; set; }
        public string imageURI { get; set; }

        public Book(int id, string t, double p, string i)
        {
            Id = id;
            Title = t;
            price = p;
            imageURI = i;
        }
    }
}
