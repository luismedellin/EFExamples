using System.Collections.Generic;

namespace Library.CodeFirst.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        //public ICollection<Book> Books { get; set; }
    }
}
