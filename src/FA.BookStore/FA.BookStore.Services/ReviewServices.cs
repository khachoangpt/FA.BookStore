using FA.BookStore.Data.Infrastructure;
using FA.BookStore.Models.Common;
using FA.BookStore.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FA.BookStore.Services
{
    public class ReviewServices : BaseServices<Review>, IReviewServices
    {
        public ReviewServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<Review> GetReviewByBook(Guid bookId)
        {
            return _unitOfWork.ReviewRepository.GetQuery().Where(r => r.BookId == bookId).ToList();
        }
    }
}
