using System.Data.Entity;
using System.Diagnostics;
using Library.CodeFirst.ModelConfigurations;
using Library.CodeFirst.Models;

namespace Library.CodeFirst
{
    public class LibraryContext: DbContext
    {
        public LibraryContext(): base("EF_CodeFirst")
        {
            Database.Log = sql => Debug.Write(sql);
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }

        #region One-to-Zero-or-One Author with AuthorAddress
        //public DbSet<Author> Authors { get; set; } 
        #endregion

        #region One to Many Author with FamousQuotes
        //public DbSet<Author> Authors { get; set; }
        //public DbSet<FamousQuote> FamousQuotes { get; set; }
        #endregion

        #region Many to Many Authors with Books
        //public DbSet<Author> Authors { get; set; } 
        //public DbSet<BookAuthors> BookAuthors { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Configurations.Add(new GenreConfiguration());
        }
    }
}
