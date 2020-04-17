namespace Library.CodeFirst.Models
{
    public class FamousQuote
    {
        public int FamousQuoteId { get; set; }
        public string Description { get; set; }
        public Author Author { get; set; }
        //public int AuthorId { get; set; }
    }
}
