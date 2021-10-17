using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TecnicalTestLibraryMVC.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int Genre { get; set; }
        public int NumberOfPages { get; set; }
        public int AuthorId { get; set; }
        public AuthorViewModel AuthorViewModel { get; set; }
    }
}
