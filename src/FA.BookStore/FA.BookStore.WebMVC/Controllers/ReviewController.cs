using FA.BookStore.Models.Common;
using FA.BookStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FA.BookStore.WebMVC.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewServices _reviewServices;

        public ReviewController(IReviewServices reviewServices)
        {
            _reviewServices = reviewServices;
        }

        [HttpPost]
        public JsonResult AddReview(Guid bookId, string content)
        {
            var review = new Review();
            review.Id = Guid.NewGuid();
            review.Content = content;
            review.CreatedDate = DateTime.Now;
            review.BookId = bookId;

            _reviewServices.Add(review);

            return Json(new { review.Content, review.CreatedDate }, JsonRequestBehavior.AllowGet);
        }
    }
}