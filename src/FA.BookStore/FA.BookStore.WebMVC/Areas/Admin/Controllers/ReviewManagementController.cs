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
    public class ReviewManagementController : Controller
    {
        private readonly IReviewServices _reviewServices;

        public ReviewManagementController(IReviewServices reviewServices)
        {
            _reviewServices = reviewServices;
        }

        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString,
            int? pageIndex = 1, int pageSize = 4)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["BookTitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "bookTitle_desc" : "";
            ViewData["ContentSortParm"] = sortOrder == "Content" ? "content_desc" : "Content";
            ViewData["CreateDateSortParm"] = sortOrder == "CreateDate" ? "createDate_desc" : "CreateDate";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Review, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = b => b.Content.Contains(searchString);
            }

            Func<IQueryable<Review>, IOrderedQueryable<Review>> orderBy = null;

            switch (sortOrder)
            {
                case "bookTitle_desc":
                    orderBy = q => q.OrderByDescending(c => c.Book.Title);
                    break;
                case "Content":
                    orderBy = q => q.OrderBy(c => c.Content);
                    break;
                case "content_desc":
                    orderBy = q => q.OrderByDescending(c => c.Content);
                    break;
                case "CreateDate":
                    orderBy = q => q.OrderBy(c => c.CreatedDate);
                    break;
                case "createDate_desc":
                    orderBy = q => q.OrderByDescending(c => c.CreatedDate);
                    break;
                default:
                    orderBy = q => q.OrderBy(c => c.Book.Title);
                    break;
            }

            var reviews = await _reviewServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(reviews);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ReviewViewModel reviewViewModel)
        {
            if (ModelState.IsValid)
            {
                var review = new Review
                {
                    Id = Guid.NewGuid(),
                    BookId = reviewViewModel.BookId,
                    Content = reviewViewModel.Content,
                    CreatedDate = reviewViewModel.CreatedDate
                };
                var result = await _reviewServices.AddAsync(review);
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

            return View(reviewViewModel);
        }

        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var review = await _reviewServices.GetByIdAsync((Guid)id);
            var reviewViewModel = new ReviewViewModel
            {
                Id = review.Id,
                BookId = review.BookId,
                Content = review.Content,
                CreatedDate = review.CreatedDate,
            };

            return View(reviewViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(ReviewViewModel reviewViewModel)
        {
            if (ModelState.IsValid)
            {
                var review = await _reviewServices.GetByIdAsync(reviewViewModel.Id);
                if (review == null)
                {
                    return HttpNotFound();
                }

                review.BookId = reviewViewModel.BookId;
                review.Content = reviewViewModel.Content;
                review.CreatedDate = reviewViewModel.CreatedDate;

                var result = await _reviewServices.UpdateAsync(review);
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

            return View(reviewViewModel);
        }

        public async Task<ActionResult> Delete(Guid? id)
        {
            var result = await _reviewServices.DeleteAsync((Guid)id);
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