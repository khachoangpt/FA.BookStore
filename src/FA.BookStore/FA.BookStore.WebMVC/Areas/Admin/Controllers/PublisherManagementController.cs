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
    public class PublisherManagementController : Controller
    {
        private readonly IPublisherServices _publisherServices;

        public PublisherManagementController(IPublisherServices publisherServices)
        {
            _publisherServices = publisherServices;
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

            Expression<Func<Publisher, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = b => b.Name.Contains(searchString);
            }

            Func<IQueryable<Publisher>, IOrderedQueryable<Publisher>> orderBy = null;

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

            var authors = await _publisherServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(authors);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PublisherViewModel publisherViewModel)
        {
            if (ModelState.IsValid)
            {
                var publisher = new Publisher
                {
                    Id = Guid.NewGuid(),
                    Name = publisherViewModel.Name,
                    Description = publisherViewModel.Description
                };
                var result = await _publisherServices.AddAsync(publisher);
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

            return View(publisherViewModel);
        }

        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var publisher = await _publisherServices.GetByIdAsync((Guid)id);
            var publisherViewModel = new PublisherViewModel
            {
                Id = publisher.Id,
                Name = publisher.Name,
                Description = publisher.Description,
            };

            return View(publisherViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(PublisherViewModel publisherViewModel)
        {
            if (ModelState.IsValid)
            {
                var publisher = await _publisherServices.GetByIdAsync(publisherViewModel.Id);
                if (publisher == null)
                {
                    return HttpNotFound();
                }

                publisher.Name = publisherViewModel.Name;
                publisher.Description = publisherViewModel.Description;

                var result = await _publisherServices.UpdateAsync(publisher);
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

            return View(publisherViewModel);
        }

        public async Task<ActionResult> Delete(Guid? id)
        {
            var result = await _publisherServices.DeleteAsync((Guid)id);
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