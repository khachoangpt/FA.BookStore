using FA.BookStore.Models.Common;
using FA.BookStore.Services;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FA.BookStore.WebMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookServices _bookServices;

        public BookController(IBookServices bookServices)
        {
            _bookServices = bookServices;
        }

        public ActionResult Details(Guid id)
        {
            var book = _bookServices.GetById(id);
            return View(book);
        }

        public ActionResult GetBookByCategory(string category)
        {
            var books = _bookServices.GetBooksByCategory(category);
            return View(books);
        }

        public async Task<ActionResult> Search(string currentFilter, string searchString,
            int? pageIndex = 1, int pageSize = 8)
        {

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Book, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = p => p.Title.Contains(searchString);
            }

            Func<IQueryable<Book>, IOrderedQueryable<Book>> orderBy = q => q.OrderBy(c => c.Title);

            var posts = await _bookServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(posts);
        }
    }
}