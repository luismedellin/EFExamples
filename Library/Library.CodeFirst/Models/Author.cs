

using System.Collections.Generic;

namespace Library.CodeFirst.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public virtual AuthorAddress Address { get; set; }
        //public ICollection<FamousQuote> FamousQuotes { get; set; }
        //public ICollection<Book> Books { get; set; }
        //public ICollection<BookAuthors> Books { get; set; }
    }
}
