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
    public class CategoryManagementController : Controller
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryManagementController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
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

            Expression<Func<Category, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = b => b.Name.Contains(searchString);
            }

            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null;

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

            var authors = await _categoryServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(authors);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = categoryViewModel.Name,
                    Description = categoryViewModel.Description
                };
                var result = await _categoryServices.AddAsync(category);
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

            return View(categoryViewModel);
        }

        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = await _categoryServices.GetByIdAsync((Guid)id);
            var categoryViewModel = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };

            return View(categoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var category = await _categoryServices.GetByIdAsync(categoryViewModel.Id);
                if (category == null)
                {
                    return HttpNotFound();
                }

                category.Name = categoryViewModel.Name;
                category.Description = categoryViewModel.Description;

                var result = await _categoryServices.UpdateAsync(category);
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

            return View(categoryViewModel);
        }

        public async Task<ActionResult> Delete(Guid? id)
        {
            var result = await _categoryServices.DeleteAsync((Guid)id);
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