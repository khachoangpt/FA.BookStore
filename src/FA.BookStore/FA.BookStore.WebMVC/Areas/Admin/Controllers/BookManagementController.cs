using FA.BookStore.Models.Common;
using FA.BookStore.Services;
using FA.BookStore.WebMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FA.BookStore.WebMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BookManagementController : Controller
    {
        private readonly IBookServices _bookServices;
        private readonly ICategoryServices _categoryServices;
        private readonly IPublisherServices _publisherServices;
        private readonly IAuthorServices _authorServices;

        public BookManagementController(
            IBookServices postServices,
            ICategoryServices categoryServices,
            IPublisherServices publisherServices,
            IAuthorServices authorServices
            )
        {
            _bookServices = postServices;
            _categoryServices = categoryServices;
            _publisherServices = publisherServices;
            _authorServices = authorServices;
        }

        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString,
            int? pageIndex = 1, int pageSize = 4)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["SummarySortParm"] = sortOrder == "Summary" ? "summary_desc" : "Summary";
            ViewData["ImageUrlSortParm"] = sortOrder == "ImageUrl" ? "imageUrl_desc" : "ImageUrl";
            ViewData["UnitPriceSortParm"] = sortOrder == "UnitPrice" ? "unitPrice_desc" : "UnitPrice";
            ViewData["QuantitySortParm"] = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";
            ViewData["CategoryNameSortParm"] = sortOrder == "CategoryName" ? "categoryName_desc" : "CategoryName";
            ViewData["PublisherNameSortParm"] = sortOrder == "PublisherName" ? "publisherName_desc" : "PublisherName";
            ViewData["PublishedSortParm"] = sortOrder == "Published" ? "publisher_desc" : "Published";

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
                filter = b => b.Title.Contains(searchString);
            }

            Func<IQueryable<Book>, IOrderedQueryable<Book>> orderBy = null;

            switch (sortOrder)
            {
                case "title_desc":
                    orderBy = q => q.OrderByDescending(c => c.Title);
                    break;
                case "Summary":
                    orderBy = q => q.OrderBy(c => c.Summary);
                    break;
                case "summary_desc":
                    orderBy = q => q.OrderByDescending(c => c.Summary);
                    break;
                case "ImgUrl":
                    orderBy = q => q.OrderBy(c => c.ImgUrl);
                    break;
                case "imgUrl_desc":
                    orderBy = q => q.OrderByDescending(c => c.ImgUrl);
                    break;
                case "UnitPrice":
                    orderBy = q => q.OrderBy(c => c.UnitPrice);
                    break;
                case "unitPrice_desc":
                    orderBy = q => q.OrderByDescending(c => c.UnitPrice);
                    break;
                case "Quantity":
                    orderBy = q => q.OrderBy(c => c.Quantity);
                    break;
                case "quantity_desc":
                    orderBy = q => q.OrderByDescending(c => c.Quantity);
                    break;
                case "CategoryName":
                    orderBy = q => q.OrderBy(c => c.Category.Name);
                    break;
                case "categoryName_desc":
                    orderBy = q => q.OrderByDescending(c => c.Category.Name);
                    break;
                case "PublisherName":
                    orderBy = q => q.OrderBy(c => c.Publisher.Name);
                    break;
                case "publisherName_desc":
                    orderBy = q => q.OrderByDescending(c => c.Publisher.Name);
                    break;
                case "Published":
                    orderBy = q => q.OrderBy(c => c.Published);
                    break;
                case "published_desc":
                    orderBy = q => q.OrderByDescending(c => c.Published);
                    break;
                default:
                    orderBy = q => q.OrderBy(c => c.Title);
                    break;
            }

            var books = await _bookServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(books);
        }

        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryServices.GetAll(), "Id", "Name");
            ViewBag.Publishers = new SelectList(_publisherServices.GetAll(), "Id", "Name");
            var bookViewModel = new BookViewModel
            {
                Authors = _authorServices.GetAll().Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
            };
            return View(bookViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookViewModel bookViewModel)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    Id = Guid.NewGuid(),
                    Title = bookViewModel.Title,
                    Summary = bookViewModel.Summary,
                    ImgUrl = bookViewModel.ImgUrl,
                    UnitPrice = bookViewModel.UnitPrice,
                    Quantity = bookViewModel.Quantity,
                    CategoryId = bookViewModel.CategoryId,
                    PublisherId = bookViewModel.PublisherId,
                    Published = bookViewModel.Published,
                    Authors = await GetSelectedAuthorFromIds(bookViewModel.SelectedAuthorIds)
                };
                var result = await _bookServices.AddAsync(book);

                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(await _categoryServices.GetAllAsync(), "Id", "Name", bookViewModel.CategoryId);
            ViewBag.Publishers = new SelectList(await _publisherServices.GetAllAsync(), "Id", "Name", bookViewModel.PublisherId);
            bookViewModel.Authors = _authorServices.GetAll().Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name });
            return View(bookViewModel);
        }

        private async Task<IList<Author>> GetSelectedAuthorFromIds(IEnumerable<Guid> selectedAuthorIds)
        {
            var authors = new List<Author>();

            var authorEntities = await _authorServices.GetAllAsync();

            foreach (var item in authorEntities)
            {
                if (selectedAuthorIds.Any(x => x == item.Id))
                {
                    authors.Add(item);
                }
            }
            return authors;
        }

        public async Task<ActionResult> Edit(Guid? id)
        {
            var book = await _bookServices.GetByIdAsync((Guid)id);
            var bookViewModel = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Summary = book.Summary,
                ImgUrl = book.ImgUrl,
                UnitPrice = book.UnitPrice,
                Quantity = book.Quantity,
                CategoryId = book.CategoryId,
                PublisherId = book.PublisherId,
                Published = book.Published,
                SelectedAuthorIds = book.Authors.Select(x => x.Id)
            };
            ViewBag.Categories = new SelectList(_categoryServices.GetAll(), "Id", "Name", bookViewModel.CategoryId);
            ViewBag.Publishers = new SelectList(_publisherServices.GetAll(), "Id", "Name", bookViewModel.PublisherId);
            bookViewModel.Authors = _authorServices.GetAll().Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name });
            ViewBag.AuthorList = _authorServices.GetAll();
            return View(bookViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(BookViewModel bookViewModel)
        {
            if (ModelState.IsValid)
            {
                var book = await _bookServices.GetByIdAsync(bookViewModel.Id);
                if (book == null)
                {
                    return HttpNotFound();
                }

                book.Title = bookViewModel.Title;
                book.Summary = bookViewModel.Summary;
                book.ImgUrl = bookViewModel.ImgUrl;
                book.UnitPrice = bookViewModel.UnitPrice;
                book.Quantity = bookViewModel.Quantity;
                book.CategoryId = bookViewModel.CategoryId;
                book.PublisherId = bookViewModel.PublisherId;
                book.Published = bookViewModel.Published;
                await UpdateSelectedAuthorFromIds(bookViewModel.SelectedAuthorIds, book);

                var result = await _bookServices.UpdateAsync(book);
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

            ViewBag.Categories = new SelectList(await _categoryServices.GetAllAsync(), "Id", "Name", bookViewModel.CategoryId);
            ViewBag.Publishers = new SelectList(await _publisherServices.GetAllAsync(), "Id", "Name", bookViewModel.PublisherId);
            bookViewModel.Authors = _authorServices.GetAll().Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name });
            ViewBag.AuthorList = _authorServices.GetAll();

            return View(bookViewModel);
        }

        private async Task UpdateSelectedAuthorFromIds(IEnumerable<Guid> selectedAuthorIds, Book book)
        {
            var authors = book.Authors;
            foreach (var item in authors.ToList())
            {
                book.Authors.Remove(item);
            }
            book.Authors = await GetSelectedAuthorFromIds(selectedAuthorIds);
        }

        public async Task<ActionResult> Delete(Guid? id)
        {
            var result = await _bookServices.DeleteAsync((Guid)id);
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