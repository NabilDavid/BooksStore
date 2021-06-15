using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Models
{
    public class Book
    {

        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Auther Auther { get; set; }
        public string imageUrl { get; set; }

    }
}
