using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Library.CodeFirst;
using Library.CodeFirst.Models;

namespace Library.Controllers
{
    public class GenresController : Controller
    {
        private LibraryContext _db = new LibraryContext();

        #region TODO move to service

        private List<Genre> GetGenresByLinqQuerySintax()
        {
            var linqQueryGenres = from genre in _db.Genres
                                  orderby genre.Description
                                  select genre;

            return linqQueryGenres.ToList();
        }

        private List<Genre> GetGenresByLinqMethodSintax()
        {
            return _db.Genres
                   .OrderBy(g => g.Description)
                   .ToList();
        }

        private IQueryable<Genre> GetGenresQuery()
        {
            return _db.Genres;
        }

        private void GetExplictLoading()
        {
            var firstBook = _db.Books.FirstOrDefault();
            //my Code
            _db.Entry(firstBook).Reference(g=>g.Genre).Load();
            _db.Entry(firstBook.Genre).Collection(g=>g.Books).Load();
        }

        #endregion

        // GET: Genres
        public ActionResult Index()
        {

            #region IQueryable example
            //IQueryable<Genre> queryGenres = GetGenresQuery();
            //var resultQueryGenres = queryGenres.Where(g => g.GenreId > 1).ToList(); 
            #endregion

            #region Lazy loading and N+1 problem
            var genres = _db.Genres.ToList();
            var i = 0;
            foreach (var genre in genres)
            {
                //N+1 problem
                i += genre.Books?.Count() ?? 0;
            }
            #endregion

            #region Eager loading
            var books2 = _db.Books.Include(b => b.Genre).ToList();
            #endregion

            #region Explicit loading
            GetExplictLoading(); 
            #endregion

            return View(genres);
        }

        // GET: Genres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var genre = _db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenreId,Description")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                #region Tracking to add

                _db.Genres.Add(genre);
                var addes = _db.ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();
                var unchangeds = _db.ChangeTracker.Entries().Where(e => e.State == EntityState.Unchanged).ToList();
                _db.SaveChanges();

                var newAddes = _db.ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();
                var newUnchangeds = _db.ChangeTracker.Entries().Where(e => e.State == EntityState.Unchanged).ToList();

                #endregion


                return RedirectToAction("Index");
            }

            return View(genre);
        }

        // GET: Genres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var genre = _db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Genre genre3)
        {
            if (ModelState.IsValid)
            {
                #region Tracking to update

                _db.Entry(genre3).State = EntityState.Modified;
                var modifieds = _db.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified).ToList();
                _db.SaveChanges();

                var unchangeds = _db.ChangeTracker.Entries().Where(e => e.State == EntityState.Unchanged).ToList();

                #endregion

                return RedirectToAction("Index");
            }
            return View(genre3);
        }

        // GET: Genres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var genre = _db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            #region Tracking to delete

            var genre2 = _db.Genres.Find(id);
            _db.Entry(genre2).State = EntityState.Deleted;
            _db.Genres.Remove(genre2);

            var changes = _db.ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted);

            _db.SaveChanges();

            #endregion

            var genre = new Genre() {Description = "Test"};
            _db.Genres.Add(genre);
            _db.Genres.Remove(genre);
            _db.SaveChanges();


            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
