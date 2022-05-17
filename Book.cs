using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;  //BitmapImage namespace

namespace BookStore
{
    internal class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }  
        public double price { get; set; }
        public BitmapImage image { get; set; }

        public Book(string id, string t, double p, BitmapImage i)
        {
            Id = id;
            Title = t;
            price = p;
            image = i;
        }
    }
}
