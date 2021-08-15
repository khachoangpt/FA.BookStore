using FA.BookStore.Services;
using System;
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
    }
}