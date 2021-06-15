using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Models.ViewModel
{
    public class AutherBookViewModel
    {
        public int  Bookid { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 5)]
        public string title { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 5)]
        public string description { get; set; }

        public int Autherid { get; set; }

        public List<Auther> authers { get; set; }
        public IFormFile file { get; set; }
        public string imageUrl { get; set; }

    }
}
