using FA.BookStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FA.BookStore.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookServices _bookServices;

        public HomeController(IBookServices bookServices)
        {
            _bookServices = bookServices;
        }

        public ActionResult Index()
        {
            var books = _bookServices.GetLatestBook(8);
            return View(books);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}