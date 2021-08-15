using FA.BookStore.Models.Common;
using FA.BookStore.Services.BaseServices;
using System;
using System.Collections.Generic;

namespace FA.BookStore.Services
{
    public interface IReviewServices : IBaseService<Review>
    {
        /// <summary>
        /// Get Review By Book Id
        /// </summary>
        /// <param name="bookId">Id of Book</param>
        /// <returns>List of Review</returns>
        List<Review> GetReviewByBook(Guid bookId);
    }
}
