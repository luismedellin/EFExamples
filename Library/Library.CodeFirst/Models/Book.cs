using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.CodeFirst.Models
{
    //[Table("Book")]
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        //public Genre Genre { get; set; }

        //public ICollection<BookAuthors> Authors { get; set; }
    }
}
