using FA.BookStore.Services;
using System.Web.Mvc;

namespace FA.BookStore.WebMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        public ActionResult CategoryLeftMenu()
        {
            var categories = _categoryServices.GetAll();
            return PartialView("_CategoryLeftMenuPartial", categories);
        }
    }
}