using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TecnicalTestLibraryMVC.Models
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string CityOrigin { get; set; }
        public string EMail { get; set; }
        public ICollection<BookViewModel> BookViewModels { get; set; }
    }
}
