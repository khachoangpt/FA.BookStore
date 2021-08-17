using FA.BookStore.Models.Common;
using FA.BookStore.Services;
using FA.BookStore.WebMVC.ViewModels;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FA.BookStore.WebMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AuthorManagementController : Controller
    {
        private readonly IAuthorServices _authorServices;

        public AuthorManagementController(IAuthorServices authorServices)
        {
            _authorServices = authorServices;
        }

        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString,
            int? pageIndex = 1, int pageSize = 4)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DescriptionSortParm"] = sortOrder == "Description" ? "description_desc" : "Description";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Author, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = b => b.Name.Contains(searchString);
            }

            Func<IQueryable<Author>, IOrderedQueryable<Author>> orderBy = null;

            switch (sortOrder)
            {
                case "name_desc":
                    orderBy = q => q.OrderByDescending(c => c.Name);
                    break;
                case "Description":
                    orderBy = q => q.OrderBy(c => c.Description);
                    break;
                case "description_desc":
                    orderBy = q => q.OrderByDescending(c => c.Description);
                    break;
                default:
                    orderBy = q => q.OrderBy(c => c.Name);
                    break;
            }

            var authors = await _authorServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(authors);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AuthorViewModel authorViewModel)
        {
            if (ModelState.IsValid)
            {
                var author = new Author
                {
                    Id = Guid.NewGuid(),
                    Name = authorViewModel.Name,
                    Description = authorViewModel.Description
                };
                var result = await _authorServices.AddAsync(author);
                if (result > 0)
                {
                    TempData["Message"] = "Insert successful!";
                }
                else
                {
                    TempData["Message"] = "Insert failed!";
                }
                return RedirectToAction("Index");
            }

            return View(authorViewModel);
        }

        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var author = await _authorServices.GetByIdAsync((Guid)id);
            var authorViewModel = new AuthorViewModel
            {
                Id = author.Id,
                Name = author.Name,
                Description = author.Description,
            };

            return View(authorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(AuthorViewModel authorViewModel)
        {
            if (ModelState.IsValid)
            {
                var author = await _authorServices.GetByIdAsync(authorViewModel.Id);
                if (author == null)
                {
                    return HttpNotFound();
                }

                author.Name = authorViewModel.Name;
                author.Description = authorViewModel.Description;

                var result = await _authorServices.UpdateAsync(author);
                if (result)
                {
                    TempData["Message"] = "Update successful!";
                }
                else
                {
                    TempData["Message"] = "Update failed!";
                }
                return RedirectToAction("Index");
            }

            return View(authorViewModel);
        }

        public async Task<ActionResult> Delete(Guid? id)
        {
            var result = await _authorServices.DeleteAsync((Guid)id);
            if (result)
            {
                TempData["Message"] = "Delete Successful";
            }
            else
            {
                TempData["Message"] = "Delete failed";
            }
            return RedirectToAction("Index");
        }
    }
}