using System.ComponentModel.DataAnnotations.Schema;

namespace Library.CodeFirst.Models
{
    public class AuthorAddress
    {
        [ForeignKey("Author")]
        public int AuthorAddressId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public virtual Author Author { get; set; }
    }
}
